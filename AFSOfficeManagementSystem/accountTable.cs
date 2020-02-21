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
    public partial class accountTable : Form
    {
        SQLClass sqlquery = new SQLClass();
        public accountTable()
        {
            InitializeComponent();
        }

        private void accountTable_Load(object sender, EventArgs e)
        {
            loadAccData();
        }
        public void loadAccData()
        {
            dataGridView5.DataSource = sqlquery.ShowDataInGridView(@"SELECT
                                                                        account_id AS 'ID',
                                                                        account_bank AS 'BANK ACCOUNT',
                                                                        account_name AS 'ACCOUNT NAME',
                                                                        account_number AS 'ACCOUNT NUMBER',
                                                                        account_bal AS 'BALANCE'
                                                                    FROM
                                                                        accounts
                                                                    WHERE
                                                                        is_deleted = 0; ");
            dataGridView5.Columns[4].DefaultCellStyle.Format = "C2";
            dataGridView5.Columns[4].DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("en-PH");
            dataGridView5.ClearSelection();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView5.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView5.Rows[selectedrowindex];
                string Accid = Convert.ToString(selectedRow.Cells["ID"].Value);
                string bankName = Convert.ToString(selectedRow.Cells["BANK ACCOUNT"].Value) + " - " + Convert.ToString(selectedRow.Cells["ACCOUNT NUMBER"].Value);
                AddVoucher.accBal = Double.Parse(Convert.ToString(selectedRow.Cells["BALANCE"].Value).Replace("\"", ""), NumberStyles.Currency);
                AddVoucher f1 = (AddVoucher)Application.OpenForms["AddVoucher"];
                TextBox tb = (TextBox)f1.Controls["txtAccNm"];
                Label lb = (Label)f1.Controls["lblAccID"];
                tb.Text = bankName;
                lb.Text = Accid;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAccount ac = new AddAccount();
            ac.ShowDialog();
        }
    }
}
