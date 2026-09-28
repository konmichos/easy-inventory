
using EasyInventory.UserControls;

namespace EasyInventory
{
    public partial class MainProgram : Form
    {
        private Customers customers;
        private Warehouse warehouse;
        private Sales sales;
        private Settings settings;
        private Payments payments;
        private TranslationManager translationManager;

        public MainProgram()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            customers = new();
            warehouse = new();
            sales = new();
            settings = new();
            payments = new();
            LoadControls(customers);
            customersView.Text = translationManager.GetTranslation(StringKeys.MainWindow_MainWindowCustomers);
            warehouseView.Text = translationManager.GetTranslation(StringKeys.MainWindow_MainWindowWarehouse);
            salesView.Text = translationManager.GetTranslation(StringKeys.MainWindow_MainWindowSales);
            paymentsView.Text = translationManager.GetTranslation(StringKeys.MainWindow_MainWindowPayments);
            settingsView.Text = translationManager.GetTranslation(StringKeys.MainWindow_MainWindowSettings);
        }

        private void LoadControls(UserControl userControl)
        {
            controlContainer.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            controlContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void CustomersView(object sender, EventArgs e)
        {
            if (controlContainer.Controls.Contains(customers)) return;
            LoadControls(customers);
        }

        private void WarehouseView(object sender, EventArgs e)
        {
            if (controlContainer.Controls.Contains(warehouse)) return;
            warehouse.LoadWarehouse();
            LoadControls(warehouse);
        }

        private void SalesView(object sender, EventArgs e)
        {
            if (controlContainer.Controls.Contains(sales)) return;
            sales.LoadSales();
            LoadControls(sales);
        }

        private void paymentsView_Click(object sender, EventArgs e)
        {
            if (controlContainer.Controls.Contains(payments)) return;
            payments.LoadPayments();
            LoadControls(payments);
        }

        private void SettingsView(object sender, EventArgs e)
        {
            if (controlContainer.Controls.Contains(settings)) return;
            LoadControls(settings);
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
