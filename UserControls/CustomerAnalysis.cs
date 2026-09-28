using System.Data;
using System.Data.SQLite;

namespace EasyInventory.UserControls
{
    public partial class CustomerAnalysis : Form
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EasyInventory.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";
        private int originalFormWidth;
        private int original_sDGV_Width;
        private int original_pDGV_Width;
        private const int fixedRightGap = 200;
        private static int custId;
        private int progressBar;
        private TranslationManager translationManager;

        public CustomerAnalysis(int customerId)
        {
            InitializeComponent();
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            custId = customerId;
            LoadSales(customerId);
            LoadPayments(customerId);

            progressBar = paidPercentage.Width;
            foreach (DataGridViewColumn column in sDGV.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            foreach (DataGridViewColumn column in pDGV.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            Calculations();

            this.Text = translationManager.GetTranslation(StringKeys.CustomerAnalysis_CustomerAnalysisWindowTitle);
            label1.Text = translationManager.GetTranslation(StringKeys.CustomerAnalysis_LabelsDGV);
            label2.Text = translationManager.GetTranslation(StringKeys.CustomerAnalysis_LabelpDGV);
            label3.Text = translationManager.GetTranslation(StringKeys.CustomerAnalysis_LabelTotalSales);
            label4.Text = translationManager.GetTranslation(StringKeys.CustomerAnalysis_LabelPaidAmount);
            label5.Text = translationManager.GetTranslation(StringKeys.CustomerAnalysis_LabelUnpaidBalance);
            label6.Text = translationManager.GetTranslation(StringKeys.CustomerAnalysis_LabelPaidPercentage);

            this.sDGV.DoubleBuffered(true);
            this.pDGV.DoubleBuffered(true);
        }
        private void Form_Load(object sender, EventArgs e)
        {
            originalFormWidth = this.ClientSize.Width;
            original_sDGV_Width = sDGV.Width;
            original_pDGV_Width = pDGV.Width;
        }
        private void LoadSales(int customerId)
        {
            string? selectedLine = null;
            if (sDGV.SelectedRows.Count > 0)
            {
                selectedLine = sDGV.SelectedRows[0].Cells[0].Value?.ToString();
            }
            string query = "SELECT Sales.ID AS [#], " +
                            $"Customers.Name AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVName)}], " +
                            $"Warehouse.[Item Name] AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVName)}], " +
                            $"Sales.[Quantity Sold] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVQuantitySold)}], " +
                            $"Sales.[Item Price] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVItemPrice)}], " +
                            $"Sales.[Total Price] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVTotalPrice)}], " +
                            $"Sales.[Sale Date] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVSaleDate)}]" +
                            "FROM Sales " +
                            "JOIN Customers ON Sales.Customer = Customers.ID " +
                            "JOIN Warehouse ON Sales.Item = Warehouse.ID " +
                            "WHERE Sales.Customer = @CustomerID";
            DataTable salesData = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(salesData);
                    }
                }
            }
            if (salesData.Rows.Count > 0)
            {
                sDGV.DataSource = salesData;
            }
            else
            {
                MessageBox.Show(translationManager.GetTranslation(StringKeys.CustomerAnalysis_CustomerNotFoundInSales));
                sDGV.DataSource = null;
            }
            if (sDGV.RowCount > 0)
            {
                sDGV.Columns[4].DefaultCellStyle.Format = "€0.00";
                sDGV.Columns[5].DefaultCellStyle.Format = "€0.00";
            }
            if (!string.IsNullOrEmpty(selectedLine))
            {
                foreach (DataGridViewRow row in sDGV.Rows)
                {
                    if (row.Cells[0].Value?.ToString() == selectedLine)
                    {
                        row.Selected = true;
                        sDGV.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }
        private void sDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sDGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(sDGV.SelectedRows[0].Cells[0].Value);
                PopupForm popupForm = new PopupForm("Sales", itemId);
                popupForm.ShowDialog();
                LoadSales(custId);
                Calculations();
            }
        }
        private void LoadPayments(int customerId)
        {
            string? selectedLine = null;
            if (pDGV.SelectedRows.Count > 0)
            {
                selectedLine = pDGV.SelectedRows[0].Cells[0].Value?.ToString();
            }
            string query = "SELECT Payments.ID, " +
                            $"Customers.Name AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVName)}], " +
                            $"Payments.Amount AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVAmount)}], " +
                            $"Payments.[Payment Date] AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVPaymentDate)}] " +
                            $"FROM Payments " +
                            "JOIN Customers ON Payments.Customer = Customers.ID " +
                            "WHERE Payments.Customer = @CustomerID";
            DataTable paymentsData = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(paymentsData);
                    }
                }
            }
            if (paymentsData.Rows.Count > 0)
            {
                pDGV.DataSource = paymentsData;
            }
            else
            {
                MessageBox.Show(translationManager.GetTranslation(StringKeys.CustomerAnalysis_CustomerNotFoundInPayments));
                pDGV.DataSource = null;
            }
            if (sDGV.RowCount > 0)
            {
                pDGV.Columns[2].DefaultCellStyle.Format = "€0.00";
            }
            if (!string.IsNullOrEmpty(selectedLine))
            {
                foreach (DataGridViewRow row in pDGV.Rows)
                {
                    if (row.Cells[0].Value?.ToString() == selectedLine)
                    {
                        row.Selected = true;
                        pDGV.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }
        private void pDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pDGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(pDGV.SelectedRows[0].Cells[0].Value);
                PopupForm popupForm = new PopupForm("Payments", itemId);
                popupForm.ShowDialog();
                LoadPayments(custId);
                Calculations();
            }
        }
        private void Calculations()
        {
            decimal totalSales = 0;
            decimal paidAmount = 0;
            foreach (DataGridViewRow row in sDGV.Rows)
            {
                if (!row.IsNewRow)
                {
                    object salesValue = row.Cells[5].Value;
                    if (salesValue != DBNull.Value)
                    {
                        decimal saleAmount = Convert.ToDecimal(salesValue);
                        totalSales += saleAmount;
                    }
                }
            }
            foreach (DataGridViewRow row in pDGV.Rows)
            {
                if (!row.IsNewRow)
                {
                    object paymentValue = row.Cells[2].Value;
                    if (paymentValue != DBNull.Value)
                    {
                        decimal amountPaid = Convert.ToDecimal(paymentValue);
                        paidAmount += amountPaid;
                    }
                }
            }
            totalSalesTextBox.Text = totalSales.ToString("€ 0.00");
            paidAmountTextBox.Text = paidAmount.ToString("€ 0.00");
            decimal unpaidBalance = totalSales - paidAmount;
            unpaidBalanceTextBox.Text = unpaidBalance.ToString("€ 0.00");
            ProgressBar(paidAmount, totalSales);
        }
        private void ProgressBar(decimal paidAmount, decimal totalSales)
        {
            if (paidAmount <= 0 || totalSales <= 0)
            {
                return;
            }
            decimal percentage = (paidAmount / totalSales) * 100;
            if (percentage > 100)
            {
                percentage = 100;
                label5.Text = translationManager.GetTranslation(StringKeys.CustomerAnalysis_LabelOverpaidAmount);
            }
            paidPercentage.Width = (int)(progressBar * (percentage / 100)); ;
            ChangeProgressBarColor(percentage);
            lblPercentage.Text = $"{percentage:F2}%";
            panel3.Visible = true;
            label6.Visible = true;
            lblPercentage.Visible = true;
            paidPercentage.Visible = true;
        }
        private void ChangeProgressBarColor(decimal percentage)
        {
            if (percentage <= 30)
            {
                paidPercentage.BackColor = Color.Red;
            }
            else if (percentage <= 60)
            {
                paidPercentage.BackColor = Color.Yellow;
            }
            else
            {
                paidPercentage.BackColor = Color.Green;
            }
        }

        private void CustomerAnalysis_Resize(object sender, EventArgs e)
        {
            int newFormWidth = this.ClientSize.Width;
            if (this.WindowState == FormWindowState.Maximized || newFormWidth > originalFormWidth)
            {
                int availableWidth = newFormWidth - fixedRightGap;
                float sDGV_Percentage = (float)original_sDGV_Width / (originalFormWidth - fixedRightGap);
                float pDGV_Percentage = (float)original_pDGV_Width / (originalFormWidth - fixedRightGap);
                int new_sDGV_Width = (int)(availableWidth * sDGV_Percentage);
                int new_pDGV_Width = (int)(availableWidth * pDGV_Percentage);
                sDGV.Width = new_sDGV_Width;
                pDGV.Width = new_pDGV_Width;
                pDGV.Location = new Point(sDGV.Location.X + sDGV.Width + 3, pDGV.Location.Y);
                border.Location = new Point(sDGV.Location.X + sDGV.Width, border.Location.Y);
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                sDGV.Width = original_sDGV_Width;
                pDGV.Width = original_pDGV_Width;
                pDGV.Location = new Point(sDGV.Location.X + sDGV.Width, pDGV.Location.Y);
                border.Location = new Point(original_sDGV_Width - 3, border.Location.Y);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
