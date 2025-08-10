namespace VendorCrudWinFormsFramework
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblVendorID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblBusesManaged;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.TextBox txtVendorID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtBusesManaged;

        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.DataGridView dgvVendors;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblVendorID = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblBusesManaged = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();

            this.txtVendorID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtBusesManaged = new System.Windows.Forms.TextBox();

            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.dgvVendors = new System.Windows.Forms.DataGridView();

            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvVendors)).BeginInit();
            this.SuspendLayout();

            // 
            // Labels
            // 
            this.lblVendorID.AutoSize = true;
            this.lblVendorID.Location = new System.Drawing.Point(20, 20);
            this.lblVendorID.Text = "Vendor ID:";

            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 60);
            this.lblName.Text = "Name:";

            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 100);
            this.lblEmail.Text = "Email:";

            this.lblBusesManaged.AutoSize = true;
            this.lblBusesManaged.Location = new System.Drawing.Point(20, 140);
            this.lblBusesManaged.Text = "Buses Managed:";

            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 180);
            this.lblStatus.Text = "Status:";

            // 
            // TextBoxes
            // 
            this.txtVendorID.Location = new System.Drawing.Point(140, 17);
            this.txtVendorID.ReadOnly = true;
            this.txtVendorID.Width = 200;

            this.txtName.Location = new System.Drawing.Point(140, 57);
            this.txtName.Width = 200;

            this.txtEmail.Location = new System.Drawing.Point(140, 97);
            this.txtEmail.Width = 200;

            this.txtBusesManaged.Location = new System.Drawing.Point(140, 137);
            this.txtBusesManaged.Width = 200;

            // 
            // ComboBox
            // 
            this.cmbStatus.Location = new System.Drawing.Point(140, 177);
            this.cmbStatus.Width = 200;

            // 
            // DataGridView
            // 
            this.dgvVendors.Location = new System.Drawing.Point(370, 20);
            this.dgvVendors.Size = new System.Drawing.Size(400, 250);
            this.dgvVendors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendors.MultiSelect = false;
            this.dgvVendors.ReadOnly = true;
            this.dgvVendors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVendors.SelectionChanged += new System.EventHandler(this.dgvVendors_SelectionChanged);

            // 
            // Buttons
            // 
            this.btnAdd.Text = "Add";
            this.btnAdd.Location = new System.Drawing.Point(20, 230);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text = "Update";
            this.btnUpdate.Location = new System.Drawing.Point(100, 230);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new System.Drawing.Point(180, 230);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClear.Text = "Clear";
            this.btnClear.Location = new System.Drawing.Point(260, 230);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new System.Drawing.Point(340, 230);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 300);
            this.Controls.Add(this.lblVendorID);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblBusesManaged);
            this.Controls.Add(this.lblStatus);

            this.Controls.Add(this.txtVendorID);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtBusesManaged);

            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.dgvVendors);

            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRefresh);

            this.Name = "Form1";
            this.Text = "Vendor CRUD - .NET Framework";
            this.Load += new System.EventHandler(this.Form1_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvVendors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
