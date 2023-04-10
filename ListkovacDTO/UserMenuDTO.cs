using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class UserMenuDTO
    {
        public List<VenueDTO> Venues { get; set; }
        public List<AkceFullDTO> Akce { get; set; }
        public int Role { get; set; }
        public int PoradatelID { get; set; }
        public ZakazniciDTO Zakaznik { get; set; }

    }
}
