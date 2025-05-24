<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGallery
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGallery))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tabNFrontal = New System.Windows.Forms.TabPage()
        Me.TableDataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.NotesDataSet = New Numistmatics__Final__working_.NotesDataSet()
        Me.lbFront = New System.Windows.Forms.ListBox()
        Me.lvFront = New System.Windows.Forms.ListView()
        Me.tabNBackward = New System.Windows.Forms.TabPage()
        Me.lbBack = New System.Windows.Forms.ListBox()
        Me.lvBack = New System.Windows.Forms.ListView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TableTableAdapter = New Numistmatics__Final__working_.NotesDataSetTableAdapters.TableTableAdapter()
        Me.TableAdapterManager = New Numistmatics__Final__working_.NotesDataSetTableAdapters.TableAdapterManager()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tabNFrontal.SuspendLayout()
        CType(Me.TableDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabNBackward.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TabControl2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1138, 549)
        Me.SplitContainer1.SplitterDistance = 507
        Me.SplitContainer1.TabIndex = 0
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.tabNFrontal)
        Me.TabControl2.Controls.Add(Me.tabNBackward)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(1138, 507)
        Me.TabControl2.TabIndex = 1
        '
        'tabNFrontal
        '
        Me.tabNFrontal.Controls.Add(Me.TableDataGridView)
        Me.tabNFrontal.Controls.Add(Me.lbFront)
        Me.tabNFrontal.Controls.Add(Me.lvFront)
        Me.tabNFrontal.Location = New System.Drawing.Point(4, 22)
        Me.tabNFrontal.Name = "tabNFrontal"
        Me.tabNFrontal.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNFrontal.Size = New System.Drawing.Size(1130, 481)
        Me.tabNFrontal.TabIndex = 0
        Me.tabNFrontal.Text = "Frontal Image"
        Me.tabNFrontal.UseVisualStyleBackColor = True
        '
        'TableDataGridView
        '
        Me.TableDataGridView.AutoGenerateColumns = False
        Me.TableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13})
        Me.TableDataGridView.DataSource = Me.TableBindingSource
        Me.TableDataGridView.Location = New System.Drawing.Point(706, 83)
        Me.TableDataGridView.Name = "TableDataGridView"
        Me.TableDataGridView.Size = New System.Drawing.Size(300, 220)
        Me.TableDataGridView.TabIndex = 3
        Me.TableDataGridView.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "ID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Continent"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Continent"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Country"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Country"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Catalogue_Code"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Catalogue_Code"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Currency_Name"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Currency_Name"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Denomination"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Denomination"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Date_Recorded_On"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Date_Recorded_On"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Serial_No"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Serial_No"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Condition"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Condition"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Cost_To_Me"
        Me.DataGridViewTextBoxColumn10.HeaderText = "Cost_To_Me"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Source"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Source"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Frontal_Image"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Frontal_Image"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "Backward_Image"
        Me.DataGridViewTextBoxColumn13.HeaderText = "Backward_Image"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        '
        'TableBindingSource
        '
        Me.TableBindingSource.DataMember = "Table"
        Me.TableBindingSource.DataSource = Me.NotesDataSet
        '
        'NotesDataSet
        '
        Me.NotesDataSet.DataSetName = "NotesDataSet"
        Me.NotesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lbFront
        '
        Me.lbFront.FormattingEnabled = True
        Me.lbFront.Location = New System.Drawing.Point(886, 325)
        Me.lbFront.Name = "lbFront"
        Me.lbFront.Size = New System.Drawing.Size(120, 95)
        Me.lbFront.TabIndex = 1
        Me.lbFront.Visible = False
        '
        'lvFront
        '
        Me.lvFront.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFront.HideSelection = False
        Me.lvFront.Location = New System.Drawing.Point(3, 3)
        Me.lvFront.Name = "lvFront"
        Me.lvFront.Size = New System.Drawing.Size(1124, 475)
        Me.lvFront.TabIndex = 0
        Me.lvFront.UseCompatibleStateImageBehavior = False
        '
        'tabNBackward
        '
        Me.tabNBackward.Controls.Add(Me.lbBack)
        Me.tabNBackward.Controls.Add(Me.lvBack)
        Me.tabNBackward.Location = New System.Drawing.Point(4, 22)
        Me.tabNBackward.Name = "tabNBackward"
        Me.tabNBackward.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNBackward.Size = New System.Drawing.Size(1130, 481)
        Me.tabNBackward.TabIndex = 1
        Me.tabNBackward.Text = "Backward Image"
        Me.tabNBackward.UseVisualStyleBackColor = True
        '
        'lbBack
        '
        Me.lbBack.FormattingEnabled = True
        Me.lbBack.Location = New System.Drawing.Point(692, 207)
        Me.lbBack.Name = "lbBack"
        Me.lbBack.Size = New System.Drawing.Size(120, 95)
        Me.lbBack.TabIndex = 2
        Me.lbBack.Visible = False
        '
        'lvBack
        '
        Me.lvBack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvBack.HideSelection = False
        Me.lvBack.Location = New System.Drawing.Point(3, 3)
        Me.lvBack.Name = "lvBack"
        Me.lvBack.Size = New System.Drawing.Size(1124, 475)
        Me.lvBack.TabIndex = 1
        Me.lvBack.UseCompatibleStateImageBehavior = False
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(1138, 38)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Back"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TableTableAdapter
        '
        Me.TableTableAdapter.ClearBeforeFill = True
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.TableTableAdapter = Me.TableTableAdapter
        Me.TableAdapterManager.UpdateOrder = Numistmatics__Final__working_.NotesDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'frmGallery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1138, 549)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGallery"
        Me.Text = "Notes Gallery"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.tabNFrontal.ResumeLayout(False)
        CType(Me.TableDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabNBackward.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Button1 As Button
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents tabNFrontal As TabPage
    Friend WithEvents lbFrontFileName As ListBox
    Friend WithEvents lbFront As ListBox
    Friend WithEvents lvFront As ListView
    Friend WithEvents tabNBackward As TabPage
    Friend WithEvents NotesDataSet As NotesDataSet
    Friend WithEvents TableBindingSource As BindingSource
    Friend WithEvents TableTableAdapter As NotesDataSetTableAdapters.TableTableAdapter
    Friend WithEvents TableAdapterManager As NotesDataSetTableAdapters.TableAdapterManager
    Friend WithEvents TableDataGridView As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
    Friend WithEvents lbBack As ListBox
    Friend WithEvents lvBack As ListView
End Class
