using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Attendance
    {



        static void Main(string[] args)
        {


            char[] charsToTrim = { '"' };
            Console.WriteLine("Please enter the file Path:");
            string path = Console.ReadLine();
            string filepath = path.Trim(charsToTrim);
            string databasepath = "Data Source=.;Initial Catalog=Development;Integrated Security=True";
            var linenumber = 0;




            using (SqlConnection con = new SqlConnection(databasepath))
            {
                con.Open();
                using (StreamReader reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (linenumber != 0)
                        {
                            var values = line.Split(',');
                            var sql = "INSERT INTO Development.dbo.AttendanceDetails VALUES ('" + values[0] + "','" + values[1] + "','" + values[2] +"')";
                            var cmd = new SqlCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();


                        }
                        linenumber++;
                    }
                    // Console.WriteLine(dt.ToString());*/
                }

                Console.WriteLine("Inserting data into database.");
                for (int i = 1; i <= 30; i++)
                {
                    Thread.Sleep(300);
                    Console.Write(".");
                    if (i == 30)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Data added to database");
                    }
                }
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter("Select * from AttendanceDetails", con);
                adp.Fill(ds);
                Console.WriteLine("Displaying the data:");
                Thread.Sleep(3000);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                foreach (DataColumn cn in ds.Tables[0].Columns)
                {
                    Console.Write(cn + "  ");
                }
                Console.WriteLine();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Console.WriteLine(row[0] + " , " + row[1] + " , " + row[2]);
                }
                con.Close();
            }
            Console.ReadLine();
        }
    }
}
