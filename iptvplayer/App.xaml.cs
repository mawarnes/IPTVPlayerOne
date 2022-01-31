using Xamarin.Forms;
using iptvplayer.Services;
using iptvplayer.Models;

namespace iptvplayer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<IPlaylistService,PlaylistService>();
            DependencyService.Register<IChannelService, ChannelService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
