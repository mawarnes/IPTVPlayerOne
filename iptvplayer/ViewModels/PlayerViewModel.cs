using System;
using LibVLCSharp.Shared;
using Xamarin.Forms;

namespace iptvplayer.ViewModels
{
    [QueryProperty(nameof(ChannelURL), nameof(ChannelURL))]
    public class PlayerViewModel:BaseViewModel
    {

        private string channelURL;
        public string ChannelURL
        {
            get => channelURL;
            set
            {
                channelURL = value;
                LoadChannel(value);
            }
        }

        private void LoadChannel(string value)
        {
            Core.Initialize();

            LibVLC = new LibVLC();

            //see https://github.com/iptv-org/iptv/tree/master/channels for more m3u resources

            //var media = new Media(LibVLC, "http://192.168.0.107:34400/stream/fae990310f47705a931f39617661e451",FromType.FromLocation);
            var media = new Media(LibVLC, new Uri(ChannelURL));

            MediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };

            //media.Dispose(); need to remove this for streaming to work

            MediaPlayer.Play(media);
        }

        internal void OnDisAppearing()
        {
            MediaPlayer.Stop();
        }

        private LibVLC _libVLC;
        /// <summary>
        /// Gets the <see cref="LibVLCSharp.Shared.LibVLC"/> instance.
        /// </summary>
        public LibVLC LibVLC
        {
            get => _libVLC;
            private set => SetProperty<LibVLC>(ref _libVLC, value);
        }

        private MediaPlayer _mediaPlayer;
        /// <summary>
        /// Gets the <see cref="LibVLCSharp.Shared.MediaPlayer"/> instance.
        /// </summary>
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => SetProperty<MediaPlayer>(ref _mediaPlayer, value);
        }

        public PlayerViewModel()
        {
        }
    }
}
