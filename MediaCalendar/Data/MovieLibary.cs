using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCalendar.Containers;
using MediaCalendar.Data.Media;
using Microsoft.EntityFrameworkCore;

namespace MediaCalendar.Data
{
    public class MovieLibary
    {
        Database database;
        public MovieLibary(Database database)
        {
            this.database = database;
        }

        public ResultContainer AddMovie(string movieName)
        {
            ResultContainer result = new ResultContainer();
            GetImdbMedia getter = new GetImdbMedia();
            Movie movie;

            movie = getter.getMovie(movieName);

            if (movie.Title == null)
            {
                result.result = false;
                result.errorMessage = "Not found";
                return result;
            }

            if (checkIfMovieIsInDB(movie))
            {
                result.result = false;
                result.errorMessage = "Already added";
                return result;
            }

            database.Add(movie);
            database.SaveChanges();

            result.result = true;
            return result;
        }

        private bool checkIfMovieIsInDB(Movie movie)
        {
            bool isInDb;

            isInDb = database.MovieLibary.Any(m => m.imdbID == movie.imdbID);

            return isInDb;
        }
    }
}
