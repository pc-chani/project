using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
   public class DonationConverter
    {
        public static DTO.Donations convertToDTO(DAL.Donation d)
        {
            return new DTO.Donations
            {
               Adress=d.Adress,
               Category=d.Category,
               Description=d.Description,
               donationCode=d.donationCode,
               donationName=d.donationName,
               donorName=d.donorName,
               MasterCategory=d.MasterCategory,
               Phone=d.Phone,
               Picture=d.Picture

            };
        }
        public static DAL.Donation convertToDAL(DTO.Donations d)
        {
            return new DAL.Donation
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
        public static List<DTO.Donations> convertToDTOList(List<DAL.Donation> dList)
        {
            return dList.Select(d => convertToDTO(d)).ToList();
        }
    }
}
