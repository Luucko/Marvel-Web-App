namespace IIS___Project_2.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<int> CharacterIds { get; set; } = new List<int>();
    }
}