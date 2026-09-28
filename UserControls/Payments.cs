using System.Data;

namespace EasyInventory.UserControls
{
    public partial class Payments : UserControl
    {
        private TranslationManager translationManager;
        public Payments()
        {
            InitializeComponent();
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            LoadPayments();
            textBox1.TextChanged += (sender, e) => LoadPayments();
            foreach (DataGridViewColumn column in DGV.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            label1.Text = translationManager.GetTranslation(StringKeys.Generic_LabelSearch);
            button1.Text = translationManager.GetTranslation(StringKeys.Generic_ClearSearchBtn);
            addData.Text = translationManager.GetTranslation(StringKeys.Generic_AddBtn);
            editData.Text = translationManager.GetTranslation(StringKeys.Generic_EditBtn);
            removeData.Text = translationManager.GetTranslation(StringKeys.Generic_DelBtn);
            this.DGV.DoubleBuffered(true);
        }

        public void LoadPayments()
        {
            try
            {
                string? selectedLine = null;
                if (DGV.SelectedRows.Count > 0)
                {
                    selectedLine = DGV.SelectedRows[0].Cells[0].Value?.ToString();
                }

                string search = textBox1.Text.Trim();
                DataTable dt = CRUD.GetDataByTable("Payments");
                if (!string.IsNullOrEmpty(search))
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = string.Format($"[{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVName)}] LIKE '%{search}%'");
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
                    DGV.Columns[2].DefaultCellStyle.Format = "€0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(translationManager.GetTranslation(StringKeys.PaymentsWindow_ErrorLoadingPayments) + ex.Message,
                    translationManager.GetTranslation(StringKeys.Generic_GenericError),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addData_Click(object sender, EventArgs e)
        {
            PopupForm popupForm = new("Payments");
            popupForm.ShowDialog();
            LoadPayments();
        }

        private void editData_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);
                PopupForm popupForm = new PopupForm("Payments", itemId);
                popupForm.ShowDialog();
                LoadPayments();
            }
        }

        private void removeData_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(DGV.SelectedRows[0].Cells[0].Value);

                DialogResult result = MessageBox.Show(translationManager.GetTranslation(StringKeys.PaymentsWindow_DelPaymentsConfirmation),
                    translationManager.GetTranslation(StringKeys.PaymentsWindow_DelPaymentsQuestion),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CRUD.DeleteRecord("Payments", itemId);
                }
            }
            LoadPayments();
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
                PopupForm popupForm = new PopupForm("Payments", itemId);
                popupForm.ShowDialog();
                LoadPayments();
            }
        }
    }
}
