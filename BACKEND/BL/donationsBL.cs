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
                if (d.MasterCategory == 0) d.MasterCategory = null;

                db.Donations.Add(Converters.DonationConverter.convertToDAL(d));
                try
                {
                    db.SaveChanges();
                    int code = db.Donations.ToArray().Last().donationCode;
                    //   BL.EmailService.SendMail("תרומתך התקבלה", code+"מספר התרומה",d.donorEmail);
                    foreach (DAL.GMH g in db.GMH.Where(g1=> g1.CategoryCode==d.MasterCategory || g1.CategoryCode == d.Category))
                    {
                         BL.EmailService.SendMail("אולי זה מעניין אותך", code+"מספר התרומה",g.e_mail);
                    }
                    return code;
                      
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
        public static Donations GetDonation(int code)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.DonationConverter.convertToDTO(db.Donations.FirstOrDefault(d => d.donationCode==code));
            }

        }
        public static bool RemoveDonation(Donations d)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                if(d!=null)
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
        public static List<Donations> filterDonations(int c,int tc,string adress)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                List<Donations> donations = new List<Donations>();
                    if (tc != 0)
                    {


                        donations.AddRange(BL.Converters.DonationConverter.convertToDTOList(db.Donations.Where(d => d.Category == tc).ToList()));
                    if (adress != "" && adress != "undefind")
                    {
                        foreach (Donations d in donations)
                        {
                            if (BL.GoogleMaps.GetDistance(d.Adress, adress) > 50)
                                donations.Remove(d);
                        }


                    }
                }
                    else if (c != 0)
                    {
                    donations.AddRange(BL.Converters.DonationConverter.convertToDTOList(db.Donations.Where(d => d.MasterCategory == c).ToList()));
                    if (adress != "" && adress != "undefined")
                    {
                        foreach (Donations d in donations.ToList())
                        {
                            if (BL.GoogleMaps.GetDistance(d.Adress, adress) > 50)
                                donations.Remove(d);
                        }


                    }

                }
                else
                    {
                        foreach (DAL.Donations d in db.Donations)
                        {
                            if (BL.GoogleMaps.GetDistance(d.Adress, adress) < 50)
                                donations.Add(BL.Converters.DonationConverter.convertToDTO(d));
                        }
                }
                return donations;
            }
        }
        public static bool saveChanges(Donations d)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.Donations.FirstOrDefault(d1 => d1.donationCode == d.donationCode).donationName = d.donationName;
                db.Donations.FirstOrDefault(d1 => d1.donationCode == d.donationCode).Description = d.Description;
                db.Donations.FirstOrDefault(d1 => d1.donationCode == d.donationCode).Picture = d.Picture;
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
