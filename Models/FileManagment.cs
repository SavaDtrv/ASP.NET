using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Sava_Dimitrov.Models
{
    public class FileManagment
    {
        public static int id = 0;
        public int number;
        public string fileName { set; get; }
        public string status { set; get; }
        public bool validXML { set; get; }
        public string dbStatus { set; get; }
        public bool validLoad { set; get; }

        public FileManagment()
        {
            this.number = id++;
            this.fileName = "";
            this.status = "";
            this.validXML = false;
            this.dbStatus = "";
            this.validLoad = false;
        }
        public FileManagment(string fileName, string status, bool validXML, string dbStatus, bool validLoad)
        {
            this.number = id++;
            this.fileName = fileName;
            this.status = status;
            this.validXML = validXML;
            this.dbStatus = dbStatus;
            this.validLoad = validLoad;
        }

        public void fillInformation(string fileName, string status, bool validXML, string dbStatus, bool validLoad)
        {
            this.number = id++;
            this.fileName = fileName;
            this.status = status;
            this.validXML = validXML;
            this.dbStatus = dbStatus;
            this.validLoad = validLoad;
        }
    }
}