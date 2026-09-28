using System.Data;

namespace EasyInventory.UserControls
{
    public partial class Customers : UserControl
    {
        private TranslationManager translationManager;
        public Customers()
        {
            InitializeComponent();
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            LoadCustomers();
            textBox1.TextChanged += (sender, e) => LoadCustomers();
            foreach (DataGridViewColumn column in DGV.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            label1.Text = translationManager.GetTranslation(StringKeys.Generic_LabelSearch);
            button1.Text = translationManager.GetTranslation(StringKeys.Generic_ClearSearchBtn);
            addData.Text = translationManager.GetTranslation(StringKeys.Generic_AddBtn);
            editData.Text = translationManager.GetTranslation(StringKeys.Generic_EditBtn);
            removeData.Text = translationManager.GetTranslation(StringKeys.Generic_DelBtn);
            this.DGV.DoubleBuffered(true);
        }

        public void LoadCustomers()
        {
            try
            {
                string? selectedLine = DGV.SelectedRows.Count > 0
                    ? DGV.SelectedRows[0].Cells[0].Value?.ToString()
                    : null;


                string search = textBox1.Text.Trim();
                DataTable dt = CRUD.GetDataByTable("Customers");
                if (!string.IsNullOrEmpty(search))
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = string.Format($"[{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVName)}] LIKE '%{search}%' " +
                                                $"OR [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVPrimaryPhone)}] LIKE '%{search}%' " +
                                                $"OR [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVSecondaryPhone)}] LIKE '%{search}%' " +
                                                $"OR [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVEmail)}] LIKE '%{search}%' " +
                                                $"OR [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVNotes)}] LIKE '%{search}%'");
                    dt = dv.ToTable();
                }
                DGV.DataSource = dt;

                if (!string.IsNullOrEmpty(selectedLine))
                {
                    foreach (DataGridViewRow row in DGV.Rows)
                    {
                        if (row.Cells[0].Value?.ToString() == selectedLine)
                        {
                            row.Selected = true;
                            DGV.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(translationManager.GetTranslation(StringKeys.CustomerWindow_ErrorLoadingCustomers) + ex.Message,
                    translationManager.GetTranslation(StringKeys.Generic_GenericError), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addData_Click(object sender, EventArgs e)
        {
            PopupForm popupForm = new("Customers");
            popupForm.ShowDialog();
            LoadCustomers();
        }

        private void editData_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);
                PopupForm popupForm = new PopupForm("Customers", itemId);
                popupForm.ShowDialog();
                LoadCustomers();
            }
        }

        private void removeData_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);

                DialogResult result = MessageBox.Show(translationManager.GetTranslation(StringKeys.CustomerWindow_DelCustomerConfirmation),
                    translationManager.GetTranslation(StringKeys.CustomerWindow_DelCustomerQuestion),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CRUD.DeleteRecord("Customers", itemId);
                }
            }
            LoadCustomers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void LoadCustomerHistory(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int customerId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);
                CustomerAnalysis customerAnalysis = new CustomerAnalysis(customerId);
                customerAnalysis.Show();
            }
            LoadCustomers();
        }
    }
}
