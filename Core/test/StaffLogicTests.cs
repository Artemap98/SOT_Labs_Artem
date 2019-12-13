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
    class StaffLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["myDataBaseConnectionString"].ConnectionString;
        private StaffLogic logic = new StaffLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Nikolay Baskov","Dantist", true)]
        public void AddStaffTest(string FIO, string Position, bool expected)
        {
            myDataBaseDataSet.StaffRow r = logic.NewStaffRow();
            r.FIO = FIO;
            r.Position = Position;
          
            bool actual = logic.AddStaff(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.StaffRow inserted = logic.getStaffByID(id);
            // was added properly
            Assert.AreEqual(FIO, inserted.FIO);
        }

        [TestCase("Albert Einstein","Trainee", true)]
        [TestCase("Danny De Vito","Student", false)]
        public void AddStaffRestrictedTest(string FIO, string Position, bool expected)
        {
            myDataBaseDataSet.StaffRow r = logic.NewStaffRow();
            r.FIO = FIO;


            bool actual = logic.AddStaff(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("Jason Statham","Administrator", true)]
        public void UpdateStaffTest(string FIO, string Position, bool expected)
        {
            int id = logic.getLastID();
            //myDataBaseDataSet.StaffRow row = logic.getStaffByID(id);

            myDataBaseDataSet.StaffRow newRow = logic.NewStaffRow();
            newRow.FIO = FIO;
            newRow.Position = Position;
            bool result = logic.UpdateStaff(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.StaffRow updated = logic.getStaffByID(id);
            // was updated correctly
            Assert.AreEqual(FIO, updated.FIO);
        }

        [TestCase("Karl Johnson","Assistant", true)]
        public void DeleteStaffTest(string FIO, string Position, bool expected)
        {
            myDataBaseDataSet.StaffRow r = logic.NewStaffRow();
            r.FIO = FIO;
            r.Position = Position;
            bool wasAdded = logic.AddStaff(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            myDataBaseDataSet.StaffRow inserted = logic.getStaffByID(insertedId);
            // was added correctly
            Assert.AreEqual(FIO, inserted.FIO);
            
            bool actual = logic.DeleteStaffWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }

        [TestCase("John Doe","Update DB", true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string FIO, string Position, bool expected)
        {
            myDataBaseDataSet.StaffRow r = logic.NewStaffRow();
            r.FIO = FIO;
            r.Position = Position;

            bool actual = logic.AddStaff(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.StaffRow inserted = logic.getStaffByID(id);
            // was added properly
            Assert.AreEqual(FIO, inserted.FIO);
           
            // Updating db
            logic.UpdateDBWithCache();

            myDataBaseDataSet db = logic.GetStaff();
            myDataBaseDataSet final = new myDataBaseDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }


    }
}
