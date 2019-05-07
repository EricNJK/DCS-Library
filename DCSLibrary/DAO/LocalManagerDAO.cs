using System;
using System.Data.SqlClient;

namespace DCSLibrary.DAO
{
    public class LocalManagerDAO : SuperDAO
    {
        //LocalManager manager;

        /// <summary>
        /// Authenticates local managers.
        /// </summary>
        /// <param name="nationalId"> Employee's national id number. </param>
        /// <param name="password"> Employee's secret key. </param>
        /// <returns> A <see cref="LocalManager"/> instance, or null if credentials are invalid. </returns>
        public LocalManager Authenticate(string nationalId, string password)
        {
            LocalManager manager = null;
            int nId = int.Parse(nationalId);

            SqlDataReader dr;
            string sql = String.Format("SELECT employeeId, name, store FROM Employees WHERE title=2 " +
                "AND employeeId='{0}' AND password='{1}'", nId, password);
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.de90ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            int employeeId = 0;
            try
            {
                cn.Open();
                dr = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    employeeId = (int)dr["employeeId"];
                    if (employeeId != 0)
                        manager = new LocalManager(employeeId, dr["name"].ToString(), dr["store"].ToString());
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error connecting to database", ex);
            }
            finally
            {
                cn.Close();
            }
            return manager;
        }

        
    }
}
