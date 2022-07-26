using TechChallenge.ViewModels.Main;
using Xamarin.Forms;

namespace TechChallenge.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.MainPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((MainPageViewModel)BindingContext).OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((MainPageViewModel)BindingContext).OnDisappearing();
        }
    }
}
