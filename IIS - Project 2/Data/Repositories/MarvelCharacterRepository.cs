using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using IIS___Project_2.Models;

namespace IIS___Project_2.Data
{
    public class MarvelCharacterRepository
    {
        private readonly DatabaseConnection _dbConnection;

        // Query Constants for CRUD operations related to characters
        private const string GET_ALL_CHARACTERS_QUERY = "SELECT * FROM Characters";
        private const string GET_CHARACTER_BY_ID_QUERY = "SELECT * FROM Characters WHERE Id = @Id";
        private const string ADD_CHARACTER_QUERY = "INSERT INTO Characters (Name, Superpower, Team) VALUES (@Name, @Superpower, @Team)";
        private const string UPDATE_CHARACTER_QUERY = "UPDATE Characters SET Name = @Name, Superpower = @Superpower, Team = @Team WHERE Id = @Id";
        private const string DELETE_CHARACTER_QUERY = "DELETE FROM Characters WHERE Id = @Id";

        public MarvelCharacterRepository()
        {
            _dbConnection = new DatabaseConnection();  // Create a new instance of the DatabaseConnection class
        }

        // Get all characters from the database
        public IEnumerable<MarvelCharacter> GetAllCharacters()
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                var characters = connection.Query<MarvelCharacter>(GET_ALL_CHARACTERS_QUERY).ToList();
                return characters;
            }
        }

        // Get a character by id
        public MarvelCharacter GetCharacterById(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                var character = connection.QueryFirstOrDefault<MarvelCharacter>(GET_CHARACTER_BY_ID_QUERY, new { Id = id });
                return character;
            }
        }

        // Add a new character to the database
        public void AddCharacter(MarvelCharacter character)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                connection.Execute(ADD_CHARACTER_QUERY, character);
            }
        }

        // Update an existing character
        public void UpdateCharacter(int id, MarvelCharacter updatedCharacter)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                connection.Execute(UPDATE_CHARACTER_QUERY, new { updatedCharacter.Name, updatedCharacter.Superpower, updatedCharacter.Team, Id = id });
            }
        }

        // Delete a character
        public void DeleteCharacter(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                connection.Execute(DELETE_CHARACTER_QUERY, new { Id = id });
            }
        }
    }
}
