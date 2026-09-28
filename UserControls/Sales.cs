using System.Data;
namespace EasyInventory.UserControls
{
    public partial class Sales : UserControl
    {
        private TranslationManager translationManager;

        public Sales()
        {
            InitializeComponent();
            LoadSales();
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            textBox1.TextChanged += (sender, e) => LoadSales();
            foreach (DataGridViewColumn column in DGV.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            label1.Text = translationManager.GetTranslation(StringKeys.Generic_LabelSearch);
            button1.Text = translationManager.GetTranslation(StringKeys.Generic_ClearSearchBtn);
            addData.Text = translationManager.GetTranslation(StringKeys.Generic_AddBtn);
            editData.Text = translationManager.GetTranslation(StringKeys.Generic_EditBtn);
            removeData.Text = translationManager.GetTranslation(StringKeys.Generic_DelBtn);
            this.DGV.DoubleBuffered(true);
        }
        public void LoadSales()
        {
            try
            {
                string? selectedLine = null;
                if (DGV.SelectedRows.Count > 0)
                {
                    selectedLine = DGV.SelectedRows[0].Cells[0].Value?.ToString();
                }

                string search = textBox1.Text.Trim();
                DataTable dt = CRUD.GetDataByTable("Sales");
                if (!string.IsNullOrEmpty(search))
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = string.Format($"[{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVName)}] LIKE '%{search}%' " +
                                                    $"OR [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVItem)}] LIKE '%{search}%' " +
                                                    $"OR [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVSaleDate)}] LIKE '%{search}%'");
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
                if (DGV.RowCount > 0)
                {
                    DGV.Columns[4].DefaultCellStyle.Format = "€0.00";
                    DGV.Columns[5].DefaultCellStyle.Format = "€0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(translationManager.GetTranslation(StringKeys.SalesWindow_ErrorLoadingSales) + ex.Message,
                    translationManager.GetTranslation(StringKeys.Generic_GenericError),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addData_Click(object sender, EventArgs e)
        {
            PopupForm popupForm = new("Sales");
            popupForm.ShowDialog();
            LoadSales();
        }

        private void editData_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);
                PopupForm popupForm = new PopupForm("Sales", itemId);
                popupForm.ShowDialog();
                LoadSales();
            }
        }

        private void removeData_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);

                DialogResult result = MessageBox.Show(translationManager.GetTranslation(StringKeys.SalesWindow_DelSalesConfirmation),
                    translationManager.GetTranslation(StringKeys.SalesWindow_DelSalesQuestion),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CRUD.DeleteRecord("Sales", itemId);
                }
            }
            LoadSales();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void DoubleClickEditLine(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);
                PopupForm popupForm = new PopupForm("Sales", itemId);
                popupForm.ShowDialog();
                LoadSales();
            }
        }
    }
}
