using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
        public class KomentarDTO
        {
            public int ClanekId { get; set; }
            public int Id { get; set; }
            public string Text { get; set; }
            public int UserId { get; set; }
            public BlogUserDTO User { get; set; }
            public DateTime Time { get; set; }

        }
    
}