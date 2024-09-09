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
using static QuanLiVeTau.FrmLogin;

namespace QuanLiVeTau
{
    public partial class FrmNhanVien : Form
    {
        public FrmNhanVien()
        {
            InitializeComponent();
            dataGridViewNhanVien.SelectionChanged += dataGridViewNhanVien_SelectionChanged;
            AttachButtonHandlers();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Form formToShow = null;
                switch (button.Name)
                {
                    case "buttonDatCho":
                        formToShow = new FrmDatCho();
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
        private void dataGridViewNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewNhanVien.SelectedRows[0];
                textBoxMaNV.Text = row.Cells["MANV"].Value.ToString();
                textBoxTENNV.Text = row.Cells["TENNV"].Value.ToString();
                textBoxDCHI.Text = row.Cells["DIACHI"].Value.ToString();
                dateTimePicker1.Value = (DateTime)row.Cells["NGAYSING"].Value;
                textBoxDTHOAI.Text = row.Cells["DTHOAI"].Value.ToString();
                textBoxLUONG.Text = row.Cells["LUONG"].Value.ToString();
                textBoxTenDN.Text = row.Cells["TENDN"].Value.ToString();
                textBoxMK.Text = row.Cells["MK"].Value.ToString();
                textBoxMAQUYEN.Text = row.Cells["MAQUYEN"].Value.ToString();

                // Assuming there are radio buttons named radioButton1 and radioButton2 in groupBox1
                string gioitinh = row.Cells["GIOITINH"].Value.ToString();
                if (gioitinh == "Nam")
                {
                    radioButton1.Checked = true;
                }
                else if (gioitinh == "Nữ")
                {
                    radioButton2.Checked = true;
                }
            }
        }

        KetnoiCSDL kn = new KetnoiCSDL();
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewNhanVien.Rows[e.RowIndex];

                // Populate text boxes with details from the selected row
                textBoxMaNV.Text = row.Cells["MANV"].Value.ToString();
                textBoxTENNV.Text = row.Cells["TENNV"].Value.ToString();
                textBoxDCHI.Text = row.Cells["DIACHI"].Value.ToString();
                dateTimePicker1.Value = (DateTime)row.Cells["NGAYSING"].Value;
                textBoxDTHOAI.Text = row.Cells["DTHOAI"].Value.ToString();
                textBoxLUONG.Text = row.Cells["LUONG"].Value.ToString();
                textBoxTenDN.Text = row.Cells["TENDN"].Value.ToString();
                textBoxMK.Text = row.Cells["MK"].Value.ToString();
                textBoxMAQUYEN.Text = row.Cells["MAQUYEN"].Value.ToString();

