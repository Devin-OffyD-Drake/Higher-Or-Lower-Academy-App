using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dapper;
using HigherOrLowerAcademyApp.DBRowClasses;
using Microsoft.Data.SqlClient;

namespace HigherOrLowerAcademyApp
{
    public partial class MainMenu : Form
    {
        public DBLiason dbL; // handles connection to the database, created on program startup and a copy is passed to all forms we'll open *** should always pass ref not the object?

        public User currentUser = null;
        public bool loggedIn = false;
        public MainMenu(DBLiason dbLNew = null)
        {
            InitializeComponent();

            if (dbL == null)
                dbL = dbLNew;

            PopulateUserComboBox(); // Handles the user combo box
        }

        private void ShowHideMainMenuSections()
        { // preparations on the main menu to be run when it opens, or needs refreshing. main function is to show only the relevant portion of the menues to certain users

            //MessageBox.Show("Menu show-hde functinality coming soon...");

            loginSuccessIcon.Visible = loggedIn;
            passwordTextBox.Visible = !loggedIn; // password disappears after login so its not sitting there telling everyone your passowrd. neato.
            PasswordLabel.Visible = !loggedIn;
            loginButton.Visible = !loggedIn; // might as well toggle the ubtton since we're toggling stuff - there is no logout, feature, you need to select another user to auto log out

            if (currentUser != null && loggedIn)
            {
                StudentOptionsGroupBox.Visible = currentUser.studentID != 0; // 0 (null in db) means its a non-student user, such as Admin
                AdminGroupBox.Visible = currentUser.isAdmin;
            }
            else
            {
                StudentOptionsGroupBox.Visible = false;
                AdminGroupBox.Visible = false;
            }

        }

        public void PopulateUserComboBox()
        {
            // gathers a list of all possible users and makes a list of them available in the user combo box, so they can be selected for logging in

            // the user list will be all students, plus any designated 'admin' accounts. The two types of user are mutually exclusive, admins can't take tests, students
            // can't access the admin controls

            // users will be defined in the database, where their passowrds are also stored. 
            // here, we load the rows from that table as a list of class objects, ready to populate our combo box
            SqlConnection c = dbL.ConnectToDatabase();
            string query = $"SELECT * FROM {dbL.ConvertTableEnumToTableName(DBLiason.Tables.Users)}";

            try
            {
                List<User> allUserList = c.Query<User>(query).AsList();             // dap it up

                UserComboBox.DataSource = allUserList;
                UserComboBox.DisplayMember = "userName"; // name of the member on the class that will be shown in the box
                UserComboBox.ValueMember = "userID"; // name of the memberon the class that represents the value of the section
            }
            catch
            {
                MessageBox.Show("Failed to load app users from database :(");
            }

        }

        public void RefreshUserComboBox()
        {
            // reloads the combo box data, then returns to the index it was previous on - for refreshing the data when teh DB is updated while the mennu is open
            int index = UserComboBox.SelectedIndex;
            PopulateUserComboBox();
            UserComboBox.SelectedIndex = index;
        }

        private void StudentViewerButton_Click(object sender, EventArgs e)
        { // simply open up the student viewer form (opens over the main menu, should be safe to use it alongside the menu form)
            StudentViewer s = new StudentViewer(dbL);
            s.Show();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            // if the user and passowrd match, we set the user as the curren tuser, and adjust the menu accordingly.

            bool pwCheckResult = false;
            bool firstTimeSetupNeeded = false;
            pwCheckResult = CheckUserNameAndPassword(out currentUser, out firstTimeSetupNeeded);  //  the current user member on the class object will be set by the out parameters. 

            if (pwCheckResult == false && !firstTimeSetupNeeded) // message supressed when PW setup required, as that fires its own message seperately
                MessageBox.Show("Cannot log in: User name and password combination is incorrect. Please try again.");

            loggedIn = pwCheckResult;

            // gonna refresh teh screen either way, to account for a login in state reverting to a non logged in state due to the controls being changed.
            ShowHideMainMenuSections(); // refresh the menu to show whatever the current user is supposed to see
        }

        private User GetSelectedUser()
        {
            // Helper function that returns the user selected in teh combo box, with some validation attached. returns null if it can't do it.
            object u = UserComboBox.SelectedItem;
            User returnUser = null;

            if (u != null)
            {
                // since the selected item is 'object' type, IN THEORY it might not be a user, if something went realy lwrong in development, so i'm gonna validate this
                try
                {
                    returnUser = (User)u;
                }
                catch
                {
                    returnUser = null;
                }
            }

            return returnUser;
        }

        private bool CheckUserNameAndPassword(out User selectedUser, out bool pwSetupRequired)
        {
            // looks at teh currently input username combo and passwrd entered, and makes sure the password on the User class matches what the user wrote. if so,
            // return true, otherwise, false
            // we willl have the selected user as an out parameters, as the calling code will probably wanna have this to do what it needs
            bool validLogin = false;

            string inputPassword = passwordTextBox.Text;
            selectedUser = GetSelectedUser(); // reutnrs null if they can't be found / no selected
            pwSetupRequired = false;

            if (selectedUser != null && !String.IsNullOrEmpty(inputPassword))
            {
                try
                {
                    // if the user doesn't have a password set in the database, it will not be possible for them to login.
                    // in this case, they will be asked to set their password before continuing (this funnction will terminate and they will 
                    /// have to login again).
                    pwSetupRequired = String.IsNullOrEmpty(selectedUser.userPassword); // its an out parameter so calling funtion knows we're going down this other rpute
                    if (pwSetupRequired)
                        MessageBox.Show("This user has no password setup. Please use the password editor feature, or contact an admin to get it set up. Then, return and log in again.");
                    else if (selectedUser.userPassword.Equals(inputPassword))
                        validLogin = true;
                }
                catch (Exception ex)
                {
                    // in case the object can't be converted to user, for whatever reason, probably something wrong at the development level
                    MessageBox.Show($"Exception raised while checking username and password combo: {ex.Message}");
                }
            }

            return validLogin;
        }

        private void editPasswordButton_Click(object sender, EventArgs e)
        {
            OpenPasswordEditor();
        }

        private void OpenPasswordEditor()
        {
            // simply opens the editor for the currently selected user. doing this logs the user out, also
            User u = GetSelectedUser();
            if (u != null)
            {
                UserPasswordEditor upe = new UserPasswordEditor(u, dbL, this);
                upe.Show();
                LogOutCurrentUser();
            }
            else
            {
                MessageBox.Show("No user is seleted, cannot open password editor.");
            }

        }

        private void LogOutCurrentUser()
        { // returns the menu to its initial state
            loggedIn = false;
            currentUser = null;
            passwordTextBox.Text = "";
            ShowHideMainMenuSections();
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login(); // tries to login when you press enter, NEATO
        }

        private void UserComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // when you pick a new user, we need to make sure we're logged out
            LogOutCurrentUser();
        }
    }
}
