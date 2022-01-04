using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iptvplayer.Events;
using iptvplayer.Models;

namespace iptvplayer.Interfaces
{
    public interface IDownloader
    {
        void DownloadFile(string url, string folder);
        Task<List<Channel>> GetM3UChannels(string url);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }
}
