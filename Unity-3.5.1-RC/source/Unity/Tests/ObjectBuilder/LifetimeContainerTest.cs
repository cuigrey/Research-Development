﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#elif __IOS__
using NUnit.Framework;
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestInitializeAttribute = NUnit.Framework.SetUpAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Microsoft.Practices.ObjectBuilder2.Tests
{
    [TestClass]
    public class LifetimeContainerTest
    {
        [TestInitialize]
        public void Setup()
        {
            DisposeOrderCounter.ResetCount();
        }

        [TestMethod]
        public void CanDetermineIfLifetimeContainerContainsObject()
        {
            ILifetimeContainer container = new LifetimeContainer();
            object obj = new object();

            container.Add(obj);

            Assert.IsTrue(container.Contains(obj));
        }

        [TestMethod]
        public void CanEnumerateItemsInContainer()
        {
            ILifetimeContainer container = new LifetimeContainer();
            DisposableObject mdo = new DisposableObject();

            container.Add(mdo);

            int count = 0;
            bool foundMdo = false;

            foreach (object obj in container)
            {
                count++;

                if (ReferenceEquals(mdo, obj))
                {
                    foundMdo = true;
                }
            }

            Assert.AreEqual(1, count);
            Assert.IsTrue(foundMdo);
        }

        [TestMethod]
        public void ContainerEnsuresObjectsWontBeCollected()
        {
            ILifetimeContainer container = new LifetimeContainer();
            DisposableObject mdo = new DisposableObject();
            WeakReference wref = new WeakReference(mdo);

            container.Add(mdo);
            mdo = null;
            GC.Collect();

            Assert.AreEqual(1, container.Count);
            mdo = wref.Target as DisposableObject;
            Assert.IsNotNull(mdo);
            Assert.IsFalse(mdo.WasDisposed);
        }

        [TestMethod]
        public void DisposingContainerDisposesOwnedObjects()
        {
            ILifetimeContainer container = new LifetimeContainer();
            DisposableObject mdo = new DisposableObject();

            container.Add(mdo);
            container.Dispose();

            Assert.IsTrue(mdo.WasDisposed);
        }

        [TestMethod]
        public void DisposingItemsFromContainerDisposesInReverseOrderAdded()
        {
            ILifetimeContainer container = new LifetimeContainer();
            DisposeOrderCounter obj1 = new DisposeOrderCounter();
            DisposeOrderCounter obj2 = new DisposeOrderCounter();
            DisposeOrderCounter obj3 = new DisposeOrderCounter();

            container.Add(obj1);
            container.Add(obj2);
            container.Add(obj3);

            container.Dispose();

            Assert.AreEqual(1, obj3.DisposePosition);
            Assert.AreEqual(2, obj2.DisposePosition);
            Assert.AreEqual(3, obj1.DisposePosition);
        }

        [TestMethod]
        public void RemovingItemsFromContainerDoesNotDisposeThem()
        {
            ILifetimeContainer container = new LifetimeContainer();
            DisposableObject mdo = new DisposableObject();

            container.Add(mdo);
            container.Remove(mdo);
            container.Dispose();

            Assert.IsFalse(mdo.WasDisposed);
        }

        [TestMethod]
        public void RemovingNonContainedItemDoesNotThrow()
        {
            ILifetimeContainer container = new LifetimeContainer();

            container.Remove(new object());
        }

        private class DisposableObject : IDisposable
        {
            public bool WasDisposed = false;

            public void Dispose()
            {
                WasDisposed = true;
            }
        }

        private class DisposeOrderCounter : IDisposable
        {
            private static int count = 0;
            public int DisposePosition;

            public static void ResetCount()
            {
                count = 0;
            }

            public void Dispose()
            {
                DisposePosition = ++count;
            }
        }
    }
}
