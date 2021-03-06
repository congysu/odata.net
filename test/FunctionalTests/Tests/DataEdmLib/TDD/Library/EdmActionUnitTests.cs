﻿//---------------------------------------------------------------------
// <copyright file="EdmActionUnitTests.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.Test.Edm.TDD.Tests
{
    using FluentAssertions;
    using Microsoft.OData.Edm;
    using Microsoft.OData.Edm.Library;
    using Microsoft.OData.Edm.Library.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EdmActionUnitTests
    {
        private IEdmPrimitiveTypeReference boolType;
        private IEdmEntityType personType;
        private static string defaultNamespaceName = "DefaultNamespace";
        private static string checkout = "Checkout";

        public EdmActionUnitTests()
        {
            this.boolType = EdmCoreModel.Instance.GetBoolean(false);
            this.personType = new EdmEntityType(defaultNamespaceName, "Person");
        }

        [TestMethod]
        public void EdmActionConstructorWithNullReturnTypeShouldNotThrow()
        {
            var edmAction = new EdmAction(defaultNamespaceName, checkout, null);
            edmAction.Namespace.Should().Be(defaultNamespaceName);
            edmAction.Name.Should().Be(checkout);
            edmAction.ReturnType.Should().BeNull();
        }

        [TestMethod]
        public void EdmActionConstructorShouldDefaultNonSpecifiedPropertiesCorrectly()
        {
            var edmAction = new EdmAction(defaultNamespaceName, checkout, this.boolType);
            edmAction.Namespace.Should().Be(defaultNamespaceName);
            edmAction.Name.Should().Be(checkout);
            edmAction.ReturnType.Should().Be(this.boolType);
            edmAction.EntitySetPath.Should().BeNull();
            edmAction.IsBound.Should().BeFalse();
            edmAction.SchemaElementKind.Should().Be(EdmSchemaElementKind.Action);
        }

        [TestMethod]
        public void EdmActionConstructorShouldHaveSpecifiedConstructorValues()
        {
            var entitySetPath = new EdmPathExpression("Param1/Nav");
            var edmAction = new EdmAction(defaultNamespaceName, checkout, this.boolType, true, entitySetPath);
            edmAction.AddParameter(new EdmOperationParameter(edmAction, "Param1", new EdmEntityTypeReference(personType, false)));
            edmAction.Namespace.Should().Be(defaultNamespaceName);
            edmAction.Name.Should().Be(checkout);
            edmAction.ReturnType.Should().Be(this.boolType);
            edmAction.EntitySetPath.Should().Be(entitySetPath);
            edmAction.IsBound.Should().BeTrue();
            edmAction.SchemaElementKind.Should().Be(EdmSchemaElementKind.Action);
        }
    }
}
