using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class OpinionConverter
    {
        public static DTO.OPINION convertToDTO(DAL.OPINION o)
        {
            return new DTO.OPINION
            {
             Comment=o.Comment,
             LandingCode=o.LandingCode,
             OpinionCode=o.OpinionCode,
             Rating=o.Rating
            };
        }
        public static DAL.OPINION convertToDal(DTO.OPINION o)
        {
            return new DAL.OPINION
            {
                Comment = o.Comment,
                LandingCode = o.LandingCode,
                OpinionCode = o.OpinionCode,
                Rating = o.Rating
            };
        }
        public static List<DTO.OPINION> convertToDTOList(List<DAL.OPINION> oList)
        {
            return oList.Select(o => convertToDTO(o)).ToList();
        }
    }
}
