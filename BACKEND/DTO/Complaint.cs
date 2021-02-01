using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Complaint
    {
            public int complaintId { get; set; }
            public int gmhCode { get; set; }
            public System.DateTime date { get; set; }
            public string text { get; set; }
            public string fingerPrint { get; set; }      
    }
}
