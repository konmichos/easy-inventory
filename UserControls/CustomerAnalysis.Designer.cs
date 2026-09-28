namespace EasyInventory.UserControls
{
    partial class CustomerAnalysis
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerAnalysis));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            sDGV = new DataGridView();
            pDGV = new DataGridView();
            totalSalesTextBox = new TextBox();
            paidAmountTextBox = new TextBox();
            unpaidBalanceTextBox = new TextBox();
            paidPercentage = new Panel();
            lblPercentage = new Label();
            border = new Panel();
            ((System.ComponentModel.ISupportInitialize)sDGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pDGV).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(230, 5);
            label1.Name = "label1";
            label1.Size = new Size(141, 22);
            label1.TabIndex = 0;
            label1.Text = "Customer's Sales:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(744, 5);
            label2.Name = "label2";
            label2.Size = new Size(174, 22);
            label2.TabIndex = 0;
            label2.Text = "Customer's Payments:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(1000, 33);
            label3.Name = "label3";
            label3.Size = new Size(200, 22);
            label3.TabIndex = 0;
            label3.Text = "Total Sales:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(1000, 143);
            label4.Name = "label4";
            label4.Size = new Size(200, 22);
            label4.TabIndex = 0;
            label4.Text = "Paid Amount:";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(1000, 253);
            label5.Name = "label5";
            label5.Size = new Size(200, 22);
            label5.TabIndex = 0;
            label5.Text = "Unpaid Balance:";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label6.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(1000, 583);
            label6.Name = "label6";
            label6.Size = new Size(200, 22);
            label6.TabIndex = 0;
            label6.Text = "Paid Percentage:";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Visible = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.BackColor = SystemColors.InactiveCaptionText;
            panel1.Enabled = false;
            panel1.Location = new Point(1000, 120);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 1);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel2.BackColor = SystemColors.InactiveCaptionText;
            panel2.Enabled = false;
            panel2.Location = new Point(1000, 230);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 1);
            panel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel3.BackColor = SystemColors.InactiveCaptionText;
            panel3.Enabled = false;
            panel3.Location = new Point(1000, 570);
            panel3.Name = "panel3";
            panel3.Size = new Size(200, 1);
            panel3.TabIndex = 0;
            panel3.Visible = false;
            // 
            // sDGV
            // 
            sDGV.AllowUserToAddRows = false;
            sDGV.AllowUserToDeleteRows = false;
            sDGV.AllowUserToResizeRows = false;
            sDGV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            sDGV.BorderStyle = BorderStyle.None;
            sDGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            sDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            sDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            sDGV.DefaultCellStyle = dataGridViewCellStyle2;
            sDGV.EnableHeadersVisualStyles = false;
            sDGV.Location = new Point(0, 30);
            sDGV.MultiSelect = false;
            sDGV.Name = "sDGV";
            sDGV.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            sDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            sDGV.RowHeadersVisible = false;
            sDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            sDGV.RowTemplate.ReadOnly = true;
            sDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            sDGV.ShowCellErrors = false;
            sDGV.ShowCellToolTips = false;
            sDGV.ShowEditingIcon = false;
            sDGV.ShowRowErrors = false;
            sDGV.Size = new Size(597, 631);
            sDGV.TabIndex = 8;
            sDGV.CellDoubleClick += sDGV_CellDoubleClick;
            // 
            // pDGV
            // 
            pDGV.AllowUserToAddRows = false;
            pDGV.AllowUserToDeleteRows = false;
            pDGV.AllowUserToResizeRows = false;
            pDGV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            pDGV.BorderStyle = BorderStyle.None;
            pDGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.Control;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            pDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            pDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            pDGV.DefaultCellStyle = dataGridViewCellStyle5;
            pDGV.EnableHeadersVisualStyles = false;
            pDGV.Location = new Point(600, 30);
            pDGV.MultiSelect = false;
            pDGV.Name = "pDGV";
            pDGV.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            pDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            pDGV.RowHeadersVisible = false;
            pDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            pDGV.RowTemplate.ReadOnly = true;
            pDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pDGV.ShowCellErrors = false;
            pDGV.ShowCellToolTips = false;
            pDGV.ShowEditingIcon = false;
            pDGV.ShowRowErrors = false;
            pDGV.Size = new Size(400, 631);
            pDGV.TabIndex = 9;
            pDGV.CellDoubleClick += pDGV_CellContentDoubleClick;
            // 
            // totalSalesTextBox
            // 
            totalSalesTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            totalSalesTextBox.BackColor = SystemColors.ScrollBar;
            totalSalesTextBox.BorderStyle = BorderStyle.None;
            totalSalesTextBox.Location = new Point(1000, 70);
            totalSalesTextBox.MaximumSize = new Size(200, 26);
            totalSalesTextBox.MaxLength = 30;
            totalSalesTextBox.MinimumSize = new Size(200, 26);
            totalSalesTextBox.Name = "totalSalesTextBox";
            totalSalesTextBox.ReadOnly = true;
            totalSalesTextBox.Size = new Size(200, 26);
            totalSalesTextBox.TabIndex = 10;
            totalSalesTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // paidAmountTextBox
            // 
            paidAmountTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            paidAmountTextBox.BackColor = SystemColors.ScrollBar;
            paidAmountTextBox.BorderStyle = BorderStyle.None;
            paidAmountTextBox.Location = new Point(1000, 180);
            paidAmountTextBox.MaximumSize = new Size(200, 26);
            paidAmountTextBox.MaxLength = 30;
            paidAmountTextBox.MinimumSize = new Size(200, 26);
            paidAmountTextBox.Name = "paidAmountTextBox";
            paidAmountTextBox.ReadOnly = true;
            paidAmountTextBox.Size = new Size(200, 26);
            paidAmountTextBox.TabIndex = 11;
            paidAmountTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // unpaidBalanceTextBox
            // 
            unpaidBalanceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            unpaidBalanceTextBox.BackColor = SystemColors.ScrollBar;
            unpaidBalanceTextBox.BorderStyle = BorderStyle.None;
            unpaidBalanceTextBox.Location = new Point(1000, 290);
            unpaidBalanceTextBox.MaximumSize = new Size(200, 26);
            unpaidBalanceTextBox.MaxLength = 30;
            unpaidBalanceTextBox.MinimumSize = new Size(200, 26);
            unpaidBalanceTextBox.Name = "unpaidBalanceTextBox";
            unpaidBalanceTextBox.ReadOnly = true;
            unpaidBalanceTextBox.Size = new Size(200, 26);
            unpaidBalanceTextBox.TabIndex = 12;
            unpaidBalanceTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // paidPercentage
            // 
            paidPercentage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            paidPercentage.Location = new Point(1000, 631);
            paidPercentage.Name = "paidPercentage";
            paidPercentage.Size = new Size(200, 30);
            paidPercentage.TabIndex = 0;
            paidPercentage.Visible = false;
            // 
            // lblPercentage
            // 
            lblPercentage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblPercentage.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPercentage.Location = new Point(1000, 605);
            lblPercentage.Name = "lblPercentage";
            lblPercentage.Size = new Size(200, 22);
            lblPercentage.TabIndex = 13;
            lblPercentage.Text = "00,00%";
            lblPercentage.TextAlign = ContentAlignment.MiddleCenter;
            lblPercentage.Visible = false;
            // 
            // border
            // 
            border.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            border.BackColor = SystemColors.MenuText;
            border.Location = new Point(597, 30);
            border.Name = "border";
            border.Size = new Size(3, 631);
            border.TabIndex = 14;
            // 
            // CustomerAnalysis
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1200, 661);
            Controls.Add(border);
            Controls.Add(lblPercentage);
            Controls.Add(paidPercentage);
            Controls.Add(unpaidBalanceTextBox);
            Controls.Add(paidAmountTextBox);
            Controls.Add(totalSalesTextBox);
            Controls.Add(pDGV);
            Controls.Add(sDGV);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            DoubleBuffered = true;
            Font = new Font("Trebuchet MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1216, 700);
            Name = "CustomerAnalysis";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CustomerAnalysis";
            Load += Form_Load;
            Resize += CustomerAnalysis_Resize;
            ((System.ComponentModel.ISupportInitialize)sDGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)pDGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private DataGridView sDGV;
        private DataGridView pDGV;
        private TextBox totalSalesTextBox;
        private TextBox paidAmountTextBox;
        private TextBox unpaidBalanceTextBox;
        private Panel paidPercentage;
        private Label label6;
        private Label lblPercentage;
        private Panel border;
    }
}