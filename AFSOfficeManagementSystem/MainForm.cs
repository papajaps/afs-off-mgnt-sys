﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace AFSOfficeManagementSystem
{
    public partial class MainForm : Form
    {
        SQLClass sqlquery = new SQLClass();
        string del_acc_id = "";
        string branch_id = "";
        string supp_id = "";
        string emp_id = "";
        string purch_id = "";
        public static string account_name = "";
        public static string bank_name = "";
        public static string account_number = "";
        public static decimal account_bal = 0;
        public MainForm()
        {
            InitializeComponent();
            lblHeader.Text = "DASHBOARD";
            dateTimePicker1.CustomFormat = "MMMM yyyy";
            dataGridView5.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView5_CellMouseDown);
            int weeks = MondaysInMonth(new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1));
            for (int i = 1; i <= weeks; i++)
            {
                cmbWeek.Items.Add(i);

            }
            lblDateTime.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            dataGridView1.Width = pnlDashboard.Width/(2) + 100;
            dataGridView2.Width = pnlDashboard.Width/(2) + 100;
        }


        //Accounts CMS start

        private void dataGridView5_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    del_acc_id = dataGridView5.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    account_name = dataGridView5.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                    bank_name = dataGridView5.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    account_number = dataGridView5.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                    char[] balchar = dataGridView5.Rows[e.RowIndex].Cells[4].FormattedValue.ToString().ToCharArray();
                    balchar[0] = '$';
                    string newstring = new string(balchar);
                    account_bal = Decimal.Parse(newstring, NumberStyles.Currency);
                    int rowSelected = e.RowIndex;
                    if (e.RowIndex != -1)
                    {
                        dataGridView5.ClearSelection();
                        dataGridView5.Rows[rowSelected].Selected = true;
                    }
                }
            }
            
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddAccount aa = new AddAccount();
            aa.ShowDialog(bank_name, account_bal, account_name, account_number, "edit", del_acc_id);
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Int32 rowToDelete = dataGridView5.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                dataGridView5.Rows.RemoveAt(rowToDelete);
                dataGridView5.ClearSelection();
                string qry = string.Format("UPDATE accounts set is_deleted = 1 where account_id = '" + del_acc_id + "';");
                sqlquery.ExecuteQueries(qry);
            }
            else
            {

            }
            
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                for (j = 0; j < dataGridView5.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView5.Columns[j].HeaderText;
                }

                StartRow++;

                for (i = 0; i < dataGridView5.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridView5.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dataGridView5[j, i].Value == null ? "" : dataGridView5[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Accounts CMS end

        //branch CMS start
        private void dataGridView7_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    branch_id = dataGridView7.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    int rowSelected = e.RowIndex;
                    if (e.RowIndex != -1)
                    {
                        dataGridView7.ClearSelection();
                        dataGridView7.Rows[rowSelected].Selected = true;
                    }
                }
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBranch ab = new AddBranch();
            ab.ShowDialog(branch_id);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Int32 rowToDelete = dataGridView7.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                dataGridView7.Rows.RemoveAt(rowToDelete);
                dataGridView7.ClearSelection();
                string qry = string.Format("UPDATE branch set is_deleted = 1 where branch_id = '" + branch_id + "';");
                sqlquery.ExecuteQueries(qry);
                sqlquery.CloseConnection();
            }
            else
            {

            }
        }

        private void openAsExcelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                for (j = 0; j < dataGridView7.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView7.Columns[j].HeaderText;
                }

                StartRow++;

                for (i = 0; i < dataGridView7.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridView7.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dataGridView7[j, i].Value == null ? "" : dataGridView7[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //branch CMS end

        //supplier cms start
        private void dataGridView8_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    supp_id = dataGridView8.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    int rowSelected = e.RowIndex;
                    if (e.RowIndex != -1)
                    {
                        dataGridView8.ClearSelection();
                        dataGridView8.Rows[rowSelected].Selected = true;
                    }
                }
            }
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddSupplier asup = new AddSupplier();
            asup.ShowDialog(supp_id);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                        "Confirm Delete.",
                                        MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Int32 rowToDelete = dataGridView8.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                dataGridView8.Rows.RemoveAt(rowToDelete);
                dataGridView8.ClearSelection();
                string qry = string.Format("UPDATE supplier set is_deleted = 1 where supplier_id = '" + supp_id + "';");
                sqlquery.ExecuteQueries(qry);
                sqlquery.CloseConnection();
            }
            else
            {

            }

        }

        private void openAsExcelFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                for (j = 0; j < dataGridView8.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView8.Columns[j].HeaderText;
                }

                StartRow++;

                for (i = 0; i < dataGridView8.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridView8.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dataGridView8[j, i].Value == null ? "" : dataGridView8[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // supplier cms end


        // Employee CMS start
        private void dataGridView6_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    emp_id = dataGridView6.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    int rowSelected = e.RowIndex;
                    if (e.RowIndex != -1)
                    {
                        dataGridView6.ClearSelection();
                        dataGridView6.Rows[rowSelected].Selected = true;
                    }
                }
            }
        }
        private void editToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AddEmployee asup = new AddEmployee();
            asup.ShowDialog(emp_id);
        }

        private void deleteToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                        "Confirm Delete.",
                                        MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Int32 rowToDelete = dataGridView6.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                dataGridView6.Rows.RemoveAt(rowToDelete);
                dataGridView6.ClearSelection();
                string qry = string.Format("UPDATE employee set is_deleted = 1 where employee_id = '" + emp_id + "';");
                sqlquery.ExecuteQueries(qry);
                sqlquery.CloseConnection();
            }
            else
            {

            }
        }

        private void openAsExcelFileToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                for (j = 0; j < dataGridView6.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView6.Columns[j].HeaderText;
                }

                StartRow++;

                for (i = 0; i < dataGridView6.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridView6.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dataGridView6[j, i].Value == null ? "" : dataGridView6[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        // Employee CMS end

        //purchase cms start

        private void tblPurchaseOrder_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    purch_id = tblPurchaseOrder.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    int rowSelected = e.RowIndex;
                    if (e.RowIndex != -1)
                    {
                        tblPurchaseOrder.ClearSelection();
                        tblPurchaseOrder.Rows[rowSelected].Selected = true;
                    }
                }
            }

        }
        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddPurchaseOrder apo = new AddPurchaseOrder();
            apo.Text = "Edit Information";
            apo.ShowDialog(purch_id, "edit");
        }

        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddPurchaseOrder apo = new AddPurchaseOrder();
            apo.Text = "Information";
            apo.ShowDialog(purch_id, "view");
        }

        private void openAsExcelFileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                for (j = 0; j < tblPurchaseOrder.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = tblPurchaseOrder.Columns[j].HeaderText;
                }

                StartRow++;

                for (i = 0; i < tblPurchaseOrder.Rows.Count; i++)
                {
                    for (j = 0; j < tblPurchaseOrder.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = tblPurchaseOrder[j, i].Value == null ? "" : tblPurchaseOrder[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void deleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                       "Confirm Delete.",
                                       MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Int32 rowToDelete = tblPurchaseOrder.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                tblPurchaseOrder.Rows.RemoveAt(rowToDelete);
                tblPurchaseOrder.ClearSelection();
                string qry = string.Format("UPDATE purchase set is_deleted = 1 where purch_id = '" + purch_id + "';");
                sqlquery.ExecuteQueries(qry);
                sqlquery.CloseConnection();
            }
            else
            {

            }
        }
        //purchase cms end

        private void Button_MouseHover(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = ColorTranslator.FromHtml("#74E39A");
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = ColorTranslator.FromHtml("#3BD16F");
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel panel = new Panel();
            if (button.Name == "btnDashboard")
            {
                lblHeader.Text = "DASHBOARD";
                panel = pnlDashboard;
            }
            else if (button.Name == "btnAccounts")
            {
                lblHeader.Text = "ACCOUNTS";
                panel = pnlAccounts;
                loadAccData();
            }
            else if (button.Name == "btnExpenses")
            {
                lblHeader.Text = "EXPENSES";
                panel = pnlExpenses;
                loadExpenses();
            }
            else if (button.Name == "btnSupplier")
            {
                lblHeader.Text = "SUPPLIER";
                panel = pnlSupplier;
                loadSuppData();
            }
            else if (button.Name == "btnPurchOrder")
            {
                lblHeader.Text = "PURCHASE ORDER";
                panel = pnlPurchOrder;
                loadPurchData();
            }
            else if (button.Name == "btnVoucher")
            {
                lblHeader.Text = "VOUCHER";
                panel = pnlVoucher;
                loadVouchData();
            }
            else if (button.Name == "btnAccounting")
            {
                lblHeader.Text = "ACCOUNTING";
                panel = pnlAccounting;
            }
            else if (button.Name == "btnEmployee")
            {
                lblHeader.Text = "EMPLOYEE";
                panel = pnlEmployee;
                loadEmpData();
            }
            else if (button.Name == "btnBranches")
            {
                lblHeader.Text = "BRANCHES";
                panel = pnlBranches;
                loadBranData();
            }
            foreach (Control pnl in pnlContainer.Controls)
            {
                pnl.Visible = false;
            }
            panel.Visible = true;
            foreach (Control btn in pnlMenu.Controls)
            {
                btn.BackColor = Color.White;
                btn.Font = new Font("MS Reference Sans Serif", 9, FontStyle.Regular);
            }
            button.BackColor = Color.Gainsboro;
            button.Font = new Font("MS Reference Sans Serif", 9, FontStyle.Bold);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.Gainsboro;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            int[] yValues = { 78, 45, 50, 88, 80, 25, 30}; // Here y values is related to display three month values
            string[] xValues = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
            chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chart2.Series["Series1"].Points.DataBindXY(xValues, yValues);

            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart2.ChartAreas[0].AxisX.Interval = 1;

            txtSearch.GotFocus += new EventHandler(this.TextGotFocus);
            txtSearch.LostFocus += new EventHandler(this.TextLostFocus);
        }

        public void TextGotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "SEARCH")
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
                txtSearch.Text = "SEARCH";
                tb.ForeColor = Color.Gray;
            }
        }

        public void loadCategory()
        {
            tblCategory.DataSource = sqlquery.ShowDataInGridView(@"SELECT
                                                                        ctg_id AS 'CATEGORY ID',
                                                                        ctg_name AS 'CATEGORY'
                                                                    FROM
                                                                        ecategory");
            tblCategory.Columns["CATEGORY ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void loadExpenses()
        {
            tblExpenses.DataSource = sqlquery.ShowDataInGridView(@"SELECT
                                                                        e.expense_id AS 'ID',
                                                                        c.ctg_name AS 'CATEGORY',
                                                                        e.expense_desc AS 'DESCRIPTION',
                                                                        e.expense_amnt AS 'AMOUNT',
                                                                        e.expense_stat AS 'STATUS',
                                                                        e.expense_date_paid AS 'DATE PAID'
                                                                    FROM
                                                                        expense e
                                                                    JOIN ecategory c ON
                                                                        e.ctg_id = c.ctg_id
                                                                    WHERE
                                                                        e.is_deleted = 0;");
            tblExpenses.Columns["ID"].Visible = false;
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

        public void loadPurchData()
        {
            tblPurchaseOrder.DataSource = sqlquery.ShowDataInGridView(@"SELECT
                                                                            p.purchase_id AS 'ID',
                                                                            s.supplier_name AS 'SUPPLIER',
                                                                            p.purchase_number AS 'PO NUMBER',
                                                                            b.branch_name AS 'BRANCH NAME',
                                                                            p.purchase_order_date AS 'ORDER DATE',
                                                                            p.purchase_stat AS 'STATUS'
                                                                        FROM
                                                                            purchase p
                                                                        JOIN supplier s ON
                                                                            p.supplier_id = s.supplier_id
                                                                        JOIN branch b ON
                                                                            p.branch_id = b.branch_id
                                                                        WHERE
                                                                            p.is_deleted = 0;");
            tblPurchaseOrder.ClearSelection();
        }

        public void loadBranData()
        {
            dataGridView7.DataSource = sqlquery.ShowDataInGridView(@"SELECT
                                                                        br.branch_id AS 'BRANCH ID',
                                                                        br.branch_name AS 'BRANCH NAME',
                                                                        CONCAT(
                                                                            UPPER(b.brgyDesc),
                                                                            ', ',
                                                                            c.citymunDesc,
                                                                            ', ', p.provDesc
                                                                        ) AS 'Address',
                                                                        COncat(e.employee_fname, ' ', e.employee_lname) AS 'BRANCH PERSONNEL',
                                                                        br.branch_cont_no AS 'CONTACT NUMBER'
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
                                                                        br.is_deleted = 0;");
            dataGridView7.ClearSelection();
        }
        public void loadSuppData()
        {
            dataGridView8.DataSource = sqlquery.ShowDataInGridView(@"SELECT
                                                                        supplier_id as 'ID',       
                                                                        supplier_date_added AS 'DATE CREATED',
                                                                        supplier_name AS 'SUPPLIER NAME',
                                                                        supplier_stat as 'STATUS'
                                                                    FROM
                                                                        supplier
                                                                    WHERE
                                                                        is_deleted = 0;");

            dataGridView8.ClearSelection();
        }
        public void loadVouchData()
        {
            dataGridView10.DataSource = sqlquery.ShowDataInGridView(@"SELECT
                                                                        v.voucher_id AS 'ID',
                                                                        s.supplier_name AS 'SUPPLIER',
                                                                        v.voucher_num AS 'VOUCHER NUMBER',
                                                                        b.branch_name AS 'BRANCH',
                                                                        v.voucher_date AS 'DATE'
                                                                    FROM
                                                                        voucher v
                                                                    JOIN purchase p ON
                                                                        v.purchase_id = p.purchase_id
                                                                    JOIN supplier s ON
                                                                        s.supplier_id = p.supplier_id
                                                                    JOIN branch b ON
                                                                        b.branch_id = p.branch_id
                                                                    WHERE
                                                                        v.is_deleted = 0;");

            dataGridView10.ClearSelection();
        }
        public void loadEmpData()
        {
            dataGridView6.DataSource = sqlquery.ShowDataInGridView(@"SELECT
                                                                        e.employee_id AS 'ID',
                                                                        CONCAT(
                                                                            e.employee_fname,
                                                                            ' ',
                                                                            e.employee_lname
                                                                        ) AS 'NAME',
                                                                        p.position_name AS 'POSITION',
                                                                        p.salary_rate AS 'SALARY',
                                                                        e.employee_tin_no AS 'TIN No.',
                                                                        e.employee_sss AS 'SSS No.'
                                                                    FROM
                                                                        employee e
                                                                    JOIN position p ON
                                                                        p.position_id = e.employee_pos_id
                                                                    WHERE
                                                                        e.is_deleted = 0;");
            dataGridView6.ClearSelection();

        }
        public static int MondaysInMonth(DateTime thisMonth)
        {
            int mondays = 0;
            int month = thisMonth.Month;
            int year = thisMonth.Year;
            int daysThisMonth = DateTime.DaysInMonth(year, month);
            DateTime beginingOfThisMonth = new DateTime(year, month, 1);
            for (int i = 0; i < daysThisMonth; i++)
                if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                    mondays++;
            return mondays;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            cmbWeek.Items.Clear();
            int weeks = MondaysInMonth(new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1));
            for (int i = 1; i<= weeks; i++)
            {
                cmbWeek.Items.Add(i);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            AddAccount aa = new AddAccount();
            aa.ShowDialog();
        }             


        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeleteRow_Opening(object sender, CancelEventArgs e)
        {
            var cms = sender as ContextMenuStrip;
            var mousepos = Control.MousePosition;
            if (cms != null)
            {
                var rel_mousePos = cms.PointToClient(mousepos);
                if (cms.ClientRectangle.Contains(rel_mousePos))
                {
                    var dgv_rel_mousePos = dataGridView5.PointToClient(mousepos);
                    var hti = dataGridView5.HitTest(dgv_rel_mousePos.X, dgv_rel_mousePos.Y);
                    if (hti.RowIndex == -1)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddBranch ab = new AddBranch();
            ab.ShowDialog();
        }


        private void cmbBranches_DropDown(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.ForeColor = Color.Black;
            object ds = sqlquery.fillcombobox("select branch_id, branch_name from branch where is_deleted = 0;");
            cmb.DataSource = ds;
            cmb.DisplayMember = "branch_name";
            cmb.ValueMember = "branch_id";
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text =  DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddSupplier asup = new AddSupplier();
            asup.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEmployee ae = new AddEmployee();
            ae.ShowDialog();
        }

        private void btnNewPO_Click(object sender, EventArgs e)
        {
            AddPurchaseOrder po = new AddPurchaseOrder();
            po.ShowDialog();
        }

        private void branchCMS_Opening(object sender, CancelEventArgs e)
        {
            var cms = sender as ContextMenuStrip;
            var mousepos = Control.MousePosition;
            if (cms != null)
            {
                var rel_mousePos = cms.PointToClient(mousepos);
                if (cms.ClientRectangle.Contains(rel_mousePos))
                {
                    var dgv_rel_mousePos = dataGridView7.PointToClient(mousepos);
                    var hti = dataGridView7.HitTest(dgv_rel_mousePos.X, dgv_rel_mousePos.Y);
                    if (hti.RowIndex == -1)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void suppCMS_Opening(object sender, CancelEventArgs e)
        {
            var cms = sender as ContextMenuStrip;
            var mousepos = Control.MousePosition;
            if (cms != null)
            {
                var rel_mousePos = cms.PointToClient(mousepos);
                if (cms.ClientRectangle.Contains(rel_mousePos))
                {
                    var dgv_rel_mousePos = dataGridView8.PointToClient(mousepos);
                    var hti = dataGridView8.HitTest(dgv_rel_mousePos.X, dgv_rel_mousePos.Y);
                    if (hti.RowIndex == -1)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void purchaseCMS_Opening(object sender, CancelEventArgs e)
        {
            var cms = sender as ContextMenuStrip;
            var mousepos = Control.MousePosition;
            if (cms != null)
            {
                var rel_mousePos = cms.PointToClient(mousepos);
                if (cms.ClientRectangle.Contains(rel_mousePos))
                {
                    var dgv_rel_mousePos = tblPurchaseOrder.PointToClient(mousepos);
                    var hti = tblPurchaseOrder.HitTest(dgv_rel_mousePos.X, dgv_rel_mousePos.Y);
                    if (hti.RowIndex == -1)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void tblExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tblExpenses.CurrentRow.Selected = true;
        }

        private void tabExpenseCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabExpenseCtrl.SelectedIndex == 1)
            {
                loadCategory();
            }
            else
            {
                loadExpenses();
            }
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            AddExpense exp = new AddExpense();
            exp.ShowDialog();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView table = (DataGridView)sender;
            if (table.Name == "tblExpenses")
            {
                table.ClearSelection();
            } 
                
            else if (table.Name == "tblPurchaseOrder")
            {
                table.ClearSelection();
            }
            else if (table.Name == "tblCategory")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView5")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView3")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView4")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView2")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView1")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView6")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView8")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView10")
            {
                table.ClearSelection();
            }
            else if (table.Name == "dataGridView7")
            {
                table.ClearSelection();
            }
        }

        private void tblPurchaseOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            foreach (DataGridViewRow row in tblPurchaseOrder.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(42, 222, 114);

                if (Convert.ToString(row.Cells["STATUS"].Value) == "Paid")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(42, 222, 114);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 41, 41);
                }
            }
            //if(tblPurchaseOrder.Rows(e.RowIndex).Cells("LevelID").Value.ToString() = "6")
            //    e.CellStyle.BackColor = Color.DimGray
            //for (int iCount = 0; iCount < tblPurchaseOrder.Rows.Count - 1; iCount++)
            //{
            //    if (Convert.ToString(tblPurchaseOrder.Rows[iCount].Cells[5].Value) == "Paid")
            //        tblPurchaseOrder.Rows[iCount].DefaultCellStyle.BackColor = Color.FromArgb(42, 222, 114);
            //    else
            //        tblPurchaseOrder.Rows[iCount].DefaultCellStyle.BackColor = Color.FromArgb(255, 41, 41);

            //}
        }
    }
}
