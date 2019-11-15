using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCalendar.Containers;
using MediaCalendar.Data.Media;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace MediaCalendar.Data
{
    public class SeriesLibary
    {
        //[Inject]
        //protected GetImdbSeries GetImdbSeries { set; get; }
        Database database;
        GetImdbSeries getImdbSeries;
        public SeriesLibary(Database database, GetImdbSeries GetImdbSeries)
        {
            this.database = database;
            this.getImdbSeries = GetImdbSeries;
        }

        public async Task<ResultContainer> AddSeries(string seriesName, Database database)
        {
            Series series;
            ResultContainer result = new ResultContainer();
            //GetImdbSeries getter = new GetImdbSeries();
            GetImdbSeries getter = getImdbSeries;
            int seriesId;

            // Searches API for seriesID
            seriesId = await getImdbSeries.SearchForSeries(seriesName);
            if (seriesId == -1)
            {
                result.result = false;
                result.errorMessage = "Series not found";
                return result;
            }

            // Check if it's already added
            if (CheckIfSeriesIsInDB(seriesId))
            {
                result.result = false;
                result.errorMessage = "Already added";
                return result;
            }

            // Gets series
            series = await getter.GetSeries(seriesId);

            if (series.seriesName != null)
            {
                result = await addAllSeriesEpisodes(series);
            }
            else
            {
                // Creates error message
                result.result = false;
                result.errorMessage = "Series id not found";
            }

            return result;
        }

        private async Task<ResultContainer> addAllSeriesEpisodes(Series series)
        {
            ResultContainer result = new ResultContainer();

            // Adds series to database
            List<Episode> episodes = await AddSeason(series.id, database);
            series.episodes = episodes;

            database.Add(series);
            foreach (Episode episode in episodes)
            {
                episode.SeriesName = series.seriesName;
                if (episode.firstAired != new DateTime(0001, 1, 1))
                {
                    database.Add(episode);
                }
            }
            database.SaveChanges();

            result.result = true;
            return result;
        }

        private async Task<List<Episode>> AddSeason(int seriesId, Database database)
        {
            List<Episode> episodes;
            ResultContainer result = new ResultContainer();

            episodes = await getImdbSeries.getEpisodeList(seriesId);

            if (episodes.Count == 0)
            {
                result.result = false;
                result.errorMessage = "No episodes found [Database.cs]";
                //return result;
            }

            //result.result = true;
            return episodes;
        }

        private bool CheckIfSeriesIsInDB(int seriesId)
        {
            bool isInDb;

            isInDb = database.SeriesLibary.Any(s => s.id == seriesId);

            return isInDb;
        }

        public async Task<ResultContainer> DownloadAllEpisodesForAllSeries()
        {
            foreach (Series series in database.SeriesLibary)
            {
                await addAllSeriesEpisodes(series);
            }


            return new ResultContainer();
        }
    }
}
