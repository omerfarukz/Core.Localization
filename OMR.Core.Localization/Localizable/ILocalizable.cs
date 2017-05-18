using System.Globalization;

namespace OMR.Core.Localization.Localizable
{
    public interface ILocalizable
    {
        string Key { get; set; }
        string Name { get; set; }
        object GetValue();
    }
}