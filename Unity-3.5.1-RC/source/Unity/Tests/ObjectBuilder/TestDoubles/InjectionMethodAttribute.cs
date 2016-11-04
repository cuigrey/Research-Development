﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.ObjectBuilder2.Tests.TestDoubles
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectionMethodAttribute : Attribute
    {
    }
}
