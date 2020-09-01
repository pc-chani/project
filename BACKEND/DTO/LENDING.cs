using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LENDING
    {
        public int LendingCode { get; set; }
        public int UserCode { get; set; }
        public int ProductCode { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<System.DateTime> LendingDate { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
    }
}
