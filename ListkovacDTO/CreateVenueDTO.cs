using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class CreateVenueDTO
    {
        public string nazevProstoru { get; set; }
        public int kapacita { get; set; }
        public int pocetsekci { get; set; }
        public bool kSezeniNormal { get; set; }
        public int pocetVipsekci { get; set; }
        public bool kSezeniVIP { get; set; }
        public string cpp { get; set; }
        public string mesto { get; set; }
        public string psc{get; set;}
        public string stat { get; set; }
        public string ulice { get; set; }

    }
}
