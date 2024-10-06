using System.Net.Mail;

namespace InformationHandlerApi.Email
{
	public class DestinationConfiguration
	{
		public DestinationConfiguration(string subject, MailPriority priority, string[] destinations)
		{
			Priority = priority;
			Subject = subject;
			Destinations = destinations;
		}

		public MailPriority Priority { get; set; }
		public string Subject { get; set; } = string.Empty;
		public string[] Destinations { get; private set; } = Array.Empty<string>();
	}
}
