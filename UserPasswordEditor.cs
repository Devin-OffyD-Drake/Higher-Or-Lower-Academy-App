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
    public partial class UserPasswordEditor : Form
    {
        public User? user;
        public DBLiason? dbL;
        public MainMenu mainMenuForm;

        public UserPasswordEditor(User inputUser, DBLiason passedDBLiason, MainMenu mainMenuCallingThis)
        {
            InitializeComponent();
            dbL = passedDBLiason;
            mainMenuForm = mainMenuCallingThis; // need this ref stored so we can updat ethe main meny on close if update is successful

            user = inputUser; // pass in the user this form pertains to when constructiong. we cann't continue if this fails. might as well fail if no db ilason too, since we can't write new pw anyway then
            if (user == null || dbL == null)
                this.Close();

            userNameLabel.Text = $"Editing password for {user.userName}";

            // there are 2 modes of use for this form. 1 is that the user has no passowrd, and is setting it for the first time. the other is that they are editing their PW.
            // we will have different visability states for each case.
            if (String.IsNullOrEmpty(inputUser.userPassword))
            { // they need to set tehir PW for the first time.
                EnterFirstTimeMode();
            }
            else
            {
                // edit mode - the form's default properties are those appropriate for the first entry point of edit mode, so we don't adjust anything
            }
        }

        private void EnterFirstTimeMode()
        {
            // the user has no PW, so they don' tneed to enter it to proceed at first. they simply submit a password and save it, subject to validation.
            // to prepare forthis, we set the visibilty of teh relevant controls.

            enterPasswordLabel.Visible = false;
            enterPasswordTextBox.Visible = false;
            passwordStatusLabel.Visible = false;

            enterNewPasswordLabel.Visible = true;
            newPasswordTextBox.Visible = true;
            SaveButton.Visible = true;
        }

        private bool CheckEnteredPassword()
        {
            // if the password written in the input box is the same as the user password, return true.
            bool correct = false;
            if (String.Equals(enterPasswordTextBox.Text, user.userPassword))
                correct = true;

            passwordStatusLabel.Visible = !correct; // message appears when wrong, disappears when true
            return correct;
        }

        private void enterPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            bool checkResult = CheckEnteredPassword(); // we can progress if the text changed to teh correct passowrd, otherwise it just warns its wrong and awaits another change

            // controls for next step appear when check is good
            enterNewPasswordLabel.Visible = checkResult;
            newPasswordTextBox.Visible = checkResult;
            SaveButton.Visible = checkResult;
        }

        private bool SaveNewPassword(string password = "")
        {
            // commits what's in the new password textbox to the user entry in teh database. valiation happens elsewhere, this functino assu,es its good to go

            using SqlConnection c = dbL.ConnectToDatabase();
            bool success = false;

            if (password == "") // shold be passed by the calling function, but for whatever reason i want this to be optional and to have it look up what the user put in if we can't pass it
                password = newPasswordTextBox.Text;

            // simeple update query, expecting only a single row to update
            string query = "UPDATE Users SET UserPassword = @UserPassword WHERE UserID=@UserID";
            try
            {
                int rowsAffected = c.Execute(query, new { UserPassword = password, UserID = user.userId });

                if (rowsAffected > 0)
                    success = true; // could complain if the amount if more than 1 too, but lets believe in teh starts that the UserID really is unique
                else if (rowsAffected == 0) // probably means user id wasn't in the database, not sure if this can happen, but its an issue
                    MessageBox.Show($"Tried to update password for user with ID {user.userId} but no records were affected, this user record may be missing :(");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database transaction to update user password failed. The query was:\n{query}.\nThe exception was:{ex.Message}");
            }

            return success;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // all changes ignored
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        { // validates, tries to sae, reports result, probably closes the form on success

            // -------------------------------------------------------------------------------
            // VALIDATION GOES HERE *** LOL
            bool valid = true;
            string passwordToSave = newPasswordTextBox.Text;

            if (String.IsNullOrWhiteSpace(passwordToSave)) // lets have one little validation for now, as an example
            {
                MessageBox.Show("Enter a password with some non-whitespace characters.");
                valid = false;
            }

            // ---------------------------------------------------------------------------------
            // if we're valid by now, let's try to save and report back
            if (valid)
            {
                bool saved = SaveNewPassword(passwordToSave);
                if (saved)
                {
                    MessageBox.Show($"Save successful. Your password is now {passwordToSave}");
                    // we will need to update the calling form - we look at the ser combo box, save teh index, refresh it, then reutrn to that index.
                    // this needs to be doine on teh main meny class, so we just call something here
                    mainMenuForm.RefreshUserComboBox();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Save failed, your password has not been updated. Please contact an admin.");
                }
            }
        }
    }
}
