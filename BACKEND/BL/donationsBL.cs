using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class donationsBL
    {
        public static int AddDonations(Donations d)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                db.Donations.Add(Converters.DonationConverter.convertToDAL(d));
                try
                {
                    db.SaveChanges();
                    return db.Donations.Last().donationCode;
               //שליחת מייל
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine(
                            "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("no");
                    return 0;
                }

            }
        }
        public static List<Donations> GetDonations()
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.DonationConverter.convertToDTOList(db.Donations.ToList());
            }
        
        }
        public static bool RemoveDonation(Donations d)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.Donations.Remove(BL.Converters.DonationConverter.convertToDAL(d));
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine(
                            "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("no");
                    return false;
                }
            }
        }
    }
}
