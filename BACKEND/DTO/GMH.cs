using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class GMH
    {
        public int GmhCode { get; set; }
        public string GmhName { get; set; }
        public string Adress { get; set; }
        public int UserCode { get; set; }
        public int CategoryCode { get; set; }
        public string Phone { get; set; }
        public string e_mail { get; set; }
        public string comments { get; set; }

    }

    public class NeedsGmhim
    {
        public int id { get; set; }
        public int category { get; set; }
        public string Adress { get; set; }
        public string city { get; set; }

    }

}
