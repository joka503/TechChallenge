using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SelectedComicPageViewModel : ObservableRecipient
    {

        #region Variables

        public Comic SelectedComic { get; set; }

        public IRelayCommand FavoritesCommand { get; }
        public IRelayCommand OnAppearingCommand { get; }

        private readonly IDialogService _dialogService;

        private readonly IPreferencesService _preferencesService;

        public string ButtonText { get; set; }

        #endregion

        public SelectedComicPageViewModel(IDialogService dialogService, IPreferencesService preferencesService)
        {
            _dialogService = dialogService;
            _preferencesService = preferencesService;

            Messenger.Register<SelectedComicPageViewModel, SelectedComicChangedMessage>(this, (recipient, message) =>
            {
                SelectedComic = message.Value;
            });
            OnAppearingCommand = new RelayCommand(OnAppearing, () => SelectedComic != null);
            FavoritesCommand = new RelayCommand(CheckFavorites);
        }

        private void OnAppearing()
        {
            var prefs = _preferencesService.GetPreference(Constants.FavoritesPref);

            if (string.IsNullOrEmpty(prefs))
            {
                ButtonText = "Add to Favourites";
            }
            else
            {
                List<Comic>? list = JsonSerializer.Deserialize<List<Comic>>(prefs);

                var item = list?.FirstOrDefault(t => t.Id == SelectedComic.Id);
                ButtonText = item != null ? "Remove from Favourites" : "Add to Favourites";
            }
        }

        private void CheckFavorites()
        {
            try
            {
                var prefs = _preferencesService.GetPreference(Constants.FavoritesPref);

                if (string.IsNullOrEmpty(prefs))
                {
                    List<Comic> comicsList = new List<Comic>();
                    comicsList.Add(SelectedComic);

                    string values = JsonSerializer.Serialize(comicsList);
                    _preferencesService.SetPreference(Constants.FavoritesPref, values);

                    _dialogService.ShowAlert("Comic added to favorites!");
                    ButtonText = "Remove from Favourites";
                }
                else
                {
                    List<Comic>? list = JsonSerializer.Deserialize<List<Comic>>(prefs);
                    if (list != null && list.Count != 0)
                    {
                        var item = list.FirstOrDefault(t => t.Id == SelectedComic.Id);
                        if (item != null)
                        {
                            list.Remove(item);
                            _dialogService.ShowAlert("Comic removed from favorites!");
                            ButtonText = "Add to Favourites";
                        }
                        else
                        {
                            list.Add(SelectedComic);
                            _dialogService.ShowAlert("Comic added to favorites!");
                            ButtonText = "Remove from Favourites";
                        }

                        if (list.Count == 0)
                        {
                            _preferencesService.SetPreference(Constants.FavoritesPref, string.Empty);
                        }
                        else
                        {
                            string values = JsonSerializer.Serialize(list);
                            _preferencesService.SetPreference(Constants.FavoritesPref, values);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _dialogService.ShowAlert("Error executing action.");
            }
        }
    }
}
