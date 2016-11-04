﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace Microsoft.Practices.ObjectBuilder2.Tests.TestDoubles
{
    public class CurrentOperationSensingResolverPolicy<T> : IDependencyResolverPolicy
    {
        public object CurrentOperation;

        public object Resolve(IBuilderContext context)
        {
            this.CurrentOperation = context.CurrentOperation;

            return default(T);
        }
    }
}
