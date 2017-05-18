﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OMR.Core.Localization.Localizable;

namespace OMR.Core.Localization.Source.Base
{
    public abstract class LocalizationSourceDictionaryBase : LocalizationSourceBase
    {
        protected readonly Dictionary<string, string> _dictionary;

        public LocalizationSourceDictionaryBase(Dictionary<string, string> dictionary, CultureInfo cultureInfo) 
            : base(cultureInfo)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            _dictionary = dictionary;
        }

        public override bool CanLocalize(string name, CultureInfo cultureInfo)
        {
            if (_cultureInfo.Name != cultureInfo.Name)
                return false;

            var localizations = GetDictionary();
            return localizations.Any(f => f.Key == name);
        }

        public override IEnumerable<ILocalizable> GetAll()
        {
            foreach (var kvp in _dictionary)
            {
                var key = GetKey(kvp.Key);
                yield return new LocalizableString()
                {
                    Key = $"{nameof(Dictionary<string,string>)}#{kvp.Key}",
                    Name = kvp.Key,
                    Value = kvp.Value
                };
            }
        }

        public override object Localize(string name, CultureInfo cultureInfo)
        {
            var localizations = GetDictionary();
            return localizations.Single(f => f.Key == name).Value;
        }
    }
}