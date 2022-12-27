using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Replace with your own connection string and file path
            string connectionString = "Server=yourserver;Database=yourdatabase;User Id=yourusername;Password=yourpassword";
            string filePath = "C:\\path\\to\\your\\csv\\file.csv";

            // Open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Read the data from the CSV file
                DataTable dataTable = new DataTable();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string[] headers = sr.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }

                    while (!sr.EndOfStream)
                    {
                        string[] rows = sr.ReadLine().Split(',');
                        DataRow dr = dataTable.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                        }
                        dataTable.Rows.Add(dr);
                    }
                }

                // Write the data to the database table
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "yourtablename";
                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }
    }
}
