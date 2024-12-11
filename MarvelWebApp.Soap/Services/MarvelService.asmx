<%@ WebService Language="C#" Class="MarvelWebApp.Soap.Services.MarvelService" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MarvelWebApp.Soap.Models;

namespace MarvelWebApp.Soap.Services
{
    /// <summary>
    /// MarvelService is a SOAP web service for managing Marvel characters and movies.
    /// </summary>
    [WebService(Namespace = "http://marvelwebapp.soap/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MarvelService : WebService
    {
        // In-memory list of Marvel characters
        private static readonly List<MarvelCharacter> Characters = new List<MarvelCharacter>
        {
            new MarvelCharacter { Id = 1, Name = "Iron Man", Superpower = "Genius intellect, advanced suit of armor", Team = "Avengers" },
            new MarvelCharacter { Id = 2, Name = "Captain America", Superpower = "Enhanced strength, agility, and healing", Team = "Avengers" },
            new MarvelCharacter { Id = 3, Name = "Thor", Superpower = "God of Thunder, control over lightning and storms", Team = "Avengers" }
        };

        // In-memory list of Marvel movies
        private static readonly List<Movie> Movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Iron Man", ReleaseDate = new DateTime(2008, 5, 2), CharacterIds = new List<int> { 1 } },
            new Movie { Id = 2, Title = "Avengers", ReleaseDate = new DateTime(2012, 5, 4), CharacterIds = new List<int> { 1, 2, 3 } }
        };

        // CHARACTER OPERATIONS

        [WebMethod]
        public List<MarvelCharacter> GetAllCharacters()
        {
            return Characters;
        }

        [WebMethod]
        public MarvelCharacter GetCharacterById(int id)
        {
            return Characters.FirstOrDefault(c => c.Id == id);
        }

        [WebMethod]
        public string AddCharacter(MarvelCharacter character)
        {
            character.Id = Characters.Any() ? Characters.Max(c => c.Id) + 1 : 1;
            Characters.Add(character);
            return $"Character '{character.Name}' added successfully with ID {character.Id}.";
        }

        [WebMethod]
        public string UpdateCharacter(int id, MarvelCharacter updatedCharacter)
        {
            var character = Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
                return $"Character with ID {id} not found.";

            character.Name = updatedCharacter.Name;
            character.Superpower = updatedCharacter.Superpower;
            character.Team = updatedCharacter.Team;
            return $"Character with ID {id} updated successfully.";
        }

        [WebMethod]
        public string DeleteCharacter(int id)
        {
            var character = Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
                return $"Character with ID {id} not found.";

            // Remove the character from any movies they appear in
            Movies.ForEach(m => m.CharacterIds.Remove(id));
            Characters.Remove(character);
            return $"Character with ID {id} deleted successfully.";
        }

        // MOVIE OPERATIONS

        [WebMethod]
        public List<Movie> GetAllMovies()
        {
            return Movies;
        }

        [WebMethod]
        public Movie GetMovieById(int id)
        {
            return Movies.FirstOrDefault(m => m.Id == id);
        }

        [WebMethod]
        public string AddMovie(Movie movie)
        {
            movie.Id = Movies.Any() ? Movies.Max(m => m.Id) + 1 : 1;
            // Ensure all characters in the movie exist
            foreach (var characterId in movie.CharacterIds)
            {
                if (!Characters.Any(c => c.Id == characterId))
                    return $"Character with ID {characterId} does not exist.";
            }
            Movies.Add(movie);
            return $"Movie '{movie.Title}' added successfully with ID {movie.Id}.";
        }

        [WebMethod]
        public string UpdateMovie(int id, Movie updatedMovie)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return $"Movie with ID {id} not found.";

            // Ensure all characters in the updated movie exist
            foreach (var characterId in updatedMovie.CharacterIds)
            {
                if (!Characters.Any(c => c.Id == characterId))
                    return $"Character with ID {characterId} does not exist.";
            }

            movie.Title = updatedMovie.Title;
            movie.ReleaseDate = updatedMovie.ReleaseDate;
            movie.CharacterIds = updatedMovie.CharacterIds;
            return $"Movie with ID {id} updated successfully.";
        }

        [WebMethod]
        public string DeleteMovie(int id)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return $"Movie with ID {id} not found.";

            Movies.Remove(movie);
            return $"Movie with ID {id} deleted successfully.";
        }
    }
}