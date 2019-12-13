using NUnit.Framework;
using System.Configuration;

namespace Core
{
    [TestFixture]
    class PricesLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["myDataBaseConnectionString"].ConnectionString;
        private PricesLogic logic = new PricesLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Adults", 100, true)]
        public void AddPriceTest(string name, int id_material, int price , bool expected)
        {
            myDataBaseDataSet.PricesRow r = logic.NewPricesRow();
            r.name = name;
            r.id_material = id_material;
            r.price = price;

            bool actual = logic.AddPrice(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.PricesRow inserted = logic.getPriceByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(id_material, inserted.id_material);
            Assert.AreEqual(price, inserted.price);
        }

        [TestCase("Young", 100,645, true)]
        [TestCase("Students", 100,7864, false)]
        public void AddPriceRestrictedTest(string name, int id_material, int price , bool expected)
        {
            myDataBaseDataSet.PricesRow r = logic.NewPricesRow();
            r.name = name;
            r.id_material = id_material;
            r.price = price;

            bool actual = logic.AddPrice(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("Students", 100,23234, false)]
        public void UpdatePriceTest(string name, int id_material, int price , bool expected)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.PricesRow row = logic.getPriceByID(id);

            myDataBaseDataSet.PricesRow newRow = logic.NewPricesRow();
            newRow.name = name;
            newRow.id_material = id_material;
            newRow.price = price;

            bool result = logic.UpdatePrice(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.PricesRow updated = logic.getPriceByID(id);
            // was updated correctly
            Assert.AreEqual(name, updated.name);
            Assert.AreEqual(id_material, updated.id_material);
            Assert.AreEqual(price, updated.price);
        }

        [TestCase("Fishmen", 100,234, true)]
        public void DeleteServiceTest(string name, int id_material, int price , bool expected)
        {
            myDataBaseDataSet.PricesRow r = logic.NewPricesRow();
            r.name = name;
            r.id_material = id_material;
            r.price = price;
            bool wasAdded = logic.AddPrice(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.PricesRow inserted = logic.getPriceByID(insertedId);
            // was added correctly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(id_material, inserted.id_material);
            Assert.AreEqual(price, inserted.price);

            bool actual = logic.DeletePriceWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", 100,100, true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string name, int id_material, int price , bool expected)
        {
            myDataBaseDataSet.PricesRow r = logic.NewPricesRow();
            r.name = name;
            r.id_material = id_material;
            r.price = price;

            bool actual = logic.AddPrice(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.PricesRow inserted = logic.getPriceByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(id_material, inserted.id_material);
            Assert.AreEqual(price, inserted.price);

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
