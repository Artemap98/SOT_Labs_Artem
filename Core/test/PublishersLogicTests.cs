using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Configuration;

namespace Core
{
    [TestFixture]
    class CustomersLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["ServicesConnectionString"].ConnectionString;
        private CustomersLogic logic = new CustomersLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Google", "www.google.com", true)]
        public void AddCustomerTest(string name, string url, bool expected)
        {
            myDataBaseDataSet.CustomersRow r = logic.NewCustomersRow();
            r.name = name;
            r.url = url;

            bool actual = logic.AddCustomer(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.CustomersRow inserted = logic.getCustomerByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(url, inserted.url);
        }

        [TestCase("Mail Ru", "www.mail.ru", false)]
        [TestCase("ASTU", "www.altstu.edu", true)]
        public void AddCustomerRestrictedTest(string name, string url, bool expected)
        {
            myDataBaseDataSet.CustomersRow r = logic.NewCustomersRow();
            r.name = name;
            r.url = url;
            bool actual = logic.AddCustomer(r);
            Assert.AreEqual(expected, actual);

        }
        [TestCase("www.newurl.com")]
        public void UpdateCustomerTest(string url)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.CustomersRow row = logic.getCustomerByID(id);
            myDataBaseDataSet.CustomersRow newRow = logic.NewCustomersRow();
            newRow.url = url;

            bool result = logic.UpdateCustomer(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.CustomersRow updated = logic.getCustomerByID(id);
            // was updated correctly
            Assert.AreEqual(updated.url, url);
            // the same values
            Assert.AreEqual(updated.name, row.name);
        }
        [TestCase("O'Reily", "www.oreily.com", true)]
        public void DeleteServiceTest(string name, string url, bool expected)
        {
            myDataBaseDataSet.CustomersRow r = logic.NewCustomersRow();
            r.name = name;
            r.url = url;

            bool wasAdded = logic.AddCustomer(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.CustomersRow inserted = logic.getCustomerByID(insertedId);
            // was added correctly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(url, inserted.url);
        
            bool actual = logic.DeleteCustomerWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Petersburg", "www.petersburg.ru", true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string name, string url, bool expected)
        {
            myDataBaseDataSet.CustomersRow r = logic.NewCustomersRow();
            r.name = name;
            r.url = url;

            bool actual = logic.AddCustomer(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.CustomersRow inserted = logic.getCustomerByID(id);
            // was added properly
            Assert.AreEqual(inserted.name, name);
            Assert.AreEqual(inserted.url, url);

            // Updating db
            logic.UpdateDBWithCache();

            myDataBaseDataSet db = logic.getCustomersDirectlyFromDb();
            myDataBaseDataSet final = new myDataBaseDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }
    }
}
