using Dapper;
using HigherOrLowerAcademyApp.DBRowClasses;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace HigherOrLowerAcademyApp
{
    // THINGS TO DO
    // *** some kinda 'export to excel or csv' type button
    // ability to detect what it selected and open a version of the new student menu to edit it, or a dialog to let you delete a student
    // *** the winforms limitatoin where you can't resize the form kinda SUCKS. is ther a way around it? can i port this whole thin gint oa better framework
    // with automatic dynamic UI? i guess winforms isn't for this and I should forget it for now, but i'm still mad.

    public partial class StudentViewer : Form
    {
        public DBLiason dbL; // handles connection to teh database

        public StudentViewer(DBLiason dbLNew = null)
        {
            InitializeComponent();

            // if a dbLiasion was passed in the argument, we need to attach that now before we do anything else
            if (dbLNew != null)
                dbL = dbLNew;

            // main action happens in here, getting data and putting it in the datagrid view
            GenerateAndShowStudentList();

        }

        public void GenerateAndShowStudentList()
        {
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

            // i want to force otes to be at the end
            dgv.Columns["notes"].DisplayIndex = dgv.Columns.Count - 1;
        }

        private void NewStudentButton_Click(object sender, EventArgs e)
        { // simply opens the student add meny to handle the creation of a new student
            InitialiseAndOpenStudentAddMenu();
        }

        private void InitialiseAndOpenStudentAddMenu(Student editStudent = null)
        { // opens a smaller form above the student viewer, with input boxes for making a new student. the update will be handled by that class.
            // openal arg lets it pass a student to edit
            StudentAddMenu studentAddMenu = new StudentAddMenu();
            studentAddMenu.dbL = dbL; // each new form we open gets the same refernce to our database liason, as this may hold important info for database interaction (actaully doesn'mt at time of writing, this is future proofing)
            studentAddMenu.studViewForm = this; // it needs to know the oibject that called it, so it can talk back e.g. refresh student viewer after something is added

            if (editStudent != null)
                studentAddMenu.LoadStudentToEdit(editStudent); // this handles all the changes needed to the form to proceed in edit mode

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
            // *** WE WILL likely change this to load a view ith some logic in it so that the columns include reporting via aggregate functions, and the view ill be saved in
            // the db, so a more complex query will appear here
            string query = $"SELECT {dbL.GetAliasedColumnsString(DBLiason.Tables.Student)} FROM Students";
            try
            {
                return c.Query<Student>(query).AsList();
                // so it tries to convert the return from the query into Student objects, and AsList lets us then use that as a list in c#
                // *** validation and handling and alternate returns go here
            }
            catch
            {
                // COULDN'T LOAD, PANIC TIME, ALERT USER, log error, allow alternative behaviour or whathaveyou

                string noStudentListMsg = "Failed to load student list from database. Check Error log???***";
                this.statusStrip.Text = noStudentListMsg;
                this.statusStrip.Show(); // necessary*** can't really see how statuc strip works, we'l need a status strip update at some point?
                MessageBox.Show(noStudentListMsg);
                return []; // this strange thing automatically returns an empty version of the function's type. ie here it returns an empty list of students. neato!
                throw; // thish throw wil make visual sutdio show the exceptin with details and point out the line, really sueful! 
                       // whether it's okay to use this in a real build, or if it behaves differenlty there... ***


            }

        }

        private void EditStudentButton_Click(object sender, EventArgs e)
        {
            // when this button si clicked we need do a) ascertain which student is currently selected in teh data grid view (ie. what row is in focus), if any
            // and then b) run the studentaddmenu in edit mode, with any relevant setup.

            Student editStudent = GetCurrentlyFocusedStudent(); // returns null if something fails
            if (editStudent != null)
                InitialiseAndOpenStudentAddMenu(editStudent); // the rest is handled in here thanks to this overload arg
            else
            {
                // could raise messages here, but the deeper code layers probably already gave out message for whatever failed along the way.
                // *** perhaps a SOUND EFFECT?!
            }

        }

        private Student GetCurrentlyFocusedStudent()
        {
            // GETTING THE CURRENTLY SELECTED STUDENT - returns null is failed

            // the sutdent viewers rows will probably not be Student objects forever, as in, it may up displaying a View will aloadof other stuff that isn't in the 
            // student class, so we'll have to approach it differently. to be as general as possible, all I will do is assume that the first column of the data grid view
            // is ALWAYS the student ID number. (the column may be hidden in practice?). so we will try to extract that int from the current row, and use that to pull
            // a student from the database, then use that sutdent as the editStudent who we pass on to the StudentAddMenu. let's go
            // ------------------------------------------------------------------------
            // GET THE ID NUMBER
            int studentID = -1;
            try
            { // the grid view has properties we can drill into that should get us whatever is written in column 0 of the current row
                DataGridViewRow dgvr = this.StudentDataGridView.CurrentRow;
                object cellValue;
                if (dgvr != null && dgvr.Cells.Count > 0) // being cautious as the currentrow can be null (nothing selected) or you might somehow select a row with no cells
                {
                    cellValue = this.StudentDataGridView.CurrentRow.Cells[0].Value; // doesn't come with a particular data type

                    if (cellValue != null)
                        studentID = Convert.ToInt32(cellValue); // hopefuly possible, hence the try-catch since we can't guarantee something int-able is in that cell
                }

            }
            catch (InvalidCastException ex)
            {
                // it's probably gonna be this one
                MessageBox.Show($"Could not read the Student ID from the currently selected row, because whatever is in the first column cannot be converted to an Int32." +
                    $"/nError Message: {ex.Message}");
                return null; // give up
            }
            catch
            {
                MessageBox.Show("Reading of Student ID from the currently selected row has failed.");
                return null;
            }

            // another catch for if the process failed in a non-exception raising way
            if (studentID == -1)
            {
                MessageBox.Show("Cannot read Student ID from selected row. Please make sure a row is selected and contains a student." +
                    "\nIf this error persists, there is likely an error in the database or this app that needs correcting, please contact support.");
                return null;
            }

            // if we're here, we have a valid student ID to try, let's go ahead.

            // ----------------------------------------------------------------------
            // GET THE STUDENT FROM THE DATABASE

            // the Dl liason will handl eloading from the db for us, in simple cases like this
            Student loadedStudent = dbL.LoadStudent(studentID); // can return null if it failed

            if (loadedStudent == null)
            {
                MessageBox.Show("Cannot find currently selected student.");
                return null; // yes i know we can return loadedStudent here, but i like it be to explicit in the code when stuff returns null
            }
            else
                return loadedStudent;

            // ------------------------------------------------------------------------

        }

        private Student ConfirmSelectedStudentForDelete()
        {
            // we need the ability to delete a student from the DB. this will be done by just gettin gthe curretly selected one, asking for confirmation on delete,
            // then, for reasons of easier reporting, the delet itself will be done outside this function (it's a one line call to DBLiason).
            // RETURNS either the student we need to delete, for handy passing to delete function, or null, if we're not going ahead

            Student studentToDelete = GetCurrentlyFocusedStudent();

            if (studentToDelete != null) // i won't have a message if the student can' tbe sleected, as the prior selection function has it own ones already
            {
                // let's confirm we wanna do this
                DialogResult choice = MessageBox.Show($"Are you sure you want to delete {studentToDelete.studentName} from the database? " +
                                 $"This is irreversable, and will delete other data associated with them, such as records of their Higher or Lower games.",
                                 "Delete Warning",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                // the windows message boxes return a 'Dialog result' type of object, with a different one for each possible thing you can click in a dialog box
                if (choice.Equals(DialogResult.No))
                { // nulling the return value will make downstream code cancel the delete.
                    studentToDelete = null;
                }
            }

            return studentToDelete;
        }

        private void deleteStudentButton_Click(object sender, EventArgs e)
        { // Decide who to delete, confirm it, then do it, then refresh the list to reflect the change

            bool deleted = false;
            Student studentToDelete = ConfirmSelectedStudentForDelete(); // gets selected student, shows dialog to confirm. returns null if no student found or NO was pressed

            if (studentToDelete != null)
            {
                deleted = dbL.DeleteDBRow(DBLiason.Tables.Student, studentToDelete.studentID); // does the business
                                                                                               // if the delete failed, this function will have already called errors out
            }

            if (deleted)
            {
                GenerateAndShowStudentList(); // refreshg list with the deleted thing missing, hopefully
                MessageBox.Show("Delete Successful.");
            }
        }
    }
}
