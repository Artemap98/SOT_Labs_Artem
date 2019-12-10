using NUnit.Framework;
using System.Configuration;

namespace Core
{
    [TestFixture]
    class ServicesLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["ServicesConnectionString"].ConnectionString;
        private ServicesLogic logic = new ServicesLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Inserterd Service", 10, 12.5f, 100, 100, true)]
        public void AddServiceTest(string title, int Customer_id, float price, int Price_id, int Material_id, bool expected)
        {
            myDataBaseDataSet.ServicesRow r = logic.NewServicesRow();
            r.title = title;
            r.Customer_id = Customer_id;
            r.price = price;
            r.Price_id = Price_id;
            r.Material_id = Material_id;

            bool actual = logic.AddService(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.ServicesRow inserted = logic.getServiceByID(id);
            // was added properly
            Assert.AreEqual(title, inserted.title);
            Assert.AreEqual(Customer_id, inserted.Customer_id);
            Assert.AreEqual(price, inserted.price);
            Assert.AreEqual(Price_id, inserted.Price_id);
        }

        [TestCase("Some Title", 10, 12.5f, 11, 100, true)]
        [TestCase("Some Title", 10, 12.5f, 11, 100, true)]
        [TestCase("Some Title", 10, 12.5f, 11, 100, true)]
        [TestCase("Another Title", 10, 12.5f, 11, 100, false)]
        [TestCase("Another Title", 10, 12.5f, 11, 100, false)]
        public void AddServiceRestrictedTest(string title, int Customer_id, float price, int Price_id, int Material_id, bool expected)
        {
            myDataBaseDataSet.ServicesRow r = logic.NewServicesRow();
            r.title = title;
            r.Customer_id = Customer_id;
            r.price = price;
            r.Price_id = Price_id;
            r.Material_id = Material_id;

            bool actual = logic.AddService(r);
            Assert.AreEqual(expected, actual);

        }
        [TestCase("Updated Service", 123.2f)]
        public void UpdateServiceTest(string title, float price)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.ServicesRow row = logic.getServiceByID(id);
            /// ?????? ---new myDataBaseDataSet.ServicesDataTable().NewServicesRow()
            myDataBaseDataSet.ServicesRow newRow = logic.NewServicesRow();
            newRow.title = title;
            newRow.price = price;

            bool result = logic.UpdateService(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.ServicesRow updated = logic.getServiceByID(id);
            // was updated correctly
            Assert.AreEqual(updated.title, title);
            Assert.AreEqual(updated.price, price);
            // the same values
            Assert.AreEqual(updated.Price_id, row.Price_id);
            Assert.AreEqual(updated.Material_id, row.Material_id);
            Assert.AreEqual(updated.Customer_id, row.Customer_id);
        }
        [TestCase("Deleted Service", 10, 12.5f, 100, 100, true)]
        public void DeleteServiceTest(string title, int Customer_id, float price, int Price_id, int Material_id, bool expected)
        {
            myDataBaseDataSet.ServicesRow r = logic.NewServicesRow();
            r.title = title;
            r.Customer_id = Customer_id;
            r.price = price;
            r.Price_id = Price_id;
            r.Material_id = Material_id;

            bool wasAdded = logic.AddService(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.ServicesRow inserted = logic.getServiceByID(insertedId);
            // was added correctly
            Assert.AreEqual(inserted.title, title);
            Assert.AreEqual(inserted.Customer_id, Customer_id);
            Assert.AreEqual(inserted.Material_id, Material_id);
            Assert.AreEqual(inserted.price, price);
            Assert.AreEqual(inserted.Price_id, Price_id);

            bool actual = logic.DeleteServiceWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", 10, 12.5f, 2, 100, true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string title, int Customer_id, float price, int Price_id, int Material_id, bool expected)
        {
            myDataBaseDataSet.ServicesRow r = logic.NewServicesRow();
            r.title = title;
            r.Customer_id = Customer_id;
            r.price = price;
            r.Price_id = Price_id;
            r.Material_id = Material_id;

            bool actual = logic.AddService(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.ServicesRow inserted = logic.getServiceByID(id);
            // was added properly
            Assert.AreEqual(inserted.title, title);
            Assert.AreEqual(inserted.Customer_id, Customer_id);
            Assert.AreEqual(inserted.price, price);
            Assert.AreEqual(inserted.Price_id, Price_id);
            Assert.AreEqual(inserted.Material_id, Material_id);

            // Updating db
            logic.UpdateDBWithCache();

            myDataBaseDataSet db = logic.getServicesDirectlyFromDb();
            myDataBaseDataSet final = new myDataBaseDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }
    }
}
