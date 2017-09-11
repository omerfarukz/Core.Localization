# Localization library for dotnet core 
Localization library for dotnet core. Supports xml, json and any other types like Dictionary<string, string>.

## Setup
```cs
  var sources = new List<ILocalizationSource> {
      new StringSourceInMemory(),
      new XmlDirectoryLocalizationSource("localizations/others/xml"),
      new XmlLocalizationSource("localizations/main.xml", new CultureInfo("tr-TR")),
      new JsonDirectoryLocalizationSource("localizations/others/json"),
      new JsonLocalizationSource("localizations/main.json", new CultureInfo("en-US"))
  };
```

## Usage
```cs
  ILocalizationContext context = new DefaultLocalizationContext(man);
  context.CultureInfo = new CultureInfo(cultureName);
  var localizationResult = context.Localize(name);
```
