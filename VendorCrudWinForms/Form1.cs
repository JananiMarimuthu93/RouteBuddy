using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VendorCrudWinForms
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;

        public Form1(IConfiguration config)
        {
            InitializeComponent();
            _connectionString = config.GetConnectionString("DefaultConnection");

            // setup controls
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvVendors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVendors.MultiSelect = false;
            dgvVendors.ReadOnly = true;
            dgvVendors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        private void Form1_Load(object sender, EventArgs e)
        {
            EnsureVendorsTable();
            LoadVendors();
        }

        private void EnsureVendorsTable()
        {
            using var conn = CreateConnection();
            using var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Vendors' AND xtype='U')
                BEGIN
                    CREATE TABLE Vendors (
                        VendorID INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(100) NOT NULL,
                        Email NVARCHAR(100) UNIQUE NOT NULL,
                        BusesManaged INT DEFAULT 0,
                        Status NVARCHAR(20) CHECK (Status IN ('Active', 'Inactive')) NOT NULL
                    )
                END", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        private void LoadVendors()
        {
            using var conn = CreateConnection();
            using var cmd = new SqlCommand("SELECT VendorID, Name, Email, BusesManaged, Status FROM Vendors ORDER BY VendorID", conn);
            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            dgvVendors.DataSource = dt;
            if (dgvVendors.Columns["VendorID"] != null)
                dgvVendors.Columns["VendorID"].ReadOnly = true;
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
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Name is required."); return false; }
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) { MessageBox.Show("Email is required."); return false; }
            try { var _ = new MailAddress(txtEmail.Text.Trim()); } catch { MessageBox.Show("Invalid email."); return false; }
            if (!int.TryParse(txtBusesManaged.Text.Trim(), out busesManaged) || busesManaged < 0) { MessageBox.Show("Buses Managed must be non-negative."); return false; }
            if (cmbStatus.SelectedItem == null) { MessageBox.Show("Select a status."); return false; }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int busesManaged)) return;
            using var conn = CreateConnection();
            using var cmd = new SqlCommand(@"INSERT INTO Vendors (Name, Email, BusesManaged, Status) VALUES (@Name, @Email, @BusesManaged, @Status)", conn);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@BusesManaged", busesManaged);
            cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
            try { conn.Open(); cmd.ExecuteNonQuery(); MessageBox.Show("Vendor added."); LoadVendors(); }
            catch (SqlException ex) { if (ex.Number == 2627 || ex.Number == 2601) MessageBox.Show("Email already exists."); else MessageBox.Show(ex.Message); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVendorID.Text)) { MessageBox.Show("Select a vendor to update."); return; }
            if (!ValidateInputs(out int busesManaged)) return;
            using var conn = CreateConnection();
            using var cmd = new SqlCommand(@"UPDATE Vendors SET Name=@Name, Email=@Email, BusesManaged=@BusesManaged, Status=@Status WHERE VendorID=@VendorID", conn);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@BusesManaged", busesManaged);
            cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@VendorID", int.Parse(txtVendorID.Text));
            try { conn.Open(); cmd.ExecuteNonQuery(); MessageBox.Show("Vendor updated."); LoadVendors(); }
            catch (SqlException ex) { if (ex.Number == 2627 || ex.Number == 2601) MessageBox.Show("Email already exists."); else MessageBox.Show(ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVendorID.Text)) { MessageBox.Show("Select a vendor to delete."); return; }
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            using var conn = CreateConnection();
            using var cmd = new SqlCommand("DELETE FROM Vendors WHERE VendorID=@VendorID", conn);
            cmd.Parameters.AddWithValue("@VendorID", int.Parse(txtVendorID.Text));
            conn.Open(); cmd.ExecuteNonQuery(); MessageBox.Show("Vendor deleted."); LoadVendors();
        }

        private void dgvVendors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVendors.SelectedRows.Count == 0) return;
            var row = dgvVendors.SelectedRows[0].DataBoundItem as DataRowView;
            if (row == null) return;
            txtVendorID.Text = row["VendorID"].ToString();
            txtName.Text = row["Name"].ToString();
            txtEmail.Text = row["Email"].ToString();
            txtBusesManaged.Text = row["BusesManaged"].ToString();
            cmbStatus.SelectedItem = row["Status"].ToString();
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();
        private void btnRefresh_Click(object sender, EventArgs e) => LoadVendors();

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
