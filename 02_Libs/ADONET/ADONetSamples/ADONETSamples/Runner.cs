using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Data;
using System.Transactions;

namespace CommandSamples
{
    public class RunnerConfiguration
    {
        public string? ConnectionString { get; set; }
    }
    public class Runner
    {
        private readonly string _connectionString;
        public Runner(IOptions<RunnerConfiguration> options)
        {
            _connectionString = options.Value.ConnectionString ?? throw new ArgumentException("Connection string missing with options");
        }

        public void CreateDatabase()
        {
            using SqliteConnection connection = new(_connectionString);
            string sql = "CREATE DATABASE Books"
        }

        public void ExecuteScalar()
        {
            using SqliteConnection connection = new(_connectionString);
            string sql = "SELECT COUNT(*) FROM Books";
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = sql;
            connection.Open();
            object count = command.ExecuteScalar();
            Console.WriteLine($"counted {count} book records");
        }

        //public void CreateCommand()
        //{
        //    using (var connection = new SqlConnection(GetConnectionString()))
        //    {
        //        string sql = "SELECT [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books]";
        //        var command = new SqlCommand(sql, connection);

        //        //SqlCommand command2 = connection.CreateCommand();
        //        //command2.CommandText = sql;
        //        //command2.CommandType = CommandType.Text;

        //        connection.Open();

        //        // ...

        //        SqlDataReader reader = command.ExecuteReader();
        //    }
        //}

        public void CreateCommandWithParameters()
        {
            using SqliteConnection connection = new(_connectionString);
            string sql = "SELECT [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books] WHERE lower([Title]) LIKE @Title";
            SqliteCommand command = new(sql, connection);
            command.Parameters.AddWithValue("Title", "Professional C#");
            command.Parameters.Add("TitleStart", SqliteType.Text, 50);
            command.Parameters["Title"].Value = "Professional C#%";

            connection.Open();

            // ...

            SqliteDataReader reader = command.ExecuteReader();
        }

        private void ExecuteCommand()
        {
            using SqliteConnection connection = new(_connectionString);
            
            string sql = "SELECT [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books] WHERE lower([Title]) LIKE @Title ORDER BY [ReleaseDate]";
            SqliteCommand command = new(sql, connection);
            SqliteParameter parameter = new("Title", SqliteType.Text, 50)
            {
                Value = "Professional C#"
            };
            command.Parameters.Add(parameter);

            //SqlCommand command2 = connection.CreateCommand();
            //command2.CommandText = sql;
            //command2.CommandType = CommandType.Text;

            connection.Open();

            // ...

            using SqliteDataReader reader = command.ExecuteReader();
                
            while (reader.Read())
            {
                var s = reader[0];
                Console.WriteLine(s);
                //Console.WriteLine($"{reader.GetString(1)} {reader.GetString(2) ?? String.Empty}");
            }            
        }

        public void ExecuteNonQuery()
        {
            using TransactionScope scope = new(TransactionScopeOption.Required);
            try
            {
                using SqliteConnection connection = new(_connectionString);
                SqliteTransaction tx = connection.BeginTransaction();

                string sql = "INSERT INTO [Books] ([Title], [Publisher], [Isbn], [ReleaseDate]) " +
                    "VALUES (@Title, @Publisher, @Isbn, @ReleaseDate)";

                using SqliteCommand command = new(sql, connection);
                command.Parameters.AddWithValue("Title", "Professional C# 7 and .NET Core 2.0");
                command.Parameters.AddWithValue("Publisher", "Wrox Press");
                command.Parameters.AddWithValue("Isbn", " 978-1119449270");
                command.Parameters.AddWithValue("ReleaseDate", new DateTime(2018, 4, 2));
              
                connection.Open();
                int records = command.ExecuteNonQuery();
                Console.WriteLine($"{records} record(s) inserted");
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ExecuteReader(string title)
        {
            string GetBookQuery() =>
                "SELECT [Id], [Title], [Publisher], [ReleaseDate] FROM [Books] WHERE lower([Title]) LIKE @Title ORDER BY [ReleaseDate] DESC";

            SqliteConnection connection = new(_connectionString);

            SqliteCommand command = new(GetBookQuery(), connection);
            SqliteParameter parameter = new("Title", SqliteType.Text)
            {
                Value = $"{title}%"
            };
            command.Parameters.Add(parameter);

            connection.Open();

            using SqliteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string bookTitle = reader.GetString(1);
                string publisher = reader.GetString(2);
                DateTime? releaseDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);
                Console.WriteLine($"{id,5}. {bookTitle,-40} {publisher,-15} {releaseDate:d}");
            }
        }
    }
}
