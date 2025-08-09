using Microsoft.Data.SqlClient;
using System.Data;
namespace UserCrudApp
{
    internal class Program
    {
        private static SqlConnection _connection;
        private static void EstablishConnection()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
            {
                String ConnectionString = "server=KDJ-LAPTOP\\SQLEXPRESS;database=KANINI;integrated security=true;trustservercertificate=true";
                _connection = new SqlConnection(ConnectionString);
                _connection.Open();
            }
        }

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("__________Users CRUD (ADO.NET)__________");
                Console.WriteLine("Enter the operation to be performed:");
                Console.WriteLine("1) Add user");
                Console.WriteLine("2) View all users");
                Console.WriteLine("3) View user by ID");
                Console.WriteLine("4) Update user information");
                Console.WriteLine("5) Delete user");
                Console.WriteLine("0) Exit");
                Console.Write("Choice: ");
                String choice= Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": CreateUser(); break;
                        case "2": ListUsers(); break;
                        case "3": ViewUser(); break;
                        case "4": UpdateUser(); break;
                        case "5": DeleteUser(); break;
                        case "0": _connection?.Close(); return;
                        default: Console.WriteLine("Invalid choice.Enter the correct choice"); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
        }

        private static void CreateUser()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Role: ");
            string role = Console.ReadLine();

            EstablishConnection();
            string query = "INSERT INTO Users (Name, Email, Password, Phone, Role, UpdatedAt) " +
                           "VALUES (@Name, @Email, @Password, @Phone, @Role, @UpdatedAt)";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "User added successfully." : "Failed to add user.");
            }
        }

        private static void ListUsers()
        {
            EstablishConnection();
            string query = "SELECT * FROM Users";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nID\tName\tEmail\tPhone\tRole\tUpdatedAt");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["UserID"]}\t{reader["Name"]}\t{reader["Email"]}\t{reader["Phone"]}\t{reader["Role"]}\t{reader["UpdatedAt"]}");
                    }
                }
            }
        }

        private static void ViewUser()
        {
            Console.Write("Enter User ID: ");
            int id = int.Parse(Console.ReadLine());

            EstablishConnection();
            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@UserID", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["Name"]}");
                        Console.WriteLine($"Email: {reader["Email"]}");
                        Console.WriteLine($"Phone: {reader["Phone"]}");
                        Console.WriteLine($"Role: {reader["Role"]}");
                        Console.WriteLine($"Updated At: {reader["UpdatedAt"]}");
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }
                }
            }
        }

        private static void UpdateUser()
        {
            Console.Write("Enter User ID to update: ");
            int id = int.Parse(Console.ReadLine());

            EstablishConnection();
            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@UserID", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string currentName = reader["Name"].ToString();
                        string currentEmail = reader["Email"].ToString();
                        string currentPhone = reader["Phone"].ToString();
                        string currentRole = reader["Role"].ToString();
                        reader.Close();

                        Console.WriteLine($"\nCurrent Name: {currentName}");
                        Console.Write("Enter new Name (leave blank to keep): ");
                        string name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name)) name = currentName;

                        Console.WriteLine($"Current Email: {currentEmail}");
                        Console.Write("Enter new Email (leave blank to keep): ");
                        string email = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(email)) email = currentEmail;

                        Console.WriteLine($"Current Phone: {currentPhone}");
                        Console.Write("Enter new Phone (leave blank to keep): ");
                        string phone = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(phone)) phone = currentPhone;

                        Console.WriteLine($"Current Role: {currentRole}");
                        Console.Write("Enter new Role (leave blank to keep): ");
                        string role = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(role)) role = currentRole;

                        // Update query
                        string updateQuery = "UPDATE Users SET Name = @Name, Email = @Email, Phone = @Phone, Role = @Role, UpdatedAt = @UpdatedAt WHERE UserID = @UserID";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, _connection))
                        {
                            updateCmd.Parameters.AddWithValue("@Name", name);
                            updateCmd.Parameters.AddWithValue("@Email", email);
                            updateCmd.Parameters.AddWithValue("@Phone", phone);
                            updateCmd.Parameters.AddWithValue("@Role", role);
                            updateCmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                            updateCmd.Parameters.AddWithValue("@UserID", id);

                            int rows = updateCmd.ExecuteNonQuery();
                            Console.WriteLine(rows > 0 ? "User updated successfully." : "Update failed.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }
                }
            }
        }

        private static void DeleteUser()
        {
            Console.Write("Enter User ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            EstablishConnection();
            string query = "DELETE FROM Users WHERE UserID = @UserID";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@UserID", id);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "User deleted successfully." : "User not found.");
            }
        }
    }
}