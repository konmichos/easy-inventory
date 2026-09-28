using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace EasyInventory.UserControls
{
    public partial class PopupForm : Form
    {
        private TranslationManager translationManager;
        private int stockAfterEdit;
        private int quantitySold;
        private int finalizedStock;
        private int warehouseStock;
        private string? tableName;
        private int itemId;

        public PopupForm(string tableName, int itemId = -1)
        {
            InitializeComponent();
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            stockAfterEdit = 0;
            quantitySold = 0;
            finalizedStock = 0;
            warehouseStock = 0;
            this.itemId = itemId;
            this.tableName = tableName;
            switch (tableName)
            {
                case "Customers":
                    textBox2.TextChanged += TextBox_TextChanged;
                    textBox2.KeyPress += NumericChecking;
                    textBox2.MaxLength = 15;
                    textBox3.TextChanged += TextBox_TextChanged;
                    textBox3.KeyPress += NumericChecking;
                    textBox3.MaxLength = 15;
                    label1.Text = translationManager.GetTranslation(StringKeys.PopupForm_CustomersLabel1);
                    label2.Text = translationManager.GetTranslation(StringKeys.PopupForm_CustomersLabel2);
                    label3.Text = translationManager.GetTranslation(StringKeys.PopupForm_CustomersLabel3);
                    label4.Text = translationManager.GetTranslation(StringKeys.PopupForm_CustomersLabel4);
                    label5.Text = translationManager.GetTranslation(StringKeys.PopupForm_CustomersLabel5);
                    label6.Text = itemId == -1
                        ? translationManager.GetTranslation(StringKeys.PopupForm_CustomersLabel6New)
                        : translationManager.GetTranslation(StringKeys.PopupForm_CustomersLabel6Edit);
                    button1.Text = itemId == -1
                        ? translationManager.GetTranslation(StringKeys.PopupForm_PopupAddBtn)
                        : translationManager.GetTranslation(StringKeys.PopupForm_PopupSaveBtn);
                    break;

                case "Warehouse":
                    textBox3.TextChanged += TextBox_TextChanged;
                    textBox3.KeyPress += NumericChecking;
                    textBox3.MaxLength = 10;
                    textBox3.Tag = "int";
                    textBox4.TextChanged += TextBox_TextChanged;
                    textBox4.KeyPress += PreventCharOnDecimal;
                    textBox4.KeyUp += DecimalChecking;
                    textBox4.MaxLength = 10;
                    textBox4.Tag = "decimal";
                    label1.Text = translationManager.GetTranslation(StringKeys.PopupForm_WarehouseLabel1);
                    label2.Text = translationManager.GetTranslation(StringKeys.PopupForm_WarehouseLabel2);
                    label3.Text = translationManager.GetTranslation(StringKeys.PopupForm_WarehouseLabel3);
                    label4.Text = translationManager.GetTranslation(StringKeys.PopupForm_WarehouseLabel4);
                    label5.Text = translationManager.GetTranslation(StringKeys.PopupForm_WarehouseLabel5);
                    label6.Text = itemId == -1
                        ? translationManager.GetTranslation(StringKeys.PopupForm_WarehouseLabel6New)
                        : translationManager.GetTranslation(StringKeys.PopupForm_WarehouseLabel6Edit);
                    button1.Text = itemId == -1
                        ? translationManager.GetTranslation(StringKeys.PopupForm_PopupAddBtn)
                        : translationManager.GetTranslation(StringKeys.PopupForm_PopupSaveBtn);
                    break;

                case "Sales":
                    textBox3.TextChanged += TextBox_TextChanged;
                    textBox3.TextChanged += CalculateTotalPrice;
                    textBox3.KeyPress += NumericChecking;
                    textBox3.MaxLength = 10;
                    textBox3.Tag = "int";
                    textBox4.TextChanged += TextBox_TextChanged;
                    textBox4.TextChanged += CalculateTotalPrice;
                    textBox4.KeyPress += PreventCharOnDecimal;
                    textBox4.KeyUp += DecimalChecking;
                    textBox4.MaxLength = 10;
                    textBox4.Tag = "decimal";
                    SalesComboboxFilling();
                    label1.Text = translationManager.GetTranslation(StringKeys.PopupForm_SalesLabel1);
                    label2.Text = translationManager.GetTranslation(StringKeys.PopupForm_SalesLabel2);
                    label3.Text = translationManager.GetTranslation(StringKeys.PopupForm_SalesLabel3);
                    label4.Text = translationManager.GetTranslation(StringKeys.PopupForm_SalesLabel4);
                    label5.Text = translationManager.GetTranslation(StringKeys.PopupForm_SalesLabel5);
                    label7.Text = translationManager.GetTranslation(StringKeys.PopupForm_SalesLabel7);
                    label7.Visible = true;
                    label8.Visible = true;
                    label8.Text = "";
                    label6.Text = itemId == -1
                        ? translationManager.GetTranslation(StringKeys.PopupForm_SalesLabel6New)
                        : translationManager.GetTranslation(StringKeys.PopupForm_SalesLabel6Edit);
                    button1.Text = itemId == -1
                        ? translationManager.GetTranslation(StringKeys.PopupForm_PopupAddBtn)
                        : translationManager.GetTranslation(StringKeys.PopupForm_PopupSaveBtn);

                    #region ENABLES-DISABLES
                    textBox1.Enabled = false;
                    textBox1.Visible = false;
                    textBox1.TabIndex = 9;
                    textBox2.Enabled = false;
                    textBox2.Visible = false;
                    textBox2.TabIndex = 1;
                    textBox5.Enabled = false;
                    textBox5.Visible = false;
                    comboBox1.Enabled = true;
                    comboBox1.Visible = true;
                    comboBox1.TabIndex = 1;
                    comboBox2.Enabled = true;
                    comboBox2.Visible = true;
                    comboBox2.TabIndex = 2;
                    dateTimePicker1.Enabled = true;
                    dateTimePicker1.Visible = true;
                    dateTimePicker1.Value = DateTime.Now;
                    label8.Visible = true;
                    label8.Text = "€0.00";
                    #endregion ENABLES-DISABLES

                    break;
                case "Payments":
                    textBox2.TextChanged += TextBox_TextChanged;
                    textBox2.KeyPress += PreventCharOnDecimal;
                    textBox2.KeyUp += DecimalChecking;
                    textBox2.Tag = "decimal";
                    textBox2.MaxLength = 10;
                    PaymentsComboboxFilling();
                    label1.Text = translationManager.GetTranslation(StringKeys.PopupForm_PaymentsLabel1);
                    label2.Text = translationManager.GetTranslation(StringKeys.PopupForm_PaymentsLabel2);
                    label3.Text = translationManager.GetTranslation(StringKeys.PopupForm_PaymentsLabel3);
                    label6.Text = itemId == -1
                        ? translationManager.GetTranslation(StringKeys.PopupForm_PaymentsLabel6New)
                        : translationManager.GetTranslation(StringKeys.PopupForm_PaymentsLabel6Edit);
                    button1.Text = itemId == -1
                        ? translationManager.GetTranslation(StringKeys.PopupForm_PopupAddBtn)
                        : translationManager.GetTranslation(StringKeys.PopupForm_PopupSaveBtn);

                    #region ENABLES-DISABLES
                    textBox1.Enabled = false;
                    textBox1.Visible = false;
                    textBox1.TabIndex = 9;
                    textBox3.Enabled = false;
                    textBox3.Visible = false;
                    textBox3.TabIndex = 9;
                    textBox4.Enabled = false;
                    textBox4.Visible = false;
                    textBox5.Enabled = false;
                    textBox5.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;

                    comboBox1.Enabled = true;
                    comboBox1.Visible = true;
                    comboBox1.TabIndex = 1;
                    dateTimePicker1.Enabled = true;
                    dateTimePicker1.Visible = true;
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker1.Location = new Point(119, 209);
                    #endregion ENABLES-DISABLES

                    break;
            }
            if (itemId != -1) LoadTableToUpdate();

        }

        private void PaymentsComboboxFilling()
        {
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            using (var connection = CRUD.GetConnection())
            {
                connection.Open();

                var customerList = new List<KeyValuePair<int, string>>();
                using (var command = new SQLiteCommand("SELECT ID, Name FROM Customers ORDER BY Name ASC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customerList.Add(new KeyValuePair<int, string>(
                            reader.GetInt32(0),
                            reader.GetString(1)));
                    }
                }
                comboBox1.DataSource = customerList;
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";

                comboBox1.SelectedIndex = -1;
            }
        }

        private void SalesComboboxFilling()
        {
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            comboBox1.Items.Add("1");
            comboBox2.Items.Add("1");
            using (var connection = CRUD.GetConnection())
            {
                connection.Open();

                var customerList = new List<KeyValuePair<int, string>>();
                using (var command = new SQLiteCommand("SELECT ID, Name FROM Customers ORDER BY Name ASC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customerList.Add(new KeyValuePair<int, string>(
                            reader.GetInt32(0),
                            reader.GetString(1)));
                    }
                }
                var itemList = new List<KeyValuePair<int, string>>();
                using (var command = new SQLiteCommand("SELECT ID, [Item Name] FROM Warehouse ORDER BY ID ASC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itemList.Add(new KeyValuePair<int, string>(
                            reader.GetInt32(0),
                            reader.GetString(1)));
                    }
                }
                comboBox1.DataSource = customerList;
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";

                comboBox2.DataSource = itemList;
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";

                comboBox1.Text = "Select...";
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
        }

        private void LoadTableToUpdate()
        {
            using (var connection = CRUD.GetConnection())
            {
                connection.Open();
                string? query = $"SELECT * FROM {tableName} WHERE ID = @ID";
                using (var command = new SQLiteCommand($"{query}", connection))
                {
                    command.Parameters.AddWithValue("@ID", itemId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader != null)
                                switch (tableName)
                                {
                                    case "Customers":
                                        textBox1.Text = reader?["Name"].ToString();
                                        textBox2.Text = reader?["Main Phone"].ToString();
                                        textBox3.Text = reader?["Secondary Phone"].ToString();
                                        textBox4.Text = reader?["Email"].ToString();
                                        textBox5.Text = reader?["Notes"].ToString();
                                        break;
                                    case "Warehouse":
                                        textBox1.Text = reader?["Item Name"].ToString();
                                        textBox2.Text = reader?["Item Description"].ToString();
                                        textBox3.Text = reader?["Quantity"].ToString();
                                        textBox4.Text = reader?["Price"].ToString();
                                        textBox5.Text = reader?["SKU"].ToString();
                                        break;
                                    case "Sales":
                                        comboBox1.SelectedValue = Convert.ToInt32(reader?[1]);
                                        comboBox2.SelectedValue = Convert.ToInt32(reader?[2]);
                                        textBox3.Text = reader?["Quantity Sold"].ToString();
                                        textBox4.Text = reader?["Item Price"].ToString();
                                        label8.Text = string.Format("€{0:0.00}", Convert.ToDecimal(reader?["Total Price"]));
                                        if (DateTime.TryParse(reader?["Sale Date"].ToString(), out DateTime saleDate))
                                        {
                                            dateTimePicker1.Value = saleDate;
                                        }
                                        else
                                        {
                                            dateTimePicker1.Value = DateTime.Now;
                                        }
                                        quantitySold = Convert.ToInt32(textBox3.Text);
                                        break;
                                    case "Payments":
                                        comboBox1.SelectedValue = Convert.ToInt32(reader?[1]);
                                        textBox2.Text = reader?["Amount"].ToString();
                                        if (DateTime.TryParse(reader?["Payment Date"].ToString(), out DateTime paymentDate))
                                        {
                                            dateTimePicker1.Value = paymentDate;
                                        }
                                        else
                                        {
                                            dateTimePicker1.Value = DateTime.Now;
                                        }
                                        break;
                                }
                        }
                    }
                }
            }
            CalculateTotalPrice(sender: null, EventArgs.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = label8.Text.Replace("€", "");
            label8.Text = result;
            object[] values = new object[]
                {
                    textBox1.Text,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text
                };
            object[] salesValues = new object[]
                {
                    comboBox1.SelectedValue ?? -1,
                    comboBox2.SelectedValue ?? -1,
                    textBox3.Text,
                    textBox4.Text,
                    label8.Text,
                    dateTimePicker1.Value.ToString("dd-MM-yyyy HH:mm:ss")
                };
            object[] paymentValues = new object[]
                {
                    comboBox1.SelectedValue ?? -1,
                    textBox2.Text,
                    dateTimePicker1.Value.ToString("dd-MM-yyyy HH:mm:ss")
                };
            switch (tableName)
            {
                case "Customers":
                    if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()) || string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
                    {
                        MessageBox.Show(translationManager.GetTranslation(StringKeys.PopupForm_CustomersRequired),
                            translationManager.GetTranslation(StringKeys.PopupForm_ValidationError),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (itemId == -1)
                    {
                        CRUD.AddRecord(tableName, values);
                        this.Close();
                    }
                    if (itemId != -1)
                    {
                        CRUD.UpdateRecord(tableName, itemId, values);
                        this.Close();
                    }
                    break;
                case "Warehouse":
                    if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                    {
                        MessageBox.Show(translationManager.GetTranslation(StringKeys.PopupForm_WarehouseRequired),
                            translationManager.GetTranslation(StringKeys.PopupForm_ValidationError),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (itemId == -1)
                    {
                        CRUD.AddRecord(tableName, values);
                        this.Close();
                    }
                    if (itemId != -1)
                    {
                        CRUD.UpdateRecord(tableName, itemId, values);
                        this.Close();
                    }
                    break;
                case "Sales":
                    if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                    {
                        MessageBox.Show(translationManager.GetTranslation(StringKeys.PopupForm_SalesRequired),
                            translationManager.GetTranslation(StringKeys.PopupForm_ValidationError),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (itemId == -1)
                    {
                        WarehouseStockUpdater(Convert.ToInt32(comboBox2.SelectedValue), Convert.ToInt32(textBox3.Text));
                        CRUD.AddRecord(tableName, salesValues);
                        this.Close();
                    }
                    if (itemId != -1)
                    {
                        stockAfterEdit = Convert.ToInt32(salesValues[2]);
                        finalizedStock = stockAfterEdit - quantitySold;
                        WarehouseStockUpdater(Convert.ToInt32(comboBox2.SelectedValue), finalizedStock);
                        CRUD.UpdateRecord(tableName, itemId, salesValues);
                        this.Close();
                    }
                    break;
                case "Payments":
                    if (comboBox1.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        MessageBox.Show(translationManager.GetTranslation(StringKeys.PopupForm_PaymentsRequired),
                            translationManager.GetTranslation(StringKeys.PopupForm_ValidationError),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (itemId == -1)
                    {
                        CRUD.AddRecord(tableName, paymentValues);
                        this.Close();
                    }
                    if (itemId != -1)
                    {
                        CRUD.UpdateRecord(tableName, itemId, paymentValues);
                        this.Close();
                    }
                    break;
            }
        }

        private void WarehouseStockUpdater(int ID, int updateStock = 0)
        {
            int quantity = 0;
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT ID, Quantity FROM Warehouse " +
                                                $"WHERE ID = {ID}", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        quantity = reader.GetInt32(1);
                    }
                }
                warehouseStock = quantity;
                warehouseStock += updateStock < 0 ? Math.Abs(updateStock) : -updateStock;
                warehouseStock = Math.Max(0, warehouseStock);

                command.Connection = connection;
                command.CommandText = @"UPDATE Warehouse 
                                        SET Quantity = @Quantity
                                        WHERE ID = @ID";
                command.Parameters.AddWithValue("@Quantity", warehouseStock);
                command.Parameters.AddWithValue("@ID", ID);
                command.ExecuteNonQuery();
            }
        }

        private void CalculateTotalPrice(object? sender, EventArgs e)
        {
            if (int.TryParse(textBox3.Text, out int quantity) && double.TryParse(textBox4.Text, out double price))
            {
                double result = quantity * price;
                label8.Text = result.ToString("€ 0.00");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NumericChecking(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DecimalChecking(object? sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string currentText = textBox.Text;
                if (currentText == "." && e.KeyCode != Keys.Back)
                {
                    textBox.Text = "0" + currentText;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void PreventCharOnDecimal(object? sender, KeyPressEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string currentText = textBox.Text;
                if (char.IsControl(e.KeyChar))
                {
                    return;
                }
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == '.')
                {
                    if (currentText.Contains('.'))
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    if (currentText.Contains('.'))
                    {
                        string decimalPart = currentText.Substring(currentText.IndexOf('.') + 1);
                        if (decimalPart.Length >= 2)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                bool isDecimal = textBox.Tag?.ToString() == "decimal";

                string originalText = textBox.Text;
                string filteredText = string.Empty;
                if (isDecimal)
                {
                    filteredText = Regex.Replace(originalText, @"[^\d.]", "");
                    filteredText = Regex.Match(filteredText, @"^\d*(\.\d{0,2})?").Value;
                }
                else
                {
                    filteredText = Regex.Replace(originalText, @"[^\d]", "");
                }
                if (originalText != filteredText)
                {
                    textBox.Text = filteredText;
                    textBox.SelectionStart = filteredText.Length;
                }
            }
        }

        private void ComboBoxExitValidation(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                string enteredText = comboBox.Text;
                if (string.IsNullOrEmpty(enteredText))
                {
                    return;
                }
                var matchingItem = comboBox.Items.Cast<KeyValuePair<int, string>>()
           .FirstOrDefault(item => item.Value.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase));

                if (!matchingItem.Equals(default(KeyValuePair<int, string>)))
                {
                    comboBox.SelectedItem = matchingItem;
                }
                else
                {
                    comboBox.SelectedItem = null;
                }
                comboBox.DroppedDown = false;
            }

        }
    }

}

