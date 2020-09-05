using System.Xml.Serialization;

namespace ASP_Sava_Dimitrov.Models
{
    public class Times_checked
    {
        public int times;
        public bool this_month;

        public Times_checked()
        {
            this.times = 0;
            this.this_month = false;
        }

        public Times_checked(int times, bool this_month)
        {
            this.times = times;
            this.this_month = this_month;
        }
    }
}