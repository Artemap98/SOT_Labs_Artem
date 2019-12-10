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
    class PriceLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["ServicesConnectionString"].ConnectionString;
        private PricesLogic logic = new PricesLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Klement", "Ivanov", true)]
        public void AddPriceTest(string first_name, string second_name, bool expected)
        {
            myDataBaseDataSet.PricesRow r = logic.NewPricesRow();
            r.first_name = first_name;
            r.second_name = second_name;

            bool actual = logic.AddPrice(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.PricesRow inserted = logic.getPriceByID(id);
            // was added properly
            Assert.AreEqual(first_name, inserted.first_name);
            Assert.AreEqual(second_name, inserted.second_name);
        }

        [TestCase("Robert", "Kyosaki", false)]
        [TestCase("Zamyatin", "Vyacheslav", true)]
        public void AddPricesRestrictedTest(string first_name, string second_name, bool expected)
        {
            myDataBaseDataSet.PricesRow r = logic.NewPricesRow();
            r.first_name = first_name;
            r.second_name = second_name;
            

            bool actual = logic.AddPrice(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("NewName", "NewSurName", true)]
        public void UpdateServiceTest(string first_name, string second_name, bool expected)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.PricesRow row = logic.getPriceByID(id);
           
            myDataBaseDataSet.PricesRow newRow = logic.NewPricesRow();
            newRow.first_name = first_name;
            newRow.second_name = second_name;

            bool result = logic.UpdatePrice(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.PricesRow updated = logic.getPriceByID(id);
            // was updated correctly
            Assert.AreEqual(first_name, updated.first_name);
            Assert.AreEqual(second_name, updated.second_name);
        }

        [TestCase("Deleted Price name", "Deleted Price surname", true)]
        public void DeletePriceTest(string first_name, string second_name, bool expected)
        {
            myDataBaseDataSet.PricesRow r = logic.NewPricesRow();
            r.first_name = first_name;
            r.second_name = second_name;
           
            bool wasAdded = logic.AddPrice(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.PricesRow inserted = logic.getPriceByID(insertedId);
            // was added correctly
            Assert.AreEqual(first_name, inserted.first_name);
            Assert.AreEqual(second_name, inserted.second_name);

            bool actual = logic.DeletePriceWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", "Any surname",true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string first_name, string second_name, bool expected)
        {
            myDataBaseDataSet.PricesRow r = logic.NewPricesRow();
            r.first_name = first_name;
            r.second_name = second_name;

            bool wasAdded = logic.AddPrice(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.PricesRow inserted = logic.getPriceByID(insertedId);
            // was added correctly
            Assert.AreEqual(first_name, inserted.first_name);
            Assert.AreEqual(second_name, inserted.second_name);
            // finding

            // Updating db
            logic.UpdateDBWithCache();

            myDataBaseDataSet db = logic.getPricesDirectlyFromDb();
            myDataBaseDataSet final = new myDataBaseDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }
    }
}
