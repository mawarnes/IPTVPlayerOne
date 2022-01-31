using iptvplayer.ViewModels;
using Xamarin.Forms;

namespace iptvplayer.Views
{
    public partial class PlaylistPage : ContentPage
    {
        PlaylistViewModel _viewModel;

        public PlaylistPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new PlaylistViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
