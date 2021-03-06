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
                        foreach (var line in lines)
                        {
                            if (line.Contains("#EXTINF"))
                            {
                                info = line;
                            }
                            else if (!line.Contains("#EXTM3U"))
                            {
                                result.Add(new Channel
                                {
                                    Info = info,
                                    FileLocation = line,
                                });
                            }
                        }
                    }
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true,result));
                }
            }
        }

        public async Task<List<Channel>> GetM3UChannels(Playlist playlist)
        {
            string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IPTVPlayer");
            Directory.CreateDirectory(pathToNewFolder);

            try
            {
                pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(playlist.Url));
                if (!File.Exists(pathToNewFile))
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(new Uri(playlist.Url), pathToNewFile);
                }
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
                            var ch=new Channel
                            {
                                Info = info,
                                FileLocation = item,
                                PlaylistId = playlist.Id
                            };
                            try
                            {
                                if (!string.IsNullOrEmpty(ch.TvgLogo))
                                {
                                    pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(ch.TvgLogo));
                                    if (!File.Exists(pathToNewFile))
                                    {
                                        WebClient webClient = new WebClient();
                                        webClient.DownloadFileAsync(new Uri(ch.TvgLogo), pathToNewFile);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //oh oh 
                            }

                            result.Add(ch);
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
