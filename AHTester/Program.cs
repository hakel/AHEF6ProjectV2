using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Practices.Unity;
using Unity;
using System.Net;
using System.ServiceModel.Web;
using System.ServiceModel;
using Newtonsoft.Json;
using System.IO;

namespace AHTester
{
    class Program
    {

        static void Main(string[] args)
        {

            //TestInLine();
            TestWithJson();

        }

        static AHControllers.CustomerController DIRegister()
        {
            // UNITY
            var container = new UnityContainer();
            // map/register the types
            container.RegisterType<AHControllers.ICustomerStore, AHSQLDB.CustomerStore>();
            // create the controller from Unity
            var controller = container.Resolve<AHControllers.CustomerController>();

            return controller;

        }

        static void TestInLine()
        {
            Console.WriteLine("*** START ***");

            // create my customer
            var newCustomer = new AHControllers.Customer();

            //set customer name
            string newFirstName = "ursula";
            newCustomer.FirstName = newFirstName;
            newCustomer.LastName = "Hakel";


            //// UNITY
            // call the bootstrapper to register stuff
            var controller = DIRegister();

            // save the customer
            controller.CreateCustomer(newCustomer);

            Console.WriteLine("*** Added customer " + newCustomer.FirstName + " " + newCustomer.LastName + " - " + newCustomer.CustomerID.ToString());

            // fetch the customer to prove we saved them
            //AHControllers.Customer fetchedCustomer = controller.GetCustomerByName(newCustomer.FirstName, newCustomer.LastName);
            AHControllers.Customer fetchedCustomer = controller.GetCustomerByID(newCustomer.CustomerID);

            Console.WriteLine("*** Fetched customer " + fetchedCustomer.FirstName + " " + fetchedCustomer.LastName);

            // update the customer
            fetchedCustomer.FirstName = fetchedCustomer.FirstName + "x";

            // save the customer
            controller.SaveCustomer(fetchedCustomer);

            Console.WriteLine("*** Updated customer " + fetchedCustomer.FirstName + " " + fetchedCustomer.LastName);

            Console.WriteLine("*** END ***");
            Console.ReadKey();

        }
        static void TestWithJson()
        {

            Console.WriteLine("*** START ***");

            ////// build my customer
            ////var stubCustomer = new AHControllers.Customer();

            //////set customer name
            ////string newFirstName = "Maurice";
            ////stubCustomer.FirstName = newFirstName;
            ////stubCustomer.LastName = "Hakel";


            ////// create the customer
            ////AHControllers.Customer newCustomer = createCustomerFromWebService(stubCustomer);
            ////Console.WriteLine("*** Added customer " + newCustomer.FirstName + " " + newCustomer.LastName + " - " + newCustomer.CustomerID.ToString());

            AHControllers.Customer newCustomer = new AHControllers.Customer { CustomerID = 5 };

            // fetch the customer to prove we saved them
            AHControllers.Customer fetchedCustomer = getCustomerFromWebService(newCustomer.CustomerID);
            Console.WriteLine("*** Fetched customer " + fetchedCustomer.FirstName + " " + fetchedCustomer.LastName);

            ////// update the customer
            ////fetchedCustomer.FirstName = fetchedCustomer.FirstName + "x";

            ////// save the customer
            ////updateCustomerFromWebService(fetchedCustomer);

            ////// fetch the customer again to prove we saved them
            ////AHControllers.Customer fetchedAgainCustomer = getCustomerFromWebService(newCustomer.CustomerID);
            ////Console.WriteLine("*** Fetched customer " + fetchedAgainCustomer.FirstName + " " + fetchedAgainCustomer.LastName);

            Console.WriteLine("*** END ***");
            Console.ReadKey();

        }

        static AHControllers.Customer getCustomerFromWebService(int customerID)
        {
            string requestUrl = "http://localhost:8732/customer/" + customerID.ToString();

            try
            {

                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).", response.StatusCode,
                    response.StatusDescription));

                    Stream dataStream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(dataStream);
                    string json = sr.ReadToEnd();
                    AHControllers.Customer myCustomer = JsonConvert.DeserializeObject<AHControllers.Customer>(json);
                    return myCustomer;

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }

        }
        ////static AHControllers.Customer createCustomerFromWebService(AHControllers.Customer newCustomer)
        ////{

        ////    //TODO - now we need to post to the "create" endpiont
        ////    // search HttpWebRequest post

        ////    string requestUrl = "http://localhost:8732/customer/" + "create";

        ////    try
        ////    {

        ////        HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
        ////        request.Method = "POST";
        ////        request.ContentType = "application/json";

        ////        //TODO - how do i add my json?  convert, then add?

        ////        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        ////        {

        ////            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format(
        ////                "Server error (HTTP {0}: {1}).", response.StatusCode,
        ////            response.StatusDescription));

        ////            Stream dataStream = response.GetResponseStream();
        ////            StreamReader sr = new StreamReader(dataStream);
        ////            string json = sr.ReadToEnd();
        ////            //TODO - need to extract customer id from here
        ////            //AHControllers.Customer myCustomer = JsonConvert.DeserializeObject<AHControllers.Customer>(json);
        ////            int newCustomerID = 0;

        ////            AHControllers.Customer myCustomer = getCustomerFromWebService(newCustomerID);

        ////            return myCustomer;

        ////        }
        ////    }
        ////    catch (Exception e)
        ////    {

        ////        Console.WriteLine(e.Message);
        ////        return null;
        ////    }

        ////}
        ////static void updateCustomerFromWebService(AHControllers.Customer myCustomer)
        ////{

        ////    ////string requestUrl = "http://localhost:8732/customer/" + customerID.ToString();

        ////    ////try
        ////    ////{

        ////    ////    HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
        ////    ////    request.Method = "GET";
        ////    ////    request.ContentType = "application/json";

        ////    ////    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        ////    ////    {

        ////    ////        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format(
        ////    ////            "Server error (HTTP {0}: {1}).", response.StatusCode,
        ////    ////        response.StatusDescription));

        ////    ////        Stream dataStream = response.GetResponseStream();
        ////    ////        StreamReader sr = new StreamReader(dataStream);
        ////    ////        string json = sr.ReadToEnd();
        ////    ////        AHControllers.Customer myCustomer = JsonConvert.DeserializeObject<AHControllers.Customer>(json);
        ////    ////        return myCustomer;

        ////    ////    }
        ////    ////}
        ////    ////catch (Exception e)
        ////    ////{

        ////    ////    Console.WriteLine(e.Message);
        ////    ////    return null;
        ////    ////}

        ////}

    }
}
