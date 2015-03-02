<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MastertronicsTest
    Inherits Bwl.Framework.FormAppBase

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.connectButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.releaseAfterEndCheckbox = New System.Windows.Forms.CheckBox()
        Me.runSteppingButton = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.stepsPauseTextbox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.stepsCountTextbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.stepperSelect = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.XMin = New System.Windows.Forms.CheckBox()
        Me.XMax = New System.Windows.Forms.CheckBox()
        Me.YMax = New System.Windows.Forms.CheckBox()
        Me.YMin = New System.Windows.Forms.CheckBox()
        Me.ZMax = New System.Windows.Forms.CheckBox()
        Me.ZMin = New System.Windows.Forms.CheckBox()
        Me.refreshBlockersState = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.Location = New System.Drawing.Point(0, 164)
        Me.logWriter.Size = New System.Drawing.Size(712, 322)
        '
        'connectButton
        '
        Me.connectButton.Location = New System.Drawing.Point(6, 19)
        Me.connectButton.Name = "connectButton"
        Me.connectButton.Size = New System.Drawing.Size(75, 23)
        Me.connectButton.TabIndex = 2
        Me.connectButton.Text = "Connect"
        Me.connectButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.connectButton)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(92, 126)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Controller"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.releaseAfterEndCheckbox)
        Me.GroupBox2.Controls.Add(Me.runSteppingButton)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.stepsPauseTextbox)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.stepsCountTextbox)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.stepperSelect)
        Me.GroupBox2.Location = New System.Drawing.Point(110, 27)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(307, 126)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Steppers"
        '
        'releaseAfterEndCheckbox
        '
        Me.releaseAfterEndCheckbox.AutoSize = True
        Me.releaseAfterEndCheckbox.Checked = True
        Me.releaseAfterEndCheckbox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.releaseAfterEndCheckbox.Location = New System.Drawing.Point(87, 98)
        Me.releaseAfterEndCheckbox.Name = "releaseAfterEndCheckbox"
        Me.releaseAfterEndCheckbox.Size = New System.Drawing.Size(106, 17)
        Me.releaseAfterEndCheckbox.TabIndex = 7
        Me.releaseAfterEndCheckbox.Text = "ReleaseAfterEnd"
        Me.releaseAfterEndCheckbox.UseVisualStyleBackColor = True
        '
        'runSteppingButton
        '
        Me.runSteppingButton.Location = New System.Drawing.Point(214, 19)
        Me.runSteppingButton.Name = "runSteppingButton"
        Me.runSteppingButton.Size = New System.Drawing.Size(81, 21)
        Me.runSteppingButton.TabIndex = 6
        Me.runSteppingButton.Text = "Go"
        Me.runSteppingButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Pause, msec"
        '
        'stepsPauseTextbox
        '
        Me.stepsPauseTextbox.Location = New System.Drawing.Point(87, 72)
        Me.stepsPauseTextbox.Name = "stepsPauseTextbox"
        Me.stepsPauseTextbox.Size = New System.Drawing.Size(121, 20)
        Me.stepsPauseTextbox.TabIndex = 4
        Me.stepsPauseTextbox.Text = "2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Steps"
        '
        'stepsCountTextbox
        '
        Me.stepsCountTextbox.Location = New System.Drawing.Point(87, 46)
        Me.stepsCountTextbox.Name = "stepsCountTextbox"
        Me.stepsCountTextbox.Size = New System.Drawing.Size(121, 20)
        Me.stepsCountTextbox.TabIndex = 2
        Me.stepsCountTextbox.Text = "-1000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Stepper"
        '
        'stepperSelect
        '
        Me.stepperSelect.FormattingEnabled = True
        Me.stepperSelect.Location = New System.Drawing.Point(87, 19)
        Me.stepperSelect.Name = "stepperSelect"
        Me.stepperSelect.Size = New System.Drawing.Size(121, 21)
        Me.stepperSelect.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.refreshBlockersState)
        Me.GroupBox3.Controls.Add(Me.ZMax)
        Me.GroupBox3.Controls.Add(Me.ZMin)
        Me.GroupBox3.Controls.Add(Me.YMax)
        Me.GroupBox3.Controls.Add(Me.YMin)
        Me.GroupBox3.Controls.Add(Me.XMax)
        Me.GroupBox3.Controls.Add(Me.XMin)
        Me.GroupBox3.Location = New System.Drawing.Point(423, 27)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(138, 126)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "SteppersBlockers"
        '
        'XMin
        '
        Me.XMin.AutoSize = True
        Me.XMin.Location = New System.Drawing.Point(17, 22)
        Me.XMin.Name = "XMin"
        Me.XMin.Size = New System.Drawing.Size(50, 17)
        Me.XMin.TabIndex = 0
        Me.XMin.Text = "XMin"
        Me.XMin.UseVisualStyleBackColor = True
        '
        'XMax
        '
        Me.XMax.AutoSize = True
        Me.XMax.Location = New System.Drawing.Point(73, 22)
        Me.XMax.Name = "XMax"
        Me.XMax.Size = New System.Drawing.Size(53, 17)
        Me.XMax.TabIndex = 1
        Me.XMax.Text = "XMax"
        Me.XMax.UseVisualStyleBackColor = True
        '
        'YMax
        '
        Me.YMax.AutoSize = True
        Me.YMax.Location = New System.Drawing.Point(73, 48)
        Me.YMax.Name = "YMax"
        Me.YMax.Size = New System.Drawing.Size(53, 17)
        Me.YMax.TabIndex = 3
        Me.YMax.Text = "YMax"
        Me.YMax.UseVisualStyleBackColor = True
        '
        'YMin
        '
        Me.YMin.AutoSize = True
        Me.YMin.Location = New System.Drawing.Point(17, 48)
        Me.YMin.Name = "YMin"
        Me.YMin.Size = New System.Drawing.Size(50, 17)
        Me.YMin.TabIndex = 2
        Me.YMin.Text = "YMin"
        Me.YMin.UseVisualStyleBackColor = True
        '
        'ZMax
        '
        Me.ZMax.AutoSize = True
        Me.ZMax.Location = New System.Drawing.Point(73, 74)
        Me.ZMax.Name = "ZMax"
        Me.ZMax.Size = New System.Drawing.Size(53, 17)
        Me.ZMax.TabIndex = 5
        Me.ZMax.Text = "ZMax"
        Me.ZMax.UseVisualStyleBackColor = True
        '
        'ZMin
        '
        Me.ZMin.AutoSize = True
        Me.ZMin.Location = New System.Drawing.Point(17, 74)
        Me.ZMin.Name = "ZMin"
        Me.ZMin.Size = New System.Drawing.Size(50, 17)
        Me.ZMin.TabIndex = 4
        Me.ZMin.Text = "ZMin"
        Me.ZMin.UseVisualStyleBackColor = True
        '
        'refreshBlockersState
        '
        Me.refreshBlockersState.Location = New System.Drawing.Point(17, 99)
        Me.refreshBlockersState.Name = "refreshBlockersState"
        Me.refreshBlockersState.Size = New System.Drawing.Size(109, 21)
        Me.refreshBlockersState.TabIndex = 7
        Me.refreshBlockersState.Text = "Refresh"
        Me.refreshBlockersState.UseVisualStyleBackColor = True
        '
        'MastertronicsTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(712, 484)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MastertronicsTest"
        Me.Text = "Mastertronics Test"
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents connectButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents stepperSelect As System.Windows.Forms.ComboBox
    Friend WithEvents releaseAfterEndCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents runSteppingButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents stepsPauseTextbox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents stepsCountTextbox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents refreshBlockersState As System.Windows.Forms.Button
    Friend WithEvents ZMax As System.Windows.Forms.CheckBox
    Friend WithEvents ZMin As System.Windows.Forms.CheckBox
    Friend WithEvents YMax As System.Windows.Forms.CheckBox
    Friend WithEvents YMin As System.Windows.Forms.CheckBox
    Friend WithEvents XMax As System.Windows.Forms.CheckBox
    Friend WithEvents XMin As System.Windows.Forms.CheckBox

End Class
