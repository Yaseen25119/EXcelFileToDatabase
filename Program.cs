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
    internal class Program
    {



        static void Main(string[] args)
        {
            
            
            char[] charsToTrim = { '"' };
            Console.WriteLine("Please enter the file Path:");
            string path = Console.ReadLine();
            string filepath = path.Trim(charsToTrim);
            string databasepath = "Data Source=.;Initial Catalog=Development;Integrated Security=True";
            var linenumber = 0;
            string[] rows;
            string[] columns;
            int index = 1;
            rows = File.ReadAllLines(filepath);
            while(index< rows.Length)
            {
                columns = rows[index].Split(',');
                Console.WriteLine(index+ ": " + columns[0]);
                index++;
            }

            /*DataTable dt = new DataTable();
            dt.Columns.Add("ExternalStudentID", typeof(SqlInt32));
            dt.Columns.Add("FirstName", typeof(SqlString));
            dt.Columns.Add("LastName", typeof(SqlString));
            dt.Columns.Add("DOB", typeof(SqlString));
            dt.Columns.Add("SSN", typeof(SqlInt32));
            dt.Columns.Add("Address", typeof(SqlString));
            dt.Columns.Add("City", typeof(SqlString));
            dt.Columns.Add("State", typeof(SqlString));
            dt.Columns.Add("Email", typeof(SqlString));
            dt.Columns.Add("MaritalStatus", typeof(SqlString));
            dt.Columns.Add("InternalStudentId", typeof(SqlInt32));*/




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
                            var sql = "INSERT INTO Development.dbo.importexcel VALUES ('" + values[0] + "','" + values[1] + "','" + values[2] + "','" + values[3] + "','" + values[4] + "','" + values[5] + "','" + values[6] + "','" + values[7] + "','" + values[8] + "','" + values[9] + "','" + values[10]+"')";
                            var cmd = new SqlCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                           // dt.Rows.Add(Convert.ToInt32( values[0]), values[1], values[2], values[3],Convert.ToInt32( values[4]), values[5], values[6], values[7], values[8], values[9],Convert.ToInt32( values[10]));


                        }
                        linenumber++;
                    }
                    /*foreach (DataRow row in dt.Columns)
                    {
                        Console.WriteLine(row);
                    }
                    Console.WriteLine(dt.ToString());*/
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
                SqlDataAdapter adp = new SqlDataAdapter("Select * from importexcel", con);
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
                    Console.WriteLine(row[0] + " , " + row[1] + " , " + row[2] + " , " + row[3] + " , " + row[4] + " , " + row[5] + " , " + row[6] + " , " + row[7] + " , " + row[8] + " , " + row[9] + " , " + row[10]);
                }
                con.Close();
            }
            Console.ReadLine();
        }
    }
}
