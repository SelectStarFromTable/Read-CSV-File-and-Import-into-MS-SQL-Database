using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Replace with your own file path
            string filePath = "C:\\path\\to\\your\\csv\\file.csv";

            // Read the data from the CSV file and map it to a list of User objects
            List<User> users = new List<User>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                // Read the column headers and store them in a dictionary
                string[] headers = sr.ReadLine().Split(',');
                Dictionary<string, int> headerDictionary = new Dictionary<string, int>();
                for (int i = 0; i < headers.Length; i++)
                {
                    headerDictionary.Add(headers[i], i);
                }

                // Read the data rows and map them to User objects
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    User user = new User
                    {
                        FirstName = fields[headerDictionary["FirstName"]],
                        LastName = fields[headerDictionary["LastName"]],
                        Age = int.Parse(fields[headerDictionary["Age"]])
                    };
                    users.Add(user);
                }
            }

            // You can now work with the list of User objects as needed
            foreach (User user in users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} is {user.Age} years old.");
            }
        }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
