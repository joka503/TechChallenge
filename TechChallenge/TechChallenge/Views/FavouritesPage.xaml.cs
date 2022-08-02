using CommunityToolkit.Mvvm.DependencyInjection;
using TechChallenge.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritesPage : ContentPage
    {
        public FavouritesViewModel ViewModel => (FavouritesViewModel)BindingContext;

        public FavouritesPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<FavouritesViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearingCommand.Execute(null);
        }
    }
}