using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class ZakazniciDTO
    {
        public string Email { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public int Telefon { get; set; }
        public int ZakazniciID { get; set; }
        public AddressDTO Address { get; set; }
    }
}
