using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class TicketDTO
    {
        public string NazevAkce { get; set; }
        public int AkceID { get; set; }
        public int VstupenkyID { get; set;}
        public int Cena { get; set; }
        public string Verze { get; set; }
        public string QrCode { get; set; }
        public BitmapByteQRCode QrImage { get; set; }
        public bool Status { get; set; }
    }
}
