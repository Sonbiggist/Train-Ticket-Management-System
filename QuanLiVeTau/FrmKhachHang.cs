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
    public partial class FrmKhachHang : Form
    {
        public FrmKhachHang()
        {
            InitializeComponent();
            AttachButtonHandlers();
            dataGridViewKHACHHANG.SelectionChanged += dataGridViewKHACHHANG_SelectionChanged;

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            LayDuLieu();

        }
        public void LayDuLieu()
        {
            DataTable dta = new DataTable();
            dta = kn.LayBang("select TENKH, CMND, DIACHI, DTHOAI, GIOITINH, NGAYSINH, EMAIL, GHICHU from KHACHHANG");
            dataGridViewKHACHHANG.DataSource = dta;
            HienThiDuLieu();
        }
        public void HienThiDuLieu()
        {
            DataTable dta = kn.LayBang("select TENKH, CMND, DIACHI, DTHOAI,GIOITINH, NGAYSINH, EMAIL, GHICHU from KHACHHANG");
            dataGridViewKHACHHANG.DataSource = dta;

            textBoxTENKH.DataBindings.Clear();
            textBoxTENKH.DataBindings.Add("Text", dta, "TENKH");

            textBoxCMND.DataBindings.Clear();
            textBoxCMND.DataBindings.Add("Text", dta, "CMND");

            textBoxDCHI.DataBindings.Clear();
            textBoxDCHI.DataBindings.Add("Text", dta, "DIACHI");

            textBoxDTHOAI.DataBindings.Clear();
            textBoxDTHOAI.DataBindings.Add("Text", dta, "DTHOAI");

            foreach (DataGridViewRow row in dataGridViewKHACHHANG.Rows)
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

            dateTimePicker1.DataBindings.Clear();
            dateTimePicker1.DataBindings.Add("Value", dta, "NGAYSINH");

            textBoxEMAIL.DataBindings.Clear();
            textBoxEMAIL.DataBindings.Add("Text", dta, "EMAIL");

            textBoxGHICHU.DataBindings.Clear();
            textBoxGHICHU.DataBindings.Add("Text", dta, "GHICHU");
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
                    case "buttonChuyenTau":
                        formToShow = new FrmChuyenTau();
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
                        break;
                }

                if (formToShow != null)
                {
                    formToShow.Show();
                    this.Hide();
                }
            }
        }

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonTao_Click(object sender, EventArgs e)
        {
            textBoxTENKH.Text = "";
            textBoxCMND.Text = "";
            groupBox1.Text = "";
            dateTimePicker1.Text = "";
            textBoxDCHI.Text = "";
            textBoxDTHOAI.Text = "";
            textBoxGHICHU.Text = "";
            textBoxEMAIL.Text = "";
            textBoxTENKH.Focus();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            string tenkh = textBoxTENKH.Text;
            string cmnd = textBoxCMND.Text;
            string diachi = textBoxDCHI.Text;
            string dienthoai = textBoxDTHOAI.Text;
            string ghichu = textBoxGHICHU.Text;
            string email = textBoxEMAIL.Text;
            DateTime ngaysinh = dateTimePicker1.Value;

            string gioitinh;
            if (radioButton1.Checked)
            {
                gioitinh = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                gioitinh = radioButton2.Text;
            }
            else
            {
                gioitinh = "Giá trị mặc định hoặc xử lý khác";
            }

            string sql_insert = "INSERT INTO KHACHHANG (TENKH, CMND, DIACHI, DTHOAI, GIOITINH, NGAYSINH, EMAIL, GHICHU) " +
                       "VALUES (@TENKH, @CMND, @DIACHI, @DIENTHOAI, @GIOITINH, @NGAYSINH, @EMAIL, @GHICHU)";

            kn.KetNoi_Dulieu();
            using (SqlCommand command = new SqlCommand(sql_insert, kn.cnn))
            {
                // Thêm các tham số vào câu lệnh SQL
                command.Parameters.AddWithValue("@TENKH", tenkh);
                command.Parameters.AddWithValue("@CMND", cmnd);
                command.Parameters.AddWithValue("@DIACHI", diachi);
                command.Parameters.AddWithValue("@DIENTHOAI", dienthoai);
                command.Parameters.AddWithValue("@GIOITINH", gioitinh);
                command.Parameters.AddWithValue("@NGAYSINH", ngaysinh);
                command.Parameters.AddWithValue("@EMAIL", email);
                command.Parameters.AddWithValue("@GHICHU", ghichu);

                try
                {
                    // Thực thi câu lệnh SQL
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record added successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding record.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding record: " + ex.Message);
                }
            }
            LayDuLieu();
        }
        private void dataGridViewKHACHHANG_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewKHACHHANG.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewKHACHHANG.SelectedRows[0];
                textBoxTENKH.Text = row.Cells["TENKH"].Value.ToString();
                textBoxCMND.Text = row.Cells["CMND"].Value.ToString();
                textBoxDCHI.Text = row.Cells["DIACHI"].Value.ToString();
                dateTimePicker1.Text = row.Cells["NGAYSINH"].Value.ToString();
                textBoxDTHOAI.Text = row.Cells["DTHOAI"].Value.ToString();
                textBoxEMAIL.Text = row.Cells["EMAIL"].Value.ToString();
                textBoxGHICHU.Text = row.Cells["GHICHU"].Value.ToString();
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
        KetnoiCSDL kn = new KetnoiCSDL();
        private void buttonSua_Click(object sender, EventArgs e)
        {
            // Lấy các giá trị từ các controls trên giao diện
            string tenkh = textBoxTENKH.Text;
            string cmnd = textBoxCMND.Text;
            string diachi = textBoxDCHI.Text;
            string dienthoai = textBoxDTHOAI.Text;
            string ghichu = textBoxGHICHU.Text;
            string email = textBoxEMAIL.Text;
            DateTime ngaysinh = dateTimePicker1.Value;

            // Xác định giá trị của RadioButton trong groupBox1
            string gioitinh;
            if (radioButton1.Checked)
            {
                gioitinh = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                gioitinh = radioButton2.Text;
            }
            else
            {
                // Nếu không có RadioButton nào được chọn, gán giá trị mặc định hoặc xử lý theo nhu cầu của bạn
                gioitinh = "Giá trị mặc định hoặc xử lý khác";
            }

            string sql_update = "UPDATE KHACHHANG SET TENKH = @TENKH, DIACHI = @DIACHI, DTHOAI = @DIENTHOAI, GIOITINH = @GIOITINH, NGAYSINH = @NGAYSINH, EMAIL = @EMAIL, GHICHU = @GHICHU WHERE CMND = @CMND";


            kn.KetNoi_Dulieu();
            using (SqlCommand command = new SqlCommand(sql_update, kn.cnn))
            {
                // Thêm các tham số vào câu lệnh SQL
                command.Parameters.AddWithValue("@TENKH", tenkh);
                command.Parameters.AddWithValue("@CMND", cmnd);
                command.Parameters.AddWithValue("@DIACHI", diachi);
                command.Parameters.AddWithValue("@DIENTHOAI", dienthoai);
                command.Parameters.AddWithValue("@GIOITINH", gioitinh);
                command.Parameters.AddWithValue("@NGAYSINH", ngaysinh);
                command.Parameters.AddWithValue("@EMAIL", email);
                command.Parameters.AddWithValue("@GHICHU", ghichu);

                try
                {
                    // Thực thi câu lệnh SQL
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record added successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding record.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding record: " + ex.Message);
                }
            }
            LayDuLieu();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            String sqlxoa = "delete from KHACHHANG where CMND = '" + textBoxCMND.Text + "'";
            kn.ThucThiDuLieu(sqlxoa);
            LayDuLieu();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {

            kn.KetNoi_Dulieu();
            string keyword = textBoxTimKiem.Text;

            string sql_search = "SELECT * FROM KHACHHANG WHERE CMND LIKE @keyword OR TENKH LIKE @keyword OR DIACHI LIKE @keyword OR DTHOAI LIKE @keyword OR EMAIL LIKE @keyword";

            using (SqlCommand cmd = new SqlCommand(sql_search, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Clear the previous data in dataGridViewKHACHHANG
                        dataGridViewKHACHHANG.DataSource = null;
                        dataGridViewKHACHHANG.Rows.Clear();

                        // Display new search results in dataGridViewKHACHHANG
                        dataGridViewKHACHHANG.DataSource = dataTable;
                    }
                    else
                    {
                        // Clear dataGridViewKHACHHANG if no records found
                        dataGridViewKHACHHANG.DataSource = null;
                        dataGridViewKHACHHANG.Rows.Clear();

                        MessageBox.Show("Không tìm thấy bản ghi nào phù hợp !");
                    }
                }
            }
        }
    }
}
