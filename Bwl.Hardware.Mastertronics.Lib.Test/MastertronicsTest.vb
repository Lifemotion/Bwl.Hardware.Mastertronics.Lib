Imports Bwl.Framework

Public Class MastertronicsTest
    Inherits FormAppBase
    Private _serialPort As New StringSetting(AppBase.RootStorage, "SerialPort", "COM0")
    Private _mastertronics As New MastertronicsController
    Private _logger As Logger = AppBase.RootLogger

    Private Sub MastertronicsTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each code In [Enum].GetNames(GetType(MastertronicsController.StepperCode))
            stepperSelect.Items.Add(code)
        Next
        stepperSelect.SelectedIndex = 1
    End Sub

    Private Sub connectButton_Click(sender As Object, e As EventArgs) Handles connectButton.Click
        Try
            _mastertronics.Connect(_serialPort.Value)
            _logger.AddMessage("Connected: " + _serialPort.Value)
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub runSteppingButton_Click(sender As Object, e As EventArgs) Handles runSteppingButton.Click
        'код движка (перечисление)
        Dim stepperCode As MastertronicsController.StepperCode
        [Enum].TryParse(Of MastertronicsController.StepperCode)(stepperSelect.Text, stepperCode)
        'число шагов (именно шагов непосредственно и без конвертации). Может быть отрицательным, тогда поедет в другую сторону
        Dim steps As Integer = Val(stepsCountTextbox.Text)
        'пауза между шагами в секундах. т.к. на форме она была в милисекундах, делим на 1000
        Dim stepPause As Double = Val(stepsPauseTextbox.Text) / 1000.0
        'освободить ли мотор после движения или оставить под током. Если надо просто освободить, без движения - надо вызвать StartStepping и указать 0 шагов
        Dim releaseAfterEnd As Boolean = releaseAfterEndCheckbox.Checked
        Try
            'запускаем процесс движения. все процедуры синхронные, т.е. возвращают определенный ответ, полученный от контроллера или ошибку
            _mastertronics.StartStepping(stepperCode, steps, stepPause, releaseAfterEnd)
            'успешно запустили движение
            _logger.AddMessage("Stepping started")
            'в цикле ждем окончания движения. контроллер может выполнять только одно движение одновременно.
            Do
                Application.DoEvents() ' это чтобы форма не подвисала ' внутри отделнього потока заменить на Sleep
            Loop While _mastertronics.IsBusy ' проверяем, занят ли контроллер. Если занят, значит движение выполняется
            'контроллен не занят, значит приехали (заданное число шагов или концевик)
            'если движок едет на положительное количество шагов, он остановится от концевика Max, если на отрицательное - от Min
            _logger.AddMessage("Stepping finished")
            'обновляем состояние концевиков
            Dim blockers = _mastertronics.GetStepperBlockersState
            'показываем на форме 
            ShowBlockersState(blockers)
            'получаем значение внутреннего счетчика шагов
            Dim position = _mastertronics.GetStepperCounter(stepperCode)
            positionResult.Text = position.ToString
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub refreshBlockersState_Click(sender As Object, e As EventArgs) Handles refreshBlockersState.Click
        Try
            Dim blockers = _mastertronics.GetStepperBlockersState
            ShowBlockersState(blockers)
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub ShowBlockersState(blockers As MastertronicsController.StepperBlockers)
        XMin.Checked = blockers.XMin
        XMax.Checked = blockers.XMax
        YMin.Checked = blockers.YMin
        YMax.Checked = blockers.YMax
        ZMin.Checked = blockers.ZMin
        ZMax.Checked = blockers.ZMax
        _logger.AddMessage("Blockers refreshed")
    End Sub

    Private Sub VirtualMode_CheckedChanged(sender As Object, e As EventArgs) Handles VirtualMode.CheckedChanged

        _mastertronics.VirtualMode = VirtualMode.Checked
        If VirtualMode.Checked Then
            _mastertronics.VirtualForm.Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            _mastertronics.ResetStepperCounters()
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub MastertronicsTest_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim steps As Integer = Val(stepsCountTextbox.Text)
        'пауза между шагами в секундах. т.к. на форме она была в милисекундах, делим на 1000
        Dim stepPause As Double = Val(stepsPauseTextbox.Text) / 1000.0
        'освободить ли мотор после движения или оставить под током. Если надо просто освободить, без движения - надо вызвать StartStepping и указать 0 шагов
        Dim releaseAfterEnd As Boolean = releaseAfterEndCheckbox.Checked

        Try
            Select Case e.KeyCode
                Case Keys.Left
                    _mastertronics.StartStepping(MastertronicsController.StepperCode.X, -steps, stepPause, releaseAfterEnd)
                Case Keys.Right
                    _mastertronics.StartStepping(MastertronicsController.StepperCode.X, +steps, stepPause, releaseAfterEnd)
                Case Keys.Up
                    _mastertronics.StartStepping(MastertronicsController.StepperCode.Y, -steps, stepPause, releaseAfterEnd)
                Case Keys.Down
                    _mastertronics.StartStepping(MastertronicsController.StepperCode.Y, +steps, stepPause, releaseAfterEnd)

            End Select
        Catch ex As Exception

        End Try       'запускаем процесс движения. все процедуры синхронные, т.е. возвращают определенный ответ, полученный от контроллера или ошибку

    End Sub

    Private Sub runStepAndWait_Click(sender As Object, e As EventArgs) Handles runStepAndWait.Click
        'код движка (перечисление)
        Dim stepperCode As MastertronicsController.StepperCode
        [Enum].TryParse(Of MastertronicsController.StepperCode)(stepperSelect.Text, stepperCode)
        'число шагов (именно шагов непосредственно и без конвертации). Может быть отрицательным, тогда поедет в другую сторону
        Dim steps As Integer = Val(stepsCountTextbox.Text)
        'пауза между шагами в секундах. т.к. на форме она была в милисекундах, делим на 1000
        Dim stepPause As Double = Val(stepsPauseTextbox.Text) / 1000.0
        'освободить ли мотор после движения или оставить под током. Если надо просто освободить, без движения - надо вызвать StartStepping и указать 0 шагов
        Dim releaseAfterEnd As Boolean = releaseAfterEndCheckbox.Checked
        Try
            _logger.AddMessage("Stepping started")
            Dim position = _mastertronics.StepAndWait(stepperCode, steps, stepPause, releaseAfterEnd)
            _logger.AddMessage("Stepping finished")
            'в цикле ждем окончания движения. контроллер может выполнять только одно движение одновременно.
            positionResult.Text = position.ToString
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub
End Class
