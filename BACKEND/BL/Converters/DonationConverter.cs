using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
   public class DonationConverter
    {
        public static DTO.Donations convertToDTO(DAL.Donations d)
        {
            return new DTO.Donations
            {
               Adress=d.Adress,
               Category=d.Category,
               Description=d.Description,
               donationCode=d.donationCode,
               donationName=d.donationName,
               donorName=d.donorName,
               donorEmail=d.donorEmail,
               MasterCategory=d.MasterCategory,
               Phone=d.Phone,
               Picture=d.Picture

            };
        }
        public static DAL.Donations convertToDAL(DTO.Donations d)
        {
            return new DAL.Donations
            {
                Adress = d.Adress,
                Category = d.Category,
                Description = d.Description,
                donationCode = d.donationCode,
                donationName = d.donationName,
                donorName = d.donorName,
                MasterCategory = d.MasterCategory,
                Phone = d.Phone,
                Picture = d.Picture

            };
        }
        public static List<DTO.Donations> convertToDTOList(List<DAL.Donations> dList)
        {
            return dList.Select(d => convertToDTO(d)).ToList();
        }
    }
}
