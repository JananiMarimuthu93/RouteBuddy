using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BusManagementWebForms
{
    public partial class Bus : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadBuses();
        }

        void LoadBuses()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Buses", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvBuses.DataSource = dt;
                gvBuses.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Buses (BusName, Type, RegistrationNo, Status) VALUES (@BusName, @Type, @RegNo, @Status)", con);
                cmd.Parameters.AddWithValue("@BusName", txtBusName.Text);
                cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue);
                cmd.Parameters.AddWithValue("@RegNo", txtRegNo.Text);
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadBuses();
            ClearForm();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Buses SET BusName=@BusName, Type=@Type, RegistrationNo=@RegNo, Status=@Status WHERE BusID=@BusID", con);
                cmd.Parameters.AddWithValue("@BusID", txtBusID.Text);
                cmd.Parameters.AddWithValue("@BusName", txtBusName.Text);
                cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue);
                cmd.Parameters.AddWithValue("@RegNo", txtRegNo.Text);
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadBuses();
            ClearForm();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Buses WHERE BusID=@BusID", con);
                cmd.Parameters.AddWithValue("@BusID", txtBusID.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadBuses();
            ClearForm();
        }

        protected void gvBuses_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvBuses.SelectedRow;
            txtBusID.Text = row.Cells[0].Text;
            txtBusName.Text = row.Cells[1].Text;
            ddlType.SelectedValue = row.Cells[2].Text;
            txtRegNo.Text = row.Cells[3].Text;
            ddlStatus.SelectedValue = row.Cells[4].Text;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        void ClearForm()
        {
            txtBusID.Text = "";
            txtBusName.Text = "";
            ddlType.SelectedIndex = 0;
            txtRegNo.Text = "";
            ddlStatus.SelectedIndex = 0;
        }
    }
}
