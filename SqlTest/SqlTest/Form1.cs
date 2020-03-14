using MySql.Data.MySqlClient;
using SqlTest.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlTest
{
    public partial class Form1 : Form
    {
        public  User CurentUser;

        public Form1()
        {
            InitializeComponent();
            LogProvider.Instance.AddLog("Init form");
            LogProvider.Instance.SaveLogsToFile();
            SQLHelper.GetAllUsers(); 
            
        }

        private void buttonAuth_Click(object sender, EventArgs e)
        {
            try
            {

                Validation.Login(loginField.Text);
                Validation.Pass(passField.Text);

                SettingsProvider.Instance.CurentUser = SQLHelper.Login(loginField.Text, passField.Text);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string loginUser = loginField.Text;

            SettingsProvider.Instance.SaveSettings();

        }
    }
}
