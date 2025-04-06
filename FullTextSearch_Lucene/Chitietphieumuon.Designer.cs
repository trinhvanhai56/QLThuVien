using System.Drawing;
using System.Windows.Forms;

namespace FullTextSearch_Lucene
{
    partial class Chitietphieumuon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtmapm = new System.Windows.Forms.TextBox();
            this.txtmadg = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btntimkiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvluoisach = new System.Windows.Forms.DataGridView();
            this.MaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenTheLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenTacGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViTri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamXuatBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NhaXuatBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTrang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnluu = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvchitietphieumuon = new System.Windows.Forms.DataGridView();
            this.MaPhieuMuon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaChiTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaSachCT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSachCT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongCT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnhuy = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvluoisach)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvchitietphieumuon)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(251, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHI TIẾT PHIẾU MƯỢN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã phiếu mượn:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(267, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tên độc giả:";
            // 
            // txtmapm
            // 
            this.txtmapm.Enabled = false;
            this.txtmapm.Location = new System.Drawing.Point(115, 44);
            this.txtmapm.Name = "txtmapm";
            this.txtmapm.ReadOnly = true;
            this.txtmapm.Size = new System.Drawing.Size(128, 20);
            this.txtmapm.TabIndex = 2;
            // 
            // txtmadg
            // 
            this.txtmadg.Enabled = false;
            this.txtmadg.Location = new System.Drawing.Point(353, 43);
            this.txtmadg.Name = "txtmadg";
            this.txtmadg.ReadOnly = true;
            this.txtmadg.Size = new System.Drawing.Size(140, 20);
            this.txtmadg.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnhuy);
            this.groupBox1.Controls.Add(this.btntimkiem);
            this.groupBox1.Controls.Add(this.txtTimKiem);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dgvluoisach);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 277);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn sách";
            // 
            // btntimkiem
            // 
            this.btntimkiem.BackColor = System.Drawing.Color.Transparent;
            this.btntimkiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btntimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntimkiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btntimkiem.Location = new System.Drawing.Point(363, 19);
            this.btntimkiem.Name = "btntimkiem";
            this.btntimkiem.Size = new System.Drawing.Size(68, 23);
            this.btntimkiem.TabIndex = 45;
            this.btntimkiem.Text = "Tìm kiếm";
            this.btntimkiem.UseVisualStyleBackColor = false;
            this.btntimkiem.Click += new System.EventHandler(this.btntimkiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.ForeColor = System.Drawing.Color.Teal;
            this.txtTimKiem.Location = new System.Drawing.Point(103, 19);
            this.txtTimKiem.Multiline = true;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(254, 23);
            this.txtTimKiem.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Nhập nội dung :";
            // 
            // dgvluoisach
            // 
            this.dgvluoisach.AllowUserToAddRows = false;
            this.dgvluoisach.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvluoisach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvluoisach.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaSach,
            this.TenSach,
            this.TenTheLoai,
            this.TenTacGia,
            this.ViTri,
            this.NamXuatBan,
            this.NhaXuatBan,
            this.SoTrang,
            this.SoLuong});
            this.dgvluoisach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dgvluoisach.Location = new System.Drawing.Point(6, 53);
            this.dgvluoisach.Name = "dgvluoisach";
            this.dgvluoisach.RowHeadersVisible = false;
            this.dgvluoisach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvluoisach.Size = new System.Drawing.Size(475, 218);
            this.dgvluoisach.TabIndex = 34;
            this.dgvluoisach.Click += new System.EventHandler(this.dgvluoisach_Click);
            // 
            // MaSach
            // 
            this.MaSach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MaSach.DataPropertyName = "MaSach";
            this.MaSach.HeaderText = "Mã Sách";
            this.MaSach.Name = "MaSach";
            this.MaSach.ToolTipText = "MaSach";
            this.MaSach.Visible = false;
            // 
            // TenSach
            // 
            this.TenSach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenSach.DataPropertyName = "TenSach";
            this.TenSach.HeaderText = "Tên Sách";
            this.TenSach.Name = "TenSach";
            this.TenSach.ToolTipText = "TenSach";
            // 
            // TenTheLoai
            // 
            this.TenTheLoai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TenTheLoai.DataPropertyName = "TenTheLoai";
            this.TenTheLoai.HeaderText = "Thể loại";
            this.TenTheLoai.Name = "TenTheLoai";
            this.TenTheLoai.ToolTipText = "TheLoai";
            this.TenTheLoai.Width = 54;
            // 
            // TenTacGia
            // 
            this.TenTacGia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TenTacGia.DataPropertyName = "TenTacGia";
            this.TenTacGia.HeaderText = "Tác Giả";
            this.TenTacGia.Name = "TenTacGia";
            this.TenTacGia.ToolTipText = "TacGia";
            this.TenTacGia.Width = 54;
            // 
            // ViTri
            // 
            this.ViTri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ViTri.DataPropertyName = "ViTri";
            this.ViTri.HeaderText = "Vị trí";
            this.ViTri.Name = "ViTri";
            this.ViTri.ToolTipText = "ViTri";
            this.ViTri.Width = 56;
            // 
            // NamXuatBan
            // 
            this.NamXuatBan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.NamXuatBan.DataPropertyName = "NamXuatBan";
            this.NamXuatBan.HeaderText = "Năm xuất bản";
            this.NamXuatBan.Name = "NamXuatBan";
            this.NamXuatBan.ToolTipText = "NamXuatBan";
            this.NamXuatBan.Visible = false;
            // 
            // NhaXuatBan
            // 
            this.NhaXuatBan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.NhaXuatBan.DataPropertyName = "NhaXuatBan";
            this.NhaXuatBan.HeaderText = "Nhà Xuất Bản";
            this.NhaXuatBan.Name = "NhaXuatBan";
            this.NhaXuatBan.Width = 102;
            // 
            // SoTrang
            // 
            this.SoTrang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SoTrang.DataPropertyName = "SoTrang";
            this.SoTrang.HeaderText = "Số Trang";
            this.SoTrang.Name = "SoTrang";
            this.SoTrang.Visible = false;
            // 
            // SoLuong
            // 
            this.SoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SoLuong.DataPropertyName = "SoLuong";
            this.SoLuong.HeaderText = "Số Lượng";
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnluu);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.dgvchitietphieumuon);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(514, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 300);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi tiết phiếu mượn";
            // 
            // btnluu
            // 
            this.btnluu.BackColor = System.Drawing.Color.Transparent;
            this.btnluu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnluu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnluu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnluu.Location = new System.Drawing.Point(173, 275);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(123, 23);
            this.btnluu.TabIndex = 46;
            this.btnluu.Text = "Lưu";
            this.btnluu.UseVisualStyleBackColor = false;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button1.Location = new System.Drawing.Point(363, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Tìm kiếm";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // dgvchitietphieumuon
            // 
            this.dgvchitietphieumuon.AllowUserToAddRows = false;
            this.dgvchitietphieumuon.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvchitietphieumuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvchitietphieumuon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaPhieuMuon,
            this.MaChiTiet,
            this.MaSachCT,
            this.TenSachCT,
            this.SoLuongCT});
            this.dgvchitietphieumuon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dgvchitietphieumuon.Location = new System.Drawing.Point(6, 23);
            this.dgvchitietphieumuon.Name = "dgvchitietphieumuon";
            this.dgvchitietphieumuon.RowHeadersVisible = false;
            this.dgvchitietphieumuon.Size = new System.Drawing.Size(290, 246);
            this.dgvchitietphieumuon.TabIndex = 34;
            this.dgvchitietphieumuon.Click += new System.EventHandler(this.dgvchitietphieumuon_Click);
            // 
            // MaPhieuMuon
            // 
            this.MaPhieuMuon.DataPropertyName = "MaPhieuMuon";
            this.MaPhieuMuon.HeaderText = "Mã PM";
            this.MaPhieuMuon.Name = "MaPhieuMuon";
            this.MaPhieuMuon.Visible = false;
            // 
            // MaChiTiet
            // 
            this.MaChiTiet.DataPropertyName = "MaChiTiet";
            this.MaChiTiet.HeaderText = "Mã CTPM";
            this.MaChiTiet.Name = "MaChiTiet";
            this.MaChiTiet.Visible = false;
            // 
            // MaSachCT
            // 
            this.MaSachCT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MaSachCT.DataPropertyName = "MaSach";
            this.MaSachCT.HeaderText = "Mã Sách";
            this.MaSachCT.Name = "MaSachCT";
            this.MaSachCT.ToolTipText = "MaSach";
            this.MaSachCT.Width = 82;
            // 
            // TenSachCT
            // 
            this.TenSachCT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenSachCT.DataPropertyName = "TenSach";
            this.TenSachCT.HeaderText = "Tên Sách";
            this.TenSachCT.Name = "TenSachCT";
            this.TenSachCT.ToolTipText = "TenSach";
            // 
            // SoLuongCT
            // 
            this.SoLuongCT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SoLuongCT.DataPropertyName = "SoLuong";
            this.SoLuongCT.HeaderText = "Số Lượng";
            this.SoLuongCT.Name = "SoLuongCT";
            this.SoLuongCT.Width = 86;
            // 
            // btnhuy
            // 
            this.btnhuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.ForeColor = System.Drawing.Color.Purple;
            this.btnhuy.Location = new System.Drawing.Point(437, 19);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(44, 23);
            this.btnhuy.TabIndex = 46;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.UseVisualStyleBackColor = true;
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // Chitietphieumuon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 357);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtmadg);
            this.Controls.Add(this.txtmapm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Chitietphieumuon";
            this.Text = "Chi tiết phiếu mượn";
            this.Load += new System.EventHandler(this.Chitietphieumuon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvluoisach)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvchitietphieumuon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtmapm;
        private System.Windows.Forms.TextBox txtmadg;
        private System.Windows.Forms.GroupBox groupBox1;
        private DataGridView dgvluoisach;
        private DataGridViewTextBoxColumn MaSach;
        private DataGridViewTextBoxColumn TenSach;
        private DataGridViewTextBoxColumn TenTheLoai;
        private DataGridViewTextBoxColumn TenTacGia;
        private DataGridViewTextBoxColumn ViTri;
        private DataGridViewTextBoxColumn NamXuatBan;
        private DataGridViewTextBoxColumn NhaXuatBan;
        private DataGridViewTextBoxColumn SoTrang;
        private DataGridViewTextBoxColumn SoLuong;
        private Button btntimkiem;
        private TextBox txtTimKiem;
        private Label label8;
        private GroupBox groupBox2;
        private Button button1;
        private DataGridView dgvchitietphieumuon;
        private Button btnluu;
        private DataGridViewTextBoxColumn MaPhieuMuon;
        private DataGridViewTextBoxColumn MaChiTiet;
        private DataGridViewTextBoxColumn MaSachCT;
        private DataGridViewTextBoxColumn TenSachCT;
        private DataGridViewTextBoxColumn SoLuongCT;
        private Button btnhuy;
    }
}