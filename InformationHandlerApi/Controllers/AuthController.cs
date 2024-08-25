using ClientServer.Shared.Database.Models.Authentication;
using ClientServer.Shared.DataTransferObjects.Authentication;
using InformationHandlerApi.Business.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InformationHandlerApi.Controllers
{
	//TODO PARAR DE USAR O CAMPO ESTÁTICO E ACESSAR O BANCO DE DADOS DIRETAMENTE
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public AuthController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost("Login")]
		public async Task<ActionResult<string>> Login(UserDto request)
		{
			var token =
				"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";

			if (user.UserName.Equals(request.UserName, StringComparison.OrdinalIgnoreCase) is false)
			{
				return BadRequest("Usuário ou senha incorretos");
			}

			if (VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt) is false)
			{
				return BadRequest("Usuário ou senha incorretos");
			}

			token = CreateToken(user);

			return Ok(token);
		}

		private string CreateToken(DbUser user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var jwtSecurityToken = new JwtSecurityToken(
				claims: claims, 
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

			return jwt;
		}


		public static DbUser user = new DbUser();
		[HttpPost("Register")]
		public async Task<ActionResult<StandardResponse>> RegisterUser(UserDto request)
		{
			CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

			user.PasswordSalt = passwordSalt;
			user.PasswordHash = passwordHash;
			user.UserName = request.UserName;

			return StandardResponse.CreateOkResponse();
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
