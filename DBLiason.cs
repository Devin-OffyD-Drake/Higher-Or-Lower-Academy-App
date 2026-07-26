using Dapper;
using HigherOrLowerAcademyApp.DBRowClasses;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;

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

        // as time of writing, this class doesn't interfact with any variables or otherwise need context such that it needs
        // to exist anywhere in the code. one day, if the connect string will be configurable from teh app to hit different databases, i suppose it will need
        // to be non-static, so maybe its more future-proof to pass the object around?

        // i want to define some enums to represent tables, so i can make some general transaction functions that have branches based on the enum passed
        public enum Tables
        {
            Student,
            NotApplicable // no functionality as it stands
        }

        // i will also keep a list of a hardcoded column snames of the primary key for each table, which will be useful in allowing us to use soem general functions in here
        public static Dictionary<Tables, string> primaryKeyNames = new Dictionary<Tables, string>
        {
            {Tables.Student, "StudentID"}
            // add more here as an when needed (commas between the sets)
        };

        public string ConvertTableEnumToTableName(Tables fromTable)
        { // this returns a hardcoded table name string based on the value of a Table enum - this will be theuniversal place where tables names are defined in the DB

            string tableNameString = String.Empty;

            switch (fromTable)
            {
                case Tables.Student:
                    tableNameString = "Students";
                    break;

                // more coming soon...

                default: // just be careful so it never goes here, nee?
                    MessageBox.Show($"Invalid Table enum detected: {fromTable.ToString()}");
                    break;
            }

            return tableNameString;
        }

        public string GetAliasedColumnsString(Tables tableEnum)
        {
            // a place to store these hardcoded strings, which will contain the list of all fields on a database row, and their aliases,
            // which are the variable names on th Student class in this app. Dapper needs to this to be perfect to load from the DB correctly, so this will be the
            // on eplace all future table changes are accounted for in the App

            string s = String.Empty;

            switch (tableEnum) // just gonna allow a load of hardcoded options here, based on table names, no funny business, just gotta get it right while developing
            {
                case Tables.Student:
                    s = "StudentID AS studentID, StudentName AS studentName, StudentNotes AS notes, BlessedByTheGods AS blessedByGods";
                    break;

                // *** MORE COMING SOON YA

                default:
                    MessageBox.Show($"GetAliasedColumnsString was called with an invalid Tables Enum arguement: {tableEnum.ToString()}." +
                        $"\nThe arguement must be setup in the DBLiason class and refer to a table that can be imported to the App (i.e. has an equivalent class in the app)." +
                        $"\nAlso, I need to have remembered to setup the columns string for it #ItsNeverTheDevelopersFault.");
                    break;

            }

            return s;

        }

        public SqlConnection ConnectToDatabase() // that doesn't need any contentual data as it stands, so it could be static, but in prinpciapl we might connect dirrenelty from diffrent places
        {
            // connects to the main database linked to the app, the connection string for which is defined above. returns null if failed.
            try
            {
                return new SqlConnection(databaseConnectionString);
            }
            catch (ArgumentException ex) // this is the only exception for SqlConnection
            { // *** do some proper testing on reactino to different con string inputs
                MessageBox.Show($"Failed to connect to database (argument exception). The given connection string did not lead to a successful database connection. " +
                    $"Connection string tried was:\n{databaseConnectionString} \nAnd the returned error message was: {ex.Message}");
                return null;
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error encountered when connecting to database, no connection was made.");
                return null;
                throw;
            }
        }

        // ===================================================================================================================
        // GENERALISED TRANSACTIONS

        public bool DeleteDBRow(Tables fromTable, long primaryKeyValue =-1, string customWHERE = "")
        { // attemps to delete a row from a table in the database. table is passed as an enum, no magic stringing
            // use the optional string arg to overwrite the genearted WHERE line with something of your own, 
            // otherwise it will delete by the hardcoded primary key of the table
            // and yes we assume the primary key is a number, which it will be for our database, but this isnt necessarily true in general

            bool deleteSuccessful = false;

            using SqlConnection c = ConnectToDatabase();

            if (c != null)
            {
                // very basic query, just deleting the row based on ID number

                string tableName = ConvertTableEnumToTableName(fromTable);

                string query = $"DELETE FROM {tableName} ";
                if (customWHERE != "")
                    query += customWHERE; // being VERY tursting here to let you put on any where you want, and then going ahead wily nily. maybe don't use this, if you aren't sure.
                else
                    query += $"WHERE {primaryKeyNames[fromTable]} = {primaryKeyValue};"; // simple ID match attempt

                // well, let's do it!
                long rowsEffected = 0;
                try
                {
                    rowsEffected = c.Execute(query);

                    // check for no rows being effected, indicating that nothing was deleted
                    if (rowsEffected > 0)
                    {
                        deleteSuccessful = true;
                    }
                    else
                    {
                        MessageBox.Show($"A delete query ran successfully, but no rows were deleted. The table was {tableName}. " +
                            $"Primary Key searched for was {primaryKeyValue}. Custom WHERE used was: {customWHERE}");
                    }
                }
                catch
                { // eeprion was probably c.Execute failing to talk to the database, or the SQL query being invalid in some way. big problem, probably needs dev attention.
                    MessageBox.Show($"Delete execution failed on table {tableName}. Sorry. PLease check the SQL is correct. Attempted query:\n{query}");
                }


            }



            return deleteSuccessful;

        }


        // ===============================================================================================================
        // STUDENT TRANSACTIONS

        public Student LoadStudent(int studentID)
        {
            // loads a student from the database with teh given ID, using dapper

            Student loadedStudent = null;
            using SqlConnection c = ConnectToDatabase();

            if (c != null && studentID > -1)
            {
                string query = $"SELECT {GetAliasedColumnsString(Tables.Student)} FROM {ConvertTableEnumToTableName(Tables.Student)} WHERE StudentID=@IDToFind";
                try
                { // to get a single item from the databse, we use QuerySingle. There are several versions, bu the basic one throws an exception when nothing is found
                    // so instead i use this SingleOfDefault thing, will lets the result be null. since this function is general purpose, we need need to allow a null return
                    // and then the callin gfunction can decide what to do about that
                    loadedStudent = c.QuerySingleOrDefault<Student>(query, new { IDToFind = studentID });
                    if (loadedStudent == null)
                        MessageBox.Show($"The App searched the databse for a student with ID {studentID} and did not find it.");
                }
                catch (Exception ex)
                { // either we can't convert the query result into a student, or the database couldnt be queries at all
                    MessageBox.Show("LoadStudent function couldn't query the database to look for a student, even though a database connection exists. Please check the Student" +
                        " tables are all present and correct (and that the App is connected to the correct database), and that the query being made returns rows that can be " +
                        $"translated into Student class objects. The query used was:\n{query}" +
                        $"\nAnd the exception raised was: {ex.Message}");
                }
            }
            else
            { // no connection or student id given isn't a plausible one
                Console.WriteLine("LoadStudent failed because connection is null or student ID is <0");
            }

            // will be a null return is something went wrong on the way.
            if (loadedStudent == null)
                return null; // making null return explicit in code - it's the Devin way.
            else
                return loadedStudent;
        }
        // ================================================================================================



    }
}
