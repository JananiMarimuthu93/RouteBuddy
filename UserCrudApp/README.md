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
    Role NVARCHAR(20) CHECK (Role IN ('traveller', 'vendor', 'admin')) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

ALTER TABLE Users DROP CONSTRAINT CK__Users__Role__38996AB5;

ALTER TABLE Users
ADD CONSTRAINT CK_Users_Role
CHECK (LOWER(Role) IN ('traveller', 'vendor', 'admin'));