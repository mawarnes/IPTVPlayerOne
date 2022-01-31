using System.Collections.Generic;
using System.Threading.Tasks;
using iptvplayer.Interfaces;
using iptvplayer.Models;
using Xamarin.Forms;

namespace iptvplayer.Services
{
    public class ChannelService : BaseDataService<Channel>, IChannelService
    {
        private readonly IDownloader downloader = DependencyService.Get<IDownloader>();
        public ChannelService()
        {
        }

        public async Task<List<Channel>> DeleteByPlaylistId(int playlistid)
        {
            await Init();
            return await db.QueryAsync<Channel>($"delete from Channel where PlaylistId=?", playlistid);
        }

        public async Task<List<Channel>> GetByPlaylistId(int playlistid)
        {
            await Init();
            var response = await db.QueryAsync<Channel>("select * from Channel where PlaylistId=?",playlistid);
            if (response.Count == 0)
            {
                await RefreshChannels(playlistid);
                response = await db.QueryAsync<Channel>("select * from Channel where PlaylistId=?", playlistid);
            }
            return response;
        }

        public async Task<List<Channel>> GetByPlaylistIdAndGroupName(int playlistid,string groupName)
        {
            await Init();
            if (groupName == "Other") groupName = string.Empty;
            var response = await db.QueryAsync<Channel>("select * from Channel where PlayListId=? and GroupName=?",playlistid, groupName);
            return response;
        }

        public async Task RefreshChannels(int playlistId)
        {
            var playlist = await db.Table<Playlist>().FirstOrDefaultAsync(p => p.Id == playlistId); 
            if (playlist == null)
                return;
            await DeleteByPlaylistId(playlistId);
            var channels = await downloader.GetM3UChannels(playlist);
            _ = await AddAll(channels);
        }
    }
}
