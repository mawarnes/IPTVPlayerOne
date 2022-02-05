using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iptvplayer.Interfaces;
using iptvplayer.Models;
using Xamarin.Forms;

namespace iptvplayer.Services
{
    public class PlaylistService : BaseDataService<Playlist>, IPlaylistService
    {
        private readonly IChannelService channelService = DependencyService.Get<IChannelService>();
        public PlaylistService() { }

        protected override async Task InitTable()
        {
            //removed as per apples comments
            //var defaultData = await db.FindWithQueryAsync<Playlist>($"select * from Playlist where Name=?", "SampleIPTV");
            //if (defaultData == null)
            //{
            //    await Add(new Playlist
            //    {
            //        Name = "SampleIPTV",
            //        Description = "Sample IPTV Channels",
            //        Url = @"https://raw.githubusercontent.com/mawarnes/iptv/master/channels/uk_samsung.m3u"
            //    });
            //}
        }

        public async Task<Playlist> Add(string url, string name, string description)
        {
            await Init();
            var playlist = new Playlist
            {
                Name = name,
                Url = url,
                Description = description
            };
            playlist.Id = await Add(playlist);
            playlist.SetChannels(await channelService.GetByPlaylistId(playlist.Id));
            return playlist;
        }

        public async Task<List<Playlist>> GetPlayListGroups(int playlistId)
        {
            List<Playlist> result = new List<Playlist>();

            var channels = await channelService.GetByPlaylistId(playlistId);
            foreach (var groupName in channels.Select(c => c.GroupName).Distinct())
            {
                var playlist = new Playlist
                {
                    Id = playlistId,
                    Description = string.IsNullOrEmpty(groupName) ? "Other" : groupName,
                    Name = string.IsNullOrEmpty(groupName) ? "Other" : groupName
                };
                playlist.SetChannels(channels.Where(c => c.GroupName == groupName).ToList());
                result.Add(playlist);
            }

            return result.OrderBy(r => r.Name).ToList();
        }
    }
}
