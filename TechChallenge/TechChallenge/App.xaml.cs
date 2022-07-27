using System;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using TechChallenge.Core.Services;
using TechChallenge.Core.ViewModels;
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

            if (!_initialized)
            {
                _initialized = true;
                Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                        //Services
                        .AddSingleton(RestService.For<IMarvelService>("https://gateway.marvel.com/v1/public"))
                        //ViewModels
                        .AddTransient<MainPageViewModel>()
                        .BuildServiceProvider());
            }

            MainPage = new MainPage();
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
