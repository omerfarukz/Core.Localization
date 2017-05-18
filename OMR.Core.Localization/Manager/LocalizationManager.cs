using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OMR.Core.Localization.Source;

namespace OMR.Core.Localization.Manager
{
    public class LocalizationManager : ILocalizationManager
    {
        private readonly IEnumerable<ILocalizationSource> _sources;
        private readonly LocalizationOptions _options;

        public LocalizationManager(LocalizationOptions options, IEnumerable<ILocalizationSource> sources)
        {
            _options = options;
            _sources = sources;
        }

        public LocalizationResult Localize(string name, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (cultureInfo == null)
                throw new ArgumentNullException(nameof(cultureInfo));

            LocalizationResult result = null;
            var source = _sources.FirstOrDefault(f => f.CanLocalize(name, cultureInfo));
            if (source != null)
            {
                var localized = source.Localize(name, cultureInfo);
                result = new LocalizationResult()
                {
                    CultureInfo = cultureInfo,
                    Name = name,
                    Source = source,
                    Value = localized
                };
            }
            else
            {
                if (_options.ReturnNameWhenNotFound)
                {
                    result = new LocalizationResult()
                    {
                        CultureInfo = cultureInfo,
                        Name = name,
                        Source = source,
                        Value = _options.ReturnBracketsOnUsingName ? $"[{name}]" : name
                    };
                }
            }

            return result;
        }
    }
}
