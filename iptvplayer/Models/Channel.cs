using System;
using System.Linq;

namespace iptvplayer.Models
{
    public class Channel
    {
        public string FileLocation { get; set; }
        public string Info { get; set; }
        public string Name
        {
            get
            {
                var splitinfo = Info.Split(",");
                return splitinfo.Last();
            }
        }
        public int TrackNumber { get; set; }
        public string ChannelURI
        {
            get
            {
                var names = FileLocation.Split("/");
                return names.Last();
            }
        }
        public Channel()
        {
        }
    }
}
