using System.Globalization;
using OMR.Core.Localization.Source.Base;

namespace OMR.Core.Localization.Source.Json
{
    public class JsonDirectoryLocalizationSource : LocalizationSourceDirectoryBase
    {
        public JsonDirectoryLocalizationSource(string directoryPath) : base(directoryPath, "json")
        {}

        public override LocalizationSourceBase GetSource(string filePath, CultureInfo cultureInfo)
        {
            return new JsonLocalizationSource(filePath, cultureInfo);
        }
    }
}