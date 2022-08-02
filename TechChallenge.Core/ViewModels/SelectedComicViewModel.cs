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

        private readonly IDialogService _dialogService;

        #endregion

        public SelectedComicPageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Messenger.Register<SelectedComicPageViewModel, SelectedComicChangedMessage>(this, (recipient, message) =>
            {
                SelectedComic = message.Value;
            });

            FavoritesCommand = new RelayCommand(CheckFavorites);
        }

        private void CheckFavorites()
        {
            try
            {
                var prefs = Xamarin.Essentials.Preferences.Get(Constants.FavoritesPref, string.Empty);

                if (string.IsNullOrEmpty(prefs))
                {
                    List<Comic> comicsList = new List<Comic>();
                    comicsList.Add(SelectedComic);

                    string values = JsonSerializer.Serialize(comicsList);
                    Xamarin.Essentials.Preferences.Set(Constants.FavoritesPref, values);

                    _dialogService.ShowAlert("Comic added to favorites!");
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
                        }
                        else
                        {
                            list.Add(SelectedComic);
                            _dialogService.ShowAlert("Comic added to favorites!");
                        }

                        if (list.Count == 0)
                        {
                            Xamarin.Essentials.Preferences.Set(Constants.FavoritesPref, string.Empty);

                        }
                        else
                        {
                            string values = JsonSerializer.Serialize(list);
                            Xamarin.Essentials.Preferences.Set(Constants.FavoritesPref, values);
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
