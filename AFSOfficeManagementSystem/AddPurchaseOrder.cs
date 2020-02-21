using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFSOfficeManagementSystem
{
    public partial class AddPurchaseOrder : Form
    {
        SQLClass sqlquery = new SQLClass();
        double Ftotlbl = 0;
        double Fsublbl = 0;
        string id = "";
        int addEdit = 0, rowCtr = 0;
        bool flag = false;
        string dueDate;
        DialogResult dr = DialogResult.None;

        string supp_id, brn_id = "";
        string supp_name, brn_name = "";
        public AddPurchaseOrder()
        {
            InitializeComponent();

            this.ActiveControl = lblHeader;
            sqlquery.OpenConection();
            MySqlDataReader dr = sqlquery.DataReader(@"SELECT
                                                            MAX(purchase_id) + 1 as 'max_id'
                                                        FROM
                                                            purchase where is_deleted = 0");
            if (dr.Read())
            {
                if (Convert.ToString(dr["max_id"]) == null)
                {
                    label9.Text = "1";
                }
                else
                {
                    label9.Text = Convert.ToString(dr["max_id"]);
                }
            }
            sqlquery.CloseConnection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string payTerm = cmbPayment.Text;
            if(payTerm == "7 days")
            {
                dueDate = dateDeliver.Value.AddDays(7).ToString("yyyy-MM-dd");
            }
            else if (payTerm == "15 days")
            {
                dueDate = dateDeliver.Value.AddDays(15).ToString("yyyy-MM-dd");
            }
            else if (payTerm == "30 days")
            {
                dueDate = dateDeliver.Value.AddDays(30).ToString("yyyy-MM-dd");
            }


            Ftotlbl = 0;
            Fsublbl = 0;
            for (int i = 0; i < tblPurchases.Rows.Count - 1; i++)
            {
                Ftotlbl += Double.Parse(tblPurchases.Rows[i].Cells[5].Value.ToString());
                Fsublbl += Double.Parse(tblPurchases.Rows[i].Cells[3].Value.ToString()) * Double.Parse(tblPurchases.Rows[i].Cells[2].Value.ToString());
            }
            lblSub.Text = Fsublbl.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            lblTot.Text = Ftotlbl.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            if (addEdit == 0)
            {
                String qry = string.Format(@"INSERT INTO purchase(
                                                purchase_order_date,
                                                purchase_delivery_date,
                                                supplier_id,
                                                branch_id,
                                                purchase_amount,
                                                purchase_subtot,
                                                purchase_payment,
                                                purchase_due_date,
                                                purchase_number
                                            )
                                            VALUES('{0}',
                                                   '{1}',
                                                   '{2}',
                                                   '{3}',
                                                   '{4}',
                                                   '{5}',
                                                   '{6}',
                                                   '{7}',
                                                   '{8}'
                                    ); ", dateRequest.Text, dateDeliver.Text, cmbSupplier.SelectedValue, cmbBranch.SelectedValue, Ftotlbl, Fsublbl, cmbPayment.Text, dueDate, txtPONum.Text);
                sqlquery.ExecuteQueries(qry);
                String purch_id = "";
                sqlquery.OpenConection();
                MySqlDataReader dr = sqlquery.DataReader(@"SELECT
                                                            MAX(purchase_id) as 'max_id'
                                                        FROM
                                                            purchase where is_deleted = 0");
                if (dr.Read())
                {
                    purch_id = Convert.ToString(dr["max_id"]);
                }
                for (int i = 0; i < tblPurchases.Rows.Count - 1; i++)
                {
                    string qry1 = string.Format(@"INSERT INTO purchase_details(
                                                    purchase_id,
                                                    product_desc,
                                                    product_unit,
                                                    product_quan,
                                                    product_price,
                                                    product_tax,
                                                    product_amount)
                                                    VALUES(
                                                        '{0}',
                                                        '{1}',
                                                        '{2}',
                                                        '{3}',
                                                        '{4}',
                                                        '{5}',
                                                        '{6}'
                                    ); ", purch_id,
                                        tblPurchases.Rows[i].Cells[0].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[1].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[2].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[3].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[4].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[5].Value.ToString());
                    sqlquery.ExecuteQueries(qry1);
                }
            }
            else
            {
                if (cmbBranch.Text == brn_name | cmbSupplier.Text == supp_name)
                {
                    if (cmbBranch.Text == brn_name & cmbSupplier.Text != supp_name)
                    {
                        supp_id = cmbSupplier.SelectedValue.ToString();
                    }
                    else if(cmbBranch.Text != brn_name & cmbSupplier.Text == supp_name)
                    {
                        brn_id = cmbBranch.SelectedValue.ToString();
                    }
                    string qry = string.Format(@"UPDATE
                                        purchase
                                      SET
                                        purchase_order_date = '{0}',
                                        purchase_delivery_date = '{1}',
                                        supplier_id = '{2}',
                                        branch_id = '{3}',
                                        purchase_amount = '{4}',
                                        purchase_subtot = '{5}',
                                        purchase_payment = '{7}',
                                        purchase_due_date = '{8}',
                                        purchase_number = '{9}'
                                      WHERE
                                          purchase_id = '{6}'; ", dateRequest.Text,
                                                             dateDeliver.Text,
                                                             supp_id,
                                                             brn_id,
                                                             Ftotlbl,
                                                             Fsublbl,
                                                             id, cmbPayment.Text, dueDate, txtPONum.Text);
                    sqlquery.ExecuteQueries(qry);

                }
                else
                {
                    string qry = string.Format(@"UPDATE
                                        purchase
                                      SET
                                        purchase_order_date = '{0}',
                                        purchase_delivery_date = '{1}',
                                        supplier_id = '{2}',
                                        branch_id = '{3}',
                                        purchase_amount = '{4}',
                                        purchase_subtot = '{5}',
                                        purchase_payment = '{7}',
                                        purchase_due_date = '{8},
                                        purchase_due_date = '{9}
                                      WHERE
                                          purchase_id = '{6}'; ", dateRequest.Text,
                                                             dateDeliver.Text,
                                                             cmbSupplier.SelectedValue,
                                                             cmbBranch.SelectedValue,
                                                             Ftotlbl,
                                                             Fsublbl,
                                                             id, cmbPayment.Text, dueDate, txtPONum.Text);
                    sqlquery.ExecuteQueries(qry);
                }
                for (int i = 0; i < tblPurchases.Rows.Count - 1; i++)
                    {
                    if(tblPurchases.Rows[i].Cells[6].Value != null)
                    {
                        string qry1 = string.Format(@"update purchase_details set
                                                    product_desc = '{0}',
                                                    product_unit = '{1}',
                                                    product_quan = '{2}',
                                                    product_price = '{3}',
                                                    product_tax = '{4}',
                                                    product_amount = '{5}' where purch_det_id = '{6}';",
                                              tblPurchases.Rows[i].Cells[0].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[1].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[2].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[3].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[4].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[5].Value.ToString(), tblPurchases.Rows[i].Cells[6].Value.ToString());
                        sqlquery.ExecuteQueries(qry1);
                    }
                    else
                    {
                        string qry1 = string.Format(@"INSERT INTO purchase_details(
                                                    purchase_id,
                                                    product_desc,
                                                    product_unit,
                                                    product_quan,
                                                    product_price,
                                                    product_tax,
                                                    product_amount)
                                                    VALUES(
                                                        '{0}',
                                                        '{1}',
                                                        '{2}',
                                                        '{3}',
                                                        '{4}',
                                                        '{5}',
                                                        '{6}'
                                    ); ", id,
                                       tblPurchases.Rows[i].Cells[0].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[1].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[2].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[3].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[4].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[5].Value.ToString());
                        sqlquery.ExecuteQueries(qry1);
                    }                   

                   

                }

                
            }
            sqlquery.CloseConnection();
            btnPrint.Enabled = true;
            this.Close();
            MainForm obj = (MainForm)Application.OpenForms["MainForm"];
            obj.loadPurchData();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            cmbBranch.ForeColor = Color.Black;

            object ds = sqlquery.fillcombobox("select branch_id, branch_name from branch where is_deleted = 0;");
            cmbBranch.DataSource = ds;
            cmbBranch.DisplayMember = "branch_name";
            cmbBranch.ValueMember = "branch_id";
        }

        private void cmbSupplier_DropDown(object sender, EventArgs e)
        {
            cmbSupplier.ForeColor = Color.Black;

            object ds = sqlquery.fillcombobox("select supplier_id, supplier_name from supplier where is_deleted = 0;");
            cmbSupplier.DataSource = ds;
            cmbSupplier.DisplayMember = "supplier_name";
            cmbSupplier.ValueMember = "supplier_id";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog(label9.Text);
        }

        private void AddPurchaseOrder_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string payTerm = cmbPayment.Text;
            if (payTerm == "7 days")
            {
                dueDate = dateDeliver.Value.AddDays(7).ToString("yyyy-MM-dd");
            }
            else if (payTerm == "15 days")
            {
                dueDate = dateDeliver.Value.AddDays(15).ToString("yyyy-MM-dd");
            }
            else if (payTerm == "30 days")
            {
                dueDate = dateDeliver.Value.AddDays(30).ToString("yyyy-MM-dd");
            }

            Ftotlbl = 0;
            Fsublbl = 0;
            for (int i = 0; i < tblPurchases.Rows.Count - 1; i++)
            {
                Ftotlbl += Double.Parse(tblPurchases.Rows[i].Cells[5].Value.ToString());
                Fsublbl += Double.Parse(tblPurchases.Rows[i].Cells[3].Value.ToString()) * Double.Parse(tblPurchases.Rows[i].Cells[2].Value.ToString());
            }
            lblSub.Text = Fsublbl.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            lblTot.Text = Ftotlbl.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            if (addEdit == 0)
            {
                String qry = string.Format(@"INSERT INTO purchase(
                                                purchase_order_date,
                                                purchase_delivery_date,
                                                supplier_id,
                                                branch_id,
                                                purchase_amount,
                                                purchase_subtot,
                                                purchase_payment,
                                                purchase_due_date,
                                                purchase_number
                                            )
                                            VALUES('{0}',
                                                   '{1}',
                                                   '{2}',
                                                   '{3}',
                                                   '{4}',
                                                   '{5}',
                                                   '{6}',
                                                   '{7}',
                                                   '{8}'
                                    ); ", dateRequest.Text, dateDeliver.Text, cmbSupplier.SelectedValue, cmbBranch.SelectedValue, Ftotlbl, Fsublbl, cmbPayment.Text, dueDate, txtPONum.Text);
                sqlquery.ExecuteQueries(qry);
                String purch_id = "";
                sqlquery.OpenConection();
                MySqlDataReader dr = sqlquery.DataReader(@"SELECT
                                                            MAX(purchase_id) as 'max_id'
                                                        FROM
                                                            purchase where is_deleted = 0");
                if (dr.Read())
                {
                    purch_id = Convert.ToString(dr["max_id"]);
                }
                for (int i = 0; i < tblPurchases.Rows.Count - 1; i++)
                {
                    string qry1 = string.Format(@"INSERT INTO purchase_details(
                                                    purchase_id,
                                                    product_desc,
                                                    product_unit,
                                                    product_quan,
                                                    product_price,
                                                    product_tax,
                                                    product_amount)
                                                    VALUES(
                                                        '{0}',
                                                        '{1}',
                                                        '{2}',
                                                        '{3}',
                                                        '{4}',
                                                        '{5}',
                                                        '{6}'
                                    ); ", purch_id,
                                        tblPurchases.Rows[i].Cells[0].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[1].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[2].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[3].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[4].Value.ToString(),
                                        tblPurchases.Rows[i].Cells[5].Value.ToString());
                    sqlquery.ExecuteQueries(qry1);
                }
            }
            else
            {
                if (cmbBranch.Text == brn_name | cmbSupplier.Text == supp_name)
                {
                    if (cmbBranch.Text == brn_name & cmbSupplier.Text != supp_name)
                    {
                        supp_id = cmbSupplier.SelectedValue.ToString();
                    }
                    else if (cmbBranch.Text != brn_name & cmbSupplier.Text == supp_name)
                    {
                        brn_id = cmbBranch.SelectedValue.ToString();
                    }
                    string qry = string.Format(@"UPDATE
                                        purchase
                                      SET
                                        purchase_order_date = '{0}',
                                        purchase_delivery_date = '{1}',
                                        supplier_id = '{2}',
                                        branch_id = '{3}',
                                        purchase_amount = '{4}',
                                        purchase_subtot = '{5}',
                                        purchase_payment = '{7}',
                                        purchase_due_date = '{8}',
                                        purchase_number = '{9}'
                                      WHERE
                                          purchase_id = '{6}'; ", dateRequest.Text,
                                                             dateDeliver.Text,
                                                             supp_id,
                                                             brn_id,
                                                             Ftotlbl,
                                                             Fsublbl,
                                                             id, cmbPayment.Text, dueDate, txtPONum.Text);
                    sqlquery.ExecuteQueries(qry);

                }
                else
                {
                    string qry = string.Format(@"UPDATE
                                        purchase
                                      SET
                                        purchase_order_date = '{0}',
                                        purchase_delivery_date = '{1}',
                                        supplier_id = '{2}',
                                        branch_id = '{3}',
                                        purchase_amount = '{4}',
                                        purchase_subtot = '{5}',
                                        purchase_payment = '{7}',
                                        purchase_due_date = '{8},
                                        purchase_number = '{9}'
                                      WHERE
                                          purchase_id = '{6}'; ", dateRequest.Text,
                                                             dateDeliver.Text,
                                                             cmbSupplier.SelectedValue,
                                                             cmbBranch.SelectedValue,
                                                             Ftotlbl,
                                                             Fsublbl,
                                                             id, cmbPayment.Text, dueDate, txtPONum.Text);
                    sqlquery.ExecuteQueries(qry);
                }
                for (int i = 0; i < tblPurchases.Rows.Count - 1; i++)
                {
                    if (tblPurchases.Rows[i].Cells[6].Value != null)
                    {
                        string qry1 = string.Format(@"update purchase_details set
                                                    product_desc = '{0}',
                                                    product_unit = '{1}',
                                                    product_quan = '{2}',
                                                    product_price = '{3}',
                                                    product_tax = '{4}',
                                                    product_amount = '{5}' where purch_det_id = '{6}';",
                                              tblPurchases.Rows[i].Cells[0].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[1].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[2].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[3].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[4].Value.ToString(),
                                              tblPurchases.Rows[i].Cells[5].Value.ToString(), tblPurchases.Rows[i].Cells[6].Value.ToString());
                        sqlquery.ExecuteQueries(qry1);
                    }
                    else
                    {
                        string qry1 = string.Format(@"INSERT INTO purchase_details(
                                                    purchase_id,
                                                    product_desc,
                                                    product_unit,
                                                    product_quan,
                                                    product_price,
                                                    product_tax,
                                                    product_amount)
                                                    VALUES(
                                                        '{0}',
                                                        '{1}',
                                                        '{2}',
                                                        '{3}',
                                                        '{4}',
                                                        '{5}',
                                                        '{6}'
                                    ); ", id,
                                       tblPurchases.Rows[i].Cells[0].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[1].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[2].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[3].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[4].Value.ToString(),
                                       tblPurchases.Rows[i].Cells[5].Value.ToString());
                        sqlquery.ExecuteQueries(qry1);
                    }



                }


            }
            sqlquery.CloseConnection();
            this.Close();
            Form1 f1 = new Form1();
            f1.ShowDialog(label9.Text);
            MainForm obj = (MainForm)Application.OpenForms["MainForm"];
            obj.loadPurchData();
        }

        private void tblPurchases_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!flag)
            {
                dr = MessageBox.Show("Are you sure to delete row?", "Confirmation", MessageBoxButtons.YesNo);
                flag = true;
            }
            if (dr == DialogResult.Yes)
            {
                if (addEdit == 1)
                {
                    foreach (DataGridViewRow row in tblPurchases.SelectedRows)
                    {
                        string qry2 = string.Format(@"update purchase_details set
                                                    is_deleted = 1 where purch_det_id = '{0}';",
                                                                            row.Cells[6].Value.ToString());
                        sqlquery.ExecuteQueries(qry2);
                    }
                }
                
            }
            else
            {
                e.Cancel = true;
            }
            if (rowCtr == tblPurchases.SelectedRows.Count)
            {
                flag = false;
                rowCtr = 0;
            }
            rowCtr++;
        }

        private void btnVouch_Click(object sender, EventArgs e)
        {
            AddVoucher av = new AddVoucher();
            av.ShowDialog(id,"newPO");
        }


        private void tblPurchases_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Ftotlbl = 0;
            Fsublbl = 0;
            for (int i = 0; i < tblPurchases.Rows.Count - 1; i++)
            {
                Ftotlbl += Double.Parse(tblPurchases.Rows[i].Cells[5].Value.ToString());
                Fsublbl += Double.Parse(tblPurchases.Rows[i].Cells[3].Value.ToString()) * Double.Parse(tblPurchases.Rows[i].Cells[2].Value.ToString());
            }
            lblSub.Text = Fsublbl.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            lblTot.Text = Ftotlbl.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));

            if(addEdit == 1)
            {
                string qry = string.Format(@"UPDATE
                                        purchase
                                      SET
                                        purchase_amount = '{0}',
                                        purchase_subtot = '{1}'
                                      WHERE
                                          purchase_id = '{2}'; ",
                                          Ftotlbl,
                                          Fsublbl,
                                          id);
                sqlquery.ExecuteQueries(qry);
            }
        }

        public void ShowDialog(string bName, string todo)
        {
            btnVouch.Show();
            id = bName;
            label9.Text = id;
            string paidUnpaid = "";
            sqlquery.OpenConection();
            string sql = @"SELECT
                                product_desc,
                                product_unit,
                                product_quan,
                                product_price,
                                product_tax,
                                product_amount,
                                purch_det_id
                            FROM
                                purchase_details
                            WHERE
                                is_deleted = 0 AND purchase_id = '" + id + "'; ";
            double amount, amt_ctr = 0, sub_ctr = 0;
            string dispAmt;
            DataTable tbl = new DataTable();
            MySqlConnection con = new MySqlConnection(sqlquery.ConnectionString);
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
            dr.Fill(tbl);
            cmbBranch.ForeColor = Color.Black;
            cmbPayment.ForeColor = Color.Black;
            cmbSupplier.ForeColor = Color.Black;
            sqlquery.CloseConnection();

            foreach (DataRow row in tbl.Rows)
            {
                amount = Double.Parse(row[5].ToString()); //Double.Parse(row[2].ToString()) * Double.Parse(row[3].ToString()) + ((Double.Parse(row[2].ToString()) * Double.Parse(row[3].ToString())) * (Double.Parse(row[4].ToString()) / 100));
                sub_ctr += Double.Parse(row[2].ToString()) * Double.Parse(row[3].ToString());
                amt_ctr += amount;
                if (todo == "view")
                {
                    dispAmt = amount.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                }
                else
                {
                    dispAmt = amount.ToString();
                }
                tblPurchases.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), dispAmt, row[6].ToString());
            }
            sqlquery.OpenConection();
            MySqlDataReader dr1 = sqlquery.DataReader(@"SELECT
                                                            p.purchase_order_date,
                                                            p.purchase_delivery_date,
                                                            p.purchase_amount,
                                                            p.purchase_subtot,
                                                            p.purchase_number,
                                                            s.supplier_name,
                                                            s.supplier_id,
                                                            b.branch_name,
                                                            p.purchase_payment,
                                                            b.branch_id,
                                                            p.purchase_stat
                                                        FROM
                                                            purchase p
                                                        JOIN supplier s ON
                                                            s.supplier_id = p.supplier_id
                                                        JOIN branch b ON
                                                            p.branch_id = b.branch_id
                                                        WHERE
                                                            p.purchase_id ='" + id + "' and p.is_deleted = 0;");
            while (dr1.Read())
            {
                dateRequest.Value = Convert.ToDateTime(dr1["purchase_order_date"]);
                dateDeliver.Value = Convert.ToDateTime(dr1["purchase_delivery_date"]);

                cmbSupplier.Text = Convert.ToString(dr1["supplier_name"]);
                supp_id = Convert.ToString(dr1["supplier_id"]);
                supp_name = Convert.ToString(dr1["supplier_name"]);

                cmbBranch.Text = Convert.ToString(dr1["branch_name"]);
                brn_id = Convert.ToString(dr1["branch_id"]);
                brn_name = Convert.ToString(dr1["branch_name"]);

                txtPONum.Text = Convert.ToString(dr1["purchase_number"]);
                cmbPayment.Text = Convert.ToString(dr1["purchase_payment"]);

                paidUnpaid = Convert.ToString(dr1["purchase_stat"]);
            }
            if (paidUnpaid == "Paid")
            {
                btnVouch.Hide();
            }
            if (todo == "view" || paidUnpaid == "Paid")
            {
                txtPONum.Enabled = false;
                cmbPayment.Enabled = false;
                cmbBranch.Enabled = false;
                cmbSupplier.Enabled = false;
                dateRequest.Enabled = false;
                dateDeliver.Enabled = false;
                tblPurchases.ReadOnly = true;
                tblPurchases.AllowUserToDeleteRows = false;
                tblPurchases.AllowUserToAddRows = false;
                btnSave.Enabled = false;
                btnSave.Text = "SAVE";
                btnPrntSv.Enabled = false;
                btnPrntSv.Text = "PRINT/SAVE";
                lblSub.Text = sub_ctr.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                lblTot.Text = amt_ctr.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            }
            else
            {
                addEdit = 1;
                txtPONum.Enabled = true;
                cmbPayment.Enabled = true;
                cmbBranch.Enabled = true;
                cmbSupplier.Enabled = true;
                dateRequest.Enabled = true;
                dateDeliver.Enabled = true;
                tblPurchases.ReadOnly = false;
                tblPurchases.AllowUserToAddRows = true;
                btnSave.Enabled = true;
                btnSave.Text = "UPDATE";
                btnPrntSv.Enabled = true;
                btnPrntSv.Text = "PRINT/UPDATE";
                lblSub.Text = sub_ctr.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                lblTot.Text = amt_ctr.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            }
           
            sqlquery.CloseConnection();
            this.ShowDialog();
        }


        private void tblPurchases_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Ftotlbl = 0;
            Fsublbl = 0;
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4)

            {
                double quantity = Convert.ToDouble(tblPurchases.CurrentRow.Cells[2].Value);
                double untPrc = Convert.ToDouble(tblPurchases.CurrentRow.Cells[3].Value);
                double tax = Convert.ToDouble(tblPurchases.CurrentRow.Cells[4].Value);
                double total = Convert.ToDouble(tblPurchases.CurrentRow.Cells[5].Value);
                if (quantity.ToString() != "" && untPrc.ToString() != "")
                {
                    tblPurchases.CurrentRow.Cells[5].Value = quantity * untPrc + ((untPrc * quantity) * (tax / 100));
                }
                if(total != 0)
                {
                    for (int i = 0; i<tblPurchases.Rows.Count - 1; i++)
                    {
                        Ftotlbl += Double.Parse(tblPurchases.Rows[i].Cells[5].Value.ToString());
                        Fsublbl += Double.Parse(tblPurchases.Rows[i].Cells[3].Value.ToString()) * Double.Parse(tblPurchases.Rows[i].Cells[2].Value.ToString());
                    }
                    lblSub.Text = Fsublbl.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                    lblTot.Text = Ftotlbl.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                }
            }
        }
    }
}
