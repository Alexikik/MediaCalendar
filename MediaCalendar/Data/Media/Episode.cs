using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalendar.Data.Media
{
    public class Episode
    {
        #region info
        public int Key { get; set; }
        public int id { get; set; }
        public string SeriesName { get; set; }
        public int airedSeason { get; set; }
        public int airedSeasonID { get; set; }
        public int airedEpisodeNumber { get; set; }
        public string episodeName { get; set; }
        public DateTime firstAired { get; set; }
        public string guestStars { get; set; }          // str
        //public List<object> guestStars { get; set; }
        public string director { get; set; }
        public string directors { get; set; }           // str
        //public List<object> directors { get; set; }
        public string writers { get; set; }             // str
        //public List<object> writers { get; set; }
        public string overview { get; set; }
        public string productionCode { get; set; }
        public string showUrl { get; set; }
        public int lastUpdated { get; set; }
        //public string dvdDiscid { get; set; }
        //public int dvdSeason { get; set; }
        //public int dvdEpisodeNumber { get; set; }
        //public object dvdChapter { get; set; }
        public int absoluteNumber { get; set; }
        public string filename { get; set; }
        public int seriesId { get; set; }
        public int lastUpdatedBy { get; set; }
        //public object airsAfterSeason { get; set; }
        //public object airsBeforeSeason { get; set; }
        //public object airsBeforeEpisode { get; set; }
        //public int thumbAuthor { get; set; }
        public string thumbAdded { get; set; }
        public string thumbWidth { get; set; }
        public string thumbHeight { get; set; }
        public string imdbId { get; set; }
        public double siteRating { get; set; }
        public int siteRatingCount { get; set; }
        #endregion info
    }
}
