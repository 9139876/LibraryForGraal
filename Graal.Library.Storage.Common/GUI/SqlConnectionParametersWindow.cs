using System;
using System.Data;
using System.Windows.Forms;

namespace Graal.Library.Storage.Common
{
    public partial class SqlConnectionParametersWindow : Form
    {
        readonly Action<IDbConnection> ConnectionAct;
        readonly Action<string> ConnStrAct;

        IDbConnection connection;

        string connStr;

        public SqlConnectionParametersWindow(string server, string port, string user, string password, string database, Action<IDbConnection> connectionAct, Action<string> connStrAct)
        {
            InitializeComponent();

            TxtBox_Server.Text = server;
            TxtBox_Port.Text = port;
            TxtBox_User.Text = user;
            TxtBox_Password.Text = password;
            TxtBox_DataBase.Text = database;

            ConnectionAct = connectionAct;
            ConnStrAct = connStrAct;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

                connection.Dispose();
            }

            Close();
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (connection == null)
            {
                connStr = StorageManager.CreateConnectionString(TxtBox_Server.Text, TxtBox_Port.Text, TxtBox_User.Text, TxtBox_Password.Text, TxtBox_DataBase.Text);
                
                if (StorageManager.TryGetConnection(connStr, out connection, out string err))
                {
                    Btn_Ok.Text = "OK";
                    Btn_Ok.DialogResult = DialogResult.OK;
                    MessageBox.Show($"Соединение успешно установлено!");
                }
                else
                {
                    MessageBox.Show($"Ошибка: {err}");
                }
            }
            else
            {
                ConnectionAct(connection);
                ConnStrAct(connStr);
                Close();
            }
        }
    }
}
