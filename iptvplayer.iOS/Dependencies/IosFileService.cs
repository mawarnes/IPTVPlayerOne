using System;
using iptvplayer.Interfaces;

namespace iptvplayer.iOS.Dependencies
{
    public class IosFileService:IFileService
    {
        public IosFileService()
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
