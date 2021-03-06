using System;
using System.Collections.Generic;
using iptvplayer.ViewModels;
using iptvplayer.Views;
using Xamarin.Forms;

namespace iptvplayer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PlayerPage), typeof(PlayerPage));
            Routing.RegisterRoute(nameof(ConfigPage), typeof(ConfigPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewPlaylistPage), typeof(NewPlaylistPage));
            Routing.RegisterRoute(nameof(ChannelsPage), typeof(ChannelsPage));
            Routing.RegisterRoute(nameof(PlaylistGroupPage), typeof(PlaylistGroupPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
