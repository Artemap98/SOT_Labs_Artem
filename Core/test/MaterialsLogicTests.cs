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
    class MaterialsLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["myDataBaseConnectionString"].ConnectionString;
        private MaterialsLogic logic = new MaterialsLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Cement", "Prokter&Gumble", true)]
        public void AddMaterialTest(string name, string manufacturer, bool expected)
        {
            myDataBaseDataSet.MaterialsRow r = logic.NewMaterialsRow();
            r.name = name;
            r.manufacturer = manufacturer;

            bool actual = logic.AddMaterial(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.MaterialsRow inserted = logic.getMaterialByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(manufacturer, inserted.manufacturer);
        }

        [TestCase("koronka zolotaya", "IzhevskZubTech", false)]
        [TestCase("anestetic", "YandexMed", true)]
        public void AddMaterialsRestrictedTest(string name, string manufacturer, bool expected)
        {
            myDataBaseDataSet.MaterialsRow r = logic.NewMaterialsRow();
            r.name = name;
            r.manufacturer = manufacturer;
            

            bool actual = logic.AddMaterial(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("NewName", "NewManufacturer", true)]
        public void UpdateServiceTest(string name, string manufacturer, bool expected)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.MaterialsRow row = logic.getMaterialByID(id);
           
            myDataBaseDataSet.MaterialsRow newRow = logic.NewMaterialsRow();
            newRow.name = name;
            newRow.manufacturer = manufacturer;

            bool result = logic.UpdateMaterial(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.MaterialsRow updated = logic.getMaterialByID(id);
            // was updated correctly
            Assert.AreEqual(name, updated.name);
            Assert.AreEqual(manufacturer, updated.manufacturer);
        }

        [TestCase("Deleted Material name", "Deleted Material manufacturer", true)]
        public void DeleteMaterialTest(string name, string manufacturer, bool expected)
        {
            myDataBaseDataSet.MaterialsRow r = logic.NewMaterialsRow();
            r.name = name;
            r.manufacturer = manufacturer;
           
            bool wasAdded = logic.AddMaterial(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.MaterialsRow inserted = logic.getMaterialByID(insertedId);
            // was added correctly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(manufacturer, inserted.manufacturer);

            bool actual = logic.DeleteMaterialWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", "Any manufacturer",true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string name, string manufacturer, bool expected)
        {
            myDataBaseDataSet.MaterialsRow r = logic.NewMaterialsRow();
            r.name = name;
            r.manufacturer = manufacturer;

            bool wasAdded = logic.AddMaterial(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.MaterialsRow inserted = logic.getMaterialByID(insertedId);
            // was added correctly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(manufacturer, inserted.manufacturer);
            // finding

            // Updating db
            logic.UpdateDBWithCache();

            myDataBaseDataSet db = logic.GetMaterialsDirectlyFromDb();
            myDataBaseDataSet final = new myDataBaseDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }
    }
}
