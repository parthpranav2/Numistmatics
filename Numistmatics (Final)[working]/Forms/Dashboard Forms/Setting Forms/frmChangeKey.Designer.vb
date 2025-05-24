<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeKey
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChangeKey))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MaskedTextBoxO = New System.Windows.Forms.MaskedTextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.MaskedTextBoxN = New System.Windows.Forms.MaskedTextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MaskedTextBoxRN = New System.Windows.Forms.MaskedTextBox()
        Me.butApply = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(97, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 20)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Current Password"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MaskedTextBoxO
        '
        Me.MaskedTextBoxO.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaskedTextBoxO.Location = New System.Drawing.Point(259, 70)
        Me.MaskedTextBoxO.Name = "MaskedTextBoxO"
        Me.MaskedTextBoxO.Size = New System.Drawing.Size(280, 26)
        Me.MaskedTextBoxO.TabIndex = 21
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(558, 69)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(98, 30)
        Me.Button2.TabIndex = 24
        Me.Button2.Text = "Show Password"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(558, 203)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 30)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "Show Password"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(119, 207)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 20)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "New Password"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MaskedTextBoxN
        '
        Me.MaskedTextBoxN.Enabled = False
        Me.MaskedTextBoxN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaskedTextBoxN.Location = New System.Drawing.Point(259, 204)
        Me.MaskedTextBoxN.Name = "MaskedTextBoxN"
        Me.MaskedTextBoxN.Size = New System.Drawing.Size(280, 26)
        Me.MaskedTextBoxN.TabIndex = 25
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.Location = New System.Drawing.Point(558, 285)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(98, 30)
        Me.Button3.TabIndex = 30
        Me.Button3.Text = "Show Password"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(99, 289)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 20)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Retype Password"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MaskedTextBoxRN
        '
        Me.MaskedTextBoxRN.Enabled = False
        Me.MaskedTextBoxRN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaskedTextBoxRN.Location = New System.Drawing.Point(259, 286)
        Me.MaskedTextBoxRN.Name = "MaskedTextBoxRN"
        Me.MaskedTextBoxRN.Size = New System.Drawing.Size(280, 26)
        Me.MaskedTextBoxRN.TabIndex = 28
        '
        'butApply
        '
        Me.butApply.Location = New System.Drawing.Point(103, 384)
        Me.butApply.Name = "butApply"
        Me.butApply.Size = New System.Drawing.Size(129, 104)
        Me.butApply.TabIndex = 31
        Me.butApply.Text = "Apply Password"
        Me.butApply.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(312, 384)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(129, 104)
        Me.Button5.TabIndex = 32
        Me.Button5.Text = "Clear All"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(527, 384)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(129, 104)
        Me.Button6.TabIndex = 33
        Me.Button6.Text = "Back"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Image = Global.Numistmatics__Final__working_.My.Resources.Resources.icons8_close_window_20
        Me.Button4.Location = New System.Drawing.Point(1, 1)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 22)
        Me.Button4.TabIndex = 34
        Me.Button4.UseVisualStyleBackColor = True
        '
        'frmChangeKey
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(753, 568)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.butApply)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.MaskedTextBoxRN)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.MaskedTextBoxN)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MaskedTextBoxO)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmChangeKey"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChangeKey"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents MaskedTextBoxO As MaskedTextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents MaskedTextBoxN As MaskedTextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents MaskedTextBoxRN As MaskedTextBox
    Friend WithEvents butApply As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button4 As Button
End Class
