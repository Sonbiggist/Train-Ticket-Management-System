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
    public partial class FrmThanhToan : Form
    {
        public FrmThanhToan()
        {
            InitializeComponent();
            AttachButtonHandlers();
            dataGridViewTHANHTOAN.SelectionChanged += dataGridViewTHANHTOAN_SelectionChanged;
        }
        private void dataGridViewTHANHTOAN_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewTHANHTOAN.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewTHANHTOAN.SelectedRows[0];
                textBoxMATT.Text = row.Cells["MATT"].Value.ToString();
                comboBoxCMND.Text = row.Cells["CMND"].Value.ToString();
                comboBoxMADCHO.Text = row.Cells["MADCHO"].Value.ToString();
                textBoxSOVEMUA.Text = row.Cells["SOLUONGVEMUA"].Value.ToString();
                dateTimePicker2.Text = row.Cells["NGAYTHANHTOAN"].Value.ToString();
                textBoxTHANHTIEN.Text = row.Cells["THANHTIEN"].Value.ToString();
                textBoxPHUONGTHUC.Text = row.Cells["PHUONGTHUC"].Value.ToString();

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
                    case "buttonKhachHang":
                        formToShow = new FrmKhachHang();
                        break;
                    case "buttonChuyenTau":
                        formToShow = new FrmChuyenTau();
                        break;
                    case "buttonDatCho":
                        formToShow = new FrmDatCho();
                        break;
                    case "buttonVetau":
                        formToShow = new FrmVeTau();
                        break;
                    case "buttonGaTau":
                        formToShow = new FrmGaTau();
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
        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            LayDuLieu();
            Lay_DL_Bang_CMND();
            Lay_DL_Bang_MADCHO();
        }

        private void textBoxPHUONGTHUC_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonTao_Click(object sender, EventArgs e)
        {
            textBoxMATT.Text = "";
            comboBoxCMND.Text = "";
            comboBoxMADCHO.Text = "";
            dateTimePicker2.Text = "";
            textBoxSOVEMUA.Text = "";
            textBoxTHANHTIEN.Text = "";
            textBoxPHUONGTHUC.Text = "";
            textBoxMATT.Focus();
        }
        public void Lay_DL_Bang_CMND()
        {
            DataTable dt = kn.LayBang("SELECT CMND FROM KHACHHANG");
            comboBoxCMND.DataSource = dt;
            comboBoxCMND.DisplayMember = "CMND";
            comboBoxCMND.ValueMember = "CMND";
        }
        public void Lay_DL_Bang_MADCHO()
        {
            DataTable dt = kn.LayBang("SELECT MADCHO FROM DATCHO");
            comboBoxMADCHO.DataSource = dt;
            comboBoxMADCHO.DisplayMember = "MADCHO";
            comboBoxMADCHO.ValueMember = "MADCHO";
            comboBoxMADCHO.SelectedIndexChanged += comboBoxMADCHO_SelectedIndexChanged;
            comboBoxMADCHO_SelectedIndexChanged(null, EventArgs.Empty);
        }
        private void comboBoxMADCHO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string MADCHO = comboBoxMADCHO.Text;
                string query = "SELECT SOLUONGVEMUA, THANHTIEN, CMND FROM DATCHO WHERE MADCHO = @madcho";
                using (SqlConnection connection = new SqlConnection(kn.cnn.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@madcho", MADCHO);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string cmnd = reader.GetString(2);
                            int soVeMua = reader.GetInt32(0);
                            double thanhTien = reader.GetDouble(1); // Lấy giá trị float từ cột THANHTIEN
                            textBoxSOVEMUA.Text = soVeMua.ToString();
                            textBoxTHANHTIEN.Text = thanhTien.ToString();
                            comboBoxCMND.Text = cmnd;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu cho MADCHO được chọn!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn CSDL: " + ex.Message);
            }
        }



        KetnoiCSDL kn = new KetnoiCSDL();   
        private void buttonXoa_Click(object sender, EventArgs e)
        {
            String sqlxoa = "delete from THANHTOAN where MATT = '" + textBoxMATT.Text + "'";
            kn.ThucThiDuLieu(sqlxoa);
            LayDuLieu();
        }


        public void LayDuLieu()
        {
            DataTable dta = new DataTable();
            dta = kn.LayBang("select MATT,CMND, MADCHO, SOLUONGVEMUA, THANHTIEN, NGAYTHANHTOAN, PHUONGTHUC from THANHTOAN");
            dataGridViewTHANHTOAN.DataSource = dta;
            HienThiDuLieu();
        }
        public void HienThiDuLieu()
        {
            DataTable dta = kn.LayBang("select MATT,CMND, MADCHO, SOLUONGVEMUA, THANHTIEN, NGAYTHANHTOAN, PHUONGTHUC from THANHTOAN");
            dataGridViewTHANHTOAN.DataSource = dta;

            textBoxMATT.DataBindings.Clear();
            textBoxMATT.DataBindings.Add("Text", dta, "MATT");

            comboBoxCMND.DataBindings.Clear();
            comboBoxCMND.DataBindings.Add("Text", dta, "CMND");

            comboBoxMADCHO.DataBindings.Clear();
            comboBoxMADCHO.DataBindings.Add("Text", dta, "MADCHO");

            dateTimePicker2.DataBindings.Clear();
            dateTimePicker2.DataBindings.Add("Text", dta, "NGAYTHANHTOAN");

            textBoxSOVEMUA.DataBindings.Clear();
            textBoxSOVEMUA.DataBindings.Add("Text", dta, "SOLUONGVEMUA");

            textBoxPHUONGTHUC.DataBindings.Clear();
            textBoxPHUONGTHUC.DataBindings.Add("Text", dta, "PHUONGTHUC");

            textBoxTHANHTIEN.DataBindings.Clear();
            textBoxTHANHTIEN.DataBindings.Add("Text", dta, "THANHTIEN");

        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            string MATT = textBoxMATT.Text;
            string CMND = comboBoxCMND.Text;
            string MADCHO = comboBoxMADCHO.Text;
            DateTime NGAYTHANHTOAN = dateTimePicker2.Value;
            int SOLUONGVEMUA = int.Parse(textBoxSOVEMUA.Text); // Chuyển đổi thành kiểu số nguyên
            decimal THANHTIEN = decimal.Parse(textBoxTHANHTIEN.Text); // Chuyển đổi thành kiểu số thập phân
            string PHUONGTHUC = textBoxPHUONGTHUC.Text;

            kn.KetNoi_Dulieu();

            string sql_insert = "INSERT INTO THANHTOAN (MATT, CMND, MADCHO, SOLUONGVEMUA, THANHTIEN, NGAYTHANHTOAN, PHUONGTHUC) VALUES (@MATT, @CMND, @MADCHO, @SOLUONGVEMUA, @THANHTIEN, @NGAYTHANHTOAN, @PHUONGTHUC)";

            using (SqlCommand cmd = new SqlCommand(sql_insert, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@MATT", MATT);
                cmd.Parameters.AddWithValue("@CMND", CMND);
                cmd.Parameters.AddWithValue("@MADCHO", MADCHO);
                cmd.Parameters.AddWithValue("@SOLUONGVEMUA", SOLUONGVEMUA);
                cmd.Parameters.AddWithValue("@THANHTIEN", THANHTIEN);
                cmd.Parameters.AddWithValue("@NGAYTHANHTOAN", NGAYTHANHTOAN);
                cmd.Parameters.AddWithValue("@PHUONGTHUC", PHUONGTHUC);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Dữ liệu đã được thêm thành công !");
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào được thêm !");
                }
                LayDuLieu();
            }
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            string MATT = textBoxMATT.Text;
            string CMND = comboBoxCMND.Text;
            string MADCHO = comboBoxMADCHO.Text;
            DateTime NGAYTHANHTOAN = dateTimePicker2.Value;
            int SOLUONGVEMUA = int.Parse(textBoxSOVEMUA.Text); // Chuyển đổi thành kiểu số nguyên
            decimal THANHTIEN = decimal.Parse(textBoxTHANHTIEN.Text); // Chuyển đổi thành kiểu số thập phân
            string PHUONGTHUC = textBoxPHUONGTHUC.Text;

            kn.KetNoi_Dulieu();

            string sql_update = "UPDATE THANHTOAN SET CMND = @CMND, MADCHO = @MADCHO, SOLUONGVEMUA = @SOLUONGVEMUA, THANHTIEN = @THANHTIEN, NGAYTHANHTOAN = @NGAYTHANHTOAN, PHUONGTHUC = @PHUONGTHUC WHERE MATT = @MATT";

            using (SqlCommand cmd = new SqlCommand(sql_update, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@MATT", MATT);
                cmd.Parameters.AddWithValue("@CMND", CMND);
                cmd.Parameters.AddWithValue("@MADCHO", MADCHO);
                cmd.Parameters.AddWithValue("@SOLUONGVEMUA", SOLUONGVEMUA);
                cmd.Parameters.AddWithValue("@THANHTIEN", THANHTIEN);
                cmd.Parameters.AddWithValue("@NGAYTHANHTOAN", NGAYTHANHTOAN);
                cmd.Parameters.AddWithValue("@PHUONGTHUC", PHUONGTHUC);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Dữ liệu đã được cập nhật thành công !");
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào được cập nhật !");
                }
                LayDuLieu();
            }
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            kn.KetNoi_Dulieu();
            string keyword = textBoxTimKiem.Text;

            string sql_search = "SELECT * FROM THANHTOAN WHERE MATT LIKE @keyword OR CMND LIKE @keyword OR MADCHO LIKE @keyword";

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
                        dataGridViewTHANHTOAN.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi nào phù hợp !");
                    }
                }
            }
        }
    }
}
