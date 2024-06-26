﻿using System;
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
        private int currentTrackIndex = -1;
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
                    currentTrackIndex = all_songs.Items.IndexOf(selectedItem);
                    PlayTrack(selectedTitle);

                }
            }
        }

        private void PlayTrack(string title)
        {
            var url = GetUrlFromTracks(title, infoTrack.data);
            mediaPlayer_.Source = new Uri(url);
            mediaPlayer_.Play();
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
            string bearerToken = "APIKEY";
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

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrackIndex > 0)
            {
                currentTrackIndex--;
                PlayTrack((string)all_songs.Items[currentTrackIndex]);
                all_songs.SelectedIndex = currentTrackIndex;
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer_.Stop();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrackIndex < all_songs.Items.Count - 1)
            {
                currentTrackIndex++;
                PlayTrack((string)all_songs.Items[currentTrackIndex]);
                all_songs.SelectedIndex = currentTrackIndex;
            }
        }

    }
}
