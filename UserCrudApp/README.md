# Console CRUD Application (ADO.NET)

## 📌 Overview
This is a simple **Console-based CRUD application** built using **C#** and **ADO.NET** to manage a `Users` table in SQL Server.  
It demonstrates database connectivity, parameterized queries, and basic Create, Read, Update, and Delete operations.

---

## 🚀 Features
- **Add New User** — Insert records into the database
- **View All Users** — Display all records in a tabular format
- **View User by ID** — Retrieve details for a specific user
- **Update User** — Modify existing user details
- **Delete User** — Remove a user from the database

---

## 🛠 Technologies Used
- **C# (.NET)**
- **ADO.NET**
- **SQL Server**
- **Microsoft.Data.SqlClient**

---

## 🗄 Database Table Structure
```sql
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(15),
    Role NVARCHAR(50),
    UpdatedAt DATETIME
);
