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
                    message = string.Format("Pan: {0} or Serial Number: {1} exists already", panDetail.pan, panDetail.account_number);
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
                        SerialNumber = panDetail.account_number,
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

        public static PANDetail RetrievePanDetailsByAccountNumber(string accountNumber)
        {
            try
            {
                return ThirdPartyDL.RetrievePanDetailByAccountNumber(accountNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
