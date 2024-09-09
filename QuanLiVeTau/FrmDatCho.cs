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
    public partial class FrmDatCho : Form
    {
        public FrmDatCho()
        {
            InitializeComponent();
            AttachButtonHandlers();
            dataGridViewDATCHO.SelectionChanged += dataGridViewDATCHO_SelectionChanged;

        }
        private void dataGridViewDATCHO_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDATCHO.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewDATCHO.SelectedRows[0];
                textBoxMADCHO.Text = row.Cells["MADCHO"].Value.ToString();
                comboBoxCMND.Text = row.Cells["CMND"].Value.ToString();
                comboBoxMACT.Text = row.Cells["MACT"].Value.ToString();
                dateTimePicker1.Text = row.Cells["NGAYDATVE"].Value.ToString();
                textBoxSOLUONGVEMUA.Text = row.Cells["SOLUONGVEMUA"].Value.ToString();
                textBoxDONGIA.Text = row.Cells["DONGIA"].Value.ToString();
                textBoxTHANHTIEN.Text = row.Cells["THANHTIEN"].Value.ToString();
                textBoxGHICHU.Text = row.Cells["GHICHU"].Value.ToString();

            }
        }
        public void Lay_DL_Bang_CMND()
        {
            DataTable dt = kn.LayBang("SELECT CMND from KHACHHANG");
            comboBoxCMND.DataSource = dt;
            comboBoxCMND.DisplayMember = "CMND";
            comboBoxCMND.ValueMember = "CMND";

        }

        public void Lay_DL_Bang_MACT()
        {
            DataTable dt = kn.LayBang("SELECT MACT from CHUYENTAU");
            comboBoxMACT.DataSource = dt;
            comboBoxMACT.DisplayMember = "MACT";
            comboBoxMACT.ValueMember = "MACT";

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
                    case "buttonChuyenTau":
                        formToShow = new FrmChuyenTau();
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
        private void FrmDatCho_Load(object sender, EventArgs e)
        {
            Lay_DL_Bang_CMND();
            Lay_DL_Bang_MACT();
            LayDuLieu();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void comboBoxCMND_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonTao_Click(object sender, EventArgs e)
        {
            textBoxMADCHO.Text = "";
            comboBoxCMND.Text = "";
            comboBoxMACT.Text = "";
            dateTimePicker1.Text = "";
            textBoxSOLUONGVEMUA.Text = "";
            textBoxTHANHTIEN.Text = "";
            textBoxGHICHU.Text = "";
            textBoxDONGIA.Text = "";
            textBoxMADCHO.Focus();

        }
        private void textBoxTHANHTIEN_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSOLUONGVEMUA.Text) && !string.IsNullOrEmpty(textBoxDONGIA.Text))
            {
                int soLuongVeMua = Convert.ToInt32(textBoxSOLUONGVEMUA.Text);
                decimal donGia = Convert.ToDecimal(textBoxDONGIA.Text);
                decimal thanhTien = soLuongVeMua * donGia;
                textBoxTHANHTIEN.Text = thanhTien.ToString();
            }
        }
        KetnoiCSDL kn = new KetnoiCSDL();
        public void LayDuLieu()
        {
            DataTable dta = new DataTable();
            dta = kn.LayBang("select MADCHO,CMND,MACT,SOLUONGVEMUA,DONGIA,THANHTIEN,NGAYDATVE,GHICHU from DATCHO");
            dataGridViewDATCHO.DataSource = dta;
            HienThiDuLieu();
        }
        public void HienThiDuLieu()
        {
            DataTable dta = kn.LayBang("select MADCHO,CMND,MACT,SOLUONGVEMUA,DONGIA,THANHTIEN,NGAYDATVE,GHICHU from DATCHO");
            dataGridViewDATCHO.DataSource = dta;

            textBoxMADCHO.DataBindings.Clear();
            textBoxMADCHO.DataBindings.Add("Text", dta, "MADCHO");

            comboBoxCMND.DataBindings.Clear();
            comboBoxCMND.DataBindings.Add("Text", dta, "CMND");

            comboBoxMACT.DataBindings.Clear();
            comboBoxMACT.DataBindings.Add("Text", dta, "MACT");

            dateTimePicker1.DataBindings.Clear();
            dateTimePicker1.DataBindings.Add("Text", dta, "NGAYDATVE");

            textBoxSOLUONGVEMUA.DataBindings.Clear();
            textBoxSOLUONGVEMUA.DataBindings.Add("Text", dta, "SOLUONGVEMUA");

            textBoxTHANHTIEN.DataBindings.Clear();
            textBoxTHANHTIEN.DataBindings.Add("Text", dta, "THANHTIEN");

            textBoxDONGIA.DataBindings.Clear();
            textBoxDONGIA.DataBindings.Add("Text", dta, "DONGIA");
            textBoxGHICHU.DataBindings.Clear();
            textBoxGHICHU.DataBindings.Add("Text", dta, "GHICHU");
        }
        private void buttonXoa_Click(object sender, EventArgs e)
        {
            String sqlxoa = "delete from DATCHO where MADCHO = '" + textBoxMADCHO.Text + "'";
            kn.ThucThiDuLieu(sqlxoa);
            LayDuLieu();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            string madcho = textBoxMADCHO.Text;
            string cmnd = comboBoxCMND.Text;
            string mact = comboBoxMACT.Text;
            int soluongvemua = int.Parse(textBoxSOLUONGVEMUA.Text);
            float dongia = float.Parse(textBoxDONGIA.Text); // Assuming you have a textBoxDONGIA
            float thanhtien = float.Parse(textBoxTHANHTIEN.Text);
            DateTime ngaydatve = dateTimePicker1.Value;
            string ghichu = textBoxGHICHU.Text;
            kn.KetNoi_Dulieu();
            string sql_sua = "UPDATE DATCHO SET CMND = @CMND, MACT = @MACT, SOLUONGVEMUA = @SOLUONGVEMUA, DONGIA = @DONGIA, THANHTIEN = @THANHTIEN, NGAYDATVE = @NGAYDATVE, GHICHU = @GHICHU WHERE MADCHO = @MADCHO";
            // Update the record in the DATCHO table
            using (SqlCommand cmd = new SqlCommand(sql_sua, kn.cnn))
            {

                cmd.Parameters.AddWithValue("@CMND", cmnd);
                cmd.Parameters.AddWithValue("@MACT", mact);
                cmd.Parameters.AddWithValue("@SOLUONGVEMUA", soluongvemua);
                cmd.Parameters.AddWithValue("@DONGIA", dongia);
                cmd.Parameters.AddWithValue("@THANHTIEN", thanhtien);
                cmd.Parameters.AddWithValue("@NGAYDATVE", ngaydatve);
                cmd.Parameters.AddWithValue("@GHICHU", ghichu);
                cmd.Parameters.AddWithValue("@MADCHO", madcho);
                int rowsAffected = cmd.ExecuteNonQuery();
                try
                {

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the given MADCHO.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating record: " + ex.Message);
                }
            }
            LayDuLieu();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            kn.KetNoi_Dulieu();
            string keyword = textBoxTimKiem.Text;

            string sql_search = "SELECT * FROM DATCHO WHERE MADCHO LIKE @keyword OR CMND LIKE @keyword OR MACT LIKE @keyword OR GHICHU LIKE @keyword";

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
                        dataGridViewDATCHO.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi nào phù hợp !");
                    }
                }
            }
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            string madcho = textBoxMADCHO.Text;
            string cmnd = comboBoxCMND.Text;
            string mact = comboBoxMACT.Text;
            int soluongvemua = int.Parse(textBoxSOLUONGVEMUA.Text);
            float dongia = float.Parse(textBoxDONGIA.Text);
            float thanhtien = float.Parse(textBoxTHANHTIEN.Text);
            DateTime ngaydatve = dateTimePicker1.Value;
            string ghichu = textBoxGHICHU.Text;

            try
            {
                // Mở kết nối CSDL
                kn.KetNoi_Dulieu();

                // Truy vấn để lấy thông tin số vé còn lại từ bảng CHUYENTAU
                string query = "SELECT SOGHECON FROM CHUYENTAU WHERE MACT = @mact";
                SqlCommand selectCommand = new SqlCommand(query, kn.cnn);
                selectCommand.Parameters.AddWithValue("@mact", mact);
                int soVeConLai = (int)selectCommand.ExecuteScalar(); // Lấy số vé còn lại từ cơ sở dữ liệu

                // Nếu số vé còn lại trong bảng CHUYENTAU đủ để đặt
                if (soVeConLai >= soluongvemua)
                {
                    // Trừ số vé đã đặt từ số vé còn lại trong bảng CHUYENTAU
                    soVeConLai -= soluongvemua;

                    // Cập nhật số vé còn lại trong bảng CHUYENTAU
                    string updateQuery = "UPDATE CHUYENTAU SET SOGHECON = @SOGHECON WHERE MACT = @mact";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, kn.cnn);
                    updateCommand.Parameters.AddWithValue("@SOGHECON", soVeConLai);
                    updateCommand.Parameters.AddWithValue("@mact", mact);
                    updateCommand.ExecuteNonQuery(); // Thực thi truy vấn cập nhật

                    // Thêm dữ liệu vào bảng DATCHO
                    string insertQuery = "INSERT INTO DATCHO (MADCHO, CMND, MACT, SOLUONGVEMUA, DONGIA, THANHTIEN, NGAYDATVE, GHICHU) VALUES (@MADCHO, @CMND, @MACT, @SOLUONGVEMUA, @DONGIA, @THANHTIEN, @NGAYDATVE, @GHICHU)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, kn.cnn);
                    insertCommand.Parameters.AddWithValue("@MADCHO", madcho);
                    insertCommand.Parameters.AddWithValue("@CMND", cmnd);
                    insertCommand.Parameters.AddWithValue("@MACT", mact);
                    insertCommand.Parameters.AddWithValue("@SOLUONGVEMUA", soluongvemua);
                    insertCommand.Parameters.AddWithValue("@DONGIA", dongia);
                    insertCommand.Parameters.AddWithValue("@THANHTIEN", thanhtien);
                    insertCommand.Parameters.AddWithValue("@NGAYDATVE", ngaydatve);
                    insertCommand.Parameters.AddWithValue("@GHICHU", ghichu);
                    insertCommand.ExecuteNonQuery(); // Thực thi truy vấn thêm

                    MessageBox.Show("Record added successfully.");
                }
                else
                {
                    MessageBox.Show("Không đủ số vé còn lại trong chuyến tàu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối CSDL
                kn.HuyKetNoi();
            }
            LayDuLieu();
        }
    }
}
