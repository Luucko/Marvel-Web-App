using IIS___Project_2.Models;
using System.Collections.Generic;

namespace IIS___Project_2.Services
{
    public interface IMarvelService
    {
        // Character Operations
        IEnumerable<MarvelCharacter> GetAllCharacters();
        MarvelCharacter GetCharacterById(int id);
        void AddCharacter(MarvelCharacter character);
        void UpdateCharacter(int id, MarvelCharacter updatedCharacter);
        void DeleteCharacter(int id);

        // Movie Operations
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void UpdateMovie(int id, Movie updatedMovie);
        void DeleteMovie(int id);

        // Relationship Queries
        IEnumerable<Movie> GetMoviesForCharacter(int characterId);
        IEnumerable<MarvelCharacter> GetCharactersInMovie(int movieId);
    }
}
