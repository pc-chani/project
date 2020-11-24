using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class searchesBL
    {
        public static bool addSearch(searches s)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.Searches.Add(Converters.SearchesConverter.convertToDal(s));
                try
                {
                    db.SaveChanges();
                    int count = 0;
                    foreach (var s1 in db.Searches)
                    {
                        if (s.Category == s1.Category && BL.GoogleMaps.GetDistance(s.Adress, s1.Adress) < 50 && s.fingerPrint != s1.fingerPrint)
                            count++;
                    }

                    {
                        if (count >= 5)
                        {
                            NeedsGmhim ng = new NeedsGmhim
                            {
                                Adress = s.Adress,
                                category = s.Category
                            };
                            System.Diagnostics.Debug.WriteLine(s.Adress);
                            db.NeedsGmhim.Add(BL.Converters.GMHConverter.convertToDal(ng));
                            try
                            {
                                db.SaveChanges();
                                return true;
                            }
                            catch { return false; }
                        }
                    }
                    return false;
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
