namespace TechChallenge.Core.Services
{
    public interface IPreferencesService
    {
        string GetPreference(string key);

        void SetPreference(string key, string value);
    }
}
