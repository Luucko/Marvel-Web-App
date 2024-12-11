using IIS___Project_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace IIS___Project_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarvelController : ControllerBase
    {
        // Separate in-memory lists for Characters and Movies
        private static readonly List<MarvelCharacter> Characters = new List<MarvelCharacter>
        {
            new MarvelCharacter
            {
                Id = 1,
                Name = "Iron Man",
                Superpower = "Genius intellect, advanced suit of armor",
                Team = "Avengers"
            },
            new MarvelCharacter
            {
                Id = 2,
                Name = "Captain America",
                Superpower = "Enhanced strength, agility, and healing",
                Team = "Avengers"
            },
            new MarvelCharacter
            {
                Id = 3,
                Name = "Thor",
                Superpower = "God of Thunder, control over lightning and storms",
                Team = "Avengers"
            }
        };

        private static readonly List<Movie> Movies = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Title = "Iron Man",
                ReleaseDate = new DateTime(2008, 5, 2),
                CharacterIds = new List<int> { 1 } // Linking movie to Iron Man
            },
            new Movie
            {
                Id = 2,
                Title = "Avengers",
                ReleaseDate = new DateTime(2012, 5, 4),
                CharacterIds = new List<int> { 1, 2, 3 } // Linking movie to Iron Man, Captain America, and Thor
            }
        };

        // CHARACTER CRUD OPERATIONS

        // GET: api/marvel/characters
        [HttpGet("characters")]
        public ActionResult<IEnumerable<MarvelCharacter>> GetAllCharacters()
        {
            return Ok(Characters);
        }

        // GET: api/marvel/characters/{id}
        [HttpGet("characters/{id}")]
        public ActionResult<MarvelCharacter> GetCharacterById(int id)
        {
            var character = Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // POST: api/marvel/characters
        [HttpPost("characters")]
        public ActionResult<MarvelCharacter> AddCharacter(MarvelCharacter character)
        {
            character.Id = Characters.Any() ? Characters.Max(c => c.Id) + 1 : 1;
            Characters.Add(character);
            return CreatedAtAction(nameof(GetCharacterById), new { id = character.Id }, character);
        }

        // PUT: api/marvel/characters/{id}
        [HttpPut("characters/{id}")]
        public IActionResult UpdateCharacter(int id, MarvelCharacter updatedCharacter)
        {
            var character = Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            character.Name = updatedCharacter.Name;
            character.Superpower = updatedCharacter.Superpower;
            character.Team = updatedCharacter.Team;

            return NoContent();
        }

        // DELETE: api/marvel/characters/{id}
        [HttpDelete("characters/{id}")]
        public IActionResult DeleteCharacter(int id)
        {
            var character = Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            // Remove character from any movies they are linked to
            Movies.ForEach(m => m.CharacterIds.Remove(id));
            Characters.Remove(character);

            return NoContent();
        }

        // MOVIE CRUD OPERATIONS

        // GET: api/marvel/movies
        [HttpGet("movies")]
        public ActionResult<IEnumerable<Movie>> GetAllMovies()
        {
            return Ok(Movies);
        }

        // GET: api/marvel/movies/{id}
        [HttpGet("movies/{id}")]
        public ActionResult<Movie> GetMovieById(int id)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST: api/marvel/movies
        [HttpPost("movies")]
        public ActionResult<Movie> AddMovie(Movie movie)
        {
            // Ensure all linked characters exist
            foreach (var characterId in movie.CharacterIds)
            {
                if (!Characters.Any(c => c.Id == characterId))
                {
                    return BadRequest($"Character with ID {characterId} does not exist.");
                }
            }

            movie.Id = Movies.Any() ? Movies.Max(m => m.Id) + 1 : 1;
            Movies.Add(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        // PUT: api/marvel/movies/{id}
        [HttpPut("movies/{id}")]
        public IActionResult UpdateMovie(int id, Movie updatedMovie)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            // Ensure all linked characters exist
            foreach (var characterId in updatedMovie.CharacterIds)
            {
                if (!Characters.Any(c => c.Id == characterId))
                {
                    return BadRequest($"Character with ID {characterId} does not exist.");
                }
            }

            movie.Title = updatedMovie.Title;
            movie.ReleaseDate = updatedMovie.ReleaseDate;
            movie.CharacterIds = updatedMovie.CharacterIds;

            return NoContent();
        }

        // DELETE: api/marvel/movies/{id}
        [HttpDelete("movies/{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            Movies.Remove(movie);
            return NoContent();
        }
    }
}