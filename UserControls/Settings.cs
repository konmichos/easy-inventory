using System.Data;
using System.Data.SQLite;
using ClosedXML.Excel;

namespace EasyInventory
{
    public partial class Settings : UserControl
    {
        private TranslationManager translationManager;
        private Dictionary<int, string> backupOptions;

        public Settings()
        {
            InitializeComponent();
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            LoadSettings();
            AutomaticBackup();

            label1.Text = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsAutoBackupLabel);
            label2.Text = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsBackupNowLabel);
            label3.Text = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExportToExcelLabel);
            label4.Text = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsLastBackupLabel);
            label5.Text = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsLanguageLabel);
            ManualBackupButton.Text = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsBackupNowBtn);
            exportBtn.Text = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExportToExcelBtn);
            saveSettings.Text = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsSaveSettingsBtn);
        }

        private void LoadSettings()
        {
            string languageFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Language");

            if (Directory.Exists(languageFolderPath))
            {
                var languageFiles = Directory.GetFiles(languageFolderPath, "*.json");

                foreach (var file in languageFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    languageSelection.Items.Add(fileName);
                }
            }
            else
            {
                MessageBox.Show("Language folder not found.");
            }


            string settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");
            backupOptions = new Dictionary<int, string>
{
            { 0, translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsDefaultBackupFrequency) },
            { 1, translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsAutoBackupDaily) },
            { 2, translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsAutoBackupWeekly) },
            { 3, translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsAutoBackupMonthly) }
            };
            autoBackupCombobox.Items.Clear();
            foreach (var text in backupOptions.Values)
            {
                autoBackupCombobox.Items.Add(text);
            }
            string json = File.ReadAllText(settingsPath);
            dynamic? settings = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            languageSelection.SelectedItem = (string)settings.Language;
            lastBackupLabel.Text = settings.LastBackup ==
                translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsDefaultLastBackup) ?
                translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsDefaultLastBackupReplacement) :
                settings.LastBackup;

            int backupIndex = (int)settings.BackupFrequency;
            if (backupOptions.TryGetValue(backupIndex, out var selectedText))
            {
                autoBackupCombobox.SelectedItem = selectedText;
            }

        }

        private void AutomaticBackup()
        {
            string settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");

            if (!File.Exists(settingsPath)) return;

            string json = File.ReadAllText(settingsPath);
            dynamic? settings = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            string backupFrequency = settings.BackupFrequency;
            string lastBackupString = settings.LastBackup;
            DateTime now = DateTime.Now;
            DateTime lastBackup = Convert.ToDateTime(settings.LastBackup);
            bool shouldBackup = false;

            string daily = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsAutoBackupDaily);
            string weekly = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsAutoBackupWeekly);
            string monthly = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsAutoBackupMonthly);
            string never = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsDefaultBackupFrequency);
            switch (backupFrequency)
            {
                case var value when value == daily:
                    shouldBackup = (now - lastBackup).TotalDays >= 1;
                    break;
                case var value when value == weekly:
                    shouldBackup = (now - lastBackup).TotalDays >= 7;
                    break;
                case var value when value == monthly:
                    shouldBackup = (now - lastBackup).TotalDays >= 30;
                    break;
                case var value when value == never:
                    return;
                default:
                    return;
            }
            if (shouldBackup)
            {
                Backup();
            }
        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            string settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");
            string json = File.ReadAllText(settingsPath);
            dynamic? language = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            dynamic settings = new
            {
                BackupFrequency = backupOptions.FirstOrDefault(f => f.Value == autoBackupCombobox.SelectedItem.ToString()).Key,
                LastBackup = lastBackupLabel.Text.ToString() ==
                                translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsDefaultLastBackupReplacement) ?
                                translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsDefaultLastBackup) :
                                lastBackupLabel.Text.ToString(),
                Language = languageSelection.SelectedItem.ToString()
            };

            if ((string)settings.Language != (string)language.Language)
            {
                DialogResult result = MessageBox.Show(translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsRestartApplication),
                    translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsRestartQuestion),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }

            File.WriteAllText(settingsPath, Newtonsoft.Json.JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented));

            MessageBox.Show(translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsSavedSuccess),
                translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsSuccess),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Backup()
        {
            try
            {
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EasyInventory.db");
                string backupDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFilePath = Path.Combine(backupDirectory, $"backup_{timestamp}.db");

                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                if (File.Exists(dbPath))
                {
                    File.Copy(dbPath, backupFilePath, true);
                    string settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");
                    if (File.Exists(settingsPath))
                    {
                        string json = File.ReadAllText(settingsPath);
                        dynamic? settings = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                        settings.LastBackup = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        File.WriteAllText(settingsPath, Newtonsoft.Json.JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented));
                        lastBackupLabel.Text = (string)settings.LastBackup;
                    }
                    MessageBox.Show($"{translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsSuccessfulBackup)} {backupFilePath}",
                        translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsBackup), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsErrorDatabaseFileNotFound),
                        translationManager.GetTranslation(StringKeys.Generic_GenericError), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsErrorBackup)} {ex.Message}",
                    translationManager.GetTranslation(StringKeys.Generic_GenericError), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManualBackupButton_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private void ExportToExcel(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            string defaultFileName = $"Export_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";
            saveFileDialog.FileName = defaultFileName;
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string exportPath = saveFileDialog.FileName;
                var workbook = new XLWorkbook();

                using (var connection = new SQLiteConnection($"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EasyInventory.db")};Version=3;"))
                {
                    connection.Open();

                    var customersQuery = "SELECT ID AS [#], " +
                                         $"Name AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVName)}], " +
                                         $"[Main Phone] AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVPrimaryPhone)}], " +
                                         $"[Secondary Phone] AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVSecondaryPhone)}], " +
                                         $"Email AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVEmail)}], " +
                                         $"Notes AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVNotes)}] " +
                                         "FROM Customers";
                    DataTable customersData = GetData(customersQuery, connection);
                    var customersSheet = workbook.AddWorksheet(translationManager.GetTranslation(StringKeys.MainWindow_MainWindowCustomers));
                    customersSheet.Cell(1, 1).InsertTable(customersData);
                    customersSheet.Columns().AdjustToContents();

                    var warehouseQuery = "SELECT ID AS [#], " +
                                         $"[Item Name] AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVName)}], " +
                                         $"[Item Description] AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVDescription)}], " +
                                         $"Quantity AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVQuantity)}], " +
                                         $"Price AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVPrice)}], " +
                                         $"SKU AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVBarcode)}] " +
                                        "FROM Warehouse";
                    DataTable warehouseData = GetData(warehouseQuery, connection);
                    var warehouseSheet = workbook.AddWorksheet(translationManager.GetTranslation(StringKeys.MainWindow_MainWindowWarehouse));
                    warehouseSheet.Cell(1, 1).InsertTable(warehouseData);
                    warehouseSheet.Columns().AdjustToContents();

                    var salesQuery = "SELECT Sales.ID AS [#], " +
                                     $"Customers.Name AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVName)}], " +
                                     $"Warehouse.[Item Name] AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVName)}], " +
                                     $"Sales.[Quantity Sold] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVQuantitySold)}], " +
                                     $"Sales.[Item Price] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVItemPrice)}], " +
                                     $"Sales.[Total Price] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVTotalPrice)}], " +
                                     $"Sales.[Sale Date] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVSaleDate)}]" +
                                     "FROM Sales " +
                                     "JOIN Customers ON Sales.Customer = Customers.ID " +
                                     "JOIN Warehouse ON Sales.Item = Warehouse.ID;";
                    DataTable salesData = GetData(salesQuery, connection);
                    var salesSheet = workbook.AddWorksheet(translationManager.GetTranslation(StringKeys.MainWindow_MainWindowSales));
                    salesSheet.Cell(1, 1).InsertTable(salesData);
                    salesSheet.Columns().AdjustToContents();

                    var paymentsQuery = "SELECT Payments.ID AS [#], " +
                                        $"Customers.Name AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVName)}], " +
                                        $"Payments.Amount AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVAmount)}], " +
                                        $"Payments.[Payment Date] AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVPaymentDate)}] " +
                                        "FROM Payments " +
                                        "JOIN Customers ON Payments.Customer = Customers.ID ";
                    DataTable paymentsData = GetData(paymentsQuery, connection);
                    var paymentsSheet = workbook.AddWorksheet(translationManager.GetTranslation(StringKeys.MainWindow_MainWindowPayments));
                    paymentsSheet.Cell(1, 1).InsertTable(paymentsData);
                    paymentsSheet.Columns().AdjustToContents();


                    var summarySheet = workbook.AddWorksheet(translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExcelSummary));
                    summarySheet.Cell(6, 1).Value = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExcelSalesData);
                    summarySheet.Cell(7, 1).InsertTable(salesData);
                    summarySheet.Range(6, 1, 6, 7).Merge();
                    summarySheet.Cell(6, 1).Style.Font.Bold = true;
                    summarySheet.Cell(6, 1).Style.Font.Underline = XLFontUnderlineValues.Single;
                    summarySheet.Cell(6, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(6, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    summarySheet.Cell(6, 9).Value = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExcelPaymentsData);
                    summarySheet.Cell(7, 9).InsertTable(paymentsData);
                    summarySheet.Range(6, 9, 6, 12).Merge();
                    summarySheet.Cell(6, 9).Style.Font.Bold = true;
                    summarySheet.Cell(6, 9).Style.Font.Underline = XLFontUnderlineValues.Single;
                    summarySheet.Cell(6, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(6, 9).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    summarySheet.Cell(1, 2).Value = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExcelTotalSales);
                    summarySheet.Cell(1, 2).Style.Font.Bold = true;
                    summarySheet.Cell(1, 2).Style.Font.Underline = XLFontUnderlineValues.Single;
                    summarySheet.Cell(1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(1, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    summarySheet.Cell(1, 3).FormulaA1 = $"SUBTOTAL(9, F8:F1000)";
                    summarySheet.Cell(1, 3).Style.Font.Bold = true;
                    summarySheet.Cell(1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(1, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    summarySheet.Cell(3, 2).Value = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExcelPayments);
                    summarySheet.Cell(3, 2).Style.Font.Bold = true;
                    summarySheet.Cell(3, 2).Style.Font.Underline = XLFontUnderlineValues.Single;
                    summarySheet.Cell(3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(3, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    summarySheet.Cell(3, 3).FormulaA1 = $"SUBTOTAL(9, K8:K1000)";
                    summarySheet.Cell(3, 3).Style.Font.Bold = true;
                    summarySheet.Cell(3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(3, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                    summarySheet.Cell(1, 5).Value = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExcelUnpaidAmount);
                    summarySheet.Cell(1, 5).Style.Font.Bold = true;
                    summarySheet.Cell(1, 5).Style.Font.Underline = XLFontUnderlineValues.Single;
                    summarySheet.Cell(1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(1, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    summarySheet.Cell(1, 6).FormulaA1 = $"C1-C3";
                    summarySheet.Cell(1, 6).Style.Font.Bold = true;
                    summarySheet.Cell(1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(1, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    summarySheet.Cell(3, 5).Value = translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsExcelPercentagePaid);
                    summarySheet.Cell(3, 5).Style.Font.Bold = true;
                    summarySheet.Cell(3, 5).Style.Font.Underline = XLFontUnderlineValues.Single;
                    summarySheet.Cell(3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(3, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    summarySheet.Cell(3, 6).FormulaA1 = $"(C3 / C1)";
                    summarySheet.Cell(3, 6).Style.NumberFormat.Format = "0.00%";
                    summarySheet.Cell(3, 6).Style.Font.Bold = true;
                    summarySheet.Cell(3, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    summarySheet.Cell(3, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    summarySheet.Columns().AdjustToContents();
                }
                workbook.SaveAs(exportPath);
                MessageBox.Show(translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsDataExportedSuccess), translationManager.GetTranslation(StringKeys.SettingsWindow_SettingsDataExportComplete), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private DataTable GetData(string query, SQLiteConnection connection)
        {
            DataTable dataTable = new DataTable();
            using (var command = new SQLiteCommand(query, connection))
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
    }
}


