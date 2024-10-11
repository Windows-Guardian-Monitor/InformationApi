using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database.Models.Authentication;
using ClientServer.Shared.DataTransferObjects.Authentication;
using ClientServer.Shared.Reponses;
using ClientServer.Shared.Requests.User;
using InformationHandlerApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InformationHandlerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly IUserRepository _userRepository;
		private readonly IEmailService _emailService;
		private readonly IPasswordService _passwordService;

		public AuthController(IConfiguration configuration, IUserRepository userRepository, IEmailService emailService, IPasswordService passwordService)
		{
			_configuration = configuration;
			_userRepository = userRepository;
			_emailService = emailService;
			_passwordService = passwordService;
		}

		[HttpPost("Login")]
		public ActionResult<LoginResponse> Login(UserDto userDto)
		{
			try
			{
				var token =
						"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";

				if (_userRepository.ExistsByUserName(userDto.UserName) is false)
				{
					return LoginResponse.Create(string.Empty, string.Empty, string.Empty, requestPasswordChange: false, StandardResponse.CreateConflict("Usuário ou senha incorretos"));
				}

				var dbUser = _userRepository.GetUserByUserName(userDto.UserName);

				if (dbUser is null)
				{
					return LoginResponse.Create(string.Empty, string.Empty, string.Empty, requestPasswordChange: false, StandardResponse.CreateConflict("Usuário ou senha incorretos"));
				}

				if (VerifyPasswordHash(userDto.Password, dbUser.PasswordHash, dbUser.PasswordSalt) is false)
				{
					return LoginResponse.Create(string.Empty, string.Empty, string.Empty, requestPasswordChange: false, StandardResponse.CreateConflict("Usuário ou senha incorretos"));
				}

				token = CreateToken(dbUser);

				if (dbUser.HasChangedPassword is false)
				{
					return LoginResponse.Create(token, dbUser.UserName, dbUser.Role, requestPasswordChange: true, StandardResponse.CreateOkResponse());
				}

				if (dbUser.HasLoggedIn is false)
				{
					dbUser.HasLoggedIn = false;
					dbUser.CanChangePassword = true;
					dbUser.HasChangedPassword = false;
					_userRepository.Update(dbUser);
					return LoginResponse.Create(token, dbUser.UserName, dbUser.Role, requestPasswordChange: true, StandardResponse.CreateOkResponse());
				}

				return LoginResponse.Create(token, dbUser.UserName, dbUser.Role, requestPasswordChange: false, StandardResponse.CreateOkResponse());
			}
			catch (Exception e)
			{
				return LoginResponse.Create(string.Empty, string.Empty, string.Empty, requestPasswordChange: false, StandardResponse.CreateInternalServerErrorResponse(e.Message));
			}
		}

		private string CreateToken(DbUser user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var jwtSecurityToken = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

			return jwt;
		}

		[HttpPost("ResetPassword")]
		public StandardResponse ResetPassword(ResetPasswordDto request)
		{
			try
			{
				if (string.IsNullOrEmpty(request.Email))
				{
					throw new Exception("Por favor preencha o e-mail");
				}

				if (_userRepository.ExistsByEmail(request.Email) is false)
				{
					return StandardResponse.CreateOkResponse();
				}

				var user = _userRepository.GetUserByEmail(request.Email);

				if (user is null)
				{
					return StandardResponse.CreateOkResponse();
				}

				var password = _passwordService.Create();

				CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

				user.HasLoggedIn = false;
				user.PasswordHash = passwordHash;
				user.PasswordSalt = passwordSalt;
				user.CanChangePassword = true;
				user.HasChangedPassword = false;

				_userRepository.Update(user);

				_emailService.SendResetPassword(password, user.UserName, user.Email);

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpPost("NewPassword")]
		public StandardResponse NewPassword(NewPasswordDto request)
		{
			try
			{
				var user = _userRepository.GetUserByUserName(request.UserName);

				if (string.IsNullOrEmpty(request.UserName))
				{
					throw new Exception("Por favor preencha o nome de usuário");
				}

				if (string.IsNullOrEmpty(request.Password))
				{
					throw new Exception("Por favor preencha a senha");
				}

				if (string.IsNullOrEmpty(request.PasswordRepeat))
				{
					throw new Exception("Por favor preencha o segundo campo de senha");
				}

				if (user is null)
				{
					return StandardResponse.CreateOkResponse();
				}

				if (user.CanChangePassword is false)
				{
					throw new Exception("Para trocar sua senha por favor clique em \"Esqueci minha senha...\" na tela de login");
				}

				var (isValid, message) = _passwordService.IsPasswordValid(request.Password, request.PasswordRepeat);

				if (isValid is false)
				{
					return StandardResponse.CreateConflict(message);
				}

				var password = request.Password;

				CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

				user.HasLoggedIn = true;
				user.CanChangePassword = false;
				user.HasChangedPassword = true;
				user.PasswordHash = passwordHash;
				user.PasswordSalt = passwordSalt;

				_userRepository.Update(user);

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpPost("Register")]
		public StandardResponse RegisterUser(UserDto request)
		{
			try
			{
				if (_userRepository.ExistsByUserName(request.UserName))
				{
					return new StandardResponse("Nome de usuário inválido", false, HttpStatusCode.Conflict);
				}

				if (string.IsNullOrEmpty(request.Password))
				{
					request.Password = _passwordService.Create();
				}

				CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

				var user = new DbUser()
				{
					PasswordHash = passwordHash,
					PasswordSalt = passwordSalt,
					UserName = request.UserName,
					Role = request.IsAdmin ? "Administrator" : "Common",
					Email = request.Email,
					CanChangePassword = true,
					HasChangedPassword = false
				};

				if (string.IsNullOrEmpty(request.Email) is false)
				{
					_emailService.SendNewUserRegistration(request.Password, request.UserName, request.Email);
				}

				_userRepository.Insert(user);

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

				return computedHash.SequenceEqual(passwordHash);
			}
		}

		[HttpPost("GetAllUsers")]
		public async Task<UsersResponse> GetAllUsers(UserListRequest userListRequest)
		{
			try
			{
				var users = _userRepository.GetAll().Select(u => new DbUserWithoutPassword
				{
					Email = u.Email,
					IsAdmin = u.Role.Equals("Administrator", StringComparison.OrdinalIgnoreCase) ? true : false,
					UserName = u.UserName
				}).ToList();

				if (users == null || users.Count is 0)
				{
					return new UsersResponse(null, "Não foi possível encontrar os usuários", false, HttpStatusCode.OK);
				}

				return new UsersResponse(users, string.Empty, false, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new UsersResponse(null, e.Message, false, HttpStatusCode.OK);
			}
		}
	}
}
