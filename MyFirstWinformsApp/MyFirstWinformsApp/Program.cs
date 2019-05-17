using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using Npgsql;

namespace MyFirstWinformsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static DataGridView grid = new DataGridView();

        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();

            Thread dataQueryThread = new Thread(updateGridData);
            dataQueryThread.Start();

            String timeStamp = GetTimestamp(DateTime.Now);
            Console.WriteLine($"Query thread started at: {timeStamp}");

            Form1 myForm = new Form1();
            myForm.Size = new System.Drawing.Size(1100, 600)

            timeStamp = GetTimestamp(DateTime.Now);
            Console.WriteLine($"Form setup finished at: {timeStamp}");

            dataQueryThread.Join();

            timeStamp = GetTimestamp(DateTime.Now);
            Console.WriteLine($"Query thread finished at: {timeStamp}");

            myForm.EnterData(grid);
            Application.Run(myForm);
        }

        static void updateGridData()
        {
            grid.DataSource = RentalService.GetAllRentals();
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }

    class DBHelper
    {
        public string connectionString;
        public DBHelper(string connection)
        {
            connectionString = connection;
        }
        private NpgsqlConnection CreateConnection()
        {
            NpgsqlConnection cnn;
            cnn = new NpgsqlConnection(connectionString);
            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
                MessageBox.Show(ex.ToString());
            }
            return cnn;
        }

        public DataTable ExecuteTable(string sql)
        {
            NpgsqlConnection cnn = CreateConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, cnn);

                da.Fill(ds);
                dt = ds.Tables[0];

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                cnn.Close();
            }
            return dt;
        }
    }


    static class RentalService
    {

        public static DataTable GetAllRentals()
        {
            string connStr = $"Server=localhost;Port=5432;User Id=postgres;Password=kImpa20$;Database=dvdrental;";
            DBHelper myDBHelper = new DBHelper(connStr);
            DataTable rentalData = myDBHelper.ExecuteTable("SELECT * FROM rental");
            return rentalData;
        }
    }

}
