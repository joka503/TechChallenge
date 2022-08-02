namespace TechChallenge.Core.Services
{
    public class PreferencesService : IPreferencesService
    {
        public string GetPreference(string key)
        {
            return Xamarin.Essentials.Preferences.Get(key, string.Empty);
        }

        public void SetPreference(string key, string value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }
    }
}
