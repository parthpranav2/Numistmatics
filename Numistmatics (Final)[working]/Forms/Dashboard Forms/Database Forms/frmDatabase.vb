Imports System.IO
Imports System.Data.OleDb
Imports System.Windows.Forms

Public Class frmDatabase
    Dim folder As String = ("Resources\Images")
    Dim conn As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Currency_Short_Names.accdb")
    Dim conn1 As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Coins.accdb")
    Dim conn2 As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Notes.accdb")


    ' Use constants for folder names and combine paths robustly
    Private Const ResourcesFolder As String = "Resources"
    Private Const ImagesFolder As String = "Images"
    Private ReadOnly FullImagePath As String = Path.Combine(Application.StartupPath, ResourcesFolder, ImagesFolder)




    Private Sub TableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.TableBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.CoinsDataSet)

    End Sub

    Private Sub frmDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize DataSets and TableAdapters if not done by designer
        ' Example (repeat for all DataSets/TableAdapters/BindingSources):
        If Me.CoinsDataSet Is Nothing Then Me.CoinsDataSet = New CoinsDataSet()
        If Me.TableTableAdapter Is Nothing Then Me.TableTableAdapter = New CoinsDataSetTableAdapters.TableTableAdapter()
        If Me.TableBindingSource Is Nothing Then Me.TableBindingSource = New BindingSource()
        Me.TableBindingSource.DataSource = Me.CoinsDataSet
        Me.TableBindingSource.DataMember = "Table"
        Me.TableDataGridView.DataSource = Me.TableBindingSource ' Ensure DataGridView is bound

        ' Repeat for NotesDataSet/TableBindingSource1/TableDataGridView1
        ' Repeat for Currency_Short_NamesDataSet/TableBindingSource2/TableDataGridView2

        Try
            RefreshAllDatabases() ' Call the centralized refresh method
        Catch ex As Exception
            MessageBox.Show($"A critical error occurred during database loading: {ex.Message}{Environment.NewLine}The application may not function correctly. Please check database files and configurations.", "Critical Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Critical Load Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            ' Consider more robust error recovery or exit strategy here
            If MessageBox.Show("Do you want to go to Backup and Restore?", "Recovery Option", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                frmBackupRestore.Show()
                ' Assuming tabRestore is a TabPage or similar control on frmBackupRestore
                ' frmBackupRestore.tabRestore.Show() ' Or frmBackupRestore.TabControl.SelectTab(1)
                Me.Hide()
            Else
                System.Windows.Forms.Application.Exit() ' Exit if user doesn't want to recover
            End If
        End Try

        ActiveControls() ' Set initial active control based on tab

        ' Ensure image folder exists and is hidden
        If Not Directory.Exists(FullImagePath) Then
            Directory.CreateDirectory(FullImagePath)
        End If
        ' Set attributes for parent folders if they exist
        If Directory.Exists(Path.Combine(Application.StartupPath, ResourcesFolder)) Then
            File.SetAttributes(Path.Combine(Application.StartupPath, ResourcesFolder), FileAttributes.Hidden)
        End If
        If Directory.Exists(FullImagePath) Then
            File.SetAttributes(FullImagePath, FileAttributes.Hidden)
        End If

    End Sub

    ' Centralized method to refresh all database data
    Public Sub RefreshAllDatabases()
        Try
            Me.TableTableAdapter2.Fill(Me.Currency_Short_NamesDataSet.Table)
        Catch ex As OleDbException
            MessageBox.Show($"Database error loading Currency Short Names: {ex.Message}", "Database Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"OleDbException (Currency Names Refresh): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred loading Currency Short Names: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (Currency Names Refresh): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try

        Try
            Me.TableTableAdapter1.Fill(Me.NotesDataSet.Table)
        Catch ex As OleDbException
            MessageBox.Show($"Database error loading Notes data: {ex.Message}", "Database Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"OleDbException (Notes Refresh): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred loading Notes data: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (Notes Refresh): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try

        Try
            Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
        Catch ex As OleDbException
            MessageBox.Show($"Database error loading Coins data: {ex.Message}", "Database Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"OleDbException (Coins Refresh): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred loading Coins data: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (Coins Refresh): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

    ' Helper function to apply filter to a DataGridView
    Private Sub ApplyFilter(ByVal categoryComboBox As ComboBox, ByVal searchTextBox As ComboBox, ByVal bindingSource As BindingSource, ByVal dataGridView As DataGridView, ByVal isPerfectMatch As Boolean)
        Dim filterColumn As String = ""
        Dim filterValue As String = searchTextBox.Text.Replace("'", "''") ' Escape single quotes

        ' Determine the column to filter based on categoryComboBox.SelectedIndex
        Select Case categoryComboBox.Name ' Use name to differentiate between cmbCategory, ComboBox2, ComboBox4
            Case "cmbCategory" ' For TableDataGridView2 (Currency_Short_Names)
                Select Case categoryComboBox.SelectedIndex
                    Case 0 : filterColumn = "Country_Currency"
                    Case 1 : filterColumn = "Full_Name"
                    Case 2 : filterColumn = "Local_Names"
                    Case 3 : filterColumn = "Abbriviated_Names"
                    Case Else : bindingSource.Filter = Nothing : Return
                End Select
            Case "ComboBox2" ' For TableDataGridView (Coins)
                Select Case categoryComboBox.SelectedIndex
                    Case 0 : filterColumn = "Continent"
                    Case 1 : filterColumn = "Country"
                    Case 2 : filterColumn = "Catalogue_Code"
                    Case 3 : filterColumn = "Currency_Name"
                        filterValue = filterValue.ToUpper()
                    Case 4 : filterColumn = "Denomination"
                    Case 5 : filterColumn = "Date_Recorded_On"
                    Case 6 : filterColumn = "Source"
                    Case Else : bindingSource.Filter = Nothing : Return
                End Select
            Case "ComboBox4" ' For TableDataGridView1 (Notes)
                Select Case categoryComboBox.SelectedIndex
                    Case 0 : filterColumn = "Continent"
                    Case 1 : filterColumn = "Country"
                    Case 2 : filterColumn = "Catalogue_Code"
                    Case 3 : filterColumn = "Currency_Name"
                        filterValue = filterValue.ToUpper()
                    Case 4 : filterColumn = "Denomination"
                    Case 5 : filterColumn = "Date_Recorded_On"
                    Case 6 : filterColumn = "Serial_No"
                    Case 7 : filterColumn = "Condition"
                    Case 8 : filterColumn = "Source"
                    Case Else : bindingSource.Filter = Nothing : Return
                End Select
            Case Else : bindingSource.Filter = Nothing : Return
        End Select

        Try
            Dim filterString As String
            If isPerfectMatch Then
                If filterColumn = "Date_Recorded_On" Then
                    filterString = $"FORMAT({filterColumn}, 'dd/mm/yyyy') = '{filterValue}'"
                Else
                    filterString = $"{filterColumn} = '{filterValue}'"
                End If
            Else
                If filterColumn = "Date_Recorded_On" Then
                    filterString = $"FORMAT({filterColumn}, 'dd/mm/yyyy') LIKE '%{filterValue}%'"
                Else
                    filterString = $"{filterColumn} LIKE '%{filterValue}%'"
                End If
            End If
            bindingSource.Filter = filterString
        Catch ex As Exception
            MessageBox.Show($"Error applying filter: {ex.Message}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Filter Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

    Private Sub ActiveControls()
        If TabControl1.SelectedIndex = 0 Then
            ActiveControl = TableDataGridView
        ElseIf TabControl1.SelectedIndex = 1 Then
            ActiveControl = TableDataGridView1
        ElseIf TabControl1.SelectedIndex = 2 Then
            ActiveControl = TableDataGridView2
        End If
    End Sub

    Private Sub frmDatabase_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            frmECoins.Close()
            frmENotes.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If frmLogin.Visible = True Or frmNotes.Visible = True Or frmCoins.Visible = True Or frmAboutMe.Visible = True Or frmBackupRestore.Visible = True Or frmSetting.Visible = True Or frmDashboard.Visible = True Or frmGallery.Visible = True Or frmInfoCenter.Visible = True Or frmChangeKey.Visible = True Then
            Me.Close()
        Else
            frmDashboard.Show()
            Me.Close()
        End If
    End Sub

    Dim Front As String
    Dim Back As String
    Private Sub ImageOpener()
        Try
            If Front = Nothing Then
                Frontal_ImagePictureBox.Image = My.Resources.None_Image
            Else
                Frontal_ImagePictureBox.Image = Image.FromFile(Application.StartupPath & "\" & folder & "\" & Front)
            End If

            If Back = Nothing Then
                Backward_ImagePictureBox.Image = My.Resources.None_Image
            Else
                Backward_ImagePictureBox.Image = Image.FromFile(Application.StartupPath & "\" & folder & "\" & Back)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Frontal_ImagePictureBox_DoubleClick(sender As Object, e As EventArgs) Handles Frontal_ImagePictureBox.DoubleClick
        Try
            System.Diagnostics.Process.Start(Application.StartupPath & "\" & folder & "\" & Front)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Backward_ImagePictureBox_DoubleClick(sender As Object, e As EventArgs) Handles Backward_ImagePictureBox.DoubleClick
        Try
            System.Diagnostics.Process.Start(Application.StartupPath & "\" & folder & "\" & Back)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Frontal_ImagePictureBox_Click(sender As Object, e As EventArgs) Handles Frontal_ImagePictureBox.Click
        Frontal_ImagePictureBox.Cursor = Cursors.Hand
    End Sub

    Private Sub Backward_ImagePictureBox_Click(sender As Object, e As EventArgs) Handles Backward_ImagePictureBox.Click
        Backward_ImagePictureBox.Cursor = Cursors.Hand
    End Sub

    Private Sub Backward_ImagePictureBox_Leave(sender As Object, e As EventArgs) Handles Backward_ImagePictureBox.Leave

    End Sub

    Private Sub Frontal_ImagePictureBox_Leave(sender As Object, e As EventArgs) Handles Frontal_ImagePictureBox.Leave

    End Sub

    Private Sub TableDataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView1.CellContentClick

    End Sub

    Private Sub Frontal_ImagePictureBox_MouseLeave(sender As Object, e As EventArgs) Handles Frontal_ImagePictureBox.MouseLeave
        Frontal_ImagePictureBox.Cursor = Cursors.Default
    End Sub

    Private Sub Backward_ImagePictureBox_MouseLeave(sender As Object, e As EventArgs) Handles Backward_ImagePictureBox.MouseLeave
        Backward_ImagePictureBox.Cursor = Cursors.Default
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        Me.cmbtxtSearch.Text = Nothing
        TableBindingSource2.Filter = Nothing

        TableDataGridView2.Columns(0).DefaultCellStyle.BackColor = Color.MediumTurquoise
        TableDataGridView2.Columns(1).DefaultCellStyle.BackColor = Color.MediumTurquoise
        TableDataGridView2.Columns(2).DefaultCellStyle.BackColor = Color.MediumTurquoise
        TableDataGridView2.Columns(3).DefaultCellStyle.BackColor = Color.MediumTurquoise
        TableDataGridView2.Columns(4).DefaultCellStyle.BackColor = Color.MediumTurquoise

        If cmbCategory.SelectedIndex >= 0 Then
            TableDataGridView2.Columns(cmbCategory.SelectedIndex + 1).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
        End If

        conn.Open()

        If cmbCategory.SelectedIndex = 0 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Country_Currency FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Country_Currency"))
            End While
        ElseIf cmbCategory.SelectedIndex = 1 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Full_Name FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Full_Name"))
            End While
        ElseIf cmbCategory.SelectedIndex = 2 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Local_Names FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Local_Names"))
            End While
        ElseIf cmbCategory.SelectedIndex = 3 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Abbriviated_Names FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Abbriviated_Names"))
            End While
        Else
            cmbtxtSearch.Items.Clear()
        End If

        conn.Close()
    End Sub

    Private Sub cmbCategory_DragDrop(sender As Object, e As DragEventArgs) Handles cmbCategory.DragDrop

    End Sub

    Private Sub filterPerfect()
        Dim cantfind As String = cmbtxtSearch.Text

        Try

            If cmbCategory.SelectedIndex = 0 Then

                TableBindingSource2.Filter = "Country_Currency Like'" & Me.cmbtxtSearch.Text & "'"
            ElseIf cmbCategory.SelectedIndex = 1 Then

                TableBindingSource2.Filter = "Full_Name Like'" & Me.cmbtxtSearch.Text & "'"
            ElseIf cmbCategory.SelectedIndex = 2 Then

                TableBindingSource2.Filter = "Local_Names Like'" & Me.cmbtxtSearch.Text & "'"

            ElseIf cmbCategory.SelectedIndex = 3 Then

                TableBindingSource2.Filter = "Abbriviated_Names Like'" & Me.cmbtxtSearch.Text & "'"

            Else

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub filter()
        Dim cantfind As String = cmbtxtSearch.Text

        Try

            If cmbCategory.SelectedIndex = 0 Then

                TableBindingSource2.Filter = "Country_Currency Like'%" & Me.cmbtxtSearch.Text & "%'"
            ElseIf cmbCategory.SelectedIndex = 1 Then

                TableBindingSource2.Filter = "Full_Name Like'%" & Me.cmbtxtSearch.Text & "%'"
            ElseIf cmbCategory.SelectedIndex = 2 Then

                TableBindingSource2.Filter = "Local_Names Like'%" & Me.cmbtxtSearch.Text & "%'"

            ElseIf cmbCategory.SelectedIndex = 3 Then

                TableBindingSource2.Filter = "Abbriviated_Names Like'%" & Me.cmbtxtSearch.Text & "%'"

            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbtxtSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.SelectedIndexChanged
        ApplyFilter(cmbCategory, cmbtxtSearch, TableBindingSource2, TableDataGridView2, True)
    End Sub

    ' Example usage in your existing handlers:
    Private Sub cmbtxtSearch_TextChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.TextChanged
        If cmbCategory.Text Is Nothing Then cmbCategory.SelectedIndex = 0 ' Ensure a category is selected
        ApplyFilter(cmbCategory, cmbtxtSearch, TableBindingSource2, TableDataGridView2, False)
    End Sub

    Private Sub butReset_Click(sender As Object, e As EventArgs) Handles butReset.Click
        Me.cmbCategory.Text = Nothing
        Me.cmbtxtSearch.Text = Nothing
        TableBindingSource2.Filter = Nothing

        TableDataGridView2.Columns(0).DefaultCellStyle.BackColor = Color.MediumTurquoise
        TableDataGridView2.Columns(1).DefaultCellStyle.BackColor = Color.MediumTurquoise
        TableDataGridView2.Columns(2).DefaultCellStyle.BackColor = Color.MediumTurquoise
    End Sub

    Private Sub cmbCategory_TextChanged(sender As Object, e As EventArgs) Handles cmbCategory.TextChanged
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        Me.ComboBox1.Text = Nothing
        TableBindingSource.Filter = Nothing

        TableDataGridView.Columns(0).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(1).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(2).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(3).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(4).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(5).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(6).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(7).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(8).DefaultCellStyle.BackColor = Color.Coral

        If ComboBox2.SelectedIndex >= 0 Then
            If ComboBox2.SelectedIndex < 6 Then
                TableDataGridView.Columns(ComboBox2.SelectedIndex + 1).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
            ElseIf ComboBox2.SelectedIndex = 6 Then
                TableDataGridView.Columns(8).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
            Else
            End If
        End If

        conn1.Open()

        If ComboBox2.SelectedIndex = 0 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Continent FROM [Table]", conn1)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox1.Items.Clear()
            While myreader.Read
                ComboBox1.Items.Add(myreader("Continent"))
            End While
        ElseIf ComboBox2.SelectedIndex = 1 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Country FROM [Table]", conn1)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox1.Items.Clear()
            While myreader.Read
                ComboBox1.Items.Add(myreader("Country"))
            End While
        ElseIf ComboBox2.SelectedIndex = 2 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Catalogue_Code FROM [Table]", conn1)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox1.Items.Clear()
            While myreader.Read
                ComboBox1.Items.Add(myreader("Catalogue_Code"))
            End While
        ElseIf ComboBox2.SelectedIndex = 3 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Currency_Name FROM [Table]", conn1)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox1.Items.Clear()
            While myreader.Read
                ComboBox1.Items.Add(myreader("Currency_Name"))
            End While
        ElseIf ComboBox2.SelectedIndex = 4 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Denomination FROM [Table]", conn1)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox1.Items.Clear()
            While myreader.Read
                ComboBox1.Items.Add(myreader("Denomination"))
            End While
        ElseIf ComboBox2.SelectedIndex = 5 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Date_Recorded_On FROM [Table]", conn1)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox1.Items.Clear()
            While myreader.Read
                ComboBox1.Items.Add(myreader("Date_Recorded_On"))
            End While

        ElseIf ComboBox2.SelectedIndex = 6 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Source FROM [Table]", conn1)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox1.Items.Clear()
            While myreader.Read
                ComboBox1.Items.Add(myreader("Source"))
            End While

        Else
            ComboBox1.Items.Clear()
        End If

        conn1.Close()
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        Dim cantfind As String = ComboBox1.Text
        If ComboBox2.Text = Nothing Then
            ComboBox2.SelectedIndex = 1
        End If
        Try

            If ComboBox2.SelectedIndex = 0 Then

                TableBindingSource.Filter = "Continent Like'%" & Me.ComboBox1.Text & "%'"

            ElseIf ComboBox2.SelectedIndex = 1 Then

                TableBindingSource.Filter = "Country Like'%" & Me.ComboBox1.Text & "%'"

            ElseIf ComboBox2.SelectedIndex = 2 Then

                TableBindingSource.Filter = "Catalogue_Code Like'%" & Me.ComboBox1.Text & "%'"

            ElseIf ComboBox2.SelectedIndex = 3 Then
                Dim text As String

                text = UCase(ComboBox1.Text)

                TableBindingSource.Filter = "Currency_Name Like'%" & text & "%'"

            ElseIf ComboBox2.SelectedIndex = 4 Then

                TableBindingSource.Filter = "Denomination Like'%" & Me.ComboBox1.Text & "%'"

            ElseIf ComboBox2.SelectedIndex = 5 Then

                TableBindingSource.Filter = "Date_Recorded_On Like'%" & Me.ComboBox1.Text & "%'"

            ElseIf ComboBox2.SelectedIndex = 6 Then

                TableBindingSource.Filter = "Source Like'%" & Me.ComboBox1.Text & "%'"

            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim cantfind As String = ComboBox1.Text

        Try

            If ComboBox2.SelectedIndex = 0 Then

                TableBindingSource.Filter = "Continent Like'" & Me.ComboBox1.Text & "'"

            ElseIf ComboBox2.SelectedIndex = 1 Then

                TableBindingSource.Filter = "Country Like'" & Me.ComboBox1.Text & "'"

            ElseIf ComboBox2.SelectedIndex = 2 Then

                TableBindingSource.Filter = "Catalogue_Code Like'" & Me.ComboBox1.Text & "'"

            ElseIf ComboBox2.SelectedIndex = 3 Then
                Dim text As String

                text = UCase(ComboBox1.Text)

                TableBindingSource.Filter = "Currency_Name Like'" & text & "'"

            ElseIf ComboBox2.SelectedIndex = 4 Then

                TableBindingSource.Filter = "Denomination Like'" & Me.ComboBox1.Text & "'"

            ElseIf ComboBox2.SelectedIndex = 5 Then

                TableBindingSource.Filter = "Date_Recorded_On Like'" & Me.ComboBox1.Text & "'"

            ElseIf ComboBox2.SelectedIndex = 6 Then

                TableBindingSource.Filter = "Source Like'" & Me.ComboBox1.Text & "'"

            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ComboBox2.Text = Nothing
        ComboBox1.Text = Nothing
        ComboBox1.Enabled = True
        ComboBox1.Items.Clear()
        TableBindingSource.Filter = Nothing

        TableDataGridView.Columns(0).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(1).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(2).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(3).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(4).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(5).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(6).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(7).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(8).DefaultCellStyle.BackColor = Color.Coral

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

        Me.ComboBox3.Text = Nothing
        TableBindingSource1.Filter = Nothing

        TableDataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(1).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(2).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(3).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(4).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(5).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(6).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(7).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(8).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(9).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(10).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(11).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(12).DefaultCellStyle.BackColor = Color.YellowGreen

        If ComboBox4.SelectedIndex >= 0 Then
            If ComboBox4.SelectedIndex < 8 Then
                TableDataGridView1.Columns(ComboBox4.SelectedIndex + 1).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
            ElseIf ComboBox4.SelectedIndex = 8 Then
                TableDataGridView1.Columns(10).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
            Else
            End If
        End If

        conn2.Open()

        If ComboBox4.SelectedIndex = 0 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Continent FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Continent"))
            End While
        ElseIf ComboBox4.SelectedIndex = 1 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Country FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Country"))
            End While
        ElseIf ComboBox4.SelectedIndex = 2 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Catalogue_Code FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Catalogue_Code"))
            End While
        ElseIf ComboBox4.SelectedIndex = 3 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Currency_Name FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Currency_Name"))
            End While
        ElseIf ComboBox4.SelectedIndex = 4 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Denomination FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Denomination"))
            End While
        ElseIf ComboBox4.SelectedIndex = 5 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Date_Recorded_On FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Date_Recorded_On"))
            End While
        ElseIf ComboBox4.SelectedIndex = 6 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Serial_No FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Serial_No"))
            End While
        ElseIf ComboBox4.SelectedIndex = 7 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Condition FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Condition"))
            End While
        ElseIf ComboBox4.SelectedIndex = 8 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Source FROM [Table]", conn2)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            ComboBox3.Items.Clear()
            While myreader.Read
                ComboBox3.Items.Add(myreader("Source"))
            End While
        Else
            ComboBox3.Items.Clear()
        End If

        conn2.Close()
    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim cantfind As String = ComboBox3.Text

        Try

            If ComboBox4.SelectedIndex = 0 Then

                TableBindingSource1.Filter = "Continent Like'" & Me.ComboBox3.Text & "'"

            ElseIf ComboBox4.SelectedIndex = 1 Then

                TableBindingSource1.Filter = "Country Like'" & Me.ComboBox3.Text & "'"

            ElseIf ComboBox4.SelectedIndex = 2 Then

                TableBindingSource1.Filter = "Catalogue_Code Like'" & Me.ComboBox3.Text & "'"

            ElseIf ComboBox4.SelectedIndex = 3 Then
                Dim text As String

                text = UCase(ComboBox3.Text)

                TableBindingSource1.Filter = "Currency_Name Like'" & text & "'"

            ElseIf ComboBox4.SelectedIndex = 4 Then

                TableBindingSource1.Filter = "Denomination Like'" & Me.ComboBox3.Text & "'"

            ElseIf ComboBox4.SelectedIndex = 5 Then

                TableBindingSource1.Filter = "Date_Recorded_On Like'" & Me.ComboBox3.Text & "'"

            ElseIf ComboBox4.SelectedIndex = 6 Then

                TableBindingSource1.Filter = "Serial_No Like'" & Me.ComboBox3.Text & "'"

            ElseIf ComboBox4.SelectedIndex = 7 Then

                TableBindingSource1.Filter = "Condition Like'" & Me.ComboBox3.Text & "'"


            ElseIf ComboBox4.SelectedIndex = 8 Then

                TableBindingSource1.Filter = "Source Like'" & Me.ComboBox3.Text & "'"

            Else
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ComboBox3_TextChanged(sender As Object, e As EventArgs) Handles ComboBox3.TextChanged
        Dim cantfind As String = ComboBox3.Text

        If ComboBox4.Text = Nothing Then
            ComboBox4.SelectedIndex = 1
        End If

        Try

            If ComboBox4.SelectedIndex = 0 Then

                TableBindingSource1.Filter = "Continent Like'%" & Me.ComboBox3.Text & "%'"

            ElseIf ComboBox4.SelectedIndex = 1 Then

                TableBindingSource1.Filter = "Country Like'%" & Me.ComboBox3.Text & "%'"

            ElseIf ComboBox4.SelectedIndex = 2 Then

                TableBindingSource1.Filter = "Catalogue_Code Like'%" & Me.ComboBox3.Text & "%'"

            ElseIf ComboBox4.SelectedIndex = 3 Then
                Dim text As String

                text = UCase(ComboBox3.Text)

                TableBindingSource1.Filter = "Currency_Name Like'%" & text & "%'"

            ElseIf ComboBox4.SelectedIndex = 4 Then

                TableBindingSource1.Filter = "Denomination Like'%" & Me.ComboBox3.Text & "%'"

            ElseIf ComboBox4.SelectedIndex = 5 Then

                TableBindingSource1.Filter = "Date_Recorded_On Like'%" & Me.ComboBox3.Text & "%'"

            ElseIf ComboBox4.SelectedIndex = 6 Then

                TableBindingSource1.Filter = "Serial_No Like'%" & Me.ComboBox3.Text & "%'"

            ElseIf ComboBox4.SelectedIndex = 7 Then

                TableBindingSource1.Filter = "Condition Like'%" & Me.ComboBox3.Text & "%'"


            ElseIf ComboBox4.SelectedIndex = 8 Then

                TableBindingSource1.Filter = "Source Like'%" & Me.ComboBox3.Text & "%'"

            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ComboBox4.Text = Nothing
        ComboBox3.Text = Nothing
        ComboBox3.Enabled = True
        ComboBox3.Items.Clear()
        TableBindingSource1.Filter = Nothing

        TableDataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(1).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(2).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(3).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(4).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(5).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(6).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(7).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(8).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(9).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(10).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(11).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView1.Columns(12).DefaultCellStyle.BackColor = Color.YellowGreen

    End Sub

    Dim screen As Screen
    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPresentorDecision.SelectedIndexChanged

    End Sub

    Public Sub SecondaryScreen()
        Try
            If cmbPresentorDecision.SelectedIndex = 0 Then
                screen = Screen.AllScreens(1)

                If cmbPresentorOption.SelectedIndex = 0 Then 'coins
                    frmECoins.Location = screen.Bounds.Location + New Point(100, 100)
                    frmECoins.Show()
                ElseIf cmbPresentorOption.SelectedIndex = 1 Then 'notes
                    frmENotes.Location = screen.Bounds.Location + New Point(100, 100)
                    frmENotes.Show()
                End If

            Else
                frmECoins.Close()
                frmENotes.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SecondaryScreenTabIndex0()
        Try
            If cmbPresentorDecision.SelectedIndex = 0 Then
                screen = Screen.AllScreens(1)

                frmENotes.Close()
                frmECoins.Location = screen.Bounds.Location + New Point(100, 100)
                frmECoins.Show()

            Else
                frmECoins.Close()
                frmENotes.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SecondaryScreenTabIndex1()
        Try
            If cmbPresentorDecision.SelectedIndex = 0 Then
                screen = Screen.AllScreens(1)

                frmECoins.Close()
                frmENotes.Location = screen.Bounds.Location + New Point(100, 100)
                frmENotes.Show()

            Else
                frmECoins.Close()
                frmENotes.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If cmbPresentorDecision.SelectedIndex = 1 Then
            SecondaryScreen()
        ElseIf cmbPresentorDecision.SelectedIndex = 0 Then
            Try
                screen = Screen.AllScreens(1)

                Frontal_ImagePictureBox.Image = Nothing
                Backward_ImagePictureBox.Image = Nothing

                If cmbPresentorOption.SelectedIndex = 0 Then 'coins
                    frmECoins.Location = screen.Bounds.Location + New Point(100, 100)
                    frmECoins.Show()
                    MsgBox("The data presentation has started please select the desired data from any database to present", vbOKOnly + vbInformation)

                ElseIf cmbPresentorOption.SelectedIndex = 1 Then 'notes
                    frmENotes.Location = screen.Bounds.Location + New Point(100, 100)
                    frmENotes.Show()
                    MsgBox("The data presentation has started please select the desired data from any database to present", vbOKOnly + vbInformation)

                Else

                    MsgBox("Please select any item from " & Label7.Text)
                End If

            Catch ex As Exception
                cmbPresentorDecision.SelectedIndex = 1
                cmbPresentorOption.Text = Nothing
                MsgBox("No external screen connected", vbExclamation)
            End Try

        ElseIf cmbPresentorDecision.SelectedIndex = Nothing Then
            MsgBox("Please select your decision to display the presentor view in secondary screen", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub TableDataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView1.CellClick
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        ActiveControls()

        If TabControl1.SelectedIndex = 0 Then
            SecondaryScreenTabIndex0()
        ElseIf TabControl1.SelectedIndex = 1 Then
            SecondaryScreenTabIndex1()
        End If

    End Sub

    Private Sub TableDataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView.CellClick

    End Sub

    Private Sub TableDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView.CellContentClick

    End Sub

    Private Sub TableDataGridView_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView.CellEnter
        If cmbPresentorDecision.SelectedIndex = 1 Then

        ElseIf cmbPresentorDecision.SelectedIndex = 0 Then
            If e.ColumnIndex >= 0 And e.ColumnIndex <= 12 Then
                Dim row As DataGridViewRow = TableDataGridView.Rows(e.RowIndex)

                frmECoins.Label2.Text = row.Cells(1).Value.ToString
                frmECoins.Label3.Text = row.Cells(2).Value.ToString
                frmECoins.Label4.Text = row.Cells(3).Value.ToString
                frmECoins.Label5.Text = row.Cells(4).Value.ToString
                frmECoins.Label6.Text = row.Cells(5).Value.ToString
                frmECoins.Label7.Text = row.Cells(6).Value.ToString
                frmECoins.Label8.Text = row.Cells(7).Value.ToString
                frmECoins.Label9.Text = row.Cells(8).Value.ToString

            End If
        End If
    End Sub


    Private Sub TableDataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView1.CellEnter
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return ' Exit if invalid cell

        Dim row As DataGridViewRow = TableDataGridView1.Rows(e.RowIndex)



        ' Always clear images first
        Frontal_ImagePictureBox.Image = Nothing
        Backward_ImagePictureBox.Image = Nothing
        frmENotes.Frontal_Image.Image = Nothing
        frmENotes.Backward_Image.Image = Nothing

        Try
            ' Populate labels on frmENotes (using column names is better than indices if possible)
            ' Assuming your DataGridView columns map to these properties
            frmENotes.Label2.Text = row.Cells("Continent").Value?.ToString()
            frmENotes.Label3.Text = row.Cells("Country").Value?.ToString()
            frmENotes.Label4.Text = row.Cells("Catalogue_Code").Value?.ToString()
            frmENotes.Label5.Text = row.Cells("Currency_Name").Value?.ToString()
            frmENotes.Label6.Text = row.Cells("Denomination").Value?.ToString()
            frmENotes.Label7.Text = row.Cells("Date_Recorded_On").Value?.ToString()
            frmENotes.Label8.Text = row.Cells("Serial_No").Value?.ToString()
            frmENotes.Label9.Text = row.Cells("Condition").Value?.ToString()
            frmENotes.Label10.Text = row.Cells("Source").Value?.ToString()
            ' Assuming Label11 is for CommentRichTextBox
            frmENotes.Label11.Text = row.Cells("Comment").Value?.ToString()


            ' Image handling for frmDatabase's own PictureBoxes (if any, not clear from snippet)
            Front = row.Cells(11).Value?.ToString() ' Assuming column 11 is Frontal_ImageFileName
            Back = row.Cells(12).Value?.ToString()  ' Assuming column 12 is Backward_ImageFileName
            TextBox1.Text = Front
            TextBox2.Text = Back
            ImageOpener() ' Call your existing image opener for this form

            ' Image handling for frmENotes
            Dim frontalImageFileName As String = row.Cells(11).Value?.ToString()
            If Not String.IsNullOrWhiteSpace(frontalImageFileName) Then
                Dim fullPath As String = Path.Combine(FullImagePath, frontalImageFileName)
                If File.Exists(fullPath) Then
                    frmENotes.Frontal_Image.Image = Image.FromFile(fullPath)
                Else
                    frmENotes.Frontal_Image.Image = My.Resources.None_Image
                    Console.WriteLine($"Warning: Frontal image file not found for Notes: {fullPath}")
                End If
            Else
                frmENotes.Frontal_Image.Image = My.Resources.None_Image
            End If

            Dim backwardImageFileName As String = row.Cells(12).Value?.ToString()
            If Not String.IsNullOrWhiteSpace(backwardImageFileName) Then
                Dim fullPath As String = Path.Combine(FullImagePath, backwardImageFileName)
                If File.Exists(fullPath) Then
                    frmENotes.Backward_Image.Image = Image.FromFile(fullPath)
                Else
                    frmENotes.Backward_Image.Image = My.Resources.None_Image
                    Console.WriteLine($"Warning: Backward image file not found for Notes: {fullPath}")
                End If
            Else
                frmENotes.Backward_Image.Image = My.Resources.None_Image
            End If

            ' Logic for secondary screen presentation (as per your original code)
            If cmbPresentorDecision.SelectedIndex = 0 Then ' If presenter mode is active
                If Not frmENotes.Visible Then frmENotes.Show() ' Show if not already visible
            End If

        Catch ex As Exception
            MessageBox.Show($"Error loading Notes data for display: {ex.Message}", "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Notes Display Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            frmENotes.Frontal_Image.Image = My.Resources.None_Image
            frmENotes.Backward_Image.Image = My.Resources.None_Image
        End Try
    End Sub

End Class