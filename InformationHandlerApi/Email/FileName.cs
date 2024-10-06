using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace InformationHandlerApi.Email
{
	public class EmailManager
	{
		private const string host = "smtp.gmail.com";
		private const int port = 587;
		private const string email = "windowsguardianmonitor@gmail.com";
		private const string appKey = "yobt ckvw zuqo mcig";
		private const string displayName = "WGM Administrator";

		public void SendMail(EmailContent content, DestinationConfiguration destinationConfiguration)
		{
			MailMessage msg = CreateEmailMessage(content, destinationConfiguration);
			Send(msg);
		}

		private static MailMessage CreateEmailMessage(EmailContent content, DestinationConfiguration destinationConfiguration)
		{
			var msg = new MailMessage();

			msg.From = new MailAddress(email,
									   displayName,
									   System.Text.Encoding.UTF8);

			msg.IsBodyHtml = content.IsHtml;

			msg.Body = content.Content;

			msg.Priority = destinationConfiguration.Priority;

			msg.Subject = destinationConfiguration.Subject;

			msg.BodyEncoding = System.Text.Encoding.UTF8;

			msg.SubjectEncoding = System.Text.Encoding.UTF8;


			if (destinationConfiguration.Destinations is null || destinationConfiguration.Destinations.Length is 0)
			{
				throw new Exception("Por favor preencha o e-mail");
			}

			foreach (var destination in destinationConfiguration.Destinations)
			{
				msg.To.Add(destination);
			}

			if (content.AttachFileName != null)
			{
				Attachment data = new Attachment(content.AttachFileName,
												 MediaTypeNames.Application.Zip);
				msg.Attachments.Add(data);
			}

			return msg;
		}

		private static void Send(MailMessage message)
		{
			var client = new SmtpClient(host, port)
			{
				Credentials = new NetworkCredential(
								  email,
								  appKey),

				EnableSsl = true  // this is critical
			};


			try
			{
				client.Send(message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error in Send email: {0}", e.Message);
				throw;
			}
			finally
			{
				client.Dispose();
				message.Dispose();
			}
		}
	}
}