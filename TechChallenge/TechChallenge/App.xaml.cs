using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using TechChallenge.Core.Config;
using TechChallenge.Core.Services;
using TechChallenge.Core.ViewModels;
using TechChallenge.Navigation;
using TechChallenge.Views;
using Xamarin.Forms;

namespace TechChallenge
{
    public partial class App : Application
    {

        private bool _initialized;

        public App()
        {
            InitializeComponent();

            var navigation = new NavigationService();
            navigation.Configure(Constants.MainPageKey, typeof(MainPage));
            navigation.Configure(Constants.SelectedComicPageKey, typeof(SelectedComicPage));
            navigation.Configure(Constants.FavouritesPageKey, typeof(FavouritesPage));

            if (!_initialized)
            {
                _initialized = true;
                Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                        //Services
                        .AddSingleton(RestService.For<IMarvelService>("https://gateway.marvel.com/v1/public"))
                        .AddSingleton<IDialogService, DialogService>()
                        .AddSingleton<INavigationService>(navigation)
                        //ViewModels
                        .AddTransient<MainPageViewModel>()
                        .AddTransient<SelectedComicPageViewModel>()
                        .AddTransient<FavouritesViewModel>()
                        .BuildServiceProvider());
            }

            var navigationPage = GetMainPage();
            navigation.Initialize(navigationPage);
            MainPage = navigationPage;
        }

        private static NavigationPage GetMainPage()
        {
            return new NavigationPage(new TabPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
