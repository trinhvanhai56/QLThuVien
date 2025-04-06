using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullTextSearch_Lucene
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }  

        private void thôngTinSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void độcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Docgia frm2 = new Docgia();
            frm2.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nhanvien frm1 = new Nhanvien();
            frm1.Show();
        }

        private void sáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sach frm3 = new Sach();
            frm3.Show();
        }

        private void mượnSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Phieumuonsach frm4 = new Phieumuonsach();
            frm4.Show();
        }


        private void Main_Load(object sender, EventArgs e)
        {
            mnu_user.Visible = true;           
            Login fl = new Login();
            fl.ShowDialog();
            if (Login.quyen == "Admin")
            {
                mnu_user.Visible = true;
                quảnLýToolStripMenuItem.Visible = true;
                mượnSáchToolStripMenuItem.Visible = true;
                mnu_user.Text = "Đăng xuất admin";
            }
            else if (Login.quyen == "DocGia")
            {
                mnu_user.Visible = true ;
                quảnLýToolStripMenuItem.Visible = false;
                mnu_user.Text = "Đăng xuất guest";
            }
        }

        private void mnu_user_Click(object sender, EventArgs e)
        {
            Main_Load(sender, e);
        }
    }
}
