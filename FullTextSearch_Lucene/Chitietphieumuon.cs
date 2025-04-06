using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace FullTextSearch_Lucene
{
    public partial class Chitietphieumuon : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ThuVien"].ConnectionString;
        private readonly string luceneIndexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuceneIndex");
        bool them_chi_tiet_phieu_muon_moi = false;
        private int ma_phieu_muon;
        private int ma_doc_gia;
        private DataTable bang_sach;
        public Chitietphieumuon(int ma_phieu_muon, int ma_doc_gia)
        {
            InitializeComponent();
            this.ma_phieu_muon = ma_phieu_muon;
            this.ma_doc_gia = ma_doc_gia;
        }

        internal void LoadData()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ct.*, s.TenSach FROM ChiTietPhieuMuon ct JOIN Sach s ON ct.MaSach = s.MaSach WHERE ct.MaPhieuMuon = @MaPhieuMuon";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieuMuon", ma_phieu_muon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                
                dgvchitietphieumuon.DataSource = dataTable;
                dgvchitietphieumuon.AutoGenerateColumns = false;
            }
        }

        private void Chitietphieumuon_Load(object sender, EventArgs e)
        {
            txtTimKiem.Focus();
            txtmapm.Text = ma_phieu_muon.ToString();
            txtmadg.Text = ma_doc_gia.ToString();
            //Load books
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Sach";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                bang_sach = new DataTable();
                adapter.Fill(bang_sach);
                dgvluoisach.DataSource = dataTable;
            }
            //Get data from database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ct.*, s.TenSach FROM ChiTietPhieuMuon ct JOIN Sach s ON ct.MaSach = s.MaSach WHERE ct.MaPhieuMuon = @MaPhieuMuon";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieuMuon", ma_phieu_muon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvchitietphieumuon.DataSource = dataTable;
                dgvchitietphieumuon.AutoGenerateColumns = false;
            }
            CreateIndex();
        }

        private void dgvluoisach_Click(object sender, EventArgs e)
        {
            if(dgvluoisach.CurrentRow != null)
            {
                Chonsach chonsach = new Chonsach(ma_phieu_muon,0, int.Parse(dgvluoisach.CurrentRow.Cells["MaSach"].Value.ToString()), dgvluoisach.CurrentRow.Cells["TenSach"].Value.ToString(), 0, true, this);
                chonsach.ShowDialog();
            }
        }

        private void dgvchitietphieumuon_Click(object sender, EventArgs e)
        {
            if(dgvchitietphieumuon.CurrentRow != null)
            {
                Chonsach chonsach = new Chonsach(ma_phieu_muon, (int)dgvchitietphieumuon.CurrentRow.Cells["MaChiTiet"].Value, (int)dgvchitietphieumuon.CurrentRow.Cells["MaSachCT"].Value, (string)dgvchitietphieumuon.CurrentRow.Cells["TenSachCT"].Value, (int)dgvchitietphieumuon.CurrentRow.Cells["SoLuongCT"].Value, false, this);
                chonsach.ShowDialog();
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if(dgvchitietphieumuon.Rows.Count == 0)
            {
                bool b = MessageBox.Show("Không có sách nào trong phiếu mượn. Bạn có muốn xóa phiếu mượn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (b)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM PhieuMuon WHERE MaPhieuMuon = @MaPhieuMuon";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaPhieuMuon", ma_phieu_muon);
                        cmd.ExecuteNonQuery();
                    }
                    this.Close();
                }
                Phieumuonsach phieumuonsach = new Phieumuonsach();
                phieumuonsach.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            Search(txtTimKiem.Text);
        }
        private void Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return;
            }

            using (var directory = FSDirectory.Open(new DirectoryInfo(luceneIndexPath)))
            using (var analyzer = new NonUnicodeVietnameseAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
            using (var reader = DirectoryReader.Open(directory))
            {
                var searcher = new IndexSearcher(reader);
                var parser = new MultiFieldQueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48, new[] { "TenSach", "NhaXuatBan", "ViTri", "TenTacGia", "TenTheLoai" }, analyzer);
                var query = parser.Parse(keyword);

                var hits = searcher.Search(query, 10).ScoreDocs;
                var results = new DataTable();
                results.Columns.Add("MaSach");
                results.Columns.Add("TenSach");
                results.Columns.Add("TenTacGia");
                results.Columns.Add("TenTheLoai");
                results.Columns.Add("SoTrang");
                results.Columns.Add("NamXuatBan");
                results.Columns.Add("NhaXuatBan");
                results.Columns.Add("SoLuong");
                results.Columns.Add("ViTri");

                foreach (var hit in hits)
                {
                    var doc = searcher.Doc(hit.Doc);
                    results.Rows.Add(
                        doc.Get("MaSach"),
                        doc.Get("TenSach"),
                        doc.Get("TenTacGia"),
                        doc.Get("TenTheLoai"),
                        doc.Get("SoTrang"),
                        doc.Get("NamXuatBan"),
                        doc.Get("NhaXuatBan"),
                        doc.Get("SoLuong"),
                        doc.Get("ViTri")
                    );
                }

                dgvluoisach.DataSource = results;
            }
        }


        public void CreateIndex()
        {
            using (var directory = FSDirectory.Open(new DirectoryInfo(luceneIndexPath)))
            using (var analyzer = new NonUnicodeVietnameseAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
            using (var writer = new IndexWriter(directory, new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer)))
            {
                writer.DeleteAll(); // Xóa tất cả các chỉ mục cũ
                foreach (DataRow row in bang_sach.Rows)
                {
                    var doc = new Document
                    {
                        new StringField("MaSach", row["MaSach"].ToString(), Field.Store.YES),
                        new TextField("TenSach", row["TenSach"].ToString(), Field.Store.YES),
                        new TextField("TenTacGia", row["TenTacGia"].ToString(), Field.Store.YES),
                        new TextField("TenTheLoai", row["TenTheLoai"].ToString(), Field.Store.YES),
                        new StringField("SoTrang", row["SoTrang"].ToString(), Field.Store.YES),
                        new StringField("NamXuatBan", row["NamXuatBan"].ToString(), Field.Store.YES),
                        new TextField("NhaXuatBan", row["NhaXuatBan"].ToString(), Field.Store.YES),
                        new StringField("SoLuong", row["SoLuong"].ToString(), Field.Store.YES),
                        new TextField("ViTri", row["ViTri"].ToString(), Field.Store.YES)
                    };

                    writer.AddDocument(doc);
                }

                writer.Flush(triggerMerge: false, applyAllDeletes: false);
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            dgvluoisach.DataSource = null;
            dgvluoisach.DataSource = bang_sach;
        }
    }
}
