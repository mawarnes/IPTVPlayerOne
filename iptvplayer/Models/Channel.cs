using System;
using System.Linq;
using SQLite;

namespace iptvplayer.Models
{
    public class Channel:DbEntity
    {
        public string FileLocation { get; set; }
        public string Info { get; set; }

        private string tvgName;
        public string TvgName
        {
            get
            {
                if (tvgName == null)
                    tvgName = ExtractInfo("tvg-name=");
                return tvgName;
            }
            set { tvgName = value; }
        }

        private string description;
        public string Description
        {
            get
            {
                if (description == null)
                    description = Info.Split(',').LastOrDefault();
                return string.IsNullOrEmpty(TvgName) ? description : TvgName;
            }
            set { description = value; }
        }

        private string tvgId;
        public string TvgId
        {
            get
            {
                if (tvgId == null)
                    tvgId = ExtractInfo("tvg-id=");
                return tvgId;
            }
        }

        private string tvgLogo;
        public string TvgLogo
        {
            get
            {
                if (tvgLogo == null)
                    tvgLogo = ExtractInfo("tvg-logo=");
                return tvgLogo;
            }
            set { tvgLogo = value; }
        }

        public string LogoFileName => TvgLogo.Split('/').Last();

        private string groupName;
        public string GroupName
        {
            get
            {
                if (groupName == null)
                    groupName = ExtractInfo("group-title=");
                return groupName;
            }
            set {
                groupName = value; }
        }

        [Indexed]
        public int PlaylistId { get; set; }

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
