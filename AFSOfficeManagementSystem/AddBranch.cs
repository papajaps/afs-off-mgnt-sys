using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFSOfficeManagementSystem
{
    public partial class AddBranch : Form
    {
        SQLClass sqlquery = new SQLClass();
        Int16 addOrEdit = 0;
        string id = "";
        string prov_id, city_id, brgy_id, emp_id = "";
        string prov_name, city_name, brgy_name, emp_name = "";
        string emp_val = "";
        public AddBranch()
        {
            InitializeComponent();

            this.ActiveControl = lblHeader;
            textBox1.GotFocus += new EventHandler(this.TextGotFocus);
            textBox1.LostFocus += new EventHandler(this.TextLostFocus);

            textBox2.GotFocus += new EventHandler(this.TextGotFocus);
            textBox2.LostFocus += new EventHandler(this.TextLostFocus);

            textBox3.GotFocus += new EventHandler(this.TextGotFocus);
            textBox3.LostFocus += new EventHandler(this.TextLostFocus);
        }
        public void TextGotFocus(object sender, EventArgs e)
        {

        }

        public void TextLostFocus(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            if(comboBox1.Text != prov_name)
            {
                comboBox2.Text = "CITY/MUNICIPALITY";
                comboBox3.Text = "BARANGAY";
                comboBox2.ForeColor = Color.Gray;
                comboBox3.ForeColor = Color.Gray;
            }
        }
               
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPerso_DropDown(object sender, EventArgs e)
        {
            cmbPerso.ForeColor = Color.Black;
            sqlquery.OpenConection();

            object ds = sqlquery.fillcombobox("select employee_id, Concat(employee_fname, ' ', employee_lname) as 'Name' from employee");
            cmbPerso.DataSource = ds;
            cmbPerso.DisplayMember = "Name";
            cmbPerso.ValueMember = "employee_id";
            sqlquery.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (textBox1.Text != "BRANCH NAME" & comboBox1.Text != "PROVINCE" & comboBox2.Text != "CITY/MUNICIPALITY" & comboBox3.Text != "BARANGAY" & textBox2.Text != "ZIP CODE" & textBox3.Text != "E-MAIL ADDRESS")
            {
                if (addOrEdit == 0)
                {
                    qry = string.Format(@"INSERT INTO branch(
                                        branch_name,
                                        prov_id,
                                        city_id,
                                        brgy_id,
                                        zip_id,
                                        branch_email,
                                        branch_cont_no,
                                        employee_id
                                    )
                                    VALUES(
                                        '{0}',
                                        '{1}',
                                        '{2}',
                                        '{3}',
                                        '{4}',
                                        '{5}',
                                        '{6}',
                                        '{7}'
                                    ); ", textBox1.Text,
                                              comboBox1.SelectedValue,
                                              comboBox2.SelectedValue,
                                              comboBox3.SelectedValue,
                                              textBox2.Text,
                                              textBox3.Text,
                                              maskedTextBox1.Text,
                                              cmbPerso.SelectedValue);
                }
                else
                {

                    if (emp_name == cmbPerso.Text)
                    {
                        emp_val = emp_id;
                    }
                    else
                    {
                        emp_val = cmbPerso.SelectedValue.ToString();
                    }
                    if (comboBox1.Text == prov_name & comboBox2.Text == city_name & comboBox3.Text == brgy_name)
                    {
                        qry = string.Format(@"UPDATE
                                        branch
                                      SET
                                        branch_name = '{0}',
                                        prov_id = '{1}',
                                        city_id = '{2}',
                                        brgy_id = '{3}',
                                        zip_id = '{4}',
                                        employee_id = '{5}',
                                        branch_email = '{6}',
                                        branch_cont_no = '{7}'
                                      WHERE
                                        branch_id = '{8}';", textBox1.Text,
                                                             prov_id,
                                                             city_id,
                                                             brgy_id,
                                                             textBox2.Text,
                                                             emp_val,
                                                             textBox3.Text,
                                                             maskedTextBox1.Text,
                                                             id);
                    }
                    else
                    {
                        qry = string.Format(@"UPDATE
                                        branch
                                      SET
                                        branch_name = '{0}',
                                        prov_id = '{1}',
                                        city_id = '{2}',
                                        brgy_id = '{3}',
                                        zip_id = '{4}',
                                        employee_id = '{5}',
                                        branch_email = '{6}',
                                        branch_cont_no = '{7}'
                                      WHERE
                                        branch_id = '{8}';", textBox1.Text,
                                                             comboBox1.SelectedValue,
                                                             comboBox2.SelectedValue,
                                                             comboBox3.SelectedValue,
                                                             textBox2.Text,
                                                             emp_val,
                                                             textBox3.Text,
                                                             maskedTextBox1.Text,
                                                             id);
                    }
                }
                sqlquery.ExecuteQueries(qry);
                sqlquery.CloseConnection();
                this.Close();
                MainForm obj = (MainForm)Application.OpenForms["MainForm"];
                obj.loadBranData();

            }
            else
            {
                label4.Visible = true;
            }

        }
        public void ShowDialog(string bName)
        {
            textBox1.ForeColor = Color.Black;
            textBox2.ForeColor = Color.Black;
            textBox3.ForeColor = Color.Black;
            cmbPerso.ForeColor = Color.Black;
            comboBox1.ForeColor = Color.Black;
            comboBox2.ForeColor = Color.Black;
            comboBox3.ForeColor = Color.Black;
            addOrEdit = 1;
            id = bName;

            sqlquery.OpenConection();
            MySqlDataReader dr = sqlquery.DataReader(@"SELECT
                                                            *
                                                        FROM
                                                            branch br
                                                        JOIN refbrgy b ON
                                                            br.brgy_id = b.brgyCode
                                                        JOIN refcitymun c ON
                                                            br.city_id = c.citymunCode
                                                        JOIN refprovince p ON
                                                            br.prov_id = p.provCode
                                                        JOIN employee e ON
                                                            br.employee_id = e.employee_id
                                                        WHERE
                                                            br.branch_id = '" + id + "'");
            while (dr.Read())
            {
                textBox1.Text = Convert.ToString(dr["branch_name"]);
                comboBox1.Text = Convert.ToString(dr["provDesc"]);
                prov_id = Convert.ToString(dr["prov_id"]);
                prov_name = Convert.ToString(dr["provDesc"]);
                comboBox2.Text = Convert.ToString(dr["citymunDesc"]);
                city_id = Convert.ToString(dr["city_id"]);
                city_name = Convert.ToString(dr["citymunDesc"]);
                comboBox3.Text = Convert.ToString(dr["brgyDesc"]);
                brgy_id = Convert.ToString(dr["brgy_id"]);
                brgy_name = Convert.ToString(dr["brgyDesc"]);

                textBox2.Text = Convert.ToString(dr["zip_id"]);
                textBox3.Text = Convert.ToString(dr["branch_email"]);
                maskedTextBox1.Text = Convert.ToString(dr["branch_cont_no"]);

                cmbPerso.Text = Convert.ToString(dr["employee_fname"]) + " " + Convert.ToString(dr["employee_lname"]);
                emp_id = Convert.ToString(dr["employee_id"]);
                emp_name = Convert.ToString(dr["employee_fname"]) + " " + Convert.ToString(dr["employee_lname"]);
            }
            sqlquery.CloseConnection();

            this.ShowDialog();

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

        private void AddBranch_Load(object sender, EventArgs e)
        {

        }
    }
}
