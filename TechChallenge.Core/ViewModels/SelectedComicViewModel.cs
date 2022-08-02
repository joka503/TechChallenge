using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TechChallenge.Core.Models;
using TechChallenge.Core.Utils;

namespace TechChallenge.Core.ViewModels
{
    public class SelectedComicPageViewModel : ObservableRecipient
    {

        public Comic SelectedComic { get; set; }

        public SelectedComicPageViewModel()
        {
            Messenger.Register<SelectedComicPageViewModel, SelectedComicChangedMessage>(this, (recipient, message) =>
            {
                SelectedComic = message.Value;
            });
        }
    }
}
