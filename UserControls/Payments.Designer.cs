namespace EasyInventory.UserControls
{
    partial class Payments
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            DGV = new DataGridView();
            editData = new Button();
            removeData = new Button();
            addData = new Button();
            ((System.ComponentModel.ISupportInitialize)DGV).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Font = new Font("Trebuchet MS", 11.25F);
            button1.ImeMode = ImeMode.NoControl;
            button1.Location = new Point(500, 504);
            button1.Name = "button1";
            button1.Size = new Size(108, 28);
            button1.TabIndex = 18;
            button1.Text = "Clear Search";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBox1.Font = new Font("Trebuchet MS", 11.25F);
            textBox1.Location = new Point(190, 506);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(300, 25);
            textBox1.TabIndex = 17;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Trebuchet MS", 18F, FontStyle.Bold);
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(3, 503);
            label1.Name = "label1";
            label1.Size = new Size(104, 29);
            label1.TabIndex = 16;
            label1.Text = "Search :";
            // 
            // DGV
            // 
            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.AllowUserToResizeRows = false;
            DGV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.Font = new Font("Trebuchet MS", 10F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DGV.EnableHeadersVisualStyles = false;
            DGV.Location = new Point(0, 0);
            DGV.MultiSelect = false;
            DGV.Name = "DGV";
            DGV.ReadOnly = true;
            DGV.RowHeadersVisible = false;
            DGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            DGV.RowTemplate.ReadOnly = true;
            DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV.ShowCellErrors = false;
            DGV.ShowCellToolTips = false;
            DGV.ShowEditingIcon = false;
            DGV.ShowRowErrors = false;
            DGV.Size = new Size(759, 500);
            DGV.TabIndex = 15;
            DGV.CellDoubleClick += DoubleClickEditLine;
            // 
            // editData
            // 
            editData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editData.ImeMode = ImeMode.NoControl;
            editData.Location = new Point(765, 61);
            editData.Name = "editData";
            editData.Size = new Size(116, 52);
            editData.TabIndex = 14;
            editData.Text = "Edit Payment";
            editData.UseVisualStyleBackColor = true;
            editData.Click += editData_Click;
            // 
            // removeData
            // 
            removeData.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            removeData.ImeMode = ImeMode.NoControl;
            removeData.Location = new Point(765, 448);
            removeData.Name = "removeData";
            removeData.Size = new Size(116, 52);
            removeData.TabIndex = 13;
            removeData.Text = "Delete Payment";
            removeData.UseVisualStyleBackColor = true;
            removeData.Click += removeData_Click;
            // 
            // addData
            // 
            addData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addData.ImeMode = ImeMode.NoControl;
            addData.Location = new Point(765, 3);
            addData.Name = "addData";
            addData.Size = new Size(116, 52);
            addData.TabIndex = 12;
            addData.Text = "Add Payment";
            addData.UseVisualStyleBackColor = true;
            addData.Click += addData_Click;
            // 
            // Payments
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.ScrollBar;
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(DGV);
            Controls.Add(editData);
            Controls.Add(removeData);
            Controls.Add(addData);
            Font = new Font("Trebuchet MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "Payments";
            Size = new Size(884, 537);
            ((System.ComponentModel.ISupportInitialize)DGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private Button editData;
        private Button removeData;
        private Button addData;
        public DataGridView DGV;
    }
}
