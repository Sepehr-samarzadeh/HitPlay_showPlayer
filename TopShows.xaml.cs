using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HitPlay
{
    /// <summary>
    /// Interaction logic for TopShows.xaml
    /// </summary>
    public partial class TopShows : Window
    {
        private const string ApiUrl = "https://phish.in/api/v1/tracks";
        private readonly HandleTrackAPI handleApi2_;
        private MediaElement mediaPlayer_;
        public TopShows()
        {
            InitializeComponent();
            mediaPlayer_ = new MediaElement();
            handleApi2_ = new HandleTrackAPI();
            LoadTracks();
            mediaPlayer_.LoadedBehavior = MediaState.Manual;
            mediaPlayer_.UnloadedBehavior = MediaState.Manual;
            background_music.Children.Add(mediaPlayer_);
            all_songs.MouseDoubleClick += AllSongs_MouseDoubleClick;

        }
        private void AllSongs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = (sender as ListView);
            var selectedItem = listView?.SelectedItem;

            if (selectedItem != null)
            {
                if (selectedItem is string)
                {
                    string selectedTitle = (string)selectedItem;
                    var url = GetUrlFromTracks(selectedTitle, infoTrack.data);
                    mediaPlayer_.Source = new Uri(url);


                    mediaPlayer_.Play();
                }
            }
        }
        private string GetUrlFromTracks(string title, IList<Track> tracks)
        {
            foreach (var track in tracks)
            {
                if (track.title.Equals(title))
                {
                    return track.mp3;
                }
            }
            return string.Empty;

        }
        private InfoTrack? infoTrack = null;

        private async void LoadTracks()
        {
            string bearerToken = "ed18372e1a98b945f4224a8a7323b454edd6947160ca299806b1e9ef1148da816458a6175af98cc8e28a0399a8626bac";
            infoTrack = await handleApi2_.GetApipleaseWorkData(ApiUrl, bearerToken);
            FillTrackView(infoTrack.data);

        }

        private void FillTrackView(IList<Track> tracks)
        {
           
            foreach (var track in tracks)
            {
                all_songs.Items.Add(track.title);
            }
        }
    }
}
