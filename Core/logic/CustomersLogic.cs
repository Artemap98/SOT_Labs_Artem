using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CustomersLogic : AbstractLogic
    {
        public CustomersLogic(AbstractConnection c) : base(c)
        {
        }
        public myDataBaseDataSet getCustomers()
        {
            return Cache;
        }

        public myDataBaseDataSet getCustomersDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddCustomer(myDataBaseDataSet.CustomersRow row)
        {
            return AddRecord(row);
        }
        public myDataBaseDataSet.CustomersRow NewCustomersRow()
        {
            return (myDataBaseDataSet.CustomersRow) NewRow();
        }
        public bool DeleteCustomerWithID(int id)
        {
            //getCustomerByID(id).Delete();
            //return true;
            return DeleteRecordWithId(id);
        }
        public myDataBaseDataSet.CustomersRow getCustomerByID(int id)
        {
            //return Cache.Customers.FindByCustomer_id(id);
            return (myDataBaseDataSet.CustomersRow) getRecordWithId(id);
        }
        public bool UpdateCustomer(int id, myDataBaseDataSet.CustomersRow row)
        {
            /*myDataBaseDataSet.CustomersRow oldRow = getCustomerByID(id);
            if (!row.IsNull("name"))
                oldRow.name = row.name;
            if (!row.IsNull("url"))
                oldRow.url = row.url;
            return true;*/
            return UpdateRecord(id, row);
        }
        protected override AbstractDataAccessor provideWithAccessor()
        {
            return new CustomersAccessor();
        }
    }
}
