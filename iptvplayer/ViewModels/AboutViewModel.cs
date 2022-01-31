using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace iptvplayer.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.warnes.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
