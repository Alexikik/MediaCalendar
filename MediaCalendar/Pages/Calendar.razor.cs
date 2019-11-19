using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MediaCalendar.Containers;
using Syncfusion.EJ2.Blazor;
using Syncfusion.EJ2.Blazor.Schedule;
using MediaCalendar.Data;
using MediaCalendar.Data.Media;

namespace MediaCalendar.Pages
{
    public class CalendarBase : ComponentBase
    {
        [Inject]
        protected MovieLibary MovieLibary { set; get; }
        [Inject]
        protected SeriesLibary SeriesLibary { set; get; }
        [Inject]
        protected Database Database { set; get; }
        public List<AppointmentData> DataSource = new List<AppointmentData>();
        public StringContainer movieString;
        public EjsSchedule<AppointmentData> ScheduleObj;
        public string movieName, seriesName, seriesImdbId, addMovieResultString, addSeriesResultString, EpNum, clearDatabaseResultString, updatingEpisodes;
        public bool loaded = false, loginStatus;

        #region Calendar things
        // Needed because of bug in Blazor https://github.com/Joelius300/ChartJSBlazor/issues/93
        //private ReferenceConverter ReferenceConverter = new ReferenceConverter(typeof(EjsSchedule<object>));
        // Idk
        private void begin(Syncfusion.EJ2.Blazor.SplitButtons.ProgressEventArgs args)
        {
            args.Step = 2;
        }
        #endregion Calendar things

        protected override async Task OnInitializedAsync()
        {
            GetAllEpisodes();
        }

        public void AddMovie()
        {
            // Sends movie name to be added and retrieves bool about succesfulness
            ResultContainer answer = MovieLibary.AddMovie(movieName);

            // Process depending on result
            if (answer.result)
            {
                addMovieResultString = $"{movieName} added to followed media";
            }
            else
                addMovieResultString = $"{movieName} could not be added <br />{answer.errorMessage}";
        }

        public async Task AddSeriesViaName()
        {
            int seriesId;

            addSeriesResultString = "Adding series in progress";

            seriesId = await SeriesLibary.searchForSeriesViaName(seriesName);                   // Gets series id

            await AddSeries(seriesId);
        }

        public async Task AddSeriesViaID()
        {
            int seriesId;

            addSeriesResultString = "Adding series in progress";

            seriesId = await SeriesLibary.searchForSeriesViaImdbId(seriesImdbId);    // Gets series id

            await AddSeries(seriesId);
        }
        private async Task AddSeries(int seriesId)
        {
            // Sends movie name to be added and retrieves bool about succesfulness
            Task<ResultContainer> taskAnswer = SeriesLibary.AddSeries(seriesId);

            ResultContainer answer = await taskAnswer;

            // Process depending on result
            if (answer.result)
            {
                if (seriesName != "")
                    addSeriesResultString = $"{seriesName} added to followed media";
                else
                    addSeriesResultString = $"{seriesImdbId} added to followed media";
                UpdateSchedule();
                seriesName = "";
                seriesImdbId = "";
            }
            else
                addSeriesResultString = $"{seriesName} could not be added <br />{answer.errorMessage}";
        }

        public void GetAllEpisodes()
        {
            List<Episode> episodeList = new List<Episode>();
            episodeList = Database.EpisodeLibary.Where(e => e.firstAired > DateTime.Now.AddMonths(-1)).ToList();

            foreach (Episode episode in episodeList)
            {
                if (episode.firstAired == null)
                {
                    episode.firstAired = DateTime.Now;
                }
            }

            // Process depending on result
            foreach (Episode episode in episodeList.Reverse<Episode>())
            {
                AppointmentData epField = new AppointmentData();
                epField.Id = 1;
                if (episode.airedSeason == 0)
                    epField.Subject = episode.SeriesName + $" [Special {episode.airedEpisodeNumber}]";
                else
                    epField.Subject = episode.SeriesName + $" [S{episode.airedSeason}E{episode.airedEpisodeNumber}]";
                epField.StartTime = episode.firstAired;
                epField.EndTime = epField.StartTime;
                epField.Color = "#357cd2";

                DataSource.Add(epField);
            }
            EpNum = episodeList.Count().ToString();
            loaded = true;
        }

        public DateTime ConvertStrDateToDateTime(string dateString)
        {
            DateTime date = new DateTime();

            date = DateTime.Parse(dateString);

            return date;
        }

        public void ClearDatabase()
        {
            ResultContainer answer = new ResultContainer();

            try
            {
                foreach (Episode episode in Database.EpisodeLibary)
                {
                    Database.Remove(episode);
                }
                foreach (Series series in Database.SeriesLibary)
                {
                    Database.Remove(series);
                }
                Database.SaveChanges();

                answer.result = true;
            }
            catch (Exception)
            {
                answer.result = false;
                answer.errorMessage = "Error clearing database";
            }

            if (answer.result)
                clearDatabaseResultString = $"Database have been cleared";
            else
                clearDatabaseResultString = $"Database could not be cleared <br />{answer.errorMessage}";
        }
        public void UpdateSchedule()
        {
            DataSource.Clear();
            GetAllEpisodes();
            ScheduleObj.Refresh();
        }

        public async Task UpdateAllSeriesInDB()
        {
            updatingEpisodes = "Updating episodes";
            await SeriesLibary.UpdateAllSeriesInDB();
            updatingEpisodes = "Done Updating";
            UpdateSchedule();
        }
    }
}