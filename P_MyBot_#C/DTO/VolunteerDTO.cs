using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class VolunteerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public bool status { get; set; }
    }
}
