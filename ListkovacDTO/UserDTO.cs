using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListkovacDTO
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Pass { get; set; }
        public int Role { get; set; }
        public int IdPoradatele { get; set; }
        public int IdZakaznika { get; set; }
        
    }
}
