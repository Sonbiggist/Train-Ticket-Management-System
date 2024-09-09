namespace QuanLiVeTau
{
    partial class FrmTau
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewTAU = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTAU)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.Location = new System.Drawing.Point(279, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(264, 63);
            this.button1.TabIndex = 0;
            this.button1.Text = "Trở về";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewTAU
            // 
            this.dataGridViewTAU.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewTAU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTAU.Location = new System.Drawing.Point(41, 26);
            this.dataGridViewTAU.Name = "dataGridViewTAU";
            this.dataGridViewTAU.RowHeadersWidth = 51;
            this.dataGridViewTAU.RowTemplate.Height = 24;
            this.dataGridViewTAU.Size = new System.Drawing.Size(724, 300);
            this.dataGridViewTAU.TabIndex = 1;
            // 
            // FrmTau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridViewTAU);
            this.Controls.Add(this.button1);
            this.Name = "FrmTau";
            this.Text = "Tàu";
            this.Load += new System.EventHandler(this.FrmTau_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTAU)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewTAU;
    }
}