using System;
using System.Linq;

namespace iptvplayer.Models
{
    public class Channel
    {
        public int TrackNumber { get; set; }
        public string FileLocation { get; set; }
        public string Info { get; set; }

        private string name;
        public string Name
        {
            get
            {
                if (name == null)
                    name = ExtractInfo("tvg-name=");
                return name;
            }
        }

        private string id;
        public string Id
        {
            get
            {
                if (id == null)
                    id = ExtractInfo("tvg-id=");
                return id;
            }
        }

        private string logo;
        public string Logo
        {
            get
            {
                if (logo == null)
                    logo = ExtractInfo("tvg-logo=");
                return logo;
            }
        }

        private string group;
        public string Group
        {
            get
            {
                if (group == null)
                    group = ExtractInfo("group-title=");
                return group;
            }
        }

        public Channel()
        {
        }

        private string ExtractInfo(string token)
        {
            var result = string.Empty;
            if (Info != null)
            {
                var startpos = Info.IndexOf(token);
                if (startpos > -1)
                {
                    startpos += token.Length + 1;
                    var endpos = Info.IndexOf("\"", startpos);
                    if (endpos > startpos)
                    {
                        result = Info.Substring(startpos, endpos-startpos).Replace("\"", string.Empty);
                    }
                }
            }
            return result;
        }
    }
}
