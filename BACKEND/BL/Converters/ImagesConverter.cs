using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
   public class ImagesConverter
    {
        public static DTO.Images convertToDTO(DAL.Image i)
        {
            return new DTO.Images
            {
               ProductCodeToGMH=i.ProductCodeToGMH,
               Path=i.Path,
               ImageCode=i.ImageCode

            };
        }
        public static DAL.Image convertToDAL(DTO.Images i)
        {
            return new DAL.Image
            {
                ProductCodeToGMH = i.ProductCodeToGMH,
                Path = i.Path,
                ImageCode = i.ImageCode

            };
        }
        public static List<DTO.Images> convertToDTOList(List<DAL.Image> iList)
        {
            return iList.Select(i => convertToDTO(i)).ToList();
        }
    }
}
