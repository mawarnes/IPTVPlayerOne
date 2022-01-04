using System.ComponentModel;
using Xamarin.Forms;
using iptvplayer.ViewModels;

namespace iptvplayer.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
