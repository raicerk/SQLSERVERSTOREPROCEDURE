using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SQLSERVERPROCEDURE
{
    public static class SQLSERVER
    {
        public static DataTable Exec(string NombreConexion, string Procedimiento, Dictionary<string, object> VariableYValores, DataTable dt = null)
        {
            DataTable _dt = dt == null ? new DataTable() : dt;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings[NombreConexion]))
            {
                try
                {

                    conn.Open();
                    SqlCommand command = new SqlCommand(Procedimiento, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    if (VariableYValores.Count > 0)
                    {
                        foreach (var var in VariableYValores)
                        {
                            command.Parameters.Add(new SqlParameter(var.Key, var.Value));
                        }
                    }

                    SqlDataReader dr = command.ExecuteReader();
                    _dt.Load(dr);
                    conn.Close();
                    return _dt;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static DataSet Exec(string NombreConexion, string Procedimiento, Dictionary<string, object> VariableYValores, DataSet ds = null)
        {
            DataSet _ds = ds == null ? new DataSet() : ds;
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
                        foreach (var var in VariableYValores)
                        {
                            command.Parameters.Add(new SqlParameter(var.Key, var.Value));
                        }
                    }

                    SqlDataReader dr = command.ExecuteReader();
                    dt.Load(dr);
                    _ds.Tables.Add(dt);
                    conn.Close();
                    return _ds;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public static void Exec(string NombreConexion, string Procedimiento, Dictionary<string, object> VariableYValores)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings[NombreConexion]))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(Procedimiento, conn);
                command.CommandType = CommandType.StoredProcedure;

                if (VariableYValores.Count > 0)
                {
                    foreach (var var in VariableYValores)
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
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    conn.Close();
                    throw ex;
                }
            }


        }
    }
}
