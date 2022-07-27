using CommunityToolkit.Mvvm.DependencyInjection;
using TechChallenge.Core.ViewModels;
using Xamarin.Forms;

namespace TechChallenge.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<MainPageViewModel>();
        }
    }
}
