using CommunityToolkit.Mvvm.DependencyInjection;
using TechChallenge.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedComicPage : ContentPage
    {
        public SelectedComicPageViewModel ViewModel => (SelectedComicPageViewModel)BindingContext;

        public SelectedComicPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<SelectedComicPageViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearingCommand.Execute(null);
        }
    }
}