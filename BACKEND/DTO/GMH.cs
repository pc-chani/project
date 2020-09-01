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
        public string GmeName { get; set; }
        public string Adress { get; set; }
        public int UserCode { get; set; }
        public int CategoryCode { get; set; }
        public byte[] Picture { get; set; }
        public Nullable<int> Amount { get; set; }
        public string FreeDescription { get; set; }
        public bool IsDisposable { get; set; }
        public Nullable<int> SecurityDepositAmount { get; set; }
        public string Status { get; set; }

    }
}
