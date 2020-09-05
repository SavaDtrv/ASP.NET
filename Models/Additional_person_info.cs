using System.Xml.Serialization;

namespace ASP_Sava_Dimitrov.Models
{
    public class Additional_person_info
    {

        public string phone { set; get; }
        public string email { set; get; }

        public Additional_person_info()
        {
            this.phone = "";
            this.email = "";
        }

        public Additional_person_info(string phone, string email)
        {
            this.phone = phone;
            this.email = email;
        }
    }
}