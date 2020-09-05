using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ASP_Sava_Dimitrov.Services
{
    public class ValidateXmlUsingXsd
    {
        public static bool isValid(string xsdName, string xmlName) // give URI of files
        {
            bool errors = false;

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("http://example.com/XMLProject", xsdName);

            XDocument file = null;
            try
            {
                file = XDocument.Load(xmlName);
            } catch (Exception ex)
            {
                errors = true;
                return !errors;
            }

            if (file != null)
            {
                file.Validate(schemas, (o, e) =>
                {
                    errors = true;
                }
                );
            }
            

            return !errors;
        }
    }
}