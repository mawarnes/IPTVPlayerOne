using Xamarin.Forms;
using iptvplayer.ViewModels;
using iptvplayer.Services;
using iptvplayer.Models;

namespace iptvplayer.Views
{
    public partial class ChannelsPage : ContentPage
    {
        ChannelsViewModel _viewModel;

        public ChannelsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ChannelsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
