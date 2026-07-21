using Microsoft.Data.SqlClient;
using Dapper;
using System.ComponentModel.Design.Serialization;
using HigherOrLowerAcademyApp.DBRowClasses;

namespace HigherOrLowerAcademyApp
{
    // THINGS TO DO
    // *** some kinda 'export to excel or csv' type button
    // ability to detect what it selected and open a version of the new student menu to edit it, or a dialog to let you delete a student


    public partial class StudentViewer : Form
    {
        public DBLiason dbL; // holds the open connection to the database, among other things

        public StudentViewer(DBLiason dbLNew = null)
        {
            InitializeComponent();

            // if a dbLiasion was passed in the argument, we need to attach that now before we do anything else
            if (dbLNew != null)
                dbL = dbLNew;

            // we nneed to load some student data from sql server. what exactly we load isn't cler right now, might be a viewin teh end, 
            // *** but as amn example for the time being, let's try to load up teh sutdent table to display it

            List<Student> loadedStudentsList = GetStudentList();
            // *** do something with it to show in data grid view, and see what we can configure about it

            DataGridView dgv = this.StudentDataGridView;

            // ---------------------------------------------------------------
            // SORTABILITY DEBACLE
            /* SCRREW THIS COPOLIT IS LYING ABOUT THE DGV ALLOWING SORTING NEVER MIND
            // we could just sat the datasouce to be the list we loaded, however to be able to sort the list by its columns later,
            // it needs to be a 'BindingSource' type of datasource. ther is some CS magic in teh bacjgroud, but the gist og it is...
            BindingSource bs = new BindingSource();
            bs.DataSource = loadedStudentsList; 
            dgv.DataSource = bs; // so we set he bindingsource datasource to our thing, then the gridview datasource to the binding source.
            // that enabled hte magic that allows sorting, probably at the cost of speed and such.
            
            // also, when you show a list in a data grid view, eh ability to sort by the columns is disabled by default. so we shall enabled it
            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = DataGridViewColumnSortMode.Automatic;*/
            // -------------------------------------------------------------------

            dgv.DataSource = loadedStudentsList;

            // the column names will be the proeprty names by default, so let's make them something better
            // note vs studio is mad about these lines because the index of columns isn't guaranteed to exist, but in practice,
            // with how strict this sql setup is, it actually is. just don't hardcode it wrong, basically, and it should never change.
            dgv.Columns["studentID"].HeaderText = "ID";
            dgv.Columns["studentName"].HeaderText = "Name";
            dgv.Columns["blessedByGods"].HeaderText = "Blessed?";
            dgv.Columns["notes"].HeaderText = "Notes";



        }

        private void NewStudentButton_Click(object sender, EventArgs e)
        { // opens a smaller form above the student viewer, with input boxes for making a new student. the update will be handled by that class.
            StudentAddMenu studentAddMenu = new StudentAddMenu();
            studentAddMenu.dbL = dbL; // each new form we open gets the same refernce to our database liason, whic holds the open connect for general use
            studentAddMenu.Show();
        }

        private List<Student> GetStudentList()
        {
            // TO INTERACT WITH A DB WE NEED a) the Microsoft.Data.SqlClient NuGet package, and b) the Dapper NuGet package installed, and invoked with using at the top

            // connection should be availabel already. quick exit from this function if it isnt already around, won't bother trying to make it again here, neater to have one place only
            using SqlConnection c = dbL.ConnectToDatabase();
            // not eyou don't actaully need to declare a scope for using - you can just invoke it, and it will automatically close the connect at the end of hte CURRENt scope.
            // which is margially easier to type even if it someties means its open margially longer tha needed. its neater, yo. also don't not use it or it might not close,
            // and tha tis bad practice, error prone, and such.

            if (c == null)
            {
                MessageBox.Show("Tried to load student list but the connection to the database was not available (null).");
                return [];
            }

            // with dapper the process of interacting with teh connection is imsplified. we don't even have to officially open teh connectin, it seems. we can return
            // a set query directly into a list of objects, as long as those objects have properties that map to the column names. wish me luck then.
            // IMPORTANTLY you need to alias each column with the name of the property on teh custom class, so it knows where to put it
            // THIS WON'T UPDATE IF YOU EVER CHANGE THE PROPERTY NAME. ALSO THE PROETY NEEDS THA TGETTER SETTER THING.
            string query = "SELECT StudentID AS studentID, StudentName AS studentName, StudentNotes AS notes, BlessedByTheGods AS blessedByGods FROM Students";
            try
            {
                return c.Query<Student>(query).AsList();
                // so it tries to convert the return from the query into Student objects, and AsList lets us then use that as a list in c#
                // *** validation and handling and alternate returns go here
            }
            catch
            {
                // *** COULDN@T LOADm PANIC TIME, ALERT USER, log error, allow alternative behaviour or whathaveyou

                string noStudentListMsg = "Failed to load student list from database. Check Error log???***";
                this.statusStrip.Text = noStudentListMsg;
                this.statusStrip.Show(); // necessary***?
                MessageBox.Show(noStudentListMsg);
                return []; // this strange thing automatically returns an empty version of the function's type. ie here it returns an empty list of students. neato!
                throw; // thish throw wil make visual sutdio show the exceptin with details and point out the line, really sueful! 
                        // whether it's okay to use this in a real build, or if it behaves differenlty there... ***


            }
   
        }
    }
}
