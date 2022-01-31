using System;
namespace iptvplayer.Interfaces
{
    public interface IFileService
    {
        void SaveFile(string filename, string data);
        string ReadFile(string filename);
    }
}
