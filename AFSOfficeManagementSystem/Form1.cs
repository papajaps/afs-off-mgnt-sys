using Microsoft.Reporting.WinForms;
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
    public partial class Form1 : Form
    {
        SQLClass sqlquery = new SQLClass();
        string id;
        DateTime dd,od;
        string fdd, pon, fod, supp, bran;
        double subt, tot;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("paramPON", pon));
            parameters.Add(new ReportParameter("paramDD", fdd));
            parameters.Add(new ReportParameter("paramOD", fod));
            parameters.Add(new ReportParameter("paramSupp", supp));
            parameters.Add(new ReportParameter("paramBran", bran));

            parameters.Add(new ReportParameter("paramTot", tot.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"))));
            parameters.Add(new ReportParameter("paramSubt", subt.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-PH"))));
            this.reportViewer1.LocalReport.SetParameters(parameters);

            DataSet1 ds = GetData(@"SELECT
                                        product_desc AS 'Description',
                                        product_unit AS 'Unit',
                                        product_quan AS 'Quantity',
                                        product_price AS 'Unit_Price',
                                        product_tax AS 'Tax',
                                        CONCAT('₱', FORMAT(product_amount, 'C')) AS 'Amount'
                                    FROM
                                        purchase_details where purchase_id = '" + id + "' and is_deleted = 0;");

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(datasource);

            this.reportViewer1.RefreshReport();
        }
        public DataSet1 GetData(string qry)
        {
            MySqlConnection con;
            con = new MySqlConnection(sqlquery.ConnectionString);
            MySqlCommand cmd = new MySqlCommand(qry, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "DataTable1");
            return ds;
        }
        public void ShowDialog(string bName)
        {
            id = bName;
            sqlquery.OpenConection();
            MySqlDataReader dr = sqlquery.DataReader(@"SELECT
                                                            p.purchase_order_date,
                                                            p.purchase_delivery_date,
                                                            p.purchase_amount,
                                                            p.purchase_subtot,
                                                            p.purchase_number,
                                                            s.supplier_name,
                                                            b.branch_name
                                                        FROM
                                                            purchase p
                                                        JOIN supplier s ON
                                                            s.supplier_id = p.supplier_id
                                                        JOIN branch b ON
                                                            p.branch_id = b.branch_id
                                                        WHERE
                                                            p.purchase_id ='" + id + "' and p.is_deleted = 0;");
            while (dr.Read())
            {
                pon = Convert.ToString(dr["purchase_number"]);
                od = Convert.ToDateTime(dr["purchase_order_date"]);
                fod = od.ToString("MMM/dd/yyyy");
                dd = Convert.ToDateTime(dr["purchase_delivery_date"]);
                fdd = dd.ToString("MMM/dd/yyyy");
                supp = Convert.ToString(dr["supplier_name"]);
                bran = Convert.ToString(dr["branch_name"]);
                subt = Convert.ToDouble(dr["purchase_subtot"]);
                tot = Convert.ToDouble(dr["purchase_amount"]);
            }
            sqlquery.CloseConnection();
            this.ShowDialog();

        }
    }
}
