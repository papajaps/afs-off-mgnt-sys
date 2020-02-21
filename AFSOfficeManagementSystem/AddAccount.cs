using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFSOfficeManagementSystem
{
    public partial class AddAccount : Form
    {
        SQLClass sqlquery = new SQLClass();
        Int16 addOrEdit = 0;
        string id = "";
        public AddAccount()
        {
            InitializeComponent();
        }

        private void AddAccount_Load(object sender, EventArgs e)
        {
            this.ActiveControl = lblHeader;
            textBox1.GotFocus += new EventHandler(this.TextGotFocus);
            textBox1.LostFocus += new EventHandler(this.TextLostFocus);

            textBox2.GotFocus += new EventHandler(this.TextGotFocus2);
            textBox2.LostFocus += new EventHandler(this.TextLostFocus2);

            textBox3.GotFocus += new EventHandler(this.TextGotFocus3);
            textBox3.LostFocus += new EventHandler(this.TextLostFocus3);

            textBox4.GotFocus += new EventHandler(this.TextGotFocus4);
            textBox4.LostFocus += new EventHandler(this.TextLostFocus4);

        }

        public void TextGotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "BANK NAME")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        public void TextLostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "BANK NAME";
                tb.ForeColor = Color.Gray;
            }
        }

        public void TextGotFocus2(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "ACCOUNT NAME")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        public void TextLostFocus2(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "ACCOUNT NAME";
                tb.ForeColor = Color.Gray;
            }
        }

        public void TextGotFocus3(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "ACCOUNT NUMBER")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        public void TextLostFocus3(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "ACCOUNT NUMBER";
                tb.ForeColor = Color.Gray;
            }
        }

        public void TextGotFocus4(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "BALANCE")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        public void TextLostFocus4(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "BALANCE";
                tb.ForeColor = Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (textBox1.Text != "BANK NAME" & textBox2.Text != "ACCOUNT NAME" & textBox3.Text != "ACCOUNT NUMBER" & textBox4.Text != "BALANCE")
            {
                if (addOrEdit == 0)
                {
                    qry = string.Format(@"INSERT INTO accounts(
                                            account_bank,
                                            account_name,
                                            account_number,
                                            account_bal
                                        )
                                        VALUES('{0}', '{1}', '{2}', '{3}'); ", textBox1.Text,
                                                                                   textBox2.Text,
                                                                                   textBox3.Text,
                                                                                   textBox4.Text);
                }
                else
                {
                    qry = string.Format(@"UPDATE
                                            accounts
                                        SET
                                            account_bank = '{0}',
                                            account_name = '{1}',
                                            account_number = '{2}',
                                            account_bal = {3}
                                                        WHERE
                                            account_id = '{4}'; ", textBox1.Text,
                                                                       textBox2.Text,
                                                                       textBox3.Text,
                                                                       textBox4.Text,
                                                                       id);
                }
                sqlquery.ExecuteQueries(qry);

                MainForm obj = (MainForm)Application.OpenForms["MainForm"];
                obj.loadAccData();

                accountTable fc = (accountTable)Application.OpenForms["accountTable"];

                if (fc != null)
                    fc.loadAccData();

                this.Close();
            }
            else
            {
                label4.Visible = true;
            }
            
        }

        public void ShowDialog(string bName, decimal aBal, string aName, string aNum, string edit, string aId)
        {
            //Assign received parameter(s) to local context
            textBox1.ForeColor = Color.Black;
            textBox2.ForeColor = Color.Black;
            textBox3.ForeColor = Color.Black;
            textBox4.ForeColor = Color.Black;
            addOrEdit = 1;
            id = aId;

            textBox1.Text = bName;
            textBox2.Text = aName;
            textBox3.Text = aNum;
            textBox4.Text = aBal.ToString();

            this.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
