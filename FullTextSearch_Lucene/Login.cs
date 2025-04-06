using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullTextSearch_Lucene
{
    public partial class Login : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ThuVien"].ConnectionString;
        public static string quyen;

        public Login()
        {
            InitializeComponent();
            LoadSavedLogin();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txttk.Text;
            string matKhau = txtmk.Text;

            if (KiemTraDangNhap(tenDangNhap, matKhau))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();

                if (cbSaveLogin.Checked)
                {
                    SaveLogin(tenDangNhap, matKhau);
                }
                else
                {
                    ClearSavedLogin();
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT QuyenHan FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        quyen = result.ToString();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void SaveLogin(string tenDangNhap, string matKhau)
        {
            Properties.Settings.Default.Username = tenDangNhap;
            Properties.Settings.Default.Password = matKhau;
            Properties.Settings.Default.Save();
        }

        private void ClearSavedLogin()
        {
            Properties.Settings.Default.Username = string.Empty;
            Properties.Settings.Default.Password = string.Empty;
            Properties.Settings.Default.Save();
        }

        private void LoadSavedLogin()
        {
            txttk.Text = Properties.Settings.Default.Username;
            txtmk.Text = Properties.Settings.Default.Password;
            cbSaveLogin.Checked = !string.IsNullOrEmpty(txttk.Text) && !string.IsNullOrEmpty(txtmk.Text);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}