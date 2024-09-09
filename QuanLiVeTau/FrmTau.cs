using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiVeTau
{
    public partial class FrmTau : Form
    {
        public FrmTau()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmGaTau form1 = new FrmGaTau();
            form1.Show();
        }
        KetnoiCSDL kn = new KetnoiCSDL();   
        public void LayDuLieu()
        {
            DataTable dta = new DataTable();
            dta = kn.LayBang("select SOHIEU,SOTOA,SOGHE from TAU");
            dataGridViewTAU.DataSource = dta;
        }

        private void FrmTau_Load(object sender, EventArgs e)
        {
            LayDuLieu();
        }
    }
}
