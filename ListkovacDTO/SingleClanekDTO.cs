using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class SingleClanekDTO
    {
        public List<KomentarDTO> Komentare { get; set; }
        public ClanekDTO Clanek { get; set; }
    }
}
