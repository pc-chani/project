using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class productsBL
    {
        public static PRODUCT[] getProducts()
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return (BL.Converters.ProductConverter.convertToDTOList((db.Products.Select(p => p).ToList())).ToArray());
      
            }
        }
        public static ProductToGMH[] getProductsForGMH(GMH gmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                List<ProductToGMH> p = new List<ProductToGMH>();
                foreach (var prtg in db.PRODUCTtoGMH)
                {
                    if (prtg.GmhCode == gmh.GmhCode)
                        p.Add(BL.Converters.ProductToGmhConverter.convertToDTO(prtg));
                }
                return p.ToArray();
                //return BL.Converters.ProductToGmhConverter.convertToDTOarray(db.PRODUCTtoGMH.Where(p=>p.GmhCode==gmh.GmhCode).ToArray());
            }
        }   
        public static PRODUCT[] getProductsAccordingToGmhCategory(GMH gmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.ProductConverter.convertToDTOarray(db.Products.Where(p => p.CategoryCode == gmh.CategoryCode).ToArray());
            }
        }
        public static PRODUCT getProduct(ProductToGMH pTgmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.ProductConverter.convertToDTO(db.Products.FirstOrDefault(p => p.ProductCode == pTgmh.ProductCode));
            }
        }

        public static int addProduct(PRODUCT p)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.Products.Add(Converters.ProductConverter.convertToDal(p));

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
                return db.Products.ToArray().Last().ProductCode;
            }
        }

        public static bool saveChange(ProductToGMH pTgmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.PRODUCTtoGMH.FirstOrDefault(p => p.ProductCodeToGMH == pTgmh.ProductCodeToGMH).Amount = pTgmh.Amount;
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
        public static bool add(ProductToGMH pTgmh, List<string> photos)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.PRODUCTtoGMH.Add(Converters.ProductToGmhConverter.convertToDal(pTgmh));
                photos.ForEach(p =>
                {
                    DAL.Images image = new DAL.Images();
                    image.Path = p;
                    image.ProductCodeToGMH = pTgmh.ProductCodeToGMH;
                    db.Images.Add(image);
                });
                try
                {
                    db.SaveChanges();
                    return true;
                }
                 catch (DbEntityValidationException ex) {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine(
                        "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                System.Diagnostics.Debug.WriteLine("no");
                return false; }
            }
        }
        public static bool edit(ProductToGMH pTgmh, List<string> photos)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.PRODUCTtoGMH.FirstOrDefault(p => p.ProductCodeToGMH == pTgmh.ProductCodeToGMH).FreeDescription = pTgmh.FreeDescription;
                db.PRODUCTtoGMH.FirstOrDefault(p => p.ProductCodeToGMH == pTgmh.ProductCodeToGMH).IsDisposable = pTgmh.IsDisposable;
                db.PRODUCTtoGMH.FirstOrDefault(p => p.ProductCodeToGMH == pTgmh.ProductCodeToGMH).Status = pTgmh.Status;
                db.PRODUCTtoGMH.FirstOrDefault(p => p.ProductCodeToGMH == pTgmh.ProductCodeToGMH).SecurityDepositAmount = pTgmh.SecurityDepositAmount;
                db.PRODUCTtoGMH.FirstOrDefault(p => p.ProductCodeToGMH == pTgmh.ProductCodeToGMH).ProductCode = pTgmh.ProductCode;

                photos.ForEach(p =>
                {
                    DAL.Images image = new DAL.Images();
                    image.Path = p;
                    image.ProductCodeToGMH = pTgmh.ProductCodeToGMH;
                    db.Images.Add(image);
                });
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
        public static bool delete(ProductToGMH p)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.LENDINGS.RemoveRange(db.LENDINGS.Where(l => l.ProductCode == p.ProductCodeToGMH));
                db.Images.RemoveRange(db.Images.Where(i => i.ProductCodeToGMH == p.ProductCodeToGMH));
                DAL.PRODUCTtoGMH p1 = db.PRODUCTtoGMH.SingleOrDefault(pt => pt.ProductCodeToGMH == p.ProductCodeToGMH);
                if(p1!=null)//לבינתיים
                db.PRODUCTtoGMH.Remove(p1);
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
        public static List<Images> getImages(ProductToGMH pTgmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.ImagesConverter.convertToDTOList(db.Images.Where(i => i.ProductCodeToGMH == pTgmh.ProductCodeToGMH).ToList());
            }
        }
    }
}
