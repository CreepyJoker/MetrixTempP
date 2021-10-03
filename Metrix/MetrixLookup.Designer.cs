using System.Windows.Forms;

namespace Metrix
{
    partial class MetrixLookup
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
            this.metrixLookupTxtbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.selectChangeGameFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectChangeGameFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mlk_searchbtn1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.labelLatestUpdate = new System.Windows.Forms.Label();
            this.checkForBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metrixLookupTxtbox
            // 
            this.metrixLookupTxtbox.Location = new System.Drawing.Point(218, 68);
            this.metrixLookupTxtbox.Name = "metrixLookupTxtbox";
            this.metrixLookupTxtbox.Size = new System.Drawing.Size(308, 20);
            this.metrixLookupTxtbox.TabIndex = 0;
            this.metrixLookupTxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.metrixLookupTxtbox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lookup User ( Name - Realm )";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Region is auto-detected by your regional game data.";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectChangeGameFolderToolStripMenuItem1,
            this.checkForBackupToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // selectChangeGameFolderToolStripMenuItem1
            // 
            this.selectChangeGameFolderToolStripMenuItem1.Name = "selectChangeGameFolderToolStripMenuItem1";
            this.selectChangeGameFolderToolStripMenuItem1.Size = new System.Drawing.Size(158, 20);
            this.selectChangeGameFolderToolStripMenuItem1.Text = "Select / Change Game Folder";
            this.selectChangeGameFolderToolStripMenuItem1.Click += new System.EventHandler(this.selectChangeGameFolderToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem1});
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.checkForUpdatesToolStripMenuItem.Text = "Help";
            // 
            // checkForUpdatesToolStripMenuItem1
            // 
            this.checkForUpdatesToolStripMenuItem1.Name = "checkForUpdatesToolStripMenuItem1";
            this.checkForUpdatesToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.checkForUpdatesToolStripMenuItem1.Text = "Check for updates";
            // 
            // selectChangeGameFolderToolStripMenuItem
            // 
            this.selectChangeGameFolderToolStripMenuItem.Name = "selectChangeGameFolderToolStripMenuItem";
            this.selectChangeGameFolderToolStripMenuItem.Size = new System.Drawing.Size(158, 20);
            this.selectChangeGameFolderToolStripMenuItem.Text = "Select / Change Game Folder";
            this.selectChangeGameFolderToolStripMenuItem.Click += new System.EventHandler(this.selectChangeGameFolderToolStripMenuItem_Click);
            // 
            // mlk_searchbtn1
            // 
            this.mlk_searchbtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mlk_searchbtn1.Location = new System.Drawing.Point(532, 65);
            this.mlk_searchbtn1.Name = "mlk_searchbtn1";
            this.mlk_searchbtn1.Size = new System.Drawing.Size(75, 24);
            this.mlk_searchbtn1.TabIndex = 4;
            this.mlk_searchbtn1.Text = "Search";
            this.mlk_searchbtn1.UseVisualStyleBackColor = true;
            this.mlk_searchbtn1.Click += new System.EventHandler(this.mlk_searchbtn1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(581, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Update Personal Stats";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelLatestUpdate
            // 
            this.labelLatestUpdate.AutoSize = true;
            this.labelLatestUpdate.Location = new System.Drawing.Point(13, 137);
            this.labelLatestUpdate.Name = "labelLatestUpdate";
            this.labelLatestUpdate.Size = new System.Drawing.Size(190, 13);
            this.labelLatestUpdate.TabIndex = 6;
            this.labelLatestUpdate.Text = "Latest update for personal data: 0:00:0";
            // 
            // checkForBackupToolStripMenuItem
            // 
            this.checkForBackupToolStripMenuItem.Name = "checkForBackupToolStripMenuItem";
            this.checkForBackupToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.checkForBackupToolStripMenuItem.Text = "Restore Data";
            this.checkForBackupToolStripMenuItem.Click += new System.EventHandler(this.checkForBackupToolStripMenuItem_Click);
            // 
            // MetrixLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 187);
            this.Controls.Add(this.labelLatestUpdate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mlk_searchbtn1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metrixLookupTxtbox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MetrixLookup";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metrix - Mythic Plus Performance & Stats Tracker";
            this.Load += new System.EventHandler(this.MetrixLookup_Load);
            this.Shown += new System.EventHandler(this.MetrixLookup_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox metrixLookupTxtbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem1;
        private System.Windows.Forms.Button mlk_searchbtn1;
        private System.Windows.Forms.ToolStripMenuItem selectChangeGameFolderToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem selectChangeGameFolderToolStripMenuItem1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelLatestUpdate;
        private ToolStripMenuItem checkForBackupToolStripMenuItem;
    }
}

