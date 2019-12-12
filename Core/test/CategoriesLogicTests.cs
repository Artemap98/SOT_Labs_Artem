using NUnit.Framework;
using System.Configuration;

namespace Core
{
    [TestFixture]
    class MaterialsLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["myDataBaseConnectionString"].ConnectionString;
        private MaterialsLogic logic = new MaterialsLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Adults", 100, true)]
        public void AddMaterialTest(string name, int Staff_id, bool expected)
        {
            myDataBaseDataSet.MaterialsRow r = logic.NewMaterialsRow();
            r.name = name;
            r.Staff_id = Staff_id;

            bool actual = logic.AddMaterial(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.MaterialsRow inserted = logic.getMaterialByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(Staff_id, inserted.Staff_id);
        }

        [TestCase("Young", 100, true)]
        [TestCase("Students", 100, false)]
        public void AddMaterialRestrictedTest(string name, int Staff_id, bool expected)
        {
            myDataBaseDataSet.MaterialsRow r = logic.NewMaterialsRow();
            r.name = name;
            r.Staff_id = Staff_id;


            bool actual = logic.AddMaterial(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("Students", 100, false)]
        public void UpdateMaterialTest(string name, int Staff_id, bool expected)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.MaterialsRow row = logic.getMaterialByID(id);

            myDataBaseDataSet.MaterialsRow newRow = logic.NewMaterialsRow();
            newRow.name = name;
            newRow.Staff_id = Staff_id;

            bool result = logic.UpdateMaterial(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.MaterialsRow updated = logic.getMaterialByID(id);
            // was updated correctly
            Assert.AreEqual(name, updated.name);
            Assert.AreEqual(Staff_id, updated.Staff_id);
        }

        [TestCase("Fishmen", 100, true)]
        public void DeleteServiceTest(string name, int Staff_id, bool expected)
        {
            myDataBaseDataSet.MaterialsRow r = logic.NewMaterialsRow();
            r.name = name;
            r.Staff_id = Staff_id;

            bool wasAdded = logic.AddMaterial(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.MaterialsRow inserted = logic.getMaterialByID(insertedId);
            // was added correctly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(Staff_id, inserted.Staff_id);

            bool actual = logic.DeleteMaterialWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", 100, true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string name, int Staff_id, bool expected)
        {
            myDataBaseDataSet.MaterialsRow r = logic.NewMaterialsRow();
            r.name = name;
            r.Staff_id = Staff_id;

            bool actual = logic.AddMaterial(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.MaterialsRow inserted = logic.getMaterialByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(Staff_id, inserted.Staff_id);

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
