namespace EasyInventory
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            saveSettings = new Button();
            label1 = new Label();
            label3 = new Label();
            autoBackupCombobox = new ComboBox();
            exportBtn = new Button();
            ManualBackupButton = new Button();
            label2 = new Label();
            label4 = new Label();
            lastBackupLabel = new Label();
            languageSelection = new ComboBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // saveSettings
            // 
            resources.ApplyResources(saveSettings, "saveSettings");
            saveSettings.Name = "saveSettings";
            saveSettings.UseVisualStyleBackColor = true;
            saveSettings.Click += saveSettings_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // autoBackupCombobox
            // 
            autoBackupCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            autoBackupCombobox.FormattingEnabled = true;
            resources.ApplyResources(autoBackupCombobox, "autoBackupCombobox");
            autoBackupCombobox.Name = "autoBackupCombobox";
            // 
            // exportBtn
            // 
            resources.ApplyResources(exportBtn, "exportBtn");
            exportBtn.Name = "exportBtn";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += ExportToExcel;
            // 
            // ManualBackupButton
            // 
            resources.ApplyResources(ManualBackupButton, "ManualBackupButton");
            ManualBackupButton.Name = "ManualBackupButton";
            ManualBackupButton.UseVisualStyleBackColor = true;
            ManualBackupButton.Click += ManualBackupButton_Click;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // lastBackupLabel
            // 
            resources.ApplyResources(lastBackupLabel, "lastBackupLabel");
            lastBackupLabel.Name = "lastBackupLabel";
            // 
            // languageSelection
            // 
            languageSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            languageSelection.FormattingEnabled = true;
            resources.ApplyResources(languageSelection, "languageSelection");
            languageSelection.Name = "languageSelection";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // Settings
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.ScrollBar;
            Controls.Add(label5);
            Controls.Add(languageSelection);
            Controls.Add(lastBackupLabel);
            Controls.Add(label4);
            Controls.Add(ManualBackupButton);
            Controls.Add(label2);
            Controls.Add(exportBtn);
            Controls.Add(autoBackupCombobox);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(saveSettings);
            resources.ApplyResources(this, "$this");
            Name = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button saveSettings;
        private Label label1;
        private Label label3;
        private ComboBox autoBackupCombobox;
        private Button exportBtn;
        private Button ManualBackupButton;
        private Label label2;
        private Label label4;
        private Label lastBackupLabel;
        private ComboBox languageSelection;
        private Label label5;
    }
}
