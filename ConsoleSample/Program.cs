using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using OMR.Core.Localization;
using OMR.Core.Localization.Context;
using OMR.Core.Localization.Localizable;
using OMR.Core.Localization.Manager;
using OMR.Core.Localization.Source;
using OMR.Core.Localization.Source.Json;
using OMR.Core.Localization.Source.Xml;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            RunLocalizationDemo();
        }

        private static void RunLocalizationDemo()
        {
            ILocalizationManager localizationManager = CreateManager();

            for (int i = 0; i < 10; i++)
            {
                PrintLocalized(localizationManager, $"name{i}", "en-US");
                PrintLocalized(localizationManager, $"name{i}", "tr-TR");
            }
        }

        private static ILocalizationManager CreateManager()
        {
            LocalizationOptions options = new LocalizationOptions();
            options.ReturnNameWhenNotFound = true;
            options.ReturnBracketsOnUsingName = true;

            var sources = new List<ILocalizationSource> {
                new StringSourceInMemory(),
                new XmlDirectoryLocalizationSource("xml"),
                new XmlLocalizationSource("demo.xml", new CultureInfo("tr-TR")),
                new JsonDirectoryLocalizationSource("json"),
                new JsonLocalizationSource("demo.json", new CultureInfo("en-US"))
            };

            ILocalizationManager localizationManager = new LocalizationManager(options, sources);
            return localizationManager;
        }

        private static void PrintLocalized(ILocalizationManager man, string name, string cultureName)
        {
            ILocalizationContext context = new DefaultLocalizationContext(man);
            context.CultureInfo = new CultureInfo(cultureName);
            var localizationResult = context.Localize(name);

            if (localizationResult != null)
            {
                Console.WriteLine($"{localizationResult.Value.ToString().PadRight(30)}{localizationResult.Source?.GetType().Name}");
            }
            else
            {
                Console.WriteLine($"NOT FOUND: {name}");
            }
        }

        class StringSourceInMemory : ILocalizationSource
        {
            private Dictionary<string, string> _values = new Dictionary<string, string>();

            public StringSourceInMemory()
            {
                _values.Add("tr-TR#name1", "ornek");
                _values.Add("tr-TR#name2", "sunum");

                _values.Add("en-US#name1", "example");
                _values.Add("en-US#name2", "presentation");
            }

            public bool CanLocalize(string name, CultureInfo cultureInfo)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                if (cultureInfo == null)
                    throw new ArgumentNullException(nameof(cultureInfo));

                string key = GetKey(name, cultureInfo);
                return _values.ContainsKey(key);
            }

            public IEnumerable<ILocalizable> GetAll()
            {
                return _values.Select(f => CreateLocalizableStringFrom(f.Key, f.Value));
            }

            public object Localize(string name, CultureInfo cultureInfo)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                if (cultureInfo == null)
                    throw new ArgumentNullException(nameof(cultureInfo));

                string key = GetKey(name, cultureInfo);

                if (!_values.ContainsKey(key))
                {
                    throw new InvalidProgramException($"Name not found {name} with key {key}");
                }
                return _values[key];
            }

            private ILocalizable CreateLocalizableStringFrom(string name, string stringValue)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                return new LocalizableString() { Name = name, Value = stringValue };
            }

            private string GetKey(string name, CultureInfo cultureInfo)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                if (cultureInfo == null)
                    throw new ArgumentNullException(nameof(cultureInfo));

                return $"{cultureInfo.Name}#{name}";
            }
        }
    }
}
