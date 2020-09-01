using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RequestForLoan
    {
        public int RequestCode { get; set; }
        public int UserCode { get; set; }
        public int ProductCode { get; set; }
        public System.DateTime RequestDate { get; set; }
        public Nullable<int> Amount { get; set; }
    }
}
