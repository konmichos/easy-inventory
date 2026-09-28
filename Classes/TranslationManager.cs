using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class TranslationManager
{
    private Dictionary<string, object> translations;
    private string language;

    public TranslationManager(string settingsFilePath)
    {
        language = LoadLanguageFromSettings(settingsFilePath);
        LoadTranslations(language);
    }

    private string LoadLanguageFromSettings(string settingsFilePath)
    {
        if (!File.Exists(settingsFilePath))
        {
            throw new FileNotFoundException($"Settings file not found: {settingsFilePath}");
        }

        string settingsContent = File.ReadAllText(settingsFilePath);

        var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(settingsContent);

        if (settings.ContainsKey("Language"))
        {
            return settings["Language"];
        }

        throw new Exception("Language setting not found in the settings file.");
    }

    private void LoadTranslations(string language)
    {
        string jsonFilePath = $"Language/{language}.json";

        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException($"Translation file not found for language {language}: {jsonFilePath}");
        }

        string jsonContent = File.ReadAllText(jsonFilePath);

        translations = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);
    }

    public string GetTranslation(StringKeys key)
    {
        var keyString = key.ToString();

        var keyParts = keyString.Split('_');
        if (keyParts.Length != 2)
        {
            throw new ArgumentException("Invalid enum key format.");
        }

        string category = keyParts[0];

        string specificKey = keyParts[1];

        if (translations.ContainsKey(category))
        {
            var categoryData = translations[category] as JObject;

            if (categoryData != null && categoryData.ContainsKey(specificKey))
            {
                return categoryData[specificKey]?.ToString() ?? $"[Translation not found for {keyString}]";
            }
        }

        return $"[Translation not found for {keyString}]";
    }
}
