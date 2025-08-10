====================================================
BUS MANAGEMENT SYSTEM - ASP.NET Web Forms Application
====================================================

📌 ABOUT THE PROJECT
--------------------
This is an ASP.NET Web Forms application for managing buses in a transport system.
It provides CRUD operations (Create, Read, Update, Delete) with a modern, 
Bootstrap-powered UI for better user experience.

The application connects to a SQL Server database to store bus details.

🛠 PROJECT TEMPLATE
-------------------
Created using the **"Empty" ASP.NET Web Application (.NET Framework)** template in Visual Studio,
with Web Forms added manually.

📋 FEATURES
-----------
1. Add new bus records.
2. View all buses in a styled table.
3. Update existing bus details.
4. Delete buses.
5. Clear form fields easily.
6. Responsive design with Bootstrap.

🗄 DATABASE SETUP
-----------------
Run the following SQL script in SQL Server Management Studio (SSMS):

CREATE TABLE Buses (
    BusID INT PRIMARY KEY IDENTITY(1,1),
    BusName NVARCHAR(100) NOT NULL,
    Type NVARCHAR(50),
    RegistrationNo NVARCHAR(50) UNIQUE NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Active', 'Inactive'))
);

⚙ CONFIGURATION
---------------
1. Open Web.config and update the connection string according to your SQL Server:

<connectionStrings>
    <add name="DefaultConnection"
         connectionString="server=YOUR_SERVER_NAME;database=RouteBuddy;integrated security=true;trustservercertificate=true"
         providerName="System.Data.SqlClient"/>
</connectionStrings>

Replace `YOUR_SERVER_NAME` with your actual server name (e.g., DESKTOP\SQLEXPRESS).

🚀 RUNNING THE APPLICATION
--------------------------
1. Open the project in Visual Studio.
2. Build the project (Ctrl + Shift + B).
3. Press F5 to run using IIS Express.
4. The default page is `Bus.aspx`.

💻 TECHNOLOGIES USED
--------------------
- ASP.NET Web Forms (C#)
- SQL Server
- Bootstrap 5
- ADO.NET

📝 CRUD OPERATIONS
------------------
- **Add**: Enter details and click the ➕ Add button.
- **Update**: Select a bus from the table, edit the details, and click ✏️ Update.
- **Delete**: Select a bus and click 🗑 Delete.
- **Clear**: Click 🧹 Clear to reset the form.

📦 FILE STRUCTURE
-----------------
- Bus.aspx        → UI for managing buses.
- Bus.aspx.cs     → Code-behind for CRUD logic.
- Web.config      → Application configuration (including DB connection).
- App_Data        → Database-related files (if any).
- Content         → CSS/JS files (Bootstrap).
