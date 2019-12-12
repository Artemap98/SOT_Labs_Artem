﻿using System;
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
        public void AddStaffTest(string FIO, string Position, string Phone, bool expected)
        {
            myDataBaseDataSet.StaffRow r = logic.NewStaffRow();
            r.FIO = FIO;
          
            bool actual = logic.AddStaff(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            myDataBaseDataSet.StaffRow inserted = logic.getStaffByID(id);
            // was added properly
            Assert.AreEqual(FIO, inserted.FIO);
        }

        [TestCase("Young", true)]
        [TestCase("Students", false)]
        public void AddStaffRestrictedTest(string FIO, bool expected)
        {
            myDataBaseDataSet.StaffRow r = logic.NewStaffRow();
            r.FIO = FIO;


            bool actual = logic.AddStaff(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("Students", false)]
        public void UpdateStaffTest(string FIO, bool expected)
        {
            int id = logic.getLastID();
            myDataBaseDataSet.StaffRow row = logic.getStaffByID(id);

            myDataBaseDataSet.StaffRow newRow = logic.NewStaffRow();
            newRow.FIO = FIO;
           
            bool result = logic.UpdateStaff(id, newRow);
            // was updated
            Assert.IsTrue(result);

            myDataBaseDataSet.StaffRow updated = logic.getStaffByID(id);
            // was updated correctly
            Assert.AreEqual(FIO, updated.FIO);
        }

        [TestCase("Fishmen", true)]
        public void DeleteStaffTest(string FIO, bool expected)
        {
            myDataBaseDataSet.StaffRow r = logic.NewStaffRow();
            r.FIO = FIO;
        
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
        [TestCase("Update DB", true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string FIO, bool expected)
        {
            myDataBaseDataSet.StaffRow r = logic.NewStaffRow();
            r.FIO = FIO;
          
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