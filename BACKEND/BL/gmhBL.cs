using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class gmhBL
{
        public static bool addGMH(GMH gmh)
        {           
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.GMH.Add(Converters.GMHConverter.convertToDal(gmh));
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
        public static bool delete(GMH gmh)
        {

            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            { 
                //List<ProductToGMH> pr =BL.Converters.ProductToGmhConverter.convertToDTOList(db.PRODUCTtoGMHs.Where(p => p.GmhCode == gmh.GmhCode).ToList());
                //pr.ForEach(p => db.Products.Remove(db.Products.FirstOrDefault(p1=>p1.ProductCode== p.ProductCode)));
                db.PRODUCTtoGMH.RemoveRange(db.PRODUCTtoGMH.Where(p => p.GmhCode == gmh.GmhCode));
                List<DAL.LENDINGS> list = new List<DAL.LENDINGS>();
                List<DAL.Images> list1 = new List<DAL.Images>();

                foreach (var l in db.LENDINGS)
                {
                    foreach (var p in db.PRODUCTtoGMH)
                    {
                        if (l.ProductCode == p.ProductCodeToGMH && p.GmhCode == gmh.GmhCode)
                            list.Add(l);
                    }

                }
                foreach (var i in db.Images)
                {
                    foreach (var p in db.PRODUCTtoGMH)
                    {
                        if (i.ProductCodeToGMH == p.ProductCodeToGMH && p.GmhCode == gmh.GmhCode)
                            list1.Add(i);
                    }

                }
                db.LENDINGS.RemoveRange(list);
                db.Images.RemoveRange(list1);
                db.GMH.Remove((db.GMH.SingleOrDefault(g => g.GmhCode == gmh.GmhCode)));
                try
                {
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("yes");
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    //הדפסת שגיאה בקישור לדאטא בס
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
        public static GMH[] getMyGmhim(User user)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {               
                    return BL.Converters.GMHConverter.convertToDTOarray((db.GMH.Where(g => g.UserCode == user.UserCode).ToArray()));                
            }
        }
        public static bool saveChange(GMH gmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                object a = db.GMH.FirstOrDefault(g => gmh.GmhCode == g.GmhCode);
                db.GMH.FirstOrDefault(g => gmh.GmhCode == g.GmhCode).GmhName = gmh.GmhName;
                db.GMH.FirstOrDefault(g => gmh.GmhCode == g.GmhCode).Phone = gmh.Phone;
                db.GMH.FirstOrDefault(g => gmh.GmhCode == g.GmhCode).e_mail = gmh.e_mail;
                db.GMH.FirstOrDefault(g => gmh.GmhCode == g.GmhCode).comments = gmh.comments;
                db.GMH.FirstOrDefault(g => gmh.GmhCode == g.GmhCode).Adress = gmh.Adress;
                try
                {
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("yes");
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    //הדפסת שגיאה בקישור לדאטא בס
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
        public static CategoryGMH[] getCategoriesForGmach(CategoryGMH masterGmachCode)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                List<CategoryGMH> a = BL.Converters.CategoryGMHConvereter.convertToDTOList(db.CategoryGMH.Where(s => s.MasterCategoryCode == masterGmachCode.CategoryCode).ToList());
                // System.Diagnostics.Debug.WriteLine(a.ToArray<CategoryGMH>());
                return (a.ToArray<CategoryGMH>());

            }
        }
        public static CategoryGMH[] getCategories()
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                List<CategoryGMH> a = BL.Converters.CategoryGMHConvereter.convertToDTOList(db.CategoryGMH.Where(s => s.MasterCategoryCode == null).ToList());
                System.Diagnostics.Debug.WriteLine(a.ToArray<CategoryGMH>());
                return (a.ToArray<CategoryGMH>());

            }
        }

       // public static List<GMH> searchGMH(CategoryGMH gmhForSEarch)
       // {
       //     using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
       //     {
       //         List<GMH> a=new List<GMH>();
       //
       //
       //         if (db.GMHs.Where(s => s.CategoryCode == gmhForSEarch.CategoryCode) != null)
       //              a = BL.Converters.GMHConverter.convertToDTOList(db.GMHs.Where(s => s.CategoryCode == gmhForSEarch.CategoryCode).ToList());
       //             // System.Diagnostics.Debug.WriteLine(a.ToArray<CategoryGMH>());
       //             return a;
       //         
       //
       //     }
       // }

        public static List<GMH> searchGMH( string text, int category, int tatCategory, double CurrentLocation1, double CurrentLocation2, string location)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {         
                List<GMH> a = new List<GMH>();

                if (text != "")               
                    foreach (var product in db.Products)
                    {
                        System.Diagnostics.Debug.WriteLine("textBox");

                        if (product.Productname.Equals(text))
                        {
                            foreach (var productToGmh in db.PRODUCTtoGMH)
                            {
                                
                                if (productToGmh.ProductCode == product.ProductCode)
                                {
                                    foreach (var gmh in db.GMH)
                                    {
                                        if( gmh.GmhCode==productToGmh.GmhCode)
                                        
                                            if (CurrentLocation1 != 0) {

                                                if (BL.GoogleMaps.GetDistance(gmh.Adress, Convert.ToString(CurrentLocation1 + " " + CurrentLocation2)) < 50)
                                                {
                                                    a.Add(BL.Converters.GMHConverter.convertToDTO(gmh));
                                                    System.Diagnostics.Debug.WriteLine("!!***** " + BL.GoogleMaps.GetDistance(gmh.Adress, Convert.ToString(CurrentLocation1 + " " + CurrentLocation2)));
                                                }
                                                    System.Diagnostics.Debug.WriteLine("textBox 0");
                                               // System.Diagnostics.Debug.WriteLine("!!***** "+ BL.GoogleMaps.GetDistance(gmh.Adress, Convert.ToString(CurrentLocation1 + "," + CurrentLocation2)));

                                            }
                                            else if (BL.GoogleMaps.GetDistance(gmh.Adress, location) < 50)
                                                    a.Add(BL.Converters.GMHConverter.convertToDTO(gmh));


                                        
                                    }
                                }                               
                                
                            }
                        }
                            
                    }

                

               if (db.GMH.Where(g => g.CategoryCode == tatCategory) != null)
                    foreach (var item in db.GMH)
                    {
                        System.Diagnostics.Debug.WriteLine("category");

                        if (CurrentLocation1 != 0) {
                            System.Diagnostics.Debug.WriteLine("category 0");

                            if (item.CategoryCode == tatCategory && (BL.GoogleMaps.GetDistance(item.Adress, Convert.ToString(CurrentLocation1 + " " + CurrentLocation2))) < 50)
                            {
                                a.Add(BL.Converters.GMHConverter.convertToDTO(item));
                                System.Diagnostics.Debug.WriteLine("!!***** " + BL.GoogleMaps.GetDistance(item.Adress, Convert.ToString(CurrentLocation1 + " " + CurrentLocation2)));
                            }
                            }
                            else {
                            System.Diagnostics.Debug.WriteLine("category 1");
                            if (item.CategoryCode == tatCategory && ((BL.GoogleMaps.GetDistance(item.Adress, location)) < 50)) { 
                                a.Add(BL.Converters.GMHConverter.convertToDTO(item));
                            }
                            System.Diagnostics.Debug.WriteLine(BL.GoogleMaps.GetDistance(item.Adress, location) + "," + item.Adress);

                        }
                    }

                if (db.GMH.Where(g => g.CategoryCode == category) != null)
                    foreach (var item in db.GMH)
                    {
                        System.Diagnostics.Debug.WriteLine("category");

                        if (CurrentLocation1 != 0)
                        {
                            System.Diagnostics.Debug.WriteLine("category 0");

                            if (item.CategoryCode == category && (BL.GoogleMaps.GetDistance(item.Adress, Convert.ToString(CurrentLocation1 + " " + CurrentLocation2))) < 50)
                            {
                                a.Add(BL.Converters.GMHConverter.convertToDTO(item));
                                System.Diagnostics.Debug.WriteLine("!!***** " + BL.GoogleMaps.GetDistance(item.Adress, Convert.ToString(CurrentLocation1 + " " + CurrentLocation2)));
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("category 1");
                            if (item.CategoryCode == category && ((BL.GoogleMaps.GetDistance(item.Adress, location)) < 50))
                            {
                                a.Add(BL.Converters.GMHConverter.convertToDTO(item));
                            }
                            System.Diagnostics.Debug.WriteLine(BL.GoogleMaps.GetDistance(item.Adress, location) + "," + item.Adress);

                        }

                    }
                return a;


            }
        }
    }
}
