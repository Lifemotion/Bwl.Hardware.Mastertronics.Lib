Public Class MastertronicsVirtualForm

    Public Property IsBusy As Boolean

    Private Sub MastertronicsDebugForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Init(-100, 0, 100, 100)
    End Sub

    Public Sub Init(min As Integer, value As Integer, max As Integer, divider As Integer)
        If value > max Or value < min Or divider < 1 Then Throw New ArgumentOutOfRangeException
        XMinimum = min : XValue = value : XMaximum = max
        YMinimum = min : YValue = value : YMaximum = max
        ZMinimum = min : ZValue = value : ZMaximum = max
        VirtualDivider = divider
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
    Public Property BypassDelays As Boolean

    Private Sub xt_ValueChanged(sender As Object, e As EventArgs) Handles xt.ValueChanged
        XValue = xt.Value.ToString
    End Sub

    Private Sub yt_ValueChanged(sender As Object, e As EventArgs) Handles yt.ValueChanged
        YValue = yt.Value.ToString
    End Sub

    Private Sub zt_ValueChanged(sender As Object, e As EventArgs) Handles zt.ValueChanged
        ZValue = zt.Value.ToString
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
                If Not BypassDelays Then Threading.Thread.Sleep(TimeSpan.FromSeconds(delay * VirtualDivider))
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
        If XValue < XMinimum Then XValue = XMinimum
        If XValue > XMaximum Then XValue = XMaximum
        xmin.Text = XMinimum.ToString
        xmax.Text = XMaximum.ToString
        x.Text = XValue.ToString
        xt.Minimum = XMinimum
        xt.Maximum = XMaximum.ToString
        xt.Value = XValue.ToString

        If YValue < YMinimum Then YValue = YMinimum
        If YValue > YMaximum Then YValue = YMaximum
        ymin.Text = YMinimum.ToString
        ymax.Text = YMaximum.ToString
        y.Text = YValue.ToString
        yt.Minimum = YMinimum
        yt.Maximum = YMaximum.ToString
        yt.Value = YValue.ToString

        If ZValue < ZMinimum Then ZValue = ZMinimum
        If ZValue > ZMaximum Then ZValue = ZMaximum
        zmin.Text = ZMinimum.ToString
        zmax.Text = ZMaximum.ToString
        z.Text = ZValue.ToString
        zt.Minimum = ZMinimum
        zt.Maximum = ZMaximum.ToString
        zt.Value = ZValue.ToString

        vd.Text = VirtualDivider.ToString
    End Sub


End Class