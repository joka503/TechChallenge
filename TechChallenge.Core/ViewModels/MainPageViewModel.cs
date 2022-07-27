using System;
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

        public IAsyncRelayCommand GetMarvelComicsCommand { get; }
        public IAsyncRelayCommand ComicSelectedCommand { get; }
        public IAsyncRelayCommand SearchTextChangedCommand { get; }

        public ObservableCollection<Comics> ComicsList { get; set; }

        public bool ShowLoadMoreButton { get; set; }

        public string Search { get; set; }

        #endregion

        public MainPageViewModel(IMarvelService marvelService)
        {
            _marvelService = marvelService;
            GetMarvelComicsCommand = new AsyncRelayCommand(DownloadMarvelComics);
            ComicSelectedCommand = new AsyncRelayCommand(ComicSelected);
            SearchTextChangedCommand = new AsyncRelayCommand(ClearList);
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
                    result.Data.Results.ForEach(t=> ComicsList.Add(t));

                    // Validar nº de linhas disponíveis para adicionar mais

                    ShowLoadMoreButton = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task ComicSelected()
        {
            Console.WriteLine("TESTE");
        }

        private async Task ClearList()
        {
            try
            {
                if (string.IsNullOrEmpty(Search))
                {
                    ComicsList = new ObservableCollection<Comics>();
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
