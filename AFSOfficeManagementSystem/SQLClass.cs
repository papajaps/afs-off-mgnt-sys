using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AFSOfficeManagementSystem
{
    class SQLClass
    {
        static string ipAdd = GetLocalIPAddress();
        public string ConnectionString = "datasource=localhost;port=3306;username=root;password=;database=afs_foods_corp;";
        MySqlConnection con;

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public void OpenConection()
        {
            con = new MySqlConnection(ConnectionString);
            con.Open();
        }
        public void CloseConnection()
        {
            con.Close();
        }
        public void ExecuteQueries(string Query_)
        {
            OpenConection();
            MySqlCommand cmd = new MySqlCommand(Query_, con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public MySqlDataReader DataReader(string Query_)
        {
            MySqlCommand cmd = new MySqlCommand(Query_, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public object ShowDataInGridView(string Query_)
        {
            MySqlDataAdapter dr = new MySqlDataAdapter(Query_, ConnectionString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }

        public object fillcombobox(string Query_)
        {
            object dataum = "";
            MySqlDataAdapter dr = new MySqlDataAdapter(Query_, ConnectionString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataum = ds.Tables[0];
            }
            return dataum;

        }
        

    }
}
