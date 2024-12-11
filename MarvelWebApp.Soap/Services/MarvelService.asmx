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

        // A method to get all Marvel characters
        [WebMethod]
        public List<MarvelCharacter> GetAllCharacters()
        {
            return Characters;
        }
    }
}