using AirportsNugetALL.Models;
using AirportsNugetALL.Services;
using NorthwindCustomersALL.Models;
using NorthwindCustomersALL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsNorthwind
{
    public partial class FormAirports : Form
    {
        public FormAirports()
        {
            InitializeComponent();
        }

        private async void btnLoadAirports_Click(object sender, EventArgs e)
        {
            ServiceAirports service = new ServiceAirports();
            AirportList list = await service.GetAirportListAsync();
            foreach (Airport a in list.Airports)
            {
                this.lstAirports.Items.Add(a.Name + " (" + a.IataCode + ") en " + a.Location.Address + " (" + a.Location.City.CountryRegion + ")");
            }
        }
    }
}
