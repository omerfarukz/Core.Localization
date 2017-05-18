using System.Collections.Generic;
using System.Globalization;
using OMR.Core.Localization.Source.Base;

namespace OMR.Core.Localization.Source.Xml
{
    public class XmlDirectoryLocalizationSource : LocalizationSourceDirectoryBase
    {
        public XmlDirectoryLocalizationSource(string directoryPath) : base(directoryPath, "xml")
        {}

        public override LocalizationSourceBase GetSource(string filePath, CultureInfo cultureInfo)
        {
            return new XmlLocalizationSource(filePath, cultureInfo);
        }
    }
}