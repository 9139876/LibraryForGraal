namespace Graal.Library.Storage.Common
{
    partial class SqlConnectionParametersWindow
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
            this.label4 = new System.Windows.Forms.Label();
            this.TxtBox_Server = new System.Windows.Forms.TextBox();
            this.TxtBox_User = new System.Windows.Forms.TextBox();
            this.TxtBox_Password = new System.Windows.Forms.TextBox();
            this.TxtBox_DataBase = new System.Windows.Forms.TextBox();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.TxtBox_Port = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "DataBase";
            // 
            // TxtBox_Server
            // 
            this.TxtBox_Server.Location = new System.Drawing.Point(71, 13);
            this.TxtBox_Server.Name = "TxtBox_Server";
            this.TxtBox_Server.Size = new System.Drawing.Size(150, 20);
            this.TxtBox_Server.TabIndex = 1;
            // 
            // TxtBox_User
            // 
            this.TxtBox_User.Location = new System.Drawing.Point(71, 65);
            this.TxtBox_User.Name = "TxtBox_User";
            this.TxtBox_User.Size = new System.Drawing.Size(150, 20);
            this.TxtBox_User.TabIndex = 3;
            // 
            // TxtBox_Password
            // 
            this.TxtBox_Password.Location = new System.Drawing.Point(71, 91);
            this.TxtBox_Password.Name = "TxtBox_Password";
            this.TxtBox_Password.PasswordChar = '*';
            this.TxtBox_Password.Size = new System.Drawing.Size(150, 20);
            this.TxtBox_Password.TabIndex = 4;
            // 
            // TxtBox_DataBase
            // 
            this.TxtBox_DataBase.Location = new System.Drawing.Point(71, 117);
            this.TxtBox_DataBase.Name = "TxtBox_DataBase";
            this.TxtBox_DataBase.Size = new System.Drawing.Size(150, 20);
            this.TxtBox_DataBase.TabIndex = 5;
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Location = new System.Drawing.Point(12, 147);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.Btn_Ok.TabIndex = 6;
            this.Btn_Ok.Text = "Тест";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.Location = new System.Drawing.Point(146, 147);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 7;
            this.Btn_Cancel.Text = "Отмена";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // TxtBox_Port
            // 
            this.TxtBox_Port.Location = new System.Drawing.Point(72, 39);
            this.TxtBox_Port.Name = "TxtBox_Port";
            this.TxtBox_Port.Size = new System.Drawing.Size(150, 20);
            this.TxtBox_Port.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Port";
            // 
            // SqlConnectionParametersWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Btn_Cancel;
            this.ClientSize = new System.Drawing.Size(234, 181);
            this.ControlBox = false;
            this.Controls.Add(this.TxtBox_Port);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.TxtBox_DataBase);
            this.Controls.Add(this.TxtBox_Password);
            this.Controls.Add(this.TxtBox_User);
            this.Controls.Add(this.TxtBox_Server);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SqlConnectionParametersWindow";
            this.Text = "Параметры соединения с БД ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtBox_Server;
        private System.Windows.Forms.TextBox TxtBox_User;
        private System.Windows.Forms.TextBox TxtBox_Password;
        private System.Windows.Forms.TextBox TxtBox_DataBase;
        private System.Windows.Forms.Button Btn_Ok;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.TextBox TxtBox_Port;
        private System.Windows.Forms.Label label5;
    }
}