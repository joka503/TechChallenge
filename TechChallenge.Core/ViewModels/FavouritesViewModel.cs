using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TechChallenge.Core.Config;
using TechChallenge.Core.Models;
using TechChallenge.Core.Services;
using TechChallenge.Core.Utils;

namespace TechChallenge.Core.ViewModels
{
    public class FavouritesViewModel : ObservableRecipient
    {

        #region Variables

        private readonly IDialogService _dialogService;

        private readonly INavigationService _navigationService;

        private readonly IPreferencesService _preferencesService;

        public IRelayCommand OnAppearingCommand { get; }

        public IRelayCommand ComicSelectedCommand { get; }

        public ObservableCollection<Comic> ComicsList { get; set; }

        public Comic SelectedComic { get; set; }

        #endregion

        public FavouritesViewModel(IDialogService dialogService, INavigationService navigationService, IPreferencesService preferencesService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            OnAppearingCommand = new RelayCommand(OnAppearing);
            ComicSelectedCommand = new RelayCommand(ComicSelected);
            _preferencesService = preferencesService;
        }

        private void OnAppearing()
        {
            try
            {
                var prefs = _preferencesService.GetPreference(Constants.FavoritesPref);

                if (!string.IsNullOrEmpty(prefs))
                {
                    List<Comic>? list = JsonSerializer.Deserialize<List<Comic>>(prefs);
                    if (list != null && list.Count != 0)
                    {
                        ComicsList = new ObservableCollection<Comic>(list);
                    }
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowAlert("Error loading favourites.");
            }
        }

        private void ComicSelected()
        {
            _navigationService.NavigateTo(Constants.SelectedComicPageKey);
            Messenger.Send(new SelectedComicChangedMessage(SelectedComic));
        }
    }
}
