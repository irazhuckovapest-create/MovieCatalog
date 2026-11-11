using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Models;

namespace MovieCatalog.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private static List<Movie> movies = new List<Movie>()
        {
            new Movie { Id = 1, Title = "Одержимость", Genre = "Horror", Year = 2014, Rating = 8.5 },
            new Movie { Id = 2, Title = "Оно", Genre = "Horror", Year = 2017, Rating = 7.3 },
            new Movie { Id = 3, Title = "Бабадук", Genre = "Horror", Year = 2014, Rating = 6.8 },
            new Movie { Id = 4, Title = "Пила", Genre = "Horror", Year = 2004, Rating = 8.1 },
            new Movie { Id = 5, Title = "Аннабель", Genre = "Horror", Year = 2014, Rating = 5.9 }
        };

        private static int nextId = 6;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            foreach (var movie in movies)
            {
                if (movie.Id == id)
                {
                    return Ok(movie);
                }
            }

            return NotFound();
        }

        [HttpGet("by-title/{title}")]
        public IActionResult GetByTitle(string title)
        {
            foreach (var movie in movies)
            {
                if (movie.Title.ToLower() == title.ToLower())
                {
                    return Ok(movie);
                }
            }

            return NotFound();
        }

        [HttpGet("search")]
        public IActionResult Search(string? genre, double? minRating)
        {
            var result = new List<Movie>();

            foreach (var movie in movies)
            {
                bool genreOk = true;
                bool ratingOk = true;

                if (genre != null)
                {
                    genreOk = movie.Genre.ToLower() == genre.ToLower();
                }

                if (minRating != null)
                {
                    ratingOk = movie.Rating >= minRating;
                }

                if (genreOk && ratingOk)
                {
                    result.Add(movie);
                }
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Movie newMovie)
        {
            newMovie.Id = nextId;
            nextId++;

            movies.Add(newMovie);

            return Ok(newMovie);
        }
    }
}