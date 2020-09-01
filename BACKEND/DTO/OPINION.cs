using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OPINION
    {
        public int OpinionCode { get; set; }
        public int LandingCode { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Comment { get; set; }
    }
}
