Imports System.IO
Imports System.Data.OleDb

Public Class frmNotes
   
    Dim folder As String = ("Resources\Images")
    Dim conn As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Notes.accdb")
    Dim access As New Control




    Private Const ResourcesFolder As String = "Resources"
    Private Const ImagesFolder As String = "Images"
    Private ReadOnly FullImagePath As String = Path.Combine(Application.StartupPath, ResourcesFolder, ImagesFolder)


    Private ReadOnly ConnectionString As String = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path.Combine(Application.StartupPath, "Notes.accdb")}"


    Private Sub frmNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.NotesDataSet = New NotesDataSet()
        Me.TableTableAdapter = New NotesDataSetTableAdapters.TableTableAdapter()
        Me.TableBindingSource = New BindingSource()
        Me.TableBindingSource.DataSource = Me.NotesDataSet
        Me.TableBindingSource.DataMember = "Table"
        Me.TableTableAdapter.Fill(Me.NotesDataSet.Table)
        Me.TableDataGridView.DataSource = Me.TableBindingSource


        Try
            Me.TableTableAdapter1.Fill(Me.Currency_Short_NamesDataSet.Table)
        Catch ex As OleDbException
            MessageBox.Show($"Database error loading currency names: {ex.Message}{Environment.NewLine}Please ensure 'Currency_Short_NamesDataSet' is correctly configured.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"OleDbException (Currency Names Load): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred loading currency names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (Currency Names Load): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try

        CountryComboBox.Text = Nothing
        CheckEnteries()

        ActiveControl = ContinentComboBox


        Try
            RefreshDatabase()
        Catch ex As Exception

            MessageBox.Show("Unable to load the main database during startup. The program may not function correctly.", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"Startup Database Load Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")

            frmBackupRestore.Show()
            frmBackupRestore.TabControl.SelectTab(1)
            Me.Hide()
        End Try


        If Not Directory.Exists(FullImagePath) Then
            Directory.CreateDirectory(FullImagePath)
        End If
        If Directory.Exists(Path.Combine(Application.StartupPath, ResourcesFolder)) Then
            File.SetAttributes(Path.Combine(Application.StartupPath, ResourcesFolder), FileAttributes.Hidden)
        End If
        If Directory.Exists(FullImagePath) Then
            File.SetAttributes(FullImagePath, FileAttributes.Hidden)
        End If

        CountryComboBox.Text = Nothing
        Currency_NameComboBox.Text = Nothing
    End Sub




    ' --- Data Retrieval (Refresh) ---
    Public Sub RefreshDatabase()

        Frontal_ImagePictureBox.Image = Nothing
        Backward_ImagePictureBox.Image = Nothing
        Frontal_ImageTextBox.Text = Nothing ' Also clear textboxes
        Backward_ImageTextBox.Text = Nothing

        Try

            Me.TableTableAdapter.Fill(Me.NotesDataSet.Table)

        Catch ex As OleDbException
            MessageBox.Show($"Database error refreshing data: {ex.Message}{Environment.NewLine}Please check your database connection and table schema.", "Database Refresh Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"OleDbException (RefreshDatabase): {ex.Message}{Environment.NewLine}{ex.StackTrace}")

            frmBackupRestore.Show()
            frmBackupRestore.TabControl.SelectTab(1)
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred during data refresh: {ex.Message}", "Refresh Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (RefreshDatabase): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

    ' --- Data Saving/Updating ---
    Private Sub TableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles TableBindingNavigatorSaveItem.Click

        If String.IsNullOrWhiteSpace(SourceTextBox.Text) Then
            SourceTextBox.Text = "(none)"
        End If

        Try
            ' 1. Validate data in bound controls (e.g., DataGridView, TextBoxes)
            Me.Validate()

            ' 2. End any pending edits on the BindingSource.
            ' This pushes changes from bound controls into the underlying DataTable.
            Me.TableBindingSource.EndEdit()

            ' 3. Update the database.
            ' TableAdapterManager.UpdateAll is typically used for saving changes across multiple
            ' related tables in a DataSet. If you only have one table or want to be explicit,
            ' TableTableAdapter.Update(NotesDataSet.Table) is sufficient for the main table.
            ' Both are present in your original code, so keeping both for consistency,
            ' but often one is enough depending on your data structure.
            Me.TableAdapterManager.UpdateAll(Me.NotesDataSet)
            Me.TableTableAdapter.Update(NotesDataSet.Table)

            MessageBox.Show("Record saved successfully!", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As OleDbException
            ' Specific handling for database errors (e.g., constraint violations, invalid data types)
            MessageBox.Show($"Database error saving record: {ex.Message}{Environment.NewLine}Please check your data for validity.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"OleDbException (Save): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            ' You might want to reject changes in the DataTable if the save fails
            ' NotesDataSet.RejectChanges()
        Catch ex As Exception
            ' General handling for other unexpected errors during save
            MessageBox.Show($"An unexpected error occurred while saving the record: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (Save): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        Finally
            ' Always refresh data after a save attempt to show the latest state,
            ' including auto-generated IDs for new records.
            RefreshDatabase()
            ActiveControl = ContinentComboBox ' Set focus
        End Try
    End Sub
    ' --- Data Adding ---
    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click
        ActiveControl = ContinentComboBox
        If String.IsNullOrWhiteSpace(SourceTextBox.Text) Then
            SourceTextBox.Text = "(none)"
        End If
        Frontal_ImagePictureBox.Image = Nothing
        Backward_ImagePictureBox.Image = Nothing
    End Sub
    ' --- Data Deletion ---
    Private Sub BindingNavigatorDeleteItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorDeleteItem.Click
        ' Ensure a record is selected and has a valid ID before attempting deletion.
        If String.IsNullOrWhiteSpace(IDTextBox.Text) OrElse Not IsNumeric(IDTextBox.Text) OrElse Val(IDTextBox.Text) <= 0 Then
            MessageBox.Show("Please select a valid, saved record to delete.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete this record permanently?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim recordID As Integer = CInt(IDTextBox.Text)

                ' Option 1: Delete directly via TableAdapter (recommended with BindingSource)
                ' The BindingSource's DeleteItem or RemoveCurrent method would mark the row for deletion,
                ' and then TableAdapter.Update would commit it.
                Me.TableBindingSource.RemoveCurrent() ' Marks the current row in BindingSource/DataTable for deletion
                Me.TableTableAdapter.Update(NotesDataSet.Table) ' Commits the deletion to the database

                ' Option 2: Direct SQL DELETE (less common with TableAdapters, but shown as in your original code)
                ' If you prefer direct SQL, ensure connection is opened/closed properly.
                ' Using conn As New OleDbConnection(ConnectionString)
                '     Dim cmd As New OleDbCommand($"DELETE FROM [Table] WHERE ID={recordID};", conn)
                '     conn.Open()
                '     cmd.ExecuteNonQuery()
                ' End Using

                ' After successful deletion, accept changes in the DataSet
                NotesDataSet.AcceptChanges()

                MessageBox.Show("The record has been deleted successfully.", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As OleDbException
                MessageBox.Show($"Database error deleting record: {ex.Message}{Environment.NewLine}This might be due to related records in other tables (referential integrity).", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Console.WriteLine($"OleDbException (Delete): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
                ' If deletion fails, reject changes to revert the DataTable state
                NotesDataSet.RejectChanges()
            Catch ex As Exception
                MessageBox.Show($"An unexpected error occurred while deleting the record: {ex.Message}", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Console.WriteLine($"Exception (Delete): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            Finally
                RefreshDatabase() ' Always refresh data after a delete attempt
                ActiveControl = ContinentComboBox
            End Try
        End If
    End Sub

    ' --- Data Filtering/Retrieval for Search ---
    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        ' Handles UI logic for search category selection (e.g., showing/hiding DateTimePicker)
        If cmbCategory.SelectedIndex = 5 Then ' Assuming index 5 is for Date_Recorded_On
            cmbtxtSearch.Enabled = False
            dtpSearch.Visible = True
        Else
            cmbtxtSearch.Enabled = True
            dtpSearch.Visible = False
        End If

        Me.cmbtxtSearch.Text = Nothing
        TableBindingSource.Filter = Nothing ' Clear previous filter

        ' Reset DataGridView column background colors
        For i As Integer = 0 To TableDataGridView.Columns.Count - 1
            TableDataGridView.Columns(i).DefaultCellStyle.BackColor = Color.YellowGreen
        Next

        ' Highlight selected column
        If cmbCategory.SelectedIndex >= 0 Then
            If cmbCategory.SelectedIndex < TableDataGridView.Columns.Count Then
                ' Direct column index for most cases
                TableDataGridView.Columns(cmbCategory.SelectedIndex).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
            ElseIf cmbCategory.SelectedIndex = 8 Then ' Special case from your original code (Source maps to column 9)
                If TableDataGridView.Columns.Count > 9 Then
                    TableDataGridView.Columns(9).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
                End If
            End If
        End If

        ' Populate cmbtxtSearch with distinct values based on selected category
        cmbtxtSearch.Items.Clear()
        Dim sqlQuery As String = ""
        Select Case cmbCategory.SelectedIndex
            Case 0 : sqlQuery = "SELECT DISTINCT Continent FROM [Table]"
            Case 1 : sqlQuery = "SELECT DISTINCT Country FROM [Table]"
            Case 2 : sqlQuery = "SELECT DISTINCT Catalogue_Code FROM [Table]"
            Case 3 : sqlQuery = "SELECT DISTINCT Currency_Name FROM [Table]"
            Case 4 : sqlQuery = "SELECT DISTINCT Denomination FROM [Table]"
            Case 5 : sqlQuery = "SELECT DISTINCT Date_Recorded_On FROM [Table]"
            Case 6 : sqlQuery = "SELECT DISTINCT Serial_No FROM [Table]"
            Case 7 : sqlQuery = "SELECT DISTINCT Condition FROM [Table]"
            Case 8 : sqlQuery = "SELECT DISTINCT Source FROM [Table]"
            Case Else : Return ' No category selected or invalid index
        End Select

        Try
            Using conn As New OleDbConnection(ConnectionString)
                Using cmd As New OleDbCommand(sqlQuery, conn)
                    conn.Open()
                    Using myreader As OleDbDataReader = cmd.ExecuteReader
                        While myreader.Read
                            cmbtxtSearch.Items.Add(myreader(0).ToString()) ' Add the first column's value
                        End While
                    End Using
                End Using
            End Using
        Catch ex As OleDbException
            MessageBox.Show($"Database error fetching distinct values for search: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"OleDbException (Distinct Values): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred fetching distinct values: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (Distinct Values): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub



    Private Sub cmbtxtSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.SelectedIndexChanged
        filterPerfect() ' Apply perfect match filter when an item is selected from dropdown
    End Sub

    Private Sub cmbtxtSearch_TextChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.TextChanged
        filter() ' Apply filter as text changes (partial match)
    End Sub


    Private Sub filter()
        Dim filterColumn As String = ""
        Dim filterValue As String = cmbtxtSearch.Text.Replace("'", "''") ' Escape single quotes for SQL

        Select Case cmbCategory.SelectedIndex
            Case 0 : filterColumn = "Continent"
            Case 1 : filterColumn = "Country"
            Case 2 : filterColumn = "Catalogue_Code"
            Case 3 : filterColumn = "Currency_Name"
                filterValue = filterValue.ToUpper() ' Convert to uppercase for currency name
            Case 4 : filterColumn = "Denomination"
            Case 5 : filterColumn = "Date_Recorded_On"
            Case 6 : filterColumn = "Serial_No"
            Case 7 : filterColumn = "Condition"
            Case 8 : filterColumn = "Source"
            Case Else : TableBindingSource.Filter = Nothing : Return
        End Select

        Try
            ' Apply a "Like" filter for partial matches
            ' Use # for date fields in Access SQL if Date_Recorded_On is Date/Time type
            If filterColumn = "Date_Recorded_On" Then
                ' Assuming date format is dd/MM/yyyy in the database
                TableBindingSource.Filter = $"FORMAT({filterColumn}, 'dd/mm/yyyy') LIKE '%{filterValue}%'"
            Else
                TableBindingSource.Filter = $"{filterColumn} LIKE '%{filterValue}%'"
            End If
        Catch ex As Exception
            MessageBox.Show($"Error applying filter: {ex.Message}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Filter Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

    Private Sub filterPerfect()
        Dim filterColumn As String = ""
        Dim filterValue As String = cmbtxtSearch.Text.Replace("'", "''") ' Escape single quotes for SQL

        Select Case cmbCategory.SelectedIndex
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
            Case Else : TableBindingSource.Filter = Nothing : Return
        End Select

        Try
            ' Apply an exact match filter
            If filterColumn = "Date_Recorded_On" Then
                ' For exact date match, ensure format consistency
                TableBindingSource.Filter = $"FORMAT({filterColumn}, 'dd/mm/yyyy') = '{filterValue}'"
            Else
                TableBindingSource.Filter = $"{filterColumn} = '{filterValue}'"
            End If
        Catch ex As Exception
            MessageBox.Show($"Error applying perfect filter: {ex.Message}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Perfect Filter Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

    ' --- Other Helper Methods (kept for context, but not directly database CRUD) ---
    Private Sub CheckEnteries()
        ' Enable/disable save button based on required fields
        If String.IsNullOrWhiteSpace(ContinentComboBox.Text) OrElse
           String.IsNullOrWhiteSpace(CountryComboBox.Text) OrElse
           String.IsNullOrWhiteSpace(Currency_NameComboBox.Text) OrElse
           String.IsNullOrWhiteSpace(DenominationComboBox.Text) OrElse
           String.IsNullOrWhiteSpace(Date_Recorded_OnTextBox.Text) Then
            TableBindingNavigatorSaveItem.Enabled = False
        Else
            TableBindingNavigatorSaveItem.Enabled = True
        End If
    End Sub

    ' --- Currency Denomination Auto-Population (kept as it's part of data entry flow) ---
    Private Sub Currency_NameComboBox_TextChanged(sender As Object, e As EventArgs) Handles Currency_NameComboBox.TextChanged
        CheckEnteries() ' Re-check entries when currency name changes

        DenominationComboBox.Text = Nothing
        DenominationComboBox.Items.Clear()

        Select Case Currency_NameComboBox.Text.ToUpper() ' Use ToUpper for case-insensitive comparison
            Case "AUD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "GBP" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50"})
            Case "EUR" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "200", "500"})
            Case "JPY" : DenominationComboBox.Items.AddRange({"1,000", "2,000", "5,000", "10,000"})
            Case "CHF" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "1,000"})
            Case "USD" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "1,000", "5,000", "10,000", "100,000"})
            Case "AFN" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "500", "1,000"})
            Case "ALL" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "500", "1,000", "2,000", "5,000"})
            Case "DZD" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "200", "500", "1,000"})
            Case "AOA" : DenominationComboBox.Items.AddRange({"10", "50", "100", "200", "500", "1,000", "2,000"})
            Case "ARS" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "500", "1,000"})
            Case "AMD" : DenominationComboBox.Items.AddRange({"10", "25", "50", "100", "200", "500", "1,000", "2,000", "5,000", "20,000", "50,000"})
            Case "AWG" : DenominationComboBox.Items.AddRange({"10", "25", "50", "100", "200"})
            Case "ATS" : DenominationComboBox.Items.AddRange({"20", "50", "100", "500", "1,000", "5,000"})
            Case "BEF" : DenominationComboBox.Items.AddRange({"100", "200", "500", "1,000", "2,000"})
            Case "AZN" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100"})
            Case "BSD" : DenominationComboBox.Items.AddRange({"1/2", "1", "3", "5", "10", "20", "50", "100"})
            Case "BHD" : DenominationComboBox.Items.AddRange({"10", "25", "50", "100", "500"})
            Case "BDT" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "500", "1,000"})
            Case "BBD" : DenominationComboBox.Items.AddRange({"2", "5", "10", "20", "50", "100"})
            Case "BYR" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "200", "500"})
            Case "BZD" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "25", "50", "100"})
            Case "BMD" : DenominationComboBox.Items.AddRange({"2", "5", "10", "20", "50", "100"})
            Case "BTN" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100", "500"})
            Case "BOB" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "200"})
            Case "BAM" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200"})
            Case "BWP" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200"})
            Case "BRL" : DenominationComboBox.Items.AddRange({"2", "5", "10", "20", "50", "100"})
            Case "BND" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "25", "50", "100", "500", "1,000", "10,000"})
            Case "BGN" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50"})
            Case "BIF" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "500", "1,000", "5,000"})
            Case "XOF" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "5,000", "10,000"})
            Case "XAF" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "5,000", "10,000"})
            Case "XPF" : DenominationComboBox.Items.AddRange({"500", "1,000", "5,000", "10,000"})
            Case "KHR" : DenominationComboBox.Items.AddRange({"50", "100", "500", "1,000", "2,000", "5,000", "10,000", "20,000", "50,000", "100,000"})
            Case "CAD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "CVE" : DenominationComboBox.Items.AddRange({"200", "500", "1,000", "2,000", "2,500", "5,000"})
            Case "KYD" : DenominationComboBox.Items.AddRange({"1", "5", "10", "25", "40", "50", "100"})
            Case "CLP" : DenominationComboBox.Items.AddRange({"1", "5", "10", "50", "100", "500", "1,000", "2,000", "5,000", "10,000", "20,000"})
            Case "CNY" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100"})
            Case "COP" : DenominationComboBox.Items.AddRange({"1,000", "2,000", "5,000", "10,000", "20,000", "50,000", "100,000"})
            Case "KMF" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "2,500", "5,000", "10,000"})
            Case "CDF" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "200", "500", "1,000", "2,000", "5,000", "10,000", "20,000"})
            Case "CRC" : DenominationComboBox.Items.AddRange({"1,000", "2,000", "5,000", "10,000", "20,000", "50,000"})
            Case "HRK" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "200", "500", "1,000"})
            Case "CUC" : DenominationComboBox.Items.AddRange({"1", "3", "5", "10", "20", "50", "100"})
            Case "CUP" : DenominationComboBox.Items.AddRange({"1", "3", "5", "10", "20", "50", "100", "200", "500", "1,000"})
            Case "CYP" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20"})
            Case "CZK" : DenominationComboBox.Items.AddRange({"100", "200", "500", "2,000", "5,000"})
            Case "DKK" : DenominationComboBox.Items.AddRange({"50", "100", "200", "500", "1,000"})
            Case "DJF" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "500", "1,000", "2,000", "5,000", "10,000"})
            Case "DOP" : DenominationComboBox.Items.AddRange({"20", "50", "100", "200", "500", "1,000", "2,000"})
            Case "XCD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "EGP" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100", "200"})
            Case "SVC" : DenominationComboBox.Items.AddRange({"1", "5", "10", "25", "100"})
            Case "EEK" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "25", "100", "500"})
            Case "ETB" : DenominationComboBox.Items.AddRange({"1", "5", "10", "50", "100"})
            Case "FKP" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50"})
            Case "FIM" : DenominationComboBox.Items.AddRange({"1", "5", "100", "500", "1,000", "5,000"})
            Case "FJD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "GMD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "25", "50", "100", "200"})
            Case "GEL" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "DMK" : DenominationComboBox.Items.AddRange({"1/2", "1", "2", "5", "10", "20", "50", "100"})
            Case "GHS" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "200"})
            Case "GIP" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "GRD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "200", "5,000"})
            Case "GTQ" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100", "200"})
            Case "GNF" : DenominationComboBox.Items.AddRange({"50", "100", "500", "1,000", "5,000", "10,000"})
            Case "GYD" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "25", "50", "100", "500", "1,000"})
            Case "HTG" : DenominationComboBox.Items.AddRange({"10", "20", "25", "50", "100", "250", "500", "1,000"})
            Case "HNL" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "500"})
            Case "HKD" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "500", "1,000"})
            Case "HUF" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "5,000", "10,000", "20,000"})
            Case "ISK" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "5,000", "10,000"})
            Case "INR" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "200", "500", "1,000", "2,000"})
            Case "IDR" : DenominationComboBox.Items.AddRange({"2,000", "5,000", "10,000", "20,000", "50,000", "100,000"})
            Case "IRR" : DenominationComboBox.Items.AddRange({"100", "200", "500", "1,000", "2,000", "100,000"})
            Case "IQD" : DenominationComboBox.Items.AddRange({"50", "250", "1,000", "5,000", "10,000", "25,000"})
            Case "IED" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "ILS" : DenominationComboBox.Items.AddRange({"20", "50", "100", "200"})
            Case "ITL" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "200", "500", "1,000"})
            Case "JMD" : DenominationComboBox.Items.AddRange({"50", "100", "200", "500", "1,000", "5,000"})
            Case "JOD" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50"})
            Case "KZT" : DenominationComboBox.Items.AddRange({"200", "500", "1,000", "2,000", "5,000", "10,000", "20,000"})
            Case "KES" : DenominationComboBox.Items.AddRange({"50", "100", "200", "500", "1,000"})
            Case "KWD" : DenominationComboBox.Items.AddRange({"1/4", "1/2", "1", "5", "10", "20"})
            Case "KGS" : DenominationComboBox.Items.AddRange({"20", "50", "100", "200", "500", "1,000", "5,000"})
            Case "LAK" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "5,000", "10,000", "20,000", "50,000"})
            Case "LVL" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "500"})
            Case "LBP" : DenominationComboBox.Items.AddRange({"1", "5", "10", "25", "50", "100", "200", "500", "1,000", "5,000", "10,000", "20,000", "50,000", "100,000"})
            Case "LSL" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200"})
            Case "LRD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "LYD" : DenominationComboBox.Items.AddRange({"1/4", "1/2", "1", "5", "10", "20"})
            Case "LTL" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "500"})
            Case "LUF" : DenominationComboBox.Items.AddRange({"1", "2", "5", "25", "125"})
            Case "MOP" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "500", "1,000"})
            Case "MKD" : DenominationComboBox.Items.AddRange({"10", "50", "100", "200", "500", "1,000"})
            Case "MGA" : DenominationComboBox.Items.AddRange({"100", "200", "500", "1,000", "2,000", "5,000", "10,000", "20,000"})
            Case "MWK" : DenominationComboBox.Items.AddRange({"20", "50", "100", "200", "500", "1,000", "2,000"})
            Case "MYR" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100"})
            Case "MVR" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "25", "50", "100", "200", "500"})
            Case "MTL" : DenominationComboBox.Items.AddRange({"2", "5", "10", "20"})
            Case "MRO" : DenominationComboBox.Items.AddRange({"50", "100", "200", "500", "1,000"})
            Case "MUR" : DenominationComboBox.Items.AddRange({"0.05", "0.2", "0.5", "1", "5", "10", "20", "25", "50", "100", "200", "500", "1,000", "2,000"})
            Case "MXN" : DenominationComboBox.Items.AddRange({"20", "50", "100", "200", "500"})
            Case "MDL" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100", "200", "500", "1,000"})
            Case "MNT" : DenominationComboBox.Items.AddRange({"1", "3", "5", "10", "20", "50", "100", "500", "1,000", "5,000", "10,000"})
            Case "MAD" : DenominationComboBox.Items.AddRange({"20", "25", "50", "100", "200"})
            Case "MZN" : DenominationComboBox.Items.AddRange({"50", "100", "500", "1,000"})
            Case "MMK" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "200", "500", "5,000", "10,000"})
            Case "ANG" : DenominationComboBox.Items.AddRange({"1", "2 1/2", "5", "10", "25", "50", "100", "250", "500"})
            Case "NAD" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200"})
            Case "NPR" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "500", "1,000"})
            Case "NLG" : DenominationComboBox.Items.AddRange({"5", "10", "25", "50", "100"})
            Case "NZD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100"})
            Case "NIO" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "500"})
            Case "NGN" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "500", "1,000"})
            Case "KPW" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "200", "500", "1,000", "2,000", "5,000"})
            Case "NOK" : DenominationComboBox.Items.AddRange({"50", "100", "200", "500", "1,000"})
            Case "OMR" : DenominationComboBox.Items.AddRange({"1/2", "1", "5", "10", "20", "50", "100", "200"})
            Case "PKR" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "500", "1,000", "5,000"})
            Case "PAB" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20"})
            Case "PGK" : DenominationComboBox.Items.AddRange({"2", "5", "10", "20", "50"})
            Case "PYG" : DenominationComboBox.Items.AddRange({"2,000", "5,000", "10,000", "20,000", "50,000", "100,000"})
            Case "PEN" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200"})
            Case "PHP" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "500", "1,000"})
            Case "PLN" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200"})
            Case "PTE" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100", "200", "500", "1,000", "2,000", "5,000", "10,000"})
            Case "QAR" : DenominationComboBox.Items.AddRange({"1", "5", "10", "50", "100", "500"})
            Case "RON" : DenominationComboBox.Items.AddRange({"1", "5", "10", "50", "100", "200", "500"})
            Case "RUB" : DenominationComboBox.Items.AddRange({"5", "10", "50", "100", "500", "1,000", "5,000"})
            Case "RWF" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "5,000"})
            Case "WST" : DenominationComboBox.Items.AddRange({"2", "5", "10", "20", "50", "100"})
            Case "STD" : DenominationComboBox.Items.AddRange({"100", "250", "500", "1,000", "2,000", "5,000", "10,000", "20,000", "50,000", "100,000"})
            Case "SAR" : DenominationComboBox.Items.AddRange({"1", "5", "10", "50", "100", "500"})
            Case "RSD" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "500", "1,000", "2,000", "5,000"})
            Case "SCR" : DenominationComboBox.Items.AddRange({"25", "50", "100", "500"})
            Case "SLL" : DenominationComboBox.Items.AddRange({"1,000", "2,000", "5,000", "10,000"})
            Case "SGD" : DenominationComboBox.Items.AddRange({"2", "5", "10", "50", "100", "500", "1,000", "10,000"})
            Case "SKK" : DenominationComboBox.Items.AddRange({"20", "50", "100", "200", "500", "1,000", "5,000"})
            Case "SIT" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "500", "1,000", "5,000", "10,000"})
            Case "SBD" : DenominationComboBox.Items.AddRange({"5", "10", "20", "40", "50", "100"})
            Case "SOS" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "500", "1,000"})
            Case "ZAR" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200"})
            Case "KRW" : DenominationComboBox.Items.AddRange({"1,000", "5,000", "10,000"})
            Case "ESP" : DenominationComboBox.Items.AddRange({"1", "5", "25", "50", "100", "200", "500", "1,000", "2,000", "5,000"})
            Case "LKR" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200", "500", "1,000", "2,000", "5,000"})
            Case "SHP" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "40", "50", "100"})
            Case "SDG" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50"})
            Case "SRD" : DenominationComboBox.Items.AddRange({"5", "10", "25", "100", "1,000"})
            Case "SZL" : DenominationComboBox.Items.AddRange({"10", "20", "50", "100", "200"})
            Case "SEK" : DenominationComboBox.Items.AddRange({"20", "50", "100", "500"})
            Case "SYP" : DenominationComboBox.Items.AddRange({"50", "100", "200", "500", "1,000", "2,000"})
            Case "TWD" : DenominationComboBox.Items.AddRange({"100", "200", "500", "1,000", "2,000"})
            Case "TZS" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "5,000", "10,000"})
            Case "THB" : DenominationComboBox.Items.AddRange({"20", "50", "100", "500", "1,000"})
            Case "TOP" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50"})
            Case "TTD" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100"})
            Case "TND" : DenominationComboBox.Items.AddRange({"5", "10", "20", "30", "50"})
            Case "TRY" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100", "200"})
            Case "TMM" : DenominationComboBox.Items.AddRange({"1", "5", "10", "20", "50", "100", "500"})
            Case "UGX" : DenominationComboBox.Items.AddRange({"1,000", "2,000", "5,000", "10,000", "20,000", "50,000"})
            Case "UAH" : DenominationComboBox.Items.AddRange({"1", "2", "5", "10", "20", "50", "100", "200", "500", "1,000"})
            Case "UYU" : DenominationComboBox.Items.AddRange({"20", "50", "100", "200", "500", "1,000", "2,000"})
            Case "AED" : DenominationComboBox.Items.AddRange({"5", "10", "20", "50", "100", "200", "500", "1,000"})
            Case "VUV" : DenominationComboBox.Items.AddRange({"100", "200", "500", "1,000", "2,000", "5,000", "10,000"})
            Case "VEB" : DenominationComboBox.Items.AddRange({"2,000", "5,000", "10,000", "20,000", "50,000"})
            Case "VND" : DenominationComboBox.Items.AddRange({"500", "1,000", "2,000", "5,000", "10,000", "20,000", "50,000", "200,000", "500,000"})
            Case "YER" : DenominationComboBox.Items.AddRange({"50", "100", "200", "250", "500", "1,000"})
            Case "ZMK" : DenominationComboBox.Items.AddRange({"50", "100", "500", "1,000", "5,000", "10,000", "20,000", "50,000"})
            Case "ZWD" : DenominationComboBox.Items.AddRange({"1", "5", "10", "50", "100", "500", "1,000", "1,000,000,000,000"})
            Case Else
                ' No specific denominations, leave empty
        End Select
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Date_Recorded_OnTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Date_Recorded_OnTextBox.Text = DateTimePicker1.Value.ToString("dd/MM/yyyy")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SourceTextBox.Text = ("(none)")
    End Sub

    Private Sub TableDataGridView_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView.CellEnter
        ' This event populates image picture boxes based on selected row.
        If e.RowIndex >= 0 Then ' Ensure a valid row is clicked
            Frontal_ImagePictureBox.Image = Nothing
            Backward_ImagePictureBox.Image = Nothing

            If Not String.IsNullOrWhiteSpace(Frontal_ImageTextBox.Text) Then
                Dim frontalImagePath As String = Path.Combine(FullImagePath, Frontal_ImageTextBox.Text)
                If File.Exists(frontalImagePath) Then
                    Frontal_ImagePictureBox.ImageLocation = frontalImagePath
                Else
                    ' Handle case where image file is missing
                    Frontal_ImagePictureBox.Image = My.Resources.None_Image ' Assuming None_Image exists
                End If
            Else
                Frontal_ImagePictureBox.Image = My.Resources.None_Image ' Assuming None_Image exists
            End If

            If Not String.IsNullOrWhiteSpace(Backward_ImageTextBox.Text) Then
                Dim backwardImagePath As String = Path.Combine(FullImagePath, Backward_ImageTextBox.Text)
                If File.Exists(backwardImagePath) Then
                    Backward_ImagePictureBox.ImageLocation = backwardImagePath
                Else
                    ' Handle case where image file is missing
                    Backward_ImagePictureBox.Image = My.Resources.None_Image ' Assuming None_Image exists
                End If
            Else
                Backward_ImagePictureBox.Image = My.Resources.None_Image ' Assuming None_Image exists
            End If
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        RefreshDatabase()
    End Sub

    Private Sub frmNotes_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            MessageBox.Show("Ctrl+S pressed!", "Keyboard Shortcut", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Optionally call save method: TableBindingNavigatorSaveItem_Click(TableBindingNavigatorSaveItem, EventArgs.Empty)
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If My.Computer.Network.IsAvailable Then
            Dim amount As String = DenominationComboBox.Text.Replace(",", "")
            Dim fromCurrency As String = Currency_NameComboBox.Text
            Dim toCurrency As String = My.Settings.ToCurrency ' Assuming My.Settings.ToCurrency is defined

            If Not String.IsNullOrWhiteSpace(amount) AndAlso Not String.IsNullOrWhiteSpace(fromCurrency) AndAlso Not String.IsNullOrWhiteSpace(toCurrency) Then
                System.Diagnostics.Process.Start($"https://www.xe.com/currencyconverter/convert/?Amount={amount}&From={fromCurrency}&To={toCurrency}")
            Else
                MessageBox.Show("Please ensure Denomination, Currency Name, and Target Currency are selected.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Network is not available. Please try again later.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If My.Settings.ScannerDrive Is Nothing OrElse String.IsNullOrWhiteSpace(My.Settings.ScannerDrive) Then
            MessageBox.Show("There is no driver file selected. Please select it from settings option.", "Scanner Driver Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If File.Exists(My.Settings.ScannerDrive) Then
                System.Diagnostics.Process.Start(My.Settings.ScannerDrive)
            Else
                MessageBox.Show("Scanner driver file not found at the specified path: " & My.Settings.ScannerDrive, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub TableBindingNavigator_RefreshItems(sender As Object, e As EventArgs) Handles TableBindingNavigator.RefreshItems

    End Sub

    Private Sub TableDataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView.CellClick
        Frontal_ImagePictureBox.Image = Nothing
        Backward_ImagePictureBox.Image = Nothing

        If Frontal_ImageTextBox.Text = Nothing Then
        Else
            Dim a As String
            a = Frontal_ImageTextBox.Text
            Frontal_ImagePictureBox.ImageLocation = Application.StartupPath & "\" & folder & "\" & a
            a = Nothing
        End If

        If Backward_ImageTextBox.Text = Nothing Then
        Else
            Dim b As String
            b = Backward_ImageTextBox.Text
            Backward_ImagePictureBox.ImageLocation = Application.StartupPath & "\" & folder & "\" & b
            b = Nothing
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Frontal_ImageTextBox.Text = Nothing
        Frontal_ImagePictureBox.Image = Nothing
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try

            With OpenFileDialog1

                .Filter = "Jpg, Jpeg Image|*,jpg;*.jpeg|PNG Image|*.png|BMP Image|*,bmp"
                .FileName = ""
                .Title = "Choose Frontal Image of the note ..."
                .AddExtension = True
                .FilterIndex = 1
                .Multiselect = False
                .ValidateNames = True
                .InitialDirectory = (Application.StartupPath)
                .RestoreDirectory = True

                Dim FileToCopy_F As String
                Dim NewCopy_F As String
                Dim FileName_F As String
                Dim FileExtention_F As String
                Dim Memory As Integer = 0



                If (.ShowDialog = DialogResult.OK) Then

                    FileToCopy_F = OpenFileDialog1.FileName
                    FileName_F = Path.GetFileName(OpenFileDialog1.FileName)
                    FileExtention_F = Path.GetExtension(OpenFileDialog1.FileName)

                    NewCopy_F = Application.StartupPath & "\" & folder & "\0_F" & FileExtention_F

                    If File.Exists(NewCopy_F) Then
                        Memory = 0

                        Do While File.Exists(Application.StartupPath & "\" & folder & "\" & Memory & "_F" & FileExtention_F)
                            Memory = Memory + 1

                        Loop

                        NewCopy_F = Application.StartupPath & "\" & folder & "\" & Memory & "_F" & FileExtention_F

                        System.IO.File.Copy(FileToCopy_F, NewCopy_F)
                    Else
                        Memory = 0

                        NewCopy_F = Application.StartupPath & "\" & folder & "\" & Memory & "_F" & FileExtention_F

                        Memory = 0

                        System.IO.File.Copy(FileToCopy_F, NewCopy_F)
                    End If

                    Frontal_ImageTextBox.Text = System.IO.Path.GetFileName(NewCopy_F)
                    Frontal_ImagePictureBox.Image = Image.FromFile(NewCopy_F)
                Else
                    Return
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try

            With OpenFileDialog2

                .Filter = "Jpg, Jpeg Image|*,jpg;*.jpeg|PNG Image|*.png|BMP Image|*,bmp"
                .FileName = ""
                .Title = "Choose Backward Image of the note ..."
                .AddExtension = True
                .FilterIndex = 1
                .Multiselect = False
                .ValidateNames = True
                .InitialDirectory = (Application.StartupPath)
                .RestoreDirectory = True

                Dim FileToCopy_B As String
                Dim NewCopy_B As String
                Dim FileName_B As String
                Dim FileExtention_B As String
                Dim Memory_B As Integer = 0



                If (.ShowDialog = DialogResult.OK) Then

                    FileToCopy_B = OpenFileDialog2.FileName
                    FileName_B = Path.GetFileName(OpenFileDialog2.FileName)
                    FileExtention_B = Path.GetExtension(OpenFileDialog2.FileName)

                    NewCopy_B = Application.StartupPath & "\" & folder & "\0_B" & FileExtention_B

                    If File.Exists(NewCopy_B) Then
                        Memory_B = 0

                        Do While File.Exists(Application.StartupPath & "\" & folder & "\" & Memory_B & "_B" & FileExtention_B)
                            Memory_B = Memory_B + 1

                        Loop

                        NewCopy_B = Application.StartupPath & "\" & folder & "\" & Memory_B & "_B" & FileExtention_B

                        System.IO.File.Copy(FileToCopy_B, NewCopy_B)
                    Else
                        Memory_B = 0

                        NewCopy_B = Application.StartupPath & "\" & folder & "\" & Memory_B & "_B" & FileExtention_B

                        Memory_B = 0

                        System.IO.File.Copy(FileToCopy_B, NewCopy_B)
                    End If

                    Backward_ImageTextBox.Text = System.IO.Path.GetFileName(NewCopy_B)
                    Backward_ImagePictureBox.Image = Image.FromFile(NewCopy_B)
                Else
                    Return
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Backward_ImageTextBox.Text = Nothing
        Backward_ImagePictureBox.Image = Nothing
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        frmDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub frmNotes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        System.Windows.Forms.Application.Exit()
    End Sub



    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles Frontal_ImagePictureBox.DoubleClick
        Try
            If Frontal_ImageTextBox.Text IsNot Nothing Or Frontal_ImagePictureBox IsNot My.Resources.None_Image Then
                System.Diagnostics.Process.Start(Application.StartupPath & "\" & folder & "\" & Frontal_ImageTextBox.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox2_DoubleClick(sender As Object, e As EventArgs) Handles Backward_ImagePictureBox.DoubleClick
        Try
            If Backward_ImageTextBox.Text IsNot Nothing Or Backward_ImagePictureBox IsNot My.Resources.None_Image Then
                System.Diagnostics.Process.Start(Application.StartupPath & "\" & folder & "\" & Backward_ImageTextBox.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles Frontal_ImagePictureBox.Click
        If Frontal_ImagePictureBox IsNot Nothing Or Frontal_ImagePictureBox IsNot My.Resources.None_Image Then
            Frontal_ImagePictureBox.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub PictureBox1_Leave(sender As Object, e As EventArgs) Handles Frontal_ImagePictureBox.Leave
        Frontal_ImagePictureBox.Cursor = Cursors.Default
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles Backward_ImagePictureBox.Click
        If Backward_ImagePictureBox IsNot Nothing Or Backward_ImagePictureBox IsNot My.Resources.None_Image Then
            Backward_ImagePictureBox.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub PictureBox2_Leave(sender As Object, e As EventArgs) Handles Backward_ImagePictureBox.Leave
        Backward_ImagePictureBox.Cursor = Cursors.Default
    End Sub



    Private Sub butReset_Click(sender As Object, e As EventArgs) Handles butReset.Click
        cmbCategory.Text = Nothing
        cmbtxtSearch.Text = Nothing
        cmbtxtSearch.Enabled = True
        cmbtxtSearch.Items.Clear()
        TableBindingSource.Filter = Nothing

        TableDataGridView.Columns(0).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(1).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(2).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(3).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(4).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(5).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(6).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(7).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(8).DefaultCellStyle.BackColor = Color.YellowGreen
        TableDataGridView.Columns(9).DefaultCellStyle.BackColor = Color.YellowGreen

    End Sub


    Private Sub dtpSearch_ValueChanged(sender As Object, e As EventArgs) Handles dtpSearch.ValueChanged

        RemoveImage()
        If cmbCategory.SelectedIndex = 5 Then
            cmbtxtSearch.Text = dtpSearch.Value.ToString("dd/MM/yyyy")
        Else

        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        filter()
    End Sub
    Private Sub RemoveImage()
        Frontal_ImagePictureBox.Image = Nothing
        Backward_ImagePictureBox.Image = Nothing
        Frontal_ImageTextBox.Text = Nothing
        Backward_ImageTextBox.Text = Nothing
    End Sub




    Private Sub Button9_Click(sender As Object, e As EventArgs)

    End Sub



    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If My.Computer.Network.IsAvailable Then
            System.Diagnostics.Process.Start("http://banknote.ws")
        Else
            MessageBox.Show("Network is not available. Please try again later.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub


    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub




    Private Sub cmbCategory_DropDown(sender As Object, e As EventArgs) Handles cmbCategory.DropDown

    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub





    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub frmNotes_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop, False) = True Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Dim FileNamePrimary As String
    Private Sub frmNotes_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Dim droppedFiles As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

        If Frontal_ImagePictureBox.Image IsNot Nothing AndAlso Backward_ImagePictureBox.Image IsNot Nothing Then
            MessageBox.Show("Both picture boxes are filled. Please clear one before dropping a new image.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        For Each file In droppedFiles
            FileNamePrimary = GetFileNameFromPath(file) ' Use a more descriptive helper function
            Dim fileExtension As String = Path.GetExtension(FileNamePrimary).ToLower()

            If fileExtension = ".png" OrElse fileExtension = ".bmp" OrElse fileExtension = ".jpg" OrElse fileExtension = ".jpeg" Then
                If Frontal_ImagePictureBox.Image IsNot Nothing Then
                    BackDrag()
                ElseIf Frontal_ImagePictureBox.Image Is Nothing Then
                    FrontDrag()
                End If
            Else
                MessageBox.Show("The file format you have chosen is not supported for this function. Please choose an image file of format (.jpeg or .png or .bmp or .jpg)", "Unsupported Format", MessageBoxButtons.OK, MessageBoxIcon.Exclamation )
            End If
        Next
    End Sub

    Private Function GetFileNameFromPath(fullPath As String) As String
        Return System.IO.Path.GetFullPath(fullPath)
    End Function

    Private Sub FrontDrag()
        Try
            If Not String.IsNullOrWhiteSpace(FileNamePrimary) Then
                Dim fileExtention_F As String = Path.GetExtension(FileNamePrimary)
                Dim memory As Integer = 0

                Do While File.Exists(Path.Combine(FullImagePath, $"{memory}_F{fileExtention_F}"))
                    memory += 1
                Loop
                Dim newCopy_F As String = Path.Combine(FullImagePath, $"{memory}_F{fileExtention_F}")

                System.IO.File.Copy(FileNamePrimary, newCopy_F)

                Frontal_ImageTextBox.Text = Path.GetFileName(newCopy_F)
                Frontal_ImagePictureBox.Image = Image.FromFile(newCopy_F)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error during frontal image drag-drop: {ex.Message}", "Drag-Drop Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"FrontDrag Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub
    Private Sub BackDrag()
        Try
            If Not String.IsNullOrWhiteSpace(FileNamePrimary) Then
                Dim fileExtention_B As String = Path.GetExtension(FileNamePrimary)
                Dim memory_B As Integer = 0

                Do While File.Exists(Path.Combine(FullImagePath, $"{memory_B}_B{fileExtention_B}"))
                    memory_B += 1
                Loop
                Dim newCopy_B As String = Path.Combine(FullImagePath, $"{memory_B}_B{fileExtention_B}")

                System.IO.File.Copy(FileNamePrimary, newCopy_B)

                Backward_ImageTextBox.Text = Path.GetFileName(newCopy_B)
                Backward_ImagePictureBox.Image = Image.FromFile(newCopy_B)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error during backward image drag-drop: {ex.Message}", "Drag-Drop Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"BackDrag Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

    Public Function getfilename(path As String)
        Return System.IO.Path.GetFullPath(path)

    End Function

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        frmDatabase.TabControl1.SelectedIndex = 2
        frmDatabase.Show()
    End Sub

    Private Sub Currency_NameComboBox_Leave(sender As Object, e As EventArgs) Handles Currency_NameComboBox.Leave
        Currency_NameComboBox.Text = Currency_NameComboBox.Text.ToUpper
    End Sub

    Private Sub ContinentComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ContinentComboBox.SelectedIndexChanged
        CheckEnteries()
    End Sub

    Private Sub ContinentComboBox_TextChanged(sender As Object, e As EventArgs) Handles ContinentComboBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub CountryComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CountryComboBox.SelectedIndexChanged
        CheckEnteries()
    End Sub

    Private Sub CountryComboBox_TextChanged(sender As Object, e As EventArgs) Handles CountryComboBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub Catalogue_CodeTextBox_TextChanged(sender As Object, e As EventArgs) Handles Catalogue_CodeTextBox.TextChanged
        CheckEnteries()
    End Sub


    Private Sub DenominationComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DenominationComboBox.SelectedIndexChanged
        CheckEnteries()
    End Sub

    Private Sub DenominationComboBox_TextChanged(sender As Object, e As EventArgs) Handles DenominationComboBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub Date_Recorded_OnTextBox_TextChanged(sender As Object, e As EventArgs) Handles Date_Recorded_OnTextBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub Serial_NoTextBox_TextChanged(sender As Object, e As EventArgs) Handles Serial_NoTextBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub ConditionComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ConditionComboBox.SelectedIndexChanged
        CheckEnteries()
    End Sub

    Private Sub ConditionComboBox_TextChanged(sender As Object, e As EventArgs) Handles ConditionComboBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub Cost_To_MeTextBox_TextChanged(sender As Object, e As EventArgs) Handles Cost_To_MeTextBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub SourceTextBox_TextChanged(sender As Object, e As EventArgs) Handles SourceTextBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub CommentRichTextBox_TextChanged(sender As Object, e As EventArgs) Handles CommentRichTextBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub ConditionComboBox_Leave(sender As Object, e As EventArgs) Handles ConditionComboBox.Leave
        ConditionComboBox.Text = ConditionComboBox.Text.ToUpper
    End Sub



    Private Sub Serial_NoTextBox_Leave(sender As Object, e As EventArgs) Handles Serial_NoTextBox.Leave
        Serial_NoTextBox.Text = Serial_NoTextBox.Text.ToUpper
    End Sub

    Private Sub Button12_MouseEnter(sender As Object, e As EventArgs) Handles Button12.MouseEnter
        Button12.Image = My.Resources.Refresh2
    End Sub

    Private Sub Button12_MouseLeave(sender As Object, e As EventArgs) Handles Button12.MouseLeave
        Button12.Image = My.Resources.Refresh1
    End Sub


End Class