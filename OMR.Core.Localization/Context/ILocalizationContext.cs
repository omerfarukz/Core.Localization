using System.Globalization;

namespace OMR.Core.Localization.Context
{
	public interface ILocalizationContext
	{
		CultureInfo CultureInfo { get; set; }
		LocalizationResult Localize(string name);
	}
}
