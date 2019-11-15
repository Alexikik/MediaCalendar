using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using MediaCalendar.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediaCalendar.Containers;
using MediaCalendar.Data.Media;

namespace MediaCalendar.Data
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        { }

        public Database() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DatabaseFile.sqlite");
            //optionsBuilder.UseSqlite(@"data source = D:\home\site\wwwroot\App_Data\DatabaseFile.sqlite");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SeriesConfiguration());
            modelBuilder.ApplyConfiguration(new EpisodeConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Series> SeriesLibary { get; set; }
        public DbSet<Episode> EpisodeLibary { get; set; }
        public DbSet<Movie> MovieLibary { get; set; }

    }
}
