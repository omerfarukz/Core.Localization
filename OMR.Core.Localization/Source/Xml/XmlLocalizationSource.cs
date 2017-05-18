using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using OMR.Core.Localization.Localizable;
using OMR.Core.Localization.Source.Base;
using OMR.Core.Localization.Source.Json;

namespace OMR.Core.Localization.Source.Xml
{
    public class XmlLocalizationSource : LocalizationSourceFileBase
    {
		public XmlLocalizationSource(string filePath, CultureInfo cultureInfo) : base(filePath, cultureInfo)
        { }
		
        public override Dictionary<string, string> GetDictionary()
        {
            var result = new Dictionary<string, string>();

            var document = GetXmlContentFrom();
            var strings = document.Element("localization").Element("strings").Elements("string");
            foreach (var item in strings)
            {
                result.Add(item.Attribute("name").Value, item.Attribute("value").Value);
            }

            return result.Count > 0 ? result : null;
        }

        private XDocument GetXmlContentFrom()
        {
            using (var fileStream = new FileStream(_filePath, FileMode.Open))
            {
                return XDocument.Load(fileStream);
            }
        }
    }
}
