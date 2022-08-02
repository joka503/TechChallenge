using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using TechChallenge.Core.Config;
using TechChallenge.Core.Models;
using TechChallenge.Core.Services;

namespace TechChallenge.Core.ViewModels
{
    public class FavouritesViewModel : ObservableRecipient
    {

        #region Variables

        private readonly IDialogService _dialogService;

        private readonly INavigationService _navigationService;

        public ObservableCollection<Comic> ComicsList { get; set; }

        public Comic SelectedComic { get; set; }

        #endregion

        public FavouritesViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;

            var prefs = Xamarin.Essentials.Preferences.Get(Constants.FavoritesPref, string.Empty);
            if (!string.IsNullOrEmpty(prefs))
            {
                List<Comic>? list = JsonSerializer.Deserialize<List<Comic>>(prefs);
                if (list != null && list.Count != 0)
                {
                    ComicsList = new ObservableCollection<Comic>(list);
                }
            }
        }
    }
}
