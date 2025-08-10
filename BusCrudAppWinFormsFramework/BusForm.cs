using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BusCrudAppWinFormsFramework
{
    public partial class BusForm : Form
    {
        private readonly string connectionString = "Server=KDJ-LAPTOP\\SQLEXPRESS;Database=RouteBuddy;Trusted_Connection=True;TrustServerCertificate=True";
        public BusForm()
        {
            InitializeComponent();
        }
        private void BusForm_Load(object sender, EventArgs e)
        {
            LoadBuses();
        }
        // Load all buses into DataGridView
        private void LoadBuses()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Buses", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvBuses.DataSource = dt;
            }
        }

        // Add Bus
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBusName.Text.Trim() == "" || cmbType.SelectedIndex == -1 || txtRegistrationNo.Text.Trim() == "" || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Buses (BusName, Type, RegistrationNo, Status) VALUES (@BusName, @Type, @RegistrationNo, @Status)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BusName", txtBusName.Text.Trim());
                cmd.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@RegistrationNo", txtRegistrationNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());

                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bus added successfully!");
                    LoadBuses();
                    ClearFields();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Update Bus
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtBusID.Text == "")
            {
                MessageBox.Show("Please select a bus to update.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Buses SET BusName=@BusName, Type=@Type, RegistrationNo=@RegistrationNo, Status=@Status WHERE BusID=@BusID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BusID", txtBusID.Text);
                cmd.Parameters.AddWithValue("@BusName", txtBusName.Text.Trim());
                cmd.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@RegistrationNo", txtRegistrationNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());

                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bus updated successfully!");
                    LoadBuses();
                    ClearFields();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Delete Bus
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtBusID.Text == "")
            {
                MessageBox.Show("Please select a bus to delete.");
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this bus?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Buses WHERE BusID=@BusID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@BusID", txtBusID.Text);

                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bus deleted successfully!");
                        LoadBuses();
                        ClearFields();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        // Clear fields
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtBusID.Clear();
            txtBusName.Clear();
            cmbType.SelectedIndex = -1;
            txtRegistrationNo.Clear();
            cmbStatus.SelectedIndex = -1;
        }

        // Refresh table
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBuses();
        }

        // Populate fields when selecting row
        private void dgvBuses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBuses.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvBuses.SelectedRows[0];
                txtBusID.Text = row.Cells["BusID"].Value.ToString();
                txtBusName.Text = row.Cells["BusName"].Value.ToString();
                cmbType.SelectedItem = row.Cells["Type"].Value.ToString();
                txtRegistrationNo.Text = row.Cells["RegistrationNo"].Value.ToString();
                cmbStatus.SelectedItem = row.Cells["Status"].Value.ToString();
            }
        }
    }
}











