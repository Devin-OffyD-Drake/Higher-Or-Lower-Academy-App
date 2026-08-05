using System;
using System.Collections.Generic;
using System.Text;

namespace HigherOrLowerAcademyApp.DBRowClasses
{
    public class User
    { // represents a row from the Users table in the database

        public int userId { get; set; }
        public string userName { get; set; }
        public int studentID { get; set; }
        public string userPassword { get; set; }   
        public bool isAdmin { get; set; }

        public User()
        {
            // leave constructor blank for Dapper
        }
    }
}
