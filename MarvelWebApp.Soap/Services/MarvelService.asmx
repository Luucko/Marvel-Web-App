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

        /// <summary>
        /// Retrieve all Marvel characters.
        /// </summary>
        [WebMethod(Description = "Retrieve all Marvel characters.")]
        public List<MarvelCharacter> GetAllCharacters()
        {
            return Characters;
        }

        /// <summary>
        /// Retrieve a Marvel character by ID.
        /// </summary>
        [WebMethod(Description = "Retrieve a Marvel character by ID.")]
        public MarvelCharacter GetCharacterById(int id)
        {
            return Characters.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Add a new Marvel character.
        /// </summary>
        [WebMethod(Description = "Add a new Marvel character.")]
        public string AddCharacter(MarvelCharacter character)
        {
            character.Id = Characters.Any() ? Characters.Max(c => c.Id) + 1 : 1;
            Characters.Add(character);
            return $"Character '{character.Name}' added successfully with ID {character.Id}.";
        }

        /// <summary>
        /// Update an existing Marvel character by ID.
        /// </summary>
        [WebMethod(Description = "Update an existing Marvel character by ID.")]
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

        /// <summary>
        /// Delete a Marvel character by ID.
        /// </summary>
        [WebMethod(Description = "Delete a Marvel character by ID.")]
        public string DeleteCharacter(int id)
        {
            var character = Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
                return $"Character with ID {id} not found.";

            Movies.ForEach(m => m.CharacterIds.Remove(id));
            Characters.Remove(character);
            return $"Character with ID {id} deleted successfully.";
        }

        /// <summary>
        /// Retrieve all Marvel movies.
        /// </summary>
        [WebMethod(Description = "Retrieve all Marvel movies.")]
        public List<Movie> GetAllMovies()
        {
            return Movies;
        }

        /// <summary>
        /// Retrieve a Marvel movie by ID.
        /// </summary>
        [WebMethod(Description = "Retrieve a Marvel movie by ID.")]
        public Movie GetMovieById(int id)
        {
            return Movies.FirstOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// Add a new Marvel movie.
        /// </summary>
        [WebMethod(Description = "Add a new Marvel movie.")]
        public string AddMovie(Movie movie)
        {
            movie.Id = Movies.Any() ? Movies.Max(m => m.Id) + 1 : 1;
            foreach (var characterId in movie.CharacterIds)
            {
                if (!Characters.Any(c => c.Id == characterId))
                    return $"Character with ID {characterId} does not exist.";
            }
            Movies.Add(movie);
            return $"Movie '{movie.Title}' added successfully with ID {movie.Id}.";
        }

        /// <summary>
        /// Update an existing Marvel movie by ID.
        /// </summary>
        [WebMethod(Description = "Update an existing Marvel movie by ID.")]
        public string UpdateMovie(int id, Movie updatedMovie)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return $"Movie with ID {id} not found.";

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

        /// <summary>
        /// Delete a Marvel movie by ID.
        /// </summary>
        [WebMethod(Description = "Delete a Marvel movie by ID.")]
        public string DeleteMovie(int id)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return $"Movie with ID {id} not found.";

            Movies.Remove(movie);
            return $"Movie with ID {id} deleted successfully.";
        }

        /// <summary>
        /// Retrieve all movies in which a specific character appears.
        /// </summary>
        [WebMethod(Description = "Retrieve all movies in which a specific character appears.")]
        public List<Movie> GetMoviesForCharacter(int characterId)
        {
            if (!Characters.Any(c => c.Id == characterId))
                throw new Exception($"Character with ID {characterId} not found.");

            return Movies.Where(m => m.CharacterIds.Contains(characterId)).ToList();
        }

        /// <summary>
        /// Retrieve all characters featured in a specific movie.
        /// </summary>
        [WebMethod(Description = "Retrieve all characters featured in a specific movie.")]
        public List<MarvelCharacter> GetCharactersInMovie(int movieId)
        {
            if (!Movies.Any(m => m.Id == movieId))
                throw new Exception($"Movie with ID {movieId} not found.");

            var characterIds = Movies.First(m => m.Id == movieId).CharacterIds;
            return Characters.Where(c => characterIds.Contains(c.Id)).ToList();
        }
    }
}