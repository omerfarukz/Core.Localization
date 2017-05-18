using System.Globalization;
using OMR.Core.Localization.Source;

namespace OMR.Core.Localization
{
    public class LocalizationResult
    {
        public string Name { get; set; }
        public CultureInfo CultureInfo { get; set; }
        public ILocalizationSource Source { get; set; }
        public virtual object Value { get; set; }
    }

    public class LocalizationResult<T> : LocalizationResult
    {
        public new T Value { get; set; }
    }
}