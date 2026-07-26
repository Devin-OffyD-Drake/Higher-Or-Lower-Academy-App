using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using HigherOrLowerAcademyApp.DBRowClasses;
using Dapper;

namespace HigherOrLowerAcademyApp
{
    public partial class StudentAddMenu : Form
    {
        private bool editMode = false; // when true, we're editing an existing record, instead of writing a new one
        private int editStudentID = -1; // when editing, store the ID in the form so its easy to lookup for the update at the end
        // *** if each instance of the form is its own object, then i guess we don't need to reset these on close. but is that always true, or
        // is there a way for the same form object to be used for multiple runs, and hence we should have some kinda initialiation to keep it clean?

        public DBLiason dbL; // we can pass this in from the main student viewer, or some other place it's already been made bfore this menu appears, nee?


        public StudentViewer studViewForm; // try and keep a refernce to the form that opened this one, so we can talk to it, such as refreshing it on the way back

        public StudentAddMenu()
        {
            InitializeComponent();
        }

        private void StudentAddMenu_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private bool SubmitStudent()
        {
            // attemts to add the new student to the database, or updates edited student record. returns true is successful, a false if nay

            // =======================================================================================================
            // INPUT VALIDATION OR AUTOEDITS 
            // before we get into the main action, we should do validation on the boxes to make sure it's all good
            // this is done with an application system - see the details of the called fucntion here for more details,
            // but it basically refuses progress if something is wrong, or might quietly make edits itself
            DBClassFactory.StudentApplication sApp = ValidateAndUpdateInputs();
            if (sApp.applicationValid == false)
                return false; // stop here and report that we failed
            // ======================================================================================================

            // # PREPARE QUERY TO WRITE OR UPDATE OUR DATABASE

            // inputs are now assumed valid, we are clear to write them to the database

            // --------------------------------------------------------------------------------------------------------
            // CONNECTION CHECK / ESTABLISH A CONNECTION
            using SqlConnection con = dbL.ConnectToDatabase();

            // --------------------------------------------------------------------------------------------------------
            // TRANSACTION WITH DATABASE

            // this will have 2 main routes, one for making a new record, and another for updating an existing record
            // we will construct sql queries using the 'dapper parametisation' method, where we put refernces in the sql to
            // variables, then pass the variables in upon execution. this is better than a pure string interpolation to make 
            // the sql query have the values hardcoded into it, because of sql injections and various other misc issues, so this 
            // is just the superior method, even though it's a but more writing.
            string sql = "";
            int rowsEffected = 0;
            bool submitSuccessful = true; // for our return at the end, assumes true and trips to false if checks fail

            // i am paranoid about running thing son teh DB, so i will look for exceptions here

            try
            {
                if (editMode && editStudentID != -1) // either update the student we're editring, or make a new one, both read the data from the studentapplication in teh same way
                {
                    // UPDATE
                    sql = "UPDATE Students SET StudentName = @Name, StudentNotes = @Notes, BlessedByTheGods = @Blessed FROM Students WHERE StudentID=@StudentID";

                    // now pass in all the args. this is done with a new {} list-looking format kinda thing, for whatever reason. gotta pas in the right order
                    rowsEffected = con.Execute(sql, new { Name = sApp.studentName, Notes = sApp.studentNotes, Blessed = sApp.blessedByGods, StudentID = editStudentID });
                }
                else
                {
                    // INSERT - similar to above but we do the insert syntax instead, different because sql hates yhou
                    sql = "INSERT INTO Students (StudentName, StudentNotes, BlessedByTheGods) VALUES (@Name, @Notes, @Blessed)";
                    rowsEffected = con.Execute(sql, new { Name = sApp.studentName, Notes = sApp.studentNotes, Blessed = sApp.blessedByGods });
                }

                // POST EXECUTION CHECK
                // since the execute returns rows effected, we can see if it actually did anything. i.e. it might have no effet without raising an exception, so we will have a specical catch-style block here
                if (rowsEffected == 0)
                {
                    submitSuccessful = false;
                    MessageBox.Show("Submission to database was made, but no rows were updated/added. Something is pretty wrong then, huh? You probably been to contact OffyD about this. ***");
                    // *** we need to be able to provide some helpful advice, but iam niot sure what would even cause this issue, or if there is anything
                    // you can do other than get the developer to try and fix it. it is possible to look at the con and see error messages printed on it ***
                }
                // there is also the ultra-outside possibility that 2 rows were updated due to an ID conflict or something! this isn't really a failure for this method's purposes,
                // but it means trouble, and needs urgent attention.
                else if (rowsEffected > 1)
                {
                    MessageBox.Show($"Submission to the database was made, however more than 1 record has been added / edited. " +
                        $"This is unexpected behaviour, indicative of database issues, such as student ID number being non-unique. " +
                        $"Please contact DBA OffyD and beg him to fix this.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Transaction with Higher or Lower Academy Database has failed.\nError Message: {ex.Message}");
                submitSuccessful = false;
            }

            // --------------------------------------------------------------------------------------------------------

            return submitSuccessful;
        }

        private DBClassFactory.StudentApplication ValidateAndUpdateInputs()
        {
            // validation is being outsourced to the relevant part of the DBClassFactory, where manual created of classes normlaly used for representing DB rows will
            /// be centrally handled. th eprocess will return any error messages we need, and a copy of teh student, in case we need to update our fields form it, or 
            // use it to read our values for the next step. which we should, so i don't know why I wrote 'in case'.

            // this functio can actually change the value of inputs according to any atuomatic schemes the ClassFactory might apply. so we will also read back data
            // from teh application, for safety

            // -------------------------------------------------
            // SUBMIT AN APPLICATION
            // the way student creation works is that we first send an application to the factory, and all the validation will happen over there
            DBClassFactory.StudentApplication sApp = new DBClassFactory.StudentApplication
                (
                this.NameInputBox.Text,
                this.StudentNotesBox.Text,
                this.blessedByGodsCheckbox.Checked
                );

            sApp = DBClassFactory.ProcessStudentApplication(sApp); //  magic happens in there
            // ---------------------------------------------------
            // READ BACK DATA
            // i am going to allow the factory to alter the data if it wants, and this can be done regardless of validation state, in principal. any changes
            // made will be reflected right back in the form controls. we also need to pass the application back to the callin gcode so it can use updated
            // values in any tudent creation that ensures.

            NameInputBox.Text = sApp.studentName;
            StudentNotesBox.Text = sApp.studentNotes;
            blessedByGodsCheckbox.Checked = sApp.blessedByGods;
            // ----------------------------------------------------

            // CONCLUSION - read out message attached to application, if needed
            if (!sApp.applicationValid || !String.IsNullOrWhiteSpace(sApp.validationMessage)) // for safety, show error if anything got in there, even when validation passes
                MessageBox.Show(sApp.validationMessage);

            return sApp;
        }


        public void LoadStudentToEdit(Student studentToEdit)
        {
            // this one lets us bring in an existing student, instead of loading empty. we can then overwrite / update that student when sbumitting.
            editMode = true;
            editStudentID = studentToEdit.studentID; // storing the ID so its easy to lookup for the update later

            // set the values from the student
            this.NameInputBox.Text = studentToEdit.studentName;
            this.StudentNotesBox.Text = studentToEdit.notes;
            this.blessedByGodsCheckbox.Checked = studentToEdit.blessedByGods;

        }

        private void StudentAddMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // clean up stuff here maybe ***?
        }

        private void DoneButton_Click(object sender, EventArgs e)
        { // basically the done button will try to transact what's in the input controls with the datbase. validation and checks will happen, of course.

            // --------------------------------------
            // SUBMISSION ATTEMPT
            bool submissionSuccess = false;
            try
            {
                submissionSuccess = SubmitStudent();
            }
            catch
            {
                MessageBox.Show("Submission failed. :(");
            }

            // --------------------------------------
            // CLOSE ON SUCCESSFUL SUBMISSION

            // then we also close this form, which will return us to the main student viewer form, which itsself will need updating
            if (submissionSuccess) // don't need to close if the submission failed though, stays open and user might have messages to read and work to do
            {
                // i would like it if when you refresh the list, it automatically reselects the row the student was on, possibly including scrolling 
                // to its position ***

                studViewForm.GenerateAndShowStudentList();
                // *** would like to update th status bar on the the viewer form with a success message
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // simply throws out everything, not much to do here
            this.Close();
        }
    }

}
