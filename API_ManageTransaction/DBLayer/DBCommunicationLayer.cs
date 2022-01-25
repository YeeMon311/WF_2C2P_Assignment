using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace API_ManageTransaction.DBLayer
{
    public class DBCommunicationLayer : DBConnections
    {
        private SqlCommand command = null;
        private SqlDataAdapter adapter = null;
        private DataSet dataset = null;

        //[ID] [int] IDENTITY(1,1) NOT NULL,
        //[TransactionID] [nvarchar] (50) NULL,
        //[Amount] [decimal](18, 2) NULL,
        //[CurrencyCode] [varchar] (3) NULL,
        //[TransactionDate] [smalldatetime] NULL,
        //[Status] [varchar] (1) NULL
        public bool CreateSingleTransaction(Entities.Transactions oneTransaction)
        {
            bool success = false;
            try
            {
                string sql = "INSERT INTO [dbo].[tblTransactions]([TransactionID],[Amount],[CurrencyCode],[TransactionDate],[Status],[CurrencyID]) VALUES (@TransactionID, @Amount, @CurrencyCode, GETDATE(), @Status, @CurrencyID)";

                SqlConnection sqlCon = OpenConnection();

                SqlCommand cmd = new SqlCommand(sql, sqlCon);

                cmd.Parameters.Add("@TransactionID", SqlDbType.VarChar, 50);
                cmd.Parameters["@TransactionID"].Value = oneTransaction.TransactionID;

                cmd.Parameters.Add("@Amount", SqlDbType.VarChar, 50);
                cmd.Parameters["@Amount"].Value = oneTransaction.Amount;

                cmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 3);
                cmd.Parameters["@CurrencyCode"].Value = oneTransaction.CurrencyCode;

                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 1);
                cmd.Parameters["@Status"].Value = oneTransaction.Status;

                cmd.Parameters.Add("@CurrencyID", SqlDbType.Int);
                cmd.Parameters["@CurrencyID"].Value = oneTransaction.CurrencyID;

                int rowsEffected = cmd.ExecuteNonQuery();

                if (rowsEffected > 0)
                    success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return success;
        }


        public DataSet GetTransactionsDataByDateRange(string from, string to)
        {
            try
            {
                dataset = new DataSet();
                string sql = " SELECT trans.TransactionID,trans.Amount,trans.TransactionDate,trans.Status,trans.CurrencyID,trans.CurrencyCode " +
                               "FROM tblTransactions trans " +
                               "WHERE trans.TransactionDate between CONVERT(smalldatetime,'" + from + "') and CONVERT(smalldatetime,'" + to + "')";

                OpenConnection();

                to = to + Utilities.Utilities.maxHHMMSS;

                command = new SqlCommand(sql, OpenConnection());
                adapter = new SqlDataAdapter(command);

                adapter.Fill(dataset, "TransactionsByDateFilters");
            }
            catch (Exception ex)
            {
                //write to log'
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return dataset;

        }


        public DataSet GetTransactionsDataByColumns(string valueToPull, string parameterToPull)
        {
            try
            {
                dataset = new DataSet();

                string sql = string.Empty;
                sql = " SELECT trans.TransactionID,trans.Amount,trans.TransactionDate,trans.Status,trans.CurrencyID,trans.CurrencyCode " +
                            "FROM tblTransactions trans " +
                            "WHERE trans." + parameterToPull + "='" + valueToPull + "'";

                command = new SqlCommand(sql, OpenConnection());
                adapter = new SqlDataAdapter(command);

                adapter.Fill(dataset, "Transactions");
            }
            catch (Exception ex)
            {
                //write to log'
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return dataset;

        }

    }
}