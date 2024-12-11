using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarvelWebApp.Soap.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<int> CharacterIds { get; set; } = new List<int>();
    }
}