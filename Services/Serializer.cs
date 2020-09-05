using ASP_Sava_Dimitrov.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ASP_Sava_Dimitrov.Services
{
    public static class SerializerMachine
    {
        static int index = 0;
        const string endFileLocation = "C://Xfiles//SCHOOL//ASP//ASP-project//Project//ASP_Sava_Dimitrov//XML_XSD//Form_XML//XML_";
        static SerializerMachine()
        {
            DirectoryInfo directory = new DirectoryInfo(endFileLocation.Remove(endFileLocation.Length - 4));
            FileInfo[] files = directory.GetFiles("*.xml");

            index = files.Length;
        }
        public static People deserializer(FileInfo file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(People));
            TextReader reader = new StreamReader(file.FullName);
            People element = (People)deserializer.Deserialize(reader);

            reader.Close();

            return element;
        }
        public static void serializer(People element)
        {
            // Get Directory and continue from last file number
            XmlSerializer serializer = new XmlSerializer(typeof(People));
            TextWriter writer = new StreamWriter(endFileLocation + (index++) + ".xml");

            serializer.Serialize(writer, element);

            writer.Close();
        }
    }
}