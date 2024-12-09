namespace IIS___Project_2.Models
{
    public class MarvelCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Superpower { get; set; }
        public string Team { get; set; } // Example: Avengers, X-Men, etc.

        // Navigation property for related movies
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}