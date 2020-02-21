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
    public partial class AddSupplier : Form
    {
        SQLClass sqlquery = new SQLClass();
        Int16 addOrEdit = 0;
        string id = "";
        string prov_id, city_id, brgy_id = "";
        string prov_name, city_name, brgy_name = "";
        string status = "";
        string newStatus = "";
        public AddSupplier()
        {
            InitializeComponent();
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
            if (tb.Text == "SUPPLIER NAME")
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
                tb.Text = "SUPPLIER NAME";
                tb.ForeColor = Color.Gray;
            }
        }
        public void TextGotFocus2(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "ZIP CODE")
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
                tb.Text = "ZIP CODE";
                tb.ForeColor = Color.Gray;
            }
        }

        public void TextGotFocus3(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "E-MAIL ADDRESS")
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
                tb.Text = "E-MAIL ADDRESS";
                tb.ForeColor = Color.Gray;
            }
        }

        public void TextGotFocus4(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "TIN NUMBER")
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
                tb.Text = "TIN NUMBER";
                tb.ForeColor = Color.Gray;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            if (comboBox1.Text != prov_id)
            {
                comboBox2.Text = "CITY/MUNICIPALITY";
                comboBox3.Text = "BARANGAY";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = true;
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.ForeColor = Color.Black;
            sqlquery.OpenConection();
            object ds = sqlquery.fillcombobox("select provCode, provDesc from refprovince");
            comboBox1.DataSource = ds;
            comboBox1.DisplayMember = "provDesc";
            comboBox1.ValueMember = "provCode";
            sqlquery.CloseConnection();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.ForeColor = Color.Black;
            sqlquery.OpenConection();

            object ds = sqlquery.fillcombobox("select citymunCode, citymunDesc from refcitymun where provCode = '" + comboBox1.SelectedValue + "';");
            comboBox2.DataSource = ds;
            comboBox2.DisplayMember = "citymunDesc";
            comboBox2.ValueMember = "citymunCode";
            sqlquery.CloseConnection();
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            comboBox3.ForeColor = Color.Black;
            sqlquery.OpenConection();

            object ds = sqlquery.fillcombobox("select brgyDesc, brgyCode from refbrgy where citymunCode = '" + comboBox2.SelectedValue + "';");
            comboBox3.DataSource = ds;
            comboBox3.DisplayMember = "brgyDesc".ToUpper();
            comboBox3.ValueMember = "brgyCode";
            sqlquery.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                newStatus = "Out of Service";
            }
            else
                newStatus = "Active";
            string qry = "";
            if(addOrEdit == 0)
            {
                qry = string.Format(@"INSERT INTO supplier(
                                        	supplier_name,
                                            province_id,
                                            city_id,
                                            brgy_id,
                                            zip_id,
                                            supplier_cont_no,
                                            supplier_email,
                                            supplier_tin,
                                            supplier_stat
                                    )
                                    VALUES(
                                        '{0}',
                                        '{1}',
                                        '{2}',
                                        '{3}',
                                        '{4}',
                                        '{5}',
                                        '{6}',
                                        '{7}',
                                        '{8}'
                                    ); ", textBox1.Text,
                                              comboBox1.SelectedValue,
                                              comboBox2.SelectedValue,
                                              comboBox3.SelectedValue,
                                              textBox2.Text,
                                              maskedTextBox1.Text,
                                              textBox3.Text,
                                              textBox4.Text,
                                              newStatus);
                
            }
            else
            {
                if (comboBox1.Text == prov_name & comboBox2.Text == city_name & comboBox3.Text == brgy_name)
                {
                    qry = string.Format(@"UPDATE
                                        supplier
                                      SET
                                        supplier_name = '{0}',
                                        province_id = '{1}',
                                        city_id = '{2}',
                                        brgy_id = '{3}',
                                        zip_id = '{4}',
                                        supplier_tin = '{5}',
                                        supplier_email = '{6}',
                                        supplier_cont_no = '{7}',
                                        supplier_stat = '{9}'
                                      WHERE
                                        supplier_id = '{8}';",  textBox1.Text,
                                                                prov_id,
                                                                city_id,
                                                                brgy_id,
                                                                textBox2.Text,
                                                                textBox4.Text,
                                                                textBox3.Text,
                                                                maskedTextBox1.Text,
                                                                id,newStatus);
                }
                else
                {
                    qry = string.Format(@"UPDATE
                                        supplier
                                      SET
                                        supplier_name = '{0}',
                                        province_id = '{1}',
                                        city_id = '{2}',
                                        brgy_id = '{3}',
                                        zip_id = '{4}',
                                        supplier_tin = '{5}',
                                        supplier_email = '{6}',
                                        supplier_cont_no = '{7}',
                                        supplier_stat = '{9}'
                                      WHERE
                                        supplier_id = '{8}';", textBox1.Text,
                                                                                  comboBox1.SelectedValue,
                                                                                  comboBox2.SelectedValue,
                                                                                  comboBox3.SelectedValue,
                                                                                  textBox2.Text,
                                                                                  textBox4.Text,
                                                                                  textBox3.Text,
                                                                                  maskedTextBox1.Text,
                                                                                  id,newStatus);
                }
            }


            sqlquery.ExecuteQueries(qry);
            sqlquery.CloseConnection();
            this.Close();
            MainForm obj = (MainForm)Application.OpenForms["MainForm"];
            obj.loadSuppData();

        }
        public void ShowDialog(string suppID)
        {
            textBox1.ForeColor = Color.Black;
            textBox2.ForeColor = Color.Black;
            textBox3.ForeColor = Color.Black;
            textBox4.ForeColor = Color.Black;
            comboBox1.ForeColor = Color.Black;
            comboBox2.ForeColor = Color.Black;
            comboBox3.ForeColor = Color.Black;
            addOrEdit = 1;
            id = suppID;
            sqlquery.OpenConection();
            MySqlDataReader dr = sqlquery.DataReader(@"SELECT
                                                            *
                                                        FROM
                                                            supplier sr
                                                        JOIN refbrgy b ON
                                                            sr.brgy_id = b.brgyCode
                                                        JOIN refcitymun c ON
                                                            sr.city_id = c.citymunCode
                                                        JOIN refprovince p ON
                                                            sr.province_id = p.provCode
                                                        WHERE
                                                            sr.supplier_id = '" + id + "'");
            while (dr.Read())
            {

                textBox1.Text = Convert.ToString(dr["supplier_name"]);
                comboBox1.Text = Convert.ToString(dr["provDesc"]);
                prov_id = Convert.ToString(dr["province_id"]);
                prov_name = Convert.ToString(dr["provDesc"]);
                comboBox2.Text = Convert.ToString(dr["citymunDesc"]);
                city_id = Convert.ToString(dr["city_id"]);
                city_name = Convert.ToString(dr["citymunDesc"]);
                comboBox3.Text = Convert.ToString(dr["brgyDesc"]);
                brgy_id = Convert.ToString(dr["brgy_id"]);
                brgy_name = Convert.ToString(dr["brgyDesc"]);
                status = Convert.ToString(dr["supplier_stat"]);
                textBox2.Text = Convert.ToString(dr["zip_id"]);
                textBox3.Text = Convert.ToString(dr["supplier_email"]);
                maskedTextBox1.Text = Convert.ToString(dr["supplier_cont_no"]);
                textBox4.Text = Convert.ToString(dr["supplier_tin"]);
            }
            sqlquery.CloseConnection();
            if (status == "Active")
            {
                radioButton1.Checked = true;
            }
            else
                radioButton2.Checked = true;
            this.ShowDialog();

        }
        private void AddSupplier_Load(object sender, EventArgs e)
        {

        }
    }
}
