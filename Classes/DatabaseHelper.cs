using System.Data;
using System.Data.SQLite;

namespace EasyInventory
{
    public static class DatabaseHelper
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EasyInventory.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE Customers (
                                                ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                                                Name TEXT NOT NULL, 
                                                [Main Phone] TEXT NOT NULL, 
                                                [Secondary Phone] TEXT NOT NULL, 
                                                Email TEXT, 
                                                Notes TEXT);";
                        command.ExecuteNonQuery();

                        command.CommandText = @"CREATE TABLE Warehouse (
                                                ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                                                [Item Name] TEXT NOT NULL, 
                                                [Item Description] TEXT, 
                                                Quantity INTEGER, 
                                                Price REAL, 
                                                SKU TEXT);";
                        command.ExecuteNonQuery();

                        command.CommandText = @"CREATE TABLE Sales (
                                                ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                                                Customer INTEGER, 
                                                Item INTEGER, 
                                                [Quantity Sold] INTEGER, 
                                                [Item Price] REAL, 
                                                [Total Price] REAL, 
                                                [Sale Date] TEXT DEFAULT (datetime('now')),  
                                                FOREIGN KEY(Customer) REFERENCES Customers(ID) ON DELETE SET NULL,
                                                FOREIGN KEY(Item) REFERENCES Warehouse(ID) ON DELETE SET NULL);";
                        command.ExecuteNonQuery();

                        command.CommandText = @"CREATE TABLE Payments (
                                                ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                                                Customer INTEGER, 
                                                Amount REAL, 
                                                [Payment Date] TEXT DEFAULT (datetime('now')), 
                                                FOREIGN KEY(Customer) REFERENCES Customers(ID) ON DELETE SET NULL)";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
    public partial class CRUD
    {
        private static TranslationManager translationManager;
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EasyInventory.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
        public static DataTable GetDataByTable(string tableName)
        {
            translationManager = new TranslationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));
            DataTable dataTable = new DataTable();
            string query = string.Empty;
            using (var connection = GetConnection())
            {
                connection.Open();
                switch (tableName)
                {
                    case "Customers":
                        query = "SELECT ID AS [#], " +
                                $"Name AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVName)}], " +
                                $"[Main Phone] AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVPrimaryPhone)}], " +
                                $"[Secondary Phone] AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVSecondaryPhone)}], " +
                                $"Email AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVEmail)}], " +
                                $"Notes AS [{translationManager.GetTranslation(StringKeys.CustomerWindow_cDGVNotes)}] " +
                                $"FROM {tableName}";
                        break;
                    case "Warehouse":
                        query = "SELECT ID AS [#], " +
                                $"[Item Name] AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVName)}], " +
                                $"[Item Description] AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVDescription)}], " +
                                $"Quantity AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVQuantity)}], " +
                                $"Price AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVPrice)}], " +
                                $"SKU AS [{translationManager.GetTranslation(StringKeys.WarehouseWindow_wDGVBarcode)}] " +
                                $"FROM {tableName}";
                        break;
                    case "Sales":
                        query = "SELECT Sales.ID AS [#], " +
                               $"Customers.Name AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVName)}], " +
                               $"Warehouse.[Item Name] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVItem)}], " +
                               $"Sales.[Quantity Sold] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVQuantitySold)}], " +
                               $"Sales.[Item Price] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVItemPrice)}], " +
                               $"Sales.[Total Price] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVTotalPrice)}], " +
                               $"Sales.[Sale Date] AS [{translationManager.GetTranslation(StringKeys.SalesWindow_sDGVSaleDate)}]" +
                               $"FROM {tableName} " +
                               "JOIN Customers ON Sales.Customer = Customers.ID " +
                               "JOIN Warehouse ON Sales.Item = Warehouse.ID;";
                        break;
                    case "Payments":
                        query = "SELECT Payments.ID AS [#], " +
                                $"Customers.Name AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVName)}], " +
                                $"Payments.Amount AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVAmount)}], " +
                                $"Payments.[Payment Date] AS [{translationManager.GetTranslation(StringKeys.PaymentsWindow_pDGVPaymentDate)}] " +
                                $"FROM {tableName} " +
                                "JOIN Customers ON Payments.Customer = Customers.ID";
                        break;
                }
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }


        public static void AddRecord(string tableName, object[] values)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand())
                {
                    command.Connection = connection;
                    switch (tableName)
                    {
                        case "Customers":
                            command.CommandText = @"INSERT INTO Customers (Name, [Main Phone], [Secondary Phone], Email, Notes) 
                                                    VALUES (@Name, @MainPhone, @SecondaryPhone, @Email, @Notes)";
                            command.Parameters.AddWithValue("@Name", values[0]);
                            command.Parameters.AddWithValue("@MainPhone", values[1]);
                            command.Parameters.AddWithValue("@SecondaryPhone", values[2]);
                            command.Parameters.AddWithValue("@Email", values[3]);
                            command.Parameters.AddWithValue("@Notes", values[4]);
                            break;
                        case "Warehouse":
                            command.CommandText = @"INSERT INTO Warehouse ([Item Name], [Item Description], Quantity, Price, SKU) 
                                                    VALUES (@ItemName, @ItemDescription, @Quantity, @Price, @SKU)";
                            command.Parameters.AddWithValue("@ItemName", values[0]);
                            command.Parameters.AddWithValue("@ItemDescription", values[1]);
                            command.Parameters.AddWithValue("@Quantity", values[2]);
                            command.Parameters.AddWithValue("@Price", values[3]);
                            command.Parameters.AddWithValue("@SKU", values[4]);
                            break;
                        case "Sales":
                            command.CommandText = @"INSERT INTO Sales (Customer, Item, [Quantity Sold], [Item Price], [Total Price], [Sale Date])
                                                    VALUES (@Customer, @Item, @QuantitySold, @ItemPrice, @TotalPrice, @SaleDate)";
                            command.Parameters.AddWithValue("@Customer", values[0]);
                            command.Parameters.AddWithValue("@Item", values[1]);
                            command.Parameters.AddWithValue("@QuantitySold", values[2]);
                            command.Parameters.AddWithValue("@ItemPrice", values[3]);
                            command.Parameters.AddWithValue("@TotalPrice", values[4]);
                            command.Parameters.AddWithValue("@SaleDate", values[5]);
                            break;
                        case "Payments":
                            command.CommandText = @"INSERT INTO Payments (Customer, Amount, [Payment Date])
                                                    VALUES (@Customer, @Amount, @PaymentDate)";
                            command.Parameters.AddWithValue("@Customer", values[0]);
                            command.Parameters.AddWithValue("@Amount", values[1]);
                            command.Parameters.AddWithValue("PaymentDate", values[2]);
                            break;
                    }
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void UpdateRecord(string tableName, int id, object[] values)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand())
                {
                    command.Connection = connection;
                    switch (tableName)
                    {
                        case "Customers":
                            command.CommandText = @"UPDATE Customers 
                                                    SET Name = @Name, 
                                                        [Main Phone] = @MainPhone, 
                                                        [Secondary Phone] = @SecondaryPhone, 
                                                        Email = @Email, 
                                                        Notes = @Notes 
                                                    WHERE ID = @ID";
                            command.Parameters.AddWithValue("@Name", values[0]);
                            command.Parameters.AddWithValue("@MainPhone", values[1]);
                            command.Parameters.AddWithValue("@SecondaryPhone", values[2]);
                            command.Parameters.AddWithValue("@Email", values[3]);
                            command.Parameters.AddWithValue("@Notes", values[4]);
                            command.Parameters.AddWithValue("@ID", id);
                            break;
                        case "Warehouse":
                            command.CommandText = @"UPDATE Warehouse 
                                                    SET [Item Name] = @ItemName, 
                                                        [Item Description] = @ItemDescription, 
                                                        Quantity = @Quantity, 
                                                        Price = @Price,
                                                        SKU = @SKU 
                                                    WHERE ID = @ID";
                            command.Parameters.AddWithValue("@ItemName", values[0]);
                            command.Parameters.AddWithValue("@ItemDescription", values[1]);
                            command.Parameters.AddWithValue("@Quantity", values[2]);
                            command.Parameters.AddWithValue("@Price", values[3]);
                            command.Parameters.AddWithValue("@SKU", values[4]);
                            command.Parameters.AddWithValue("@ID", id);
                            break;
                        case "Sales":
                            command.CommandText = @"UPDATE Sales
                                                    SET Customer = @Customer,
                                                        Item = @Item,
                                                        [Quantity Sold] = @QuantitySold,
                                                        [Item Price] = @ItemPrice,
                                                        [Total Price] = @TotalPrice,
                                                        [Sale Date] = @SaleDate
                                                    WHERE ID = @ID";
                            command.Parameters.AddWithValue("@Customer", values[0]);
                            command.Parameters.AddWithValue("@Item", values[1]);
                            command.Parameters.AddWithValue("@QuantitySold", values[2]);
                            command.Parameters.AddWithValue("@ItemPrice", values[3]);
                            command.Parameters.AddWithValue("@TotalPrice", values[4]);
                            command.Parameters.AddWithValue("@SaleDate", values[5]);
                            command.Parameters.AddWithValue("@ID", id);
                            break;
                        case "Payments":
                            command.CommandText = @"UPDATE Payments
                                                    SET Customer = @Customer,
                                                        Amount = @Amount,
                                                        [Payment Date] = @PaymentDate
                                                    WHERE ID = @ID";
                            command.Parameters.AddWithValue("@Customer", values[0]);
                            command.Parameters.AddWithValue("@Amount", values[1]);
                            command.Parameters.AddWithValue("@PaymentDate", values[2]);
                            command.Parameters.AddWithValue("@ID", id);
                            break;
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteRecord(string tableName, int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"DELETE FROM {tableName} WHERE ID = @ID";
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
