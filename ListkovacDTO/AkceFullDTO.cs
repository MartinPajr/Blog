using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ListkovacDTO
{
    public class AkceFullDTO
    {
        public int AkceID { get; set; }
        public int Prostory_konaniID { get; set; }
        public DateTime Datum { get; set; }
        public string Nazevakce { get; set; }
        public int PoradateleID { get; set; }
        public string Popisakce { get; set; }
        public string Nazevprostor { get; set; }
        public string Kapacita { get; set; }
        public string Mesto { get; set; }
        public string Cislopopisne { get; set; }
        public string PSC { get; set; }
        public string Ulice { get; set; }
        public string Stat { get; set; }
        public TimeSpan Cas { get; set; }
    }
}
