namespace AFSOfficeManagementSystem
{
    partial class AddEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.txtMName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbProvince = new System.Windows.Forms.ComboBox();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.cmbBrgy = new System.Windows.Forms.ComboBox();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhilHealth = new System.Windows.Forms.TextBox();
            this.txtSSS = new System.Windows.Forms.TextBox();
            this.txtTIN = new System.Windows.Forms.TextBox();
            this.txtPagibig = new System.Windows.Forms.TextBox();
            this.cmbPosition = new System.Windows.Forms.ComboBox();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.MaskedTextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(9, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(239, 20);
            this.lblHeader.TabIndex = 6;
            this.lblHeader.Text = "EMPLOYEE INFORMATION";
            // 
            // txtFName
            // 
            this.txtFName.ForeColor = System.Drawing.Color.Gray;
            this.txtFName.Location = new System.Drawing.Point(14, 73);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(195, 21);
            this.txtFName.TabIndex = 7;
            this.txtFName.Text = "FIRST NAME";
            // 
            // txtMName
            // 
            this.txtMName.ForeColor = System.Drawing.Color.Gray;
            this.txtMName.Location = new System.Drawing.Point(217, 73);
            this.txtMName.Name = "txtMName";
            this.txtMName.Size = new System.Drawing.Size(195, 21);
            this.txtMName.TabIndex = 8;
            this.txtMName.Text = "MIDDLE NAME";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "NAME";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "ADDRESS";
            // 
            // cmbProvince
            // 
            this.cmbProvince.ForeColor = System.Drawing.Color.Gray;
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Location = new System.Drawing.Point(14, 160);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(195, 23);
            this.cmbProvince.TabIndex = 10;
            this.cmbProvince.Text = "PROVINCE";
            this.cmbProvince.DropDown += new System.EventHandler(this.cmbProvince_DropDown);
            this.cmbProvince.SelectedIndexChanged += new System.EventHandler(this.cmbProvince_SelectedIndexChanged);
            // 
            // cmbCity
            // 
            this.cmbCity.Enabled = false;
            this.cmbCity.ForeColor = System.Drawing.Color.Gray;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(217, 160);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(195, 23);
            this.cmbCity.TabIndex = 11;
            this.cmbCity.Text = "CITY/MUNICIPALITY";
            this.cmbCity.DropDown += new System.EventHandler(this.cmbCity_DropDown);
            this.cmbCity.SelectedIndexChanged += new System.EventHandler(this.cmbCity_SelectedIndexChanged);
            // 
            // cmbBrgy
            // 
            this.cmbBrgy.Enabled = false;
            this.cmbBrgy.ForeColor = System.Drawing.Color.Gray;
            this.cmbBrgy.FormattingEnabled = true;
            this.cmbBrgy.Location = new System.Drawing.Point(14, 192);
            this.cmbBrgy.Name = "cmbBrgy";
            this.cmbBrgy.Size = new System.Drawing.Size(195, 23);
            this.cmbBrgy.TabIndex = 12;
            this.cmbBrgy.Text = "BARANGAY";
            this.cmbBrgy.DropDown += new System.EventHandler(this.cmbBrgy_DropDown);
            // 
            // txtStreet
            // 
            this.txtStreet.ForeColor = System.Drawing.Color.Gray;
            this.txtStreet.Location = new System.Drawing.Point(217, 193);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(195, 21);
            this.txtStreet.TabIndex = 13;
            this.txtStreet.Text = "HOUSE NO./STREET";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "CONTACT";
            // 
            // txtEmail
            // 
            this.txtEmail.ForeColor = System.Drawing.Color.Gray;
            this.txtEmail.Location = new System.Drawing.Point(14, 252);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(195, 21);
            this.txtEmail.TabIndex = 14;
            this.txtEmail.Text = "EMAIL";
            // 
            // txtPhilHealth
            // 
            this.txtPhilHealth.ForeColor = System.Drawing.Color.Gray;
            this.txtPhilHealth.Location = new System.Drawing.Point(217, 292);
            this.txtPhilHealth.Name = "txtPhilHealth";
            this.txtPhilHealth.Size = new System.Drawing.Size(195, 21);
            this.txtPhilHealth.TabIndex = 17;
            this.txtPhilHealth.Text = "PHILHEALTH";
            // 
            // txtSSS
            // 
            this.txtSSS.ForeColor = System.Drawing.Color.Gray;
            this.txtSSS.Location = new System.Drawing.Point(14, 292);
            this.txtSSS.Name = "txtSSS";
            this.txtSSS.Size = new System.Drawing.Size(195, 21);
            this.txtSSS.TabIndex = 16;
            this.txtSSS.Text = "SSS";
            // 
            // txtTIN
            // 
            this.txtTIN.ForeColor = System.Drawing.Color.Gray;
            this.txtTIN.Location = new System.Drawing.Point(217, 322);
            this.txtTIN.Name = "txtTIN";
            this.txtTIN.Size = new System.Drawing.Size(195, 21);
            this.txtTIN.TabIndex = 19;
            this.txtTIN.Text = "TIN";
            // 
            // txtPagibig
            // 
            this.txtPagibig.ForeColor = System.Drawing.Color.Gray;
            this.txtPagibig.Location = new System.Drawing.Point(14, 322);
            this.txtPagibig.Name = "txtPagibig";
            this.txtPagibig.Size = new System.Drawing.Size(195, 21);
            this.txtPagibig.TabIndex = 18;
            this.txtPagibig.Text = "PAG-IBIG";
            // 
            // cmbPosition
            // 
            this.cmbPosition.ForeColor = System.Drawing.Color.Gray;
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Location = new System.Drawing.Point(14, 365);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(195, 23);
            this.cmbPosition.TabIndex = 20;
            this.cmbPosition.Text = "POSITION";
            this.cmbPosition.DropDown += new System.EventHandler(this.cmbPosition_DropDown);
            this.cmbPosition.SelectedIndexChanged += new System.EventHandler(this.cmbPosition_SelectedIndexChanged);
            // 
            // txtSalary
            // 
            this.txtSalary.Enabled = false;
            this.txtSalary.ForeColor = System.Drawing.Color.Gray;
            this.txtSalary.Location = new System.Drawing.Point(217, 365);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(195, 21);
            this.txtSalary.TabIndex = 23;
            this.txtSalary.Text = "SALARY";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(325, 432);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 27);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(231, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtLName
            // 
            this.txtLName.ForeColor = System.Drawing.Color.Gray;
            this.txtLName.Location = new System.Drawing.Point(14, 103);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(195, 21);
            this.txtLName.TabIndex = 9;
            this.txtLName.Text = "LAST NAME";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(218, 252);
            this.txtPhone.Mask = "(999) 000-0000";
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(194, 21);
            this.txtPhone.TabIndex = 15;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(10, 403);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(200, 16);
            this.lblError.TabIndex = 28;
            this.lblError.Text = "Complete Filling up the Form.";
            this.lblError.Visible = false;
            // 
            // AddEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(427, 472);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtLName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.cmbPosition);
            this.Controls.Add(this.txtTIN);
            this.Controls.Add(this.txtPagibig);
            this.Controls.Add(this.txtPhilHealth);
            this.Controls.Add(this.txtSSS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtStreet);
            this.Controls.Add(this.cmbBrgy);
            this.Controls.Add(this.cmbCity);
            this.Controls.Add(this.cmbProvince);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMName);
            this.Controls.Add(this.txtFName);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Employee";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.TextBox txtMName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbProvince;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.ComboBox cmbBrgy;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhilHealth;
        private System.Windows.Forms.TextBox txtSSS;
        private System.Windows.Forms.TextBox txtTIN;
        private System.Windows.Forms.TextBox txtPagibig;
        private System.Windows.Forms.ComboBox cmbPosition;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.MaskedTextBox txtPhone;
        private System.Windows.Forms.Label lblError;
    }
}