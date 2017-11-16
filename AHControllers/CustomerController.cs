using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHControllers
{
    /// <summary>
    /// This class controls the saving/getting of a customer, but relies on the customerstore passed in to do the heavy lifting
    /// </summary>
    public class CustomerController
    {
        private readonly ICustomerStore ahCustomerStore;
        public CustomerController(ICustomerStore ahCustomerStore)
        {
            this.ahCustomerStore = ahCustomerStore;
        }

        public void SaveCustomer(Customer ahcustomer)
        {
            this.ahCustomerStore.SaveCustomer(ahcustomer);
        }
        public void CreateCustomer(Customer ahcustomer)
        {
            this.ahCustomerStore.CreateCustomer(ahcustomer);
        }
        public Customer GetCustomerByName(string firstname, string lastname)
        {
            Customer ahcustomer = this.ahCustomerStore.GetCustomerByName(firstname, lastname);
            return ahcustomer;
        }
        public Customer GetCustomerByID(int customerID)
        {
            Customer ahcustomer = this.ahCustomerStore.GetCustomerByID(customerID);
            return ahcustomer;
        }


    }
}
