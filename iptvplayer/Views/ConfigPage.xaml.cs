using iptvplayer.ViewModels;
using Xamarin.Forms;

namespace iptvplayer.Views
{
    public partial class ConfigPage : ContentPage
    {
        ConfigViewModel _viewModel;

        public ConfigPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ConfigViewModel();
        }
    }
}
