﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#elif __IOS__
using NUnit.Framework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Microsoft.Practices.Unity.TestSupport
{
    public static class EnumerableAssertionExtensions
    {
        public static void AssertContainsExactly<TItem>(this IEnumerable<TItem> items, params TItem[] expected)
        {
            CollectionAssertExtensions.AreEqual(expected, items.ToArray());
        }

        public static void AssertContainsInAnyOrder<TItem>(this IEnumerable<TItem> items, params TItem[] expected)
        {
            CollectionAssertExtensions.AreEquivalent(expected, items.ToArray());
        }

        public static void AssertTrueForAll<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Assert.IsTrue(items.All(predicate));
        }

        public static void AssertTrueForAny<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Assert.IsTrue(items.Any(predicate));
        }

        public static void AssertFalseForAll<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Assert.IsFalse(items.All(predicate));
        }

        public static void AssertFalseForAny<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            Assert.IsFalse(items.Any(predicate));
        }

        public static void AssertHasItems<TItem>(this IEnumerable<TItem> items)
        {
            Assert.IsTrue(items.Any());
        }

        public static void AssertHasNoItems<TItem>(this IEnumerable<TItem> items)
        {
            Assert.IsFalse(items.Any());
        }
    }
}
