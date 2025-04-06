using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.QueryParsers.Surround.Query;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace FullTextSearch_Lucene
{
    public partial class Sach : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ThuVien"].ConnectionString;
        private readonly string luceneIndexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuceneIndex");
        private DataTable bang_sach;
        private BindingManagerBase dong_hien_hanh_bang_sach;
        private bool them_sach_moi = false;

        public Sach()
        {
            InitializeComponent();
        }

        private void Sach_Load(object sender, EventArgs e)
        {
            try
            {
                bang_sach = new DataTable();
                LoadData();

                dong_hien_hanh_bang_sach = BindingContext[bang_sach];
                dong_hien_hanh_bang_sach.PositionChanged += dong_hien_hanh_bang_sach_PositionChanged;
                dong_hien_hanh_bang_sach_PositionChanged(sender, e);

                dgvluoisach.AutoGenerateColumns = false;
                dgvluoisach.DataSource = bang_sach;

                for (int i = 0; i < dgvluoisach.RowCount; i++)
                {
                    dgvluoisach.Rows[i].Cells[0].Value = i + 1;
                }

                txtmasach.ReadOnly = true;
                CreateIndex();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi  \n" + ex.Message, "thông báo lỗi");
            }
        }

        private void LoadData()
        {
            dgvluoisach.DataSource = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT * FROM Sach";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(bang_sach);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void dong_hien_hanh_bang_sach_PositionChanged(object sender, EventArgs e)
        {
            if (them_sach_moi)
            {
                return;
            }
            else
            {
                if (dong_hien_hanh_bang_sach.Position >= bang_sach.Rows.Count)
                {
                    return;
                }

                DataRow r = bang_sach.Rows[dong_hien_hanh_bang_sach.Position];
                txtmasach.Text = r["MaSach"].ToString();
                txttensach.Text = r["TenSach"].ToString();
                txttheloai.Text = r["TenTheLoai"].ToString();
                txttg.Text = r["TenTacGia"].ToString();
                dtpnamxb.Value = r["NamXuatBan"] != DBNull.Value ? DateTime.Parse(r["NamXuatBan"].ToString()) : DateTime.Now;
                txtsoluong.Text = r["SoLuong"].ToString();
                txtnxb.Text = r["NhaXuatBan"].ToString();
                txtsotrang.Text = r["SoTrang"].ToString();
                txtvitri.Text = r["ViTri"].ToString();
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            them_sach_moi = true;
            txtmasach.ReadOnly = false;
            txtmasach.Focus();
            txtmasach.Text = txttensach.Text = txttg.Text = txtsoluong.Text = txtnxb.Text = txtsotrang.Text = txtvitri.Text = txttheloai.Text = "";
            dtpnamxb.Value = DateTime.Now;
            btnthem.Visible = btnxoa.Visible = false;  
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Muốn xoá sách này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow r = bang_sach.Rows[dong_hien_hanh_bang_sach.Position];
                r.Delete();
                SaveData();
                dong_hien_hanh_bang_sach_PositionChanged(sender, e);
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r;
                if (them_sach_moi)
                {
                    r = bang_sach.NewRow();
                }
                else
                {
                    r = bang_sach.Rows[dong_hien_hanh_bang_sach.Position];
                }

                r["TenSach"] = txttensach.Text;
                r["TenTheLoai"] = txttheloai.Text;
                r["TenTacGia"] = txttg.Text;
                r["NamXuatBan"] = dtpnamxb.Value;
                r["SoLuong"] = int.Parse(txtsoluong.Text);
                r["NhaXuatBan"] = txtnxb.Text;
                r["SoTrang"] = int.Parse(txtsotrang.Text);
                r["ViTri"] = txtvitri.Text;

                if (them_sach_moi)
                {
                    bang_sach.Rows.Add(r);
                    dong_hien_hanh_bang_sach.Position = dong_hien_hanh_bang_sach.Count - 1;
                }

                SaveData();
                dgvluoisach.DataSource = bang_sach;
                them_sach_moi = false;
                txtmasach.ReadOnly = true;
                MessageBox.Show("đã cập nhật thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi cập nhật dữ liệu " + ex.Message);
                bang_sach.RejectChanges();
            }
        }

        private void SaveData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Sach", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(bang_sach);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            them_sach_moi = false;
            dong_hien_hanh_bang_sach_PositionChanged(sender, e);
            bang_sach.RejectChanges();
            txtTimKiem.Text = "";
            dgvluoisach.DataSource = bang_sach;
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
    }
}
