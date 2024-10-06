namespace ClientServer.Shared.DataTransferObjects.Authentication
{
	public class NewPasswordDto
	{
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
    }
}
