using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace TechChallenge.ViewModels.Main
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand SearchCommand => new Command(Search);

        public MainPageViewModel()
        {

        }

        public async void OnAppearing()
        {

        }

        public async void OnDisappearing()
        {

        }

        private async void Search()
        {

        }
    }
}
