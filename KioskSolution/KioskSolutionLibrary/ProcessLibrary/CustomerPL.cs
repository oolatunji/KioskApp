using KioskSolutionLibrary.DataLibrary;
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

        public static Branch RetrieveBranchByID(long? branchID)
        {
            try
            {
                return BranchDL.RetrieveBranchByID(branchID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
