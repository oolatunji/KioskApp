using KioskSolutionLibrary.DataLibrary;
using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSolutionLibrary.ProcessLibrary
{
    public class ThirdPartyPL
    {

        public static bool Save(PANDetail panDetail, out string message)
        {
            try
            {
                if (ThirdPartyDL.PanExists(panDetail))
                {
                    message = string.Format("Pan: {0} exists already", panDetail.pan);
                    return false;
                }
                else
                {
                    message = string.Empty;
                    return ThirdPartyDL.Save(panDetail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Object> RetrievePans()
        {
            try
            {
                List<Object> returnedPans = new List<object>();

                List<PANDetail> panDetails = ThirdPartyDL.RetrievePanDetails();

                foreach (PANDetail panDetail in panDetails)
                {
                    Object panDetailObj = new
                    {
                        Pan = panDetail.pan,
                        Status = panDetail.Status
                    };

                    returnedPans.Add(panDetailObj);
                }

                return returnedPans;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PANDetail RetrievePanDetailsByPan(string pan)
        {
            try
            {
                return ThirdPartyDL.RetrievePanDetailByPan(pan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
