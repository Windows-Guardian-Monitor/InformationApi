namespace InformationHandlerApi.Contracts
{
    public interface IEmailService
    {
        void SendNewUserRegistration(string password, string userName, string emailDestination);
        void SendResetPassword(string password, string userName, string emailDestination);
	}
}