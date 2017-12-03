using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaPlayer
{
    public sealed partial class VideoDetailItem : UserControl
    {
        public Model.Video Video { get { return this.DataContext as Model.Video; } }
        public VideoDetailItem()
        {
            this.InitializeComponent();
            if (player!=null && player.CurrentState.ToString() == "Playing")
            {
                player.Stop();
            }
            this.DataContextChanged += async (s, e) => {
                //Get The Video Uri and set it as a player source
                var Item = this.DataContext as Model.Video;
                if(Item != null && !Item.Id.Equals(String.Empty))
                {
                    try
                    {
                        var url = await YouTube.GetVideoUriAsync(Item.Id, YouTubeQuality.Quality480P);
                        player.Source = url.Uri; this.Bindings.Update();
                    }
                    catch (YouTubeUriNotFoundException _e)
                    {
                        Debug.WriteLine(_e.Message);
                        MessageDialog message = new MessageDialog("This video is not availible, please select other");
                        await message.ShowAsync();
                    }

                }
            };
        }
    }
}
