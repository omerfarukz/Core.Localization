using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OMR.Core.Localization.Source.Json
{
	internal class JsonDictionarySerializer
	{
        public Dictionary<string, string> DeserializeDictionaryStringToString(string json)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (var reader = new StringReader(json))
            {
                var begining = ReadUntil(reader, "{");
                var dictionary = ReadUntil(reader, "}");
                var pairs = dictionary.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (pairs != null)
                {
                    foreach (var pair in pairs)
                    {
                        var nameValueAsString = pair.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        string nameAsString = nameValueAsString[0].Trim().Trim('\"');
                        string valueAsString = nameValueAsString[1].Trim().Trim('\"');
                        result.Add(nameAsString, valueAsString);
                    }
                }
            }

            return result.Count > 0 ? result : null;
        }

		private string ReadUntil(StringReader reader, string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				throw new ArgumentException(nameof(text));

			StringBuilder result = new StringBuilder();
			char[] buffer = new char[text.Length];
			char[] textAsCharArray = text.ToCharArray();

			while (reader.Peek() > -1)
			{
				reader.ReadBlock(buffer, 0, buffer.Length);

				if (Enumerable.SequenceEqual(buffer, textAsCharArray))
				{
					break;
				}

				result.Append(buffer);
			}

			return result.ToString();
		}
	}
}
