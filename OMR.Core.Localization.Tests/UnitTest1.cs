using System;
using OMR.Core.Localization.Source.Xml;
using Xunit;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
using System.Linq;

namespace OMR.Core.Localization.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                          "<localization culture=\"en-US\">" +
                          " <strings>" +
                          "  <string name=\"name3\" value=\"hello xml\" />" +
                          "  <string name=\"name4\" value=\"greetings\" />" +
                          " </strings>" +
                          "</localization>";

            //var directory = "xml_source";
            //File.WriteAllText(Path.Combine($"{directory}\\enUS.xml"), content);

            //var source = new XmlLocalizationSource(content);
            //source.CanLocalize("name3", new CultureInfo("en-US"));

            var doc = XDocument.Parse(content);
            var culture = doc.Root.Attribute("culture").Value;

            var strings = doc.Element("localization").Element("strings").Elements("string").ToArray();
			foreach (var item in strings)
			{
				var val = item.Attribute("name").Value;
				var name = item.Attribute("value").Value;
			}
            Assert.False(true, culture);
            //Directory.Delete(directory, true);
        }
    }
}
