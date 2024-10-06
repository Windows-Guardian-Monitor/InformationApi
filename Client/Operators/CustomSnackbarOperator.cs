using ClientServer.Client.Operators.Contracts;

namespace ClientServer.Client.Operators
{
	public class CustomSnackbarOperator : ICustomSnackbarOperator
	{
		public string BannerClass { get; set; } = "alert-info";
		public bool IsVisible { get; set; } = false;
		public string? Message { get; set; }
		public string? SubMessage { get; set; }

		public void ShowWarningMessage(string message, string subMessage = "")
		{
			BannerClass = "alert-warning";
			Message = message;
			SubMessage= subMessage;
			IsVisible = true;
		}

		public void ShowErrorMessage(string message, string subMessage = "")
		{
			Message = message;
			BannerClass = "alert-danger";
			SubMessage = subMessage;
			IsVisible = true;
		}

		public void ShowInformationMessage(string message, string subMessage = "")
		{
			BannerClass = "alert-info";
			Message = message;
			SubMessage = subMessage;
			IsVisible = true;
		}

		public void Hide()
		{
			IsVisible = false;
		}
	}
}
