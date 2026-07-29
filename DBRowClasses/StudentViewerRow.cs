using System;
using System.Collections.Generic;
using System.Text;

namespace HigherOrLowerAcademyApp.DBRowClasses
{
    public class StudentViewerRow 
        // this class will hold the data for dapper to import from teh StudentViewData view from SQL server, which is the basis of the student viewer form
    { // unforuntaltey for dapper to work, you need to have all the propeties here, even ones that already exist on teh other class (dapper messes up index order
        // if you inherit another DBRow class). 

        // THE ORDER THESE VARIABLE APPEAR IN HERE IS THE DEFAULT COLUMN ORDER IN THE STUDENT VIEWER
        public int studentID { get; set; }
        public string studentName { get; set; }

        public bool blessedByGods { get; set; }

        public int totalGames { get; set; }
        public int averageScore { get; set; }

        public string notes { get; set; }


        public StudentViewerRow()
        {
            // enmpty construtor for dapper
        }

    }
}
