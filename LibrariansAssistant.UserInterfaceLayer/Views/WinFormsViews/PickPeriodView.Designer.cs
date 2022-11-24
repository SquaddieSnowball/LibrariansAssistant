namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews
{
    partial class PickPeriodView
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelActions = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanelPeriod = new System.Windows.Forms.TableLayoutPanel();
            this.labelStartPeriod = new System.Windows.Forms.Label();
            this.labelEndPeriod = new System.Windows.Forms.Label();
            this.dateTimePickerStartPeriod = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndPeriod = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanelColumn = new System.Windows.Forms.TableLayoutPanel();
            this.labelColumn = new System.Windows.Forms.Label();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelActions.SuspendLayout();
            this.tableLayoutPanelPeriod.SuspendLayout();
            this.tableLayoutPanelColumn.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelActions, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelStatus, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelPeriod, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelColumn, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(342, 223);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelActions
            // 
            this.tableLayoutPanelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutPanelActions.ColumnCount = 3;
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelActions.Controls.Add(this.buttonCancel, 2, 0);
            this.tableLayoutPanelActions.Controls.Add(this.buttonConfirm, 0, 0);
            this.tableLayoutPanelActions.Controls.Add(this.buttonReset, 1, 0);
            this.tableLayoutPanelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanelActions.Location = new System.Drawing.Point(3, 166);
            this.tableLayoutPanelActions.Name = "tableLayoutPanelActions";
            this.tableLayoutPanelActions.RowCount = 1;
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelActions.Size = new System.Drawing.Size(336, 54);
            this.tableLayoutPanelActions.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.buttonCancel.Location = new System.Drawing.Point(226, 7);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 3, 3, 10);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 32);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonConfirm.AutoSize = true;
            this.buttonConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.buttonConfirm.Location = new System.Drawing.Point(16, 7);
            this.buttonConfirm.Margin = new System.Windows.Forms.Padding(3, 3, 2, 10);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(94, 32);
            this.buttonConfirm.TabIndex = 0;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = false;
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonReset.AutoSize = true;
            this.buttonReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.buttonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.buttonReset.Location = new System.Drawing.Point(121, 7);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(94, 32);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = false;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.labelStatus.Location = new System.Drawing.Point(143, 12);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(56, 23);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "Status";
            // 
            // tableLayoutPanelPeriod
            // 
            this.tableLayoutPanelPeriod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutPanelPeriod.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelPeriod.ColumnCount = 2;
            this.tableLayoutPanelPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPeriod.Controls.Add(this.labelStartPeriod, 0, 0);
            this.tableLayoutPanelPeriod.Controls.Add(this.labelEndPeriod, 1, 0);
            this.tableLayoutPanelPeriod.Controls.Add(this.dateTimePickerStartPeriod, 0, 1);
            this.tableLayoutPanelPeriod.Controls.Add(this.dateTimePickerEndPeriod, 1, 1);
            this.tableLayoutPanelPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanelPeriod.Location = new System.Drawing.Point(20, 43);
            this.tableLayoutPanelPeriod.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.tableLayoutPanelPeriod.Name = "tableLayoutPanelPeriod";
            this.tableLayoutPanelPeriod.RowCount = 2;
            this.tableLayoutPanelPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelPeriod.Size = new System.Drawing.Size(302, 77);
            this.tableLayoutPanelPeriod.TabIndex = 2;
            // 
            // labelStartPeriod
            // 
            this.labelStartPeriod.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelStartPeriod.AutoSize = true;
            this.labelStartPeriod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.labelStartPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.labelStartPeriod.Location = new System.Drawing.Point(31, 5);
            this.labelStartPeriod.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.labelStartPeriod.Name = "labelStartPeriod";
            this.labelStartPeriod.Size = new System.Drawing.Size(88, 20);
            this.labelStartPeriod.TabIndex = 0;
            this.labelStartPeriod.Text = "Start period";
            // 
            // labelEndPeriod
            // 
            this.labelEndPeriod.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelEndPeriod.AutoSize = true;
            this.labelEndPeriod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.labelEndPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.labelEndPeriod.Location = new System.Drawing.Point(185, 5);
            this.labelEndPeriod.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.labelEndPeriod.Name = "labelEndPeriod";
            this.labelEndPeriod.Size = new System.Drawing.Size(82, 20);
            this.labelEndPeriod.TabIndex = 1;
            this.labelEndPeriod.Text = "End period";
            // 
            // dateTimePickerStartPeriod
            // 
            this.dateTimePickerStartPeriod.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dateTimePickerStartPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStartPeriod.Location = new System.Drawing.Point(10, 40);
            this.dateTimePickerStartPeriod.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.dateTimePickerStartPeriod.MaxDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStartPeriod.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStartPeriod.Name = "dateTimePickerStartPeriod";
            this.dateTimePickerStartPeriod.Size = new System.Drawing.Size(130, 27);
            this.dateTimePickerStartPeriod.TabIndex = 2;
            this.dateTimePickerStartPeriod.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePickerEndPeriod
            // 
            this.dateTimePickerEndPeriod.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dateTimePickerEndPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerEndPeriod.Location = new System.Drawing.Point(161, 40);
            this.dateTimePickerEndPeriod.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.dateTimePickerEndPeriod.MaxDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndPeriod.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndPeriod.Name = "dateTimePickerEndPeriod";
            this.dateTimePickerEndPeriod.Size = new System.Drawing.Size(130, 27);
            this.dateTimePickerEndPeriod.TabIndex = 3;
            this.dateTimePickerEndPeriod.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // tableLayoutPanelColumn
            // 
            this.tableLayoutPanelColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutPanelColumn.ColumnCount = 2;
            this.tableLayoutPanelColumn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelColumn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelColumn.Controls.Add(this.labelColumn, 0, 0);
            this.tableLayoutPanelColumn.Controls.Add(this.comboBoxColumn, 1, 0);
            this.tableLayoutPanelColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelColumn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanelColumn.Location = new System.Drawing.Point(3, 126);
            this.tableLayoutPanelColumn.Name = "tableLayoutPanelColumn";
            this.tableLayoutPanelColumn.RowCount = 1;
            this.tableLayoutPanelColumn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelColumn.Size = new System.Drawing.Size(336, 34);
            this.tableLayoutPanelColumn.TabIndex = 3;
            // 
            // labelColumn
            // 
            this.labelColumn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelColumn.AutoSize = true;
            this.labelColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.labelColumn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.labelColumn.Location = new System.Drawing.Point(66, 8);
            this.labelColumn.Margin = new System.Windows.Forms.Padding(3, 3, 5, 0);
            this.labelColumn.Name = "labelColumn";
            this.labelColumn.Size = new System.Drawing.Size(63, 20);
            this.labelColumn.TabIndex = 0;
            this.labelColumn.Text = "Column:";
            // 
            // comboBoxColumn
            // 
            this.comboBoxColumn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBoxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxColumn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Location = new System.Drawing.Point(139, 5);
            this.comboBoxColumn.Margin = new System.Windows.Forms.Padding(5, 5, 3, 3);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(130, 28);
            this.comboBoxColumn.TabIndex = 1;
            // 
            // PickPeriodView
            // 
            this.AcceptButton = this.buttonConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(342, 223);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PickPeriodView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pick period";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.tableLayoutPanelActions.ResumeLayout(false);
            this.tableLayoutPanelActions.PerformLayout();
            this.tableLayoutPanelPeriod.ResumeLayout(false);
            this.tableLayoutPanelPeriod.PerformLayout();
            this.tableLayoutPanelColumn.ResumeLayout(false);
            this.tableLayoutPanelColumn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelActions;
        private Button buttonConfirm;
        private Button buttonReset;
        private Label labelStatus;
        private TableLayoutPanel tableLayoutPanelPeriod;
        private Label labelStartPeriod;
        private Label labelEndPeriod;
        private DateTimePicker dateTimePickerStartPeriod;
        private DateTimePicker dateTimePickerEndPeriod;
        private Button buttonCancel;
        private TableLayoutPanel tableLayoutPanelColumn;
        private Label labelColumn;
        private ComboBox comboBoxColumn;
    }
}