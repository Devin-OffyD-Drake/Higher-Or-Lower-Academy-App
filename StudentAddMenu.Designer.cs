namespace HigherOrLowerAcademyApp
{
    partial class StudentAddMenu
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
            NameInputBox = new TextBox();
            NameInputLabel = new Label();
            AddStudentInfoLabel = new Label();
            blessedByGodsCheckbox = new CheckBox();
            blessedByGodsLabel = new Label();
            StudentNotesBox = new RichTextBox();
            StudentNotesLabel = new Label();
            DoneButton = new Button();
            CancelButton = new Button();
            SuspendLayout();
            // 
            // NameInputBox
            // 
            NameInputBox.Font = new Font("Segoe UI", 10.8F);
            NameInputBox.Location = new Point(121, 40);
            NameInputBox.Margin = new Padding(4);
            NameInputBox.Name = "NameInputBox";
            NameInputBox.Size = new Size(477, 31);
            NameInputBox.TabIndex = 0;
            // 
            // NameInputLabel
            // 
            NameInputLabel.AutoSize = true;
            NameInputLabel.Font = new Font("Segoe UI", 10.8F);
            NameInputLabel.ImageAlign = ContentAlignment.MiddleRight;
            NameInputLabel.Location = new Point(18, 43);
            NameInputLabel.Margin = new Padding(4, 0, 4, 0);
            NameInputLabel.Name = "NameInputLabel";
            NameInputLabel.Size = new Size(95, 25);
            NameInputLabel.TabIndex = 1;
            NameInputLabel.Text = "Full Name:";
            NameInputLabel.TextAlign = ContentAlignment.MiddleRight;
            NameInputLabel.Click += label1_Click;
            // 
            // AddStudentInfoLabel
            // 
            AddStudentInfoLabel.AutoSize = true;
            AddStudentInfoLabel.Location = new Point(18, 9);
            AddStudentInfoLabel.Margin = new Padding(4, 0, 4, 0);
            AddStudentInfoLabel.Name = "AddStudentInfoLabel";
            AddStudentInfoLabel.Size = new Size(762, 25);
            AddStudentInfoLabel.TabIndex = 2;
            AddStudentInfoLabel.Text = "Add new Student details, then click Done. This Student will be immediately available for testing.";
            // 
            // blessedByGodsCheckbox
            // 
            blessedByGodsCheckbox.AutoSize = true;
            blessedByGodsCheckbox.Location = new Point(688, 68);
            blessedByGodsCheckbox.Name = "blessedByGodsCheckbox";
            blessedByGodsCheckbox.Size = new Size(18, 17);
            blessedByGodsCheckbox.TabIndex = 3;
            blessedByGodsCheckbox.UseVisualStyleBackColor = true;
            // 
            // blessedByGodsLabel
            // 
            blessedByGodsLabel.AutoSize = true;
            blessedByGodsLabel.Font = new Font("Segoe UI", 10.8F);
            blessedByGodsLabel.ImageAlign = ContentAlignment.MiddleRight;
            blessedByGodsLabel.Location = new Point(622, 40);
            blessedByGodsLabel.Margin = new Padding(4, 0, 4, 0);
            blessedByGodsLabel.Name = "blessedByGodsLabel";
            blessedByGodsLabel.Size = new Size(147, 25);
            blessedByGodsLabel.TabIndex = 4;
            blessedByGodsLabel.Text = "Blessed by Gods:";
            blessedByGodsLabel.TextAlign = ContentAlignment.MiddleRight;
            blessedByGodsLabel.Click += blessedByGodsLabel_Click;
            // 
            // StudentNotesBox
            // 
            StudentNotesBox.Location = new Point(121, 79);
            StudentNotesBox.Name = "StudentNotesBox";
            StudentNotesBox.Size = new Size(477, 146);
            StudentNotesBox.TabIndex = 5;
            StudentNotesBox.Text = "";
            // 
            // StudentNotesLabel
            // 
            StudentNotesLabel.AutoSize = true;
            StudentNotesLabel.Font = new Font("Segoe UI", 10.8F);
            StudentNotesLabel.ImageAlign = ContentAlignment.MiddleRight;
            StudentNotesLabel.Location = new Point(18, 82);
            StudentNotesLabel.Margin = new Padding(4, 0, 4, 0);
            StudentNotesLabel.Name = "StudentNotesLabel";
            StudentNotesLabel.Size = new Size(63, 25);
            StudentNotesLabel.TabIndex = 6;
            StudentNotesLabel.Text = "Notes:";
            StudentNotesLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(644, 102);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(101, 54);
            DoneButton.TabIndex = 7;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(644, 171);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(101, 54);
            CancelButton.TabIndex = 8;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            // 
            // StudentAddMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 253);
            ControlBox = false;
            Controls.Add(CancelButton);
            Controls.Add(DoneButton);
            Controls.Add(StudentNotesLabel);
            Controls.Add(StudentNotesBox);
            Controls.Add(blessedByGodsLabel);
            Controls.Add(blessedByGodsCheckbox);
            Controls.Add(AddStudentInfoLabel);
            Controls.Add(NameInputLabel);
            Controls.Add(NameInputBox);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "StudentAddMenu";
            Text = "Add Student";
            FormClosing += StudentAddMenu_FormClosing;
            Load += StudentAddMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox NameInputBox;
        private Label NameInputLabel;
        private Label AddStudentInfoLabel;
        private CheckBox blessedByGodsCheckbox;
        private Label blessedByGodsLabel;
        private RichTextBox StudentNotesBox;
        private Label StudentNotesLabel;
        private Button DoneButton;
        private Button CancelButton;
    }
}