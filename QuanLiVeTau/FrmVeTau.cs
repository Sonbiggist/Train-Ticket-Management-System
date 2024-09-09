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
    public partial class FrmVeTau : Form
    {
        public FrmVeTau()
        {
            InitializeComponent();
            AttachButtonHandlers();
            dataGridViewVE.SelectionChanged += dataGridViewVE_SelectionChanged;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void dataGridViewVE_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewVE.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewVE.SelectedRows[0];
                textBoxMAVE.Text = row.Cells["MAVE"].Value.ToString();
                comboBoxCMND.Text = row.Cells["CMND"].Value.ToString();
                comboBoxMACT.Text = row.Cells["MACT"].Value.ToString();
                comboBoxMANV.Text = row.Cells["MANV"].Value.ToString();
                textBoxLOAIGHE.Text = row.Cells["LOAIGHE"].Value.ToString();
                dateTimePicker1.Text = row.Cells["NGAYDATVE"].Value.ToString();
                textBoxGIAVE.Text = row.Cells["GIAVE"].Value.ToString();
                textBoxGHICHU.Text = row.Cells["GHICHU"].Value.ToString();

            }
        }
        public void LayDuLieu()
        {
            DataTable dta = new DataTable();
            dta = kn.LayBang("select MAVE,CMND,MACT,MANV,LOAIGHE,GIAVE,NGAYDATVE,GHICHU from VETAU");
            dataGridViewVE.DataSource = dta;
            HienThiDuLieu();
        }
        public void HienThiDuLieu()
        {
            DataTable dta = kn.LayBang("select MAVE,CMND,MACT,MANV,LOAIGHE,GIAVE,NGAYDATVE,GHICHU from VETAU");
            dataGridViewVE.DataSource = dta;

            textBoxMAVE.DataBindings.Clear();
            textBoxMAVE.DataBindings.Add("Text", dta, "MAVE");

            comboBoxCMND.DataBindings.Clear();
            comboBoxCMND.DataBindings.Add("Text", dta, "CMND");

            comboBoxMACT.DataBindings.Clear();
            comboBoxMACT.DataBindings.Add("Text", dta, "MACT");

            comboBoxMANV.DataBindings.Clear();
            comboBoxMANV.DataBindings.Add("Text", dta, "MANV");

            dateTimePicker1.DataBindings.Clear();
            dateTimePicker1.DataBindings.Add("Value", dta, "NGAYDATVE");

            textBoxGIAVE.DataBindings.Clear();
            textBoxGIAVE.DataBindings.Add("Text", dta, "GIAVE");

            textBoxLOAIGHE.DataBindings.Clear();
            textBoxLOAIGHE.DataBindings.Add("Text", dta, "LOAIGHE");

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
                    case "buttonKhachHang":
                        formToShow = new FrmKhachHang();
                        break;
                    case "buttonChuyenTau":
                        formToShow = new FrmChuyenTau();
                        break;
                    case "buttonDatCho":
                        formToShow = new FrmDatCho();
                        break;
                    case "buttonThanhToan":
                        formToShow = new FrmThanhToan();
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
        private void FrmVeTau_Load(object sender, EventArgs e)
        {
            LayDuLieu();
            Lay_DL_Bang_CMND(); 
            Lay_DL_Bang_MACT();
            Lay_DL_Bang_MANV();
        }
        KetnoiCSDL kn = new KetnoiCSDL();

        private void textBoxSOGHECON_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonTao_Click(object sender, EventArgs e)
        {
            textBoxMAVE.Text = "";
            comboBoxCMND.Text = "";
            comboBoxMACT.Text = "";
            comboBoxMANV.Text = "";
            dateTimePicker1.Text = "";
            textBoxGIAVE.Text = "";
            textBoxLOAIGHE.Text = "";
            textBoxGHICHU.Text = "";
            textBoxMAVE.Focus();
        }
        public void Lay_DL_Bang_CMND()
        {
            DataTable dt = kn.LayBang("SELECT CMND FROM KHACHHANG");
            comboBoxCMND.DataSource = dt;
            comboBoxCMND.DisplayMember = "CMND";
            comboBoxCMND.ValueMember = "CMND";
        }

        public void Lay_DL_Bang_MACT()
        {
            DataTable dt = kn.LayBang("SELECT MACT FROM CHUYENTAU");
            comboBoxMACT.DataSource = dt;
            comboBoxMACT.DisplayMember = "MACT";
            comboBoxMACT.ValueMember = "MACT";
        }

        public void Lay_DL_Bang_MANV()
        {
            DataTable dt = kn.LayBang("SELECT MANV FROM NHANVIEN");
            comboBoxMANV.DataSource = dt;
            comboBoxMANV.DisplayMember = "MANV";
            comboBoxMANV.ValueMember = "MANV";
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            if (comboBoxCMND.SelectedIndex == -1 || comboBoxMACT.SelectedIndex == -1 || comboBoxMANV.SelectedIndex == -1 )
            {
                MessageBox.Show("Vui lòng chọn giá trị cho các trường bắt buộc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string mave = textBoxMAVE.Text;
            string cmnd = comboBoxCMND.Text;
            string mact = comboBoxMACT.Text;
            string manv = comboBoxMANV.Text;
            string loaighe = textBoxLOAIGHE.Text;
            string giave = textBoxGIAVE.Text;
            DateTime ngaydatve = dateTimePicker1.Value;
            string ghichu = textBoxGHICHU.Text;

            string sql_update = "UPDATE VETAU SET CMND = @CMND, MANV = @MANV, MACT = @MACT, LOAIGHE = @LOAIGHE, GIAVE = @GIAVE, NGAYDATVE = @NGAYDATVE, GHICHU = @GHICHU WHERE MAVE = @MAVE";

            kn.KetNoi_Dulieu();
            using (SqlCommand command = new SqlCommand(sql_update, kn.cnn))
            {
                command.Parameters.AddWithValue("@MAVE", mave);
                command.Parameters.AddWithValue("@CMND", cmnd);
                command.Parameters.AddWithValue("@MACT", mact);
                command.Parameters.AddWithValue("@MANV", manv);
                command.Parameters.AddWithValue("@LOAIGHE", loaighe);
                command.Parameters.AddWithValue("@GIAVE", giave);
                command.Parameters.AddWithValue("@NGAYDATVE", ngaydatve);
                command.Parameters.AddWithValue("@GHICHU", ghichu);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error updating record.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating record: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LayDuLieu();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            if (comboBoxCMND.SelectedIndex == -1 || comboBoxMACT.SelectedIndex == -1 || comboBoxMANV.SelectedIndex == -1 )
            {
                MessageBox.Show("Vui lòng chọn giá trị cho các trường bắt buộc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tiếp tục với quá trình thêm dữ liệu
            string mave = textBoxMAVE.Text;
            string cmnd = comboBoxCMND.Text;
            string mact = comboBoxMACT.Text;
            string manv = comboBoxMANV.Text;
            string loaighe = textBoxLOAIGHE.Text;
            string giave = textBoxGIAVE.Text;
            DateTime ngaydatve = dateTimePicker1.Value;
            string ghichu = textBoxGHICHU.Text;

            string sql_insert = "INSERT INTO VETAU (MAVE, CMND, MANV, MACT, LOAIGHE, GIAVE, NGAYDATVE, GHICHU) VALUES (@MAVE, @CMND, @MANV, @MACT, @LOAIGHE,  @GIAVE, @NGAYDATVE, @GHICHU)";

            kn.KetNoi_Dulieu();
            using (SqlCommand command = new SqlCommand(sql_insert, kn.cnn))
            {
                command.Parameters.AddWithValue("@MAVE", mave);
                command.Parameters.AddWithValue("@CMND", cmnd);
                command.Parameters.AddWithValue("@MACT", mact);
                command.Parameters.AddWithValue("@MANV", manv);
                command.Parameters.AddWithValue("@LOAIGHE", loaighe);
                command.Parameters.AddWithValue("@GIAVE", giave);
                command.Parameters.AddWithValue("@NGAYDATVE", ngaydatve);
                command.Parameters.AddWithValue("@GHICHU", ghichu);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record added successfully.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error adding record.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding record: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LayDuLieu();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            String sqlxoa = "delete from VETAU where MAVE = '" + textBoxMAVE.Text + "'";
            kn.ThucThiDuLieu(sqlxoa);
            LayDuLieu();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            kn.KetNoi_Dulieu();
            string keyword = textBoxTimKiem.Text;

            string sql_search = "SELECT * FROM VETAU WHERE MAVE LIKE @keyword OR CMND LIKE @keyword OR MACT LIKE @keyword OR MANV LIKE @keyword";

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
                        dataGridViewVE.DataSource = dataTable;
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
