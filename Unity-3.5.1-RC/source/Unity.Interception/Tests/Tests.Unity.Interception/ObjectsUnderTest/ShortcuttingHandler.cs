﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace Microsoft.Practices.Unity.InterceptionExtension.Tests.ObjectsUnderTest
{
    public class ShortcuttingHandler : ICallHandler
    {
        private string shortcutKey;
        private int order = 0;

        public ShortcuttingHandler(string shortcutKey)
        {
            this.shortcutKey = shortcutKey;
        }

        /// <summary>
        /// Gets or sets the order in which the handler will be executed
        /// </summary>
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            string key = (string)input.Inputs[0];
            if (key == shortcutKey)
            {
                IMethodReturn result = input.CreateMethodReturn(-1);
                return result;
            }
            return getNext()(input, getNext);
        }
    }
}
