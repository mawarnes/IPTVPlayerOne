using System.Collections.Generic;
using System.Threading.Tasks;
using iptvplayer.Models;

namespace iptvplayer.Services
{
    public interface IChannelService:IDataService<Channel>
    {
        Task<List<Channel>> GetByPlaylistId(int playlistid);
        Task<List<Channel>> GetByPlaylistIdAndGroupName(int playlistid, string groupName);
        Task<List<Channel>> DeleteByPlaylistId(int playlistid);
        Task RefreshChannels(int playlistId);
    }
}