﻿Public Class MastertronicsController
    Public Enum StepperCode As Byte
        Undefined = 0
        X = 1
        Y = 2
        Z = 3
        E0 = 21
        E1 = 22
    End Enum
    Public Class StepperBlockers
        Public Property XMin As Boolean
        Public Property XMax As Boolean
        Public Property YMin As Boolean
        Public Property YMax As Boolean
        Public Property ZMin As Boolean
        Public Property ZMax As Boolean
    End Class

    Private _ss As New SimplSerialBusLight("COM0")
    Private _virtualForm As MastertronicsVirtualForm

    Public Property VirtualMode As Boolean

    Public Sub VirtualReset(min As Integer, value As Integer, max As Integer, divider As Integer)
        VirtualForm.Init(min, value, max, divider)
    End Sub
    Public ReadOnly Property VirtualForm As MastertronicsVirtualForm
        Get
            SyncLock Me
                If _virtualForm Is Nothing Then _virtualForm = New MastertronicsVirtualForm
                Return _virtualForm
            End SyncLock
        End Get
    End Property

    Public Sub StartStepping(stepperCode As StepperCode, steps As Integer, stepPauseSec As Double, releaseAfterEnd As Boolean)
        If VirtualMode = False Then
            Dim direction As Byte = 0
            If steps < 0 Then steps = -steps : direction = 1
            Dim stepsH As Byte = (steps >> 8) Mod 256
            Dim stepsL As Byte = (steps >> 0) Mod 256
            Dim pauseH As Byte = (CInt(stepPauseSec * 10000.0) >> 8) Mod 256
            Dim pauseL As Byte = (CInt(stepPauseSec * 10000.0) >> 0) Mod 256
            Dim release As Byte = If(releaseAfterEnd, 1, 0)
            Dim response = _ss.Request(New SSRequest(0, 81, {stepperCode, stepsH, stepsL, direction, pauseH, pauseL, release}))
            If response.ResponseState <> ResponseState.ok Then Throw New Exception("RunStepping: " + response.ResponseState.ToString)
            If response.Result <> 128 + 81 Then Throw New Exception("RunStepping: bad return code")

        Else
            VirtualForm.IsBusy = True
            Dim t As New Threading.Thread(Sub()
                                              VirtualForm.Stepping(stepperCode, steps, stepPauseSec)
                                          End Sub)
            t.IsBackground = True
            t.Start()
        End If
    End Sub

    Public Function StepAndWait(stepperCode As StepperCode, steps As Integer, stepPauseSec As Double, releaseAfterEnd As Boolean) As Integer
        StartStepping(stepperCode, steps, stepPauseSec, releaseAfterEnd)
        Dim answer = _ss.Read
        Dim started = Now
        Do While answer Is Nothing And (Now - started).TotalSeconds < (stepPauseSec * Math.Abs(steps) + 1)
            answer = _ss.Read
            Threading.Thread.Sleep(10)
        Loop
        If answer Is Nothing Then Throw New Exception("StepAndWait: no answer after stepping end")
        If answer.ResponseState <> ResponseState.ok Then Throw New Exception("StepAndWait: " + answer.ResponseState.ToString)
        If answer.Result <> 128 + 88 Then Throw New Exception("StepAndWait: bad return code")

        Dim result As Integer = (CInt(answer.Data(0)) << 24) + (CInt(answer.Data(1)) << 16) + (CInt(answer.Data(2)) << 8) + (CInt(answer.Data(3)))
        Return result
    End Function

    Public Function GetStepperCounter(stepperCode As StepperCode) As Integer

        Dim response = _ss.Request(New SSRequest(0, 88, {stepperCode}))
        If response.ResponseState <> ResponseState.ok Then Throw New Exception("GetStepperCounter: " + response.ResponseState.ToString)
        If response.Result <> 128 + 88 Then Throw New Exception("GetStepperCounter: bad return code")

        Dim result As Integer = (CInt(response.Data(0)) << 24) + (CInt(response.Data(1)) << 16) + (CInt(response.Data(2)) << 8) + (CInt(response.Data(3)))
        Return result
    End Function

    Public Sub ResetStepperCounters()
        Dim response = _ss.Request(New SSRequest(0, 84, {}))
        If response.ResponseState <> ResponseState.ok Then Throw New Exception("ResetStepperCounters: " + response.ResponseState.ToString)
        If response.Result <> 128 + 84 Then Throw New Exception("ResetStepperCounters: bad return code")
    End Sub

    Public Function GetStepperBlockersState() As StepperBlockers
        If VirtualMode = False Then
            Dim response = _ss.Request(New SSRequest(0, 101, {}))
            If response.ResponseState <> ResponseState.ok Then Throw New Exception("GetStepperBlockersState: " + response.ResponseState.ToString)
            If response.Result <> 128 + 101 Then Throw New Exception("GetStepperBlockersState: bad return code")
            Dim result As New StepperBlockers
            result.XMin = response.Data(0) And 1
            result.XMax = response.Data(0) And 2
            result.YMin = response.Data(0) And 4
            result.YMax = response.Data(0) And 8
            result.ZMin = response.Data(0) And 16
            result.ZMax = response.Data(0) And 32
            Return result
        Else
            Dim result As New StepperBlockers
            With VirtualForm
                If .XValue <= .XMinimum Then result.XMin = True
                If .XValue >= .XMaximum Then result.XMax = True
                If .YValue <= .YMinimum Then result.YMin = True
                If .YValue >= .YMaximum Then result.YMax = True
                If .ZValue <= .ZMinimum Then result.ZMin = True
                If .ZValue >= .ZMaximum Then result.ZMax = True
                Return result
            End With
        End If
    End Function

    Public Function IsBusy() As Boolean
        If VirtualMode = False Then
            Dim response = _ss.Request(New SSRequest(0, 80, {0}))
            If response.ResponseState <> ResponseState.ok Then Return True
            'If response.Result <> 128 + 80 Then Return True
            Return False
        Else
            Return VirtualForm.IsBusy
        End If
    End Function

    Public Sub Connect(port As String)
        If VirtualMode = False Then
            If _ss.SerialPort.IsOpen Then _ss.SerialPort.Close()
            _ss.SerialPort.PortName = port
            _ss.SerialPort.BaudRate = 38400
            _ss.SerialPort.Open()
        End If
    End Sub

End Class
