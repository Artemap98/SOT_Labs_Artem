using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PricesLogic : AbstractLogic
    {
        public PricesLogic(AbstractConnection c) : base(c)
        {
        }
        public myDataBaseDataSet getPrices()
        {
            return Cache;
        }

        public myDataBaseDataSet getPricesDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddPrice(myDataBaseDataSet.PricesRow row)
        {
            return AddRecord(row);
        }

        public myDataBaseDataSet.PricesRow NewPricesRow()
        {
            return (myDataBaseDataSet.PricesRow) NewRow();
        }

        public bool DeletePriceWithID(int id)
        {
            //getPriceByID(id).Delete();
            return DeleteRecordWithId(id);
        }

        public myDataBaseDataSet.PricesRow getPriceByID(int id)
        {
            //return Cache.Prices.FindByPrice_id(id);
            return (myDataBaseDataSet.PricesRow) getRecordWithId(id);
        }

        public bool UpdatePrice(int id, myDataBaseDataSet.PricesRow row)
        {
            /*myDataBaseDataSet.PricesRow oldRow = getPriceByID(id);
            if (!row.IsNull("first_name"))
                oldRow.first_name = row.first_name;
            if (!row.IsNull("second_name"))
                oldRow.second_name = row.second_name;
            return true;*/
            return UpdateRecord(id, row);
        }

        protected override AbstractDataAccessor provideWithAccessor()
        {
           return new PricesAccessor();
        }
    }
}
