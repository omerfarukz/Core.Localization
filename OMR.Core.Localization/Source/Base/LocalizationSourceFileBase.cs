﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace OMR.Core.Localization.Source.Base
{
    public abstract class LocalizationSourceFileBase : LocalizationSourceBase
    {
		protected readonly string _filePath;

        public LocalizationSourceFileBase(string filePath, CultureInfo cultureInfo) : base(cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            _filePath = filePath;
        }
    }
}
