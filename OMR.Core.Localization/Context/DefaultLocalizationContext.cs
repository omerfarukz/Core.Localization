using System;
using System.Globalization;
using OMR.Core.Localization.Manager;

namespace OMR.Core.Localization.Context
{
    public class DefaultLocalizationContext : ILocalizationContext
    {
        private readonly ILocalizationManager _localizationManager;
        public CultureInfo CultureInfo { get; set; }

        public DefaultLocalizationContext(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        public LocalizationResult Localize(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _localizationManager.Localize(name, CultureInfo);
        }
    }
}