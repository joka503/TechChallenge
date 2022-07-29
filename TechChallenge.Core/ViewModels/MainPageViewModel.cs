﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TechChallenge.Core.Config;
using TechChallenge.Core.Models;
using TechChallenge.Core.Services;
using TechChallenge.Core.Utils;

namespace TechChallenge.Core.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {

        #region Variables

        private readonly IMarvelService _marvelService;

        private readonly IDialogService _dialogService;

        private readonly INavigationService _navigationService;

        public IAsyncRelayCommand GetMarvelComicsCommand { get; }
        public IAsyncRelayCommand ComicSelectedCommand { get; }
        public IRelayCommand SearchTextChangedCommand { get; }

        public ObservableCollection<Comics> ComicsList { get; set; }

        public bool ShowLoadMoreButton { get; set; }

        public string Search { get; set; }

        public Comics SelectedComic { get; set; }

        #endregion

        public MainPageViewModel(IMarvelService marvelService, IDialogService dialogService, INavigationService navigationService)
        {
            _marvelService = marvelService;
            _dialogService = dialogService;
            _navigationService = navigationService;
            GetMarvelComicsCommand = new AsyncRelayCommand(DownloadMarvelComics);
            ComicSelectedCommand = new AsyncRelayCommand(ComicSelected);
            SearchTextChangedCommand = new RelayCommand(ClearList);
            ComicsList = new ObservableCollection<Comics>();
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
                    //OrderBy = "focDate",
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

        private async Task ComicSelected()
        {
            _navigationService.NavigateTo("SelectedComicPageKey", SelectedComic);
            Console.WriteLine(SelectedComic.Title);
        }

        private void ClearList()
        {
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    ComicsList.Clear();
                    ShowLoadMoreButton = false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
