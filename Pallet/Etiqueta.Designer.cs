namespace Pallet
{
    partial class Etiqueta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbRodape = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btLimpar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbTotalCaixa = new System.Windows.Forms.Label();
            this.lbCaixa = new System.Windows.Forms.Label();
            this.gridErro = new System.Windows.Forms.DataGridView();
            this.gridPallet = new System.Windows.Forms.DataGridView();
            this.lbAviso = new System.Windows.Forms.Label();
            this.lbBox = new System.Windows.Forms.Label();
            this.txtPallet = new System.Windows.Forms.TextBox();
            this.lbPO = new System.Windows.Forms.Label();
            this.txtPO = new System.Windows.Forms.TextBox();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridErro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbRodape
            // 
            this.lbRodape.AutoSize = true;
            this.lbRodape.BackColor = System.Drawing.Color.White;
            this.lbRodape.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRodape.ForeColor = System.Drawing.Color.Black;
            this.lbRodape.Location = new System.Drawing.Point(12, 527);
            this.lbRodape.Name = "lbRodape";
            this.lbRodape.Size = new System.Drawing.Size(66, 20);
            this.lbRodape.TabIndex = 41;
            this.lbRodape.Text = "Rodapé";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.groupBox1.Controls.Add(this.btLimpar);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lbPO);
            this.groupBox1.Controls.Add(this.txtPO);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 481);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PALLET";
            // 
            // btLimpar
            // 
            this.btLimpar.BackColor = System.Drawing.Color.MintCream;
            this.btLimpar.Location = new System.Drawing.Point(163, 40);
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.Size = new System.Drawing.Size(75, 23);
            this.btLimpar.TabIndex = 10;
            this.btLimpar.Text = "Limpar";
            this.btLimpar.UseVisualStyleBackColor = false;
            this.btLimpar.Click += new System.EventHandler(this.btLimpar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbTotalCaixa);
            this.groupBox2.Controls.Add(this.lbCaixa);
            this.groupBox2.Controls.Add(this.gridErro);
            this.groupBox2.Controls.Add(this.gridPallet);
            this.groupBox2.Controls.Add(this.lbAviso);
            this.groupBox2.Controls.Add(this.lbBox);
            this.groupBox2.Controls.Add(this.txtPallet);
            this.groupBox2.Location = new System.Drawing.Point(9, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 400);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "INFORMAÇÕES";
            // 
            // lbTotalCaixa
            // 
            this.lbTotalCaixa.AutoSize = true;
            this.lbTotalCaixa.ForeColor = System.Drawing.Color.Blue;
            this.lbTotalCaixa.Location = new System.Drawing.Point(56, 61);
            this.lbTotalCaixa.Name = "lbTotalCaixa";
            this.lbTotalCaixa.Size = new System.Drawing.Size(13, 13);
            this.lbTotalCaixa.TabIndex = 49;
            this.lbTotalCaixa.Text = "0";
            // 
            // lbCaixa
            // 
            this.lbCaixa.AutoSize = true;
            this.lbCaixa.Location = new System.Drawing.Point(6, 61);
            this.lbCaixa.Name = "lbCaixa";
            this.lbCaixa.Size = new System.Drawing.Size(54, 13);
            this.lbCaixa.TabIndex = 48;
            this.lbCaixa.Text = "CAIXA(S):";
            // 
            // gridErro
            // 
            this.gridErro.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridErro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridErro.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.gridErro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridErro.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridErro.Location = new System.Drawing.Point(244, 111);
            this.gridErro.Name = "gridErro";
            this.gridErro.ReadOnly = true;
            this.gridErro.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridErro.RowHeadersVisible = false;
            this.gridErro.Size = new System.Drawing.Size(225, 283);
            this.gridErro.TabIndex = 46;
            this.gridErro.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridErro_MouseClick);
            // 
            // gridPallet
            // 
            this.gridPallet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPallet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridPallet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.gridPallet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPallet.Location = new System.Drawing.Point(9, 111);
            this.gridPallet.Name = "gridPallet";
            this.gridPallet.ReadOnly = true;
            this.gridPallet.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridPallet.RowHeadersVisible = false;
            this.gridPallet.Size = new System.Drawing.Size(225, 283);
            this.gridPallet.TabIndex = 45;
            this.gridPallet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridPallet_MouseClick);
            // 
            // lbAviso
            // 
            this.lbAviso.AutoSize = true;
            this.lbAviso.BackColor = System.Drawing.Color.Transparent;
            this.lbAviso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAviso.ForeColor = System.Drawing.Color.Red;
            this.lbAviso.Location = new System.Drawing.Point(6, 85);
            this.lbAviso.Name = "lbAviso";
            this.lbAviso.Size = new System.Drawing.Size(56, 15);
            this.lbAviso.TabIndex = 40;
            this.lbAviso.Text = "Msg erro";
            this.lbAviso.Visible = false;
            // 
            // lbBox
            // 
            this.lbBox.AutoSize = true;
            this.lbBox.Location = new System.Drawing.Point(6, 18);
            this.lbBox.Name = "lbBox";
            this.lbBox.Size = new System.Drawing.Size(47, 13);
            this.lbBox.TabIndex = 6;
            this.lbBox.Text = "PALLET";
            // 
            // txtPallet
            // 
            this.txtPallet.Location = new System.Drawing.Point(9, 34);
            this.txtPallet.Name = "txtPallet";
            this.txtPallet.Size = new System.Drawing.Size(209, 20);
            this.txtPallet.TabIndex = 5;
            this.txtPallet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_KeyPress);
            // 
            // lbPO
            // 
            this.lbPO.AutoSize = true;
            this.lbPO.Location = new System.Drawing.Point(6, 24);
            this.lbPO.Name = "lbPO";
            this.lbPO.Size = new System.Drawing.Size(29, 13);
            this.lbPO.TabIndex = 1;
            this.lbPO.Text = "PO#";
            // 
            // txtPO
            // 
            this.txtPO.Location = new System.Drawing.Point(6, 40);
            this.txtPO.MaxLength = 10;
            this.txtPO.Name = "txtPO";
            this.txtPO.Size = new System.Drawing.Size(132, 20);
            this.txtPO.TabIndex = 0;
            // 
            // imgLogo
            // 
            this.imgLogo.Image = global::Pallet.Properties.Resources.logo;
            this.imgLogo.Location = new System.Drawing.Point(180, 0);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(140, 40);
            this.imgLogo.TabIndex = 0;
            this.imgLogo.TabStop = false;
            // 
            // Etiqueta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 553);
            this.Controls.Add(this.imgLogo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbRodape);
            this.Name = "Etiqueta";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PALLET LABEL  V.1.0.0.1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridErro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbRodape;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbPO;
        private System.Windows.Forms.TextBox txtPO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbBox;
        private System.Windows.Forms.TextBox txtPallet;
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Label lbAviso;
        private System.Windows.Forms.DataGridView gridErro;
        private System.Windows.Forms.DataGridView gridPallet;
        private System.Windows.Forms.Button btLimpar;
        private System.Windows.Forms.Label lbTotalCaixa;
        private System.Windows.Forms.Label lbCaixa;
    }
}

