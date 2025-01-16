using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesDataAccess
{
    public class clsCountryData
    {
        public static int AddNewCountry(string CountryName)
        {
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"INSERT INTO Countries (CountryName) 
                              VALUES(@CountryName);
                              SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
           

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CountryID = insertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return CountryID;
        }


        public static bool UpdateCountry(int ID, string CountryName)
        {
            int RowsEffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"UPDATE Countries
                               SET
                                   CountryID = @CountryID     
                                   where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);
            command.Parameters.AddWithValue("@CountryName", CountryName);
           
            try
            {
                connection.Open();
                RowsEffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return (RowsEffected > 0);

        }


        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "SELECT * FROM Countries order by CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;

        }


        public static bool GetCountryByID(int ID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("CountryID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = false;
                    CountryName = (string)reader[" CountryName"];
                    
                    reader.Close();
                }
                else
                {
                    isFound = false;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error" + ex.ToString());
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool DeleteCountry(int ID)
        {
            int RowsEffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"DELETE  Countries
                            WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("CountryID", ID);

            try
            {
                connection.Open();
                RowsEffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error" + ex.ToString());

            }
            finally
            {
                connection.Close();
            }
            return (RowsEffected > 0);
        }

        public static bool IsCountryExist(int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = "SELECT Found=1 FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
