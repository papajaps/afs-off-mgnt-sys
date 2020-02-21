using System;
using System.Windows.Forms;

namespace AFSOfficeManagementSystem
{
    public partial class AddExpense : Form
    {
        SQLClass sqlquery = new SQLClass();
        public AddExpense()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddExpense_Load(object sender, EventArgs e)
        {
            object ds = sqlquery.fillcombobox("select branch_id, branch_name from branch where is_deleted = 0;");
            cmbBranch.DataSource = ds;
            cmbBranch.SelectedIndex = 0;
            cmbBranch.DisplayMember = "branch_name";
            cmbBranch.ValueMember = "branch_id";

            ds = sqlquery.fillcombobox("select * from ecategory");
            cmbCategory.DataSource = ds;
            cmbCategory.SelectedIndex = 0;
            cmbCategory.DisplayMember = "ctg_name";
            cmbCategory.ValueMember = "ctg_id";

            cmbStatus.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TextBox temp;
            if (txtDescription.Text == "" || txtAmount.Text == "")
            {
                lblError.Visible = true;
                temp = txtDescription.Text == "" ? txtDescription : txtAmount;
                temp.Focus();
            }
            else
            {
                lblError.Visible = false;
                string qry = string.Format(@"INSERT INTO expense(
                                                ctg_id,
                                                expense_desc,
                                                expense_amnt,
                                                expense_stat,
                                                expense_date_paid,
                                                branch_id
                                            )
                                            VALUES('{0}',
                                                   '{1}',
                                                   '{2}',
                                                   '{3}',
                                                   '{4}',
                                                   '{5}'
                                            );", cmbCategory.SelectedValue,
                                                txtDescription.Text,
                                                Double.Parse(txtAmount.Text),
                                                cmbStatus.SelectedItem,
                                                dateExpDate.Text,
                                                cmbBranch.SelectedValue);
                sqlquery.ExecuteQueries(qry);
                this.Close();
                MainForm form = (MainForm)Application.OpenForms["MainForm"];
                form.loadExpenses();

            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateExpDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
