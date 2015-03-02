Imports Bwl.Hardware.SimplSerial

Public Class MastertronicsController

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

    Private _ss As New SimplSerialBus("COM0")

    Public Sub StartStepping(stepperCode As StepperCode, steps As Integer, stepPauseSec As Double, releaseAfterEnd As Boolean)
        Dim direction As Byte = 0
        If steps < 0 Then steps = -steps : direction = 1
        Dim stepsH As Byte = (steps >> 8) Mod 256
        Dim stepsL As Byte = (steps >> 0) Mod 256
        Dim pauseH As Byte = (CInt(stepPauseSec * 10000.0) >> 8) Mod 256
        Dim pauseL As Byte = (CInt(stepPauseSec * 10000.0) >> 0) Mod 256
        Dim release As Byte = If(releaseAfterEnd, 1, 0)
        Dim response = _ss.Request(New SSRequest(0, 81, {stepperCode, stepsH, stepsL, direction, pauseH, pauseL, release}))
        If response.ResponseState <> ResponseState.ok Then Throw New Exception("RunStepping: " + response.ResponseState.ToString)
    End Sub

    Public Function GetStepperBlockersState() As StepperBlockers
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
    End Function

    Public Function IsBusy() As Boolean
        Dim response = _ss.Request(New SSRequest(0, 80, {0}))
        If response.ResponseState <> ResponseState.ok Then Return True
        If response.Result <> 128 + 80 Then Return True
        Return False
    End Function

    Public Sub Connect(port As String)
        _ss.SerialDevice.DeviceAddress = port
        _ss.SerialDevice.DeviceSpeed = 38400
        _ss.Connect()
    End Sub

End Class
