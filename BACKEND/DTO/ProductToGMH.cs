using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
   
    public class ProductToGMH
    {
        public int ProductCodeToGMH { get; set; }
        public int ProductCode { get; set; }
        public int GmhCode { get; set; }
        public string Picture { get; set; }
        public Nullable<int> Amount { get; set; }
        public string FreeDescription { get; set; }
        public bool IsDisposable { get; set; }
        public Nullable<int> SecurityDepositAmount { get; set; }
        public string Status { get; set; }

    }
}
