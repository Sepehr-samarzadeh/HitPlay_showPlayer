using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace HitPlay
{
    public class Show
    {
        public int id { get; set; }
        public string date { get; set; }
        public int duration { get; set; }
        public bool incomplete { get; set; }
        public bool sbd { get; set; }
        public bool remastered { get; set; }
        public int tour_id { get; set; }
        public string venue_name { get; set; }
        public string taper_notes { get; set; }
        public int likes_count { get; set; }
        public List<Track> tracks { get; set; }
    }
}
