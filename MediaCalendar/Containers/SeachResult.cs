using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalendar.Containers
{
    public class Data
    {
        public List<object> aliases { get; set; }
        public string banner { get; set; }
        public string firstAired { get; set; }
        public int id { get; set; }
        public string network { get; set; }
        public string overview { get; set; }
        public string seriesName { get; set; }
        public string slug { get; set; }
        public string status { get; set; }
    }

    public class SeachResult
    {
        public List<Data> data { get; set; }
    }
}
