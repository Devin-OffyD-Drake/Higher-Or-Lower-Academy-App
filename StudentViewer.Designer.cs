namespace HigherOrLowerAcademyApp
{
    partial class StudentViewer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            StudentDataGridView = new DataGridView();
            statusStrip = new StatusStrip();
            menuStripStudentViewer = new MenuStrip();
            backToolStripMenuItem = new ToolStripMenuItem();
            returnToMainMenuToolStripMenuItem = new ToolStripMenuItem();
            quitHoLAcademyAppToolStripMenuItem = new ToolStripMenuItem();
            NewStudentButton = new Button();
            ((System.ComponentModel.ISupportInitialize)StudentDataGridView).BeginInit();
            menuStripStudentViewer.SuspendLayout();
            SuspendLayout();
            // 
            // StudentDataGridView
            // 
            StudentDataGridView.AllowUserToAddRows = false;
            StudentDataGridView.AllowUserToDeleteRows = false;
            StudentDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            StudentDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            StudentDataGridView.Location = new Point(13, 32);
            StudentDataGridView.Margin = new Padding(4);
            StudentDataGridView.Name = "StudentDataGridView";
            StudentDataGridView.ReadOnly = true;
            StudentDataGridView.RowHeadersVisible = false;
            StudentDataGridView.RowHeadersWidth = 51;
            StudentDataGridView.Size = new Size(956, 355);
            StudentDataGridView.TabIndex = 0;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Location = new Point(0, 481);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 18, 0);
            statusStrip.Size = new Size(982, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip";
            // 
            // menuStripStudentViewer
            // 
            menuStripStudentViewer.ImageScalingSize = new Size(20, 20);
            menuStripStudentViewer.Items.AddRange(new ToolStripItem[] { backToolStripMenuItem });
            menuStripStudentViewer.Location = new Point(0, 0);
            menuStripStudentViewer.Name = "menuStripStudentViewer";
            menuStripStudentViewer.Padding = new Padding(8, 2, 0, 2);
            menuStripStudentViewer.Size = new Size(982, 28);
            menuStripStudentViewer.TabIndex = 3;
            menuStripStudentViewer.Text = "Menu";
            // 
            // backToolStripMenuItem
            // 
            backToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { returnToMainMenuToolStripMenuItem, quitHoLAcademyAppToolStripMenuItem });
            backToolStripMenuItem.Name = "backToolStripMenuItem";
            backToolStripMenuItem.Size = new Size(56, 24);
            backToolStripMenuItem.Text = "&Main";
            // 
            // returnToMainMenuToolStripMenuItem
            // 
            returnToMainMenuToolStripMenuItem.Name = "returnToMainMenuToolStripMenuItem";
            returnToMainMenuToolStripMenuItem.Size = new Size(249, 26);
            returnToMainMenuToolStripMenuItem.Text = "&Return To Main Menu";
            // 
            // quitHoLAcademyAppToolStripMenuItem
            // 
            quitHoLAcademyAppToolStripMenuItem.Name = "quitHoLAcademyAppToolStripMenuItem";
            quitHoLAcademyAppToolStripMenuItem.Size = new Size(249, 26);
            quitHoLAcademyAppToolStripMenuItem.Text = "&Quit HoL Academy App";
            // 
            // NewStudentButton
            // 
            NewStudentButton.Location = new Point(13, 422);
            NewStudentButton.Margin = new Padding(4);
            NewStudentButton.Name = "NewStudentButton";
            NewStudentButton.Size = new Size(214, 36);
            NewStudentButton.TabIndex = 4;
            NewStudentButton.Text = "Add New Student";
            NewStudentButton.UseVisualStyleBackColor = true;
            NewStudentButton.Click += NewStudentButton_Click;
            // 
            // StudentViewer
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 503);
            Controls.Add(NewStudentButton);
            Controls.Add(statusStrip);
            Controls.Add(menuStripStudentViewer);
            Controls.Add(StudentDataGridView);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStripStudentViewer;
            Margin = new Padding(4);
            Name = "StudentViewer";
            Text = "HoL Academy Student Roster";
            ((System.ComponentModel.ISupportInitialize)StudentDataGridView).EndInit();
            menuStripStudentViewer.ResumeLayout(false);
            menuStripStudentViewer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView StudentDataGridView;
        private StatusStrip statusStrip;
        private MenuStrip menuStripStudentViewer;
        private ToolStripMenuItem backToolStripMenuItem;
        private ToolStripMenuItem returnToMainMenuToolStripMenuItem;
        private ToolStripMenuItem quitHoLAcademyAppToolStripMenuItem;
        private Button NewStudentButton;
    }
}
