using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OMR.Core.Localization.Source.Base;

namespace OMR.Core.Localization.Source.Json
{
    public class JsonLocalizationSource : LocalizationSourceFileBase
    {
        public JsonLocalizationSource(string filePath, CultureInfo cultureInfo) : base(filePath, cultureInfo)
        {}

        public override Dictionary<string, string> GetDictionary()
        {
			var serializer = new JsonDictionarySerializer();
            var fileContent = File.ReadAllText(_filePath);
            return serializer.DeserializeDictionaryStringToString(fileContent); ;
        }
    }
}
