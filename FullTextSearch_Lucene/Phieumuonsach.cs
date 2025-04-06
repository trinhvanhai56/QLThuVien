using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace FullTextSearch_Lucene
{
    public partial class Phieumuonsach : Form
    {
        DataTable bang_phieu_muon;
        Database bang_doc_gia;

        bool them_phieu_muon_moi = false;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ThuVien"].ConnectionString;
        private readonly string luceneIndexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuceneIndex");

        public Phieumuonsach()
        {
            InitializeComponent();
        }

        private void Phieumuonsach_Load(object sender, EventArgs e)
        {
            try
            {
                Loadbangphieumuon();
                
                if (Login.quyen == "DocGia")
                {
                    btnxoa.Visible = false;
                    dtpngaytra.Enabled = false;
                    txttinhtrang.Text = "Đang mượn";
                    txttinhtrang.Enabled = false;
                }
                CreateIndex();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi  \n" + ex.Message, "thông báo lỗi");
            }
        }

        private void Loadbangphieumuon()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PhieuMuon", conn);
                bang_phieu_muon = new DataTable();
                adapter.Fill(bang_phieu_muon);
                dgvluoipm.DataSource = bang_phieu_muon;
                txttinhtrang.Enabled = true;
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            them_phieu_muon_moi = true;
            txtmapm.Text = "";
            txtdg.Focus();
            txtdg.Text = "";
            btnchitiet.Enabled = false;
            txttinhtrang.Enabled = true;
            txttinhtrang.Text = "Đang mượn";
            dtpngaymuon.Value = DateTime.Now;
            dtpngaytra.Value = DateTime.Now.AddDays(7);
            btnthem.Visible = btnxoa.Visible = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("bạn có chắc chắn muốn xóa không", "thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
           {
                try
                {
                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                    //remove borrowed details
                    SqlCommand cmd = new SqlCommand("DELETE FROM ChiTietPhieuMuon WHERE MaPhieuMuon = @MaPhieuMuon",conn);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM PhieuMuon WHERE MaPhieuMuon = @MaPhieuMuon";
                    cmd.Parameters.AddWithValue("@MaPhieuMuon", txtmapm.Text);
                    cmd.ExecuteNonQuery();
                    
                    conn.Close();
                    MessageBox.Show("đã xóa thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("lỗi xóa dữ liệu " + ex.Message);
                    bang_phieu_muon.RejectChanges();
                }
           }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
             if (them_phieu_muon_moi)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO PhieuMuon(MaDocGia, NgayMuon, TinhTrang) OUTPUT INSERTED.MaPhieuMuon VALUES(@MaDocGia, @NgayMuon, @TinhTrang)", conn);
                        cmd.Parameters.AddWithValue("@MaDocGia", txtdg.Text);
                        cmd.Parameters.AddWithValue("@NgayMuon", dtpngaymuon.Value);
                        cmd.Parameters.AddWithValue("@TinhTrang", txttinhtrang.Text);
                        var maPhieuMuon = cmd.ExecuteScalar();
                        
                    }
                }
                else
                {
                    if (Login.quyen == "DocGia")
                    {
                        MessageBox.Show("Bạn không có quyền cập nhật thông tin phiếu mượn");
                        return;
                    }
                    if (txttinhtrang.Text == "Đã trả" && dtpngaytra.Value < dtpngaymuon.Value)
                    {
                        MessageBox.Show("Ngày trả không hợp lệ");
                        return;
                    }
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE PhieuMuon SET NgayMuon = @NgayMuon, TinhTrang = @TinhTrang WHERE MaPhieuMuon = @MaPhieuMuon", connection);
                        cmd.Parameters.AddWithValue("@MaPhieuMuon", txtmapm.Text);
                        cmd.Parameters.AddWithValue("@NgayMuon", dtpngaymuon.Value);
                        if (txttinhtrang.Text == "Đã trả")
                        {
                            cmd.CommandText = "UPDATE PhieuMuon SET NgayMuon = @NgayMuon, NgayTra = @NgayTra, TinhTrang = @TinhTrang WHERE MaPhieuMuon = @MaPhieuMuon";
                            cmd.Parameters.AddWithValue("@NgayTra", dtpngaytra.Value);
                        }
                        cmd.Parameters.AddWithValue("@TinhTrang", txttinhtrang.Text);
                        cmd.ExecuteNonQuery();
                        //Update bang_phieu_muon
                        Loadbangphieumuon();
                    }
                }
                them_phieu_muon_moi = false;
                txtmapm.ReadOnly = true;
                Loadbangphieumuon();
                MessageBox.Show("đã cập nhật thành công");  
                 CreateIndex();
            btnthem.Visible = btnxoa.Visible = true;
           
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            them_phieu_muon_moi = false;
            txtTimKiem.Text = "";
            dgvluoipm.DataSource = null;
            dgvluoipm.DataSource = bang_doc_gia;
            btnthem.Visible = btnxoa.Visible = true;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text;
            Search(keyword);
        }

        private void Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return;
            }

            using (var directory = FSDirectory.Open(new DirectoryInfo(luceneIndexPath)))
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
            using (var reader = DirectoryReader.Open(directory))
            {
                var searcher = new IndexSearcher(reader);
                var parser = new MultiFieldQueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48, new[] { "MaPhieuMuon", "MaDocGia", "NgayMuon", "NgayTra", "TinhTrang" }, analyzer);
                var query = parser.Parse(keyword);

                var hits = searcher.Search(query, 10).ScoreDocs;
                var results = new DataTable();
                results.Columns.Add("MaPhieuMuon");
                results.Columns.Add("MaDocGia");
                results.Columns.Add("NgayMuon");
                results.Columns.Add("NgayTra");
                results.Columns.Add("TinhTrang");

                foreach (var hit in hits)
                {
                    var doc = searcher.Doc(hit.Doc);
                    results.Rows.Add(
                        doc.Get("MaPhieuMuon"),
                        doc.Get("MaDocGia"),
                        doc.Get("NgayMuon"),
                        doc.Get("NgayTra"),
                        doc.Get("TinhTrang")
                    );
                }

                dgvluoipm.DataSource = results;
            }
        }

        public void CreateIndex()
        {
            using (var directory = FSDirectory.Open(new DirectoryInfo(luceneIndexPath)))
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
            using (var writer = new IndexWriter(directory, new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer)))
            {
                //Get data from database
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PhieuMuon", conn);
                    bang_phieu_muon = new DataTable();
                    adapter.Fill(bang_phieu_muon);
                }
                writer.DeleteAll(); // Xóa tất cả các chỉ mục cũ
                foreach (DataRow row in bang_phieu_muon.Rows)
                {
                    var doc = new Document
                    {
                        new StringField("MaPhieuMuon", row["MaPhieuMuon"].ToString(), Field.Store.YES),
                        new StringField("MaDocGia", row["MaDocGia"].ToString(), Field.Store.YES),
                        new TextField("NgayMuon", row["NgayMuon"].ToString(), Field.Store.YES),
                        new TextField("NgayTra", row["NgayTra"].ToString(), Field.Store.YES),
                        new StringField("TinhTrang", row["TinhTrang"].ToString(), Field.Store.YES),
                    };
                    writer.AddDocument(doc);
                }

                writer.Flush(triggerMerge: false, applyAllDeletes: false);
            }
        }

        private void dgvluoipm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvluoipm_Click(object sender, EventArgs e)
        {
            if(dgvluoipm.CurrentRow != null && dgvluoipm.CurrentRow.Index > -1)
            {
                txtmapm.Text = dgvluoipm.CurrentRow.Cells[0].Value.ToString();
                txtdg.Text = dgvluoipm.CurrentRow.Cells[1].Value.ToString();
                dtpngaymuon.Value = DateTime.Parse(dgvluoipm.CurrentRow.Cells[2].Value.ToString());
                if(dgvluoipm.CurrentRow.Cells[3].Value.ToString() != "")
                {
                    dtpngaytra.Value = DateTime.Parse(dgvluoipm.CurrentRow.Cells[3].Value.ToString());
                }
                else
                {
                    dtpngaytra.Value = DateTime.Now;
                }
                txttinhtrang.Text = dgvluoipm.CurrentRow.Cells[4].Value.ToString();

                btnchitiet.Enabled = true;
                btnthem.Visible = btnxoa.Visible = true;
                if (Login.quyen == "DocGia")
                {
                    return;
                }
                if (dgvluoipm.CurrentRow.Cells[4].Value.ToString() == "Đã trả")
                {
                    txttinhtrang.Enabled = false;
                }
                else
                {
                    txttinhtrang.Enabled = true;
                }
            }
        }

        private void btnchitiet_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtmapm.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu mượn cần xem chi tiết");
                return;
            }
            this.Hide();
            Chitietphieumuon frm = new Chitietphieumuon(int.Parse(txtmapm.Text), int.Parse(txtdg.Text));
            frm.Show();
        }
    }
}
