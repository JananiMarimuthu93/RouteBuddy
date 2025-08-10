using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VendorCrudWinFormsFramework
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;
        public Form1()
        {
            InitializeComponent();
            // Get connection string from App.config
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // ComboBox setup
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // DataGridView setup
            dgvVendors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVendors.MultiSelect = false;
            dgvVendors.ReadOnly = true;
            dgvVendors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadVendors();
        }

        private void LoadVendors()
        {
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand("SELECT VendorID, Name, Email, BusesManaged, Status FROM Vendors ORDER BY VendorID", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                dgvVendors.DataSource = dt;

                if (dgvVendors.Columns["VendorID"] != null)
                {
                    dgvVendors.Columns["VendorID"].ReadOnly = true;
                }
            }
            ClearForm();
        }

        private void ClearForm()
        {
            txtVendorID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtBusesManaged.Text = "0";
            cmbStatus.SelectedIndex = -1;
            dgvVendors.ClearSelection();
        }

        private bool ValidateInputs(out int busesManaged)
        {
            busesManaged = 0;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.");
                txtEmail.Focus();
                return false;
            }

            try
            {
                var addr = new MailAddress(txtEmail.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Invalid email format.");
                txtEmail.Focus();
                return false;
            }

            if (!int.TryParse(txtBusesManaged.Text.Trim(), out busesManaged) || busesManaged < 0)
            {
                MessageBox.Show("Buses Managed must be a non-negative number.");
                txtBusesManaged.Focus();
                return false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select status.");
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int busesManaged)) return;

            string sql = @"INSERT INTO Vendors (Name, Email, BusesManaged, Status) 
                           VALUES (@Name, @Email, @BusesManaged, @Status)";

            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@BusesManaged", busesManaged);
                cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vendor added.");
                    LoadVendors();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                        MessageBox.Show("Duplicate email.");
                    else
                        MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvVendors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVendors.SelectedRows.Count == 0) return;
            var row = dgvVendors.SelectedRows[0];
            txtVendorID.Text = row.Cells["VendorID"].Value.ToString();
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtBusesManaged.Text = row.Cells["BusesManaged"].Value.ToString();
            cmbStatus.SelectedItem = row.Cells["Status"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVendorID.Text))
            {
                MessageBox.Show("Select a vendor to update.");
                return;
            }

            if (!ValidateInputs(out int busesManaged)) return;

            string sql = @"UPDATE Vendors 
                           SET Name=@Name, Email=@Email, BusesManaged=@BusesManaged, Status=@Status 
                           WHERE VendorID=@VendorID";

            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@BusesManaged", busesManaged);
                cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@VendorID", int.Parse(txtVendorID.Text));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vendor updated.");
                    LoadVendors();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                        MessageBox.Show("Duplicate email.");
                    else
                        MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVendorID.Text))
            {
                MessageBox.Show("Select a vendor to delete.");
                return;
            }

            if (MessageBox.Show("Confirm delete?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string sql = "DELETE FROM Vendors WHERE VendorID=@VendorID";

            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@VendorID", int.Parse(txtVendorID.Text));

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Vendor deleted.");
                LoadVendors();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadVendors();
        }
    }
}


