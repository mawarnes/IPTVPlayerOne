using System;
using System.Diagnostics;
using System.Threading.Tasks;
using iptvplayer.Models;
using iptvplayer.Services;
using Xamarin.Forms;

namespace iptvplayer.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {

        private string text;
        private string description;
        public int Id { get; set; }

        public ItemDetailViewModel(IDataService<Playlist> dataService)
        {
            this.dataService = dataService;
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private int itemId;
        private readonly IDataService<Playlist> dataService;

        public int ItemId
        {
            get => itemId;
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await dataService.GetById(itemId);
                Id = item.Id;
                Text = item.Name;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
