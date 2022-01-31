using System;
using iptvplayer.Interfaces;

namespace iptvplayer.Droid.Dependency
{
    public class AndroidFileService:IFileService
    {
        public AndroidFileService()
        {
        }

        public string ReadFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(string filename, string data)
        {
            throw new NotImplementedException();
        }
    }
}
