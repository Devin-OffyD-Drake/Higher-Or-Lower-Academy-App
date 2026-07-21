using System;
using System.Collections.Generic;
using System.Text;

namespace HigherOrLowerAcademyApp.DBRowClasses
{ // represents the students, and corresponds to a row in the student table of the HigherOrLowerAcademy database
    public class Student
    {
        // REFERENCES to these variables names are hard coded in sql in this app, so DON@T CHANGE THEM
        // they all need gettersetter in order to be able to be automatically loaded from the DB by the dapper functionality
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string notes { get; set; }
        public bool blessedByGods { get; set; }

        /*// CONSTRUCTOR
        public Student(int dbID, string dbName, string dbNotes, bool dbBlessed)
        {
            // students essentially read from the databse, so i will assume all data has been passed as argument
            // even new students will be created in the database first THEN loaded into c# via this class, which is
            // necessary since we need the database to assign the ID with its own incremenal idenitity mechanic.
            studentID = dbID;
            studentName = dbName;
            notes = dbNotes;
            blessedByGods = dbBlessed;

            
        }*/

        public Student() // thing that will be DB objects need a public, parameterless constructor for Dapper to be able to load into them corretly
        {

        }

    }
}
