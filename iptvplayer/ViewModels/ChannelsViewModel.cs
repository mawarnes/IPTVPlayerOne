using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using iptvplayer.Models;
using iptvplayer.Views;
using iptvplayer.Interfaces;
using iptvplayer.Services;
using System.Collections.Generic;

namespace iptvplayer.ViewModels
{
    [QueryProperty(nameof(PlaylistId), nameof(PlaylistId))]
    [QueryProperty(nameof(GroupFilter), nameof(GroupFilter))]
    public class ChannelsViewModel : BaseViewModel
    {
        private readonly IDownloader downloader = DependencyService.Get<IDownloader>();
        private Channel _selectedItem;
        private Playlist playlist;
        private readonly IPlaylistService playlistService = DependencyService.Get<IPlaylistService>();
        private readonly IChannelService channelService = DependencyService.Get<IChannelService>();

        public ObservableCollection<Channel> Channels { get; }
        public Command LoadItemsCommand { get; }
        public Command ReloadChannelsCommand { get; }
        public Command<Channel> ItemTapped { get; }

        private int playlistId;
        public int PlaylistId
        {
            get => playlistId;
            set => SetProperty(ref playlistId, value);
        }
        private string groupFilter;
        public string GroupFilter
        {
            get => groupFilter;
            set => SetProperty(ref groupFilter, value);
        }
        public ChannelsViewModel()
        {
            Title = "Channels";
            //this.PropertyChanged += LoadChannels;

            Channels = new ObservableCollection<Channel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Channel>(OnItemSelected);
            ReloadChannelsCommand = new Command(async () => await ReLoadChannels());

        }

        private async Task ReLoadChannels()
        {
            try
            {
                Channels.Clear();
                var channels= GroupFilter != null?
                    await channelService.GetByPlaylistIdAndGroupName(playlistId,GroupFilter):
                    await channelService.GetByPlaylistId(playlistId);
                foreach (var channel in channels)
                {
                    Channels.Add(channel);
                }
            }
            catch (Exception) { }
        }

        //private async void LoadChannels(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(playlistId))
        //    {
        //        try
        //        {
        //            Channels.Clear();
        //            var channels = await channelService.GetByPlaylistId(playlistId);
        //            foreach (var channel in channels)
        //            {
        //                Channels.Add(channel);
        //            }
        //        }
        //        catch (Exception) { }
        //    }
        //}

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Channels.Clear();
                var channels = GroupFilter != null ?
                    await channelService.GetByPlaylistIdAndGroupName(playlistId, GroupFilter) :
                    await channelService.GetByPlaylistId(playlistId);

                foreach (var channel in channels)
                {
                    Channels.Add(channel);
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
            SelectedItem = null;
        }

        public Channel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewPlaylistPage));
        }

        async void OnItemSelected(Channel item)
        {
            if (item == null)
                return;

            // This will push the PlayerPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PlayerPage)}?{nameof(PlayerViewModel.ChannelURL)}={item.FileLocation}&{nameof(PlayerViewModel.Title)}={item.Description}");
        }
    }
}
