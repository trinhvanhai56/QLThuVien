using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FullTextSearch_Lucene
{
    public class LuceneIndexer
    {
        private readonly string connectionString = "Data Source=DESKTOP-107KRUE;Initial Catalog=QuanLyThuVien;Integrated Security=True;Encrypt=False";
        private readonly string luceneIndexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuceneIndex");

        public void CreateIndex()
        {
            using (var directory = FSDirectory.Open(new DirectoryInfo(luceneIndexPath)))
            using (var analyzer = new NonUnicodeVietnameseAnalyzer(LuceneVersion.LUCENE_48))
            using (var writer = new IndexWriter(directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer)))
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

            }
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
