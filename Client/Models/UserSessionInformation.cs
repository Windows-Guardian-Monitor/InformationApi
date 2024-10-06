namespace ClientServer.Client.Models
{
	public class UserSessionInformation
	{
		public UserSessionInformation(string userName, string role, string jwtToken)
		{
			UserName = userName;
			Role = role;
			JwtToken = jwtToken;
		}

		public string UserName { get; set; }
		public string Role { get; set; }
        public string JwtToken { get; set; }
    }
}
