namespace VendorCrudWinForms
{
    partial class Form1
    {
        private System.Windows.Forms.DataGridView dgvVendors;
        private System.Windows.Forms.TextBox txtVendorID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtBusesManaged;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblVendorID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblBusesManaged;
        private System.Windows.Forms.Label lblStatus;

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            dgvVendors = new DataGridView();
            txtVendorID = new TextBox();
            txtName = new TextBox();
            txtEmail = new TextBox();
            txtBusesManaged = new TextBox();
            cmbStatus = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnRefresh = new Button();
            lblVendorID = new Label();
            lblName = new Label();
            lblEmail = new Label();
            lblBusesManaged = new Label();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvVendors).BeginInit();
            SuspendLayout();
            // 
            // dgvVendors
            // 
            dgvVendors.ColumnHeadersHeight = 29;
            dgvVendors.Location = new Point(20, 200);
            dgvVendors.Name = "dgvVendors";
            dgvVendors.RowHeadersWidth = 51;
            dgvVendors.Size = new Size(750, 220);
            dgvVendors.TabIndex = 0;
            dgvVendors.SelectionChanged += dgvVendors_SelectionChanged;
            // 
            // txtVendorID
            // 
            txtVendorID.Location = new Point(120, 20);
            txtVendorID.Name = "txtVendorID";
            txtVendorID.ReadOnly = true;
            txtVendorID.Size = new Size(200, 37);
            txtVendorID.TabIndex = 1;
            // 
            // txtName
            // 
            txtName.Location = new Point(120, 50);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 37);
            txtName.TabIndex = 2;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(120, 80);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 37);
            txtEmail.TabIndex = 3;
            // 
            // txtBusesManaged
            // 
            txtBusesManaged.Location = new Point(120, 110);
            txtBusesManaged.Name = "txtBusesManaged";
            txtBusesManaged.Size = new Size(200, 37);
            txtBusesManaged.TabIndex = 4;
            // 
            // cmbStatus
            // 
            cmbStatus.Location = new Point(120, 140);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(200, 38);
            cmbStatus.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(350, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 30);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(350, 60);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(90, 30);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(350, 100);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 30);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(350, 140);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(90, 30);
            btnClear.TabIndex = 9;
            btnClear.Text = "Clear";
            btnClear.Click += btnClear_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(460, 20);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 30);
            btnRefresh.TabIndex = 10;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblVendorID
            // 
            lblVendorID.AutoSize = true;
            lblVendorID.Location = new Point(20, 23);
            lblVendorID.Name = "lblVendorID";
            lblVendorID.Size = new Size(120, 31);
            lblVendorID.TabIndex = 11;
            lblVendorID.Text = "Vendor ID:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 53);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 31);
            lblName.TabIndex = 12;
            lblName.Text = "Name:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(20, 83);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(75, 31);
            lblEmail.TabIndex = 13;
            lblEmail.Text = "Email:";
            // 
            // lblBusesManaged
            // 
            lblBusesManaged.AutoSize = true;
            lblBusesManaged.Location = new Point(20, 113);
            lblBusesManaged.Name = "lblBusesManaged";
            lblBusesManaged.Size = new Size(181, 31);
            lblBusesManaged.TabIndex = 14;
            lblBusesManaged.Text = "Buses Managed:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 143);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(81, 31);
            lblStatus.TabIndex = 15;
            lblStatus.Text = "Status:";
            // 
            // Form1
            // 
            ClientSize = new Size(1195, 715);
            Controls.Add(lblStatus);
            Controls.Add(lblBusesManaged);
            Controls.Add(lblEmail);
            Controls.Add(lblName);
            Controls.Add(lblVendorID);
            Controls.Add(btnRefresh);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(cmbStatus);
            Controls.Add(txtBusesManaged);
            Controls.Add(txtEmail);
            Controls.Add(txtName);
            Controls.Add(txtVendorID);
            Controls.Add(dgvVendors);
            Name = "Form1";
            Text = "Vendors CRUD";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvVendors).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}
