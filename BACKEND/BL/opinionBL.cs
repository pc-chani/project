using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public  class opinionBL
    {
        public static bool addOpinion(OPINION opinion)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.OPINIONS.Add(Converters.OpinionConverter.convertToDal(opinion));
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
        
             public static OPINION[] getOpinionsForGMH(GMH gmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                List<OPINION> p = new List<OPINION>();
                foreach (var op in db.OPINIONS)
                {
                    if (op.gmhCode == gmh.GmhCode)
                        p.Add(BL.Converters.OpinionConverter.convertToDTO(op));
                }
                return p.ToArray();
                //return BL.Converters.ProductToGmhConverter.convertToDTOarray(db.PRODUCTtoGMH.Where(p=>p.GmhCode==gmh.GmhCode).ToArray());
            }
        }
    }
}
