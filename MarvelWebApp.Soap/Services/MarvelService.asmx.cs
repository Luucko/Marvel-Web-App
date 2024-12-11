using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MarvelWebApp.Soap.Services
{
    /// <summary>
    /// MarvelService is a simple SOAP web service for testing.
    /// </summary>
    [WebService(Namespace = "http://marvelwebapp.soap/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MarvelService : WebService
    {
        // A test method to check if the service works
        [WebMethod]
        public string GetTestMessage()
        {
            return "Hello, Marvel SOAP service is working!";
        }
    }
}