namespace FullTextSearch_Lucene
{
    partial class Chonsach
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
            this.txttensach = new System.Windows.Forms.Label();
            this.txtSoluong = new System.Windows.Forms.Label();
            this.nbrSoluongsach = new System.Windows.Forms.NumericUpDown();
            this.btnthoat = new System.Windows.Forms.Button();
            this.btnluu = new System.Windows.Forms.Button();
            this.btnhuy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nbrSoluongsach)).BeginInit();
            this.SuspendLayout();
            // 
            // txttensach
            // 
            this.txttensach.AutoSize = true;
            this.txttensach.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttensach.Location = new System.Drawing.Point(12, 9);
            this.txttensach.Name = "txttensach";
            this.txttensach.Size = new System.Drawing.Size(97, 13);
            this.txttensach.TabIndex = 2;
            this.txttensach.Text = "Mã phiếu mượn:";
            // 
            // txtSoluong
            // 
            this.txtSoluong.AutoSize = true;
            this.txtSoluong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoluong.Location = new System.Drawing.Point(13, 38);
            this.txtSoluong.Name = "txtSoluong";
            this.txtSoluong.Size = new System.Drawing.Size(61, 13);
            this.txtSoluong.TabIndex = 3;
            this.txtSoluong.Text = "Số lượng:";
            // 
            // nbrSoluongsach
            // 
            this.nbrSoluongsach.Location = new System.Drawing.Point(80, 36);
            this.nbrSoluongsach.Name = "nbrSoluongsach";
            this.nbrSoluongsach.Size = new System.Drawing.Size(73, 20);
            this.nbrSoluongsach.TabIndex = 4;
            // 
            // btnthoat
            // 
            this.btnthoat.BackColor = System.Drawing.Color.Transparent;
            this.btnthoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnthoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthoat.ForeColor = System.Drawing.Color.Red;
            this.btnthoat.Location = new System.Drawing.Point(115, 62);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(85, 23);
            this.btnthoat.TabIndex = 49;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.UseVisualStyleBackColor = false;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // btnluu
            // 
            this.btnluu.BackColor = System.Drawing.Color.Transparent;
            this.btnluu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnluu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnluu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnluu.Location = new System.Drawing.Point(16, 62);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(93, 23);
            this.btnluu.TabIndex = 48;
            this.btnluu.Text = "Lưu";
            this.btnluu.UseVisualStyleBackColor = false;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.BackColor = System.Drawing.Color.Transparent;
            this.btnhuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.ForeColor = System.Drawing.Color.Purple;
            this.btnhuy.Location = new System.Drawing.Point(159, 36);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(41, 23);
            this.btnhuy.TabIndex = 50;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.UseVisualStyleBackColor = false;
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // Chonsach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 94);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.btnluu);
            this.Controls.Add(this.nbrSoluongsach);
            this.Controls.Add(this.txtSoluong);
            this.Controls.Add(this.txttensach);
            this.Name = "Chonsach";
            this.Text = "ChonSach";
            this.Load += new System.EventHandler(this.Chonsach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nbrSoluongsach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txttensach;
        private System.Windows.Forms.Label txtSoluong;
        private System.Windows.Forms.NumericUpDown nbrSoluongsach;
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.Button btnluu;
        private System.Windows.Forms.Button btnhuy;
    }
}