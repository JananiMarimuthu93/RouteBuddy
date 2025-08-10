# 🚌 Bus Management Console Application (ADO.NET)

## 📌 Overview
This is a **Console-based CRUD application** built using **C#** and **ADO.NET** to manage a `Buses` table in **SQL Server**.  
It allows adding, viewing, updating, and deleting bus details directly from the console.

---

## 🚀 Features
- **Add New Bus** — Insert records into the database
- **View All Buses** — Display all buses in a tabular format
- **View Bus by ID** — Retrieve details of a specific bus
- **Update Bus Details** — Modify bus details (with option to keep old values)
- **Delete Bus** — Remove a bus from the database

---

## 🛠 Technologies Used
- **C# (.NET 6/7)**
- **ADO.NET**
- **SQL Server**
- **Microsoft.Data.SqlClient**

---

## 🗄 Database Table Structure

```sql
CREATE TABLE Buses (
    BusID INT PRIMARY KEY IDENTITY(1,1),
    BusName NVARCHAR(100) NOT NULL,
    Type NVARCHAR(50),
    RegistrationNo NVARCHAR(50) UNIQUE NOT NULL,
    Status NVARCHAR(20) CHECK (Status IN ('Active', 'Inactive')) NOT NULL
);
