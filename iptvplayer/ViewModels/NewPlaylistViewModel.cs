using System;
using iptvplayer.Interfaces;
using iptvplayer.Models;
using iptvplayer.Services;
using Xamarin.Forms;

namespace iptvplayer.ViewModels
{
    [QueryProperty(nameof(PlaylistId), nameof(PlaylistId))]
    public class NewPlaylistViewModel : BaseViewModel
    {
        private string name;
        private string url;
        private string description;
        private readonly IPlaylistService playlistService = DependencyService.Get<IPlaylistService>();
        private readonly IChannelService channelService = DependencyService.Get<IChannelService>();
        private readonly IDownloader downloader = DependencyService.Get<IDownloader>();
        Playlist Playlist;

        public NewPlaylistViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }


        private int? playlistId;
        public int PlaylistId
        {
            get => playlistId.HasValue?playlistId.Value:0;
            set => SetProperty(ref playlistId, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(url);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Url
        {
            get => url;
            set => SetProperty(ref url, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async void OnAppearing()
        {
            if (PlaylistId > 0)
            {
                Playlist = await playlistService.GetById(PlaylistId);
                Name = Playlist.Name;
                Description = Playlist.Description;
                Url = Playlist.Url;
            }
        }

        private async void OnSave()
        {
            Playlist newPlaylist = Playlist;
            if (newPlaylist != null)
            {
                newPlaylist.Name = Name;
                newPlaylist.Url = Url;
                newPlaylist.Description = Description;
                await playlistService.Update(newPlaylist);
            }
            else
            {
                PlaylistId = await playlistService.Add(new Playlist()
                {
                    Name = Name,
                    Url = Url,
                    Description = Description
                });
                newPlaylist = await playlistService.GetById(PlaylistId);
            }

            var channels = await downloader.GetM3UChannels(newPlaylist);
            await channelService.AddAll(channels);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
