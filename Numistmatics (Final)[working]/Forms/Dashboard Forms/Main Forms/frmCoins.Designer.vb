<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCoins
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
        Dim ContinentLabel As System.Windows.Forms.Label
        Dim CountryLabel As System.Windows.Forms.Label
        Dim Catalogue_CodeLabel As System.Windows.Forms.Label
        Dim Currency_NameLabel As System.Windows.Forms.Label
        Dim Date_Recorded_OnLabel As System.Windows.Forms.Label
        Dim Cost_To_MeLabel As System.Windows.Forms.Label
        Dim SourceLabel As System.Windows.Forms.Label
        Dim DenominationLabel As System.Windows.Forms.Label
        Dim CommentLabel As System.Windows.Forms.Label
        Dim IDLabel1 As System.Windows.Forms.Label
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCoins))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbtxtSearch = New System.Windows.Forms.ComboBox()
        Me.butReset = New System.Windows.Forms.Button()
        Me.dtpSearch = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.IDTextBox = New System.Windows.Forms.Label()
        Me.TableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CoinsDataSet = New Numistmatics__Final__working_.CoinsDataSet()
        Me.CommentRichTextBox = New System.Windows.Forms.RichTextBox()
        Me.DenominationTextBox = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ContinentComboBox = New System.Windows.Forms.ComboBox()
        Me.CountryComboBox = New System.Windows.Forms.ComboBox()
        Me.TableBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.CurrencyShortNamesDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Currency_Short_NamesDataSet = New Numistmatics__Final__working_.Currency_Short_NamesDataSet()
        Me.Catalogue_CodeTextBox = New System.Windows.Forms.TextBox()
        Me.Currency_NameComboBox = New System.Windows.Forms.ComboBox()
        Me.Date_Recorded_OnTextBox = New System.Windows.Forms.TextBox()
        Me.Cost_To_MeTextBox = New System.Windows.Forms.TextBox()
        Me.SourceTextBox = New System.Windows.Forms.TextBox()
        Me.TableDataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.TableBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.TableTableAdapter = New Numistmatics__Final__working_.CoinsDataSetTableAdapters.TableTableAdapter()
        Me.TableAdapterManager = New Numistmatics__Final__working_.CoinsDataSetTableAdapters.TableAdapterManager()
        Me.NotesDataSet = New Numistmatics__Final__working_.NotesDataSet()
        Me.TableBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableTableAdapter2 = New Numistmatics__Final__working_.Currency_Short_NamesDataSetTableAdapters.TableTableAdapter()
        Me.TableBindingSource4 = New System.Windows.Forms.BindingSource(Me.components)
        ContinentLabel = New System.Windows.Forms.Label()
        CountryLabel = New System.Windows.Forms.Label()
        Catalogue_CodeLabel = New System.Windows.Forms.Label()
        Currency_NameLabel = New System.Windows.Forms.Label()
        Date_Recorded_OnLabel = New System.Windows.Forms.Label()
        Cost_To_MeLabel = New System.Windows.Forms.Label()
        SourceLabel = New System.Windows.Forms.Label()
        DenominationLabel = New System.Windows.Forms.Label()
        CommentLabel = New System.Windows.Forms.Label()
        IDLabel1 = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.TableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CoinsDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CurrencyShortNamesDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Currency_Short_NamesDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableBindingNavigator.SuspendLayout()
        CType(Me.NotesDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableBindingSource4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContinentLabel
        '
        ContinentLabel.AutoSize = True
        ContinentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ContinentLabel.Location = New System.Drawing.Point(13, 43)
        ContinentLabel.Name = "ContinentLabel"
        ContinentLabel.Size = New System.Drawing.Size(65, 16)
        ContinentLabel.TabIndex = 2
        ContinentLabel.Text = "Continent:"
        '
        'CountryLabel
        '
        CountryLabel.AutoSize = True
        CountryLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        CountryLabel.Location = New System.Drawing.Point(13, 70)
        CountryLabel.Name = "CountryLabel"
        CountryLabel.Size = New System.Drawing.Size(55, 16)
        CountryLabel.TabIndex = 4
        CountryLabel.Text = "Country:"
        '
        'Catalogue_CodeLabel
        '
        Catalogue_CodeLabel.AutoSize = True
        Catalogue_CodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Catalogue_CodeLabel.Location = New System.Drawing.Point(13, 97)
        Catalogue_CodeLabel.Name = "Catalogue_CodeLabel"
        Catalogue_CodeLabel.Size = New System.Drawing.Size(108, 16)
        Catalogue_CodeLabel.TabIndex = 6
        Catalogue_CodeLabel.Text = "Catalogue Code:"
        '
        'Currency_NameLabel
        '
        Currency_NameLabel.AutoSize = True
        Currency_NameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Currency_NameLabel.Location = New System.Drawing.Point(13, 123)
        Currency_NameLabel.Name = "Currency_NameLabel"
        Currency_NameLabel.Size = New System.Drawing.Size(103, 16)
        Currency_NameLabel.TabIndex = 8
        Currency_NameLabel.Text = "Currency Name:"
        '
        'Date_Recorded_OnLabel
        '
        Date_Recorded_OnLabel.AutoSize = True
        Date_Recorded_OnLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Date_Recorded_OnLabel.Location = New System.Drawing.Point(13, 177)
        Date_Recorded_OnLabel.Name = "Date_Recorded_OnLabel"
        Date_Recorded_OnLabel.Size = New System.Drawing.Size(123, 16)
        Date_Recorded_OnLabel.TabIndex = 12
        Date_Recorded_OnLabel.Text = "Date Recorded On:"
        '
        'Cost_To_MeLabel
        '
        Cost_To_MeLabel.AutoSize = True
        Cost_To_MeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Cost_To_MeLabel.Location = New System.Drawing.Point(13, 203)
        Cost_To_MeLabel.Name = "Cost_To_MeLabel"
        Cost_To_MeLabel.Size = New System.Drawing.Size(79, 16)
        Cost_To_MeLabel.TabIndex = 14
        Cost_To_MeLabel.Text = "Cost To Me:"
        '
        'SourceLabel
        '
        SourceLabel.AutoSize = True
        SourceLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SourceLabel.Location = New System.Drawing.Point(13, 229)
        SourceLabel.Name = "SourceLabel"
        SourceLabel.Size = New System.Drawing.Size(53, 16)
        SourceLabel.TabIndex = 16
        SourceLabel.Text = "Source:"
        '
        'DenominationLabel
        '
        DenominationLabel.AutoSize = True
        DenominationLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DenominationLabel.Location = New System.Drawing.Point(13, 150)
        DenominationLabel.Name = "DenominationLabel"
        DenominationLabel.Size = New System.Drawing.Size(93, 16)
        DenominationLabel.TabIndex = 10
        DenominationLabel.Text = "Denomination:"
        '
        'CommentLabel
        '
        CommentLabel.AutoSize = True
        CommentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        CommentLabel.Location = New System.Drawing.Point(13, 260)
        CommentLabel.Name = "CommentLabel"
        CommentLabel.Size = New System.Drawing.Size(67, 16)
        CommentLabel.TabIndex = 40
        CommentLabel.Text = "Comment:"
        '
        'IDLabel1
        '
        IDLabel1.AutoSize = True
        IDLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IDLabel1.Location = New System.Drawing.Point(55, 16)
        IDLabel1.Name = "IDLabel1"
        IDLabel1.Size = New System.Drawing.Size(23, 16)
        IDLabel1.TabIndex = 40
        IDLabel1.Text = "ID:"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableDataGridView)
        Me.SplitContainer1.Size = New System.Drawing.Size(1383, 770)
        Me.SplitContainer1.SplitterDistance = 546
        Me.SplitContainer1.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.FlowLayoutPanel1)
        Me.GroupBox4.Location = New System.Drawing.Point(646, 157)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(426, 111)
        Me.GroupBox4.TabIndex = 40
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Controls"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Button6)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button5)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button3)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button4)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 16)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(420, 92)
        Me.FlowLayoutPanel1.TabIndex = 41
        '
        'Button5
        '
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.Image = Global.Numistmatics__Final__working_.My.Resources.Resources.back
        Me.Button5.Location = New System.Drawing.Point(108, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(99, 86)
        Me.Button5.TabIndex = 0
        Me.Button5.Text = "Back"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.Image = Global.Numistmatics__Final__working_.My.Resources.Resources.todayvalue
        Me.Button3.Location = New System.Drawing.Point(213, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(99, 86)
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "Today's Valuation"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.Image = Global.Numistmatics__Final__working_.My.Resources.Resources.more
        Me.Button4.Location = New System.Drawing.Point(318, 3)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(99, 86)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "More"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button6.Image = Global.Numistmatics__Final__working_.My.Resources.Resources.Refresh1
        Me.Button6.Location = New System.Drawing.Point(3, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(99, 86)
        Me.Button6.TabIndex = 3
        Me.Button6.Text = "Refresh"
        Me.Button6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button6.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbtxtSearch)
        Me.GroupBox3.Controls.Add(Me.butReset)
        Me.GroupBox3.Controls.Add(Me.dtpSearch)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.cmbCategory)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(646, 37)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Size = New System.Drawing.Size(426, 115)
        Me.GroupBox3.TabIndex = 37
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Searching Area"
        '
        'cmbtxtSearch
        '
        Me.cmbtxtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtxtSearch.FormattingEnabled = True
        Me.cmbtxtSearch.Location = New System.Drawing.Point(177, 50)
        Me.cmbtxtSearch.Name = "cmbtxtSearch"
        Me.cmbtxtSearch.Size = New System.Drawing.Size(238, 26)
        Me.cmbtxtSearch.TabIndex = 38
        '
        'butReset
        '
        Me.butReset.Cursor = System.Windows.Forms.Cursors.Hand
        Me.butReset.Location = New System.Drawing.Point(14, 81)
        Me.butReset.Name = "butReset"
        Me.butReset.Size = New System.Drawing.Size(157, 26)
        Me.butReset.TabIndex = 38
        Me.butReset.Text = "Reset"
        Me.butReset.UseVisualStyleBackColor = True
        '
        'dtpSearch
        '
        Me.dtpSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSearch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSearch.Location = New System.Drawing.Point(177, 81)
        Me.dtpSearch.Name = "dtpSearch"
        Me.dtpSearch.Size = New System.Drawing.Size(238, 26)
        Me.dtpSearch.TabIndex = 38
        Me.dtpSearch.Value = New Date(2021, 1, 29, 16, 47, 37, 0)
        Me.dtpSearch.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(39, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 18)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Category"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Items.AddRange(New Object() {"Continent", "Country", "Catalogue Code", "Currency Name", "Denomination", "Date Recorded On", "Source"})
        Me.cmbCategory.Location = New System.Drawing.Point(14, 50)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(157, 26)
        Me.cmbCategory.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(231, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 18)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Search Item"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.IDTextBox)
        Me.GroupBox1.Controls.Add(IDLabel1)
        Me.GroupBox1.Controls.Add(Me.CommentRichTextBox)
        Me.GroupBox1.Controls.Add(CommentLabel)
        Me.GroupBox1.Controls.Add(Me.DenominationTextBox)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button7)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(ContinentLabel)
        Me.GroupBox1.Controls.Add(Me.ContinentComboBox)
        Me.GroupBox1.Controls.Add(CountryLabel)
        Me.GroupBox1.Controls.Add(Me.CountryComboBox)
        Me.GroupBox1.Controls.Add(Catalogue_CodeLabel)
        Me.GroupBox1.Controls.Add(Me.Catalogue_CodeTextBox)
        Me.GroupBox1.Controls.Add(Currency_NameLabel)
        Me.GroupBox1.Controls.Add(Me.Currency_NameComboBox)
        Me.GroupBox1.Controls.Add(DenominationLabel)
        Me.GroupBox1.Controls.Add(Date_Recorded_OnLabel)
        Me.GroupBox1.Controls.Add(Me.Date_Recorded_OnTextBox)
        Me.GroupBox1.Controls.Add(Cost_To_MeLabel)
        Me.GroupBox1.Controls.Add(Me.Cost_To_MeTextBox)
        Me.GroupBox1.Controls.Add(SourceLabel)
        Me.GroupBox1.Controls.Add(Me.SourceTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 37)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(628, 384)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Content"
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "ID", True))
        Me.IDTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IDTextBox.Location = New System.Drawing.Point(162, 16)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.Size = New System.Drawing.Size(100, 23)
        Me.IDTextBox.TabIndex = 41
        Me.IDTextBox.Text = "Label3"
        '
        'TableBindingSource
        '
        Me.TableBindingSource.DataMember = "Table"
        Me.TableBindingSource.DataSource = Me.CoinsDataSet
        '
        'CoinsDataSet
        '
        Me.CoinsDataSet.DataSetName = "CoinsDataSet"
        Me.CoinsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CommentRichTextBox
        '
        Me.CommentRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CommentRichTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Comment", True))
        Me.CommentRichTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CommentRichTextBox.Location = New System.Drawing.Point(165, 257)
        Me.CommentRichTextBox.MaxLength = 500
        Me.CommentRichTextBox.Name = "CommentRichTextBox"
        Me.CommentRichTextBox.Size = New System.Drawing.Size(333, 107)
        Me.CommentRichTextBox.TabIndex = 10
        Me.CommentRichTextBox.Text = ""
        '
        'DenominationTextBox
        '
        Me.DenominationTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Denomination", True))
        Me.DenominationTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DenominationTextBox.Location = New System.Drawing.Point(165, 150)
        Me.DenominationTextBox.Name = "DenominationTextBox"
        Me.DenominationTextBox.Size = New System.Drawing.Size(195, 22)
        Me.DenominationTextBox.TabIndex = 4
        '
        'Button2
        '
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.Image = Global.Numistmatics__Final__working_.My.Resources.Resources.none
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(366, 229)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(132, 25)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "None"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button7.Location = New System.Drawing.Point(366, 123)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(132, 24)
        Me.Button7.TabIndex = 11
        Me.Button7.Text = "Currency Abbriviations"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(504, 179)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(89, 20)
        Me.DateTimePicker1.TabIndex = 6
        Me.DateTimePicker1.Value = New Date(2021, 1, 29, 16, 47, 37, 0)
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Location = New System.Drawing.Point(366, 177)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(132, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Get today's Date"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ContinentComboBox
        '
        Me.ContinentComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Continent", True))
        Me.ContinentComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContinentComboBox.FormattingEnabled = True
        Me.ContinentComboBox.Items.AddRange(New Object() {"Asia", "Africa", "Australia", "Antarctica", "Europe", "North America", "South America"})
        Me.ContinentComboBox.Location = New System.Drawing.Point(165, 43)
        Me.ContinentComboBox.Name = "ContinentComboBox"
        Me.ContinentComboBox.Size = New System.Drawing.Size(195, 24)
        Me.ContinentComboBox.TabIndex = 0
        '
        'CountryComboBox
        '
        Me.CountryComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Country", True))
        Me.CountryComboBox.DataSource = Me.TableBindingSource1
        Me.CountryComboBox.DisplayMember = "Country_Currency"
        Me.CountryComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CountryComboBox.FormattingEnabled = True
        Me.CountryComboBox.Location = New System.Drawing.Point(165, 70)
        Me.CountryComboBox.Name = "CountryComboBox"
        Me.CountryComboBox.Size = New System.Drawing.Size(195, 24)
        Me.CountryComboBox.TabIndex = 1
        '
        'TableBindingSource1
        '
        Me.TableBindingSource1.DataMember = "Table"
        Me.TableBindingSource1.DataSource = Me.CurrencyShortNamesDataSetBindingSource
        '
        'CurrencyShortNamesDataSetBindingSource
        '
        Me.CurrencyShortNamesDataSetBindingSource.DataSource = Me.Currency_Short_NamesDataSet
        Me.CurrencyShortNamesDataSetBindingSource.Position = 0
        '
        'Currency_Short_NamesDataSet
        '
        Me.Currency_Short_NamesDataSet.DataSetName = "Currency_Short_NamesDataSet"
        Me.Currency_Short_NamesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Catalogue_CodeTextBox
        '
        Me.Catalogue_CodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Catalogue_Code", True))
        Me.Catalogue_CodeTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Catalogue_CodeTextBox.Location = New System.Drawing.Point(165, 97)
        Me.Catalogue_CodeTextBox.Name = "Catalogue_CodeTextBox"
        Me.Catalogue_CodeTextBox.Size = New System.Drawing.Size(195, 22)
        Me.Catalogue_CodeTextBox.TabIndex = 2
        '
        'Currency_NameComboBox
        '
        Me.Currency_NameComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Currency_Name", True))
        Me.Currency_NameComboBox.DataSource = Me.TableBindingSource1
        Me.Currency_NameComboBox.DisplayMember = "Abbriviated_Names"
        Me.Currency_NameComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Currency_NameComboBox.FormattingEnabled = True
        Me.Currency_NameComboBox.Location = New System.Drawing.Point(165, 123)
        Me.Currency_NameComboBox.Name = "Currency_NameComboBox"
        Me.Currency_NameComboBox.Size = New System.Drawing.Size(195, 24)
        Me.Currency_NameComboBox.TabIndex = 3
        '
        'Date_Recorded_OnTextBox
        '
        Me.Date_Recorded_OnTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Date_Recorded_On", True))
        Me.Date_Recorded_OnTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Date_Recorded_OnTextBox.Location = New System.Drawing.Point(165, 177)
        Me.Date_Recorded_OnTextBox.Name = "Date_Recorded_OnTextBox"
        Me.Date_Recorded_OnTextBox.ReadOnly = True
        Me.Date_Recorded_OnTextBox.Size = New System.Drawing.Size(195, 22)
        Me.Date_Recorded_OnTextBox.TabIndex = 11
        '
        'Cost_To_MeTextBox
        '
        Me.Cost_To_MeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Cost_To_Me", True))
        Me.Cost_To_MeTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cost_To_MeTextBox.Location = New System.Drawing.Point(165, 203)
        Me.Cost_To_MeTextBox.Name = "Cost_To_MeTextBox"
        Me.Cost_To_MeTextBox.Size = New System.Drawing.Size(195, 22)
        Me.Cost_To_MeTextBox.TabIndex = 7
        '
        'SourceTextBox
        '
        Me.SourceTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TableBindingSource, "Source", True))
        Me.SourceTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SourceTextBox.Location = New System.Drawing.Point(165, 229)
        Me.SourceTextBox.Name = "SourceTextBox"
        Me.SourceTextBox.Size = New System.Drawing.Size(195, 22)
        Me.SourceTextBox.TabIndex = 8
        '
        'TableDataGridView
        '
        Me.TableDataGridView.AutoGenerateColumns = False
        Me.TableDataGridView.BackgroundColor = System.Drawing.SystemColors.Control
        Me.TableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
        Me.TableDataGridView.DataSource = Me.TableBindingSource
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Coral
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TableDataGridView.DefaultCellStyle = DataGridViewCellStyle1
        Me.TableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.TableDataGridView.Name = "TableDataGridView"
        Me.TableDataGridView.ReadOnly = True
        Me.TableDataGridView.Size = New System.Drawing.Size(1383, 220)
        Me.TableDataGridView.TabIndex = 1
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Continent"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Continent"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Country"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Country"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Catalogue_Code"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Catalogue Code"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Currency_Name"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Currency Name"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Denomination"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Denomination"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Date_Recorded_On"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Date Recorded On"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Cost_To_Me"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Cost To Me"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Source"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Source"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'TableBindingNavigator
        '
        Me.TableBindingNavigator.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.TableBindingNavigator.BindingSource = Me.TableBindingSource
        Me.TableBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.TableBindingNavigator.DeleteItem = Nothing
        Me.TableBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.TableBindingNavigatorSaveItem})
        Me.TableBindingNavigator.Location = New System.Drawing.Point(0, 0)
        Me.TableBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.TableBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.TableBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.TableBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.TableBindingNavigator.Name = "TableBindingNavigator"
        Me.TableBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.TableBindingNavigator.Size = New System.Drawing.Size(1383, 25)
        Me.TableBindingNavigator.TabIndex = 1
        Me.TableBindingNavigator.Text = "BindingNavigator1"
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'TableBindingNavigatorSaveItem
        '
        Me.TableBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TableBindingNavigatorSaveItem.Enabled = False
        Me.TableBindingNavigatorSaveItem.Image = CType(resources.GetObject("TableBindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.TableBindingNavigatorSaveItem.Name = "TableBindingNavigatorSaveItem"
        Me.TableBindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
        Me.TableBindingNavigatorSaveItem.Text = "Save Data"
        '
        'TableTableAdapter
        '
        Me.TableTableAdapter.ClearBeforeFill = True
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.TableTableAdapter = Me.TableTableAdapter
        Me.TableAdapterManager.UpdateOrder = Numistmatics__Final__working_.CoinsDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'NotesDataSet
        '
        Me.NotesDataSet.DataSetName = "NotesDataSet"
        Me.NotesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TableBindingSource2
        '
        Me.TableBindingSource2.DataMember = "Table"
        Me.TableBindingSource2.DataSource = Me.CurrencyShortNamesDataSetBindingSource
        '
        'TableTableAdapter2
        '
        Me.TableTableAdapter2.ClearBeforeFill = True
        '
        'TableBindingSource4
        '
        Me.TableBindingSource4.DataMember = "Table"
        Me.TableBindingSource4.DataSource = Me.CurrencyShortNamesDataSetBindingSource
        '
        'frmCoins
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1383, 770)
        Me.Controls.Add(Me.TableBindingNavigator)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCoins"
        Me.Text = "Coins"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.TableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CoinsDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CurrencyShortNamesDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Currency_Short_NamesDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableBindingNavigator.ResumeLayout(False)
        Me.TableBindingNavigator.PerformLayout()
        CType(Me.NotesDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableBindingSource4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CoinsDataSet As CoinsDataSet
    Friend WithEvents TableBindingSource As BindingSource
    Friend WithEvents TableTableAdapter As CoinsDataSetTableAdapters.TableTableAdapter
    Friend WithEvents TableAdapterManager As CoinsDataSetTableAdapters.TableAdapterManager
    Friend WithEvents TableBindingNavigator As BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents TableBindingNavigatorSaveItem As ToolStripButton
    Friend WithEvents ContinentComboBox As ComboBox
    Friend WithEvents CountryComboBox As ComboBox
    Friend WithEvents Catalogue_CodeTextBox As TextBox
    Friend WithEvents Currency_NameComboBox As ComboBox
    Friend WithEvents Date_Recorded_OnTextBox As TextBox
    Friend WithEvents Cost_To_MeTextBox As TextBox
    Friend WithEvents SourceTextBox As TextBox
    Friend WithEvents TableDataGridView As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Button1 As Button
    Friend WithEvents DenominationTextBox As TextBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpSearch As DateTimePicker
    Friend WithEvents butReset As Button
    Friend WithEvents cmbtxtSearch As ComboBox
    Friend WithEvents Button6 As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents CommentRichTextBox As RichTextBox
    Friend WithEvents Button7 As Button
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents NotesDataSet As NotesDataSet
    Friend WithEvents IDTextBox As Label
    Friend WithEvents CurrencyShortNamesDataSetBindingSource As BindingSource
    Friend WithEvents Currency_Short_NamesDataSet As Currency_Short_NamesDataSet
    Friend WithEvents TableBindingSource2 As BindingSource
    Friend WithEvents TableTableAdapter2 As Currency_Short_NamesDataSetTableAdapters.TableTableAdapter
    Friend WithEvents TableBindingSource4 As BindingSource
    Friend WithEvents TableBindingSource1 As BindingSource
End Class
