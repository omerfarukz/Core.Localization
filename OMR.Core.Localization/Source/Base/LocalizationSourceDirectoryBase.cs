﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OMR.Core.Localization.Localizable;

namespace OMR.Core.Localization.Source.Base
{
    public abstract class LocalizationSourceDirectoryBase : LocalizationSourceBase
    {
        protected readonly string _directoryPath;
        protected readonly string _fileExtension;

        public LocalizationSourceDirectoryBase(string directoryPath, string fileExtension) : base(null)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentNullException(nameof(directoryPath));

            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException();

            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentNullException(nameof(fileExtension));

            _directoryPath = directoryPath;
            _fileExtension = fileExtension;
        }

        public override bool CanLocalize(string name, CultureInfo cultureInfo)
        {
            string filePath = GetFilePathFrom(cultureInfo);
            if (!File.Exists(filePath))
                return false;

            return GetSource(filePath, cultureInfo).CanLocalize(name, cultureInfo);
        }

        public override IEnumerable<ILocalizable> GetAll()
        {
            foreach (var filePath in EnumerateFiles())
            {
                var source = GetSource(filePath, null);
                foreach (var item in source.GetAll())
                {
                    yield return item;
                }
            }
        }

        public override object Localize(string name, CultureInfo cultureInfo)
        {
            string filePath = GetFilePathFrom(cultureInfo);
            var source = GetSource(filePath, cultureInfo);
            return source.Localize(name, cultureInfo);
        }

		public override Dictionary<string, string> GetDictionary()
		{
			var result = new Dictionary<string, string>();
			foreach (var filePath in EnumerateFiles())
			{
                var cultureName = Path.GetFileNameWithoutExtension(filePath);
                var targetCulture = new CultureInfo(cultureName);
				
                var source = GetSource(filePath, targetCulture).GetDictionary();
				foreach (var kvp in source)
				{
					result.Add(kvp.Key, kvp.Value);
				}
			}

			return result;
		}

        public abstract LocalizationSourceBase GetSource(string filePath, CultureInfo cultureInfo);

        protected IEnumerable<string> EnumerateFiles()
        {
            return Directory.GetFiles(_directoryPath, $"*.{_fileExtension}");
        }

        private string GetFilePathFrom(CultureInfo cultureInfo)
        {
            var fileName = $"{cultureInfo.Name}.{_fileExtension}";
            var filePath = Path.Combine(_directoryPath, fileName);
            return filePath;
        }
    }
}