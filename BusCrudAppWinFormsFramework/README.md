# 🚌 Bus Management System — Windows Forms (.NET Framework)

## 📌 Overview
This is a **Windows Forms application** built using **C# (.NET Framework)** and **ADO.NET** to perform **CRUD** (Create, Read, Update, Delete) operations on a `Buses` table in **SQL Server**.  

It provides a simple **graphical interface** to manage buses — including adding, updating, deleting, and viewing all bus details.

---

## 🚀 Features
- **Add Bus** — Insert new buses into the database  
- **Update Bus** — Edit details of existing buses  
- **Delete Bus** — Remove buses from the database  
- **View All Buses** — Display all buses in a DataGridView  
- **Search / Select Bus** — Click on a bus in the table to populate fields for editing  
- **Refresh** — Reload the latest bus data  
- **Clear Fields** — Reset all input fields for fresh entry  

---

## 🛠 Technologies Used
- **C# (.NET Framework)**
- **Windows Forms**
- **ADO.NET** (`SqlConnection`, `SqlCommand`, `SqlDataAdapter`)
- **SQL Server**

---

## 🗄 Database Table Structure
```sql
CREATE TABLE Buses (
    BusID INT PRIMARY KEY IDENTITY(1,1),
    BusName NVARCHAR(100) NOT NULL,
    Type NVARCHAR(50),
    RegistrationNo NVARCHAR(50) UNIQUE NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Active', 'Inactive'))
);
