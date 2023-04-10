using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class SingleFullVenueDTO
    {
        public List<TicketsAkceDTO> Tickety { get; set; }
        public AddressDTO Adresa { get; set; }
        public AkceDTO Akce { get; set; }
        public VenueDTO Misto { get; set; }
        
    }
}
