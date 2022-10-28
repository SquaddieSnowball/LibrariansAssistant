namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews
{
    partial class SettingsView
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
            this.menuStripSettings = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.additionalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelActions = new System.Windows.Forms.TableLayoutPanel();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelSettings = new System.Windows.Forms.TableLayoutPanel();
            this.menuStripSettings.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelActions.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripSettings
            // 
            this.menuStripSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.menuStripSettings.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem,
            this.additionalToolStripMenuItem});
            this.menuStripSettings.Location = new System.Drawing.Point(0, 0);
            this.menuStripSettings.Name = "menuStripSettings";
            this.menuStripSettings.Padding = new System.Windows.Forms.Padding(10);
            this.menuStripSettings.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStripSettings.Size = new System.Drawing.Size(482, 44);
            this.menuStripSettings.TabIndex = 0;
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.connectionToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // additionalToolStripMenuItem
            // 
            this.additionalToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.additionalToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.additionalToolStripMenuItem.Name = "additionalToolStripMenuItem";
            this.additionalToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.additionalToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.additionalToolStripMenuItem.Text = "Additional";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelActions, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxSettings, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 44);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(482, 104);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // tableLayoutPanelActions
            // 
            this.tableLayoutPanelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutPanelActions.ColumnCount = 2;
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActions.Controls.Add(this.buttonApply, 0, 0);
            this.tableLayoutPanelActions.Controls.Add(this.buttonCancel, 1, 0);
            this.tableLayoutPanelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanelActions.Location = new System.Drawing.Point(3, 47);
            this.tableLayoutPanelActions.Name = "tableLayoutPanelActions";
            this.tableLayoutPanelActions.RowCount = 1;
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelActions.Size = new System.Drawing.Size(476, 54);
            this.tableLayoutPanelActions.TabIndex = 0;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonApply.AutoSize = true;
            this.buttonApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.buttonApply.Location = new System.Drawing.Point(134, 7);
            this.buttonApply.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(94, 32);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.buttonCancel.Location = new System.Drawing.Point(248, 7);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(10, 3, 3, 10);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 32);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.groupBoxSettings.Controls.Add(this.tableLayoutPanelSettings);
            this.groupBoxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.groupBoxSettings.Location = new System.Drawing.Point(20, 3);
            this.groupBoxSettings.Margin = new System.Windows.Forms.Padding(20, 3, 20, 10);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Padding = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.groupBoxSettings.Size = new System.Drawing.Size(442, 31);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings group name";
            // 
            // tableLayoutPanelSettings
            // 
            this.tableLayoutPanelSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutPanelSettings.ColumnCount = 1;
            this.tableLayoutPanelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanelSettings.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanelSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelSettings.Name = "tableLayoutPanelSettings";
            this.tableLayoutPanelSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanelSettings.RowCount = 1;
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSettings.Size = new System.Drawing.Size(436, 3);
            this.tableLayoutPanelSettings.TabIndex = 0;
            // 
            // SettingsView
            // 
            this.AcceptButton = this.buttonApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(482, 148);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.menuStripSettings);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStripSettings;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.menuStripSettings.ResumeLayout(false);
            this.menuStripSettings.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelActions.ResumeLayout(false);
            this.tableLayoutPanelActions.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStripSettings;
        private ToolStripMenuItem connectionToolStripMenuItem;
        private ToolStripMenuItem additionalToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelActions;
        private Button buttonApply;
        private Button buttonCancel;
        private GroupBox groupBoxSettings;
        private TableLayoutPanel tableLayoutPanelSettings;
    }
}