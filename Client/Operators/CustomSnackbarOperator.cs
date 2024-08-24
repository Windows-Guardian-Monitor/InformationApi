using ClientServer.Client.Operators.Contracts;

namespace ClientServer.Client.Operators
{
    public class CustomSnackbarOperator : ICustomSnackbarOperator
	{
		public string BannerClass { get; set; } = "alert-info";
		public bool IsVisible { get; set; } = false;
		public string? Message { get; set; }

		public void ShowWarningMessage(string message)
		{
			BannerClass = "alert-warning";
			Message = message;
			IsVisible = true;
		}

		public void ShowErrorMessage(string message)
		{
			Message = message;
			BannerClass = "alert-danger";
			IsVisible = true;
		}

		public void ShowInformationMessage(string message)
		{
			BannerClass = "alert-info";
			Message = message;
			IsVisible = true;
		}

		public void Hide()
		{
			IsVisible = false;
		}
	}
}
