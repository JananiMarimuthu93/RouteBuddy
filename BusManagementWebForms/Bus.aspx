<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bus.aspx.cs" Inherits="BusManagementWebForms.Bus" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Bus Management System</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
<form id="form1" runat="server" class="container py-4">

    <div class="card shadow-sm">
        <div class="card-header bg-primary text-white text-center">
            <h3>🚌 Bus Management System</h3>
        </div>
        <div class="card-body">
            <div class="row g-3">

                <div class="col-md-4">
                    <label for="txtBusID" class="form-label fw-bold">Bus ID</label>
                    <asp:TextBox ID="txtBusID" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="col-md-4">
                    <label for="txtBusName" class="form-label fw-bold">Bus Name</label>
                    <asp:TextBox ID="txtBusName" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label for="ddlType" class="form-label fw-bold">Type</label>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select">
                        <asp:ListItem Text="AC Sleeper" />
                        <asp:ListItem Text="Non-AC Seater" />
                        <asp:ListItem Text="AC Seater" />
                        <asp:ListItem Text="Semi-Sleeper" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label for="txtRegNo" class="form-label fw-bold">Registration No</label>
                    <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label for="ddlStatus" class="form-label fw-bold">Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Active" />
                        <asp:ListItem Text="Inactive" />
                    </asp:DropDownList>
                </div>
            </div>

            <hr />

            <!-- Buttons -->
            <div class="d-flex gap-2">
                <asp:Button ID="btnAdd" runat="server" Text="➕ Add" CssClass="btn btn-success" OnClick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="✏️ Update" CssClass="btn btn-warning" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="🗑 Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                <asp:Button ID="btnClear" runat="server" Text="🧹 Clear" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
            </div>
        </div>
    </div>

    <!-- Table -->
    <div class="card shadow-sm mt-4">
        <div class="card-header bg-info text-white fw-bold">📋 Bus List</div>
        <div class="card-body p-0">
            <asp:GridView ID="gvBuses" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-hover mb-0"
                OnSelectedIndexChanged="gvBuses_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="BusID" HeaderText="Bus ID" />
                    <asp:BoundField DataField="BusName" HeaderText="Bus Name" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="RegistrationNo" HeaderText="Registration No" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Select" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</form>

<!-- Bootstrap JS -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
