using System.Xml.Serialization;

namespace ASP_Sava_Dimitrov.Models
{
    public class Criminal_acts
    {
        [XmlAttribute("isCriminal")]
        public bool isCriminal;
        public string acts;

        public Criminal_acts()
        {
            this.isCriminal = false;
            this.acts = "";
        }

        public Criminal_acts(bool isCriminal, string acts)
        {
            this.isCriminal = isCriminal;
            this.acts = acts;
        }
    }
}