using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PricesAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] { "name", "price", "id_material" };
        }

        protected override string id()
        {
            return "id";
        }

        protected override string tableName()
        {
            return "Prices";
        }
    }
}
