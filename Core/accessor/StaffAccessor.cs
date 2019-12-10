using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class StaffAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] {"Fio","Position","Phone"};
        }

        protected override string id()
        {
           return "id";
        }

        protected override string tableName()
        {
            return "Staff";
        }
    }
}
