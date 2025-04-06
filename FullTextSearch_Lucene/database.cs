using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FullTextSearch_Lucene
{
    class Database : DataTable
    {
        SqlConnection ketnoi;
        SqlDataAdapter bo_doc_ghi;
        string tenbang;
        string chuoitruyvan;
        public static string chuoi_ket_noi = "Data Source=DESKTOP-107KRUE;Initial Catalog=QuanLyThuVien;Integrated Security=True;Encrypt=False";

        private void tao_ket_noi()
        {
            if (ketnoi == null)
                ketnoi = new SqlConnection(chuoi_ket_noi);
        }
        public Database(): base()
        {

        }
        public Database(string ptenbang): base(ptenbang)
        {            
            tenbang = ptenbang;
            doc_bang();
        }
        
        public Database(string ptenbang, string pcautruyvan) : base(ptenbang)
        {
            tenbang = ptenbang;
            chuoitruyvan = pcautruyvan;
            doc_bang();
        }
        public void doc_bang()
        {
            tao_ket_noi();
            if (chuoitruyvan == null || chuoitruyvan == "")
                chuoitruyvan = "select * from " + tenbang;
            bo_doc_ghi = new SqlDataAdapter(chuoitruyvan, ketnoi);
            bo_doc_ghi.FillSchema(this, SchemaType.Mapped);
            bo_doc_ghi.Fill(this);
            bo_doc_ghi.SelectCommand.CommandText = "select * from " + tenbang;
            SqlCommandBuilder bophatsinh = new SqlCommandBuilder(bo_doc_ghi);
        }
        public bool ghi()
        {
            try
            {
                bo_doc_ghi.Update(this);
                this.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi cap nhat dl " + ex.Message);
                this.RejectChanges();
                return false;
            }
        }
        public void khong()
        {
            this.RejectChanges();
        }
    }
}
