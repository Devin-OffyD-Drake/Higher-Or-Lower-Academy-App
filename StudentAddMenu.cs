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

        private void blessedByGodsLabel_Click(object sender, EventArgs e)
        {

        }

        private bool SubmitNewStudent()
        {
            // attemts to add the new student to the database, returns true is successful, a false if nay

            // *** COMING SOON
            // =======================================================================================================
            // INPUT VALIDATION OR AUTOEDITS 
            // before we get into the main action, we should do validation on the boxes to make sure it's all good
            if (!ValidateInputs())
                return false; // stop here and report that we failed
            // ======================================================================================================

            // # PREPARE QUERY TO WRITE OR UPDATE OUR DATABASE

            // inputs are now assumed valid, we are clear to write them to the database

            // --------------------------------------------------------------------------------------------------------
            // CONNECTION CHECK / ESTABLISH A CONNECTION
            using SqlConnection con = dbL.ConnectToDatabase();

            // --------------------------------------------------------------------------------------------------------

            // this will have 2 main routes, one for making a new record, and another for updating an existing record
            // we will construct sql queries using the 'dapper parametisation' method, where we put refernces in the sql to
            // variables, then pass the variables in upon execution. this is better than a pure string interpolation to make 
            // the sql query have the values hardcoded into it, because of sql injections and various other misc issues, so this 
            // is just the superior method, even though it's a but more writing.
            string sql = "";
            int rowsEffected = 0;
            string notesBox = StudentNotesBox.Text; // see note below on what i did this for some reason ***

            // *** check best practice for adding try and catch to these execite, or shall we rely on then just returning 0 to tell us they didn't work, and 
            // not worry too much about what the problem was, at this level?

            if (editMode && editStudentID!=-1)
            {
                // UPDATE
                sql = "UPDATE Students SET StudentName = @Name, StudentNotes = @Notes, BlessedByTheGods = @Blessed FROM Students WHERE StudentID=@StudentID";

                // now pass in all the args. this is done with a new {} list-looking format kinda thing, for whatever reason. gotta pas in the right order
                rowsEffected = con.Execute(sql, new { NameInputBox.Text, notesBox, blessedByGodsCheckbox.Checked, editStudentID });
                // strange issue in that you can't have several things call .text within th e{} list for reasons I don['t understand, so i read the notes seperately, whatver... ***

            }
            else
            {
                // INSERT - similar to above but we do the insert syntax instead, different because sql hates yhou
                sql = "INSERT INTO Students (StudentName, StudentNotes, BlessedByTheGods) VALUES (@Name, @Notes, @Blessed)";

                rowsEffected = con.Execute(sql, new { NameInputBox.Text, notesBox, blessedByGodsCheckbox.Checked });
            }

            // --------------------------------------------------------------------------------------------------------

            // POST EXECUTION CHECK
            bool submitSuccessful = true;
            // since the execute returns rows effected, we can see if it actually did anything
            if (rowsEffected == 0)
            {
                submitSuccessful = false;
                MessageBox.Show("Submission to database was made, but no rows were updated/added. Something is pretty wrong then, huh? You probably been to contact OffyD about this. ***");
                // *** we need to be able to provide some helpful advice, but iam niot sure what would even cause this issue, or if there is anything
                // you can do other than get the developer to try and fix it
            }

            // ---------------------------------------------------------------------------------------------------------
            return submitSuccessful;
        }

        private bool ValidateInputs()
        {
            // this function will look over all the user-editable inputs and run any particular validation needed on them, returning true is all passed.
            // allong the way it will generate an error message for us to have helpful feedback on what to do if validation fails.
            // if we're doing any auto-replacements, we can even do that here too.

            bool validationSuccessful = true;
            string errorMessage = default;
            // -------------------------------------------------
            // NAME
            string name = this.NameInputBox.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                // it's nothing, or just loads of spaces / non-character characters
                errorMessage += "You need to enter the student's name. ";
                validationSuccessful = false;
            }
            else if(String.Equals(name,"Devin"))
            {
                errorMessage += "The student cannot be named Devin. There is only 1 Devin, and this student is not them. ";
                validationSuccessful=false;
            }

            // -------------------------------------------------


            // CONCLUSION
            if (!validationSuccessful || !String.IsNullOrWhiteSpace(errorMessage)) // for safety, show error if anything got in there, even when validation passes
                MessageBox.Show(errorMessage);

            return validationSuccessful;
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
    }

}
