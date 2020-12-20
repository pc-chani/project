using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class Donations
    {
        public int donationCode { get; set; }
        public string donationName { get; set; }
        public int Category { get; set; }
      //  public Nullable<int> MasterCategory { get; set; }
        public string Description { get; set; }
        public string donorName { get; set; }
        public string donorEmail { get; set; }

        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Picture { get; set; }
        public Nullable<int> ProductCode { get; set; }

    }
}
