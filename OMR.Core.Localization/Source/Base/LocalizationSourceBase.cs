﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OMR.Core.Localization.Localizable;

namespace OMR.Core.Localization.Source.Base
{
    public abstract class LocalizationSourceBase : ILocalizationSource
    {
        protected readonly CultureInfo _cultureInfo;

        public LocalizationSourceBase(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
        }

		public abstract Dictionary<string, string> GetDictionary();
		
        public virtual bool CanLocalize(string name, CultureInfo cultureInfo)
        {
            if (_cultureInfo.Name != cultureInfo.Name)
                return false;

            var localizations = GetDictionary();
            return localizations.Any(f => f.Key == name);
        }

        public virtual IEnumerable<ILocalizable> GetAll()
        {
            var localizations = GetDictionary();
            foreach (var localization in localizations)
            {
                var key = GetKey(localization.Key);
                yield return new LocalizableString()
                {
                    Key = key,
                    Name = localization.Key,
                    Value = localization.Value
                };
            }
        }

        public virtual object Localize(string name, CultureInfo cultureInfo)
        {
            var localizations = GetDictionary();
            return localizations.Single(f => f.Key == name).Value;
        }

		protected string GetKey(string key)
        {
            return $"{_cultureInfo?.Name}#{key}";
        }
    }
}