using Xamarin.Forms;

using iptvplayer.Models;
using iptvplayer.ViewModels;
using iptvplayer.Services;

namespace iptvplayer.Views
{
    public partial class NewPlaylistPage : ContentPage
    {
        NewPlaylistViewModel _viewModel;
        public Playlist Item { get; set; }

        public NewPlaylistPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewPlaylistViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
