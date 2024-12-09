using IIS___Project_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace IIS___Project_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarvelController : ControllerBase
    {
        private static readonly List<MarvelCharacter> Characters = new List<MarvelCharacter>
        {
            new MarvelCharacter
            {
                Id = 1,
                Name = "Iron Man",
                Superpower = "Genius intellect, advanced suit of armor",
                Team = "Avengers",
                Movies = new List<Movie>
                {
                    new Movie { Id = 1, Title = "Iron Man", ReleaseDate = new DateTime(2008, 5, 2) },
                    new Movie { Id = 2, Title = "Avengers", ReleaseDate = new DateTime(2012, 5, 4) }
                }
            }
        };

        // GET: api/Marvel
        [HttpGet]
        public ActionResult<IEnumerable<MarvelCharacter>> GetAllCharacters()
        {
            return Ok(Characters);
        }

        // GET: api/Marvel/{id}
        [HttpGet("{id}")]
        public ActionResult<MarvelCharacter> GetCharacterById(int id)
        {
            var character = Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // POST: api/Marvel
        [HttpPost]
        public ActionResult<MarvelCharacter> AddCharacter(MarvelCharacter character)
        {
            character.Id = Characters.Any() ? Characters.Max(c => c.Id) + 1 : 1;
            Characters.Add(character);
            return CreatedAtAction(nameof(GetCharacterById), new { id = character.Id }, character);
        }

        // POST: api/Marvel/{characterId}/movies
        [HttpPost("{characterId}/movies")]
        public ActionResult<Movie> AddMovieForCharacter(int characterId, Movie movie)
        {
            var character = Characters.FirstOrDefault(c => c.Id == characterId);
            if (character == null)
            {
                return NotFound();
            }

            movie.Id = character.Movies.Any() ? character.Movies.Max(m => m.Id) + 1 : 1;
            character.Movies.Add(movie);
            return CreatedAtAction(nameof(GetCharacterById), new { id = characterId }, movie);
        }

        // DELETE: api/Marvel/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCharacter(int id)
        {
            var character = Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            Characters.Remove(character);
            return NoContent();
        }
    }
}
