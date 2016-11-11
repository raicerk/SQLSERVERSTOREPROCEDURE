using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SQLSERVERPROCEDURE
{
    public static class SQLSERVER
    {
        public static DataTable Query(string NombreConexion, string Procedimiento, Dictionary<string, string> VariableYValores, DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings[NombreConexion]))
            {
                try
                {

                    conn.Open();
                    SqlCommand command = new SqlCommand(Procedimiento, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    if (VariableYValores.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> var in VariableYValores)
                        {
                            command.Parameters.Add(new SqlParameter(var.Key, var.Value));
                        }
                    }

                    SqlDataReader dr = command.ExecuteReader();
                    dt.Load(dr);
                    conn.Close();
                    return dt;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static DataSet Query(string NombreConexion, string Procedimiento, Dictionary<string, string> VariableYValores, DataSet ds)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings[NombreConexion]))
            {
                try
                {

                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlCommand command = new SqlCommand(Procedimiento, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    if (VariableYValores.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> var in VariableYValores)
                        {
                            command.Parameters.Add(new SqlParameter(var.Key, var.Value));
                        }
                    }

                    SqlDataReader dr = command.ExecuteReader();
                    dt.Load(dr);
                    ds.Tables.Add(dt);
                    conn.Close();
                    return ds;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public static void Query(string NombreConexion, string Procedimiento, Dictionary<string, string> VariableYValores)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings[NombreConexion]))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(Procedimiento, conn);
                command.CommandType = CommandType.StoredProcedure;

                if (VariableYValores.Count > 0)
                {
                    foreach (KeyValuePair<string, string> var in VariableYValores)
                    {
                        if (var.Key != "")
                        {
                            command.Parameters.Add(new SqlParameter(var.Key, var.Value));
                        }

                    }
                }

                try
                {
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }


        }
    }
}
