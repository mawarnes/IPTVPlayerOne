using Xamarin.Forms;
using iptvplayer.ViewModels;
using iptvplayer.Services;
using iptvplayer.Models;

namespace iptvplayer.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel(DependencyService.Get<IDataService<Playlist>>());
        }
    }
}
