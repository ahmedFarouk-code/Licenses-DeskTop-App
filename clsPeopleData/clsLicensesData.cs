using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesDataAccess
{
    public class clsLicensesData
    {
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate, string Notes,
             decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"INSERT INTO Licenses (ApplicationID,DriverID, LicenseClass, IssueDate,
                              ExpirationDate, Notes, PaidFees, IsActive, IssueReason,
                              CreatedByUserID) 
                              VALUES(@ApplicationID,@DriverID, @LicenseClass, @IssueDate,
                              @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason,
                              @CreatedByUserID);
                              SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
          
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
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
            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID ,int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate, string Notes,
             decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int RowsEffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = @"UPDATE Licenses
                               SET
                                   ApplicationID = @ApplicationID,
                                   DriverID = @DriverID,
                                   LicenseClass = @LicenseClass,
                                   IssueDate = @IssueDate,
                                   ExpirationDate = @ExpirationDate,
                                   Notes = @Notes,
                                   PaidFees = @PaidFees,
                                   IsActive = @IsActive,
                                   IssueReason = @IssueReason,
                                   CreatedByUserID = @CreatedByUserID,
                                   where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



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


        public static DataTable GetAllLicenseByDriver(int DriverID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "SELECT * FROM Licenses where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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
    }
}
