namespace EasyInventory.UserControls
{
    partial class Customers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customers));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            addData = new Button();
            removeData = new Button();
            editData = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            DGV = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)DGV).BeginInit();
            SuspendLayout();
            // 
            // addData
            // 
            resources.ApplyResources(addData, "addData");
            addData.Name = "addData";
            addData.UseVisualStyleBackColor = true;
            addData.Click += addData_Click;
            // 
            // removeData
            // 
            resources.ApplyResources(removeData, "removeData");
            removeData.Name = "removeData";
            removeData.UseVisualStyleBackColor = true;
            removeData.Click += removeData_Click;
            // 
            // editData
            // 
            resources.ApplyResources(editData, "editData");
            editData.Name = "editData";
            editData.UseVisualStyleBackColor = true;
            editData.Click += editData_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // DGV
            // 
            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.AllowUserToResizeRows = false;
            resources.ApplyResources(DGV, "DGV");
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
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Trebuchet MS", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DGV.DefaultCellStyle = dataGridViewCellStyle2;
            DGV.EnableHeadersVisualStyles = false;
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
            DGV.CellDoubleClick += LoadCustomerHistory;
            // 
            // Customers
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.ScrollBar;
            Controls.Add(DGV);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(editData);
            Controls.Add(removeData);
            Controls.Add(addData);
            DoubleBuffered = true;
            resources.ApplyResources(this, "$this");
            Name = "Customers";
            ((System.ComponentModel.ISupportInitialize)DGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button addData;
        private Button removeData;
        private Button editData;
        private Label label1;
        private TextBox textBox1;
        private Button button1;
        public DataGridView DGV;
    }
}
