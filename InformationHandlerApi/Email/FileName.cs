using System;
using System.Net.Mail;
using System.Net.Mime;

namespace InformationHandlerApi.Email
{
	public class EmailManager
	{
		private const string m_HostName = "smtp-mail.outlook.com";

		public void SendMail(EmailDestinyConfiguration emailConfig, EmailContent content)
		{
			MailMessage msg = ConstructEmailMessage(emailConfig, content);
			Send(msg, emailConfig);
		}

		private static MailMessage ConstructEmailMessage(EmailDestinyConfiguration emailConfig, EmailContent content)
		{
			MailMessage msg = new MailMessage();

			if (emailConfig.Destinations is null)
			{
				throw new InvalidOperationException("Must set Destinations property");
			}

			if (emailConfig.Destinations.All(d => string.IsNullOrEmpty(d)))
			{
				throw new InvalidOperationException("Must set Destinations property");
			}

			foreach (string to in emailConfig.Destinations)
			{
				if (string.IsNullOrEmpty(to) is false)
				{
					msg.To.Add(to);
				}
			}

			if (emailConfig.CCs is not null)
			{
				foreach (string cc in emailConfig.CCs)
				{
					if (string.IsNullOrEmpty(cc) is false)
					{
						msg.CC.Add(cc);
					}
				}
			}

			msg.From = new MailAddress(emailConfig.From,
									   emailConfig.FromDisplayName,
									   System.Text.Encoding.UTF8);

			msg.IsBodyHtml = content.IsHtml;
			
			msg.Body = content.Content;
			
			msg.Priority = emailConfig.Priority;
			
			msg.Subject = emailConfig.Subject;
			
			msg.BodyEncoding = System.Text.Encoding.UTF8;
			
			msg.SubjectEncoding = System.Text.Encoding.UTF8;

			if (content.AttachFileName != null)
			{
				Attachment data = new Attachment(content.AttachFileName,
												 MediaTypeNames.Application.Zip);
				msg.Attachments.Add(data);
			}

			return msg;
		}

		private static void Send(MailMessage message, EmailDestinyConfiguration emailConfig)
		{
			var client = new SmtpClient();
			client.UseDefaultCredentials = false;
			client.Credentials = new System.Net.NetworkCredential(
								  emailConfig.ClientCredentialUserName,
								  emailConfig.ClientCredentialPassword);
			client.Host = m_HostName;
			client.Port = 25;  // this is critical
			client.EnableSsl = true;  // this is critical

			try
			{
				client.Send(message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error in Send email: {0}", e.Message);
				throw;
			}
			message.Dispose();
		}
	}
}