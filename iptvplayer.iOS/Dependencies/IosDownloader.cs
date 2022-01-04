using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using iptvplayer.Events;
using iptvplayer.Interfaces;
using iptvplayer.Models;

namespace iptvplayer.iOS.Dependencies
{
    public class IosDownloader : IDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;
        string pathToNewFile;

        public void DownloadFile(string url, string folder)
        {
            string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder);
            Directory.CreateDirectory(pathToNewFolder);

            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false,null));
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false,null));
            }
            else
            {
                if (OnFileDownloaded != null)
                {
                    var result = new List<Channel>();
                    if (string.IsNullOrEmpty(pathToNewFile))
                    {
                        string[] lines = File.ReadAllLines(pathToNewFile, Encoding.UTF8);
                        string info = null;
                        foreach (var item in lines)
                        {
                            if (item.Contains("#EXTINF"))
                            {
                                info = item;
                            }
                            else if (!item.Contains("#EXTM3U"))
                            {
                                result.Add(new Channel
                                {
                                    Info = info,
                                    FileLocation = item,
                                    TrackNumber = result.Count + 1
                                });
                            }
                        }
                    }
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true,result));
                }
            }
        }

        public async Task<List<Channel>> GetM3UChannels(string url)
        {
            string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IPTVPlayer");
            Directory.CreateDirectory(pathToNewFolder);

            try
            {
                WebClient webClient = new WebClient();
                pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                webClient.DownloadFile(new Uri(url), pathToNewFile);
                var result = new List<Channel>();

                if (!string.IsNullOrEmpty(pathToNewFile))
                {
                    //okay so we want the url to be the executable for ffmpeg
                    string[] lines = await File.ReadAllLinesAsync(pathToNewFile, Encoding.UTF8);
                    string info = null;
                    foreach (var item in lines)
                    {
                        if (item.Contains("#EXTINF"))
                        {
                            info = item;
                        }
                        else if (!item.Contains("#EXTM3U"))
                        {
                            result.Add(new Channel
                            {
                                Info = info,
                                FileLocation = item,
                                TrackNumber = result.Count + 1
                            });
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false, null));
                return null;
            }
        }
    }
}
