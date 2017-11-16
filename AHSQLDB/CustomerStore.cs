using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Data.Entity.Validation;
//using AHControllers;

namespace AHSQLDB
{

    /// <summary>
    /// This is the class that knows how to save/retrieve a customer from our database
    /// </summary>
    public class CustomerStore : AHControllers.ICustomerStore
    {
        //public CustomerStore()
        //{

        //}

        public AHControllers.Customer GetCustomerByName(string firstname, string lastname)
        {

            AHControllers.Customer myCustomer = new AHControllers.Customer();
            //Querying with LINQ to Entities 
            using (var dbCtx = new activehoursEntities())
            {

                //// LINQ method
                //var L2EQuery_m = dbCtx.customers.Where(c => c.first_name == firstname);
                //myCustomer = L2EQuery_m.FirstOrDefault<customer>();

                // LINQ Query
                var L2EQuery_q = from cs in dbCtx.customers
                                 where cs.first_name == firstname
                                 where cs.last_name == lastname
                                 select cs;

                customer myCustomerx = L2EQuery_q.FirstOrDefault<customer>();
                myCustomer.FirstName = myCustomerx.first_name;
                myCustomer.LastName = myCustomerx.last_name;
                myCustomer.CustomerID = myCustomerx.customer_id;

                ////Entity SQL
                ////Querying with Object Services and Entity SQL
                //string sqlString = "SELECT VALUE cs FROM activehoursEntities.customers " +
                //                    "AS cs WHERE cs.first_name == '" + firstname + "'";

                //var objctx = (dbCtx as IObjectContextAdapter).ObjectContext;

                //ObjectQuery<customer> myCustomerx = objctx.CreateQuery<customer>(sqlString);
                //myCustomer = myCustomerx.First<customer>();
            }

            return myCustomer;
        }
        public AHControllers.Customer GetCustomerByID(int customerID)
        {

            AHControllers.Customer myCustomer = new AHControllers.Customer();
            //Querying with LINQ to Entities 
            using (var dbCtx = new activehoursEntities())
            {

                //// LINQ method
                //var L2EQuery_m = dbCtx.customers.Where(c => c.first_name == firstname);
                //myCustomer = L2EQuery_m.FirstOrDefault<customer>();

                // LINQ Query
                var L2EQuery_q = from cs in dbCtx.customers
                                 where cs.customer_id == customerID
                                 select cs;

                customer myCustomerx = L2EQuery_q.FirstOrDefault<customer>();
                myCustomer.FirstName = myCustomerx.first_name;
                myCustomer.LastName = myCustomerx.last_name;
                myCustomer.CustomerID = myCustomerx.customer_id;

                ////Entity SQL
                ////Querying with Object Services and Entity SQL
                //string sqlString = "SELECT VALUE cs FROM activehoursEntities.customers " +
                //                    "AS cs WHERE cs.first_name == '" + firstname + "'";

                //var objctx = (dbCtx as IObjectContextAdapter).ObjectContext;

                //ObjectQuery<customer> myCustomerx = objctx.CreateQuery<customer>(sqlString);
                //myCustomer = myCustomerx.First<customer>();
            }

            return myCustomer;
        }

        public void Initialize()
        {
            Console.WriteLine("Initialized a CustomerStore instance");
        }

        public void SaveCustomer(AHControllers.Customer myCustomer)
        {
            // using the disconnecte mode approach
            customer myCustomerx;
            // get the current customer
            //Querying with LINQ to Entities 
            using (var dbCtx = new activehoursEntities())
            {

                //// LINQ method
                //var L2EQuery_m = dbCtx.customers.Where(c => c.first_name == firstname);
                //myCustomer = L2EQuery_m.FirstOrDefault<customer>();

                // LINQ Query
                var L2EQuery_q = from cs in dbCtx.customers
                                 where cs.customer_id == myCustomer.CustomerID
                                 select cs;

                myCustomerx = L2EQuery_q.FirstOrDefault<customer>();
            }

            // update the properties
            myCustomerx.first_name = myCustomer.FirstName;
            myCustomerx.last_name = myCustomer.LastName;

            // update the db
            using (var dbCtx = new activehoursEntities())
            {
                dbCtx.Entry(myCustomerx).State = System.Data.Entity.EntityState.Modified;

                dbCtx.SaveChanges();
            }


        }
        public void CreateCustomer(AHControllers.Customer newCustomer)
        {
            //create DBContext object
            using (var dbCtx = new activehoursEntities())
            {
                //Add customer object into customers DBset
                customer myCustomerx = new customer();
                myCustomerx.first_name = newCustomer.FirstName;
                myCustomerx.last_name = newCustomer.LastName;

                dbCtx.customers.Add(myCustomerx);

                // call SaveChanges method to save customer into database
                dbCtx.SaveChanges();

                newCustomer.CustomerID = myCustomerx.customer_id;

            }

        }
    }

}
