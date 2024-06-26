using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HitPlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ApiUrl = "https://phish.in/api/v1/random-show";
        private readonly HandleShowAPI _handleApi;
        private readonly HandleTrackAPI _handleApi2;
        private MediaElement _mediaPlayer;



        public MainWindow()
        {
            InitializeComponent();
            _handleApi = new HandleShowAPI();
            _handleApi2 = new HandleTrackAPI();
            _mediaPlayer = new MediaElement();
            _mediaPlayer.LoadedBehavior = MediaState.Manual;
            _mediaPlayer.UnloadedBehavior = MediaState.Manual;
            LayoutRoot.Children.Add(_mediaPlayer);

        }
        private async void FetchRandomShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string bearerToken = "ed18372e1a98b945f4224a8a7323b454edd6947160ca299806b1e9ef1148da816458a6175af98cc8e28a0399a8626bac";
                var showData = await _handleApi.GetApiDataAsync(ApiUrl, bearerToken);
                if (showData != null && showData.Data != null && showData.Data.Musics != null)
                {
                    _mediaPlayer.Source = new Uri(showData.Data.Musics[0].Mp3);
                    _mediaPlayer.Play();
                    SongNameTextBox.Text = showData.Data.Musics[0].Title;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching show data: {ex.Message}");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var view = new TopShows();
            view.Closed += View_Closed;
            view.Show();

            
            btntop20.IsEnabled = false;

        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.Stop();
            SongNameTextBox.Text = "Stopped";
        }
        private void View_Closed(object? sender, EventArgs e)
        {
            btntop20.IsEnabled = true;
        }
        private async Task<TrackResponse> FetchRandomShow()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ApiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TrackResponse>(responseBody);
            }
        }
    }
}