using System.Xml.Serialization;

namespace ASP_Sava_Dimitrov.Models
{
    public class Person
    {
        [XmlAttribute("id_number")]
        public string id_number { set; get; }
        [XmlAttribute("id_date_of_issue")]
        public string id_date_of_issue { set; get; } //DATE
        [XmlAttribute("id_expiry")]
        public string id_expiry { set; get; } //DATE
        [XmlAttribute("id_issued_by")]
        public string id_issued_by { set; get; }
        public string first_name { set; get; }
        public string fathers_name { set; get; }
        public string surname { set; get; }
        public string personal_NO { set; get; }
        public string sex { set; get; }
        public string eyes_color { set; get; }
        public int height { set; get; }
        public string date_of_birth { set; get; } //DATE
        public string nationality { set; get; }
        public string place_of_birth { set; get; }
        public string residence { set; get; }
        [XmlElement("more_person_info")]
        public Additional_person_info more_person_info { set; get; }
        [XmlElement("close_person")]
        public Additional_person_info close_person { set; get; }
        [XmlElement("times_checked")]
        public Times_checked times_checked { set; get; }
        [XmlElement("criminal_acts")]
        public Criminal_acts criminal_acts { set; get; }

        public Person()
        {
            this.id_number = "";
            this.id_date_of_issue = "";
            this.id_expiry = "";
            this.id_issued_by = "";
            this.first_name = "";
            this.fathers_name = "";
            this.surname = "";
            this.personal_NO = "";
            this.sex = "";
            this.eyes_color = "";
            this.height = 0;
            this.date_of_birth = "";
            this.nationality = "";
            this.place_of_birth = "";
            this.residence = "";
            this.more_person_info = null;
            this.close_person = null;
            this.times_checked = null;
            this.criminal_acts = null;
    }

        public Person(string id_number, string id_date_of_issue, string id_expiry, string id_issued_by, string first_name, string fathers_name,
            string surname, string personal_NO, string sex, string eyes_color, int height, string date_of_birth, string nationality, string place_of_birth, 
            string residence, Additional_person_info more_person_info, Additional_person_info close_person, Times_checked times_checked, Criminal_acts criminal_acts)
        {
            this.id_number = id_number;
            this.id_date_of_issue = id_date_of_issue;
            this.id_expiry = id_expiry;
            this.id_issued_by = id_issued_by;
            this.first_name = first_name;
            this.fathers_name = fathers_name;
            this.surname = surname;
            this.personal_NO = personal_NO;
            this.sex = sex;
            this.eyes_color = eyes_color;
            this.height = height;
            this.date_of_birth = date_of_birth;
            this.nationality = nationality;
            this.place_of_birth = place_of_birth;
            this.residence = residence;
            this.more_person_info = more_person_info;
            this.close_person = close_person;
            this.times_checked = times_checked;
            this.criminal_acts = criminal_acts;
        }
    }
}