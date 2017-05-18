using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using OMR.Core.Localization.Localizable;

namespace OMR.Core.Localization.Source
{
    public abstract class LocalizationSourceBase : ILocalizationSource
    {
        protected readonly string _filePath;
        protected readonly CultureInfo _cultureInfo;

        public LocalizationSourceBase(string filePath, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            _filePath = filePath;
            _cultureInfo = cultureInfo;
        }

        public bool CanLocalize(string name, CultureInfo cultureInfo)
        {
            if (_cultureInfo.Name != cultureInfo.Name)
                return false;

            var localizations = ReadDocument();
            return localizations.Any(f => f.Key == name);
        }

        public IEnumerable<ILocalizable> GetAll()
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(_filePath);
            var cultureInfo = new CultureInfo(fileNameWithoutExtension);

            var localizations = ReadDocument();
            foreach (var localization in localizations)
            {
                var key = GetKey(cultureInfo, localization.Key);
                yield return new LocalizableString()
                {
                    Key = $"{fileNameWithoutExtension}#{localization.Key}",
                    Name = localization.Key,
                    Value = localization.Value
                };
            }
        }

        public object Localize(string name, CultureInfo cultureInfo)
        {
            var localizations = ReadDocument();
            return localizations.Single(f => f.Key == name).Value;
        }

		public abstract Dictionary<string, string> ReadDocument();

		private string GetKey(CultureInfo culture, string key)
        {
            return $"{culture.Name}#{key}";
        }
    }
}