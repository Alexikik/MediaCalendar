using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalendar.Data.Media
{
    public class RarEpisode
    {
        public Links links { get; set; }
        public List<RarEpisodeData> data { get; set; }

        public Episode convertToEpisode(int episodeToExtract)
        {
            Episode episode = new Episode();
            string temp;

            episode.id = this.data[episodeToExtract].id;
            episode.airedSeason = this.data[episodeToExtract].airedSeason;
            episode.airedSeasonID = this.data[episodeToExtract].airedSeasonID;
            episode.airedEpisodeNumber = this.data[episodeToExtract].airedEpisodeNumber;
            episode.episodeName = this.data[episodeToExtract].episodeName;
            DateTime tempDateTime = new DateTime();
            if (DateTime.TryParse(this.data[episodeToExtract].firstAired, out tempDateTime))
            {
                episode.firstAired = tempDateTime;
            }
            else
            {
                episode.firstAired = new DateTime(0001, 1, 1);
            }

            if (this.data[episodeToExtract].guestStars.Count > 0)
            {
                temp = this.data[episodeToExtract].guestStars[0].ToString();
                for (int i = 1; i < this.data[episodeToExtract].guestStars.Count - 1; i++)
                {
                    temp += ", " + this.data[episodeToExtract].guestStars[i];
                }
                episode.guestStars = temp;
            }
            else
                episode.guestStars = "";
            episode.director = this.data[episodeToExtract].director;

            if (this.data[episodeToExtract].directors.Count > 0)
            {
                temp = this.data[episodeToExtract].directors[0].ToString();
                for (int i = 1; i < this.data[episodeToExtract].directors.Count - 1; i++)
                {
                    temp += ", " + this.data[episodeToExtract].directors[i];
                }
                episode.directors = temp;
            }
            else
                episode.directors = "";

            if (this.data[episodeToExtract].writers.Count > 0)
            {
                temp = this.data[episodeToExtract].writers[0].ToString();
                for (int i = 1; i < this.data[episodeToExtract].writers.Count - 1; i++)
                {
                    temp += ", " + this.data[episodeToExtract].writers[i];
                }
                episode.writers = temp;
            }
            else
                episode.writers = "";
            //episode.overview = this.data[0].overview;
            episode.overview = ((this.data[episodeToExtract].overview != null) ? (this.data[episodeToExtract].overview) : (""));
            episode.productionCode = this.data[episodeToExtract].productionCode;
            episode.showUrl = this.data[episodeToExtract].showUrl;
            episode.absoluteNumber = Convert.ToInt32(this.data[episodeToExtract].absoluteNumber);
            episode.filename = this.data[episodeToExtract].filename;
            episode.seriesId = this.data[episodeToExtract].seriesId;
            episode.lastUpdatedBy = this.data[episodeToExtract].lastUpdatedBy;
            //episode.thumbAuthor = this.data[0].thumbAuthor;
            episode.thumbAdded = this.data[episodeToExtract].thumbAdded;
            episode.thumbWidth = ((this.data[episodeToExtract].thumbWidth != null) ? (this.data[episodeToExtract].thumbWidth) : (""));
            episode.thumbHeight = ((this.data[episodeToExtract].thumbHeight != null) ? (this.data[episodeToExtract].thumbHeight) : (""));
            episode.imdbId = this.data[episodeToExtract].imdbId;
            //episode.siteRating = this.data[0].siteRating;
            //episode.siteRatingCount = this.data[0].siteRatingCount;


            return episode;
        }
    }
    public class Links
    {
        public int first { get; set; }
        public int last { get; set; }
        public object next { get; set; }
        public object prev { get; set; }
    }

    //public class Language
    //{
    //    public string episodeName { get; set; }
    //    public string overview { get; set; }
    //}

    public class RarEpisodeData
    {
        public int id { get; set; }
        public int airedSeason { get; set; }
        public int airedSeasonID { get; set; }
        public int airedEpisodeNumber { get; set; }
        public string episodeName { get; set; }
        public string firstAired { get; set; }
        public List<object> guestStars { get; set; }
        public string director { get; set; }
        public List<object> directors { get; set; }
        public List<object> writers { get; set; }
        public string overview { get; set; }
        //public Language language { get; set; }
        public string productionCode { get; set; }
        public string showUrl { get; set; }
        public int lastUpdated { get; set; }
        public string dvdDiscid { get; set; }
        //public int dvdSeason { get; set; }
        public string dvdSeason { get; set; }
        //public int dvdEpisodeNumber { get; set; }
        public object dvdChapter { get; set; }
        //public int absoluteNumber { get; set; }
        public string absoluteNumber { get; set; }
        public string filename { get; set; }
        public int seriesId { get; set; }
        public int lastUpdatedBy { get; set; }
        public object airsAfterSeason { get; set; }
        public object airsBeforeSeason { get; set; }
        public object airsBeforeEpisode { get; set; }
        //public int thumbAuthor { get; set; }
        public string thumbAdded { get; set; }
        public string thumbWidth { get; set; }
        public string thumbHeight { get; set; }
        public string imdbId { get; set; }
        public double siteRating { get; set; }
        public int siteRatingCount { get; set; }
    }
}