                // Assuming there is a radio button column named "GIOITINH"
                string gioitinh = row.Cells["GIOITINH"].Value.ToString();
                if (gioitinh == radioButton1.Text)
                {
                    radioButton1.Checked = true;
                }
                else if (gioitinh == radioButton2.Text)
                {
                    radioButton2.Checked = true;
                }
            }
        }
    

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LayDuLieu(); 

            int maQuyen = AppConfig.MAQUYEN;

          
            if (maQuyen == 2)
            {
                MessageBox.Show("Bạn không có quyền chỉnh sửa thông tin nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

           
                FrmChuyenTau frmChuyenTau = new FrmChuyenTau();
                frmChuyenTau.Show();

                this.Close(); 
            }
        }
        public void LayDuLieu()
        {
            DataTable dta = new DataTable();
            dta = kn.LayBang("select MANV, TENNV, DIACHI,NGAYSING,DTHOAI,GIOITINH,LUONG,TENDN,MK,MAQUYEN from NHANVIEN");
            dataGridViewNhanVien.DataSource = dta;
            HienThiDuLieu();
        }
        public void HienThiDuLieu()
        {
            DataTable dta = kn.LayBang("SELECT MANV, TENNV, DIACHI, GIOITINH, NGAYSING, DTHOAI, LUONG, TENDN, MK, MAQUYEN FROM NHANVIEN");
            dataGridViewNhanVien.DataSource = dta;

            textBoxMaNV.DataBindings.Clear();
            textBoxMaNV.DataBindings.Add("Text", dta, "MANV");

            textBoxTENNV.DataBindings.Clear();
            textBoxTENNV.DataBindings.Add("Text", dta, "TENNV");

            textBoxDCHI.DataBindings.Clear();
            textBoxDCHI.DataBindings.Add("Text", dta, "DIACHI");

            dateTimePicker1.DataBindings.Clear();
            dateTimePicker1.DataBindings.Add("Value", dta, "NGAYSING");

            textBoxDTHOAI.DataBindings.Clear();
            textBoxDTHOAI.DataBindings.Add("Text", dta, "DTHOAI");

            textBoxLUONG.DataBindings.Clear();
            textBoxLUONG.DataBindings.Add("Text", dta, "LUONG");

            textBoxTenDN.DataBindings.Clear();
            textBoxTenDN.DataBindings.Add("Text", dta, "TENDN");

            textBoxMK.DataBindings.Clear();
            textBoxMK.DataBindings.Add("Text", dta, "MK");

            textBoxMAQUYEN.DataBindings.Clear();
            textBoxMAQUYEN.DataBindings.Add("Text", dta, "MAQUYEN");

            foreach (DataGridViewRow row in dataGridViewNhanVien.Rows)
            {
                if (row.Cells["GIOITINH"].Value != null)
                {
                    string gioitinh = row.Cells["GIOITINH"].Value.ToString();
                    if (gioitinh == "Nam")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (gioitinh == "Nữ")
                    {
                        radioButton2.Checked = true;
                    }
                }
            }
        }
        private void buttonTao_Click(object sender, EventArgs e)
        {
            textBoxMaNV.Text = "";
            textBoxTENNV.Text = "";
            textBoxDCHI.Text = "";
            dateTimePicker1.Text = "";
            textBoxDTHOAI.Text = "";
            textBoxLUONG.Text = "";
            textBoxTenDN.Text = "";
            textBoxMK.Text = "";
            textBoxMAQUYEN.Text = "";
            groupBox1 = new GroupBox();
            textBoxMaNV.Focus();
        }

        private void buttonExit_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonXoa_Click_1(object sender, EventArgs e)
        {
            String sqlxoa = "delete from NHANVIEN where MANV = '" + textBoxMaNV.Text + "'";
            kn.ThucThiDuLieu(sqlxoa);
            LayDuLieu();
        }

        private void buttonThem_Click_1(object sender, EventArgs e)
        {
            kn.KetNoi_Dulieu(); // Assuming this method establishes a database connection

            // Collecting values from controls on the form
            string manv = textBoxMaNV.Text;
            string tennv = textBoxTENNV.Text;
            string dchi = textBoxDCHI.Text;
            DateTime nsing = dateTimePicker1.Value;
            string dthoai = textBoxDTHOAI.Text;
            float luong = float.Parse(textBoxLUONG.Text);
            string tendn = textBoxTenDN.Text;
            string mk = textBoxMK.Text;
            int maquyen = int.Parse(textBoxMAQUYEN.Text);
            string gtinh;
            if (radioButton1.Checked)
            {
                gtinh = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                gtinh = radioButton2.Text;
            }
            else
            {
                gtinh = "Giá trị mặc định hoặc xử lý khác";
            }

            // SQL INSERT statement
            string sql_insert = "INSERT INTO NHANVIEN (MANV, TENNV, DIACHI, NGAYSING, GIOITINH, DTHOAI, LUONG, TENDN, MK, MAQUYEN) " +
                                "VALUES (@manv, @tennv, @dchi, @nsing, @gtinh, @dthoai, @luong, @tendn, @mk, @maquyen)";

            using (SqlCommand cmd = new SqlCommand(sql_insert, kn.cnn))
            {
                // Adding parameters to the SQL command
                cmd.Parameters.AddWithValue("@manv", manv);
                cmd.Parameters.AddWithValue("@tennv", tennv);
                cmd.Parameters.AddWithValue("@dchi", dchi);
                cmd.Parameters.AddWithValue("@nsing", nsing);
                cmd.Parameters.AddWithValue("@dthoai", dthoai);
                cmd.Parameters.AddWithValue("@luong", luong);
                cmd.Parameters.AddWithValue("@tendn", tendn);
                cmd.Parameters.AddWithValue("@mk", mk);
                cmd.Parameters.AddWithValue("@maquyen", maquyen);
                cmd.Parameters.AddWithValue("@gtinh", gtinh);

                // Executing the SQL command
                int rowsAffected = cmd.ExecuteNonQuery();

                // Displaying a message box based on the result
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Dữ liệu đã được thêm vào thành công !");
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được thêm vào !");
                }

                // Refreshing the data displayed on the form
                LayDuLieu();
            }

        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            kn.KetNoi_Dulieu(); // Assuming this method establishes a connection to your database

            // Collecting values from controls on the form
            string manv = textBoxMaNV.Text;
            string tennv = textBoxTENNV.Text;
            string dchi = textBoxDCHI.Text;
            DateTime nsing = dateTimePicker1.Value;
            string dthoai = textBoxDTHOAI.Text;
            float luong = float.Parse(textBoxLUONG.Text);
            string tendn = textBoxTenDN.Text;
            string mk = textBoxMK.Text;
            int maquyen = int.Parse(textBoxMAQUYEN.Text);
            string gtinh;
            if (radioButton1.Checked)
            {
                gtinh = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                gtinh = radioButton2.Text;
            }
            else
            {
                gtinh = "Giá trị mặc định hoặc xử lý khác";
            }

            // SQL UPDATE statement
            string sql_update = "UPDATE NHANVIEN SET TENNV = @tennv, DIACHI = @dchi, NGAYSING = @nsing, GIOITINH = @gtinh, DTHOAI = @dthoai, LUONG = @luong, TENDN = @tendn, MK = @mk, MAQUYEN = @maquyen WHERE MANV = @manv";

            using (SqlCommand cmd = new SqlCommand(sql_update, kn.cnn))
            {
                // Adding parameters to the SQL command
                cmd.Parameters.AddWithValue("@manv", manv);
                cmd.Parameters.AddWithValue("@tennv", tennv);
                cmd.Parameters.AddWithValue("@dchi", dchi);
                cmd.Parameters.AddWithValue("@nsing", nsing);
                cmd.Parameters.AddWithValue("@dthoai", dthoai);
                cmd.Parameters.AddWithValue("@luong", luong);
                cmd.Parameters.AddWithValue("@tendn", tendn);
                cmd.Parameters.AddWithValue("@mk", mk);
                cmd.Parameters.AddWithValue("@maquyen", maquyen);
                cmd.Parameters.AddWithValue("@gtinh", gtinh);

                // Executing the SQL command
                int rowsAffected = cmd.ExecuteNonQuery();

                // Displaying a message box based on the result
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Dữ liệu đã được cập nhật thành công !");
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được cập nhật !");
                }

                // Refreshing the data displayed on the form
                LayDuLieu();
            }

        }

        private void buttonTimKiem_Click_1(object sender, EventArgs e)
        {
            kn.KetNoi_Dulieu();
            string keyword = textBoxTimKiem.Text;

            string sql_search = "SELECT * FROM NHANVIEN WHERE MANV LIKE @keyword OR TENNV LIKE @keyword OR DIACHI LIKE @keyword OR CONVERT(varchar(10), NGAYSING, 103) LIKE @keyword OR DTHOAI LIKE @keyword OR LUONG LIKE @keyword OR TENDN LIKE @keyword OR MK LIKE @keyword OR MAQUYEN LIKE @keyword";

            using (SqlCommand cmd = new SqlCommand(sql_search, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Display new search results in dataGridViewNhanVien
                        dataGridViewNhanVien.DataSource = dataTable;
                    }
                    else
                    {
                        // Clear dataGridViewNhanVien if no records found
                        dataGridViewNhanVien.DataSource = null;
                        dataGridViewNhanVien.Rows.Clear();

                        MessageBox.Show("Không tìm thấy bản ghi nào phù hợp !");
                    }
                }
            }

        }
    }
}
