using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using TodoApi.Contacts;
using TodoApi.Other;

namespace TodoApi.DataAccess
{
    public class PersonsDS 
    {
        static public Contact GetPersonDA(string FirstName, string LastName)
        {            
            //var strConnectionString = "Server=tcp:pcldb.database.windows.net,1433;Initial Catalog=pcldb;Persist Security Info=False;User ID={pcl};Password={G0DucksQuack};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            var cs = BuildConnectionString();
            
            SqlDataReader objReader;
            Contact contact = new Contact();
            SqlConnection objCon = new SqlConnection(cs.ConnectionString);
            SqlCommand objCmd = new SqlCommand("sp_GetPersonByName", objCon);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 20));
            objCmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 20));
            objCmd.Parameters["@FirstName"].Value = FirstName;
            objCmd.Parameters["@LastName"].Value = LastName;

            try
            {
                objCon.Open();
                objReader = objCmd.ExecuteReader(CommandBehavior.SingleRow);
                while (objReader.Read())
                {
                    contact.PersonID = TodoApi.Other.Helper.SafeGetInt(objReader, objReader.GetOrdinal("PersonID"));
                    contact.firstname = TodoApi.Other.Helper.SafeGetString(objReader, objReader.GetOrdinal("FirstName"));
                    contact.lastname = TodoApi.Other.Helper.SafeGetString(objReader, objReader.GetOrdinal("LastName")); 
                    contact.address = TodoApi.Other.Helper.SafeGetString(objReader, objReader.GetOrdinal("Address"));
                    contact.city = TodoApi.Other.Helper.SafeGetString(objReader, objReader.GetOrdinal("City"));
                }
                objReader.Close();
            }
            catch (SqlException err)
            {
                //Fancy error handling here
            }

            return contact;
        }
    
        static public int AddContactInfo(int PersonID, string firstname, string lastname, string address, string city)
        {
            var cs = BuildConnectionString();
            
            SqlConnection objCon = new SqlConnection(cs.ConnectionString);
            SqlCommand objCmd = new SqlCommand("sp_AddPerson", objCon);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 20));
            objCmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 20));
            objCmd.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int, 4));
            objCmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar, 20));
            objCmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 20));
            
            objCmd.Parameters["@PersonID"].Value = PersonID;
            objCmd.Parameters["@FirstName"].Value = firstname;
            objCmd.Parameters["@LastName"].Value = lastname;
            objCmd.Parameters["@Address"].Value = address;
            objCmd.Parameters["@City"].Value = city;
            
            
            objCon.Open();
            objCmd.ExecuteNonQuery();
            objCon.Close();
            
            return 1;
        }

        static private SqlConnectionStringBuilder BuildConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "pcldb.database.windows.net"; 
                builder.UserID = "pcl";            
                builder.Password = "G0DucksQuack";     
                builder.InitialCatalog = "pcldb";

                return builder;
        }
    }
}