namespace HigherOrLowerAcademyApp
{
    partial class UserPasswordEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            enterPasswordLabel = new Label();
            enterPasswordTextBox = new TextBox();
            newPasswordTextBox = new TextBox();
            enterNewPasswordLabel = new Label();
            passwordStatusLabel = new Label();
            SaveButton = new Button();
            CancelButton = new Button();
            userNameLabel = new Label();
            SuspendLayout();
            // 
            // enterPasswordLabel
            // 
            enterPasswordLabel.AutoSize = true;
            enterPasswordLabel.Location = new Point(12, 35);
            enterPasswordLabel.Name = "enterPasswordLabel";
            enterPasswordLabel.Size = new Size(144, 20);
            enterPasswordLabel.TabIndex = 0;
            enterPasswordLabel.Text = "Enter Your Password:";
            // 
            // enterPasswordTextBox
            // 
            enterPasswordTextBox.Location = new Point(12, 58);
            enterPasswordTextBox.Name = "enterPasswordTextBox";
            enterPasswordTextBox.Size = new Size(358, 27);
            enterPasswordTextBox.TabIndex = 1;
            enterPasswordTextBox.TextChanged += enterPasswordTextBox_TextChanged;
            // 
            // newPasswordTextBox
            // 
            newPasswordTextBox.Location = new Point(12, 149);
            newPasswordTextBox.Name = "newPasswordTextBox";
            newPasswordTextBox.Size = new Size(358, 27);
            newPasswordTextBox.TabIndex = 3;
            newPasswordTextBox.Visible = false;
            // 
            // enterNewPasswordLabel
            // 
            enterNewPasswordLabel.AutoSize = true;
            enterNewPasswordLabel.Location = new Point(12, 126);
            enterNewPasswordLabel.Name = "enterNewPasswordLabel";
            enterNewPasswordLabel.Size = new Size(178, 20);
            enterNewPasswordLabel.TabIndex = 2;
            enterNewPasswordLabel.Text = "Enter Your New Password:";
            enterNewPasswordLabel.Visible = false;
            // 
            // passwordStatusLabel
            // 
            passwordStatusLabel.AutoSize = true;
            passwordStatusLabel.Location = new Point(12, 88);
            passwordStatusLabel.Name = "passwordStatusLabel";
            passwordStatusLabel.Size = new Size(132, 20);
            passwordStatusLabel.TabIndex = 4;
            passwordStatusLabel.Text = "Password Incorrect";
            passwordStatusLabel.Visible = false;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(12, 192);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(137, 49);
            SaveButton.TabIndex = 5;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Visible = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(233, 192);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(137, 49);
            CancelButton.TabIndex = 6;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // userNameLabel
            // 
            userNameLabel.AutoSize = true;
            userNameLabel.Location = new Point(12, 9);
            userNameLabel.Name = "userNameLabel";
            userNameLabel.Size = new Size(239, 20);
            userNameLabel.TabIndex = 7;
            userNameLabel.Text = "Editing Password for [USER NAME]";
            // 
            // UserPasswordEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 253);
            ControlBox = false;
            Controls.Add(userNameLabel);
            Controls.Add(CancelButton);
            Controls.Add(SaveButton);
            Controls.Add(passwordStatusLabel);
            Controls.Add(newPasswordTextBox);
            Controls.Add(enterNewPasswordLabel);
            Controls.Add(enterPasswordTextBox);
            Controls.Add(enterPasswordLabel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserPasswordEditor";
            Text = "User Password Editor";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label enterPasswordLabel;
        private TextBox enterPasswordTextBox;
        private TextBox newPasswordTextBox;
        private Label enterNewPasswordLabel;
        private Label passwordStatusLabel;
        private Button SaveButton;
        private Button CancelButton;
        private Label userNameLabel;
    }
}