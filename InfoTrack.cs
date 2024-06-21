using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitPlay
{
    public class InfoTrack
    {
        public bool success { get; set; }
        public int total_entries { get; set; }
        public int total_pages { get; set; }
        public int page { get; set; }
        public List<Track> data { get; set; }
    }
}
