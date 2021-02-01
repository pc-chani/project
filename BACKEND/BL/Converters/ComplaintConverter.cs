using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class ComplaintConverter
    {
        public static DTO.Complaint convertToDTO(DAL.Complaint c)
        {
            return new DTO.Complaint
            {
                complaintId = c.complaintId,
                gmhCode = c.gmhCode,
                date = c.date,
                text = c.text,
                fingerPrint = c.fingerPrint   
            };
        }
        public static DAL.Complaint convertToDAL(DTO.Complaint c)
        {
            return new DAL.Complaint
            {
                complaintId = c.complaintId,
                gmhCode = c.gmhCode,
                date = c.date,
                text = c.text,
                fingerPrint = c.fingerPrint

            };
        }
        public static List<DTO.Complaint> convertToDTOList(List<DAL.Complaint> cList)
        {
            return cList.Select(c => convertToDTO(c)).ToList();
        }
    }
}
