namespace ClientServer.Client.Operators.Contracts
{
    public interface ICustomSnackbarOperator
    {
        string BannerClass { get; set; }
        bool IsVisible { get; set; }
        string? Message { get; set; }

        string? SubMessage { get; set; }

        void Hide();
        void ShowErrorMessage(string message, string subMessage = "");
        void ShowInformationMessage(string message, string subMessage = "");
        void ShowWarningMessage(string message, string subMessage = "");
    }
}