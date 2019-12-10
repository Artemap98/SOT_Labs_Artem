using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class CustomersAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] { "FIO", "Phone"};
        }

        protected override string id()
        {
           return "id";
        }

        protected override string tableName()
        {
            return "Customers";
        }
    }
}
