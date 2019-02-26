using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StockMaster5
{
    public class CReports
    {
        CReports()
        {

        }
        ~CReports()
        {

        }
        private bool CheckRecords(string table)
        {
            Boolean Passed = false;
            int QueryResult = 0;
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                try
                {
                    sqlConnection.ConnectionString = @"";
                    try
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand())
                        {
                            sqlCommand.Connection = sqlConnection;
                            sqlCommand.CommandText = "SELECT COUNT(*) as numRows FROM " + table;
                            QueryResult =(int)sqlCommand.ExecuteScalar();
                        }
                        if (QueryResult == 0)
                        {
                            Passed = true;
                        }
                        else
                        {
                            Passed = false;
                        }
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }                   
                }
                catch (SqlException sqlex)
                {
                    Passed = false;
                    MessageBox.Show(sqlex.ToString(),"Error in database table");
                }
            }
            return Passed;            
        }
        private bool DeleteRecordTable(string table)
        {
            Boolean Deleted = false;
            int QueryResult = 0;
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                try
                {
                    sqlConnection.ConnectionString = @"";
                    try
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand())
                        {
                            sqlCommand.Connection = sqlConnection;
                            sqlCommand.CommandText = "DELETE FROM " + table;
                            QueryResult = (int)sqlCommand.ExecuteScalar();
                        }
                        if (QueryResult == 0)
                        {
                            Deleted = true;
                        }
                        else
                        {
                            Deleted = false;
                        }
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
                catch (SqlException sqlex)
                {
                    Deleted = false;
                    MessageBox.Show(sqlex.ToString(), "Error in database table");
                }
            }
            return Passed;
        }
        public bool RunCheckAndRecords()
        {
            Boolean Check = false;
            Boolean Deleted = false;
            Check = CheckRecords("tblStockRepCurrentStock");

        }
    }
}
