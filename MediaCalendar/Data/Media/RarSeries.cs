using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalendar.Data.Media
{
    public class RarSeries
    {
        public RarSeriesData data { get; set; }

        public Series convertToSeries()
        {
            Series series = new Series();
            string temp;

            series.id = this.data.id;
            series.seriesName = this.data.seriesName;

            if (this.data.aliases.Count > 0)
            {
                temp = this.data.aliases[0].ToString();
                for (int i = 1; i < this.data.aliases.Count - 1; i++)
                {
                    temp += ", " + this.data.aliases[i];
                }
                series.aliases = temp;
            }
            else
                series.aliases = "";
            series.banner = this.data.banner;
            series.seriesId = this.data.seriesId;
            series.status = this.data.status;

            DateTime tempDateTime = new DateTime();
            if (DateTime.TryParse(this.data.firstAired, out tempDateTime))
            {
                series.firstAired = tempDateTime;
            }
            else
            {
                series.firstAired = DateTime.Now;
            }
            //series.firstAired = DateTime.Parse(this.data.firstAired);
            series.network = this.data.network;
            series.networkId = this.data.networkId;
            series.runtime = this.data.runtime;

            if (this.data.genre.Count > 0)
            {
                temp = this.data.genre[0].ToString();
                for (int i = 1; i < this.data.genre.Count - 1; i++)
                {
                    temp += ", " + this.data.genre[i];
                }
                series.genre = temp;
            }
            else
                series.genre = "";
            series.overview = this.data.overview;
            series.lastUpdated = this.data.lastUpdated;
            series.airsDayOfWeek = this.data.airsDayOfWeek;
            series.airsTime = this.data.airsTime;
            series.rating = this.data.rating;
            series.imdbId = this.data.imdbId;
            series.zap2itId = this.data.zap2itId;
            series.added = this.data.added;
            series.siteRating = this.data.siteRating;
            series.siteRatingCount = this.data.siteRatingCount;
            series.slug = this.data.slug;

            return series;
        }
    }
    public class RarSeriesData
    {
        public int id { get; set; }
        public string seriesName { get; set; }
        public List<object> aliases { get; set; }
        public string banner { get; set; }
        public string seriesId { get; set; }
        public string status { get; set; }
        public string firstAired { get; set; }
        public string network { get; set; }
        public string networkId { get; set; }
        public string runtime { get; set; }
        public List<string> genre { get; set; }
        public string overview { get; set; }
        public int lastUpdated { get; set; }
        public string airsDayOfWeek { get; set; }
        public string airsTime { get; set; }
        public string rating { get; set; }
        public string imdbId { get; set; }
        public string zap2itId { get; set; }
        public string added { get; set; }
        public object addedBy { get; set; }
        public double siteRating { get; set; }
        public int siteRatingCount { get; set; }
        public string slug { get; set; }
    }
}
