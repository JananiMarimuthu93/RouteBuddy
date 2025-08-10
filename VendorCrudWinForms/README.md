# Vendor CRUD Windows Forms Application (.NET Core)

## 📌 Overview
This is a Windows Forms application built using .NET Core that allows you to perform CRUD (Create, Read, Update, Delete) operations on the `Vendors` table in a SQL Server database.

## 🗄 Database Table Structure
Run the following SQL script in your SQL Server before running the application:

CREATE TABLE Vendors (
    VendorID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    BusesManaged INT DEFAULT 0,
    Status NVARCHAR(20) CHECK (Status IN ('Active', 'Inactive')) NOT NULL
);

## ⚙️ Features
- View all vendors in a DataGridView
- Add new vendor
- Update existing vendor
- Delete vendor
- Clear form fields
- Refresh vendor list

## 🔌 Configuration
Edit your connection string in **appsettings.json**:
