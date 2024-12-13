using IIS___Project_2.Models;
using IIS___Project_2.Data; // Assuming you have these namespaces

namespace IIS___Project_2.Services
{
    public class MarvelService: IMarvelService
    {
        private readonly MarvelCharacterRepository _characterRepo;
        private readonly MovieRepository _movieRepo;

        // Constructor: Initialize the repositories
        public MarvelService(MarvelCharacterRepository characterRepo, MovieRepository movieRepo)
        {
            _characterRepo = characterRepo;
            _movieRepo = movieRepo;
        }


        // Character Operations

      
        public IEnumerable<MarvelCharacter> GetAllCharacters()
        {
            return _characterRepo.GetAllCharacters(); // Call the repository to fetch characters from the database
        }

        public MarvelCharacter GetCharacterById(int id)
        {
            return _characterRepo.GetCharacterById(id);
        }

        public void AddCharacter(MarvelCharacter character)
        {
            _characterRepo.AddCharacter(character);
        }

        public void UpdateCharacter(int id, MarvelCharacter updatedCharacter)
        {
            _characterRepo.UpdateCharacter(id, updatedCharacter);
        }

        public void DeleteCharacter(int id)
        {
            _characterRepo.DeleteCharacter(id);
        }


        // Movie Operations

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieRepo.GetAllMovies();
        }

        public Movie GetMovieById(int id)
        {
            return _movieRepo.GetMovieById(id);
        }

        public void AddMovie(Movie movie)
        {
            _movieRepo.AddMovie(movie);
        }

        public void UpdateMovie(int id, Movie updatedMovie)
        {
            _movieRepo.UpdateMovie(id, updatedMovie);
        }

        public void DeleteMovie(int id)
        {
            _movieRepo.DeleteMovie(id);
        }


        // Relationship Queries

        public IEnumerable<Movie> GetMoviesForCharacter(int characterId)
        {
            return _movieRepo.GetMoviesForCharacter(characterId);
        }

        public IEnumerable<MarvelCharacter> GetCharactersInMovie(int movieId)
        {
            return _movieRepo.GetCharactersInMovie(movieId);
        }
    }
}
