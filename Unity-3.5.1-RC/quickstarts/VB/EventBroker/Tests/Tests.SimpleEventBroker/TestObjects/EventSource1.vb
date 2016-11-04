﻿' Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

Imports System

Namespace EventBrokerTests
	Class EventSource1
		Public Event Event1 As EventHandler

		Public Sub FireEvent1()
			OnEvent1(Me, EventArgs.Empty)
		End Sub
		Protected Overridable Sub OnEvent1(ByVal sender As Object, ByVal e As EventArgs)
      If Not Event1Event Is Nothing Then
        RaiseEvent Event1(sender, e)
      End If
		End Sub

		Public ReadOnly Property NumberOfEvent1Delegates() As Integer
			Get
        If Event1Event Is Nothing Then
          Return 0
        End If
        Return Event1Event.GetInvocationList().Length
			End Get
		End Property
	End Class
End Namespace

