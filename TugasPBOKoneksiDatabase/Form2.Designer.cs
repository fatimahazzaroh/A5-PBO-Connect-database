namespace TugasPBOKoneksiDatabase
{
    partial class Form2
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
            label1 = new Label();
            namaprestasitextbox = new TextBox();
            label2 = new Label();
            tingkatlombacombobox = new ComboBox();
            label3 = new Label();
            buktitextbox = new TextBox();
            label4 = new Label();
            label5 = new Label();
            simpanbutton = new Button();
            posisicombobox = new ComboBox();
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("The Fountain of Wishes", 36F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(180, 33);
            label1.Name = "label1";
            label1.Size = new Size(414, 60);
            label1.TabIndex = 0;
            label1.Text = "TAMBAH DATA PRESTASI";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // namaprestasitextbox
            // 
            namaprestasitextbox.Location = new Point(180, 152);
            namaprestasitextbox.Name = "namaprestasitextbox";
            namaprestasitextbox.Size = new Size(414, 27);
            namaprestasitextbox.TabIndex = 1;
            namaprestasitextbox.TextChanged += namaprestasitextbox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(77, 155);
            label2.Name = "label2";
            label2.Size = new Size(102, 20);
            label2.TabIndex = 2;
            label2.Text = "Nama Lomba:";
            // 
            // tingkatlombacombobox
            // 
            tingkatlombacombobox.FormattingEnabled = true;
            tingkatlombacombobox.Location = new Point(180, 200);
            tingkatlombacombobox.Name = "tingkatlombacombobox";
            tingkatlombacombobox.Size = new Size(151, 28);
            tingkatlombacombobox.TabIndex = 3;
            tingkatlombacombobox.SelectedIndexChanged += tingkatlombacombobox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(131, 249);
            label3.Name = "label3";
            label3.Size = new Size(48, 20);
            label3.TabIndex = 4;
            label3.Text = "Posisi:";
            // 
            // buktitextbox
            // 
            buktitextbox.Location = new Point(180, 299);
            buktitextbox.Name = "buktitextbox";
            buktitextbox.Size = new Size(414, 27);
            buktitextbox.TabIndex = 5;
            buktitextbox.TextChanged += buktitextbox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(134, 302);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 6;
            label4.Text = "Bukti:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.Highlight;
            label5.Location = new Point(180, 329);
            label5.Name = "label5";
            label5.Size = new Size(428, 17);
            label5.TabIndex = 7;
            label5.Text = "Silahkan masukkan tautan atau link google drive file bukti prestasi Anda!";
            // 
            // simpanbutton
            // 
            simpanbutton.BackColor = Color.Cyan;
            simpanbutton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            simpanbutton.Location = new Point(660, 386);
            simpanbutton.Name = "simpanbutton";
            simpanbutton.Size = new Size(94, 29);
            simpanbutton.TabIndex = 8;
            simpanbutton.Text = "Simpan";
            simpanbutton.UseVisualStyleBackColor = false;
            simpanbutton.Click += simpanbutton_Click;
            // 
            // posisicombobox
            // 
            posisicombobox.FormattingEnabled = true;
            posisicombobox.Location = new Point(180, 246);
            posisicombobox.Name = "posisicombobox";
            posisicombobox.Size = new Size(151, 28);
            posisicombobox.TabIndex = 9;
            posisicombobox.SelectedIndexChanged += posisicombobox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(68, 203);
            label6.Name = "label6";
            label6.Size = new Size(111, 20);
            label6.TabIndex = 10;
            label6.Text = "Tingkat Lomba:";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label6);
            Controls.Add(posisicombobox);
            Controls.Add(simpanbutton);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(buktitextbox);
            Controls.Add(label3);
            Controls.Add(tingkatlombacombobox);
            Controls.Add(label2);
            Controls.Add(namaprestasitextbox);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox namaprestasitextbox;
        private Label label2;
        private ComboBox tingkatlombacombobox;
        private Label label3;
        private TextBox buktitextbox;
        private Label label4;
        private Label label5;
        private Button simpanbutton;
        private ComboBox posisicombobox;
        private Label label6;
    }
}