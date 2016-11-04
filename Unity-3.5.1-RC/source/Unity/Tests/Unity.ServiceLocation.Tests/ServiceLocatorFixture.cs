﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.ServiceLocation.Tests.Components;
#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#elif __IOS__
using NUnit.Framework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Unity.ServiceLocation.Tests
{
    /// <summary>
    /// Base class for tests for an adapter for the <see cref="IServiceLocator"/>
    /// interface that are independent of the actual container. Subclass this
    /// to provide your actual container implementation to test.
    /// </summary>
    public abstract class ServiceLocatorFixture
    {
        protected IServiceLocator locator;

        protected abstract IServiceLocator CreateServiceLocator();

        public void GetInstance()
        {
            ILogger instance = locator.GetInstance<ILogger>();
            Assert.IsNotNull(instance);
        }

        public void AskingForInvalidComponentShouldRaiseActivationException()
        {
            AssertThrows<ActivationException>(() => locator.GetInstance<IDictionary>());
        }

        public void GetNamedInstance()
        {
            ILogger instance = locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName);
            Microsoft.Practices.Unity.TestSupport.AssertExtensions.IsInstanceOfType(instance, typeof(AdvancedLogger));
        }

        public void GetNamedInstance2()
        {
            ILogger instance = locator.GetInstance<ILogger>(typeof(SimpleLogger).FullName);
            Microsoft.Practices.Unity.TestSupport.AssertExtensions.IsInstanceOfType(instance, typeof(SimpleLogger));
        }

        public void GetUnknownInstance2()
        {
            AssertThrows<ActivationException>(() => locator.GetInstance<ILogger>("test"));
        }

        public void GetAllInstances()
        {
            IEnumerable<ILogger> instances = locator.GetAllInstances<ILogger>();
            IList<ILogger> list = new List<ILogger>(instances);
            Assert.AreEqual(2, list.Count);
        }

        public void GetAllInstance_ForUnknownType_ReturnEmptyEnumerable()
        {
            IEnumerable<IDictionary> instances = locator.GetAllInstances<IDictionary>();
            IList<IDictionary> list = new List<IDictionary>(instances);
            Assert.AreEqual(0, list.Count);
        }

        public void GenericOverload_GetInstance()
        {
            Assert.AreEqual(
                locator.GetInstance<ILogger>().GetType(),
                locator.GetInstance(typeof(ILogger), null).GetType());
        }

        public void GenericOverload_GetInstance_WithName()
        {
            Assert.AreEqual(
                locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName).GetType(),
                locator.GetInstance(typeof(ILogger), typeof(AdvancedLogger).FullName).GetType());
        }

        public void Overload_GetInstance_NoName_And_NullName()
        {
            Assert.AreEqual(
                locator.GetInstance<ILogger>().GetType(),
                locator.GetInstance<ILogger>(null).GetType());
        }

        public void GenericOverload_GetAllInstances()
        {
            List<ILogger> genericLoggers = new List<ILogger>(locator.GetAllInstances<ILogger>());
            List<object> plainLoggers = new List<object>(locator.GetAllInstances(typeof(ILogger)));
            Assert.AreEqual(genericLoggers.Count, plainLoggers.Count);
            for (int i = 0; i < genericLoggers.Count; i++)
            {
                Assert.AreEqual(
                    genericLoggers[i].GetType(),
                    plainLoggers[i].GetType());
            }
        }

        private static void AssertThrows<TException>(Action action)
            where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException)
            {
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected exception {0}, but instead exception {1} was thrown",
                    typeof(TException).Name,
                    ex.GetType().Name);
            }
            Assert.Fail("Expected exception {0}, no exception thrown", typeof(TException).Name);
        }
    }
}
