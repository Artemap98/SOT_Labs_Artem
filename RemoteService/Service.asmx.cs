using Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RemoteService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        private ServicesLogic ServicesLogic;
        private StaffLogic StaffLogic;
        private PricesLogic PricesLogic;
        private MaterialsLogic MaterialsLogic;
        private CustomersLogic CustomersLogic;
        public Service()
        {
            string cns = ConfigurationManager.ConnectionStrings["ServicesConnectionString"].ConnectionString;
            Helper.console(cns);
            AbstractConnection c = ConnectionFactory.getConnection(DataProvider.SqlServer, cns);

            ServicesLogic = new ServicesLogic(c);
            StaffLogic = new StaffLogic(c);
            PricesLogic = new PricesLogic(c);
            MaterialsLogic = new MaterialsLogic(c);
            CustomersLogic = new CustomersLogic(c);
        }
        private myDataBaseDataSet wrapWithDataSet(DataRow row) {
            myDataBaseDataSet output = new myDataBaseDataSet();
            output.Tables[row.Table.TableName].ImportRow(row);
            return output;
        }
        // START ServiceS
        [WebMethod]
        public myDataBaseDataSet GetAllServices()
        {
            return ServicesLogic.getServices();
        }
        [WebMethod]
        public myDataBaseDataSet GetAllServicesDirectlyFromDB()
        {
            return ServicesLogic.getServicesDirectlyFromDb();
        }
        [WebMethod]
        public myDataBaseDataSet NewServicesRow()
        {
            return wrapWithDataSet(ServicesLogic.NewServicesRow());  
        }
        [WebMethod]
        public bool DeleteServiceWithID(int id)
        {
            return ServicesLogic.DeleteServiceWithID(id);
        }
        [WebMethod]
        public myDataBaseDataSet getServiceByID(int id)
        {
            return wrapWithDataSet(ServicesLogic.getServiceByID(id));
        }
        [WebMethod]
        public bool AddService(myDataBaseDataSet row)
        {
            return ServicesLogic.AddService(row.Services[0]);
        }
        [WebMethod]
        public bool UpdateService(int id, myDataBaseDataSet row)
        {
            return ServicesLogic.UpdateService(id, row.Services[0]);
        }
        [WebMethod]
        public bool UpdateServicesTable()
        {
            return ServicesLogic.UpdateDBWithCache();
        }

        // END ServiceS

        // START PriceS
        [WebMethod]
        public myDataBaseDataSet GetAllPrices()
        {
            return PricesLogic.getPrices();
        }
        [WebMethod]
        public myDataBaseDataSet GetAllPricesDirectlyFromDB()
        {
            return PricesLogic.getPricesDirectlyFromDb();
        }
        [WebMethod]
        public myDataBaseDataSet NewPricesRow()
        {
            return wrapWithDataSet( PricesLogic.NewPricesRow());
        }
        [WebMethod]
        public bool DeletePriceWithID(int id)
        {
            return PricesLogic.DeletePriceWithID(id);
        }
        [WebMethod]
        public myDataBaseDataSet getPriceByID(int id)
        {
            return wrapWithDataSet( PricesLogic.getPriceByID(id));
        }
        [WebMethod]
        public bool UpdatePrice(int id, myDataBaseDataSet row)
        {
            return PricesLogic.UpdatePrice(id, row.Prices[0]);
        }
        [WebMethod]
        public bool UpdatePricesTable()
        {
            return PricesLogic.UpdateDBWithCache();
        }
        // END PriceS

        // START CustomerS
        [WebMethod]
        public myDataBaseDataSet GetAllCustomers()
        {
            return CustomersLogic.getCustomers();
        }
        [WebMethod]
        public myDataBaseDataSet GetAllCustomersDirectlyFromDB()
        {
            return CustomersLogic.getCustomersDirectlyFromDb();
        }
        [WebMethod]
        public myDataBaseDataSet NewCustomersRow()
        {
            return wrapWithDataSet( CustomersLogic.NewCustomersRow());
        }
        [WebMethod]
        public bool DeleteCustomerWithID(int id)
        {
            return CustomersLogic.DeleteCustomerWithID(id);
        }
        [WebMethod]
        public myDataBaseDataSet getCustomerByID(int id)
        {
            return wrapWithDataSet( CustomersLogic.getCustomerByID(id));
        }
        [WebMethod]
        public bool UpdateCustomer(int id, myDataBaseDataSet row)
        {
            return CustomersLogic.UpdateCustomer(id, row.Customers[0]);
        }
        [WebMethod]
        public bool UpdateCustomersTable()
        {
            return CustomersLogic.UpdateDBWithCache();
        }
        // END CustomerS

        // START Staff
        [WebMethod]
        public myDataBaseDataSet GetAllStaff()
        {
            return StaffLogic.GetStaff();
        }
        [WebMethod]
        public myDataBaseDataSet GetAllAuditorsDirectlyFromDB()
        {
            return StaffLogic.GetStaffDirectlyFromDb();
        }
        [WebMethod]
        public myDataBaseDataSet NewStaffRow()
        {
            return wrapWithDataSet( StaffLogic.NewStaffRow());
        }
        [WebMethod]
        public bool DeleteStaffWithID(int id)
        {
            return StaffLogic.DeleteStaffWithID(id);
        }
        [WebMethod]
        public myDataBaseDataSet getStaffById(int id)
        {
            return wrapWithDataSet( StaffLogic.getStaffByID(id));
        }
        [WebMethod]
        public bool UpdateStaff(int id, myDataBaseDataSet row)
        {
            return StaffLogic.UpdateStaff(id, row.Staff[0]);
        }
        [WebMethod]
        public bool UpdateStaffTable()
        {
            return StaffLogic.UpdateDBWithCache();
        }
        // END Staff

        // START Materials
        [WebMethod]
        public myDataBaseDataSet GetAllMaterials()
        {
            return MaterialsLogic.GetMaterials();
        }
        [WebMethod]
        public myDataBaseDataSet GetAllMaterialsDirectlyFromDB()
        {
            return MaterialsLogic.GetMaterialsDirectlyFromDb();
        }
        [WebMethod]
        public myDataBaseDataSet NewMaterialsRow()
        {
            return wrapWithDataSet( MaterialsLogic.NewMaterialsRow());
        }
        [WebMethod]
        public bool DeleteMaterialWithID(int id)
        {
            return MaterialsLogic.DeleteMaterialWithID(id);
        }
        [WebMethod]
        public myDataBaseDataSet getMaterialById(int id)
        {
            return wrapWithDataSet(MaterialsLogic.getMaterialByID(id));
        }
        [WebMethod]
        public bool UpdateMaterial(int id, myDataBaseDataSet row)
        {
            return MaterialsLogic.UpdateMaterial(id, row.Materials[0]);
        }
        [WebMethod]
        public bool UpdateMaterialsTable()
        {
            return MaterialsLogic.UpdateDBWithCache();
        }
        // END Materials

        //OTHER
        [WebMethod]
        public bool UpdateWithCache(myDataBaseDataSet cache)
        {
            return MaterialsLogic.UpdateDBWithCache(cache);
        }
    }
}
