using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarvelWebApp.Soap.Models
{
    public class MarvelCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Superpower { get; set; }
        public string Team { get; set; }
    }
}