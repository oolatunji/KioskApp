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

        public static bool SaveCardRequest(CardRequest cardRequest)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    context.CardRequests.Add(cardRequest);
                    context.SaveChanges();
                }
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
    }
}
