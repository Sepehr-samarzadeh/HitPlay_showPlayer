using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitPlay
{
    public class Track
    {
        public int id { get; set; }
        public int show_id { get; set; }
        public string show_date { get; set; }
        public string venue_name { get; set; }
        public string venue_location { get; set; }
        public string title { get; set; }
        public int position { get; set; }
        public int duration { get; set; }
        public object jam_starts_at_second { get; set; }
        public string set { get; set; }
        public string set_name { get; set; }
        public int likes_count { get; set; }
        public string slug { get; set; }
        public string mp3 { get; set; }
        public string waveform_image { get; set; }
    }
}
