using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database.Models.Authentication;
using ClientServer.Shared.DataTransferObjects.Authentication;
using ClientServer.Shared.Reponses;
using InformationHandlerApi.Business.Responses;
using InformationHandlerApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PasswordGenerator;
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

		public AuthController(IConfiguration configuration, IUserRepository userRepository, IEmailService emailService)
		{
			_configuration = configuration;
			_userRepository = userRepository;
			_emailService = emailService;
		}

		[HttpPost("Login")]
		public ActionResult<LoginResponse> Login(UserDto userDto)
		{
			try
			{
				var token =
						"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";

				if (_userRepository.Exists(userDto.UserName) is false)
				{
					return LoginResponse.Create(string.Empty, string.Empty, string.Empty, requestPasswordChange: false, StandardResponse.CreateConflict("Usuário ou senha incorretos"));
				}

				var dbUser = _userRepository.GetUser(userDto.UserName);

				if (dbUser is null)
				{
					return LoginResponse.Create(string.Empty, string.Empty, string.Empty, requestPasswordChange: false, StandardResponse.CreateConflict("Usuário ou senha incorretos"));
				}

				if (VerifyPasswordHash(userDto.Password, dbUser.PasswordHash, dbUser.PasswordSalt) is false)
				{
					return LoginResponse.Create(string.Empty, string.Empty, string.Empty, requestPasswordChange: false, StandardResponse.CreateConflict("Usuário ou senha incorretos"));
				}

				token = CreateToken(dbUser);

				if (dbUser.HasLoggedIn is false)
				{
					_userRepository.SetUserAlreadyLoggedIn(dbUser);
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

		[HttpPost("Register")]
		public StandardResponse RegisterUser(UserDto request)
		{
			try
			{
				if (_userRepository.Exists(request.UserName))
				{
					return new StandardResponse("Nome de usuário inválido", false, HttpStatusCode.Conflict);
				}

				if (string.IsNullOrEmpty(request.Password))
				{
					var createdPassword = new Password(14).IncludeLowercase().IncludeNumeric().IncludeSpecial().IncludeUppercase();
					request.Password = createdPassword.Next();
				}

				CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

				var user = new DbUser()
				{
					PasswordHash = passwordHash,
					PasswordSalt = passwordSalt,
					UserName = request.UserName,
					Role = request.IsAdmin ? "Administrator" : "Common",
					Email = request.Email
				};

				if (string.IsNullOrEmpty(request.Email) is false)
				{
					_emailService.Send(request.Password, request.UserName, request.Email);
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
	}
}
