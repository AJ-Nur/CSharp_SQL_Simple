using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace SimpleSQLProj_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine();
            Console.WriteLine("Please select an option. \n");
            Console.WriteLine("Select R to SelectStatement, W to UpdateStatement, I to InsertStatement, D to DeleteStatement ...\n");

            string strIn = Console.ReadLine();

            switch (strIn)
            {
                case "R":
                    {
                        Console.WriteLine();
                        Console.Write("Enter Car ID: ");
                        doSelectStatement(int.Parse(Console.ReadLine()));
                        Console.WriteLine();
                        break;
                    }
                case "W":
                    {
                        Console.WriteLine();
                        Console.Write("Enter Update: ");
                        doUpdateStatement(Console.ReadLine());
                        Console.WriteLine();
                        break;
                    }
                case "I":
                    {
                        Console.WriteLine();
                        Console.WriteLine("Insert new data: ");

                        Console.Write("VIN: ");
                        string newVIN = Console.ReadLine();

                        Console.Write("Make: ");
                        string newMake = Console.ReadLine();

                        Console.Write("Model: ");
                        string newModel = Console.ReadLine();

                        Console.Write("Year: ");
                        int newYr = int.Parse(Console.ReadLine());

                        Console.Write("Mileage: ");
                        int newMil = int.Parse(Console.ReadLine());

                        doInsertStatement(newVIN, newMake, newModel, newYr, newMil);
                        Console.WriteLine();
                        break;
                    }
                case "D":
                    {
                        Console.WriteLine();
                        Console.Write("Enter ID to delete: ");

                        doDeleteStatement(int.Parse(Console.ReadLine()));
                        Console.WriteLine();
                        break;
                    }
            }

        }

        private static string getConnectionString()
        {
            string ConnectionStringFileLoc = @"D:\Ain-swe\proj2\SimpleSQLProj-1\ConnectionString.txt";

            if (File.Exists(ConnectionStringFileLoc))
            {
                FileStream fs = File.OpenRead(ConnectionStringFileLoc);
                var sr = new StreamReader(fs);

                string strFind = "Data Source=";
                string strDataSrc = "";

                while ((strDataSrc =  sr.ReadLine()) != null)
                {
                    if (strDataSrc.Contains(strFind))
                    {
                        //strDataSrc = sr.ReadLine();
                        return strDataSrc;
                    }
                }      
            }
            return "Data Source=Provider=SQLOLEDB.1; Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=cartest; Data Source=MYL-6ZWL733";

            //using (FileStream fs = File.OpenRead(ConnectionStringFileLoc))
            //using (var sr = new StreamReader(fs))
            //    return sr.ReadLine();
        }

        private static void doSelectStatement(int CarId)
        {
            //using (SqlConnection newConnection = new SqlConnection("Data Source=Provider=SQLOLEDB.1; Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=cartest; Data Source=MYL-6ZWL733"))
            using (SqlConnection newConnection = new SqlConnection(getConnectionString()))
            {
                SqlCommand selectCommand = new SqlCommand("Select VIN, Make, Model, CarYear, Mileage from Vehicles where CarID = " + CarId, newConnection);
                selectCommand.Connection.Open();
                SqlDataReader sqlReader;

                try
                {
                    sqlReader = selectCommand.ExecuteReader();

                    if (sqlReader.Read())
                    {
                        Console.WriteLine("VIN of car: {0}", sqlReader.GetString(0));
                        Console.WriteLine("Make of car: {0}", sqlReader.GetString(1));
                        Console.WriteLine("Model of car: {0}", sqlReader.GetString(2));
                        Console.WriteLine("Year of car: {0}", sqlReader.GetValue(3));
                        Console.WriteLine("Mileage of car: {0}", sqlReader.GetValue(4));
                    }
                    else
                    {
                        Console.WriteLine("Data not exist");
                    }
                }
                catch
                {
                    Console.WriteLine("Error occured while attempting SELECT.");
                }
                selectCommand.Connection.Close();
            }
        }       

        private static void doUpdateStatement(string updateStatement)
        {
            using (SqlConnection updateConnection = new SqlConnection(getConnectionString()))
            {
                SqlCommand updateCommand = new SqlCommand(updateStatement, updateConnection);
                updateCommand.Connection.Open();

                try
                {
                    if (updateCommand.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("Update successful");
                    }
                    else
                    {
                        Console.WriteLine("No rows updated!");
                    }
                }
                catch
                {
                    Console.WriteLine("Error occured while attempting update.");
                }
                updateCommand.Connection.Close();
            }
        }

        private static void doInsertStatement(string VIN, string Make, string Model, int Year, int Mileage)
        {
            using (SqlConnection insertConnection = new SqlConnection(getConnectionString()))
            {
                string insertStatement = "INSERT INTO Vehicles (VIN, Make, Model, CarYear, Mileage) VALUES " + "('" + VIN + "', '" + Make + "', '" + Model + "', " + Year + ", " + Mileage + ")";

                SqlCommand insertCommand = new SqlCommand(insertStatement, insertConnection);
                insertCommand.Connection.Open();

                try
                {
                    if(insertCommand.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine("Insert statement is successful");
                    }
                    else
                    {
                        Console.WriteLine("Insert statement failed");
                    }
                }
                catch
                {
                    Console.WriteLine("Error while attempting insert.");
                }
                insertCommand.Connection.Close();
            }
        }

        private static void doDeleteStatement(int CarID)
        {
            using (SqlConnection deleteConnection = new SqlConnection(getConnectionString()))
            {
                SqlCommand deleteCommand = new SqlCommand("DELETE from Vehicles WHERE CarID = " + CarID, deleteConnection);
                deleteCommand.Connection.Open();

                try
                {
                    if (deleteCommand.ExecuteNonQuery() > 0)
                        Console.WriteLine("Delete statement is successful");
                    else
                        Console.WriteLine("Delete statement failed");
                }
                catch
                {
                    Console.WriteLine("Error while attempting delete");
                }
                deleteCommand.Connection.Close();
            }
        }
    }
}
