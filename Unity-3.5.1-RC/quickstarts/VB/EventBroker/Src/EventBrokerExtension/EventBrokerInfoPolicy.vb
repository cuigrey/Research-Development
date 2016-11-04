﻿' Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

Imports System
Imports System.Collections.Generic
Imports System.Reflection

Namespace EventBrokerExtension
	Public Class EventBrokerInfoPolicy
    Implements IEventBrokerInfoPolicy

    Private _publications As New List(Of PublicationInfo)()
    Private _subscriptions As New List(Of SubscriptionInfo)()

		Public Sub AddPublication(ByVal publishedEventName As String, ByVal eventName As String)
      _publications.Add(New PublicationInfo(publishedEventName, eventName))
		End Sub

		Public Sub AddSubscription(ByVal publishedEventName As String, ByVal subscriber As MethodInfo)
      _subscriptions.Add(New SubscriptionInfo(publishedEventName, subscriber))
		End Sub

    Public ReadOnly Property Publications() As IEnumerable(Of PublicationInfo) Implements IEventBrokerInfoPolicy.Publications
      Get
        Return _publications
      End Get
    End Property

    Public ReadOnly Property Subscriptions() As IEnumerable(Of SubscriptionInfo) Implements IEventBrokerInfoPolicy.Subscriptions
      Get
        Return _subscriptions
      End Get
    End Property
	End Class
End Namespace
