<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmECoins
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
        Dim ContinentLabel As System.Windows.Forms.Label
        Dim CountryLabel As System.Windows.Forms.Label
        Dim Catalogue_CodeLabel As System.Windows.Forms.Label
        Dim Currency_NameLabel As System.Windows.Forms.Label
        Dim DenominationLabel As System.Windows.Forms.Label
        Dim Date_Recorded_OnLabel As System.Windows.Forms.Label
        Dim Cost_To_MeLabel As System.Windows.Forms.Label
        Dim SourceLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmECoins))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        ContinentLabel = New System.Windows.Forms.Label()
        CountryLabel = New System.Windows.Forms.Label()
        Catalogue_CodeLabel = New System.Windows.Forms.Label()
        Currency_NameLabel = New System.Windows.Forms.Label()
        DenominationLabel = New System.Windows.Forms.Label()
        Date_Recorded_OnLabel = New System.Windows.Forms.Label()
        Cost_To_MeLabel = New System.Windows.Forms.Label()
        SourceLabel = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContinentLabel
        '
        ContinentLabel.AutoSize = True
        ContinentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ContinentLabel.Location = New System.Drawing.Point(61, 87)
        ContinentLabel.Name = "ContinentLabel"
        ContinentLabel.Size = New System.Drawing.Size(82, 20)
        ContinentLabel.TabIndex = 41
        ContinentLabel.Text = "Continent:"
        '
        'CountryLabel
        '
        CountryLabel.AutoSize = True
        CountryLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        CountryLabel.Location = New System.Drawing.Point(61, 116)
        CountryLabel.Name = "CountryLabel"
        CountryLabel.Size = New System.Drawing.Size(68, 20)
        CountryLabel.TabIndex = 42
        CountryLabel.Text = "Country:"
        '
        'Catalogue_CodeLabel
        '
        Catalogue_CodeLabel.AutoSize = True
        Catalogue_CodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Catalogue_CodeLabel.Location = New System.Drawing.Point(61, 145)
        Catalogue_CodeLabel.Name = "Catalogue_CodeLabel"
        Catalogue_CodeLabel.Size = New System.Drawing.Size(128, 20)
        Catalogue_CodeLabel.TabIndex = 43
        Catalogue_CodeLabel.Text = "Catalogue Code:"
        '
        'Currency_NameLabel
        '
        Currency_NameLabel.AutoSize = True
        Currency_NameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Currency_NameLabel.Location = New System.Drawing.Point(61, 173)
        Currency_NameLabel.Name = "Currency_NameLabel"
        Currency_NameLabel.Size = New System.Drawing.Size(122, 20)
        Currency_NameLabel.TabIndex = 44
        Currency_NameLabel.Text = "Currency Name:"
        '
        'DenominationLabel
        '
        DenominationLabel.AutoSize = True
        DenominationLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DenominationLabel.Location = New System.Drawing.Point(61, 202)
        DenominationLabel.Name = "DenominationLabel"
        DenominationLabel.Size = New System.Drawing.Size(112, 20)
        DenominationLabel.TabIndex = 45
        DenominationLabel.Text = "Denomination:"
        '
        'Date_Recorded_OnLabel
        '
        Date_Recorded_OnLabel.AutoSize = True
        Date_Recorded_OnLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Date_Recorded_OnLabel.Location = New System.Drawing.Point(61, 231)
        Date_Recorded_OnLabel.Name = "Date_Recorded_OnLabel"
        Date_Recorded_OnLabel.Size = New System.Drawing.Size(147, 20)
        Date_Recorded_OnLabel.TabIndex = 46
        Date_Recorded_OnLabel.Text = "Date Recorded On:"
        '
        'Cost_To_MeLabel
        '
        Cost_To_MeLabel.AutoSize = True
        Cost_To_MeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Cost_To_MeLabel.Location = New System.Drawing.Point(61, 261)
        Cost_To_MeLabel.Name = "Cost_To_MeLabel"
        Cost_To_MeLabel.Size = New System.Drawing.Size(94, 20)
        Cost_To_MeLabel.TabIndex = 49
        Cost_To_MeLabel.Text = "Cost To Me:"
        '
        'SourceLabel
        '
        SourceLabel.AutoSize = True
        SourceLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SourceLabel.Location = New System.Drawing.Point(61, 290)
        SourceLabel.Name = "SourceLabel"
        SourceLabel.Size = New System.Drawing.Size(64, 20)
        SourceLabel.TabIndex = 50
        SourceLabel.Text = "Source:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(963, 43)
        Me.Panel1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(433, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 33)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Coins"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(315, 290)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 20)
        Me.Label9.TabIndex = 68
        Me.Label9.Text = "- -"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(315, 261)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 20)
        Me.Label8.TabIndex = 67
        Me.Label8.Text = "- -"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(315, 231)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(23, 20)
        Me.Label7.TabIndex = 66
        Me.Label7.Text = "- -"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(315, 202)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 20)
        Me.Label6.TabIndex = 65
        Me.Label6.Text = "- -"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(315, 173)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 20)
        Me.Label5.TabIndex = 64
        Me.Label5.Text = "- -"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(315, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 20)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "- -"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(315, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 20)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "- -"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(315, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 20)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "- -"
        '
        'frmECoins
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 600)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(ContinentLabel)
        Me.Controls.Add(CountryLabel)
        Me.Controls.Add(Catalogue_CodeLabel)
        Me.Controls.Add(Currency_NameLabel)
        Me.Controls.Add(DenominationLabel)
        Me.Controls.Add(Date_Recorded_OnLabel)
        Me.Controls.Add(Cost_To_MeLabel)
        Me.Controls.Add(SourceLabel)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmECoins"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmECoins"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
End Class
