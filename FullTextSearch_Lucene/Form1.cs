using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers.Classic;
using System.Data.SqlClient;

namespace FullTextSearch_Lucene
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (!System.IO.Directory.Exists(DBINDEX)) { 
                System.IO.Directory.CreateDirectory(DBINDEX);
            }
            // nếu folder chưa tồn tại thì chúng ta tạo ra
        }
        private readonly string connectionString = "Data Source=DESKTOP-107KRUE;Initial Catalog=QuanLyThuVien;Integrated Security=True;Encrypt=False";
    
        private string DBINDEX = Application.StartupPath + @"\db";
        private void btnIndexData_Click(object sender, EventArgs e)
        {
            var indexDir = FSDirectory.Open(new DirectoryInfo(DBINDEX));
            var analyzer = new NonUnicodeVietnameseAnalyzer(LuceneVersion.LUCENE_48);
            var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
            using (var writer = new IndexWriter(indexDir, new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer)))
            {
                writer.DeleteAll(); // Xóa tất cả các chỉ mục cũ

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Sach", conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var doc = new Document
                        {
                            new StringField("MaSach", row["MaSach"].ToString(), Field.Store.YES),
                            new TextField("TenSach", row["TenSach"].ToString(), Field.Store.YES),
                            new StringField("TenTheLoai", row["TenTheLoai"].ToString(), Field.Store.YES),
                            new StringField("TenTacGia", row["TenTacGia"].ToString(), Field.Store.YES),
                            new StringField("NamXuatBan", row["NamXuatBan"].ToString(), Field.Store.YES),
                            new TextField("NhaXuatBan", row["NhaXuatBan"].ToString(), Field.Store.YES),
                            new Int32Field("SoTrang", Convert.ToInt32(row["SoTrang"]), Field.Store.YES),
                            new Int32Field("SoLuong", Convert.ToInt32(row["SoLuong"]), Field.Store.YES),
                            new TextField("ViTri", row["ViTri"].ToString(), Field.Store.YES)
                        };
                        writer.AddDocument(doc);
                    }
                }

                writer.Commit();
                writer.Dispose();

                MessageBox.Show("Tạo index hoàn tất.");
            }
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var keywords = txtQuery.Text;
                using (var directory = FSDirectory.Open(new DirectoryInfo(DBINDEX)))
                using (var analyzer = new NonUnicodeVietnameseAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
                using (var reader = DirectoryReader.Open(directory))
                {
                    var searcher = new IndexSearcher(reader);
                    var parser = new MultiFieldQueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48, new[] { "TenSach", "NhaXuatBan", "ViTri" }, analyzer);
                    var query = parser.Parse(keywords);

                    var hits = searcher.Search(query, 10).ScoreDocs;
                    var results = new DataTable();
                    results.Columns.Add("MaSach");
                    results.Columns.Add("TenSach");
                    results.Columns.Add("TenTacGia");
                    results.Columns.Add("SoTrang");
                    results.Columns.Add("NamXuatBan");
                    results.Columns.Add("NhaXuatBan");
                    results.Columns.Add("SoLuong");
                    results.Columns.Add("ViTri");
                    foreach (var hit in hits)
                    {
                        var doc = searcher.Doc(hit.Doc);
                        results.Rows.Add(doc.Get("MaSach"), doc.Get("TenSach"), doc.Get("MaTacGia"), doc.Get("SoTrang"), doc.Get("NamXuatBan"), doc.Get("NhaXuatBan"), doc.Get("SoLuong"), doc.Get("ViTri"));
                    }

                    dgvluoisach.DataSource = results;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string BirthDay { get; set; }
        public string Gender { get; set; }
        public string BirthPlace { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public string SearchData {
            get {
                return $"{Name} {BirthDay} {Address}";// gom thông tin 3 trường vào 1 để tí mình index
            }
        }
    }

    public class NonUnicodeVietnameseAnalyzer : Analyzer
    {
        private readonly Lucene.Net.Util.LuceneVersion _version;


        public NonUnicodeVietnameseAnalyzer(Lucene.Net.Util.LuceneVersion version)
        {
            _version = version;

        }

        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            var tokenizer = new StandardTokenizer(_version, reader);

            TokenStream stream = new StandardFilter(_version, tokenizer);
            stream = new LowerCaseFilter(_version, stream);
            stream = new StopFilter(_version, stream, StopAnalyzer.ENGLISH_STOP_WORDS_SET);
            stream = new ASCIIFoldingFilter(stream);

            return new TokenStreamComponents(tokenizer, stream);
        }
    }
}
