using System.ComponentModel.DataAnnotations;

namespace ClientServer.Shared.Database.Models.Authentication
{
	public class DbUser
	{
		[Key]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
		public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
		public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public string Role { get; set; }
        public bool HasLoggedIn { get; set; }
        public string Email { get; set; }
    }
}
