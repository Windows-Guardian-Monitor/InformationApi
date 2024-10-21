namespace InformationHandlerApi.Contracts
{
    public interface IPasswordService
    {
        string Create();
		(bool, string) IsPasswordValid(string password, string passwordRepeat);
    }
}