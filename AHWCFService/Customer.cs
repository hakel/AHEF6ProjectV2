using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using Unity;

namespace AHWCFRestService
{
    public class Customer : ICustomer
    {
        //http://localhost:8732/customer/5
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "{id}")]
        public AHControllers.Customer GetCustomer(string id)
        {

            //// UNITY
            // call the bootstrapper to register stuff
            var controller = DIRegister();

            // lookup customer with the requested id 
            int lookupID = Convert.ToInt32(id);
            AHControllers.Customer fetchedCustomer = controller.GetCustomerByID(lookupID);

            return fetchedCustomer;
            //return fetchedCustomer;
        }
        //[OperationContract]
        [WebInvoke(UriTemplate = "/create",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        public int CreateCustomer(AHControllers.Customer newCustomer)
        {
            //// UNITY
            // call the bootstrapper to register stuff
            var controller = DIRegister();

            // save the customer
            controller.CreateCustomer(newCustomer);

            return newCustomer.CustomerID;

        }
        
        private AHControllers.CustomerController DIRegister()
        {
            // UNITY
            var container = new UnityContainer();
            // map/register the types
            container.RegisterType<AHControllers.ICustomerStore, AHSQLDB.CustomerStore>();
            // create the controller from Unity
            var controller = container.Resolve<AHControllers.CustomerController>();

            return controller;

        }

    }

}
