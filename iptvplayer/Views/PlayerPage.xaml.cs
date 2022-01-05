using iptvplayer.ViewModels;
using Xamarin.Forms;

namespace iptvplayer.Views
{
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage()
        {
            InitializeComponent();
            BindingContext = new PlayerViewModel();
        }

        void ContentPage_Disappearing(System.Object sender, System.EventArgs e)
        {
            ((PlayerViewModel)BindingContext).OnDisAppearing();
        }
    }
}
