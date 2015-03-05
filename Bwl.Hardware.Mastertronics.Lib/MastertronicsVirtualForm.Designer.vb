<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MastertronicsVirtualForm
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MastertronicsVirtualForm))
        Me.xt = New System.Windows.Forms.TrackBar()
        Me.yt = New System.Windows.Forms.TrackBar()
        Me.zt = New System.Windows.Forms.TrackBar()
        Me.xmin = New System.Windows.Forms.TextBox()
        Me.x = New System.Windows.Forms.TextBox()
        Me.xmax = New System.Windows.Forms.TextBox()
        Me.ymin = New System.Windows.Forms.TextBox()
        Me.y = New System.Windows.Forms.TextBox()
        Me.ymax = New System.Windows.Forms.TextBox()
        Me.zmin = New System.Windows.Forms.TextBox()
        Me.z = New System.Windows.Forms.TextBox()
        Me.zmax = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RefreshState = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.vd = New System.Windows.Forms.TextBox()
        CType(Me.xt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.yt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.zt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xt
        '
        Me.xt.Location = New System.Drawing.Point(78, 38)
        Me.xt.Maximum = 10000
        Me.xt.Name = "xt"
        Me.xt.Size = New System.Drawing.Size(751, 45)
        Me.xt.TabIndex = 0
        '
        'yt
        '
        Me.yt.Location = New System.Drawing.Point(78, 121)
        Me.yt.Maximum = 10000
        Me.yt.Name = "yt"
        Me.yt.Size = New System.Drawing.Size(751, 45)
        Me.yt.TabIndex = 0
        '
        'zt
        '
        Me.zt.Location = New System.Drawing.Point(78, 207)
        Me.zt.Maximum = 10000
        Me.zt.Name = "zt"
        Me.zt.Size = New System.Drawing.Size(751, 45)
        Me.zt.TabIndex = 0
        '
        'xmin
        '
        Me.xmin.Location = New System.Drawing.Point(78, 12)
        Me.xmin.Name = "xmin"
        Me.xmin.Size = New System.Drawing.Size(100, 20)
        Me.xmin.TabIndex = 1
        '
        'x
        '
        Me.x.Location = New System.Drawing.Point(405, 12)
        Me.x.Name = "x"
        Me.x.ReadOnly = True
        Me.x.Size = New System.Drawing.Size(100, 20)
        Me.x.TabIndex = 2
        '
        'xmax
        '
        Me.xmax.Location = New System.Drawing.Point(729, 12)
        Me.xmax.Name = "xmax"
        Me.xmax.Size = New System.Drawing.Size(100, 20)
        Me.xmax.TabIndex = 3
        '
        'ymin
        '
        Me.ymin.Location = New System.Drawing.Point(78, 89)
        Me.ymin.Name = "ymin"
        Me.ymin.Size = New System.Drawing.Size(100, 20)
        Me.ymin.TabIndex = 1
        '
        'y
        '
        Me.y.Location = New System.Drawing.Point(405, 89)
        Me.y.Name = "y"
        Me.y.ReadOnly = True
        Me.y.Size = New System.Drawing.Size(100, 20)
        Me.y.TabIndex = 2
        '
        'ymax
        '
        Me.ymax.Location = New System.Drawing.Point(729, 89)
        Me.ymax.Name = "ymax"
        Me.ymax.Size = New System.Drawing.Size(100, 20)
        Me.ymax.TabIndex = 3
        '
        'zmin
        '
        Me.zmin.Location = New System.Drawing.Point(78, 181)
        Me.zmin.Name = "zmin"
        Me.zmin.Size = New System.Drawing.Size(100, 20)
        Me.zmin.TabIndex = 1
        '
        'z
        '
        Me.z.Location = New System.Drawing.Point(405, 181)
        Me.z.Name = "z"
        Me.z.ReadOnly = True
        Me.z.Size = New System.Drawing.Size(100, 20)
        Me.z.TabIndex = 2
        '
        'zmax
        '
        Me.zmax.Location = New System.Drawing.Point(729, 181)
        Me.zmax.Name = "zmax"
        Me.zmax.Size = New System.Drawing.Size(100, 20)
        Me.zmax.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "X"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Y"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 207)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Z"
        '
        'RefreshState
        '
        Me.RefreshState.Enabled = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 259)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Real/Virtual"
        '
        'vd
        '
        Me.vd.Location = New System.Drawing.Point(78, 256)
        Me.vd.Name = "vd"
        Me.vd.ReadOnly = True
        Me.vd.Size = New System.Drawing.Size(100, 20)
        Me.vd.TabIndex = 8
        '
        'MastertronicsVirtualForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(881, 300)
        Me.Controls.Add(Me.vd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.zmax)
        Me.Controls.Add(Me.ymax)
        Me.Controls.Add(Me.z)
        Me.Controls.Add(Me.y)
        Me.Controls.Add(Me.xmax)
        Me.Controls.Add(Me.zmin)
        Me.Controls.Add(Me.ymin)
        Me.Controls.Add(Me.x)
        Me.Controls.Add(Me.xmin)
        Me.Controls.Add(Me.zt)
        Me.Controls.Add(Me.yt)
        Me.Controls.Add(Me.xt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MastertronicsVirtualForm"
        Me.Text = "Mastertronics Virtual"
        CType(Me.xt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.yt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.zt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents xt As System.Windows.Forms.TrackBar
    Friend WithEvents yt As System.Windows.Forms.TrackBar
    Friend WithEvents zt As System.Windows.Forms.TrackBar
    Friend WithEvents xmin As System.Windows.Forms.TextBox
    Friend WithEvents x As System.Windows.Forms.TextBox
    Friend WithEvents xmax As System.Windows.Forms.TextBox
    Friend WithEvents ymin As System.Windows.Forms.TextBox
    Friend WithEvents y As System.Windows.Forms.TextBox
    Friend WithEvents ymax As System.Windows.Forms.TextBox
    Friend WithEvents zmin As System.Windows.Forms.TextBox
    Friend WithEvents z As System.Windows.Forms.TextBox
    Friend WithEvents zmax As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RefreshState As System.Windows.Forms.Timer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents vd As System.Windows.Forms.TextBox
End Class
