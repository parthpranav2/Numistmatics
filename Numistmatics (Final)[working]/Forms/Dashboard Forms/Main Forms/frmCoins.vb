Imports System.Data.OleDb
Imports System.IO

Public Class frmCoins
    Dim conn As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Coins.accdb")

    Private ReadOnly ConnectionString As String = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path.Combine(Application.StartupPath, "Coins.accdb")}"


    ' --- Form Load and Initialization ---
    Private Sub frmCoins_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.CoinsDataSet = New CoinsDataSet()
        Me.TableTableAdapter = New CoinsDataSetTableAdapters.TableTableAdapter()
        Me.TableBindingSource = New BindingSource()
        Me.TableBindingSource.DataSource = Me.CoinsDataSet
        Me.TableBindingSource.DataMember = "Table"
        Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
        Me.TableDataGridView.DataSource = Me.TableBindingSource

        ActiveControl = ContinentComboBox


        Try
            RefreshDatabase()
        Catch ex As Exception
            MessageBox.Show("Unable to load the main database during startup. The program will redirect you to the Backup and Restore form.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"Startup Database Load Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")

            frmBackupRestore.Show()
            frmBackupRestore.TabControl.SelectTab(1)
            Me.Hide()
        End Try


        Try
            Me.TableTableAdapter2.Fill(Me.Currency_Short_NamesDataSet.Table)
        Catch ex As OleDbException
            MessageBox.Show($"Database error loading currency names: {ex.Message}{Environment.NewLine}Please ensure 'Currency_Short_NamesDataSet' is correctly configured.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"OleDbException (Currency Names Load): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred loading currency names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (Currency Names Load): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try

        CountryComboBox.Text = Nothing
        Currency_NameComboBox.Text = Nothing
        CheckEnteries()
    End Sub


    ' --- Data Retrieval (Refresh) ---
    Public Sub RefreshDatabase()
        Try
            Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)

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
        ' Set SourceTextBox to "(none)" if it's empty
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
            ' TableTableAdapter.Update(CoinsDataSet.Table) is sufficient for the main table.
            ' Your original code had a commented-out TableAdapterManager.UpdateAll,
            ' so I'm keeping the explicit TableTableAdapter.Update for CoinsDataSet.Table.
            Me.TableTableAdapter.Update(CoinsDataSet.Table)

            MessageBox.Show("Record saved successfully!", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As OleDbException
            ' Specific handling for database errors (e.g., constraint violations, invalid data types)
            MessageBox.Show($"Database error saving record: {ex.Message}{Environment.NewLine}Please check your data for validity.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"OleDbException (Save): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            ' You might want to reject changes in the DataTable if the save fails
            CoinsDataSet.RejectChanges()
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

    ' --- Data Adding (BindingNavigatorAddNewItem_Click) ---
    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click
        ActiveControl = ContinentComboBox

        If SourceTextBox.Text = Nothing Then
            SourceTextBox.Text = ("(none)")
        End If

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
                ' Using the BindingSource's RemoveCurrent method to mark the row for deletion,
                ' then committing with TableAdapter.Update(). This is the preferred way
                ' when using DataSets/TableAdapters.
                Me.TableBindingSource.RemoveCurrent() ' Marks the current row in BindingSource/DataTable for deletion
                Me.TableTableAdapter.Update(CoinsDataSet.Table) ' Commits the deletion to the database

                ' After successful deletion, accept changes in the DataSet
                CoinsDataSet.AcceptChanges()

                MessageBox.Show("The record has been deleted successfully.", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As OleDbException
                MessageBox.Show($"Database error deleting record: {ex.Message}{Environment.NewLine}This might be due to related records in other tables (referential integrity).", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Console.WriteLine($"OleDbException (Delete): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
                ' If deletion fails, reject changes to revert the DataTable state
                CoinsDataSet.RejectChanges()
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
            TableDataGridView.Columns(i).DefaultCellStyle.BackColor = Color.Coral ' Using Coral as per your original code
        Next

        ' Highlight selected column
        If cmbCategory.SelectedIndex >= 0 Then
            ' Adjust column index mapping based on your DataGridView columns
            Dim highlightColumnIndex As Integer = -1
            Select Case cmbCategory.SelectedIndex
                Case 0 : highlightColumnIndex = 0 ' Continent
                Case 1 : highlightColumnIndex = 1 ' Country
                Case 2 : highlightColumnIndex = 2 ' Catalogue_Code
                Case 3 : highlightColumnIndex = 3 ' Currency_Name
                Case 4 : highlightColumnIndex = 4 ' Denomination
                Case 5 : highlightColumnIndex = 5 ' Date_Recorded_On
                Case 6 : highlightColumnIndex = 7 ' Source (assuming Source is column 7 in your DataGridView)
            End Select

            If highlightColumnIndex >= 0 AndAlso highlightColumnIndex < TableDataGridView.Columns.Count Then
                TableDataGridView.Columns(highlightColumnIndex).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
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
            Case 6 : sqlQuery = "SELECT DISTINCT Source FROM [Table]"
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
        filterPerfect()
    End Sub

    Private Sub cmbtxtSearch_TextChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.TextChanged
        filter()
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
            Case 6 : filterColumn = "Source"
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
            Case 6 : filterColumn = "Source"
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
           String.IsNullOrWhiteSpace(DenominationTextBox.Text) OrElse
           String.IsNullOrWhiteSpace(Date_Recorded_OnTextBox.Text) Then
            TableBindingNavigatorSaveItem.Enabled = False
        Else
            TableBindingNavigatorSaveItem.Enabled = True
        End If
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

    Private Sub frmCoins_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub Currency_NameComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Currency_NameComboBox.SelectedIndexChanged
        CheckEnteries()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If My.Computer.Network.IsAvailable Then
            Dim amount As String = DenominationTextBox.Text.Replace(",", "")
            Dim fromCurrency As String = Currency_NameComboBox.Text
            Dim toCurrency As String = My.Settings.ToCurrency ' Assuming My.Settings.ToCurrency is defined

            If Not String.IsNullOrWhiteSpace(amount) AndAlso Not String.IsNullOrWhiteSpace(fromCurrency) AndAlso Not String.IsNullOrWhiteSpace(toCurrency) Then
                System.Diagnostics.Process.Start($"https://www.xe.com/currencyconverter/convert/?Amount={amount}&From={fromCurrency}&To={toCurrency}")
            Else
                MessageBox.Show("Please ensure Denomination, Currency Name, and Target Currency are selected.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Network is not available.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If My.Computer.Network.IsAvailable Then
            If String.IsNullOrWhiteSpace(Currency_NameComboBox.Text) Then
                System.Diagnostics.Process.Start("https://www.xe.com/currency/")
            Else
                System.Diagnostics.Process.Start("https://www.xe.com/currency/" & Currency_NameComboBox.Text)
            End If
        Else
            MessageBox.Show("Network is not available, please try again later.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        frmDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub butReset_Click(sender As Object, e As EventArgs) Handles butReset.Click ' Renamed from Button6_Click to match your original
        cmbCategory.Text = Nothing
        cmbtxtSearch.Text = Nothing
        cmbtxtSearch.Enabled = True
        cmbtxtSearch.Items.Clear()
        TableBindingSource.Filter = Nothing

        ' Reset DataGridView column background colors
        For i As Integer = 0 To TableDataGridView.Columns.Count - 1
            TableDataGridView.Columns(i).DefaultCellStyle.BackColor = Color.Coral
        Next
    End Sub


    Private Sub dtpSearch_ValueChanged(sender As Object, e As EventArgs) Handles dtpSearch.ValueChanged
        If cmbCategory.SelectedIndex = 5 Then
            cmbtxtSearch.Text = dtpSearch.Value.ToString("dd/MM/yyyy")
        End If
    End Sub

    Dim SwitchColour As Boolean



    Private Sub cmbCategory_DropDown(sender As Object, e As EventArgs) Handles cmbCategory.DropDown

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        RefreshDatabase()
    End Sub



    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        frmDatabase.TabControl1.SelectedIndex = 2
        frmDatabase.Show()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

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

    Private Sub Currency_NameComboBox_TextChanged(sender As Object, e As EventArgs) Handles Currency_NameComboBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub DenominationTextBox_TextChanged(sender As Object, e As EventArgs) Handles DenominationTextBox.TextChanged
        CheckEnteries()
    End Sub

    Private Sub Date_Recorded_OnTextBox_TextChanged(sender As Object, e As EventArgs) Handles Date_Recorded_OnTextBox.TextChanged
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


    Private Sub Button6_MouseEnter(sender As Object, e As EventArgs) Handles Button6.MouseEnter
        Button6.Image = My.Resources.Refresh2
    End Sub

    Private Sub Button6_MouseLeave(sender As Object, e As EventArgs) Handles Button6.MouseLeave
        Button6.Image = My.Resources.Refresh1
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
End Class