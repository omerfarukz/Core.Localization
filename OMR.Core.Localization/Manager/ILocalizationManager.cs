using System.Globalization;

namespace OMR.Core.Localization.Manager
{
    public interface ILocalizationManager
    {
        LocalizationResult Localize(string name, CultureInfo cultureInfo);
    }
}