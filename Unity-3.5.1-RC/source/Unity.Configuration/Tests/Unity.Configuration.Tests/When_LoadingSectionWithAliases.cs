﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.Configuration.Tests.ConfigFiles;
using Microsoft.Practices.Unity.TestSupport;
using Microsoft.Practices.Unity.TestSupport.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.Unity.Configuration.Tests
{
    /// <summary>
    /// Summary description for When_LoadingSectionWithAliases
    /// </summary>
    [TestClass]
    public class When_LoadingSectionWithAliases : SectionLoadingFixture<ConfigFileLocator>
    {
        public When_LoadingSectionWithAliases()
            : base("TwoContainersAndAliases")
        {
        }

        [TestMethod]
        public void Then_AliasesAreAvailableInTheSection()
        {
            Assert.IsNotNull(section.TypeAliases);
        }

        [TestMethod]
        public void Then_ExpectedNumberOfAliasesArePresent()
        {
            Assert.AreEqual(2, section.TypeAliases.Count);
        }

        [TestMethod]
        public void Then_IntIsMappedToSystemInt32()
        {
            Assert.AreEqual("System.Int32, mscorlib", section.TypeAliases["int"]);
        }

        [TestMethod]
        public void Then_StringIsMappedToSystemString()
        {
            Assert.AreEqual("System.String, mscorlib", section.TypeAliases["string"]);
        }

        [TestMethod]
        public void Then_EnumerationReturnsAliasesInOrderAsGivenInFile()
        {
            CollectionAssertExtensions.AreEqual(new[] { "int", "string" },
                section.TypeAliases.Select(alias => alias.Alias).ToList());
        }

        [TestMethod]
        public void Then_ContainersInTheFileAreAlsoLoaded()
        {
            Assert.AreEqual(2, section.Containers.Count);
        }
    }
}
