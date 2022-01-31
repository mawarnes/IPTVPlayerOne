using System.Collections.Generic;

namespace iptvplayer.Models
{
    public class Playlist : DbEntity
    {
        private List<Channel> channels;

        public string Name { get; set; }
        public string ChannelName {
            get {
                if (Channels == null)
                    return Name;
                else
                    return $"{Name} ({Channels.Count})";
            }
        }
        public string Url { get; set; }
        public List<Channel> Channels { get => channels; }
        public string Description { get; set; }
        public Playlist()
        {
        }
        public Playlist(List<Channel> channels)
        {
            this.channels = channels;
        }
        public void SetChannels(List<Channel> channels)
        {
            this.channels = channels;
        }
    }
}
