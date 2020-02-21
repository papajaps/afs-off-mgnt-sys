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
    public partial class AddEmployee : Form
    {
        SQLClass sqlquery = new SQLClass();
        Int16 addOrEdit = 0;
        string id = "";
        string prov_id, city_id, brgy_id, pos_id = "";
        string prov_name, city_name, brgy_name, pos_name = "";
        public AddEmployee()
        {
            InitializeComponent();

            this.ActiveControl = lblHeader;
            txtFName.GotFocus += new EventHandler(this.TextGotFocus);
            txtFName.LostFocus += new EventHandler(this.TextLostFocus);

            txtMName.GotFocus += new EventHandler(this.TextGotFocus);
            txtMName.LostFocus += new EventHandler(this.TextLostFocus);

            txtLName.GotFocus += new EventHandler(this.TextGotFocus);
            txtLName.LostFocus += new EventHandler(this.TextLostFocus);

            txtStreet.GotFocus += new EventHandler(this.TextGotFocus);
            txtStreet.LostFocus += new EventHandler(this.TextLostFocus);

            txtEmail.GotFocus += new EventHandler(this.TextGotFocus);
            txtEmail.LostFocus += new EventHandler(this.TextLostFocus);

            txtSSS.GotFocus += new EventHandler(this.TextGotFocus);
            txtSSS.LostFocus += new EventHandler(this.TextLostFocus);

            txtPhilHealth.GotFocus += new EventHandler(this.TextGotFocus);
            txtPhilHealth.LostFocus += new EventHandler(this.TextLostFocus);

            txtPagibig.GotFocus += new EventHandler(this.TextGotFocus);
            txtPagibig.LostFocus += new EventHandler(this.TextLostFocus);

            txtTIN.GotFocus += new EventHandler(this.TextGotFocus);
            txtTIN.LostFocus += new EventHandler(this.TextLostFocus);
        }

        public void TextGotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "FIRST NAME" || tb.Text == "MIDDLE NAME" || tb.Text == "LAST NAME" || tb.Text == "HOUSE NO./STREET" || tb.Text == "EMAIL" || tb.Text == "SSS" || tb.Text == "PHILHEALTH" || tb.Text == "PAG-IBIG" || tb.Text == "TIN")
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
                if (tb.Name == "txtFName") { tb.Text = "FIRST NAME"; }
                else if (tb.Name == "txtMName") { tb.Text = "MIDDLE NAME"; }
                else if (tb.Name == "txtLName") { tb.Text = "LAST NAME"; }
                else if (tb.Name == "txtStreet") { tb.Text = "HOUSE NO./STREET"; }
                else if (tb.Name == "txtEmail") { tb.Text = "EMAIL"; }
                else if (tb.Name == "txtSSS") { tb.Text = "SSS"; }
                else if (tb.Name == "txtPhilHealth") { tb.Text = "PHILHEALTH"; }
                else if (tb.Name == "txtPagibig") { tb.Text = "PAGIBIG"; }
                else if (tb.Name == "txtTIN") { tb.Text = "TIN"; }
                tb.ForeColor = Color.Gray;
            }
        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCity.Enabled = true;
            if (cmbProvince.Text != prov_name)
            {
                cmbCity.Text = "CITY/MUNICIPALITY";
                cmbBrgy.Text = "BARANGAY";
                cmbCity.ForeColor = Color.Gray;
                cmbBrgy.ForeColor = Color.Gray;
            }
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBrgy.Enabled = true;
        }

        private void cmbProvince_DropDown(object sender, EventArgs e)
        {
            cmbProvince.ForeColor = Color.Black;
            sqlquery.OpenConection();

            object ds = sqlquery.fillcombobox("select provCode, provDesc from refprovince");
            cmbProvince.DataSource = ds;
            cmbProvince.DisplayMember = "provDesc";
            cmbProvince.ValueMember = "provCode";
            sqlquery.CloseConnection();
        }

        private void cmbPosition_DropDown(object sender, EventArgs e)
        {
            cmbPosition.ForeColor = Color.Black;
            sqlquery.OpenConection();

            object ds = sqlquery.fillcombobox("select position_name, position_id from position;");
            cmbPosition.DataSource = ds;
            cmbPosition.DisplayMember = "position_name";
            cmbPosition.ValueMember = "position_id";
            sqlquery.CloseConnection();
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlquery.OpenConection();
            MySqlDataReader dr = sqlquery.DataReader(@"SELECT salary_rate from position where position_id = '" + cmbPosition.SelectedValue + "';");
            if (dr.Read())
            {
                txtSalary.Text = Convert.ToString(dr["salary_rate"]);
            }
            }
    
        private void cmbCity_DropDown(object sender, EventArgs e)
        {
            cmbCity.ForeColor = Color.Black;
            sqlquery.OpenConection();

            object ds = sqlquery.fillcombobox("select citymunCode, citymunDesc from refcitymun where provCode = '" + cmbProvince.SelectedValue + "';");
            cmbCity.DataSource = ds;
            cmbCity.DisplayMember = "citymunDesc";
            cmbCity.ValueMember = "citymunCode";
            sqlquery.CloseConnection();
        }

        private void cmbBrgy_DropDown(object sender, EventArgs e)
        {
            cmbBrgy.ForeColor = Color.Black;
            sqlquery.OpenConection();

            object ds = sqlquery.fillcombobox("select brgyDesc, brgyCode from refbrgy where citymunCode = '" + cmbCity.SelectedValue + "';");
            cmbBrgy.DataSource = ds;
            cmbBrgy.DisplayMember = "brgyDesc".ToUpper();
            cmbBrgy.ValueMember = "brgyCode";
            sqlquery.CloseConnection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "", mname = "";
            if (txtFName.Text != "FIRST NAME" & txtLName.Text != "LAST NAME" & cmbProvince.Text != "PROVINCE" & cmbCity.Text != "CITY/MUNICIPALITY" & cmbBrgy.Text != "BARANGAY" & txtStreet.Text != "HOUSE NO./STREET" & txtEmail.Text != "EMAIL" & txtPhone.Text != "PHONE NUMBER" & txtSSS.Text != "SSS" & txtPhilHealth.Text != "PHILHEALTH" & txtPagibig.Text != "PAG-IBIG" & txtTIN.Text != "TIN" & cmbPosition.Text != "POSITION")
            {
                if (txtMName.Text != "MIDDLE NAME") { mname = txtMName.Text; }
                if (addOrEdit == 0)
                {
                    qry = string.Format(@"INSERT INTO employee(
                                        employee_fname,
                                        employee_mname,
                                        employee_lname,
                                        province_id,
                                        city_id,
                                        brgy_id,
                                        street,
                                        employee_email,
                                        employee_cont_no,
                                        employee_sss,
                                        employee_pagibig,
                                        employee_philhealth,
                                        employee_tin_no,
                                        employee_pos_id
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
                                        '{8}',
                                        '{9}',
                                        '{10}',
                                        '{11}',
                                        '{12}',
                                        '{13}'
                                    ); ", txtFName.Text,
                                          mname,
                                          txtLName.Text,
                                          cmbProvince.SelectedValue,
                                          cmbCity.SelectedValue,
                                          cmbBrgy.SelectedValue,
                                          txtStreet.Text,
                                          txtEmail.Text,
                                          txtPhone.Text,
                                          txtSSS.Text,
                                          txtPagibig.Text,
                                          txtPhilHealth.Text,
                                          txtTIN.Text,
                                          cmbPosition.SelectedValue);
                }
                else
                { 
                    if(pos_name != cmbPosition.Text)
                    {
                        pos_id = cmbPosition.SelectedValue.ToString();
                    }
                    if (cmbProvince.Text == prov_name & cmbCity.Text == city_name & cmbBrgy.Text == brgy_name)
                    {
                        qry = string.Format(@"UPDATE
                                        employee
                                      SET
                                        employee_fname = '{0}',
                                        employee_mname = '{1}',
                                        employee_lname = '{2}',
                                        province_id = '{3}',
                                        city_id = '{4}',
                                        brgy_id = '{5}',
                                        street = '{6}',
                                        employee_email = '{7}',
                                        employee_cont_no = '{8}',
                                        employee_sss = '{9}',
                                        employee_pagibig = '{10}',
                                        employee_philhealth = '{11}',
                                        employee_tin_no = '{12}',
                                        employee_pos_id = '{13}'
                                      WHERE
                                        employee_id = '{14}';", txtFName.Text,
                                                                  mname,
                                                                  txtLName.Text,
                                                                  prov_id,
                                                                  city_id,
                                                                  brgy_id,
                                                                  txtStreet.Text,
                                                                  txtEmail.Text,
                                                                  txtPhone.Text,
                                                                  txtSSS.Text,
                                                                  txtPagibig.Text,
                                                                  txtPhilHealth.Text,
                                                                  txtTIN.Text,
                                                                  pos_id, id);
                    }
                    else
                    {
                        qry = string.Format(@"UPDATE
                                        employee
                                      SET
                                        employee_fname = '{0}',
                                        employee_mname = '{1}',
                                        employee_lname = '{2}',
                                        province_id = '{3}',
                                        city_id = '{4}',
                                        brgy_id = '{5}',
                                        street = '{6}',
                                        employee_email = '{7}',
                                        employee_cont_no = '{8}',
                                        employee_sss = '{9}',
                                        employee_pagibig = '{10}',
                                        employee_philhealth = '{11}',
                                        employee_tin_no = '{12}',
                                        employee_pos_id = '{13}'
                                      WHERE
                                        employee_id = '{14}';", txtFName.Text,
                                                                 mname,
                                                                 txtLName.Text,
                                                                 cmbProvince.SelectedValue,
                                                                 cmbCity.SelectedValue,
                                                                 cmbBrgy.SelectedValue,
                                                                 txtStreet.Text,
                                                                 txtEmail.Text,
                                                                 txtPhone.Text,
                                                                 txtSSS.Text,
                                                                 txtPagibig.Text,
                                                                 txtPhilHealth.Text,
                                                                 txtTIN.Text,
                                                                 pos_id, id);
                    }
                }
                sqlquery.ExecuteQueries(qry);
                sqlquery.CloseConnection();
                this.Close();
                MainForm obj = (MainForm)Application.OpenForms["MainForm"];
                obj.loadEmpData();
            } 
            else
            {
                lblError.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowDialog(string bName)
        {
            txtStreet.ForeColor = Color.Black;
            txtEmail.ForeColor = Color.Black;
            txtPhone.ForeColor = Color.Black;
            txtSSS.ForeColor = Color.Black;
            txtPhilHealth.ForeColor = Color.Black;
            txtPagibig.ForeColor = Color.Black;
            txtTIN.ForeColor = Color.Black;

            txtFName.ForeColor = Color.Black;
            txtMName.ForeColor = Color.Black;
            txtLName.ForeColor = Color.Black;

            cmbPosition.ForeColor = Color.Black;
            cmbProvince.ForeColor = Color.Black;
            cmbCity.ForeColor = Color.Black;
            cmbBrgy.ForeColor = Color.Black;
            txtSalary.ForeColor = Color.Black;

            addOrEdit = 1;
            id = bName;
            sqlquery.OpenConection();
            MySqlDataReader dr = sqlquery.DataReader(@"SELECT
                                                            *
                                                        FROM
                                                            employee e
                                                        JOIN refbrgy b ON
                                                            e.brgy_id = b.brgyCode
                                                        JOIN refcitymun c ON
                                                            e.city_id = c.citymunCode
                                                        JOIN refprovince p ON
                                                            e.province_id = p.provCode
                                                        JOIN POSITION po ON
                                                            e.employee_pos_id = po.position_id
                                                        WHERE
                                                            e.employee_id = '" + id + "'");
            while (dr.Read())
            {

                txtFName.Text = Convert.ToString(dr["employee_fname"]);
                txtMName.Text = Convert.ToString(dr["employee_mname"]);
                txtLName.Text = Convert.ToString(dr["employee_lname"]);

                cmbProvince.Text = Convert.ToString(dr["provDesc"]);
                prov_id = Convert.ToString(dr["province_id"]);
                prov_name = Convert.ToString(dr["provDesc"]);
                cmbCity.Text = Convert.ToString(dr["citymunDesc"]);
                city_id = Convert.ToString(dr["city_id"]);
                city_name = Convert.ToString(dr["citymunDesc"]);
                cmbBrgy.Text = Convert.ToString(dr["brgyDesc"]);
                brgy_id = Convert.ToString(dr["brgy_id"]);
                brgy_name = Convert.ToString(dr["brgyDesc"]);

                txtStreet.Text = Convert.ToString(dr["street"]);
                txtEmail.Text = Convert.ToString(dr["employee_email"]);
                txtPhone.Text = Convert.ToString(dr["employee_cont_no"]);
                txtSSS.Text = Convert.ToString(dr["employee_sss"]);
                txtPhilHealth.Text = Convert.ToString(dr["employee_philhealth"]);
                txtPagibig.Text = Convert.ToString(dr["employee_pagibig"]);
                txtTIN.Text = Convert.ToString(dr["employee_tin_no"]);

                cmbPosition.Text = Convert.ToString(dr["position_name"]);
                pos_id = Convert.ToString(dr["employee_pos_id"]);
                pos_name = Convert.ToString(dr["position_name"]);
                txtSalary.Text = Convert.ToString(dr["salary_rate"]);
            }
            sqlquery.CloseConnection();

            this.ShowDialog();

        }
    }
}
