using Acr.UserDialogs;

namespace TechChallenge.Core.Services
{
    public class DialogService : IDialogService
    {
        public void ShowAlert(string message, string title = null, string okText = null)
        {
            UserDialogs.Instance.Alert(message, title, okText);
        }
    }
}
