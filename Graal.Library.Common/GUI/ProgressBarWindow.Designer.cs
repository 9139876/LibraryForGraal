namespace Graal.Library.Common.GUI
{
    partial class ProgressBarWindow
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
            this.PBar = new System.Windows.Forms.ProgressBar();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Lbl_Counter = new System.Windows.Forms.Label();
            this.Lbl_Text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PBar
            // 
            this.PBar.Location = new System.Drawing.Point(12, 86);
            this.PBar.Name = "PBar";
            this.PBar.Size = new System.Drawing.Size(300, 23);
            this.PBar.TabIndex = 0;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(318, 86);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 1;
            this.Btn_Cancel.Text = "Отмена";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Lbl_Counter
            // 
            this.Lbl_Counter.AutoSize = true;
            this.Lbl_Counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Lbl_Counter.Location = new System.Drawing.Point(8, 59);
            this.Lbl_Counter.Name = "Lbl_Counter";
            this.Lbl_Counter.Size = new System.Drawing.Size(60, 24);
            this.Lbl_Counter.TabIndex = 2;
            this.Lbl_Counter.Text = "label1";
            // 
            // Lbl_Text
            // 
            this.Lbl_Text.AutoSize = true;
            this.Lbl_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Lbl_Text.Location = new System.Drawing.Point(8, 20);
            this.Lbl_Text.Name = "Lbl_Text";
            this.Lbl_Text.Size = new System.Drawing.Size(60, 24);
            this.Lbl_Text.TabIndex = 3;
            this.Lbl_Text.Text = "label1";
            // 
            // ProgressBarWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 121);
            this.ControlBox = false;
            this.Controls.Add(this.Lbl_Text);
            this.Controls.Add(this.Lbl_Counter);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.PBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProgressBarWindow";
            this.Text = "Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar PBar;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Label Lbl_Counter;
        private System.Windows.Forms.Label Lbl_Text;
    }
}