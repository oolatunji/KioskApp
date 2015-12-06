using KioskSolutionLibrary.ModelLibrary;
using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSolutionLibrary.DataLibrary
{
    public class CustomerDL
    {
        public CustomerDL()
        {

        }

        public static bool Save(Customer customer)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SaveCardRequest(CardRequest cardRequest, string loggedInUsername, out CardRequest savedCardRequest)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.CardRequests.Add(cardRequest);
                        context.SaveChanges();

                        KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.User thirdPartyUser = ThirdPartyDL.RetrieveUserByUsername(loggedInUsername);

                        Customer cardRequestCustomer = RetrieveCustomerByID(cardRequest.CustomerID);

                        KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.CardAccountRequest car = new ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.CardAccountRequest();

                        car.NameOnCard = string.Format("{0} {1}", cardRequestCustomer.Lastname, cardRequestCustomer.Othernames);

                        if (cardRequest.RequestType == StatusUtil.RequestType.WithSerialNumber.ToString())
                        {
                            KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.PANDetail panDetail = ThirdPartyDL.RetrievePanDetailByAccountNumber(cardRequest.SerialNumber);

                            car.PAN = panDetail.pan;

                            ThirdPartyDL.UpdatePan(panDetail.pan);
                        }

                        car.PrintStatus = 1;
                        car.UserPrinting = thirdPartyUser.id.ToString();
                        car.DATE = System.DateTime.Now;
                        if (cardRequest.RequestType == StatusUtil.RequestType.WithSerialNumber.ToString())
                            car.HolderIDNumber = cardRequest.SerialNumber;
                        car.PhoneNumber = cardRequestCustomer.PhoneNumber;
                        car.LastName = cardRequestCustomer.Lastname;
                        car.OtherName = cardRequestCustomer.Othernames;
                        car.emailaddress = cardRequestCustomer.EmailAddress;
                        car.Updateddate = System.DateTime.Now;
                        ThirdPartyDL.SaveCar(car);

                        cardRequest.Customer = cardRequestCustomer;

                        transaction.Commit();
                    }
                }
                cardRequest.Branch = BranchDL.RetrieveBranchByID(cardRequest.PickupBranchID);
                savedCardRequest = cardRequest;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CustomerExists(Customer customer)
        {
            try
            {
                var existingCustomer = new Customer();
                using (var context = new KioskWebDBEntities())
                {
                    existingCustomer = context.Customers
                                    .Where(t => t.EmailAddress.Equals(customer.EmailAddress))
                                    .FirstOrDefault();
                }

                if (existingCustomer == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CustomerCardRequestExists(string serialNumber)
        {
            try
            {
                var existingCardRequest = new CardRequest();
                using (var context = new KioskWebDBEntities())
                {
                    existingCardRequest = context.CardRequests
                                    .Where(t => t.SerialNumber.Equals(serialNumber))
                                    .FirstOrDefault();
                }

                if (existingCardRequest == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Customer> RetrieveCustomers()
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var customers = context.Customers
                                    .ToList();

                    return customers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CardRequest> RetrieveCardRequest(long branchID)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var cardRequests = context.CardRequests
                                    .Where(x => x.PickupBranchID == branchID)
                                    .Include(cr => cr.Branch)
                                    .Include(cr => cr.Customer)
                                    .ToList();

                    return cardRequests;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Customer RetrieveCustomerByID(long? customerID)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var customers = context.Customers
                                            .Where(f => f.ID == customerID);

                    return customers.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CardRequest RetrieveCardRequestByID(long? cardRequestID)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var cardRequests = context.CardRequests
                                            .Include(x => x.Customer)
                                            .Include(x => x.Branch)
                                            .Where(f => f.ID == cardRequestID);

                    return cardRequests.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Customer RetrieveCustomerByAccountNumber(string accountNumber)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var customers = context.Customers
                                            .Where(f => f.AccountNumber == accountNumber);

                    return customers.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Update(Customer customer)
        {
            try
            {
                Customer existingCustomer = new Customer();
                using (var context = new KioskWebDBEntities())
                {
                    existingCustomer = context.Customers
                                    .Where(t => t.ID == customer.ID)
                                    .FirstOrDefault();
                }

                if (existingCustomer != null)
                {
                    existingCustomer.Lastname = customer.Lastname;
                    existingCustomer.Othernames = customer.Othernames;
                    existingCustomer.EmailAddress = customer.EmailAddress;
                    existingCustomer.PhoneNumber = customer.PhoneNumber;

                    using (var context = new KioskWebDBEntities())
                    {
                        context.Entry(existingCustomer).State = EntityState.Modified;

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

        public static bool UpdateCardRequest(long cardRequestID, string clearPan, string loggedInUsername, out CardRequest cardRequest)
        {
            try
            {
                CardRequest existingRequest = new CardRequest();
                using (var context = new KioskWebDBEntities())
                {
                    existingRequest = context.CardRequests
                                    .Include(cr => cr.Customer)
                                    .Include(cr => cr.Branch)
                                    .Where(t => t.ID == cardRequestID)
                                    .FirstOrDefault();
                }

                if (existingRequest != null)
                {
                    if (existingRequest.RequestType == StatusUtil.RequestType.WithSerialNumber.ToString())
                    {
                        existingRequest.HashedPan = PasswordHash.MD5Hash(clearPan);
                        existingRequest.EncryptedPan = Crypter.Encrypt(System.Configuration.ConfigurationManager.AppSettings.Get("ekey"), clearPan);
                    }
                    existingRequest.Status = StatusUtil.CardStatus.Approved.ToString();
                    
                    using (var context = new KioskWebDBEntities())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Entry(existingRequest).State = EntityState.Modified;
                            context.SaveChanges();

                            KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.User thirdPartyUser = ThirdPartyDL.RetrieveUserByUsername(loggedInUsername);

                            KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.CardAccountRequest car = new ModelLibrary.EntityFrameworkLibrary.ThirdPartyData.CardAccountRequest();

                            car.NameOnCard = string.Format("{0} {1}", existingRequest.Customer.Lastname, existingRequest.Customer.Othernames);
                            if (existingRequest.RequestType == StatusUtil.RequestType.WithSerialNumber.ToString())
                                car.PAN = clearPan;
                            car.PrintStatus = 1;
                            car.UserPrinting = thirdPartyUser.id.ToString();
                            car.DATE = System.DateTime.Now;
                            if (existingRequest.RequestType == StatusUtil.RequestType.WithSerialNumber.ToString())
                                car.HolderIDNumber = existingRequest.SerialNumber;
                            car.PhoneNumber = existingRequest.Customer.PhoneNumber;
                            car.LastName = existingRequest.Customer.Lastname;
                            car.OtherName = existingRequest.Customer.Othernames;
                            car.emailaddress = existingRequest.Customer.EmailAddress;
                            car.Updateddate = System.DateTime.Now;
                            ThirdPartyDL.SaveCar(car);

                            ThirdPartyDL.UpdatePan(clearPan);
                            

                            transaction.Commit();
                        }
                    }
                    cardRequest = existingRequest;
                    return true;
                }
                else
                {
                    cardRequest = null;
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
