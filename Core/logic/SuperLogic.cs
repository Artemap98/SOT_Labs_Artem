using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class SuperLogic
    {
        private AbstractConnection _c;
        public AbstractConnection Connection
        {
            get
            {
                return _c;
            }
        }

        private ServicesLogic _ServicesLogic;
        public ServicesLogic Services {
            get
            {
                if (_ServicesLogic == null)
                    _ServicesLogic = new ServicesLogic(Connection);
                return _ServicesLogic;
            }
        }
        private PricesLogic _PricesLogic;
        public PricesLogic Prices
        {
            get
            {
                if (_PricesLogic == null)
                    _PricesLogic = new PricesLogic(Connection);
                return _PricesLogic;
            }
        }
        private MaterialsLogic _MaterialsLogic;
        public MaterialsLogic Materials
        {
            get
            {
                if (_MaterialsLogic == null)
                    _MaterialsLogic = new MaterialsLogic(Connection);
                return _MaterialsLogic;
            }
        }
        private StaffLogic _StaffLogic;
        public StaffLogic Staff
        {
            get
            {
                if (_StaffLogic == null)
                    _StaffLogic = new StaffLogic(Connection);
                return _StaffLogic;
            }
        }
        private CustomersLogic _CustomersLogic;
        public CustomersLogic Customers
        {
            get
            {
                if (_CustomersLogic == null)
                    _CustomersLogic = new CustomersLogic(Connection);
                return _CustomersLogic;
            }
        }
    }
}
