using InformationHandlerApi.Contracts;
using InformationHandlerApi.Email;
using System.Net.Mail;

namespace InformationHandlerApi.Services
{
    public class EmailService : IEmailService
	{
		//private const string email = "w-guardian-monitor@outlook.com";
		//private const string credentialPassword = "&SUf6T7d4z";

		public void SendNewUserRegistration(string password, string userName, string emailDestination)
		{
			var manager = new EmailManager();

			const string subject = "Seu usuário foi criado";

			var destinyConfiguration = new DestinationConfiguration(subject,MailPriority.High, new[] { emailDestination });

			var emailContent = new EmailContent()
			{
				Content =
				"<div style =\"background-color: #ffffff; padding: 20px;margin: 20px auto;border-radius: 8px;box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);max-width: 600px; \">" +
					"<h1 style=\"color: #333333;\">Bem-vindo(a)!</h1>" +
					"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">Olá,</p>" +
					"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">Seu usuário foi criado com sucesso. Abaixo estão suas credenciais de acesso:</p>" +
					"<div style=\"margin: 20px 0;background-color: #f9f9f9;padding: 15px;border-left: 4px solid #007bff;\">" +
						$"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\"><strong>Nome de usuário:</strong> {userName}</p>" +
						$"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\"><strong>Senha:</strong> {password}</p>" +
					"</div>" +
					"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">Por favor, faça login no sistema utilizando as credenciais fornecidas, no primeiro login a troca da senha será solicitada.</p>" +
					"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">Se você tiver alguma dúvida, entre em contato com seu administrador.</p>" +
					"</div>" +
					"<div style=\"text-align: center;color: #888888;font-size: 12px;margin-top: 20px;\">" +
						"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">&copy; 2024 WGM. Todos os direitos reservados.</p>" +
				"</div>",
				IsHtml = true
			};

			manager.SendMail(emailContent, destinyConfiguration);
		}

		public void SendResetPassword(string password, string userName, string emailDestination)
		{
			var manager = new EmailManager();

			const string subject = "Sua senha foi alterada";

			var destinyConfiguration = new DestinationConfiguration(subject, MailPriority.High, new[] { emailDestination });

			var emailContent = new EmailContent()
			{
				Content =
				"<div style =\"background-color: #ffffff; padding: 20px;margin: 20px auto;border-radius: 8px;box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);max-width: 600px; \">" +
					"<h1 style=\"color: #333333;\">Bem-vindo(a)!</h1>" +
					"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">Olá,</p>" +
					"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">Sua senha foi alterada . Abaixo estão suas credenciais atualizadas:</p>" +
					"<div style=\"margin: 20px 0;background-color: #f9f9f9;padding: 15px;border-left: 4px solid #007bff;\">" +
						$"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\"><strong>Nome de usuário:</strong> {userName}</p>" +
						$"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\"><strong>Senha:</strong> {password}</p>" +
					"</div>" +
					"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">Por favor, faça login no sistema utilizando as credenciais fornecidas, a troca da senha será solicitda.</p>" +
					"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">Se você tiver alguma dúvida, entre em contato com seu administrador.</p>" +
					"</div>" +
					"<div style=\"text-align: center;color: #888888;font-size: 12px;margin-top: 20px;\">" +
						"<p style=\"font-size: 16px;color: #555555;line-height: 1.5;\">&copy; 2024 WGM. Todos os direitos reservados.</p>" +
				"</div>",
				IsHtml = true
			};

			manager.SendMail(emailContent, destinyConfiguration);
		}
	}
}
