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
        private static string cns = ConfigurationManager.ConnectionStrings["myDataBaseConnectionString"].ConnectionString;
        private CustomersLogic logic = new CustomersLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Ananyev Pavel Ivanovich", true)]
        public void AddCustomerTest(string FIO, bool expected)
        {
            myDataBaseDataSet.CustomersRow r = logic.NewCustomersRow();
            r.FIO = FIO;

            bool actual = logic.AddCustomer(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.CustomersRow inserted = logic.getCustomerByID(id);
            // was added properly
            Assert.AreEqual(FIO, inserted.FIO);
        }

        [TestCase("Ananyev Pavel Ivanovich", true)]
        public void AddCustomerRestrictedTest(string FIO, bool expected)
        {
            myDataBaseDataSet.CustomersRow r = logic.NewCustomersRow();
            r.FIO = FIO;
            bool actual = logic.AddCustomer(r);
            Assert.AreEqual(expected, actual);

        }
        [TestCase("Ananyev Pavel Ivanovich")]
        public void UpdateCustomerTest(string FIO)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.CustomersRow row = logic.getCustomerByID(id);
            myDataBaseDataSet.CustomersRow newRow = logic.NewCustomersRow();

            bool result = logic.UpdateCustomer(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.CustomersRow updated = logic.getCustomerByID(id);
            // was updated correctly
            // the same values
            Assert.AreEqual(updated.FIO, row.FIO);
        }

        [TestCase("Ananyev Pavel Ivanovich", true)]
        public void DeleteServiceTest(string FIO, bool expected)
        {
            myDataBaseDataSet.CustomersRow r = logic.NewCustomersRow();
            r.FIO = FIO;

            bool wasAdded = logic.AddCustomer(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.CustomersRow inserted = logic.getCustomerByID(insertedId);
            // was added correctly
            Assert.AreEqual(FIO, inserted.FIO);
        
            bool actual = logic.DeleteCustomerWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }

        [TestCase("Ananyev Pavel Ivanovich", true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string FIO, bool expected)
        {
            myDataBaseDataSet.CustomersRow r = logic.NewCustomersRow();
            r.FIO = FIO;

            bool actual = logic.AddCustomer(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.CustomersRow inserted = logic.getCustomerByID(id);
            // was added properly
            Assert.AreEqual(inserted.FIO, FIO);

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
