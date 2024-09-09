using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiVeTau
{
    public partial class FrmChuyenTau : Form
    {
        public FrmChuyenTau()
        {
            InitializeComponent();
            dataGridViewCHUYENTAU.SelectionChanged += dataGridViewCHUYENTAU_SelectionChanged;
            AttachButtonHandlers();

        }
        private void dataGridViewCHUYENTAU_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCHUYENTAU.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewCHUYENTAU.SelectedRows[0];
                textBoxMACT.Text = row.Cells["MACT"].Value.ToString();
                comboBoxGAKHOIHANH.Text = row.Cells["GAKHOIHANH"].Value.ToString();
                comboBoxGADEN.Text = row.Cells["GADEN"].Value.ToString();
                dateTimePicker1.Text = row.Cells["GIODI"].Value.ToString();
                dateTimePicker2.Text = row.Cells["GIODEN"].Value.ToString();
                textBoxSOGHECON.Text = row.Cells["SOGHECON"].Value.ToString();
                textBoxTRANGTHAI.Text = row.Cells["TRANGTHAI"].Value.ToString();
                comboBoxMATAU.Text = row.Cells["MATAU"].Value.ToString();
            }
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }
        public void Lay_DL_Bang_GADI()
        {
            DataTable dt = kn.LayBang("SELECT GAKHOIHANH from CHUYENTAU");
            comboBoxGAKHOIHANH.DataSource = dt;
            comboBoxGAKHOIHANH.DisplayMember = "GAKHOIHANH";
            comboBoxGAKHOIHANH.ValueMember = "GAKHOIHANH";

        }
        public void Lay_DL_Bang_GADEN()
        {
            DataTable dt = kn.LayBang("SELECT GADEN from CHUYENTAU");
            comboBoxGADEN.DataSource = dt;
            comboBoxGADEN.DisplayMember = "GADEN";
            comboBoxGADEN.ValueMember = "GADEN";

        }
        public void Lay_DL_Bang_MATAU()
        {
            DataTable dt = kn.LayBang("SELECT SOHIEU from TAU");
            comboBoxMATAU.DataSource = dt;
            comboBoxMATAU.DisplayMember = "SOHIEU";
            comboBoxMATAU.ValueMember = "SOHIEU";

        }
        private void buttonTaoMoi_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonExit_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        KetnoiCSDL kn = new KetnoiCSDL();
        private void buttonThem_Click(object sender, EventArgs e)
        {
            string MACT = textBoxMACT.Text;
            string GAKHOIHANH = comboBoxGAKHOIHANH.Text;
            string GADEN = comboBoxGADEN.Text;
            DateTime GIODI = dateTimePicker1.Value;
            DateTime GIODEN = dateTimePicker2.Value;
            string SOGHECON = textBoxSOGHECON.Text;
            string TRANGTHAI = textBoxTRANGTHAI.Text;
            string MATAU = comboBoxMATAU.Text;

            kn.KetNoi_Dulieu();

            string sql_insert = "INSERT INTO CHUYENTAU (MACT, GAKHOIHANH, GADEN, GIODI, GIODEN, TRANGTHAI, SOGHECON, MATAU) VALUES (@MACT, @GAKHOIHANH, @GADEN, @GIODI, @GIODEN, @TRANGTHAI, @SOGHECON, @MATAU)";

            using (SqlCommand cmd = new SqlCommand(sql_insert, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@MACT", MACT);
                cmd.Parameters.AddWithValue("@GAKHOIHANH", GAKHOIHANH);
                cmd.Parameters.AddWithValue("@GADEN", GADEN);
                cmd.Parameters.AddWithValue("@GIODI", GIODI);
                cmd.Parameters.AddWithValue("@GIODEN", GIODEN);
                cmd.Parameters.AddWithValue("@TRANGTHAI", TRANGTHAI);
                cmd.Parameters.AddWithValue("@SOGHECON", SOGHECON);
                cmd.Parameters.AddWithValue("@MATAU", MATAU);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Dữ liệu đã được thêm thành công !");
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào được thêm !");
                }
            }
            LayDuLieu();
        }

        private void FrmChuyenTau_Load(object sender, EventArgs e)
        {
            LayDuLieu();
            Lay_DL_Bang_GADEN();
            Lay_DL_Bang_GADI();
            Lay_DL_Bang_MATAU();
        }
        public void LayDuLieu()
        {
            DataTable dta = new DataTable();
            dta = kn.LayBang("select MACT,GAKHOIHANH,GADEN,GIODI,GIODEN,TRANGTHAI,SOGHECON,MATAU from CHUYENTAU");
            dataGridViewCHUYENTAU.DataSource = dta;
            HienThiDuLieu();
        }
        public void HienThiDuLieu()
        {
            DataTable dta = kn.LayBang("select MACT,GAKHOIHANH,GADEN,GIODEN,GIODI,TRANGTHAI,SOGHECON,MATAU from CHUYENTAU");
            dataGridViewCHUYENTAU.DataSource = dta;

            textBoxMACT.DataBindings.Clear();
            textBoxMACT.DataBindings.Add("Text", dta, "MACT");

            comboBoxGAKHOIHANH.DataBindings.Clear();
            comboBoxGAKHOIHANH.DataBindings.Add("Text", dta, "GAKHOIHANH");

            comboBoxGADEN.DataBindings.Clear();
            comboBoxGADEN.DataBindings.Add("Text", dta, "GADEN");

            dateTimePicker1.DataBindings.Clear();
            dateTimePicker1.DataBindings.Add("Text", dta, "GIODI");

            textBoxSOGHECON.DataBindings.Clear();
            textBoxSOGHECON.DataBindings.Add("Text", dta, "SOGHECON");

            textBoxTRANGTHAI.DataBindings.Clear();
            textBoxTRANGTHAI.DataBindings.Add("Text", dta, "TRANGTHAI");

            comboBoxMATAU.DataBindings.Clear();
            comboBoxMATAU.DataBindings.Add("Text", dta, "MATAU");

            dateTimePicker2.DataBindings.Clear();
            dateTimePicker2.DataBindings.Add("Text", dta, "GIODEN");

        }

        private void buttonTao_Click(object sender, EventArgs e)
        {
            textBoxMACT.Text = "";
            comboBoxGAKHOIHANH.Text = "";
            comboBoxGADEN.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            textBoxTRANGTHAI.Text = "";
            textBoxSOGHECON.Text = "";
            comboBoxMATAU.Text = "";
            textBoxMACT.Focus();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            String sqlxoa = "delete from CHUYENTAU where MACT = '" + textBoxMACT.Text + "'";
            kn.ThucThiDuLieu(sqlxoa);
            LayDuLieu();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            string MACT = textBoxMACT.Text;
            string GAKHOIHANH = comboBoxGAKHOIHANH.Text;
            string GADEN = comboBoxGADEN.Text;
            DateTime GIODI = dateTimePicker1.Value;
            DateTime GIODEN = dateTimePicker2.Value;
            string TRANGTHAI = textBoxTRANGTHAI.Text;
            string SOGHECON = textBoxSOGHECON.Text;
            string MATAU = comboBoxMATAU.Text;

            kn.KetNoi_Dulieu();

            string sql_update = "UPDATE CHUYENTAU SET GAKHOIHANH = @GAKHOIHANH, GADEN = @GADEN, GIODI = @GIODI, TRANGTHAI = @TRANGTHAI, GIODEN = @GIODEN, SOGHECON = @SOGHECON, MATAU = @MATAU WHERE MACT = @MACT";

            using (SqlCommand cmd = new SqlCommand(sql_update, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@MACT", MACT);
                cmd.Parameters.AddWithValue("@GAKHOIHANH", GAKHOIHANH);
                cmd.Parameters.AddWithValue("@GADEN", GADEN);
                cmd.Parameters.AddWithValue("@GIODI", GIODI);
                cmd.Parameters.AddWithValue("@GIODEN", GIODEN);
                cmd.Parameters.AddWithValue("@TRANGTHAI", TRANGTHAI);
                cmd.Parameters.AddWithValue("@SOGHECON", SOGHECON);
                cmd.Parameters.AddWithValue("@MATAU", MATAU);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Dữ liệu đã được cập nhật thành công !");
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào được cập nhật !");
                }
            }
            LayDuLieu();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            kn.KetNoi_Dulieu();
            string keyword = textBoxTimKiem.Text;

            string sql_search = "SELECT * FROM CHUYENTAU WHERE MACT LIKE @keyword OR GAKHOIHANH LIKE @keyword OR GADEN LIKE @keyword OR MATAU LIKE @keyword";

            using (SqlCommand cmd = new SqlCommand(sql_search, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        // Hiển thị dữ liệu trong dataGridView
                        dataGridViewCHUYENTAU.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi nào phù hợp !");
                    }
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Form formToShow = null;
                switch (button.Name)
                {
                    case "buttonNhanVien":
                        formToShow = new FrmNhanVien();
                        break;
                    case "buttonGaTau":
                        formToShow = new FrmGaTau();
                        break;
                    case "buttonKhachHang":
                        formToShow = new FrmKhachHang();
                        break;
                    case "buttonDatCho":
                        formToShow = new FrmDatCho();
                        break;
                    case "buttonVetau":
                        formToShow = new FrmVeTau();
                        break;
                    case "buttonThanhToan":
                        formToShow = new FrmThanhToan();
                        break;
                    default:
                        // Handle other buttons if needed
                        break;
                }

                if (formToShow != null)
                {
                    formToShow.Show();
                    this.Hide();
                }
            }
        }

        // Attach Button_Click event handler to all buttons
        private void AttachButtonHandlers()
        {
            foreach (Control control in Controls)
            {
                if (control is Button)
                {
                    control.Click += Button_Click;
                }
            }
        }
    }

}
