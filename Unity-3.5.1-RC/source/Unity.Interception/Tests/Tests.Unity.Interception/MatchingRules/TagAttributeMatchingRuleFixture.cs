﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension.Tests.ObjectsUnderTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.Unity.InterceptionExtension.Tests.MatchingRules
{
    [TestClass]
    public class TagAttributeMatchingRuleFixture
    {
        [TestMethod]
        public void RuleMatchesWhenTagMatches()
        {
            MethodInfo method = typeof(AnnotatedWithTags).GetMethod("Tagged");
            TagAttributeMatchingRule rule = new TagAttributeMatchingRule("Tagged");

            Assert.IsTrue(rule.Matches(method));
        }

        [TestMethod]
        public void RuleCanMatchCaseInsensitive()
        {
            MethodInfo method = typeof(AnnotatedWithTags).GetMethod("Tagged");
            TagAttributeMatchingRule rule = new TagAttributeMatchingRule("taGGed", true);

            Assert.IsTrue(rule.Matches(method));
        }

        [TestMethod]
        public void RuleCanMatchesCaseSensitiveByDefault()
        {
            MethodInfo method = typeof(AnnotatedWithTags).GetMethod("Tagged");
            TagAttributeMatchingRule rule = new TagAttributeMatchingRule("taGGed");

            Assert.IsFalse(rule.Matches(method));
        }

        [TestMethod]
        public void RuleDeniesMatchWhenTagTextDoesNotCorrespond()
        {
            MethodInfo method = typeof(AnnotatedWithTags).GetMethod("Tagged");
            TagAttributeMatchingRule rule = new TagAttributeMatchingRule("WhichTag?");

            Assert.IsFalse(rule.Matches(method));
        }

        [TestMethod]
        public void RuleDeniesMatchWhenTagAttributeIsNotDeclared()
        {
            MethodInfo method = typeof(AnnotatedWithTags).GetMethod("NotTagged");
            TagAttributeMatchingRule rule = new TagAttributeMatchingRule("Tagged");

            Assert.IsFalse(rule.Matches(method));
        }

        [TestMethod]
        public void ShouldMatchRuleForTaggedProperty()
        {
            MethodInfo method = typeof(AnnotatedWithTags).GetProperty("Name").GetGetMethod();
            TagAttributeMatchingRule rule = new TagAttributeMatchingRule("Tagged");
            Assert.IsTrue(rule.Matches(method));
        }

        [TestMethod]
        public void ShouldMatchRuleForTaggedClass()
        {
            MethodInfo method1 = typeof(AnnotatedWithTagsOnClass).GetMethod("Method1");
            MethodInfo method2 = typeof(AnnotatedWithTagsOnClass).GetMethod("Method2");
            TagAttributeMatchingRule rule = new TagAttributeMatchingRule("Tagged");

            Assert.IsTrue(rule.Matches(method1));
            Assert.IsTrue(rule.Matches(method2));
        }

        [TestMethod]
        public void ShouldMatchForMethodWhenTagIsOnInterface()
        {
            MethodInfo createMethod = typeof(DaoImpl).GetMethod("Create");
            MethodInfo interfaceMethod = typeof(IDao).GetMethod("Create");
            TagAttributeMatchingRule rule = new TagAttributeMatchingRule("Tag on interface", true);
            Assert.IsFalse(rule.Matches(createMethod));
            Assert.IsTrue(rule.Matches(interfaceMethod));
        }

        //[TestMethod]
        //public void ShouldMatchForMethodWhenTagIsOnInterfaceViaPolicy()
        //{
        //    RuleDrivenPolicy policy = new RuleDrivenPolicy("Count tagged calls");
        //    policy.RuleSet.Add(new TagAttributeMatchingRule("Tag on interface", true));
        //    policy.Handlers.Add(new CallCountHandler());

        //    MethodInfo createMethod = typeof(DaoImpl).GetMethod("Create");
        //    List<ICallHandler> handlers = new List<ICallHandler>(policy.GetHandlersFor(createMethod));
        //    Assert.AreEqual(1, handlers.Count);
        //    Assert.IsTrue(handlers[0] is CallCountHandler);
        //}

        private class AnnotatedWithTags
        {
            [Tag("Tagged")]
            public string Name
            {
                get { return "Annotated"; }
            }

            public void NotTagged() { }

            [Tag("Tagged")]
            public void Tagged() { }
        }

        [Tag("Tagged")]
        private class AnnotatedWithTagsOnClass
        {
            public void Method1() { }

            public void Method2() { }
        }

        private interface IDao
        {
            [Tag("Tag on interface")]
            void Create();
        }

        private class DaoImpl : IDao
        {
            public void Create() { }
        }
    }
}
