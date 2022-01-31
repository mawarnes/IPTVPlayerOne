using System.Collections.Generic;
using System.Threading.Tasks;
using iptvplayer.Models;

namespace iptvplayer.Services
{
    public interface IPlaylistService:IDataService<Playlist>
    {
        Task<Playlist> Add(string url, string name, string description);
        Task<List<Playlist>> GetPlayListGroups(int playlistId);
    }
}