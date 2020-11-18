using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class GMHConverter
    {
        public static DTO.GMH convertToDTO(DAL.GMH g)
        {
            return new DTO.GMH
            {
                GmhName = g.GmhName,
                GmhCode = g.GmhCode,
                Adress = g.Adress,
                comments = g.comments,
                e_mail = g.e_mail,
                Phone = g.Phone,
                CategoryCode=g.CategoryCode,
        
                UserCode=g.UserCode
            };
        }
        public static DAL.GMH convertToDal(DTO.GMH g)
        {
            return new DAL.GMH
            {
                GmhName = g.GmhName,
                GmhCode = g.GmhCode,
                Adress = g.Adress,
                
                CategoryCode = g.CategoryCode,
            
                
                UserCode = g.UserCode

            };
        }
        public static List<DTO.GMH> convertToDTOList(List<DAL.GMH> gList)
        {
            return gList.Select(g => convertToDTO(g)).ToList();
        }
        public static DTO.GMH[] convertToDTOarray(DAL.GMH[] gList)
        {
            return gList.Select(g => convertToDTO(g)).ToArray();
        }
    }
}
