<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetting
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
        Me.components = New System.ComponentModel.Container()
        Dim Label2 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Currency_NameLabel As System.Windows.Forms.Label
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetting))
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtTocurrency = New System.Windows.Forms.ComboBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TableDataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Currency_Short_NamesDataSet = New Numistmatics__Final__working_.Currency_Short_NamesDataSet()
        Me.TableTableAdapter = New Numistmatics__Final__working_.Currency_Short_NamesDataSetTableAdapters.TableTableAdapter()
        Me.TableAdapterManager = New Numistmatics__Final__working_.Currency_Short_NamesDataSetTableAdapters.TableAdapterManager()
        Me.Button5 = New System.Windows.Forms.Button()
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Currency_NameLabel = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.TableDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Currency_Short_NamesDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.ForeColor = System.Drawing.Color.Black
        Label2.Location = New System.Drawing.Point(135, 217)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(47, 13)
        Label2.TabIndex = 71
        Label2.Text = "Source: "
        Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.ForeColor = System.Drawing.Color.Black
        Label1.Location = New System.Drawing.Point(54, 186)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(128, 13)
        Label1.TabIndex = 70
        Label1.Text = "Scanner Drive Selection :"
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Currency_NameLabel
        '
        Currency_NameLabel.AutoSize = True
        Currency_NameLabel.ForeColor = System.Drawing.Color.Black
        Currency_NameLabel.Location = New System.Drawing.Point(83, 85)
        Currency_NameLabel.Name = "Currency_NameLabel"
        Currency_NameLabel.Size = New System.Drawing.Size(99, 13)
        Currency_NameLabel.TabIndex = 65
        Currency_NameLabel.Text = "To Currency Name:"
        Currency_NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(241, 235)
        Me.Button3.Margin = New System.Windows.Forms.Padding(2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(107, 32)
        Me.Button3.TabIndex = 72
        Me.Button3.Text = "Clear Driver"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(299, 175)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(106, 32)
        Me.Button2.TabIndex = 69
        Me.Button2.Text = "Test Connection"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(187, 212)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(219, 20)
        Me.TextBox1.TabIndex = 68
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(187, 175)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(107, 32)
        Me.Button1.TabIndex = 67
        Me.Button1.Text = "Select Driver"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtTocurrency
        '
        Me.txtTocurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txtTocurrency.FormattingEnabled = True
        Me.txtTocurrency.Items.AddRange(New Object() {"AUD", "GBP", "EUR", "JPY", "CHF", "USD", "AFN", "ALL", "DZD", "AOA", "ARS", "AMD", "AWG", "AUD", "ATS ", "BEF", "AZN", "BSD", "BHD", "BDT", "BBD", "BYR", "BZD", "BMD", "BTN", "BOB", "BAM", "BWP", "BRL", "GBP", "BND", "BGN", "BIF", "XOF", "XAF", "XPF", "KHR", "CAD", "CVE", "KYD", "CLP", "CNY", "COP", "KMF", "CDF", "CRC", "HRK", "CUC", "CUP", "CYP", "CZK", "DKK", "DJF", "DOP", "XCD", "EGP", "SVC", "EEK", "ETB", "EUR", "FKP", "FIM", "FJD", "GMD", "GEL", "DMK", "GHS", "GIP", "GRD", "GTQ", "GNF", "GYD", "HTG", "HNL", "HKD", "HUF", "ISK", "INR", "IDR", "IRR", "IQD", "IED ", "ILS", "ITL", "JMD", "JPY", "JOD", "KZT", "KES", "KWD", "KGS", "LAK", "LVL", "LBP", "LSL", "LRD", "LYD", "LTL ", "LUF ", "MOP", "MKD", "MGA", "MWK", "MYR", "MVR", "MTL ", "MRO", "MUR", "MXN", "MDL", "MNT", "MAD", "MZN", "MMK", "ANG", "NAD", "NPR", "NLG ", "NZD", "NIO", "NGN", "KPW", "NOK", "OMR", "PKR", "PAB", "PGK", "PYG", "PEN", "PHP", "PLN", "PTE ", "QAR", "RON", "RUB", "RWF", "WST", "STD", "SAR", "RSD", "SCR", "SLL", "SGD", "SKK", "SIT ", "SBD", "SOS", "ZAR", "KRW", "ESP", "LKR", "SHP", "SDG", "SRD", "SZL", "SEK", "CHF", "SYP", "TWD", "TZS", "THB", "TOP", "TTD", "TND", "TRY", "TMM", "USD", "UGX", "UAH", "UYU", "AED", "VUV", "VEB", "VND", "YER", "ZMK", "ZWD"})
        Me.txtTocurrency.Location = New System.Drawing.Point(187, 82)
        Me.txtTocurrency.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTocurrency.Name = "txtTocurrency"
        Me.txtTocurrency.Size = New System.Drawing.Size(219, 21)
        Me.txtTocurrency.TabIndex = 66
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(53, 405)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(129, 104)
        Me.Button6.TabIndex = 73
        Me.Button6.Text = "Back"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button4)
        Me.SplitContainer1.Panel1.Controls.Add(Currency_NameLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTocurrency)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableDataGridView)
        Me.SplitContainer1.Size = New System.Drawing.Size(920, 613)
        Me.SplitContainer1.SplitterDistance = 493
        Me.SplitContainer1.TabIndex = 74
        '
        'Button4
        '
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.Location = New System.Drawing.Point(299, 405)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(129, 104)
        Me.Button4.TabIndex = 74
        Me.Button4.Text = "Change Password"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TableDataGridView
        '
        Me.TableDataGridView.AutoGenerateColumns = False
        Me.TableDataGridView.BackgroundColor = System.Drawing.Color.White
        Me.TableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn8})
        Me.TableDataGridView.DataSource = Me.TableBindingSource
        Me.TableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.TableDataGridView.Name = "TableDataGridView"
        Me.TableDataGridView.ReadOnly = True
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.MediumTurquoise
        Me.TableDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.TableDataGridView.Size = New System.Drawing.Size(423, 613)
        Me.TableDataGridView.TabIndex = 0
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "ID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Country_Currency"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn5.HeaderText = "Country_Currency"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Abbriviated_Names"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn8.HeaderText = "Abbriviated_Names"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'TableBindingSource
        '
        Me.TableBindingSource.DataMember = "Table"
        Me.TableBindingSource.DataSource = Me.Currency_Short_NamesDataSet
        '
        'Currency_Short_NamesDataSet
        '
        Me.Currency_Short_NamesDataSet.DataSetName = "Currency_Short_NamesDataSet"
        Me.Currency_Short_NamesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TableTableAdapter
        '
        Me.TableTableAdapter.ClearBeforeFill = True
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.TableTableAdapter = Me.TableTableAdapter
        Me.TableAdapterManager.UpdateOrder = Numistmatics__Final__working_.Currency_Short_NamesDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'Button5
        '
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Image = Global.Numistmatics__Final__working_.My.Resources.Resources.icons8_close_window_20
        Me.Button5.Location = New System.Drawing.Point(1, 1)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(22, 22)
        Me.Button5.TabIndex = 75
        Me.Button5.UseVisualStyleBackColor = True
        '
        'frmSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(920, 613)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Setting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.TableDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Currency_Short_NamesDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents txtTocurrency As ComboBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button6 As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Currency_Short_NamesDataSet As Currency_Short_NamesDataSet
    Friend WithEvents TableBindingSource As BindingSource
    Friend WithEvents TableTableAdapter As Currency_Short_NamesDataSetTableAdapters.TableTableAdapter
    Friend WithEvents TableAdapterManager As Currency_Short_NamesDataSetTableAdapters.TableAdapterManager
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents Button4 As Button
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents TableDataGridView As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents Button5 As Button
End Class
