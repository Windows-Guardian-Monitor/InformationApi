using System.Text.Json.Serialization;

namespace ClientServer.Shared.Database.Models.Authentication
{
	public class DbUserWithoutPassword
	{
		[JsonPropertyName("UserName")]
		public string UserName { get; set; } = string.Empty;

		private bool _isAdmin;

		[JsonPropertyName("IsAdmin")]
		public bool IsAdmin { get => _isAdmin;  set
			{
				if (value)
				{
					UserType = "Administrador";
				}
                else
                {
					UserType = "Usuário comum";
                }

				_isAdmin = value;
            }
		}
		
		[JsonPropertyName("Email")]
		public string Email { get; set; } = string.Empty;

		[JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonIgnore]
		public string UserType { get; set; } = string.Empty;
	}
}
