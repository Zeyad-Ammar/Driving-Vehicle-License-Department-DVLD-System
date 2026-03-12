namespace DVLD_Project
{
    partial class frmReadPersonCard
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
            this.lbFormName = new System.Windows.Forms.Label();
            this.ucPersonReadCard1 = new DVLD_Project.ctrlPersonReadCard();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(915, 390);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 48);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbFormName
            // 
            this.lbFormName.AutoSize = true;
            this.lbFormName.BackColor = System.Drawing.Color.Coral;
            this.lbFormName.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFormName.ForeColor = System.Drawing.Color.Blue;
            this.lbFormName.Location = new System.Drawing.Point(421, 9);
            this.lbFormName.Name = "lbFormName";
            this.lbFormName.Size = new System.Drawing.Size(309, 38);
            this.lbFormName.TabIndex = 2;
            this.lbFormName.Text = "Person Information";
            // 
            // ucPersonReadCard1
            // 
            this.ucPersonReadCard1.Location = new System.Drawing.Point(12, 25);
            this.ucPersonReadCard1.Name = "ucPersonReadCard1";
            this.ucPersonReadCard1.Size = new System.Drawing.Size(1076, 374);
            this.ucPersonReadCard1.TabIndex = 0;
            // 
            // frmReadPersonCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 450);
            this.Controls.Add(this.lbFormName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucPersonReadCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmReadPersonCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReadPersonCard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlPersonReadCard ucPersonReadCard1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbFormName;
    }
}