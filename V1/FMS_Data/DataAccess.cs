using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace FMS_Data
{
  public  class DataAccess
    {
        static string _ConnectionString = "Data Source=localhost:1521/XE;User Id=Freelencertest;Password=freelencer";


        static OracleConnection _Connection = null;

        public static OracleConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new OracleConnection(_ConnectionString);
                    _Connection.Open();

                    return _Connection;
                }
                else if (_Connection.State != System.Data.ConnectionState.Open)
                {
                    _Connection.Open();

                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public static DataSet GetDataSet(string query)
        {
            //create panel  to write query
            OracleCommand cmd = new OracleCommand(query, Connection);

            //convert data from oracle format to C#(dataset)
            OracleDataAdapter adp = new OracleDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            Connection.Close();
            //testing
            return ds;
        }

        public static DataTable GetDataTable(string query)
        {
            DataSet ds = GetDataSet(query);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static int ExecuteQuery(string query)
        {
            OracleCommand cmd = new OracleCommand(query, Connection);
            return cmd.ExecuteNonQuery();
        }
    }
}
