namespace HigherOrLowerAcademyApp
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            StudentViewerButton = new Button();
            AdminGroupBox = new GroupBox();
            StudentOptionsGroupBox = new GroupBox();
            LoginGroupBox = new GroupBox();
            loginSuccessIcon = new PictureBox();
            editPasswordButton = new Button();
            loginButton = new Button();
            passwordTextBox = new TextBox();
            PasswordLabel = new Label();
            UserComboBoxLabel = new Label();
            UserComboBox = new ComboBox();
            AdminGroupBox.SuspendLayout();
            LoginGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)loginSuccessIcon).BeginInit();
            SuspendLayout();
            // 
            // StudentViewerButton
            // 
            StudentViewerButton.Location = new Point(24, 36);
            StudentViewerButton.Name = "StudentViewerButton";
            StudentViewerButton.Size = new Size(193, 63);
            StudentViewerButton.TabIndex = 0;
            StudentViewerButton.Text = "Open Student Viewer";
            StudentViewerButton.UseVisualStyleBackColor = true;
            StudentViewerButton.Click += StudentViewerButton_Click;
            // 
            // AdminGroupBox
            // 
            AdminGroupBox.Controls.Add(StudentViewerButton);
            AdminGroupBox.Location = new Point(30, 284);
            AdminGroupBox.Name = "AdminGroupBox";
            AdminGroupBox.Size = new Size(763, 125);
            AdminGroupBox.TabIndex = 1;
            AdminGroupBox.TabStop = false;
            AdminGroupBox.Text = "Admin Options";
            AdminGroupBox.Visible = false;
            // 
            // StudentOptionsGroupBox
            // 
            StudentOptionsGroupBox.Location = new Point(427, 40);
            StudentOptionsGroupBox.Name = "StudentOptionsGroupBox";
            StudentOptionsGroupBox.Size = new Size(366, 238);
            StudentOptionsGroupBox.TabIndex = 2;
            StudentOptionsGroupBox.TabStop = false;
            StudentOptionsGroupBox.Text = "Student Options";
            StudentOptionsGroupBox.Visible = false;
            // 
            // LoginGroupBox
            // 
            LoginGroupBox.Controls.Add(loginSuccessIcon);
            LoginGroupBox.Controls.Add(editPasswordButton);
            LoginGroupBox.Controls.Add(loginButton);
            LoginGroupBox.Controls.Add(passwordTextBox);
            LoginGroupBox.Controls.Add(PasswordLabel);
            LoginGroupBox.Controls.Add(UserComboBoxLabel);
            LoginGroupBox.Controls.Add(UserComboBox);
            LoginGroupBox.Location = new Point(30, 40);
            LoginGroupBox.Name = "LoginGroupBox";
            LoginGroupBox.Size = new Size(372, 238);
            LoginGroupBox.TabIndex = 3;
            LoginGroupBox.TabStop = false;
            LoginGroupBox.Text = "Please Log In";
            // 
            // loginSuccessIcon
            // 
            loginSuccessIcon.Image = (Image)resources.GetObject("loginSuccessIcon.Image");
            loginSuccessIcon.Location = new Point(152, 183);
            loginSuccessIcon.Name = "loginSuccessIcon";
            loginSuccessIcon.Size = new Size(52, 39);
            loginSuccessIcon.SizeMode = PictureBoxSizeMode.Zoom;
            loginSuccessIcon.TabIndex = 7;
            loginSuccessIcon.TabStop = false;
            loginSuccessIcon.Visible = false;
            // 
            // editPasswordButton
            // 
            editPasswordButton.Location = new Point(210, 183);
            editPasswordButton.Name = "editPasswordButton";
            editPasswordButton.Size = new Size(142, 39);
            editPasswordButton.TabIndex = 6;
            editPasswordButton.Text = "Edit My Password";
            editPasswordButton.UseVisualStyleBackColor = true;
            editPasswordButton.Click += editPasswordButton_Click;
            // 
            // loginButton
            // 
            loginButton.Location = new Point(24, 183);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(122, 39);
            loginButton.TabIndex = 5;
            loginButton.Text = "Log In";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(24, 135);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(328, 27);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.KeyDown += passwordTextBox_KeyDown;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.ForeColor = SystemColors.ControlText;
            PasswordLabel.Location = new Point(24, 112);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(111, 20);
            PasswordLabel.TabIndex = 3;
            PasswordLabel.Text = "Enter Password:";
            // 
            // UserComboBoxLabel
            // 
            UserComboBoxLabel.AutoSize = true;
            UserComboBoxLabel.Location = new Point(24, 32);
            UserComboBoxLabel.Name = "UserComboBoxLabel";
            UserComboBoxLabel.Size = new Size(85, 20);
            UserComboBoxLabel.TabIndex = 1;
            UserComboBoxLabel.Text = "Select User:";
            // 
            // UserComboBox
            // 
            UserComboBox.FormattingEnabled = true;
            UserComboBox.Location = new Point(24, 55);
            UserComboBox.Name = "UserComboBox";
            UserComboBox.Size = new Size(328, 28);
            UserComboBox.TabIndex = 0;
            UserComboBox.SelectedIndexChanged += UserComboBox_SelectedIndexChanged;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(832, 453);
            Controls.Add(LoginGroupBox);
            Controls.Add(StudentOptionsGroupBox);
            Controls.Add(AdminGroupBox);
            Name = "MainMenu";
            Text = "Higher or Lower Academy Main Menu";
            AdminGroupBox.ResumeLayout(false);
            LoginGroupBox.ResumeLayout(false);
            LoginGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)loginSuccessIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button StudentViewerButton;
        private GroupBox AdminGroupBox;
        private GroupBox StudentOptionsGroupBox;
        private GroupBox LoginGroupBox;
        private Label UserComboBoxLabel;
        private ComboBox UserComboBox;
        private Label PasswordLabel;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button editPasswordButton;
        private PictureBox loginSuccessIcon;
    }
}