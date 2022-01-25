using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace API_ManageTransaction.DBLayer
{
    public class DBConnections
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connectionString_2C2PPayment"].ToString();

        protected SqlConnection connection;
        public SqlConnection OpenConnection()
        {

            try
            {
                connection = new SqlConnection(connectionString);
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                return connection;

            }
            catch (Exception ex)
            {
                //Write log to file
                throw ex;
            }

        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // if error encountered in closing connection, write log to file
            }
            finally
            {
                connection = null;
            }
        }

        ~DBConnections()
        {
            // tell the GC not to finalize
            //GC.SuppressFinalize(this);
        }
    }
}