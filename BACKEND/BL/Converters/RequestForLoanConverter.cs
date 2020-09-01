using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class RequestForLoanConverter
    {
        public static DTO.RequestForLoan convertToDTO(DAL.RequestForLoan rfl)
        {
            return new DTO.RequestForLoan
            {
                ProductCode=rfl.ProductCode,
                Amount=rfl.Amount,
                RequestCode=rfl.RequestCode,
                RequestDate=rfl.RequestDate,
                UserCode=rfl.UserCode
            };
        }
        public static DAL.RequestForLoan convertToDal(DTO.RequestForLoan rfl)
        {
            return new DAL.RequestForLoan
            {
                ProductCode = rfl.ProductCode,
                Amount = rfl.Amount,
                RequestCode = rfl.RequestCode,
                RequestDate = rfl.RequestDate,
                UserCode = rfl.UserCode
            };
        }
        public static List<DTO.RequestForLoan> convertToDTOList(List<DAL.RequestForLoan> rflList)
        {
            return rflList.Select(rfl => convertToDTO(rfl)).ToList();
        }
    }
}
