using System;
using System.Collections.Generic;
using System.Text;

namespace HigherOrLowerAcademyApp.DBRowClasses
{
    public static class DBClassFactory
    { // this facotry wuill be able to make any instances of classes that have to be made in a specific way to accept database rows through Dapper. this will 
        // keep all setting based activities out of the setters in the classes themselves, since dapper will get mad if any errors trigger in those setters, meaning
        // that since we don't really know what we'll be importing and what wil lhappen, its easiest to have them be the default, and we'll do all the other stuff out here


        // ===============================================================================================================
        // STUDENT FACTORY ZONE
        public struct StudentApplication
        {
            // holds all the user provided data needed to create a student, subject to review in this factory class.

            // the things that will be user inputs
            public string studentName;
            public string studentNotes;
            public bool blessedByGods;

            // values for reporting back to calling code
            public string validationMessage;
            public bool applicationValid;

            public StudentApplication(string inputStudentName,string inputStudentNotes,bool inputBlessedByGods)
            { // basic constructor to pass all the info in
                studentName = inputStudentName;
                studentNotes = inputStudentNotes;
                blessedByGods = inputBlessedByGods;

                // presets for the reporting data
                validationMessage = String.Empty;
                applicationValid = true; // they are assuemd valid until found oterhwise.
            }
        }

        public static StudentApplication ProcessStudentApplication(StudentApplication sApp)
        {
            // all validation on the data provided on the student application will come in here. any automatic changes can be applied, and the application can be
            // makred as suitable or suitable for goikng forward. this is effectively acting a pre-setter for the Student class, ensuring all data is ready to go
            // to a basic student creation method.

            bool validationSuccessful = true;
            string message = String.Empty;

            // there are two phases this processing, in principal: auto updating any problems we want to solve progamatically right away, then the validatoin iself
            // ---------------------------------------------------------------------------------
            // UPDATE PHASE
            // change anything that needs changing - not doing anything right now, this is future proofing

            // -----------------------------------------------------------------------------------
            // VALIDATION PHASE

            // -------------------------------------------------
            // NAME
            string name = sApp.studentName;
            if (String.IsNullOrWhiteSpace(name))
            {
                // it's nothing, or just loads of spaces / non-character characters
                message += "You need to enter the student's name. ";
                validationSuccessful = false;
            }
            else if (String.Equals(name, "Devin"))
            {
                message += "The student cannot be named Devin. There is only 1 Devin, and this student is not them. ";
                validationSuccessful = false;
            }

            // ----------------------------------------------
            // NOTES - no validation for now ( could look for sql injections or something if we're really bothered, or that that string is too long for the data type***)

            // -----------------------------------------------
            // BLESSED - no validation, probably won't ever need it

            // ------------------------------------------------

            // ----------------------------------------------------------------------------------------
            // VALIDATION COMPLETE - FINIALISE APPLICATION

            // Add reporting data to our application
            sApp.applicationValid = validationSuccessful;
            sApp.validationMessage = message;

            return sApp;
        }
        // =================================================================================================================

    }
}
