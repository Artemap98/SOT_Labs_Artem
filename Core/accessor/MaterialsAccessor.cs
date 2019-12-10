using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class MaterialsAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] { "name", "manufacturer" };
        }

        protected override string id()
        {
            return "id";
        }

        protected override string tableName()
        {
            return "Materials";
        }
    }
}
