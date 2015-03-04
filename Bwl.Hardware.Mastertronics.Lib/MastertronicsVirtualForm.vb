Public Class MastertronicsVirtualForm

    Public Property IsBusy As Boolean

    Private Sub MastertronicsDebugForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        XMinimum = -100 : XValue = 0 : XMaximum = 100
        YMinimum = -100 : YValue = 0 : YMaximum = 100
        ZMinimum = -100 : ZValue = 0 : ZMaximum = 100
    End Sub

    Public Property XMinimum As Integer
    Public Property YMinimum As Integer
    Public Property ZMinimum As Integer

    Public Property XMaximum As Integer
    Public Property YMaximum As Integer
    Public Property ZMaximum As Integer

    Public Property XValue As Integer
    Public Property YValue As Integer
    Public Property ZValue As Integer

    Public Property VirtualDivider As Integer = 100

    Private Sub xt_ValueChanged(sender As Object, e As EventArgs) Handles xt.ValueChanged
        x.Text = xt.Value.ToString
    End Sub

    Private Sub yt_ValueChanged(sender As Object, e As EventArgs) Handles yt.ValueChanged
        y.Text = yt.Value.ToString
    End Sub

    Private Sub zt_ValueChanged(sender As Object, e As EventArgs) Handles zt.ValueChanged
        z.Text = zt.Value.ToString
    End Sub

    Public Sub StepX(steps As Integer, delay As Double)
        steps = steps / VirtualDivider
        IsBusy = True
        Dim dir = 1
        If steps < 0 Then steps = -steps : dir = -1
        For i = 1 To steps
            If (dir = 1 And XValue < XMaximum) Or (dir = -1 And XValue > XMinimum) Then
                XValue += dir
                Application.DoEvents()
                Threading.Thread.Sleep(TimeSpan.FromSeconds(delay * VirtualDivider))
            End If
        Next
        IsBusy = False
    End Sub

    Public Sub StepY(steps As Integer, delay As Double)
        steps = steps / VirtualDivider
        IsBusy = True
        Dim dir = 1
        If steps < 0 Then steps = -steps : dir = -1
        For i = 1 To steps
            If (dir = 1 And YValue < YMaximum) Or (dir = -1 And YValue > YMinimum) Then
                YValue += dir
                Application.DoEvents()
                Threading.Thread.Sleep(TimeSpan.FromSeconds(delay * VirtualDivider))
            End If
        Next
        IsBusy = False
    End Sub

    Public Sub StepZ(steps As Integer, delay As Double)
        steps = steps / VirtualDivider
        IsBusy = True
        Dim dir = 1
        If steps < 0 Then steps = -steps : dir = -1
        For i = 1 To steps
            If (dir = 1 And ZValue < ZMaximum) Or (dir = -1 And ZValue > ZMinimum) Then
                ZValue += dir
                Application.DoEvents()
                Threading.Thread.Sleep(TimeSpan.FromSeconds(delay * VirtualDivider))
            End If
        Next
        IsBusy = False
    End Sub

    Public Sub Stepping(stepperCode As MastertronicsController.StepperCode, steps As Integer, delay As Double)
        If stepperCode = MastertronicsController.StepperCode.X Then Me.StepX(steps / 1, delay)
        If stepperCode = MastertronicsController.StepperCode.Y Then Me.StepY(steps / 1, delay)
        If stepperCode = MastertronicsController.StepperCode.Z Then Me.StepZ(steps / 1, delay)
    End Sub


    Private Sub RefreshState_Tick(sender As Object, e As EventArgs) Handles RefreshState.Tick
        xmin.Text = XMinimum.ToString
        xmax.Text = XMaximum.ToString
        x.Text = XValue.ToString
        xt.Minimum = XMinimum
        xt.Maximum = XMaximum.ToString
        xt.Value = XValue.ToString

        ymin.Text = YMinimum.ToString
        ymax.Text = YMaximum.ToString
        y.Text = YValue.ToString
        yt.Minimum = YMinimum
        yt.Maximum = YMaximum.ToString
        yt.Value = YValue.ToString

        zmin.Text = ZMinimum.ToString
        zmax.Text = ZMaximum.ToString
        z.Text = ZValue.ToString
        zt.Minimum = ZMinimum
        zt.Maximum = ZMaximum.ToString
        zt.Value = ZValue.ToString

        vd.Text = VirtualDivider.ToString
    End Sub
End Class