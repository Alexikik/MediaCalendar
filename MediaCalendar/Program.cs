using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediaCalendar.Data;

using MediaCalendar.Data.Media;


namespace MediaCalendar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EnsureDbCreated(false);

            //GetImdbSeries getter = new GetImdbSeries();
            //Task<Episode> TEp = getter.getEpisode("Hejsa");
            //Episode ep = TEp.Result;

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void EnsureDbCreated(bool reset = false)
        {
            using (Database ctx = new Database())
            {
                if (reset)
                    ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
                //ctx.Database.Migrate();


                //MediaCalendar.Data.Media.Episode episodeTest = new MediaCalendar.Data.Media.Episode() { seriesId = 75, SeriesName = "test" };
                //MediaCalendar.Data.Media.Series seriesTest = new MediaCalendar.Data.Media.Series() { seriesId = "75", seriesName = "test", episodes = new List<MediaCalendar.Data.Media.Episode>() { episodeTest } };
                ////seriesTest.episodes.Add(episodeTest);

                ////ctx.Add(new MediaCalender.Shared.User() { Username = ".", Password = "." });
                //ctx.Add(seriesTest);
                //ctx.Add(episodeTest);
                ctx.SaveChanges();
            }
        }
    }
}
