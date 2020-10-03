namespace Graal.Library.Common.GUI
{
    partial class InputBoxWindow
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
            this.Txt_Text = new System.Windows.Forms.TextBox();
            this.Lbl_Hint = new System.Windows.Forms.Label();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Btn_No = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Txt_Text
            // 
            this.Txt_Text.Location = new System.Drawing.Point(15, 45);
            this.Txt_Text.Name = "Txt_Text";
            this.Txt_Text.Size = new System.Drawing.Size(200, 20);
            this.Txt_Text.TabIndex = 0;
            this.Txt_Text.TextChanged += new System.EventHandler(this.Txt_Text_TextChanged);
            // 
            // Lbl_Hint
            // 
            this.Lbl_Hint.AutoSize = true;
            this.Lbl_Hint.Location = new System.Drawing.Point(15, 15);
            this.Lbl_Hint.Name = "Lbl_Hint";
            this.Lbl_Hint.Size = new System.Drawing.Size(35, 13);
            this.Lbl_Hint.TabIndex = 1;
            this.Lbl_Hint.Text = "label1";
            this.Lbl_Hint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Ok.Location = new System.Drawing.Point(15, 100);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(60, 23);
            this.Btn_Ok.TabIndex = 2;
            this.Btn_Ok.Text = "OK";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_No
            // 
            this.Btn_No.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Btn_No.Location = new System.Drawing.Point(85, 100);
            this.Btn_No.Name = "Btn_No";
            this.Btn_No.Size = new System.Drawing.Size(60, 23);
            this.Btn_No.TabIndex = 3;
            this.Btn_No.Text = "Нет";
            this.Btn_No.UseVisualStyleBackColor = true;
            this.Btn_No.Visible = false;
            this.Btn_No.Click += new System.EventHandler(this.Btn_No_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.Location = new System.Drawing.Point(155, 100);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(60, 23);
            this.Btn_Cancel.TabIndex = 4;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // InputBoxWindow
            // 
            this.AcceptButton = this.Btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Btn_No;
            this.ClientSize = new System.Drawing.Size(234, 142);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_No);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.Lbl_Hint);
            this.Controls.Add(this.Txt_Text);
            this.Name = "InputBoxWindow";
            this.Text = "InputBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Txt_Text;
        private System.Windows.Forms.Label Lbl_Hint;
        public System.Windows.Forms.Button Btn_Ok;
        public System.Windows.Forms.Button Btn_No;
        public System.Windows.Forms.Button Btn_Cancel;
    }
}