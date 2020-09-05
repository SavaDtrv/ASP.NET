using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml;
using System.Xml.XmlConfiguration;
using System.Xml.Serialization;

namespace ASP_Sava_Dimitrov.Models
{
    [Serializable, XmlRoot("people")]
    public class People
    {
        [XmlElement("person")]
        public List<Person> listOfPeople = new List<Person>();

    }
}