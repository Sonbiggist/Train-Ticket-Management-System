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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogIn_Load(object sender, EventArgs e)
        {

        }

        KetnoiCSDL kn = new KetnoiCSDL();

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public static class AppConfig
        {
            public static int MAQUYEN { get; set; }
        }
        private void buttonLogIn_Click_1(object sender, EventArgs e)
        {
            string taiKhoan = textBoxTaiKhoan.Text;
            string matKhau = textBoxMatKhau.Text;

            string query = $"SELECT MAQUYEN FROM NHANVIEN WHERE TENDN = '{taiKhoan}' AND MK = '{matKhau}'";

            try
            {
                DataTable result = kn.LayBang(query);

                if (result.Rows.Count > 0)
                {
                    int maQuyen = Convert.ToInt32(result.Rows[0]["MAQUYEN"]);

                    if (maQuyen == 1 || maQuyen == 2)
                    {
                        MessageBox.Show("Đăng nhập thành công!");

                        AppConfig.MAQUYEN = maQuyen;

                        if (maQuyen == 1)
                        {
                            FrmNhanVien form1 = new FrmNhanVien();
                            form1.Show();
                        }
                        else if (maQuyen == 2)
                        {
                            FrmChuyenTau form2 = new FrmChuyenTau();
                            form2.Show();
                        }

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bạn không có quyền truy cập vào hệ thống.");
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác. Vui lòng thử lại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
