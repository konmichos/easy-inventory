namespace EasyInventory
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");
            if (!File.Exists(settingsPath))
            {
                var defaultSettings = new
                {
                    BackupFrequency = "0",
                    LastBackup = "1970-01-01 00:00:00",
                    Language = "English (US)"
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(defaultSettings, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(settingsPath, json);
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new MainProgram());
        }
    }
}