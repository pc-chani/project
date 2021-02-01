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
               // if (d.MasterCategory == 0) d.MasterCategory = null;
               //מוסיפים תמיד?
              //  db.Donations.Add(Converters.DonationConverter.convertToDAL(d));
                try
                {
                    db.SaveChanges();
                    
                    //foreach (var item in db.RequestForLoan)
                    //{
                    //    ///if( תרומה מתאימה לITEM)
                    //    BL.EmailService.offerDonationMail("תרומה תואמת לבקשתך", d.donationCode, db.USERS.Find(item.UserCode).E_mail);
                    //}
                    //int code = db.Donations.ToArray().Last().donationCode;//
                    //BL.EmailService.SendMail("תרומתך התקבלה", code+"מספר התרומה",d.donorEmail);
                    int code = db.Donations.ToArray().Last().donationCode;
                    
                    //   BL.EmailService.SendMail("תרומתך התקבלה", code+"מספר התרומה",d.donorEmail);
                    List<DAL.GMH> gMHs = new List<DAL.GMH>();
                    List<DAL.RequestForLoan> requests = new List<DAL.RequestForLoan>();

                    requests = db.RequestForLoan.Where(r => r.ProductCode == d.ProductCode).ToList();
                    gMHs = db.GMH.Where(g => g.CategoryCode == d.Category).ToList();
                    if (requests.Count() == 0)//אם אין גמח מאותה קטגוריה
                    {
                        db.Donations.Add(Converters.DonationConverter.convertToDAL(d));
                    }
                    else if (requests.Count() == 1)//אם יש רק אחד באותה קטגוריה
                    {
                        BL.EmailService.SendMail("קבלת הצעה לתרומה", code + "מספר התרומה", db.GMH.FirstOrDefault(g => g.CategoryCode == d.Category).e_mail);
                    }
                    else if(requests.Count() > 1)//אם יש יותר בודקים מרחק 
                    {
                        foreach (DAL.GMH g in db.GMH)
                        {
                            if (BL.GoogleMaps.GetDistance(g.Adress, d.Adress) < 30)
                                gMHs.Add(g);
                        }
                        if(gMHs.Count() == 0) { }//מה לעשות אם אין אף אחד בקרבה
                        else if(gMHs.Count()==1)//אם יש אחד בקרבה גדולה
                        {
                            BL.EmailService.SendMail("קבלת הצעה לתרומה", code + "מספר התרומה", gMHs[0].e_mail);
                        }
                        else if(gMHs.Count() > 1)
                        {
                          gMHs=  gMHs.Where(g => g.UserCode == (requests.OrderBy(g1 => g1.RequestDate).FirstOrDefault().UserCode)).ToList();
                            if (gMHs.Count() == 1)
                            {
                                BL.EmailService.SendMail("קבלת הצעה לתרומה", code + "מספר התרומה", gMHs[0].e_mail);
                            }
                            //else if (gMHs.Count() > 1)
                            //{
                            //    //gMHs = gMHs.Where(g => g.UserCode == (db.OPINIONS.OrderBy(o=>o.Rating).FirstOrDefault().LandingCode)).ToList();
                            //}
                        }
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

    public class CopyOfdonationsBL
    {
        public static int AddDonations(Donations d)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                //if (d.MasterCategory == 0) d.MasterCategory = null;

                db.Donations.Add(Converters.DonationConverter.convertToDAL(d));
                try
                {
                    db.SaveChanges();

                    foreach (var item in db.RequestForLoan)
                    {
                        ///if( תרומה מתאימה לITEM)
                        BL.EmailService.offerDonationMail("תרומה תואמת לבקשתך", d.donationCode, db.USERS.Find(item.UserCode).E_mail);
                    }
                    int code = db.Donations.ToArray().Last().donationCode;//
                    //BL.EmailService.SendMail("תרומתך התקבלה", code+"מספר התרומה",d.donorEmail);
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
                return BL.Converters.DonationConverter.convertToDTO(db.Donations.FirstOrDefault(d => d.donationCode == code));
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
        public static List<Donations> filterDonations(int c, int tc, string adress)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                List<Donations> donations = new List<Donations>();
                if (tc != 0)
                {


                    donations.AddRange(BL.Converters.DonationConverter.convertToDTOList(db.Donations.Where(d => d.Category == tc).ToList()));
                }
                //else if (c != 0)
                //{
                //    donations.AddRange(BL.Converters.DonationConverter.convertToDTOList(db.Donations.Where(d => d.MasterCategory == c).ToList()));

                //}
                if (adress != "undefined")
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
