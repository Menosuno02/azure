using NorthwindCustomersALL.Models;
using NorthwindCustomersALL.Services;

namespace WinFormsNorthwind
{
    public partial class FormNorthwind : Form
    {
        public FormNorthwind()
        {
            InitializeComponent();
        }

        private async void btnCustomers_Click(object sender, EventArgs e)
        {
            ServiceNorthwind service = new ServiceNorthwind();
            CustomersList list = await service.GetCustomersListAsync();
            foreach (Customer c in list.Customers)
            {
                this.lstCustomers.Items.Add(c.Name);
            }
        }
    }
}
