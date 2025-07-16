Imports System.Data.OleDb

Public Class frmCoins
    Dim conn As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Coins.accdb")
    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub TableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles TableBindingNavigatorSaveItem.Click
        If SourceTextBox.Text = Nothing Then
            SourceTextBox.Text = ("(none)")
        End If

        Me.Validate()
        '  Me.TableBindingSource.EndEdit()
        '  Me.TableAdapterManager.UpdateAll(Me.CoinsDataSet)

        Try
            TableBindingSource.EndEdit()
            TableTableAdapter.Update(CoinsDataSet.Table)

        Catch ex As Exception

        End Try

        Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
        ActiveControl = ContinentComboBox
    End Sub
    Private Sub CheckEnteries()
        If ContinentComboBox.Text = Nothing Or CountryComboBox.Text = Nothing Or Currency_NameComboBox.Text = Nothing Or DenominationTextBox.Text = Nothing Or Date_Recorded_OnTextBox.Text = Nothing Then
            TableBindingNavigatorSaveItem.Enabled = False
        Else
            TableBindingNavigatorSaveItem.Enabled = True
        End If
    End Sub
    Private Sub frmCoins_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Currency_Short_NamesDataSet.Table' table. You can move, or remove it, as needed.
        'Me.TableTableAdapter1.Fill(Me.Currency_Short_NamesDataSet.Table)

        ActiveControl = ContinentComboBox

        Try
            Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
            'Me.TableTableAdapter1.Fill(Me.NotesDataSet.Table)
        Catch ex As Exception
            MsgBox("Unable to load the database, please try to restore or reset the database from the Backup and Restore window. The program will redirect you to the Backup and Restore form", vbOKOnly + vbCritical)
            frmBackupRestore.Show()
            frmBackupRestore.TabControl.SelectTab(1)
            Me.Hide()
        End Try

        Try
            Me.TableTableAdapter2.Fill(Me.Currency_Short_NamesDataSet.Table)
        Catch ex As Exception
            MsgBox("Unable to load the currency names database, some of the functions will not be partially functioning due to this inability. Please try to restore the database if possible.", vbOKOnly + vbExclamation)
        End Try

        CountryComboBox.Text = Nothing
        Currency_NameComboBox.Text = Nothing
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
            Process.Start("https://www.xe.com/currencyconverter/convert/?Amount=" + DenominationTextBox.Text & "&From=" + Currency_NameComboBox.Text & "&To=" + My.Settings.ToCurrency)
        Else
            MsgBox("Network is not available", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If My.Computer.Network.IsAvailable Then
            If Currency_NameComboBox.Text = "" Then
                Process.Start("https://www.xe.com/currency/")
            Else
                Process.Start("https://www.xe.com/currency/" + Currency_NameComboBox.Text)
            End If
        Else
            MsgBox("Network is not available please try again later", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        frmDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)

    End Sub


    Private Sub dtpSearch_ValueChanged(sender As Object, e As EventArgs) Handles dtpSearch.ValueChanged
        If cmbCategory.SelectedIndex = 5 Then
            cmbtxtSearch.Text = dtpSearch.Value.ToString("dd/MM/yyyy")
        Else

        End If
    End Sub
    Dim SwitchColour As Boolean
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles butReset.Click
        cmbCategory.Text = Nothing
        cmbtxtSearch.Text = Nothing
        cmbtxtSearch.Enabled = True
        cmbtxtSearch.Items.Clear()
        TableBindingSource.Filter = Nothing

        TableDataGridView.Columns(0).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(1).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(2).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(3).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(4).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(5).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(6).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(7).DefaultCellStyle.BackColor = Color.Coral

    End Sub

    Private Sub cmbtxtSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.SelectedIndexChanged
        filterPerfect()
    End Sub

    Private Sub cmbtxtSearch_TextChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.TextChanged
        filter()
    End Sub

    Private Sub filter()
        Dim cantfind As String = cmbtxtSearch.Text

        Try

            If cmbCategory.SelectedIndex = 0 Then

                TableBindingSource.Filter = "Continent Like'%" & Me.cmbtxtSearch.Text & "%'"

            ElseIf cmbCategory.SelectedIndex = 1 Then

                TableBindingSource.Filter = "Country Like'%" & Me.cmbtxtSearch.Text & "%'"

            ElseIf cmbCategory.SelectedIndex = 2 Then

                TableBindingSource.Filter = "Catalogue_Code Like'%" & Me.cmbtxtSearch.Text & "%'"

            ElseIf cmbCategory.SelectedIndex = 3 Then
                Dim text As String

                text = UCase(cmbtxtSearch.Text)

                TableBindingSource.Filter = "Currency_Name Like'%" & text & "%'"

            ElseIf cmbCategory.SelectedIndex = 4 Then

                TableBindingSource.Filter = "Denomination Like'%" & Me.cmbtxtSearch.Text & "%'"

            ElseIf cmbCategory.SelectedIndex = 5 Then

                TableBindingSource.Filter = "Date_Recorded_On Like'%" & Me.cmbtxtSearch.Text & "%'"

            ElseIf cmbCategory.SelectedIndex = 6 Then

                TableBindingSource.Filter = "Source Like'%" & Me.cmbtxtSearch.Text & "%'"

            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        If cmbCategory.SelectedIndex = 5 Then
            cmbtxtSearch.Enabled = False
            dtpSearch.Visible = True
        Else
            cmbtxtSearch.Enabled = True
            dtpSearch.Visible = False
        End If
        Me.cmbtxtSearch.Text = Nothing
        TableBindingSource.Filter = Nothing

        TableDataGridView.Columns(0).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(1).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(2).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(3).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(4).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(5).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(6).DefaultCellStyle.BackColor = Color.Coral
        TableDataGridView.Columns(7).DefaultCellStyle.BackColor = Color.Coral

        If cmbCategory.SelectedIndex >= 0 Then
            If cmbCategory.SelectedIndex < 6 Then
                TableDataGridView.Columns(cmbCategory.SelectedIndex).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
            ElseIf cmbCategory.SelectedIndex = 6 Then
                TableDataGridView.Columns(7).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
            Else
            End If
        End If

        conn.Open()

        If cmbCategory.SelectedIndex = 0 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Continent FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Continent"))
            End While
        ElseIf cmbCategory.SelectedIndex = 1 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Country FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Country"))
            End While
        ElseIf cmbCategory.SelectedIndex = 2 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Catalogue_Code FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Catalogue_Code"))
            End While
        ElseIf cmbCategory.SelectedIndex = 3 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Currency_Name FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Currency_Name"))
            End While
        ElseIf cmbCategory.SelectedIndex = 4 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Denomination FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Denomination"))
            End While
        ElseIf cmbCategory.SelectedIndex = 5 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Date_Recorded_On FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Date_Recorded_On"))
            End While

        ElseIf cmbCategory.SelectedIndex = 6 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Source FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Source"))
            End While

        Else
            cmbtxtSearch.Items.Clear()
        End If

        conn.Close()
    End Sub

    Private Sub filterPerfect()
        Dim cantfind As String = cmbtxtSearch.Text

        Try

            If cmbCategory.SelectedIndex = 0 Then

                TableBindingSource.Filter = "Continent Like'" & Me.cmbtxtSearch.Text & "'"

            ElseIf cmbCategory.SelectedIndex = 1 Then

                TableBindingSource.Filter = "Country Like'" & Me.cmbtxtSearch.Text & "'"

            ElseIf cmbCategory.SelectedIndex = 2 Then

                TableBindingSource.Filter = "Catalogue_Code Like'" & Me.cmbtxtSearch.Text & "'"

            ElseIf cmbCategory.SelectedIndex = 3 Then
                Dim text As String

                text = UCase(cmbtxtSearch.Text)

                TableBindingSource.Filter = "Currency_Name Like'" & text & "'"

            ElseIf cmbCategory.SelectedIndex = 4 Then

                TableBindingSource.Filter = "Denomination Like'" & Me.cmbtxtSearch.Text & "'"

            ElseIf cmbCategory.SelectedIndex = 5 Then

                TableBindingSource.Filter = "Date_Recorded_On Like'" & Me.cmbtxtSearch.Text & "'"

            ElseIf cmbCategory.SelectedIndex = 6 Then

                TableBindingSource.Filter = "Source Like'" & Me.cmbtxtSearch.Text & "'"

            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbCategory_DropDown(sender As Object, e As EventArgs) Handles cmbCategory.DropDown

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        RefreshDatabase()
    End Sub

    Public Sub RefreshDatabase()
        Try
            Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
        Catch ex As Exception
            MsgBox("Unable to load the database, please try to restore or reset the database from the Backup and Restore window. The program will redirect you to the Backup and Restore form", vbOKOnly + vbCritical)
            frmBackupRestore.Show()
            frmBackupRestore.TabControl.SelectTab(1)
            Me.Hide()
        End Try
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

    Private Sub BindingNavigatorDeleteItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorDeleteItem.Click
        If Val(IDTextBox.Text) < 0 Then
            MsgBox("The record which you are trying to delete is not saved, first save it to continue the action", vbOKOnly + vbCritical)

        Else
            Dim cn As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Coins.accdb")
            Dim cmd As OleDbCommand
            Dim da As OleDbDataAdapter
            Dim dt As DataTable

            cmd = New OleDbCommand("DELETE FROM [Table] WHERE ID=" & Val(IDTextBox.Text) & ";", cn)
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()

            Me.Validate()
            Me.TableBindingSource.EndEdit()

            'Me.TableAdapterManager.UpdateAll(Me.NotesDataSet)
            'Me.TableTableAdapter.Update(NotesDataSet.Table)

            CoinsDataSet.AcceptChanges()

            MsgBox("The record has been deleted ", vbOKOnly + vbExclamation)
            Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)

        End If

        ActiveControl = ContinentComboBox
    End Sub

    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click
        If SourceTextBox.Text = Nothing Then
            SourceTextBox.Text = ("(none)")
        End If

        ActiveControl = ContinentComboBox
    End Sub

    Private Sub Button6_MouseEnter(sender As Object, e As EventArgs) Handles Button6.MouseEnter
        Button6.Image = My.Resources.Refresh2
    End Sub

    Private Sub Button6_MouseLeave(sender As Object, e As EventArgs) Handles Button6.MouseLeave
        Button6.Image = My.Resources.Refresh1
    End Sub
End Class