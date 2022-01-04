using System;
using System.Collections.Generic;
using iptvplayer.Models;

namespace iptvplayer.Events
{
    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public List<Channel> ChannelList;
        public DownloadEventArgs(bool fileSaved, List<Channel> channelList)
        {
            FileSaved = fileSaved;
            ChannelList = channelList;
        }
    }
}
