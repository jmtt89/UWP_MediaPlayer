using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Controller
{
    class VideoDataSource
    {
        private static VideoDataSource _me;
        private YouTubeService _youtubeService;
        private List<Video> _videos;

        private VideoDataSource()
        {
            _youtubeService = new YouTubeService(
                new BaseClientService.Initializer
                {
                    ApiKey = "AIzaSyA6ET_4xaBxQuqJhAH7HjhwoKkeFghMgy0",
                    ApplicationName = "My Project"
                });
            _videos = new List<Video>();
        }

        public static VideoDataSource GetInstance()
        {
            if(_me == null)
            {
                _me = new VideoDataSource();
            }
            return _me;
        }

        //Get Channel Videos
        public async Task<List<Video>> GetPopularVideos()
        {
            var request = _youtubeService.Search.List("snippet");
            request.Order = SearchResource.ListRequest.OrderEnum.Date;

            var response = await request.ExecuteAsync();
            List<Video> channelVideos = new List<Video>();

            foreach (var videoItem in response.Items)
            {
                channelVideos.Add(
                    new Video
                    {
                        Id = videoItem.Id.VideoId,
                        Title = videoItem.Snippet.Title,
                        Description = videoItem.Snippet.Description,
                        PubDate = videoItem.Snippet.PublishedAt,
                        Thumbnail = videoItem.Snippet.Thumbnails.Medium.Url,
                        YoutubeLink = "https://www.youtube.com/watch?v=" + videoItem.Id.VideoId
                    });
            }

            return channelVideos;
        }
    }
}
