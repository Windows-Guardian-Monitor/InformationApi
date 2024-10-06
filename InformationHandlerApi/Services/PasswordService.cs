using InformationHandlerApi.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PasswordGenerator;

namespace InformationHandlerApi.Services
{
	public class PasswordService : IPasswordService
	{
		private const int passwordLength = 8;
		private const string LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
		private const string UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string NumericCharacters = "0123456789";
		private const string DefaultSpecialCharacters = @"!#$%&*@\";
		public string Create() => new Password(passwordLength).IncludeLowercase().IncludeNumeric().IncludeSpecial().IncludeUppercase().Next();
		public (bool, string) IsPasswordValid(string password, string passwordRepeat)
		{
			if (password.Length < passwordLength)
			{
				return (false, "A senha deve ter pelo menos 8 caracteres");
			}

			if (DefaultSpecialCharacters.Any(special => password.Any(c => c.Equals(special))) is false)
			{
				return (false, "A senha deve ter pelo menos um caractere special");
			}

			if (NumericCharacters.Any(special => password.Any(c => c.Equals(special))) is false)
			{
				return (false, "A senha deve ter pelo menos um número");
			}

			if (UppercaseCharacters.Any(special => password.Any(c => c.Equals(special))) is false)
			{
				return (false, "A senha deve ter pelo menos uma letra maiúscula");
			}

			if (LowercaseCharacters.Any(special => password.Any(c => c.Equals(special))) is false)
			{
				return (false, "A senha deve ter pelo menos uma letra minúscula");
			}

			if (password.Equals(passwordRepeat) is false)
			{
				return (false, "As senhas devem ser iguais");
			}

			return (true, string.Empty);
		}
	}
}
