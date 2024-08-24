namespace ClientServer.Client.Operators.Contracts
{
    public interface ICustomSnackbarOperator
    {
        string BannerClass { get; set; }
        bool IsVisible { get; set; }
        string? Message { get; set; }

        void Hide();
        void ShowErrorMessage(string message);
        void ShowInformationMessage(string message);
        void ShowWarningMessage(string message);
    }
}