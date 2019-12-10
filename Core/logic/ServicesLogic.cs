using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ServicesLogic : AbstractLogic
    {
        public ServicesLogic(AbstractConnection c) : base(c)
        {
        }
      
        public myDataBaseDataSet getServices() {   
            return Cache;
        }

        public myDataBaseDataSet getServicesDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddService(myDataBaseDataSet.ServicesRow row)
        {
            return AddRecord(row);
        }

        public myDataBaseDataSet.ServicesRow NewServicesRow() {
            return (myDataBaseDataSet.ServicesRow) NewRow();
        }

        public bool DeleteServiceWithID(int id) {
            //getServiceByID(id).Delete();
            //return true;
            return DeleteRecordWithId(id);
        }

        public myDataBaseDataSet.ServicesRow getServiceByID(int id)
        {
            return (myDataBaseDataSet.ServicesRow) getRecordWithId(id);//return Cache.Services.FindByService_id(id);
        }

        public bool UpdateService(int id, myDataBaseDataSet.ServicesRow row) {
            /* myDataBaseDataSet.ServicesRow oldRow = getServiceByID(id);
            if (!row.IsNull("title"))
                oldRow.title = row.title;
            if (!row.IsNull("price"))
                oldRow.price = row.price;
            // eto prosto pizdec
            if (!row.IsNull("Customer_id"))
                oldRow.Customer_id = row.Customer_id;
            if (!row.IsNull("Price_id"))
                oldRow.Price_id = row.Price_id;
            if (!row.IsNull("Material_id"))
                oldRow.Material_id = row.Material_id;
            return true; */
            return UpdateRecord(id, row);
        }
        protected override AbstractDataAccessor provideWithAccessor()
        {
            return new ServicesAccessor();
        }
       
    }
}
