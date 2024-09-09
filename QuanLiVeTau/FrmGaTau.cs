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
    public partial class FrmGaTau : Form
    {
        public FrmGaTau()
        {
            InitializeComponent();
            AttachButtonHandlers();
            dataGridViewGATAU.SelectionChanged += dataGridViewGATAU_SelectionChanged;
        }
        KetnoiCSDL kn = new KetnoiCSDL();
        private void dataGridViewGATAU_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewGATAU.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewGATAU.SelectedRows[0];
                textBoxMAGA.Text = row.Cells["MAGA"].Value.ToString();
                textBoxTENGA.Text = row.Cells["TENGA"].Value.ToString();


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
        private void FrmGaTau_Load(object sender, EventArgs e)
        {
            LayDuLieu();
        }

        private void buttonTao_Click(object sender, EventArgs e)
        {
            textBoxMAGA.Text = string.Empty;
            textBoxTENGA.Text = string.Empty;
            textBoxMAGA.Focus();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public void HienThiDuLieu()
        {
            DataTable dta = kn.LayBang("Select MAGA,TENGA from GATAU");
            dataGridViewGATAU.DataSource = dta;

            textBoxMAGA.DataBindings.Clear();
            textBoxMAGA.DataBindings.Add("Text", dta, "MAGA");

            textBoxTENGA.DataBindings.Clear();
            textBoxTENGA.DataBindings.Add("Text", dta, "TENGA");
        }

        public void LayDuLieu()
        {
            DataTable dta = new DataTable();
            dta = kn.LayBang("select MAGA,TENGA from GATAU");
            dataGridViewGATAU.DataSource = dta;
            HienThiDuLieu();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            string MAGA = textBoxMAGA.Text;
            string TENGA = textBoxTENGA.Text;
            kn.KetNoi_Dulieu();
            string sql_insert = "INSERT INTO GATAU (MAGA,TENGA) VALUES (@MAGA, @TENGA)";
            using (SqlCommand cmd = new SqlCommand(sql_insert, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@MAGA", MAGA);
                cmd.Parameters.AddWithValue("@TENGA", TENGA);
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

        private void buttonSua_Click(object sender, EventArgs e)
        {
            string MAGA = textBoxMAGA.Text;
            string TENGA = textBoxTENGA.Text;
            kn.KetNoi_Dulieu();
            string sql_update = "UPDATE GATAU SET MAGA = @MAGA, TENGA =  @TENGA";
            using (SqlCommand cmd = new SqlCommand(sql_update, kn.cnn))
            {
                cmd.Parameters.AddWithValue("@MAGA", MAGA);
                cmd.Parameters.AddWithValue("@TENGA", TENGA);
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

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            String sqlxoa = "delete from GATAU where MAGA = '" + textBoxMAGA.Text + "'";
            kn.ThucThiDuLieu(sqlxoa);
            LayDuLieu();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            kn.KetNoi_Dulieu();
            string keyword = textBoxTimKiem.Text;

            string sql_search = "SELECT * FROM GATAU WHERE MAGA LIKE @keyword OR TENGA LIKE @keyword ";

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
                        dataGridViewGATAU.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi nào phù hợp !");
                    }
                }
            }
        }

        private void buttonDANHSACH_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmTau form1 = new FrmTau();
            form1.Show();
        }
    }
   }

