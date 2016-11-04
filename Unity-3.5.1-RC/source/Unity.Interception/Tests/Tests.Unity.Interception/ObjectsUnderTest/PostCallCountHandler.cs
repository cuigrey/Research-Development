﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.Unity.InterceptionExtension.Tests.ObjectsUnderTest
{
    internal class PostCallCountHandler : ICallHandler
    {
        private int order;
        private int callsCompleted = 0;

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result = getNext()(input, getNext);
            callsCompleted++;
            return result;
        }

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public int CallsCompleted
        {
            get { return callsCompleted; }
        }
    }
}
