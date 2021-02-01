using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DonationsBL
    {
        private static Donations donations;
        public static int AddDonations(Donations d)
        {
            donations = d;
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
               // if (d.MasterCategory == 0) d.MasterCategory = null;
               //מוסיפים תמיד?
                db.Donations.Add(Converters.DonationConverter.convertToDAL(d));
                try
                {
                    db.SaveChanges();
                    int code = db.Donations.ToArray().Last().donationCode;
                    donations.donationCode = code;
                    //   BL.EmailService.SendMail("תרומתך התקבלה", code+"מספר התרומה",d.donorEmail);
                    
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
        public static void CoordinationDonation()
        {

            List<DAL.GMH> gMHs = new List<DAL.GMH>();
            List<DAL.RequestForLoan> requests = new List<DAL.RequestForLoan>();
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            { 
                requests = db.RequestForLoan.Where(r => r.ProductCode == donations.ProductCode).ToList();
                if (db.Donations.Contains(BL.Converters.DonationConverter.convertToDAL(donations))){
                    requests.ForEach(r =>
                    {
                        if (db.DonationOffers.FirstOrDefault(dd => dd.DonationCode != donations.donationCode || dd.UserCode != r.UserCode) != null)
                            requests.Remove(r);
                    }); }
            gMHs = db.GMH.Where(g => g.CategoryCode == donations.Category).ToList();
         
            if (requests.Count() == 1)//אם יש רק אחד באותה קטגוריה
            {
                BL.EmailService.SendMail("קבלת הצעה לתרומה", donations.donationCode + "מספר התרומה", db.GMH.FirstOrDefault(g => g.CategoryCode == donations.Category).e_mail);
            }
            else if (requests.Count() > 1)//אם יש יותר בודקים מרחק 
            {                 
                         requests.ForEach(r => { 
                                 foreach (DAL.GMH g in db.GMH)
                                 {
                                 if (r.UserCode == g.UserCode)
                                     gMHs.Add(g);
                                 };
                         });                
                foreach (DAL.GMH g in db.GMH)
                {
                    if (BL.GoogleMaps.GetDistance(g.Adress, donations.Adress) > 30)
                        gMHs.Remove(g);
                }
                if (gMHs.Count() == 0) { }//מה לעשות אם אין אף אחד בקרבה
                else if (gMHs.Count() == 1)//אם יש אחד בקרבה גדולה
                {
                    BL.EmailService.SendMail("קבלת הצעה לתרומה", donations.donationCode + "מספר התרומה", gMHs[0].e_mail);
                }
                else if (gMHs.Count() > 1)
                {
                    gMHs = gMHs.Where(g => g.UserCode == (requests.OrderBy(g1 => g1.RequestDate).FirstOrDefault().UserCode)).ToList();
                    if (gMHs.Count() == 1)
                    {
                        BL.EmailService.SendMail("קבלת הצעה לתרומה", donations.donationCode + "מספר התרומה", gMHs[0].e_mail);
                    }
                    else if (gMHs.Count() > 1)
                    {
                        gMHs = gMHs.Where(g => g.UserCode == (db.OPINIONS.OrderBy(o => o.Rating).FirstOrDefault().LandingCode)).ToList();
                    }
                }
            }
            }

        }
        public static List<Donations> GetDonations()
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.DonationConverter.convertToDTOList(db.Donations.ToList());
            }

        }//פונקציה שמחזירה את כל התרומות
        public static Donations GetDonation(int code)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.DonationConverter.convertToDTO(db.Donations.FirstOrDefault(d => d.donationCode==code));
            }

        }//פונקציה שמקבלת קוד תרומה ומחזירה את אותה תרומה
        public static bool RemoveDonation(Donations d)//פונקציה שמקבלת תרומה ומוחקת אותה מטבלת בתרומות
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
        public static List<Donations> FilterDonations(int c,int tc,string adress)
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
                 //   else if (c != 0)
                 //   {
                 //   donations.AddRange(BL.Converters.DonationConverter.convertToDTOList(db.Donations.Where(d => d.MasterCategory == c).ToList()));
                 //   if (adress != "" && adress != "undefined")
                 //   {
                 //       foreach (Donations d in donations.ToList())
                 //       {
                 //           if (BL.GoogleMaps.GetDistance(d.Adress, adress) > 50)
                 //               donations.Remove(d);
                 //       }
                 //
                 //
                 //   }

              //  }
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
        }//פונקציה שמקבלת קריטריונים ומחזירה את התרומות המתאימות להם
        public static bool SaveChanges(Donations d)
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
