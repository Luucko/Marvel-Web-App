namespace IIS___Project_2.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }

        // Foreign key for MarvelCharacter
        public int CharacterId { get; set; }
    }
}