using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using iptvplayer.Models;
using iptvplayer.Views;
using iptvplayer.Interfaces;

namespace iptvplayer.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        IDownloader downloader = DependencyService.Get<IDownloader>();
        private Channel _selectedItem;

        public ObservableCollection<Channel> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Channel> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "M3U Files";
            Items = new ObservableCollection<Channel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Channel>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var channels=await downloader.GetM3UChannels(@"http://192.168.0.107:34400/m3u/xteve.m3u");

                foreach (var channel in channels)
                {
                    Items.Add(channel);
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
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Channel item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PlayerPage)}?{nameof(PlayerViewModel.ChannelURL)}={item.FileLocation}");
        }
    }
}
