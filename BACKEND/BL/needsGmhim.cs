using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class needsGmhim
    {
        public static List<NeedsGmhim> GetNeedsGmhim()
        {
            using(DAL.Charity_DBEntities db=new DAL.Charity_DBEntities())
            {
                return BL.Converters.GMHConverter.convertToDTOList(db.NeedsGmhim.ToList());
            }
        }
        public static List<NeedsGmhim> filterNeedsGmhim(int c,int tc,string adress)
        {
            List<DTO.NeedsGmhim> needsGmhims = new List<DTO.NeedsGmhim>();
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                if (tc != 0)
                {
             

                    needsGmhims.AddRange(BL.Converters.GMHConverter.convertToDTOList(db.NeedsGmhim.Where(ng => ng.CATEGORY==tc).ToList()));
                    if (adress != "" && adress != "undefind")
                    {
                        foreach (NeedsGmhim ng in needsGmhims)
                        {
                            if (BL.GoogleMaps.GetDistance(ng.Adress, adress) > 50)
                                needsGmhims.Remove(ng);
                        }


                    }
                }
                else if (c != 0)
                {
                    needsGmhims.AddRange(BL.Converters.GMHConverter.convertToDTOList(db.NeedsGmhim.Where(ng => ng.CATEGORY == c).ToList()));
                    if (adress != "" && adress != "undefind")
                    {
                        foreach (NeedsGmhim ng in needsGmhims.ToList())
                        {
                            if (BL.GoogleMaps.GetDistance(ng.Adress, adress) > 50)
                                needsGmhims.Remove(ng);
                        }


                    }

                }
               else
                {
                    foreach (DAL.NeedsGmhim ng in db.NeedsGmhim)
                    {
                        if (BL.GoogleMaps.GetDistance(ng.ADRESS, adress) < 50)
                            needsGmhims.Add(BL.Converters.GMHConverter.convertToDTO(ng));
                    }
                      

                }
                return needsGmhims.Distinct().ToList();
            }
        }
    }
}
