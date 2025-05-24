<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmENotes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim ContinentLabel As System.Windows.Forms.Label
        Dim CountryLabel As System.Windows.Forms.Label
        Dim Catalogue_CodeLabel As System.Windows.Forms.Label
        Dim Currency_NameLabel As System.Windows.Forms.Label
        Dim DenominationLabel As System.Windows.Forms.Label
        Dim Date_Recorded_OnLabel As System.Windows.Forms.Label
        Dim Serial_NoLabel As System.Windows.Forms.Label
        Dim ConditionLabel As System.Windows.Forms.Label
        Dim Cost_To_MeLabel As System.Windows.Forms.Label
        Dim SourceLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmENotes))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Frontal_Image = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Backward_Image = New System.Windows.Forms.PictureBox()
        ContinentLabel = New System.Windows.Forms.Label()
        CountryLabel = New System.Windows.Forms.Label()
        Catalogue_CodeLabel = New System.Windows.Forms.Label()
        Currency_NameLabel = New System.Windows.Forms.Label()
        DenominationLabel = New System.Windows.Forms.Label()
        Date_Recorded_OnLabel = New System.Windows.Forms.Label()
        Serial_NoLabel = New System.Windows.Forms.Label()
        ConditionLabel = New System.Windows.Forms.Label()
        Cost_To_MeLabel = New System.Windows.Forms.Label()
        SourceLabel = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Frontal_Image, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.Backward_Image, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContinentLabel
        '
        ContinentLabel.AutoSize = True
        ContinentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ContinentLabel.Location = New System.Drawing.Point(64, 37)
        ContinentLabel.Name = "ContinentLabel"
        ContinentLabel.Size = New System.Drawing.Size(82, 20)
        ContinentLabel.TabIndex = 21
        ContinentLabel.Text = "Continent:"
        '
        'CountryLabel
        '
        CountryLabel.AutoSize = True
        CountryLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        CountryLabel.Location = New System.Drawing.Point(64, 66)
        CountryLabel.Name = "CountryLabel"
        CountryLabel.Size = New System.Drawing.Size(68, 20)
        CountryLabel.TabIndex = 22
        CountryLabel.Text = "Country:"
        '
        'Catalogue_CodeLabel
        '
        Catalogue_CodeLabel.AutoSize = True
        Catalogue_CodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Catalogue_CodeLabel.Location = New System.Drawing.Point(64, 95)
        Catalogue_CodeLabel.Name = "Catalogue_CodeLabel"
        Catalogue_CodeLabel.Size = New System.Drawing.Size(128, 20)
        Catalogue_CodeLabel.TabIndex = 23
        Catalogue_CodeLabel.Text = "Catalogue Code:"
        '
        'Currency_NameLabel
        '
        Currency_NameLabel.AutoSize = True
        Currency_NameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Currency_NameLabel.Location = New System.Drawing.Point(64, 123)
        Currency_NameLabel.Name = "Currency_NameLabel"
        Currency_NameLabel.Size = New System.Drawing.Size(122, 20)
        Currency_NameLabel.TabIndex = 24
        Currency_NameLabel.Text = "Currency Name:"
        '
        'DenominationLabel
        '
        DenominationLabel.AutoSize = True
        DenominationLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DenominationLabel.Location = New System.Drawing.Point(64, 152)
        DenominationLabel.Name = "DenominationLabel"
        DenominationLabel.Size = New System.Drawing.Size(112, 20)
        DenominationLabel.TabIndex = 25
        DenominationLabel.Text = "Denomination:"
        '
        'Date_Recorded_OnLabel
        '
        Date_Recorded_OnLabel.AutoSize = True
        Date_Recorded_OnLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Date_Recorded_OnLabel.Location = New System.Drawing.Point(64, 181)
        Date_Recorded_OnLabel.Name = "Date_Recorded_OnLabel"
        Date_Recorded_OnLabel.Size = New System.Drawing.Size(147, 20)
        Date_Recorded_OnLabel.TabIndex = 26
        Date_Recorded_OnLabel.Text = "Date Recorded On:"
        '
        'Serial_NoLabel
        '
        Serial_NoLabel.AutoSize = True
        Serial_NoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Serial_NoLabel.Location = New System.Drawing.Point(64, 209)
        Serial_NoLabel.Name = "Serial_NoLabel"
        Serial_NoLabel.Size = New System.Drawing.Size(77, 20)
        Serial_NoLabel.TabIndex = 27
        Serial_NoLabel.Text = "Serial No:"
        '
        'ConditionLabel
        '
        ConditionLabel.AutoSize = True
        ConditionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ConditionLabel.Location = New System.Drawing.Point(64, 237)
        ConditionLabel.Name = "ConditionLabel"
        ConditionLabel.Size = New System.Drawing.Size(80, 20)
        ConditionLabel.TabIndex = 28
        ConditionLabel.Text = "Condition:"
        '
        'Cost_To_MeLabel
        '
        Cost_To_MeLabel.AutoSize = True
        Cost_To_MeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Cost_To_MeLabel.Location = New System.Drawing.Point(64, 266)
        Cost_To_MeLabel.Name = "Cost_To_MeLabel"
        Cost_To_MeLabel.Size = New System.Drawing.Size(94, 20)
        Cost_To_MeLabel.TabIndex = 29
        Cost_To_MeLabel.Text = "Cost To Me:"
        '
        'SourceLabel
        '
        SourceLabel.AutoSize = True
        SourceLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SourceLabel.Location = New System.Drawing.Point(64, 295)
        SourceLabel.Name = "SourceLabel"
        SourceLabel.Size = New System.Drawing.Size(64, 20)
        SourceLabel.TabIndex = 30
        SourceLabel.Text = "Source:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(962, 43)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(433, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 33)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Notes"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 43)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(ContinentLabel)
        Me.SplitContainer1.Panel1.Controls.Add(CountryLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Catalogue_CodeLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Currency_NameLabel)
        Me.SplitContainer1.Panel1.Controls.Add(DenominationLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Date_Recorded_OnLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Serial_NoLabel)
        Me.SplitContainer1.Panel1.Controls.Add(ConditionLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Cost_To_MeLabel)
        Me.SplitContainer1.Panel1.Controls.Add(SourceLabel)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(962, 557)
        Me.SplitContainer1.SplitterDistance = 386
        Me.SplitContainer1.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(299, 295)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(23, 20)
        Me.Label11.TabIndex = 50
        Me.Label11.Text = "- -"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(299, 266)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(23, 20)
        Me.Label10.TabIndex = 49
        Me.Label10.Text = "- -"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(299, 237)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 20)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "- -"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(299, 209)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 20)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "- -"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(299, 181)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(23, 20)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "- -"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(299, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 20)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "- -"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(299, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 20)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "- -"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(299, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 20)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "- -"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(299, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 20)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "- -"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(299, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 20)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "- -"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer2.Size = New System.Drawing.Size(962, 167)
        Me.SplitContainer2.SplitterDistance = 481
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Frontal_Image)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(481, 167)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Frontal Image"
        '
        'Frontal_Image
        '
        Me.Frontal_Image.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Frontal_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frontal_Image.Location = New System.Drawing.Point(3, 16)
        Me.Frontal_Image.Name = "Frontal_Image"
        Me.Frontal_Image.Size = New System.Drawing.Size(475, 148)
        Me.Frontal_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Frontal_Image.TabIndex = 31
        Me.Frontal_Image.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Backward_Image)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(477, 167)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Backward Image"
        '
        'Backward_Image
        '
        Me.Backward_Image.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Backward_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Backward_Image.Location = New System.Drawing.Point(3, 16)
        Me.Backward_Image.Name = "Backward_Image"
        Me.Backward_Image.Size = New System.Drawing.Size(471, 148)
        Me.Backward_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Backward_Image.TabIndex = 32
        Me.Backward_Image.TabStop = False
        '
        'frmENotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(962, 600)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmENotes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmENotes"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.Frontal_Image, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.Backward_Image, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Frontal_Image As PictureBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Backward_Image As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
End Class
