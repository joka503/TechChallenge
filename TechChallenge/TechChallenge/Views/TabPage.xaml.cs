using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : TabbedPage
    {
        public TabPage()
        {
            InitializeComponent();
        }
    }
}