namespace OMR.Core.Localization.Localizable
{
	public class LocalizableString : ILocalizable
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public object GetValue()
		{
			return this.Value;
		}

		public static implicit operator string(LocalizableString localizableString)
		{
			return (string)localizableString?.GetValue();
		}
	}
}