using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHControllers
{
    public interface ICustomerStore
    {
        void Initialize();
        Customer GetCustomerByName(string firstname, string lastname);
        Customer GetCustomerByID(int customerID);
        void SaveCustomer(Customer ahcustomer);
        void CreateCustomer(Customer ahcustomer);
    }

}
