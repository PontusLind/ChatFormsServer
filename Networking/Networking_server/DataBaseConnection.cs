using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking_server
{
    class DataBaseConnection
    {
        static string connectionString = "Data Source=localhost;Initial Catalog=pinocchio;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static bool LoginDB(string name, string password)
        {

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectionString;
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                myCommand.CommandText = "mysp_Login";

                SqlParameter paramUserName = new SqlParameter();
                paramUserName.ParameterName = "@userName";
                paramUserName.DbType = System.Data.DbType.String;
                paramUserName.Value = name;
                myCommand.Parameters.Add(paramUserName);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@userpassword";
                paramPassword.DbType = System.Data.DbType.String;
                paramPassword.Value = password;
                myCommand.Parameters.Add(paramPassword);

                SqlParameter paramSuccess = new SqlParameter();
                paramSuccess.ParameterName = "@success";
                paramSuccess.DbType = System.Data.DbType.Int32;

                paramSuccess.Direction = System.Data.ParameterDirection.Output;
                myCommand.Parameters.Add(paramSuccess);

                SqlDataReader myReader = myCommand.ExecuteReader();

                int temp = Convert.ToInt32(paramSuccess.Value);
                if (temp == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception m)
            {
                Console.WriteLine($"Fel:  {m}");
                return false;
            }

            finally
            {
                myConnection.Close();
            }
        }
    }
}
