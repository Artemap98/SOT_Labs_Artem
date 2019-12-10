using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ServicesAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] {"Date_time", "id_Staff", "id_Customer", "id_Price", "Payment"};
        }

        protected override string tableName()
        {
            return "Services";
        }
        protected override string id()
        {
            return "id";
        }
    }
}
