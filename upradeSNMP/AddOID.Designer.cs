
namespace upradeSNMP
{
    partial class AddOID
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
            this.txtboxOIDName = new System.Windows.Forms.TextBox();
            this.txtBoxOIDTFTP = new System.Windows.Forms.TextBox();
            this.txtboxReboot = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "OID имени прошивки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "OID адреса tftp сервера";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "OID команды на перезагрузку";
            // 
            // txtboxOIDName
            // 
            this.txtboxOIDName.Location = new System.Drawing.Point(411, 130);
            this.txtboxOIDName.Name = "txtboxOIDName";
            this.txtboxOIDName.Size = new System.Drawing.Size(269, 31);
            this.txtboxOIDName.TabIndex = 3;
            // 
            // txtBoxOIDTFTP
            // 
            this.txtBoxOIDTFTP.Location = new System.Drawing.Point(411, 167);
            this.txtBoxOIDTFTP.Name = "txtBoxOIDTFTP";
            this.txtBoxOIDTFTP.Size = new System.Drawing.Size(269, 31);
            this.txtBoxOIDTFTP.TabIndex = 4;
            // 
            // txtboxReboot
            // 
            this.txtboxReboot.Location = new System.Drawing.Point(411, 204);
            this.txtboxReboot.Name = "txtboxReboot";
            this.txtboxReboot.Size = new System.Drawing.Size(269, 31);
            this.txtboxReboot.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 284);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddOID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 371);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtboxReboot);
            this.Controls.Add(this.txtBoxOIDTFTP);
            this.Controls.Add(this.txtboxOIDName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddOID";
            this.Text = "AddOID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtboxOIDName;
        private System.Windows.Forms.TextBox txtBoxOIDTFTP;
        private System.Windows.Forms.TextBox txtboxReboot;
        private System.Windows.Forms.Button button1;
    }
}