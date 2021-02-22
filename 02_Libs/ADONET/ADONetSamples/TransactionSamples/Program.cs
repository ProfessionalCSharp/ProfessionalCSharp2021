using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TransactionSamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .Build();

            await TransactionSampleAsync();
        }

        private static async Task TransactionSampleAsync()
        {
            using SqlConnection connection = new(GetConnectionString());
            await connection.OpenAsync();
            SqlTransaction tx = connection.BeginTransaction();

            try
            {
                string sql = "INSERT INTO [ProCSharp].[Books] ([Title], [Publisher], [Isbn], [ReleaseDate]) " +
                    "VALUES (@Title, @Publisher, @Isbn, @ReleaseDate);" +
                    "SELECT SCOPE_IDENTITY()";
                var command = new SqlCommand
                {
                    CommandText = sql,
                    Connection = connection,
                    Transaction = tx
                };

                SqlParameter p1 = new("Title", SqlDbType.NVarChar, 50);
                SqlParameter p2 = new("Publisher", SqlDbType.NVarChar, 50);
                SqlParameter p3 = new("Isbn", SqlDbType.NVarChar, 20);
                SqlParameter p4 = new("ReleaseDate", SqlDbType.Date);
                command.Parameters.AddRange(new SqlParameter[] { p1, p2, p3, p4 });

                command.Parameters["Title"].Value = "Professional C# 8 and .NET Core 3.0";
                command.Parameters["Publisher"].Value = "Wrox Press";
                command.Parameters["Isbn"].Value = "42-08154711";
                command.Parameters["ReleaseDate"].Value = new DateTime(2020, 9, 2);

                object? id = await command.ExecuteScalarAsync();
                Console.WriteLine($"record added with id: {id}");

                command.Parameters["Title"].Value = "Professional C# 9 and .NET Core 4.0";
                command.Parameters["Publisher"].Value = "Wrox Press";
                command.Parameters["Isbn"].Value = "42-08154711";
                command.Parameters["ReleaseDate"].Value = new DateTime(2022, 11, 2);

                id = await command.ExecuteScalarAsync();
                Console.WriteLine($"record added with id: {id}");
                // throw new Exception("abort");

                tx.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error {ex.Message}, rolling back");
                tx.Rollback();
            }
        }

        public static string GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            IConfiguration config = configurationBuilder.Build();
            string connectionString = config["Data:DefaultConnection:ConnectionString"];
            return connectionString;
        }
    }
}
