﻿' Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.ObjectBuilder
Imports SimpleEventBroker

Namespace EventBrokerExtension
	Public Class SimpleEventBrokerExtension
		Inherits UnityContainerExtension
		Implements ISimpleEventBrokerConfiguration
    Private _broker As New EventBroker()

        Protected Overloads Overrides Sub Initialize()
            Context.Container.RegisterInstance(_broker, New ExternallyControlledLifetimeManager())

            Context.Strategies.AddNew(Of EventBrokerReflectionStrategy)(UnityBuildStage.PreCreation)
            Context.Strategies.AddNew(Of EventBrokerWireupStrategy)(UnityBuildStage.Initialization)
        End Sub

    Public ReadOnly Property Broker() As EventBroker Implements ISimpleEventBrokerConfiguration.Broker
      Get
        Return _broker
      End Get
    End Property
	End Class
End Namespace

