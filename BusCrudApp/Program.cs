using Microsoft.Data.SqlClient;
using System;

namespace BusCrudApp
{
    internal class Program
    {
        static SqlConnection Connection;
        static SqlCommand Command;

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Bus Management System.");

            // Create the table if it doesn't exist
            CreateTable();

            string choice="";
            do
            {
                Console.WriteLine("\nChoose an Operation:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Add Bus Details");
                Console.WriteLine("2. Update Bus Details");
                Console.WriteLine("3. Delete Bus Details");
                Console.WriteLine("4. View All Buses");
                Console.WriteLine("5. View Bus by ID");

                if (!int.TryParse(Console.ReadLine(), out int pressedKey))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (pressedKey)
                {
                    case 0:
                        Console.WriteLine("Exiting the system. Thank you!");
                        return;
                    case 1:
                        InsertRecord();
                        break;
                    case 2:
                        UpdateRecord();
                        break;
                    case 3:
                        DeleteRecord();
                        break;
                    case 4:
                        ViewRecords();
                        break;
                    case 5:
                        FilterRecords();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.Write("\nDo you want to continue (yes/no): ");
                choice = Console.ReadLine();

            } while (!string.IsNullOrEmpty(choice) && choice.Trim().ToLower() == "yes");
        }

        private static SqlConnection CreateConnection()
        {
            string connectionString = "server=KDJ-LAPTOP\\SQLEXPRESS;database=RouteBuddy;integrated security=true;trustservercertificate=true";
            Connection = new SqlConnection(connectionString);
            Connection.Open();
            return Connection;
        }

        private static void CloseConnection()
        {
            if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
        }

        private static void CreateTable()
        {
            CreateConnection();

            string query = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Buses' AND xtype = 'U')
            BEGIN
                CREATE TABLE Buses (
                    BusID INT PRIMARY KEY IDENTITY(1,1),
                    BusName NVARCHAR(100) NOT NULL,
                    Type NVARCHAR(50),
                    RegistrationNo NVARCHAR(50) UNIQUE NOT NULL,
                    Status NVARCHAR(20) CHECK (Status IN ('Active', 'Inactive')) NOT NULL
                );
            END";

            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
            Console.WriteLine("Buses table checked/created successfully.");
            CloseConnection();
        }

        private static void InsertRecord()
        {
            CreateConnection();

            Console.Write("Enter Bus Name: ");
            string busName = Console.ReadLine();

            Console.Write("Enter Type: ");
            string type = Console.ReadLine();

            Console.Write("Enter Registration No: ");
            string registrationNo = Console.ReadLine();

            Console.Write("Enter Status (Active/Inactive): ");
            string status = Console.ReadLine().Trim();
            if (!(status.Equals("Active", StringComparison.OrdinalIgnoreCase) || status.Equals("Inactive", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Invalid status. Must be 'Active' or 'Inactive'.");
                CloseConnection();
                return;
            }

            string query = @"INSERT INTO Buses (BusName, Type, RegistrationNo, Status) 
                             VALUES (@BusName, @Type, @RegistrationNo, @Status)";

            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@BusName", busName);
            Command.Parameters.AddWithValue("@Type", type);
            Command.Parameters.AddWithValue("@RegistrationNo", registrationNo);
            Command.Parameters.AddWithValue("@Status", status);

            try
            {
                int rows = Command.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Bus added successfully." : "Failed to add bus.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            CloseConnection();
        }

        private static void ViewRecords()
        {
            CreateConnection();
            string query = "SELECT * FROM Buses";
            Command = new SqlCommand(query, Connection);
            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                Console.WriteLine("\nBus Details:");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["BusID"]} | Name: {reader["BusName"]} | Type: {reader["Type"]} | RegNo: {reader["RegistrationNo"]} | Status: {reader["Status"]}");
                }
            }
            else
            {
                Console.WriteLine("No buses found.");
            }
            reader.Close();
            CloseConnection();
        }

        private static void FilterRecords()
        {
            CreateConnection();
            Console.Write("Enter Bus ID: ");
            if (!int.TryParse(Console.ReadLine(), out int busID))
            {
                Console.WriteLine("Invalid ID.");
                CloseConnection();
                return;
            }

            string query = "SELECT * FROM Buses WHERE BusID = @BusID";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@BusID", busID);
            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                Console.WriteLine($"ID: {reader["BusID"]} | Name: {reader["BusName"]} | Type: {reader["Type"]} | RegNo: {reader["RegistrationNo"]} | Status: {reader["Status"]}");
            }
            else
            {
                Console.WriteLine("Bus not found.");
            }
            reader.Close();
            CloseConnection();
        }

        private static void DeleteRecord()
        {
            CreateConnection();
            Console.Write("Enter Bus ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int busID))
            {
                Console.WriteLine("Invalid ID.");
                CloseConnection();
                return;
            }

            string query = "DELETE FROM Buses WHERE BusID = @BusID";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@BusID", busID);
            int rows = Command.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Bus deleted successfully." : "Bus not found.");
            CloseConnection();
        }

        private static void UpdateRecord()
        {
            CreateConnection();
            Console.Write("Enter Bus ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int busID))
            {
                Console.WriteLine("Invalid ID.");
                CloseConnection();
                return;
            }

            // Fetch the existing record
            string selectQuery = "SELECT * FROM Buses WHERE BusID = @BusID";
            Command = new SqlCommand(selectQuery, Connection);
            Command.Parameters.AddWithValue("@BusID", busID);

            SqlDataReader reader = Command.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("Bus not found.");
                reader.Close();
                CloseConnection();
                return;
            }

            reader.Read();
            string oldBusName = reader["BusName"].ToString();
            string oldType = reader["Type"].ToString();
            string oldRegNo = reader["RegistrationNo"].ToString();
            string oldStatus = reader["Status"].ToString();
            reader.Close();

            Console.WriteLine("\n--- Current Bus Details ---");
            Console.WriteLine($"Name: {oldBusName}");
            Console.WriteLine($"Type: {oldType}");
            Console.WriteLine($"Registration No: {oldRegNo}");
            Console.WriteLine($"Status: {oldStatus}");

            // Ask for new values (press Enter to keep old value)
            Console.Write("\nEnter New Bus Name (Leave blank to keep old): ");
            string busName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(busName)) busName = oldBusName;

            Console.Write("Enter New Type (Leave blank to keep old): ");
            string type = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(type)) type = oldType;

            Console.Write("Enter New Registration No (Leave blank to keep old): ");
            string registrationNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(registrationNo)) registrationNo = oldRegNo;

            Console.Write("Enter New Status (Active/Inactive, Leave blank to keep old): ");
            string status = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(status)) status = oldStatus;
            if (!(status.Equals("Active", StringComparison.OrdinalIgnoreCase) || status.Equals("Inactive", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Invalid status. Keeping old status.");
                status = oldStatus;
            }

            // Update query
            string updateQuery = @"UPDATE Buses 
                                   SET BusName = @BusName, Type = @Type, RegistrationNo = @RegistrationNo, Status = @Status
                                   WHERE BusID = @BusID";

            Command = new SqlCommand(updateQuery, Connection);
            Command.Parameters.AddWithValue("@BusID", busID);
            Command.Parameters.AddWithValue("@BusName", busName);
            Command.Parameters.AddWithValue("@Type", type);
            Command.Parameters.AddWithValue("@RegistrationNo", registrationNo);
            Command.Parameters.AddWithValue("@Status", status);

            int rows = Command.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Bus updated successfully." : "Update failed.");

            CloseConnection();
        }
    }
}
