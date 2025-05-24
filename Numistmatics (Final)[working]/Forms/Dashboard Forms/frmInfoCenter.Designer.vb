<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInfoCenter
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
        Dim DenominationLabel As System.Windows.Forms.Label
        Dim Currency_NameLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInfoCenter))
        Me.DenominationTextBox = New System.Windows.Forms.TextBox()
        Me.Currency_NameComboBox = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button7 = New System.Windows.Forms.Button()
        DenominationLabel = New System.Windows.Forms.Label()
        Currency_NameLabel = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DenominationLabel
        '
        DenominationLabel.AutoSize = True
        DenominationLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DenominationLabel.ForeColor = System.Drawing.Color.Black
        DenominationLabel.Location = New System.Drawing.Point(113, 142)
        DenominationLabel.Name = "DenominationLabel"
        DenominationLabel.Size = New System.Drawing.Size(104, 18)
        DenominationLabel.TabIndex = 50
        DenominationLabel.Text = "Denomination:"
        DenominationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Currency_NameLabel
        '
        Currency_NameLabel.AutoSize = True
        Currency_NameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Currency_NameLabel.ForeColor = System.Drawing.Color.Black
        Currency_NameLabel.Location = New System.Drawing.Point(113, 95)
        Currency_NameLabel.Name = "Currency_NameLabel"
        Currency_NameLabel.Size = New System.Drawing.Size(116, 18)
        Currency_NameLabel.TabIndex = 48
        Currency_NameLabel.Text = "Currency Name:"
        Currency_NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DenominationTextBox
        '
        Me.DenominationTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DenominationTextBox.Location = New System.Drawing.Point(270, 139)
        Me.DenominationTextBox.Margin = New System.Windows.Forms.Padding(2)
        Me.DenominationTextBox.Name = "DenominationTextBox"
        Me.DenominationTextBox.Size = New System.Drawing.Size(282, 24)
        Me.DenominationTextBox.TabIndex = 51
        '
        'Currency_NameComboBox
        '
        Me.Currency_NameComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Currency_NameComboBox.FormattingEnabled = True
        Me.Currency_NameComboBox.Items.AddRange(New Object() {"AUD", "GBP", "EUR", "JPY", "CHF", "USD", "AFN", "ALL", "DZD", "AOA", "ARS", "AMD", "AWG", "AUD", "ATS ", "BEF", "AZN", "BSD", "BHD", "BDT", "BBD", "BYR", "BZD", "BMD", "BTN", "BOB", "BAM", "BWP", "BRL", "GBP", "BND", "BGN", "BIF", "XOF", "XAF", "XPF", "KHR", "CAD", "CVE", "KYD", "CLP", "CNY", "COP", "KMF", "CDF", "CRC", "HRK", "CUC", "CUP", "CYP", "CZK", "DKK", "DJF", "DOP", "XCD", "EGP", "SVC", "EEK", "ETB", "EUR", "FKP", "FIM", "FJD", "GMD", "GEL", "DMK", "GHS", "GIP", "GRD", "GTQ", "GNF", "GYD", "HTG", "HNL", "HKD", "HUF", "ISK", "INR", "IDR", "IRR", "IQD", "IED ", "ILS", "ITL", "JMD", "JPY", "JOD", "KZT", "KES", "KWD", "KGS", "LAK", "LVL", "LBP", "LSL", "LRD", "LYD", "LTL ", "LUF ", "MOP", "MKD", "MGA", "MWK", "MYR", "MVR", "MTL ", "MRO", "MUR", "MXN", "MDL", "MNT", "MAD", "MZN", "MMK", "ANG", "NAD", "NPR", "NLG ", "NZD", "NIO", "NGN", "KPW", "NOK", "OMR", "PKR", "PAB", "PGK", "PYG", "PEN", "PHP", "PLN", "PTE ", "QAR", "RON", "RUB", "RWF", "WST", "STD", "SAR", "RSD", "SCR", "SLL", "SGD", "SKK", "SIT ", "SBD", "SOS", "ZAR", "KRW", "ESP", "LKR", "SHP", "SDG", "SRD", "SZL", "SEK", "CHF", "SYP", "TWD", "TZS", "THB", "TOP", "TTD", "TND", "TRY", "TMM", "USD", "UGX", "UAH", "UYU", "AED", "VUV", "VEB", "VND", "YER", "ZMK", "ZWD"})
        Me.Currency_NameComboBox.Location = New System.Drawing.Point(270, 92)
        Me.Currency_NameComboBox.Margin = New System.Windows.Forms.Padding(2)
        Me.Currency_NameComboBox.Name = "Currency_NameComboBox"
        Me.Currency_NameComboBox.Size = New System.Drawing.Size(282, 26)
        Me.Currency_NameComboBox.TabIndex = 49
        '
        'Button2
        '
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button2.ForeColor = System.Drawing.Color.Black
        Me.Button2.Location = New System.Drawing.Point(239, 92)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(230, 83)
        Me.Button2.TabIndex = 60
        Me.Button2.Text = "Catalogue Codes"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(3, 92)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(230, 83)
        Me.Button1.TabIndex = 59
        Me.Button1.Text = "Live Exchange Rates"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.AutoSize = True
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button6.ForeColor = System.Drawing.Color.Black
        Me.Button6.Location = New System.Drawing.Point(537, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(168, 83)
        Me.Button6.TabIndex = 58
        Me.Button6.Text = "More"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button11.FlatAppearance.BorderSize = 0
        Me.Button11.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button11.ForeColor = System.Drawing.Color.Black
        Me.Button11.Location = New System.Drawing.Point(359, 3)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(172, 83)
        Me.Button11.TabIndex = 57
        Me.Button11.Text = "Info of Currency"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.AutoSize = True
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button5.ForeColor = System.Drawing.Color.Black
        Me.Button5.Location = New System.Drawing.Point(181, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(172, 83)
        Me.Button5.TabIndex = 56
        Me.Button5.Text = " Value"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button3.ForeColor = System.Drawing.Color.Black
        Me.Button3.Location = New System.Drawing.Point(3, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(172, 83)
        Me.Button3.TabIndex = 55
        Me.Button3.Text = "Currency Chart"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button4.ForeColor = System.Drawing.Color.Black
        Me.Button4.Location = New System.Drawing.Point(475, 92)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(230, 83)
        Me.Button4.TabIndex = 61
        Me.Button4.Text = "Back"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Button3)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button5)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button11)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button6)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button1)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button2)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button4)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(36, 313)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(709, 182)
        Me.FlowLayoutPanel1.TabIndex = 62
        '
        'Button7
        '
        Me.Button7.FlatAppearance.BorderSize = 0
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Image = Global.Numistmatics__Final__working_.My.Resources.Resources.icons8_close_window_20
        Me.Button7.Location = New System.Drawing.Point(1, 1)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(22, 22)
        Me.Button7.TabIndex = 63
        Me.Button7.UseVisualStyleBackColor = True
        '
        'frmInfoCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 535)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.DenominationTextBox)
        Me.Controls.Add(DenominationLabel)
        Me.Controls.Add(Me.Currency_NameComboBox)
        Me.Controls.Add(Currency_NameLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmInfoCenter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "InfoCenter"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DenominationTextBox As TextBox
    Friend WithEvents Currency_NameComboBox As ComboBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Button7 As Button
End Class
