﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Practices.Unity.InterceptionExtension.Configuration.Tests.TestObjects
{
    public class MyTransparentProxyInterceptorTypeConverter : TypeConverter
    {
        public static string SourceValue;

        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            SourceValue = (string)value;
            return new TransparentProxyInterceptor();
        }
    }
}
