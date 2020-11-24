using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class OpinionConverter
    {
        public static DTO.OPINION convertToDTO(DAL.OPINIONS o)
        {
            return new DTO.OPINION
            {
             Comment=o.Comment,
             LandingCode=o.LandingCode,
             OpinionCode=o.OpinionCode,
             Rating=o.Rating
            };
        }
        public static DAL.OPINIONS convertToDal(DTO.OPINION o)
        {
            return new DAL.OPINIONS
            {
                Comment = o.Comment,
                LandingCode = o.LandingCode,
                OpinionCode = o.OpinionCode,
                Rating = o.Rating
            };
        }
        public static List<DTO.OPINION> convertToDTOList(List<DAL.OPINIONS> oList)
        {
            return oList.Select(o => convertToDTO(o)).ToList();
        }
    }
}
