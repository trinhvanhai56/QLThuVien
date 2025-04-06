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
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace FullTextSearch_Lucene
{
    public partial class Docgia : Form
    {
        BindingManagerBase dong_hien_hanh_bang_doc_gia;
        bool them_doc_gia_moi = false;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ThuVien"].ConnectionString;
        private readonly string luceneIndexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuceneIndex");
        private DataTable bang_doc_gia;

        public Docgia()
        {
            InitializeComponent();
        }

        private void Docgia_Load(object sender, EventArgs e)
        {
            try
            {
                bang_doc_gia = new DataTable();
                LoadData();

                dong_hien_hanh_bang_doc_gia = BindingContext[bang_doc_gia];
                dong_hien_hanh_bang_doc_gia.PositionChanged += dong_hien_hanh_bang_doc_gia_PositionChanged;
                dong_hien_hanh_bang_doc_gia_PositionChanged(sender, e);

                dgvluoidg.AutoGenerateColumns = false;
                dgvluoidg.DataSource = bang_doc_gia;

                for (int i = 0; i < dgvluoidg.RowCount; i++)
                {
                    dgvluoidg.Rows[i].Cells[0].Value = i + 1;
                }

                txtmadg.ReadOnly = true;
                CreateIndex();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi  \n" + ex.Message, "thông báo lỗi");
            }
        }

        private void LoadData()
        {
            dgvluoidg.DataSource = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT * FROM DocGia";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(bang_doc_gia);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void dong_hien_hanh_bang_doc_gia_PositionChanged(object sender, EventArgs e)
        {
            if (them_doc_gia_moi)
            {
                return;
            }
            else
            {
                if (dong_hien_hanh_bang_doc_gia.Position >= bang_doc_gia.Rows.Count)
                {
                    return;
                }

                DataRow r = bang_doc_gia.Rows[dong_hien_hanh_bang_doc_gia.Position];
                txtmadg.Text = r["MaDocGia"].ToString();
                txthoten.Text = r["HoTen"].ToString();
                txtdc.Text = r["DiaChi"].ToString();
                txtsodienthoai.Text = r["SoDienThoai"].ToString();
                txtemail.Text = r["Email"].ToString();
                dtpngaysinh.Value = r["NgaySinh"] != DBNull.Value ? DateTime.Parse(r["NgaySinh"].ToString()) : DateTime.Now;
                dtplapthe.Value = r["NgayLapThe"] != DBNull.Value ? DateTime.Parse(r["NgayLapThe"].ToString()) : DateTime.Now;
                dtphethan.Value = r["NgayHetHan"] != DBNull.Value ? DateTime.Parse(r["NgayHetHan"].ToString()) : DateTime.Now;
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            them_doc_gia_moi = true;
            txtmadg.ReadOnly = false;
            txtmadg.Focus();
            txtmadg.Text = txthoten.Text = txtdc.Text = txtsodienthoai.Text = txtemail.Text = "";
            dtpngaysinh.Value = DateTime.Now;
            dtplapthe.Value = DateTime.Now;
            dtphethan.Value = DateTime.Now;
            btnthem.Visible = btnxoa.Visible = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Muốn xoá độc giả này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow r = bang_doc_gia.Rows[dong_hien_hanh_bang_doc_gia.Position];
                r.Delete();
                SaveData();
                dong_hien_hanh_bang_doc_gia_PositionChanged(sender, e);
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r;
                if (them_doc_gia_moi)
                {
                    r = bang_doc_gia.NewRow();
                }
                else
                {
                    r = bang_doc_gia.Rows[dong_hien_hanh_bang_doc_gia.Position];
                }

                r["HoTen"] = txthoten.Text;
                r["DiaChi"] = txtdc.Text;
                r["SoDienThoai"] = txtsodienthoai.Text;
                r["Email"] = txtemail.Text;
                r["NgaySinh"] = dtpngaysinh.Value;
                r["NgayLapThe"] = dtplapthe.Value;
                r["NgayHetHan"] = dtphethan.Value;

                if (them_doc_gia_moi)
                {
                    bang_doc_gia.Rows.Add(r);
                    dong_hien_hanh_bang_doc_gia.Position = dong_hien_hanh_bang_doc_gia.Count - 1;
                }

                SaveData();

                them_doc_gia_moi = false;
                txtmadg.ReadOnly = true;
                MessageBox.Show("đã cập nhật thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi cập nhật dữ liệu " + ex.Message);
                bang_doc_gia.RejectChanges();
            }
        }

        private void SaveData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM DocGia", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(bang_doc_gia);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            them_doc_gia_moi = false;
            dong_hien_hanh_bang_doc_gia_PositionChanged(sender, e);
            bang_doc_gia.RejectChanges();
            txtTimKiem.Text = "";
            dgvluoidg.DataSource = null;
            dgvluoidg.DataSource = bang_doc_gia;
            btnthem.Visible = btnxoa.Visible = true;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
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
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
            using (var reader = DirectoryReader.Open(directory))
            {
                var searcher = new IndexSearcher(reader);
                var parser = new MultiFieldQueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48, new[] { "HoTen", "DiaChi", "SoDienThoai", "Email" }, analyzer);
                var query = parser.Parse(keyword);

                var hits = searcher.Search(query, 10).ScoreDocs;
                var results = new DataTable();
                results.Columns.Add("MaDocGia");
                results.Columns.Add("HoTen");
                results.Columns.Add("DiaChi");
                results.Columns.Add("SoDienThoai");
                results.Columns.Add("Email");   
                results.Columns.Add("NgaySinh");
                results.Columns.Add("NgayLapThe");
                results.Columns.Add("NgayHetHan");

                foreach (var hit in hits)
                {
                    var doc = searcher.Doc(hit.Doc);
                    results.Rows.Add(
                        doc.Get("MaDocGia"),
                        doc.Get("HoTen"),
                        doc.Get("DiaChi"),
                        doc.Get("SoDienThoai"),
                        doc.Get("Email"),
                        doc.Get("NgaySinh"),
                        doc.Get("NgayLapThe"),
                        doc.Get("NgayHetHan")
                    );
                }

                dgvluoidg.DataSource = results;
            }
        }

        public void CreateIndex()
        {
            using (var directory = FSDirectory.Open(new DirectoryInfo(luceneIndexPath)))
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
            using (var writer = new IndexWriter(directory, new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer)))
            {
                writer.DeleteAll(); // Xóa tất cả các chỉ mục cũ
                foreach (DataRow row in bang_doc_gia.Rows)
                {
                    var doc = new Document
                    {
                        new StringField("MaDocGia", row["MaDocGia"].ToString(), Field.Store.YES),
                        new TextField("HoTen", row["HoTen"].ToString(), Field.Store.YES),
                        new TextField("DiaChi", row["DiaChi"].ToString(), Field.Store.YES),
                        new StringField("SoDienThoai", row["SoDienThoai"].ToString(), Field.Store.YES),
                        new TextField("Email", row["Email"].ToString(), Field.Store.YES),
                        new StringField("NgaySinh", row["NgaySinh"].ToString(), Field.Store.YES),
                        new StringField("NgayLapThe", row["NgayLapThe"].ToString(), Field.Store.YES),
                        new StringField("NgayHetHan", row["NgayHetHan"].ToString(), Field.Store.YES)
                    };

                    writer.AddDocument(doc);
                }

                writer.Flush(triggerMerge: false, applyAllDeletes: false);
            }
        }
    }
}