# Let's create the README.md file with the provided content.

readme_content = """# 🖥 Vendor CRUD Application — Windows Forms (.NET Framework)

## 📌 Overview
This is a **Windows Forms application** built using **C# (.NET Framework)** and **ADO.NET** to perform **CRUD** (Create, Read, Update, Delete) operations on a `Vendors` table in **SQL Server**.  

It provides a simple GUI where you can:
- Add new vendors  
- View all vendors in a DataGridView  
- Update vendor details  
- Delete vendors  
- Refresh and clear form fields  

---

## 🚀 Features
- **Add Vendor** — Insert new records into the `Vendors` table  
- **Update Vendor** — Edit existing vendor details  
- **Delete Vendor** — Remove a vendor from the database  
- **View Vendors** — Display all records in a DataGridView  
- **Selection Sync** — Selecting a row populates form fields for editing  
- **Validation** — Prevents invalid data entry (required fields, email format, etc.)  

---

## 🛠 Technologies Used
- **C# (.NET Framework)**  
- **Windows Forms**  
- **ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)**  
- **SQL Server**  

---

## 🗄 Database Table Structure
```sql
CREATE TABLE Vendors (
    VendorID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    BusesManaged INT DEFAULT 0,
    Status NVARCHAR(20) CHECK (Status IN ('Active', 'Inactive')) NOT NULL
);
---

## ⚙️ Setup Instructions
1️⃣ Database Setup
Open SQL Server Management Studio (SSMS).

Run the above CREATE TABLE script in your database.

(Optional) Insert sample data:
INSERT INTO Vendors (Name, Email, BusesManaged, Status)  
VALUES  
('Vendor One', 'vendor1@example.com', 5, 'Active'),  
('Vendor Two', 'vendor2@example.com', 3, 'Inactive');
2️⃣ Application Setup
Open the project in Visual Studio.

In App.config, set your SQL Server connection string:
<connectionStrings>
    <add name="DefaultConnection" 
         connectionString="server=YOUR_SERVER_NAME;database=YOUR_DATABASE;trusted_connection=true;TrustServerCertificate=True"/>
</connectionStrings>
Build and run the project.

🖱 Usage Guide
Add Vendor: Fill in Name, Email, Buses Managed, select Status → Click Add.
Update Vendor: Select a row from DataGridView → Modify fields → Click Update.
Delete Vendor: Select a row → Click Delete.
Clear: Clears all form inputs.
Refresh: Reloads data from the database.

📸 UI Layout
The form contains:

Labels & TextBoxes for Name, Email, Buses Managed
ComboBox for Status (Active / Inactive)
DataGridView for displaying vendor records
Buttons: Add, Update, Delete, Clear, Refresh

