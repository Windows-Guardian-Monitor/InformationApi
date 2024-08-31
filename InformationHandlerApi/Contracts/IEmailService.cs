namespace InformationHandlerApi.Contracts
{
    public interface IEmailService
    {
        void Send(string password, string userName, string emailDestination);
    }
}