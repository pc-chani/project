using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class LendingConverter
    {
        public static DTO.LENDING convertToDTO(DAL.LENDING l)
        {
            return new DTO.LENDING
            {
            Amount=l.Amount,
            LendingCode=l.LendingCode,
            LendingDate=l.LendingDate,
            ProductCode=l.ProductCode,
            ReturnDate=l.ReturnDate,
            UserCode=l.UserCode
            };
        }
        public static DAL.LENDING convertToDAL(DTO.LENDING l)
        {
            return new DAL.LENDING
            {
                Amount = l.Amount,
                LendingCode = l.LendingCode,
                LendingDate = l.LendingDate,
                ProductCode = l.ProductCode,
                ReturnDate = l.ReturnDate,
                UserCode = l.UserCode
            };
        }
        public static List<DTO.LENDING> convertToDTOList(List<DAL.LENDING> lList)
        {
            return lList.Select(l => convertToDTO(l)).ToList();
        }
    }
}
