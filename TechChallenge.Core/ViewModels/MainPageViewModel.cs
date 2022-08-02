using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TechChallenge.Core.Config;
using TechChallenge.Core.Models;
using TechChallenge.Core.Services;
using TechChallenge.Core.Utils;

namespace TechChallenge.Core.ViewModels
{
    public class MainPageViewModel : ObservableRecipient
    {

        #region Variables

        private readonly IMarvelService _marvelService;

        private readonly IDialogService _dialogService;

        private readonly INavigationService _navigationService;

        public IAsyncRelayCommand GetMarvelComicsCommand { get; }
        public IRelayCommand ComicSelectedCommand { get; }
        public IRelayCommand SearchTextChangedCommand { get; }

        public ObservableCollection<Comic> ComicsList { get; set; }

        public bool ShowLoadMoreButton { get; set; }

        public string Search { get; set; }

        public Comic SelectedComic { get; set; }

        #endregion

        public MainPageViewModel(IMarvelService marvelService, IDialogService dialogService, INavigationService navigationService)
        {
            _marvelService = marvelService;
            _dialogService = dialogService;
            _navigationService = navigationService;
            GetMarvelComicsCommand = new AsyncRelayCommand(DownloadMarvelComics);
            ComicSelectedCommand = new RelayCommand(ComicSelected);
            SearchTextChangedCommand = new RelayCommand(ClearList);
            ComicsList = new ObservableCollection<Comic>();
        }

        private async Task DownloadMarvelComics()
        {
            string date = DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
            string hash = Md5Generator.CreateMD5(date + Constants.PrivateKey + Constants.PublicKey);

            try
            {
                var result = await _marvelService.GetMarvelComicsAsync(new QueryParams()
                {
                    ApiKey = Constants.PublicKey,
                    TimeStamp = date,
                    Hash = hash,
                    Limit = 20,
                    Offset = ComicsList.Count,
                    Title = Search
                });

                if (result.Data.Count != 0)
                {
                    result.Data.Results?.ForEach(t => ComicsList.Add(t));

                    if (result.Data.Offset + result.Data.Count != result.Data.Total)
                    {
                        ShowLoadMoreButton = true;
                    }
                    else
                    {
                        ShowLoadMoreButton = false;
                    }
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowAlert("Whoops! Something went wrong.");
            }
        }

        private void ComicSelected()
        {
            _navigationService.NavigateTo(Constants.SelectedComicPageKey);
            Messenger.Send(new SelectedComicChangedMessage(SelectedComic));
        }

        private void ClearList()
        {
            if (string.IsNullOrEmpty(Search))
            {
                ComicsList.Clear();
                ShowLoadMoreButton = false;
            }
        }
    }
}
