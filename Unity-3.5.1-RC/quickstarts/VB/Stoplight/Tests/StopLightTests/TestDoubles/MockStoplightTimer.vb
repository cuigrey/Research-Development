﻿' Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

Imports System
Imports StopLight.ServiceInterfaces

Namespace StopLightTests.TestDoubles
	Friend Class MockStoplightTimer
    Implements IStoplightTimer

    Private span As TimeSpan

#Region "IStoplightTimer Members"

    Public Property Duration() As TimeSpan Implements IStoplightTimer.Duration
      Get
        Return span
      End Get
      Set(ByVal value As TimeSpan)
        span = value
      End Set
    End Property

    Public Sub Start() Implements StopLight.ServiceInterfaces.IStoplightTimer.Start
    End Sub

    Public Event Expired As EventHandler Implements StopLight.ServiceInterfaces.IStoplightTimer.Expired

#End Region

    Public Sub Expire()
      If Not ExpiredEvent Is Nothing Then
        RaiseEvent Expired(Me, EventArgs.Empty)
      End If
    End Sub

  End Class
End Namespace

