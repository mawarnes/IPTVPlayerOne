using System.Linq;

namespace iptvplayer.Models
{
    public class Track
    {
        public string FileLocation { get; set; }
        public string Info { get; set; }
        public string FileInfo
        {
            get
            {
                var splitinfo = Info.Split(",");
                return splitinfo.Last();
            }
        }
        public int TrackNumber { get; set; }
        public string FileName
        {
            get
            {
                var names = FileLocation.Split("/");
                return names.Last();
            }
        }

        public Track()
        {
        }
    }
}
