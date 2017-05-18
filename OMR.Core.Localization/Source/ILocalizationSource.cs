using System.Collections.Generic;
using System.Globalization;
using OMR.Core.Localization.Localizable;

namespace OMR.Core.Localization.Source
{
	public interface ILocalizationSource
	{
		bool CanLocalize(string name, CultureInfo cultureInfo);
		object Localize(string name, CultureInfo cultureInfo);
		IEnumerable<ILocalizable> GetAll();
	}
}
