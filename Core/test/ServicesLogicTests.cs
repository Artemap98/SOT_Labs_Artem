using NUnit.Framework;
using System.Configuration;

namespace Core
{
    [TestFixture]
    class ServicesLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["myDataBaseConnectionString"].ConnectionString;
        private ServicesLogic logic = new ServicesLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("12.12.2012", 10, 12, 100, 1000, true)]
        public void AddServiceTest(string Date_Time, int id_Staff, int id_Customer, int id_Price, int Payment, bool expected)
        {
            myDataBaseDataSet.ServicesRow r = logic.NewServicesRow();
            r.Date_Time = Date_Time;
            r.id_Staff = id_Staff;
            r.id_Customer = id_Customer;
            r.id_Price = id_Price;
            r.Payment = Payment;

            bool actual = logic.AddService(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.ServicesRow inserted = logic.getServiceByID(id);
            // was added properly
            Assert.AreEqual(Date_Time, inserted.Date_Time);
            Assert.AreEqual(id_Staff, inserted.id_Staff);
            Assert.AreEqual(id_Customer, inserted.id_Customer);
            Assert.AreEqual(id_Price, inserted.id_Price);
            Assert.AreEqual(Payment, inserted.Payment);

        }

        [TestCase("12.12.2012", 10, 12, 100, 1000, true)]
        [TestCase("02.10.2024", 160, 122, 100, 8600, true)]
        public void AddServiceRestrictedTest(string Date_Time, int id_Staff, int id_Customer, int id_Price, int Payment, bool expected)
        {
            myDataBaseDataSet.ServicesRow r = logic.NewServicesRow();
            r.Date_Time = Date_Time;
            r.id_Staff = id_Staff;
            r.id_Customer = id_Customer;
            r.id_Price = id_Price;
            r.Payment = Payment;

            bool actual = logic.AddService(r);
            Assert.AreEqual(expected, actual);

        }
        [TestCase("27.04.2007", 123)]
        public void UpdateServiceTest(string Date_Time, int Payment)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.ServicesRow row = logic.getServiceByID(id);
            /// ?????? ---new myDataBaseDataSet.ServicesDataTable().NewServicesRow()
            myDataBaseDataSet.ServicesRow newRow = logic.NewServicesRow();
            newRow.Date_Time = Date_Time;
            newRow.Payment = Payment;

            bool result = logic.UpdateService(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.ServicesRow updated = logic.getServiceByID(id);
            // was updated correctly
            Assert.AreEqual(newRow.Date_Time, updated.Date_Time);
            Assert.AreEqual(newRow.id_Staff, updated.id_Staff);
            Assert.AreEqual(newRow.id_Customer, updated.id_Customer);
            Assert.AreEqual(newRow.id_Price, updated.id_Price);
            Assert.AreEqual(newRow.Payment, updated.Payment);
        }

        [TestCase("02.10.2024", 160, 122, 100, 8600, true)]
        public void DeleteServiceTest(string Date_Time, int id_Staff, int id_Customer, int id_Price, int Payment, bool expected)
        {
            myDataBaseDataSet.ServicesRow r = logic.NewServicesRow();
            r.Date_Time = Date_Time;
            r.id_Staff = id_Staff;
            r.id_Customer = id_Customer;
            r.id_Price = id_Price;
            r.Payment = Payment;

            bool wasAdded = logic.AddService(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.ServicesRow inserted = logic.getServiceByID(insertedId);
            // was added correctly
            Assert.AreEqual(Date_Time, inserted.Date_Time);
            Assert.AreEqual(id_Staff, inserted.id_Staff);
            Assert.AreEqual(id_Customer, inserted.id_Customer);
            Assert.AreEqual(id_Price, inserted.id_Price);
            Assert.AreEqual(Payment, inserted.Payment);

            bool actual = logic.DeleteServiceWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }

        [TestCase("02.10.2024", 160, 122, 100, 8600, true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string Date_Time, int id_Staff, int id_Customer, int id_Price, int Payment, bool expected)
        {
            myDataBaseDataSet.ServicesRow r = logic.NewServicesRow();
            r.Date_Time = Date_Time;
            r.id_Staff = id_Staff;
            r.id_Customer = id_Customer;
            r.id_Price = id_Price;
            r.Payment = Payment;

            bool actual = logic.AddService(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.ServicesRow inserted = logic.getServiceByID(id);
            // was added properly
            Assert.AreEqual(Date_Time, inserted.Date_Time);
            Assert.AreEqual(id_Staff, inserted.id_Staff);
            Assert.AreEqual(id_Customer, inserted.id_Customer);
            Assert.AreEqual(id_Price, inserted.id_Price);
            Assert.AreEqual(Payment, inserted.Payment);

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
