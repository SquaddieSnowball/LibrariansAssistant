namespace LibrariansAssistant.UI.Views
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
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelActions = new TableLayoutPanel();
            buttonCancel = new Button();
            buttonConfirm = new Button();
            buttonReset = new Button();
            labelStatus = new Label();
            tableLayoutPanelPeriod = new TableLayoutPanel();
            labelStartPeriod = new Label();
            labelEndPeriod = new Label();
            dateTimePickerStartPeriod = new DateTimePicker();
            dateTimePickerEndPeriod = new DateTimePicker();
            tableLayoutPanelColumn = new TableLayoutPanel();
            labelColumn = new Label();
            comboBoxColumn = new ComboBox();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelActions.SuspendLayout();
            tableLayoutPanelPeriod.SuspendLayout();
            tableLayoutPanelColumn.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = Color.FromArgb(30, 30, 30);
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelActions, 0, 3);
            tableLayoutPanelMain.Controls.Add(labelStatus, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelPeriod, 0, 1);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelColumn, 0, 2);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.ForeColor = Color.FromArgb(230, 230, 230);
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 4;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanelMain.Size = new Size(342, 223);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelActions
            // 
            tableLayoutPanelActions.BackColor = Color.FromArgb(30, 30, 30);
            tableLayoutPanelActions.ColumnCount = 3;
            tableLayoutPanelActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanelActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanelActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanelActions.Controls.Add(buttonCancel, 2, 0);
            tableLayoutPanelActions.Controls.Add(buttonConfirm, 0, 0);
            tableLayoutPanelActions.Controls.Add(buttonReset, 1, 0);
            tableLayoutPanelActions.Dock = DockStyle.Fill;
            tableLayoutPanelActions.ForeColor = Color.FromArgb(230, 230, 230);
            tableLayoutPanelActions.Location = new Point(3, 166);
            tableLayoutPanelActions.Name = "tableLayoutPanelActions";
            tableLayoutPanelActions.RowCount = 1;
            tableLayoutPanelActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelActions.Size = new Size(336, 54);
            tableLayoutPanelActions.TabIndex = 0;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Left;
            buttonCancel.AutoSize = true;
            buttonCancel.BackColor = Color.FromArgb(30, 30, 30);
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.ForeColor = Color.FromArgb(230, 230, 230);
            buttonCancel.Location = new Point(226, 7);
            buttonCancel.Margin = new Padding(2, 3, 3, 10);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(94, 32);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            // 
            // buttonConfirm
            // 
            buttonConfirm.Anchor = AnchorStyles.Right;
            buttonConfirm.AutoSize = true;
            buttonConfirm.FlatStyle = FlatStyle.Flat;
            buttonConfirm.ForeColor = Color.FromArgb(230, 230, 230);
            buttonConfirm.Location = new Point(16, 7);
            buttonConfirm.Margin = new Padding(3, 3, 2, 10);
            buttonConfirm.Name = "buttonConfirm";
            buttonConfirm.Size = new Size(94, 32);
            buttonConfirm.TabIndex = 0;
            buttonConfirm.Text = "Confirm";
            buttonConfirm.UseVisualStyleBackColor = false;
            // 
            // buttonReset
            // 
            buttonReset.Anchor = AnchorStyles.Bottom;
            buttonReset.AutoSize = true;
            buttonReset.BackColor = Color.FromArgb(30, 30, 30);
            buttonReset.FlatStyle = FlatStyle.Flat;
            buttonReset.ForeColor = Color.FromArgb(230, 230, 230);
            buttonReset.Location = new Point(121, 7);
            buttonReset.Margin = new Padding(3, 3, 3, 15);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(94, 32);
            buttonReset.TabIndex = 1;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = false;
            // 
            // labelStatus
            // 
            labelStatus.Anchor = AnchorStyles.Bottom;
            labelStatus.AutoSize = true;
            labelStatus.BackColor = Color.FromArgb(30, 30, 30);
            labelStatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelStatus.ForeColor = Color.FromArgb(230, 230, 230);
            labelStatus.Location = new Point(143, 12);
            labelStatus.Margin = new Padding(3, 0, 3, 5);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(56, 23);
            labelStatus.TabIndex = 1;
            labelStatus.Text = "Status";
            // 
            // tableLayoutPanelPeriod
            // 
            tableLayoutPanelPeriod.BackColor = Color.FromArgb(30, 30, 30);
            tableLayoutPanelPeriod.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanelPeriod.ColumnCount = 2;
            tableLayoutPanelPeriod.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelPeriod.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelPeriod.Controls.Add(labelStartPeriod, 0, 0);
            tableLayoutPanelPeriod.Controls.Add(labelEndPeriod, 1, 0);
            tableLayoutPanelPeriod.Controls.Add(dateTimePickerStartPeriod, 0, 1);
            tableLayoutPanelPeriod.Controls.Add(dateTimePickerEndPeriod, 1, 1);
            tableLayoutPanelPeriod.Dock = DockStyle.Fill;
            tableLayoutPanelPeriod.ForeColor = Color.FromArgb(230, 230, 230);
            tableLayoutPanelPeriod.Location = new Point(20, 43);
            tableLayoutPanelPeriod.Margin = new Padding(20, 3, 20, 3);
            tableLayoutPanelPeriod.Name = "tableLayoutPanelPeriod";
            tableLayoutPanelPeriod.RowCount = 2;
            tableLayoutPanelPeriod.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanelPeriod.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanelPeriod.Size = new Size(302, 77);
            tableLayoutPanelPeriod.TabIndex = 2;
            // 
            // labelStartPeriod
            // 
            labelStartPeriod.Anchor = AnchorStyles.Bottom;
            labelStartPeriod.AutoSize = true;
            labelStartPeriod.BackColor = Color.FromArgb(30, 30, 30);
            labelStartPeriod.ForeColor = Color.FromArgb(230, 230, 230);
            labelStartPeriod.Location = new Point(31, 5);
            labelStartPeriod.Margin = new Padding(3, 0, 3, 5);
            labelStartPeriod.Name = "labelStartPeriod";
            labelStartPeriod.Size = new Size(88, 20);
            labelStartPeriod.TabIndex = 0;
            labelStartPeriod.Text = "Start period";
            // 
            // labelEndPeriod
            // 
            labelEndPeriod.Anchor = AnchorStyles.Bottom;
            labelEndPeriod.AutoSize = true;
            labelEndPeriod.BackColor = Color.FromArgb(30, 30, 30);
            labelEndPeriod.ForeColor = Color.FromArgb(230, 230, 230);
            labelEndPeriod.Location = new Point(185, 5);
            labelEndPeriod.Margin = new Padding(3, 0, 3, 5);
            labelEndPeriod.Name = "labelEndPeriod";
            labelEndPeriod.Size = new Size(82, 20);
            labelEndPeriod.TabIndex = 1;
            labelEndPeriod.Text = "End period";
            // 
            // dateTimePickerStartPeriod
            // 
            dateTimePickerStartPeriod.Anchor = AnchorStyles.Bottom;
            dateTimePickerStartPeriod.Format = DateTimePickerFormat.Short;
            dateTimePickerStartPeriod.Location = new Point(10, 40);
            dateTimePickerStartPeriod.Margin = new Padding(3, 3, 3, 9);
            dateTimePickerStartPeriod.MaxDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dateTimePickerStartPeriod.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dateTimePickerStartPeriod.Name = "dateTimePickerStartPeriod";
            dateTimePickerStartPeriod.Size = new Size(130, 27);
            dateTimePickerStartPeriod.TabIndex = 2;
            dateTimePickerStartPeriod.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePickerEndPeriod
            // 
            dateTimePickerEndPeriod.Anchor = AnchorStyles.Bottom;
            dateTimePickerEndPeriod.Format = DateTimePickerFormat.Short;
            dateTimePickerEndPeriod.Location = new Point(161, 40);
            dateTimePickerEndPeriod.Margin = new Padding(3, 3, 3, 9);
            dateTimePickerEndPeriod.MaxDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dateTimePickerEndPeriod.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dateTimePickerEndPeriod.Name = "dateTimePickerEndPeriod";
            dateTimePickerEndPeriod.Size = new Size(130, 27);
            dateTimePickerEndPeriod.TabIndex = 3;
            dateTimePickerEndPeriod.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // tableLayoutPanelColumn
            // 
            tableLayoutPanelColumn.BackColor = Color.FromArgb(30, 30, 30);
            tableLayoutPanelColumn.ColumnCount = 2;
            tableLayoutPanelColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanelColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanelColumn.Controls.Add(labelColumn, 0, 0);
            tableLayoutPanelColumn.Controls.Add(comboBoxColumn, 1, 0);
            tableLayoutPanelColumn.Dock = DockStyle.Fill;
            tableLayoutPanelColumn.ForeColor = Color.FromArgb(230, 230, 230);
            tableLayoutPanelColumn.Location = new Point(3, 126);
            tableLayoutPanelColumn.Name = "tableLayoutPanelColumn";
            tableLayoutPanelColumn.RowCount = 1;
            tableLayoutPanelColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelColumn.Size = new Size(336, 34);
            tableLayoutPanelColumn.TabIndex = 3;
            // 
            // labelColumn
            // 
            labelColumn.Anchor = AnchorStyles.Right;
            labelColumn.AutoSize = true;
            labelColumn.BackColor = Color.FromArgb(30, 30, 30);
            labelColumn.ForeColor = Color.FromArgb(230, 230, 230);
            labelColumn.Location = new Point(66, 8);
            labelColumn.Margin = new Padding(3, 3, 5, 0);
            labelColumn.Name = "labelColumn";
            labelColumn.Size = new Size(63, 20);
            labelColumn.TabIndex = 0;
            labelColumn.Text = "Column:";
            // 
            // comboBoxColumn
            // 
            comboBoxColumn.Anchor = AnchorStyles.Left;
            comboBoxColumn.BackColor = Color.FromArgb(30, 30, 30);
            comboBoxColumn.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxColumn.FlatStyle = FlatStyle.Flat;
            comboBoxColumn.ForeColor = Color.FromArgb(230, 230, 230);
            comboBoxColumn.FormattingEnabled = true;
            comboBoxColumn.Location = new Point(139, 5);
            comboBoxColumn.Margin = new Padding(5, 5, 3, 3);
            comboBoxColumn.Name = "comboBoxColumn";
            comboBoxColumn.Size = new Size(130, 28);
            comboBoxColumn.TabIndex = 1;
            // 
            // PickPeriodView
            // 
            AcceptButton = buttonConfirm;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            CancelButton = buttonCancel;
            ClientSize = new Size(342, 223);
            Controls.Add(tableLayoutPanelMain);
            ForeColor = Color.FromArgb(230, 230, 230);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PickPeriodView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pick period";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            tableLayoutPanelActions.ResumeLayout(false);
            tableLayoutPanelActions.PerformLayout();
            tableLayoutPanelPeriod.ResumeLayout(false);
            tableLayoutPanelPeriod.PerformLayout();
            tableLayoutPanelColumn.ResumeLayout(false);
            tableLayoutPanelColumn.PerformLayout();
            ResumeLayout(false);
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