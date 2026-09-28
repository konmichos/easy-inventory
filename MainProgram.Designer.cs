namespace EasyInventory
{
    partial class MainProgram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainProgram));
            mainMenu = new MenuStrip();
            customersView = new ToolStripMenuItem();
            warehouseView = new ToolStripMenuItem();
            salesView = new ToolStripMenuItem();
            paymentsView = new ToolStripMenuItem();
            settingsView = new ToolStripMenuItem();
            controlContainer = new Panel();
            mainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.BackColor = SystemColors.ScrollBar;
            mainMenu.Font = new Font("Trebuchet MS", 12F);
            mainMenu.Items.AddRange(new ToolStripItem[] { customersView, warehouseView, salesView, paymentsView, settingsView });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Padding = new Padding(0);
            mainMenu.Size = new Size(1200, 26);
            mainMenu.TabIndex = 0;
            mainMenu.Text = "mainMenu";
            // 
            // customersView
            // 
            customersView.Name = "customersView";
            customersView.Size = new Size(95, 26);
            customersView.Text = "Customers";
            customersView.Click += CustomersView;
            // 
            // warehouseView
            // 
            warehouseView.Name = "warehouseView";
            warehouseView.Size = new Size(100, 26);
            warehouseView.Text = "Warehouse";
            warehouseView.Click += WarehouseView;
            // 
            // salesView
            // 
            salesView.Name = "salesView";
            salesView.Size = new Size(57, 26);
            salesView.Text = "Sales";
            salesView.Click += SalesView;
            // 
            // paymentsView
            // 
            paymentsView.Name = "paymentsView";
            paymentsView.Size = new Size(88, 26);
            paymentsView.Text = "Payments";
            paymentsView.Click += paymentsView_Click;
            // 
            // settingsView
            // 
            settingsView.Name = "settingsView";
            settingsView.Size = new Size(78, 26);
            settingsView.Text = "Settings";
            settingsView.Click += SettingsView;
            // 
            // controlContainer
            // 
            controlContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            controlContainer.Location = new Point(0, 26);
            controlContainer.Margin = new Padding(0);
            controlContainer.Name = "controlContainer";
            controlContainer.Size = new Size(1200, 635);
            controlContainer.TabIndex = 1;
            // 
            // MainProgram
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1200, 661);
            Controls.Add(controlContainer);
            Controls.Add(mainMenu);
            DoubleBuffered = true;
            Font = new Font("Trebuchet MS", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1216, 700);
            Name = "MainProgram";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Easy Inventory";
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mainMenu;
        private ToolStripMenuItem customersView;
        private ToolStripMenuItem warehouseView;
        private ToolStripMenuItem salesView;
        private ToolStripMenuItem settingsView;
        private Panel controlContainer;
        private ToolStripMenuItem paymentsView;
    }
}
