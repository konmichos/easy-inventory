using System.Data;

namespace EasyInventory.UserControls
{
    public partial class Warehouse : UserControl
    {
        private TranslationManager translationManager;
        public Warehouse()
        {
            InitializeComponent();
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            LoadWarehouse();
            textBox1.TextChanged += (sender, e) => LoadWarehouse();
            foreach (DataGridViewColumn column in DGV.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            label1.Text = translationManager.GetTranslation(StringKeys.Generic_LabelSearch);
            button1.Text = translationManager.GetTranslation(StringKeys.Generic_ClearSearchBtn);
            addData.Text = translationManager.GetTranslation(StringKeys.Generic_AddBtn);
            editData.Text = translationManager.GetTranslation(StringKeys.Generic_EditBtn);
            removeData.Text = translationManager.GetTranslation(StringKeys.Generic_DelBtn);
            this.DGV.DoubleBuffered(true);
        }

        public void LoadWarehouse()
        {
            try
            {
                string? selectedLine = string.Empty;
                if (DGV.SelectedRows.Count > 0)
                {
                    selectedLine = DGV.SelectedRows[0].Cells[0].Value?.ToString();
                }
                string? search = textBox1.Text.Trim();
                DataTable dt = CRUD.GetDataByTable("Warehouse");

                if (!string.IsNullOrEmpty(search))
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = string.Format($"[{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVName)}] LIKE '%{search}%' " +
                                                $"OR [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVDescription)}] LIKE '%{search}%' " +
                                                $"OR [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVBarcode)}] LIKE '%{search}%'");
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
                DGV.Columns[4].DefaultCellStyle.Format = "€0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show(translationManager.GetTranslation(StringKeys.WarehouseWindow_ErrorLoadingWarehouse) + ex.Message,
                    translationManager.GetTranslation(StringKeys.Generic_GenericError), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addData_Click(object sender, EventArgs e)
        {
            PopupForm popupForm = new("Warehouse");
            popupForm.ShowDialog();
            LoadWarehouse();
        }

        private void editData_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);
                PopupForm popupForm = new PopupForm("Warehouse", itemId);
                popupForm.ShowDialog();
            }
            LoadWarehouse();
        }

        private void removeData_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);

                DialogResult result = MessageBox.Show(translationManager.GetTranslation(StringKeys.WarehouseWindow_DelWarehouseConfirmation),
                    translationManager.GetTranslation(StringKeys.WarehouseWindow_DelItemQuestion),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CRUD.DeleteRecord("Warehouse", itemId);
                }
            }
            LoadWarehouse();
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
                PopupForm popupForm = new PopupForm("Warehouse", itemId);
                popupForm.ShowDialog();
            }
            LoadWarehouse();
        }
    }
}
