﻿' Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

Imports System

Namespace EventBrokerTests.TestObjects
	Class ClipboardEventPublisher
		Public Event Copy As EventHandler
		Public Event Paste As EventHandler

		Public ReadOnly Property NumberOfCopyDelegates() As Integer
			Get
        Return GetInvocationListLength(CopyEvent)
			End Get
		End Property

		Public ReadOnly Property NumberOfPasteDelegates() As Integer
			Get
        Return GetInvocationListLength(PasteEvent)
			End Get
		End Property

		Public Sub DoPaste()
      If Not PasteEvent Is Nothing Then
        RaiseEvent Paste(Me, EventArgs.Empty)
      End If
		End Sub

		Public Sub DoCopy()
      If Not CopyEvent Is Nothing Then
        RaiseEvent Copy(Me, EventArgs.Empty)
      End If
		End Sub

		Private Function GetInvocationListLength(ByVal [event] As EventHandler) As Integer
      If [event] Is Nothing Then
        Return 0
      End If
			Return [event].GetInvocationList().Length
		End Function
	End Class
End Namespace

