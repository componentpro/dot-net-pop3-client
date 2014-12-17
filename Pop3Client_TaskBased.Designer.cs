using System.Drawing;
using System.Windows.Forms;

namespace Pop3Samples
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tsbLogin = new System.Windows.Forms.ToolStripButton();
            this.tsbLogout = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbUndelete = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressCancelButton = new System.Windows.Forms.ToolStripSplitButton();
            this.client = new ComponentPro.Net.Mail.Pop3();
            this.listView = new System.Windows.Forms.ListView();
            this.listContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undeleteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMessageAsContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chId = new System.Windows.Forms.ColumnHeader();
            this.chUid = new System.Windows.Forms.ColumnHeader();
            this.chSize = new System.Windows.Forms.ColumnHeader();
            this.chFrom = new System.Windows.Forms.ColumnHeader();
            this.chSubject = new System.Windows.Forms.ColumnHeader();
            this.chReceived = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.sessionTimeoutTimer = new System.Windows.Forms.Timer(this.components);
            this.toolbarMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip.SuspendLayout();
            this.listContextMenuStrip.SuspendLayout();
            this.toolbarMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsbLogin
            // 
            this.tsbLogin.Image = ((System.Drawing.Image)(resources.GetObject("tsbLogin.Image")));
            this.tsbLogin.Name = "tsbLogin";
            this.tsbLogin.Size = new System.Drawing.Size(51, 49);
            this.tsbLogin.Text = "Connect";
            this.tsbLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbLogin.Click += new System.EventHandler(this.tsbLogin_Click);
            // 
            // tsbLogout
            // 
            this.tsbLogout.Enabled = false;
            this.tsbLogout.Image = ((System.Drawing.Image)(resources.GetObject("tsbLogout.Image")));
            this.tsbLogout.Name = "tsbLogout";
            this.tsbLogout.Size = new System.Drawing.Size(63, 49);
            this.tsbLogout.Text = "Disconnect";
            this.tsbLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbLogout.Visible = false;
            this.tsbLogout.Click += new System.EventHandler(this.tsbLogout_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.AutoSize = false;
            this.tsbRefresh.Enabled = false;
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(49, 49);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.AutoSize = false;
            this.tsbDelete.Enabled = false;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(49, 49);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbUndelete
            // 
            this.tsbUndelete.AutoSize = false;
            this.tsbUndelete.Enabled = false;
            this.tsbUndelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndelete.Image")));
            this.tsbUndelete.Name = "tsbUndelete";
            this.tsbUndelete.Size = new System.Drawing.Size(59, 49);
            this.tsbUndelete.Text = "Undelete";
            this.tsbUndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbUndelete.Click += new System.EventHandler(this.tsbUndelete_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.AutoSize = false;
            this.tsbOpen.Enabled = false;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(49, 49);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.AutoSize = false;
            this.tsbSaveAs.Enabled = false;
            this.tsbSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAs.Image")));
            this.tsbSaveAs.Name = "tsbSaveAs";
            this.tsbSaveAs.Size = new System.Drawing.Size(49, 49);
            this.tsbSaveAs.Text = "Save As";
            this.tsbSaveAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSaveAs.Click += new System.EventHandler(this.tsbSaveAs_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar,
            this.toolStripProgressLabel,
            this.toolStripProgressCancelButton});
            this.statusStrip.Location = new System.Drawing.Point(0, 527);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(708, 22);
            this.statusStrip.TabIndex = 130;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar.Visible = false;
            // 
            // toolStripProgressLabel
            // 
            this.toolStripProgressLabel.Name = "toolStripProgressLabel";
            this.toolStripProgressLabel.Size = new System.Drawing.Size(142, 17);
            this.toolStripProgressLabel.Text = "0/0 Message(s) downloaded";
            this.toolStripProgressLabel.Visible = false;
            // 
            // toolStripProgressCancelButton
            // 
            this.toolStripProgressCancelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripProgressCancelButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripProgressCancelButton.Image")));
            this.toolStripProgressCancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripProgressCancelButton.Name = "toolStripProgressCancelButton";
            this.toolStripProgressCancelButton.Size = new System.Drawing.Size(32, 20);
            this.toolStripProgressCancelButton.Text = "Cancel Downloading";
            this.toolStripProgressCancelButton.Visible = false;
            this.toolStripProgressCancelButton.ButtonClick += new System.EventHandler(this.toolStripProgressCancelButton_ButtonClick);
            // 
            // client
            // 
            this.client.MessageListProgress += new ComponentPro.Net.Mail.Pop3MessageListProgressEventHandler(this.client_MessageListProgress);
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chUid,
            this.chSize,
            this.chFrom,
            this.chSubject,
            this.chReceived,
            this.chStatus});
            this.listView.ContextMenuStrip = this.listContextMenuStrip;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 52);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(708, 475);
            this.listView.TabIndex = 133;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.lvm_SelectedIndexChanged);
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // listContextMenuStrip
            // 
            this.listContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openContextMenuItem,
            this.deleteContextMenuItem,
            this.undeleteContextMenuItem,
            this.saveMessageAsContextMenuItem});
            this.listContextMenuStrip.Name = "listContextMenuStrip";
            this.listContextMenuStrip.Size = new System.Drawing.Size(182, 114);
            // 
            // openContextMenuItem
            // 
            this.openContextMenuItem.Name = "openContextMenuItem";
            this.openContextMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openContextMenuItem.Text = "Open...";
            this.openContextMenuItem.Click += new System.EventHandler(this.openContextMenuItem_Click);
            // 
            // deleteContextMenuItem
            // 
            this.deleteContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteContextMenuItem.Image")));
            this.deleteContextMenuItem.Name = "deleteContextMenuItem";
            this.deleteContextMenuItem.Size = new System.Drawing.Size(181, 22);
            this.deleteContextMenuItem.Text = "Delete";
            this.deleteContextMenuItem.Click += new System.EventHandler(this.deleteContextMenuItem_Click);
            // 
            // undeleteContextMenuItem
            // 
            this.undeleteContextMenuItem.Name = "undeleteContextMenuItem";
            this.undeleteContextMenuItem.Size = new System.Drawing.Size(181, 22);
            this.undeleteContextMenuItem.Text = "Undelete";
            this.undeleteContextMenuItem.Click += new System.EventHandler(this.undeleteContextMenuItem_Click);
            // 
            // saveMessageAsContextMenuItem
            // 
            this.saveMessageAsContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveMessageAsContextMenuItem.Image")));
            this.saveMessageAsContextMenuItem.Name = "saveMessageAsContextMenuItem";
            this.saveMessageAsContextMenuItem.Size = new System.Drawing.Size(181, 22);
            this.saveMessageAsContextMenuItem.Text = "Save Message As...";
            this.saveMessageAsContextMenuItem.Click += new System.EventHandler(this.saveMessageAsContextMenuItem_Click);
            // 
            // chId
            // 
            this.chId.Text = "Id";
            this.chId.Width = 40;
            // 
            // chUid
            // 
            this.chUid.Text = "Uid";
            this.chUid.Width = 120;
            // 
            // chSize
            // 
            this.chSize.Text = "Size";
            this.chSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chSize.Width = 80;
            // 
            // chFrom
            // 
            this.chFrom.Text = "From";
            this.chFrom.Width = 100;
            // 
            // chSubject
            // 
            this.chSubject.Text = "Subject";
            this.chSubject.Width = 120;
            // 
            // chReceived
            // 
            this.chReceived.Text = "Received";
            this.chReceived.Width = 100;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            // 
            // sessionTimeoutTimer
            // 
            this.sessionTimeoutTimer.Tick += new System.EventHandler(this.sessionTimeoutTimer_Tick);
            // 
            // toolbarMain
            // 
            this.toolbarMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLogin,
            this.tsbLogout,
            this.tsbRefresh,
            this.toolStripSeparator1,
            this.tsbOpen,
            this.tsbSaveAs,
            this.tsbDelete,
            this.tsbUndelete});
            this.toolbarMain.Location = new System.Drawing.Point(0, 0);
            this.toolbarMain.Name = "toolbarMain";
            this.toolbarMain.Size = new System.Drawing.Size(708, 52);
            this.toolbarMain.TabIndex = 134;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 549);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolbarMain);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.Text = "Ultimate POP3 Client Demo";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.listContextMenuStrip.ResumeLayout(false);
            this.toolbarMain.ResumeLayout(false);
            this.toolbarMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsbLogin;
        private System.Windows.Forms.ToolStripButton tsbLogout;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbUndelete;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private ComponentPro.Net.Mail.Pop3 client;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chUid;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.ColumnHeader chFrom;
        private System.Windows.Forms.ColumnHeader chSubject;
        private System.Windows.Forms.ColumnHeader chReceived;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProgressLabel;
        private System.Windows.Forms.ToolStripSplitButton toolStripProgressCancelButton;
        private System.Windows.Forms.ContextMenuStrip listContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undeleteContextMenuItem;
        private System.Windows.Forms.ToolStripButton tsbSaveAs;
        private System.Windows.Forms.ToolStripMenuItem saveMessageAsContextMenuItem;
        private System.Windows.Forms.Timer sessionTimeoutTimer;
        private System.Windows.Forms.ToolStrip toolbarMain;
        private ToolStripSeparator toolStripSeparator1;
    }
}

