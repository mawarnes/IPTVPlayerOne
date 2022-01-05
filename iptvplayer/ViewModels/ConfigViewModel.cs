using System;
namespace iptvplayer.ViewModels
{
    public class ConfigViewModel : BaseViewModel
    {
        private string feedUrl;

        public string FeedUrl
        {
            get => feedUrl;
            set => SetProperty<string>(ref feedUrl, value);
        }

        public ConfigViewModel()
        {
        }
    }
}
