using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class StaffLogic : AbstractLogic
    {
        public StaffLogic(AbstractConnection c) : base(c)
        {
        }

        public myDataBaseDataSet GetStaff()
        {
            return Cache;
        }

        public myDataBaseDataSet GetStaffDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddStaff(myDataBaseDataSet.StaffRow row)
        {
            return AddRecord(row);
        }

        public myDataBaseDataSet.StaffRow NewStaffRow()
        {
            return (myDataBaseDataSet.StaffRow)NewRow();
        }

        public bool DeleteStaffWithID(int id)
        {
           // getStaffByID(id).Delete();
           // return true;
            return DeleteRecordWithId(id);
        }

        public myDataBaseDataSet.StaffRow getStaffByID(int id)
        {
            return (myDataBaseDataSet.StaffRow) getRecordWithId(id);
            //return (myDataBaseDataSet.StaffRow)Cache.Tables[Accessor.TableName].Rows.Find("" + id);// FindByStaff_id(id);
        }

        public bool UpdateStaff(int id, myDataBaseDataSet.StaffRow row)
        {
            /*myDataBaseDataSet.StaffRow oldRow = getStaffByID(id);
            if (!row.IsNull("name"))
                oldRow.name = row.name;
            return true;*/
            return UpdateRecord(id, row);
        }


        protected override AbstractDataAccessor provideWithAccessor()
        {
           return new StaffAccessor();
        }
    }
}
