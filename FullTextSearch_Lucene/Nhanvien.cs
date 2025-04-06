using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Documents;

namespace FullTextSearch_Lucene
{
    public partial class Nhanvien : Form
    {
        BindingManagerBase dong_hien_hanh_bang_nv;
        bool them_nhan_vien_moi = false;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ThuVien"].ConnectionString;
        private readonly string luceneIndexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuceneIndex");
        private DataTable bang_nhanvien;

        public Nhanvien()
        {
            InitializeComponent();
        }

        private void Nhanvien_Load(object sender, EventArgs e)
        {
            try
            {
                bang_nhanvien = new DataTable();
                LoadData();

                dong_hien_hanh_bang_nv = BindingContext[bang_nhanvien];
                dong_hien_hanh_bang_nv.PositionChanged += dong_hien_hanh_bang_nv_PositionChanged;
                dong_hien_hanh_bang_nv_PositionChanged(sender, e);

                dgvluoinhanvien.AutoGenerateColumns = false;
                dgvluoinhanvien.DataSource = bang_nhanvien;

                for (int i = 0; i < dgvluoinhanvien.RowCount; i++)
                {
                    dgvluoinhanvien.Rows[i].Cells[0].Value = i + 1;
                }

                txtmanv.ReadOnly = true;
                CreateIndex();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi  \n" + ex.Message, "thông báo lỗi");
            }
        }

        private void LoadData()
        {
            dgvluoinhanvien.DataSource = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT * FROM NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(bang_nhanvien);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void dong_hien_hanh_bang_nv_PositionChanged(object sender, EventArgs e)
        {
            if (them_nhan_vien_moi)
            {
                return;
            }
            else
            {
                if (dong_hien_hanh_bang_nv.Position >= bang_nhanvien.Rows.Count)
                {
                    return;
                }

                DataRow r = bang_nhanvien.Rows[dong_hien_hanh_bang_nv.Position];
                txtmanv.Text = r["MaNhanVien"].ToString();
                txthoten.Text = r["HoTen"].ToString();
                txtdiachi.Text = r["DiaChi"].ToString();
                txtsdt.Text = r["SoDienThoai"].ToString();
                dtpngaysinh.Value = r["NgaySinh"] != DBNull.Value ? DateTime.Parse(r["NgaySinh"].ToString()) : DateTime.Now;
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            them_nhan_vien_moi = true;
            txtmanv.ReadOnly = false;
            txtmanv.Focus();
            txtmanv.Text = txthoten.Text = txtdiachi.Text = txtsdt.Text = "";
            dtpngaysinh.Value = DateTime.Now;
            btnthem.Visible = btnxoa.Visible = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Muốn xoá nhân viên này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow r = bang_nhanvien.Rows[dong_hien_hanh_bang_nv.Position];
                r.Delete();
                SaveData();
                dong_hien_hanh_bang_nv_PositionChanged(sender, e);
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r;
                if (them_nhan_vien_moi)
                {
                    r = bang_nhanvien.NewRow();
                }
                else
                {
                    r = bang_nhanvien.Rows[dong_hien_hanh_bang_nv.Position];
                }

                r["HoTen"] = txthoten.Text;
                r["DiaChi"] = txtdiachi.Text;
                r["SoDienThoai"] = txtsdt.Text;
                r["NgaySinh"] = dtpngaysinh.Value;

                if (them_nhan_vien_moi)
                {
                    bang_nhanvien.Rows.Add(r);
                    dong_hien_hanh_bang_nv.Position = dong_hien_hanh_bang_nv.Count - 1;
                }

                SaveData();

                them_nhan_vien_moi = false;
                txtmanv.ReadOnly = true;
                MessageBox.Show("đã cập nhật thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi cập nhật dữ liệu " + ex.Message);
                bang_nhanvien.RejectChanges();
            }
        }

        private void SaveData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM NhanVien", conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(bang_nhanvien);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            them_nhan_vien_moi = false;
            dong_hien_hanh_bang_nv_PositionChanged(sender, e);
            bang_nhanvien.RejectChanges();
            txtTimKiem.Text = "";
            dgvluoinhanvien.DataSource = null;
            dgvluoinhanvien.DataSource = bang_nhanvien;
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
                var parser = new MultiFieldQueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48, new[] { "HoTen", "DiaChi", "SoDienThoai", "NgaySinh" }, analyzer);
                var query = parser.Parse(keyword);

                var hits = searcher.Search(query, 10).ScoreDocs;
                var results = new DataTable();
                results.Columns.Add("MaNhanVien");
                results.Columns.Add("HoTen");
                results.Columns.Add("DiaChi");
                results.Columns.Add("SoDienThoai");
                results.Columns.Add("NgaySinh");

                foreach (var hit in hits)
                {
                    var doc = searcher.Doc(hit.Doc);
                    results.Rows.Add(
                        doc.Get("MaNhanVien"),
                        doc.Get("HoTen"),
                        doc.Get("DiaChi"),
                        doc.Get("SoDienThoai"),
                        doc.Get("NgaySinh")
                    );
                }

                dgvluoinhanvien.DataSource = results;
            }
        }

        public void CreateIndex()
        {
            using (var directory = FSDirectory.Open(new DirectoryInfo(luceneIndexPath)))
            using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
            using (var writer = new IndexWriter(directory, new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer)))
            {
                writer.DeleteAll(); // Xóa tất cả các chỉ mục cũ
                foreach (DataRow row in bang_nhanvien.Rows)
                {
                    var doc = new Document
                    {
                        new StringField("MaNhanVien", row["MaNhanVien"].ToString(), Field.Store.YES),
                        new TextField("HoTen", row["HoTen"].ToString(), Field.Store.YES),
                        new TextField("DiaChi", row["DiaChi"].ToString(), Field.Store.YES),
                        new TextField("SoDienThoai", row["SoDienThoai"].ToString(), Field.Store.YES),
                        new TextField("NgaySinh", row["NgaySinh"].ToString(), Field.Store.YES),
                    };

                    writer.AddDocument(doc);
                }

                writer.Flush(triggerMerge: false, applyAllDeletes: false);
            }
        }
    }
}