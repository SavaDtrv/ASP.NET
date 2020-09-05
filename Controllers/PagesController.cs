using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ASP_Sava_Dimitrov.Services;
using ASP_Sava_Dimitrov.Models;
using System.IO;
using System.Collections.Specialized;
using System.Globalization;

namespace ASP_Sava_Dimitrov.Controllers
{
    public class PagesController : Controller
    {
        public const string fileLocation = "C://Xfiles//SCHOOL//ASP//ASP-project//Project//ASP_Sava_Dimitrov//XML_XSD//";
        // GET: Pages
        public ActionResult Load()
        {
            IList<FileManagment> fileManagment = new List<FileManagment>();
            //string fileName = fileLocation + "XML_FILE_{0}.xml";
            const string xsd = fileLocation + "CheckedByThePolice.xsd";

            DirectoryInfo directory = new DirectoryInfo(fileLocation);
            FileInfo[] files = directory.GetFiles("*.xml");

            foreach (FileInfo file in files)
            {
                FileManagment element = null;
                if (ValidateXmlUsingXsd.isValid(xsd, file.FullName))
                {
                    People obj = SerializerMachine.deserializer(file);
                    
                    SqlConnector sql = new SqlConnector(obj);
                    bool isSaved = sql.saveToDb();
                    if (isSaved)
                    {
                        element = new FileManagment(file.Name, "Валиден", true, "Зареден", true);
                    }
                    else
                    {
                        element = new FileManagment(file.Name, "Валиден", true, "Отхвърлен - намира се в БД", false);
                    }
                }
                else
                {
                    element = new FileManagment(file.Name, "Невалиден", false, "Отхвърлен - невалиден", false);
                }

                fileManagment.Add(element);
            }
            
            return View(fileManagment);
        }

        public ActionResult Form()
        {            
            return View();
        }

        public ActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveData()
        {
            NameValueCollection formData = this.Request.Form;

            People root = new People();

            try
            {
                // Get data for the additional info of a person
                Additional_person_info infoAboutPerson = null;
                if (formData["additional_phone"] != "" || formData["additional_email"] != "")
                {
                    infoAboutPerson = new Additional_person_info(formData["additional_phone"], formData["additional_email"]);
                }

                // Get data for the close person of the checked one
                Additional_person_info closePersonInfo = null;
                if (formData["closeP_phone"] != "" || formData["closeP_email"] != "")
                {
                    closePersonInfo = new Additional_person_info(formData["closeP_phone"], formData["closeP_email"]);
                }

                // Get data for the times a person has been checked
                Times_checked timesPersonBeenChecked = null;
                if (Convert.ToString(formData["this_month"]) == "Да")
                {
                    timesPersonBeenChecked = new Times_checked(Convert.ToInt32(formData["times"]), true);
                }
                else if (Convert.ToString(formData["this_month"]) == "Не")
                {
                    timesPersonBeenChecked = new Times_checked(Convert.ToInt32(formData["times"]), false);
                }

                // Get data for the criminal acts of a person
                Criminal_acts criminalActs = null;
                if (Convert.ToString(formData["isCriminal"]) == "Да")
                {
                    criminalActs = new Criminal_acts(true, formData["acts"]);
                }
                else if (Convert.ToString(formData["isCriminal"]) == "Не")
                {
                    criminalActs = new Criminal_acts(false, formData["acts"]);
                }

                // Get data for the chacked person
                Person checkedPerson = null;
                checkedPerson = new Person(formData["id_number"], Convert.ToString(formData["id_date_of_issue"]), Convert.ToString(formData["id_expiry"]), formData["id_issued_by"], formData["first_name"], 
                    formData["fathers_name"], formData["surname"], formData["personal_NO"], formData["sex"], formData["eyes_color"], Convert.ToInt32(formData["height"]), 
                    Convert.ToString(formData["date_of_birth"]), formData["nationality"], formData["place_of_birth"], formData["residence"], infoAboutPerson, closePersonInfo,
                    timesPersonBeenChecked, criminalActs);

                root.listOfPeople.Add(checkedPerson);

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Неуспешно запазване на елементите в базата и нереализиране на XML файла";
                return View("About");
            }

            // INPUT DATA TO XML
            try
            {
                SerializerMachine.serializer(root);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Неуспешно запазване на елементите в базата и нереализиране на XML файла";
                return View("About");
            }

            // INPUT DATA TO DB
            var directory = new DirectoryInfo("C://Xfiles//SCHOOL//ASP//ASP-project//Project//ASP_Sava_Dimitrov//XML_XSD//Form_XML//");
            var myFile = (from f in directory.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).First();


            var lastFile = directory.GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                         .First();

            bool check = false;
            if (ValidateXmlUsingXsd.isValid(fileLocation + "CheckedByThePolice.xsd", lastFile.FullName))
            {
                SqlConnector connector = new SqlConnector(root);
                check = connector.saveToDb();

                ViewBag.Message = (check) ? "Успешно запазени параметри в базата" : "Неуспешно запазване на елементите в базата";
            }
            else
            {
                ViewBag.Message = "Неуспешно запазване на елементите в базата - невалидно генериран XML";
            }


            return View("About");
        }
    }
}