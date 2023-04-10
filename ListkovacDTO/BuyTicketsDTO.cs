using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class BuyTicketsDTO
    {
        public int AkceID { get; set; }
        public string Verze { get; set; }
        public int Pocet { get; set; }
        public int Uzivatel { get; set; }
        public int PocetVIP { get; set; }
    }
}
