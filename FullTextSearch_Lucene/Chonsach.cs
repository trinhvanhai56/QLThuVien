using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FullTextSearch_Lucene
{
    public partial class Chonsach : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ThuVien"].ConnectionString;
        private int ma_pm;
        private int ma_ctpm;
        private int ma_sach;
        private string ten_sach;
        private int sl;
        private bool them_chi_tiet_phieu_muon_moi = false;
        private Chitietphieumuon chitietphieumuon;
        public Chonsach(int ma_pm, int ma_ctpm, int ma_sach, string ten_sach, int v, bool them_ctpm_moi, Chitietphieumuon chitietphieumuon)
        {
            InitializeComponent();
            this.ma_pm = ma_pm;
            this.ma_ctpm = ma_ctpm;
            this.ma_sach = ma_sach;
            this.ten_sach = ten_sach;
            this.sl = v;
            this.them_chi_tiet_phieu_muon_moi = them_ctpm_moi;
            this.chitietphieumuon = chitietphieumuon;
            btnhuy.Visible = !them_ctpm_moi;
        }

        private void Chonsach_Load(object sender, EventArgs e)
        {
            txttensach.Text = ten_sach;
            nbrSoluongsach.Value = sl;
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            //Delete ChiTietPhieuMuon
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM ChiTietPhieuMuon WHERE MaChiTiet = @MaChiTiet";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTiet", ma_ctpm);
                cmd.ExecuteNonQuery();
            }
            chitietphieumuon.LoadData();
            this.Close();
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if(them_chi_tiet_phieu_muon_moi)
            {
                //Add new ChiTietPhieuMuon
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO ChiTietPhieuMuon(MaPhieuMuon, MaSach, SoLuong) VALUES(@MaPhieuMuon, @MaSach, @SoLuong)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaPhieuMuon", ma_pm);
                    cmd.Parameters.AddWithValue("@MaSach", ma_sach);
                    cmd.Parameters.AddWithValue("@SoLuong", nbrSoluongsach.Value);
                    cmd.ExecuteNonQuery();
                }
                chitietphieumuon.LoadData();
            }
            else
            {
                //Update ChiTietPhieuMuon
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE ChiTietPhieuMuon SET SoLuong = @SoLuong WHERE MaChiTiet = @MaChiTiet";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SoLuong", nbrSoluongsach.Value);
                    cmd.Parameters.AddWithValue("@MaChiTiet", ma_ctpm);
                    cmd.ExecuteNonQuery();
                }
                chitietphieumuon.LoadData();
            }
            this.Close();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
