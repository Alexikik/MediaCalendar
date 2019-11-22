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
            EnsureLoginDbCreated(true);

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

                ctx.SaveChanges();
            }
        }

        private static void EnsureLoginDbCreated(bool reset = false)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                if (reset)
                    ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
                //ctx.Database.Migrate();

                ctx.SaveChanges();
            }
        }
    }
}
