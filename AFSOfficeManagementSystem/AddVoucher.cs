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
    public partial class AddVoucher : Form
    {
        SQLClass sqlquery = new SQLClass();
        string id;
        double tot;
        public static double accBal;

        public AddVoucher()
        {
            InitializeComponent();
        }
        public void ShowDialog(string poID, string todo)
        {
            if(todo == "newPO")
            {
                btnPrint.Enabled = false;
            }
            else
            {
                btnPrint.Enabled = true;
            }
            id = label9.Text = poID;
            sqlquery.OpenConection();
            string sql = @"SELECT
                                product_unit,
                                product_quan,
                                product_desc,
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
            sqlquery.CloseConnection();

            foreach (DataRow row in tbl.Rows)
            {
                amount = Double.Parse(row[5].ToString()); //Double.Parse(row[2].ToString()) * Double.Parse(row[3].ToString()) + ((Double.Parse(row[2].ToString()) * Double.Parse(row[3].ToString())) * (Double.Parse(row[4].ToString()) / 100));
                sub_ctr += Double.Parse(row[1].ToString()) * Double.Parse(row[3].ToString());
                amt_ctr += amount;
                dispAmt = amount.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                
                tblPurchases.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), dispAmt);
            }
            sqlquery.OpenConection();
            MySqlDataReader dr1 = sqlquery.DataReader(@"SELECT
                                                            p.purchase_order_date,
                                                            p.purchase_delivery_date,
                                                            p.purchase_amount,
                                                            p.purchase_number,
                                                            s.supplier_name,
                                                            b.branch_name,
                                                            p.purchase_due_date,
                                                            p.purchase_payment
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
                dateDeliver.Value = Convert.ToDateTime(dr1["purchase_delivery_date"]);
                dueDate.Value = Convert.ToDateTime(dr1["purchase_due_date"]);

                lblSupp.Text = Convert.ToString(dr1["supplier_name"]);
                tot = Convert.ToDouble(dr1["purchase_amount"]);
                lblTotBot.Text = lblTotTop.Text = tot.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));

                lblterm.Text = Convert.ToString(dr1["purchase_payment"]);
                lblBranch.Text = Convert.ToString(dr1["branch_name"]);

            }
            sqlquery.CloseConnection();
            this.ShowDialog();
        }

        private void btnPrntSv_Click(object sender, EventArgs e)
        {
            if(accBal >= tot)
            {
                sqlquery.OpenConection();
                string qry = string.Format(@"UPDATE
                                        purchase
                                      SET
                                        purchase_stat = 'Paid'
                                      WHERE
                                          purchase_id = '{0}'; ", id);
                sqlquery.ExecuteQueries(qry);

                string qry1 = string.Format(@"UPDATE
                                            accounts
                                        SET
                                            account_bal = {0}
                                                        WHERE
                                            account_id = '{1}'; ", (accBal-tot), lblAccID.Text);
                sqlquery.ExecuteQueries(qry1);

                string qry3 = string.Format(@"INSERT INTO voucher(
                                            account_id,
                                            check_no,
                                            receiver,
                                            purchase_id,
                                            voucher_date,
                                            voucher_num
                                        )
                                        VALUES('{0}', '{1}', '{2}', {3}, '{4}', '{5}'); ", lblAccID.Text,
                                                                                   txtCheck.Text,
                                                                                   txtRcv.Text,
                                                                                   label9.Text,
                                                                                   dateVouch.Text,
                                                                                   txtVouchNum.Text);
                sqlquery.ExecuteQueries(qry3);


                sqlquery.CloseConnection();
                MainForm obj = (MainForm)Application.OpenForms["MainForm"];
                obj.loadPurchData();
                this.Close();
            }
            else
            {
                MessageBox.Show("Insufficient Fund!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            accountTable at = new accountTable();
            at.ShowDialog();
        }
    }
}
