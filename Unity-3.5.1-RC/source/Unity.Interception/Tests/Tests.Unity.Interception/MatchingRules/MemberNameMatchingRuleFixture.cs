﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.Unity.InterceptionExtension.Tests.MatchingRules
{
    [TestClass]
    public class MemberNameMatchingRuleFixture
    {
        private MethodInfo methodOne;
        private MethodInfo methodTwo;
        private MethodInfo save;
        private MethodInfo reset;
        private MethodInfo closeAndReset;

        [TestInitialize]
        public void Setup()
        {
            methodOne = typeof(TestTarget).GetMethod("MethodOne");
            methodTwo = typeof(TestTarget).GetMethod("MethodTwo");
            save = typeof(TestTarget).GetMethod("Save");
            reset = typeof(TestTarget).GetMethod("Reset");
            closeAndReset = typeof(TestTarget).GetMethod("CloseAndReset");
        }

        [TestMethod]
        public void ShouldMatchExactName()
        {
            MemberNameMatchingRule rule = new MemberNameMatchingRule("Save");
            Assert.IsTrue(rule.Matches(save));
        }

        [TestMethod]
        public void ShouldMatchWithWildcard()
        {
            MemberNameMatchingRule rule = new MemberNameMatchingRule("*Reset");
            foreach (MethodInfo method in new MethodInfo[] { reset, closeAndReset })
            {
                Assert.IsTrue(rule.Matches(method),
                              "Match failed for method {0}", method.Name);
            }
        }

        [TestMethod]
        public void ShouldMatchMultipleMethods()
        {
            IMatchingRule rule = new MemberNameMatchingRule(
                new string[] { "MethodTwo", "Save" });
            Assert.IsFalse(rule.Matches(methodOne));
            Assert.IsTrue(rule.Matches(methodTwo));
            Assert.IsTrue(rule.Matches(save));
        }

        [TestMethod]
        public void ShouldMatchMultipleWildcards()
        {
            IMatchingRule rule = new MemberNameMatchingRule(
                new string[] { "Method*", "*Reset" });
            foreach (MethodInfo method in new MethodInfo[] { methodOne, methodTwo, reset, closeAndReset })
            {
                Assert.IsTrue(rule.Matches(method),
                              "Match failed for method {0}", method.Name);
            }
        }
    }

    public class TestTarget
    {
        public void CloseAndReset() { }

        public void MethodOne() { }

        public void MethodTwo() { }

        public void Reset() { }

        public void Save() { }
    }
}
