using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using GithubClient;
using iptvplayer.Interfaces;
using iptvplayer.Models;
using iptvplayer.Services;
using iptvplayer.Views;
using Xamarin.Forms;

namespace iptvplayer.ViewModels
{
    [QueryProperty(nameof(PlaylistId), nameof(PlaylistId))]
    public class PlaylistViewModel:BaseViewModel
    {
        private readonly IPlaylistService playlistService;
        IDownloader downloader = DependencyService.Get<IDownloader>();
        private Playlist _selectedPlaylist;

        public ObservableCollection<Playlist> Playlists { get; }
        public Command LoadPlaylistsCommand { get; }
        public Command LoadPlaylistGroupsCommand { get; }
        public Command InfoButtonCommand { get; }
        public Command AddPlaylistCommand { get; }
        public Command<Playlist> PlaylistTapped { get; }

        private int playlistId;
        public int PlaylistId
        {
            get => playlistId;
            set => SetProperty(ref playlistId, value);
        }

        public PlaylistViewModel()
        {

            Title = "Playlists";
            playlistService = DependencyService.Get<IPlaylistService>();
            Playlists = new ObservableCollection<Playlist>();
            LoadPlaylistsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadPlaylistGroupsCommand = new Command(async () => await ExecuteLoadGroupsCommand());
            PlaylistTapped = new Command<Playlist>(OnPlaylistSelected);
            AddPlaylistCommand = new Command(OnAddItem);
            InfoButtonCommand = new Command<Playlist>(OnInfoButtonClicked);
        }

        async Task ExecuteLoadGroupsCommand()
        {
            IsBusy = true;

            try
            {
                Playlists.Clear();
                var playLists = await playlistService.GetPlayListGroups(playlistId);
                //var dir = await Github.getRepo("mawarnes", "iptv/contents/channels?ref=master", "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");

                foreach (var playList in playLists)
                {
                    Playlists.Add(playList);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Playlists.Clear();
                var playLists = await playlistService.Get();
                //var dir = await Github.getRepo("mawarnes", "iptv/contents/channels?ref=master", "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");

                foreach (var playList in playLists)
                {
                    Playlists.Add(playList);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPlaylist = null;
        }

        public Playlist SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                if (_selectedPlaylist != value)
                {
                    SetProperty(ref _selectedPlaylist, value);
                    OnPlaylistSelected(value);
                }
            }
        }

        private async void OnAddItem()
        {
            await Shell.Current.GoToAsync(nameof(NewPlaylistPage));
        }

        async void OnInfoButtonClicked(Playlist playlist)
        {
            await Shell.Current.GoToAsync($"{nameof(NewPlaylistPage)}?{nameof(ChannelsViewModel.PlaylistId)}={playlist.Id}");
        }

        async void OnPlaylistSelected(Playlist playlist)
        {
            if (playlist.Channels == null)
                await Shell.Current.GoToAsync($"{nameof(PlaylistGroupPage)}?{nameof(ChannelsViewModel.PlaylistId)}={playlist.Id}");
            else
            // This will push the ItemDetailPage onto the navigation stack
                await Shell.Current.GoToAsync($"{nameof(ChannelsPage)}?{nameof(ChannelsViewModel.PlaylistId)}={playlist.Id}&{nameof(ChannelsViewModel.GroupFilter)}={playlist.Name}");
        }
    }
}
