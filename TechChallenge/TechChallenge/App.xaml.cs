using System;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using TechChallenge.Core.Services;
using TechChallenge.Core.ViewModels;
using TechChallenge.Navigation;
using TechChallenge.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechChallenge
{
    public partial class App : Application
    {

        private bool _initialized;

        public App()
        {
            InitializeComponent();

            var navigation = new NavigationService();
            navigation.Configure("MainPageKey", typeof(MainPage));
            navigation.Configure("SelectedComicPageKey", typeof(SelectedComicPage));

            if (!_initialized)
            {
                _initialized = true;
                Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                        //Services
                        .AddSingleton(RestService.For<IMarvelService>("https://gateway.marvel.com/v1/public"))
                        //ViewModels
                        .AddTransient<MainPageViewModel>()
                        .AddTransient<SelectedComicViewModel>()
                        .AddSingleton<IDialogService, DialogService>()
                        .AddSingleton<INavigationService>(navigation)
                        .BuildServiceProvider());
            }

            var navigationPage = GetMainPage();
            navigation.Initialize(navigationPage);
            MainPage = navigationPage;
        }

        public static NavigationPage GetMainPage()
        {
            return new NavigationPage(new MainPage());
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
