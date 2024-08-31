using System.Net.Mail;

namespace InformationHandlerApi.Email
{
	public class EmailDestinyConfiguration
	{
		public EmailDestinyConfiguration(string from, string subject, string clientCredentialUserName, string clientCredentialPassword, string[] tOs)
		{
			From = from;
			Subject = subject;
			ClientCredentialUserName = clientCredentialUserName;
			ClientCredentialPassword = clientCredentialPassword;
			Destinations = tOs;
		}

		public string[] CCs { get; set; } = Array.Empty<string>();
		public string FromDisplayName { get; private set; } = "WGM Administrator";
		public MailPriority Priority { get; set; }
		
		public string[] Destinations { get; private set; } = Array.Empty<string>();
		public string From { get; private set; }
		public string Subject { get; private set; }
		public string ClientCredentialUserName { get; private set; }
		public string ClientCredentialPassword { get; private set; }
	}
}
