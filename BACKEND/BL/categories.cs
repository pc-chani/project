using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class Categories
    {
        public static List<DTO.CategoryGMH> GetCategories()
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                return BL.Converters.CategoryGMHConvereter.ConvertToDTOList((db.CategoryGMH.Select(c=>c).ToList()));

            }
        }
        public static string GetCategoryName(int c)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                return (db.CategoryGMH.FirstOrDefault(c1 => c1.CategoryCode==c)).CategoryName;

            }
        }
        public static int AddCategory(CategoryGMH c)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
               if (c.MasterCategoryCode == 0) c.MasterCategoryCode = null;
                
                db.CategoryGMH.Add( BL.Converters.CategoryGMHConvereter.ConvertToDAL(c));
                
                try
                {
                    db.SaveChanges();
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
                }
                db.CategoryGMH.ToArray().Last().MasterCategoryCode = null;
                return db.CategoryGMH.ToArray().Last().CategoryCode;
            }

        }
    }
}
