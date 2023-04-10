using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class AddressDTO
    {
        public int AdresyID { get; set; }
        public int Cislopopisne { get; set; }
        public string Mesto { get; set; }
        public int Psc { get; set; }
        public string Stat { get; set; }
        public string Ulice { get; set; }
    }
}
