using DataAccessLibrary.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataAccessLibrary.Data
{
    public static class SQLiteAccess
    {
        private static string _dbpath { get; set; }
        public static async Task InitSQLite(string databaseName)
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists);
            _dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName);
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();

                var query = @"CREATE TABLE IF NOT EXISTS [Customer] (
                [CustomerID] INTEGER NOT NULL AUTOINCREMENT PRIMARY KEY,
                [CustomerName] TEXT NOT NULL);
                CREATE TABLE IF NOT EXISTS [Issue] (
                [IssueID] INTEGER NOT NULL AUTOINCREMENT PRIMARY KEY,
                FOREIGN KEY(CustomerID) REFERENCES Customer(CustomerID),
                [IssueCategory] TEXT NOT NULL,
                [IssueDescription] TEXT NOT NULL,
                [IssueStatus] TEXT NOT NULL,
                [IssueDate] DATETIME NOT NULL)";

                var cmd = new SqliteCommand(query, db);
                await cmd.ExecuteNonQueryAsync();

                db.Close();
            }
        }
        public static async Task<int> CreateCustomer(Customer customer)
        {
            int id = 0;
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();

                var query = "INSERT INTO [Customer] VALUES (null, @CustomerName)";

                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (int)await cmd.ExecuteScalarAsync();

                db.Close();
            }
            return id;
        }

        public static async Task<int> CreateIssue(Issue issue)
        {
            int id = 0;
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();

                var query = "INSERT INTO [Issue] VALUES (null, @CustomerID, @IssueCategory, @IssueDescription, @IssueStatus, @IssueDate)";

                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@CustomerID", issue.CustomerID);
                cmd.Parameters.AddWithValue("@IssueCategory", issue.IssueCategory);
                cmd.Parameters.AddWithValue("@IssueDescription", issue.IssueDescription);
                cmd.Parameters.AddWithValue("@IssueStatus", issue.IssueStatus);
                cmd.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (int)await cmd.ExecuteScalarAsync();

                db.Close();
            }
            return id;
        }
        public static async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = new List<Customer>();

            using (SqliteConnection db = new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();

                var query = "SELECT * FROM [Customer]";

                var cmd = new SqliteCommand(query, db);
                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customers.Add(new Customer(result.GetInt32(0), result.GetString(1)));
                    }
                }

                db.Close();
            }
            return customers;
        }
        public static async Task<IEnumerable<Issue>> GetIssues()
        {
            var issues = new List<Issue>();

            using (SqliteConnection db = new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();

                var query = "SELECT * FROM [Issue]";

                var cmd = new SqliteCommand(query, db);
                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        issues.Add(new Issue(result.GetInt32(0), result.GetInt32(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetDateTime(5)));
                    }
                }

                db.Close();
            }
            return issues;
        }
    }
}
