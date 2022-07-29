using CommunityToolkit.Mvvm.DependencyInjection;
using TechChallenge.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedComicPage : ContentPage
    {
        public SelectedComicPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<SelectedComicViewModel>();
        }
    }
}