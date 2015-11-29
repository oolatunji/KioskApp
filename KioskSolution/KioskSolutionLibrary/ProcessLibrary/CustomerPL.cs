using KioskSolutionLibrary.DataLibrary;
using KioskSolutionLibrary.ModelLibrary;
using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSolutionLibrary.ProcessLibrary
{
    public class CustomerPL
    {
        public CustomerPL()
        {

        }

        public static bool Save(Customer customer, out string message)
        {
            try
            {
                if (CustomerDL.CustomerExists(customer))
                {
                    message = string.Format("Customer with email address: {0} exists already", customer.EmailAddress);
                    return false;
                }
                else
                {
                    message = string.Empty;
                    return CustomerDL.Save(customer);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SaveCardRequest(CardRequest cardRequest)
        {
            try
            {
                return CustomerDL.SaveCardRequest(cardRequest);
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
                return CustomerDL.Update(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Object> RetrieveCustomers()
        {
            try
            {
                List<Object> returnedCustomers = new List<object>();

                List<Customer> customers = CustomerDL.RetrieveCustomers();

                foreach (Customer customer in customers)
                {
                    Object customerObj = new
                    {
                        ID = customer.ID,
                        Lastname = customer.Lastname,
                        Othernames = customer.Othernames,
                        AccountNumber = customer.AccountNumber,
                        EmailAddress = customer.EmailAddress,
                        PhoneNumber = customer.PhoneNumber
                    };

                    returnedCustomers.Add(customerObj);
                }

                return returnedCustomers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Object> RetrieveCardRequests()
        {
            try
            {
                List<Object> returnedCardRequests = new List<object>();

                List<CardRequest> cardRequests = CustomerDL.RetrieveCardRequest();

                foreach (CardRequest cardRequest in cardRequests)
                {
                    Object cardRequestObj = new
                    {
                        ID = cardRequest.ID,
                        CustomerName = string.Format("{0} {1}", cardRequest.Customer.Lastname, cardRequest.Customer.Othernames),
                        AccountNumber = cardRequest.Customer.AccountNumber,
                        PickupBranch = cardRequest.Branch.Name,
                        CardType = cardRequest.CardType,
                        RequestType = cardRequest.RequestType,
                        RequestStatus = cardRequest.Status,
                        SerialNumber = cardRequest.SerialNumber,
                        RequestDate = String.Format("{0:G}", cardRequest.ModifiedDate),
                        Pan = string.IsNullOrEmpty(cardRequest.EncryptedPan) ? "None" : Crypter.Decrypt(System.Configuration.ConfigurationManager.AppSettings.Get("ekey"), cardRequest.EncryptedPan)
                    };

                    returnedCardRequests.Add(cardRequestObj);
                }

                return returnedCardRequests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static dynamic SendTokenToCustomer(string accountNumber, string serialNumber)
        {
            try
            {
                dynamic returnedCustomer = new System.Dynamic.ExpandoObject();
                if (!string.IsNullOrEmpty(serialNumber) && CustomerDL.CustomerCardRequestExists(serialNumber))
                {
                    throw new Exception(string.Format("Serial Number: {0} is already used. Kindly make use of another.", serialNumber));
                }
                else
                {
                    Customer customer = CustomerDL.RetrieveCustomerByAccountNumber(accountNumber);
                    if (customer != null)
                    {
                        Random random = new Random();
                        string token = random.Next(1999, 9999).ToString();
                        returnedCustomer.customerID = customer.ID;
                        returnedCustomer.customerToken = token;

                        Mail.SendCardRequest(customer, token);

                        return returnedCustomer;
                    }
                    else
                    {
                        throw new Exception(string.Format("Invalid customer with account number: {0}", accountNumber));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}
