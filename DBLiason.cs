using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace HigherOrLowerAcademyApp
{
    // a class which will hold various things to do with interaction with the db, and a single obkect of this class will be made on startup and 
    // persist through all parts of the app



    public class DBLiason  
    {
        // hardcoding teh database this app connects to, for now***
        public static string databaseConnectionString =
            "Data Source=.;Database=HigherOrLowerAcademy;Persist Security Info=True;User ID = sa; Password=qwertypop;TrustServerCertificate=True";
        // because sql server is encrypted, there will be issues with security certainificates being invalid looking (don't really know why)
        // but adding the TrustServerCertificate=True arg gives a hacky workaround that makes the program presume sql server has valid certificates.


        public SqlConnection ConnectToDatabase()
        {
            // connects to the main database linked to the app, the connection string for which is defined above. returns null if failed.
            try
            {
                return new SqlConnection(databaseConnectionString);
            }
            catch (Exception ex)
            { // *** probably more to be done in here
                MessageBox.Show($"Failed to connect to database. Connection string tried was:\n{databaseConnectionString}");
                return null;
                throw;
            }
        }
    }
}
