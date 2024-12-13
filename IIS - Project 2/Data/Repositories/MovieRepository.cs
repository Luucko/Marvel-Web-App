using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using IIS___Project_2.Models;

namespace IIS___Project_2.Data
{
    public class MovieRepository
    {
        private readonly DatabaseConnection _dbConnection;

        // Query Constants for CRUD operations related to movies
        private const string GET_ALL_MOVIES_QUERY = "SELECT * FROM Movies";
        private const string GET_MOVIE_BY_ID_QUERY = "SELECT * FROM Movies WHERE Id = @Id";
        private const string ADD_MOVIE_QUERY = "INSERT INTO Movies (Title, ReleaseDate) VALUES (@Title, @ReleaseDate)";
        private const string UPDATE_MOVIE_QUERY = "UPDATE Movies SET Title = @Title, ReleaseDate = @ReleaseDate WHERE Id = @Id";
        private const string DELETE_MOVIE_QUERY = "DELETE FROM Movies WHERE Id = @Id";

        private const string GET_MOVIES_FOR_CHARACTER_QUERY = @"
            SELECT m.Id, m.Title, m.ReleaseDate 
            FROM Movies m
            JOIN MovieCharacters mc ON m.Id = mc.MovieId
            WHERE mc.CharacterId = @CharacterId";

        private const string GET_CHARACTERS_IN_MOVIE_QUERY = @"
            SELECT c.Id, c.Name, c.Superpower, c.Team 
            FROM Characters c
            JOIN MovieCharacters mc ON c.Id = mc.CharacterId
            WHERE mc.MovieId = @MovieId";

        public MovieRepository()
        {
            _dbConnection = new DatabaseConnection();  // Create a new instance of the DatabaseConnection class
        }

        // Get all movies from the database
        public IEnumerable<Movie> GetAllMovies()
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                var movies = connection.Query<Movie>(GET_ALL_MOVIES_QUERY).ToList();
                return movies;
            }
        }

        // Get a movie by id
        public Movie GetMovieById(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                var movie = connection.QueryFirstOrDefault<Movie>(GET_MOVIE_BY_ID_QUERY, new { Id = id });
                return movie;
            }
        }

        // Add a new movie to the database
        public void AddMovie(Movie movie)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                connection.Execute(ADD_MOVIE_QUERY, movie);
            }
        }

        // Update an existing movie
        public void UpdateMovie(int id, Movie updatedMovie)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                connection.Execute(UPDATE_MOVIE_QUERY, new { updatedMovie.Title, updatedMovie.ReleaseDate, Id = id });
            }
        }

        // Delete a movie
        public void DeleteMovie(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                connection.Execute(DELETE_MOVIE_QUERY, new { Id = id });
            }
        }

        // Get all movies for a specific character
        public IEnumerable<Movie> GetMoviesForCharacter(int characterId)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                var movies = connection.Query<Movie>(GET_MOVIES_FOR_CHARACTER_QUERY, new { CharacterId = characterId }).ToList();
                return movies;
            }
        }

        // Get all characters in a specific movie
        public IEnumerable<MarvelCharacter> GetCharactersInMovie(int movieId)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                var characters = connection.Query<MarvelCharacter>(GET_CHARACTERS_IN_MOVIE_QUERY, new { MovieId = movieId }).ToList();
                return characters;
            }
        }
    }
}
