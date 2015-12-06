using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSolutionLibrary.DataLibrary
{
    public class ThirdPartyDL
    {
        //Pan Methods
        public static bool Save(PANDetail panDetail)
        {
            try
            {
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    context.PANDetails.Add(panDetail);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool PanExists(PANDetail panDetail)
        {
            try
            {
                var existingPanDetail = new PANDetail();
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    existingPanDetail = context.PANDetails
                                    .Where(t => t.pan.Equals(panDetail.pan) || t.account_number.Equals(panDetail.account_number))
                                    .FirstOrDefault();
                }

                if (existingPanDetail == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PANDetail> RetrievePanDetails()
        {
            try
            {
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    var panDetails = context.PANDetails
                                    .ToList();

                    return panDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PANDetail> RetrieveUnUsedPanDetails()
        {
            try
            {
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    var panDetails = context.PANDetails
                                    .Where(x => x.Status == 0)
                                    .ToList();

                    return panDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PANDetail RetrievePanDetailByPan(string pan)
        {
            try
            {
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    var panDetails = context.PANDetails
                                            .Where(f => f.pan == pan);

                    return panDetails != null ? panDetails.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PANDetail RetrievePanDetailByAccountNumber(string accountNumber)
        {
            try
            {
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    var panDetails = context.PANDetails
                                            .Where(f => f.account_number == accountNumber);

                    return panDetails != null ? panDetails.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //User Methods
        public static bool Save(User user)
        {
            try
            {
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UserExists(User user)
        {
            try
            {
                var existingUser = new User();
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.UserName.Equals(user.UserName))
                                    .FirstOrDefault();
                }

                if (existingUser == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User RetrieveUserByUsername(string username)
        {
            try
            {
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    var existingUser = context.Users.Where(t => t.UserName.Equals(username));

                    return existingUser != null ? existingUser.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ChangePassword(string username, string newPassword)
        {
            try
            {
                User existingUser = new User();
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    existingUser = context.Users
                                    .Where(t => t.UserName == username)
                                    .FirstOrDefault();
                }

                if (existingUser != null)
                {
                    existingUser.Password = newPassword;                    
                    using (var context = new CardIssuanceKIOSKEntities())
                    {
                        context.Entry(existingUser).State = EntityState.Modified;

                        context.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UpdatePan(string pan)
        {
            try
            {
                PANDetail existingPanDetail = new PANDetail();
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    existingPanDetail = context.PANDetails                                    
                                    .Where(t => t.pan == pan)
                                    .FirstOrDefault();
                }

                if (existingPanDetail != null)
                {
                    existingPanDetail.Status = 1;

                    using (var context = new CardIssuanceKIOSKEntities())
                    {
                        context.Entry(existingPanDetail).State = EntityState.Modified;

                        context.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Save Card Account Request
        public static bool SaveCar(CardAccountRequest car)
        {
            try
            {
                using (var context = new CardIssuanceKIOSKEntities())
                {
                    context.CardAccountRequests.Add(car);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
