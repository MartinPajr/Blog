using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class CreateAkceDTO
    {
        public string Date { get; set; }
        public string Name { get; set; }
        public int ProstoryID { get; set; }
        public string Popisakce { get; set; }
        public int PoradatelId { get; set; }
        public TimeSpan Cas { get; set; }
    }
}
