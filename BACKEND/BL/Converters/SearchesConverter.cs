using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
   public class SearchesConverter
    {
        public static DTO.searches convertToDTO(DAL.Searches s)
        {
            return new DTO.searches
            {
               Adress=s.Adress,
               Category=s.Category,
               fingerPrint=s.fingerPrint,
               Id=s.Id
            };
        }
        public static DAL.Searches convertToDal(DTO.searches s)
        {
            return new DAL.Searches
            {
                Adress = s.Adress,
                Category = s.Category,
                fingerPrint = s.fingerPrint,
                Id = s.Id
            };
        }
        public static List<DTO.searches> convertToDTOList(List<DAL.Searches> sList)
        {
            return sList.Select(s => convertToDTO(s)).ToList();
        }
    }
}

