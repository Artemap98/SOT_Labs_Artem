using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using Labi.RemoteServiceReference;

namespace Labi
{
    public partial class MainForm : Form
    {
        private ServiceSoapClient cli = new ServiceSoapClient();
        myDataBaseDataSet ServicesCache;
        myDataBaseDataSet PricesCache;
        myDataBaseDataSet StaffCache;
        myDataBaseDataSet CustomersCache;
        myDataBaseDataSet MaterialsCache;

        myDataBaseDataSet allTablesCache;

        public MainForm()  
        {            
            InitializeComponent();
          
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            ServicesCache = cli.GetAllServices();
            PricesCache = cli.GetAllPrices();
            StaffCache = cli.GetAllStaff();
            CustomersCache = cli.GetAllCustomers();
            MaterialsCache = cli.GetAllMaterials();
            //Merging
            allTablesCache = new myDataBaseDataSet();
            allTablesCache.Merge(ServicesCache);
            allTablesCache.Merge(PricesCache);
            allTablesCache.Merge(StaffCache);
            allTablesCache.Merge(CustomersCache);
            allTablesCache.Merge(MaterialsCache);

            ServicesGridView.DataSource = allTablesCache.Services;
            foreach (DataGridViewColumn column in ServicesGridView.Columns) {
                if (column.Name == "title") {
                    column.HeaderText = "Название";
                }
                else if (column.Name == "price")
                {
                    column.HeaderText = "Цена";
                } else { column.Visible = false; }
            }
            ServicesBindingSource.DataSource = ServicesGridView.DataSource;
            
            ServicesGridView.ReadOnly = true;
            ServicesBindingNavigator.BindingSource = ServicesBindingSource;


            MaterialsComboBox.DataSource = allTablesCache.Materials;
            MaterialsComboBox.DisplayMember = "name";
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

            // DEPRECATED! cli.UpdateWithCache(allTablesCache);
        }

        private void ServicesBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
