using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Restriction
    {
        Predicate<DataRow> predicate;
        myDataBaseDataSet dataSet;
        private Restriction(Predicate<DataRow> p, myDataBaseDataSet ds) {
            predicate = p;
            dataSet = ds;
        }
        public bool Validate(DataRow row) {
            return predicate.Invoke(row);
        }
        public static List<Restriction> getRestrictions(myDataBaseDataSet ds, string tableName) {
            List<Restriction> restrictions = new List<Restriction>();
            switch (tableName.ToLower()) {
                case "Services":
                    fillServicesRestrictions(ds, restrictions);
                        break;
                case "Customers":
                    //fillCustomersRestrictions(ds, restrictions);
                    break;
                default: break;

            }
            return restrictions;
        }

        private static void fillCustomersRestrictions(myDataBaseDataSet ds, List<Restriction> restrictions)
        {
            /*List<string> forbiddenUrls = new List<string> { "www.mail.ru" };
            restrictions.Add(new Restriction(row =>
            {
                myDataBaseDataSet.CustomersRow CustomersRow = (myDataBaseDataSet.CustomersRow)row;
                return !forbiddenUrls.Contains(CustomersRow.url);
            }, ds));*/
        }

        private static void fillServicesRestrictions(myDataBaseDataSet ds, List<Restriction> restrictions) {
            /*int ServicesLimit = 2;
            restrictions.Add(new Restriction(row => 
            {
                myDataBaseDataSet.ServicesRow ServicesRow = (myDataBaseDataSet.ServicesRow) row;
                return ds.Services.AsEnumerable().Where(r => r.Price_id == ServicesRow.Price_id).Count() <= ServicesLimit;
            }, ds));*/
        }
    }
}
