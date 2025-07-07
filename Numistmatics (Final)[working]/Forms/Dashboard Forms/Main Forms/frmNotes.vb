Imports System.IO
Imports System.Data.OleDb

Public Class frmNotes
    Dim folderPrimary As String = ("Resources")
    Dim folderSecondary As String = ("\Images")
    Dim folder As String = ("Resources\Images")
    Dim conn As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Notes.accdb")
    Dim access As New Control

    Private Sub TableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles TableBindingNavigatorSaveItem.Click
        If SourceTextBox.Text = Nothing Then
            SourceTextBox.Text = ("(none)")
        End If

        'Me.Validate()
        'Me.TableBindingSource.EndEdit()
        'Me.TableAdapterManager.UpdateAll(Me.NotesDataSet)

        Try
            TableBindingSource.EndEdit()
            TableTableAdapter.Update(NotesDataSet.Table)

        Catch ex As Exception

        End Try

        RefreshDatabase()
        ActiveControl = ContinentComboBox
    End Sub

    Private Sub frmNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Currency_Short_NamesDataSet.Table' table. You can move, or remove it, as needed.
        Me.TableTableAdapter1.Fill(Me.Currency_Short_NamesDataSet.Table)
        CountryComboBox.Text = Nothing
        CheckEnteries()

        ActiveControl = ContinentComboBox

        'TODO: This line of code loads data into the 'NotesDataSet.Table' table. You can move, or remove it, as needed.
        Try
            RefreshDatabase()
        Catch ex As Exception
            MsgBox("Unable to load the database, please try to restore or reset the database from the Backup and Restore window. The program will redirect you to the Backup and Restore form", vbOKOnly + vbCritical)
            frmBackupRestore.Show()
            frmBackupRestore.tabRestore.Show()
            Me.Hide()
        End Try
        'creating image backup folder
        If Directory.Exists(folder) Then
        Else
            Directory.CreateDirectory(folder)
        End If

        If Directory.Exists(folderPrimary) Then
            File.SetAttributes(folderPrimary, FileAttributes.Hidden)
        Else
        End If

        If Directory.Exists(folderPrimary & folderSecondary) Then
            File.SetAttributes(folderPrimary & folderSecondary, FileAttributes.Hidden)
        Else
        End If

        Try
            Me.TableTableAdapter1.Fill(Me.Currency_Short_NamesDataSet.Table)
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

    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click
        ActiveControl = ContinentComboBox

        If SourceTextBox.Text = Nothing Then
            SourceTextBox.Text = ("(none)")
        End If

        Frontal_ImagePictureBox.Image = Nothing
        Backward_ImagePictureBox.Image = Nothing
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

        If cmbCategory.SelectedIndex >= 0 Then
            If cmbCategory.SelectedIndex < 8 Then
                TableDataGridView.Columns(cmbCategory.SelectedIndex).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
            ElseIf cmbCategory.SelectedIndex = 8 Then
                TableDataGridView.Columns(9).DefaultCellStyle.BackColor = Color.FromArgb(81, 187, 255)
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
            Dim strsql As New OleDbCommand("SELECT DISTINCT Serial_No FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Serial_No"))
            End While
        ElseIf cmbCategory.SelectedIndex = 7 Then
            Dim strsql As New OleDbCommand("SELECT DISTINCT Condition FROM [Table]", conn)
            Dim myreader As OleDb.OleDbDataReader = strsql.ExecuteReader
            cmbtxtSearch.Items.Clear()
            While myreader.Read
                cmbtxtSearch.Items.Add(myreader("Condition"))
            End While
        ElseIf cmbCategory.SelectedIndex = 8 Then
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

                TableBindingSource.Filter = "Serial_No Like'%" & Me.cmbtxtSearch.Text & "%'"

            ElseIf cmbCategory.SelectedIndex = 7 Then

                TableBindingSource.Filter = "Condition Like'%" & Me.cmbtxtSearch.Text & "%'"


            ElseIf cmbCategory.SelectedIndex = 8 Then

                TableBindingSource.Filter = "Source Like'%" & Me.cmbtxtSearch.Text & "%'"

            Else
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub RemoveImage()
        Frontal_ImagePictureBox.Image = Nothing
        Backward_ImagePictureBox.Image = Nothing
        Frontal_ImageTextBox.Text = Nothing
        Backward_ImageTextBox.Text = Nothing
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If My.Computer.Network.IsAvailable Then
            Process.Start("https://www.xe.com/currencyconverter/convert/?Amount=" + DenominationComboBox.Text & "&From=" + Currency_NameComboBox.Text & "&To=" + My.Settings.ToCurrency)
        Else
            MsgBox("Network is not available please try again later", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If My.Settings.ScannerDrive = Nothing Then
            MsgBox("There is no driver file selected so please select it from settings option", +vbOKOnly + vbExclamation)
        Else
            System.Diagnostics.Process.Start("" + My.Settings.ScannerDrive)
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If My.Computer.Network.IsAvailable Then
            Process.Start("http://banknote.ws")
        Else
            MsgBox("Network is not available please try again later", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

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

                TableBindingSource.Filter = "Serial_No Like'" & Me.cmbtxtSearch.Text & "'"

            ElseIf cmbCategory.SelectedIndex = 7 Then

                TableBindingSource.Filter = "Condition Like'" & Me.cmbtxtSearch.Text & "'"


            ElseIf cmbCategory.SelectedIndex = 8 Then

                TableBindingSource.Filter = "Source Like'" & Me.cmbtxtSearch.Text & "'"

            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbtxtSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.SelectedIndexChanged
        filterPerfect()
    End Sub

    Private Sub cmbtxtSearch_TextChanged(sender As Object, e As EventArgs) Handles cmbtxtSearch.TextChanged
        filter()
    End Sub

    Private Sub cmbCategory_DropDown(sender As Object, e As EventArgs) Handles cmbCategory.DropDown

    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub CheckEnteries()
        If ContinentComboBox.Text = Nothing Or CountryComboBox.Text = Nothing Or Currency_NameComboBox.Text = Nothing Or DenominationComboBox.Text = Nothing Or Date_Recorded_OnTextBox.Text = Nothing Then
            TableBindingNavigatorSaveItem.Enabled = False
        Else
            TableBindingNavigatorSaveItem.Enabled = True
        End If
    End Sub
    Public Sub RefreshDatabase()
        Frontal_ImagePictureBox.Image = Nothing
        Backward_ImagePictureBox.Image = Nothing
        Me.TableTableAdapter.Fill(Me.NotesDataSet.Table)
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            Frontal_ImagePictureBox.Image = Nothing
            Backward_ImagePictureBox.Image = Nothing
            Me.TableTableAdapter.Fill(Me.NotesDataSet.Table)
        Catch ex As Exception
            MsgBox("Unable to load the database, please try to restore or reset the database from the Backup and Restore window. The program will redirect you to the Backup and Restore form", vbOKOnly + vbCritical)
            frmBackupRestore.Show()
            frmBackupRestore.TabControl.SelectTab(1)
            Me.Hide()
        End Try
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
        Dim droppedfile As String() = e.Data.GetData(DataFormats.FileDrop)

        If Frontal_ImagePictureBox.Image IsNot Nothing And Backward_ImagePictureBox.Image IsNot Nothing Then
            MsgBox("Both the picture boxes are filled first clear them", vbOKOnly + vbInformation)
        Else
            For Each file In droppedfile
                Dim filename As String = getfilename(file)
                FileNamePrimary = filename

                If file.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) Or file.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase) Or file.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) Or file.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase) Then
                    If Frontal_ImagePictureBox.Image IsNot Nothing Then
                        BackDrag()
                    ElseIf Frontal_ImagePictureBox.Image Is Nothing Then
                        FrontDrag()
                    End If
                Else
                    MsgBox("The file format which you have chosen is not supported for this function. Please choose an image file of format (.jpeg or .png or .bmp or .jpg)", vbOKOnly + vbCritical)
                End If
            Next

        End If
    End Sub
    Private Sub FrontDrag()
        Dim FileToCopy_F As String
        Dim NewCopy_F As String
        Dim FileName_F As String
        Dim FileExtention_F As String
        Dim Memory As Integer = 0


        If FileNamePrimary.ToString IsNot Nothing Then

            FileToCopy_F = FileNamePrimary
            FileName_F = Path.GetFileName(FileNamePrimary)
            FileExtention_F = Path.GetExtension(FileNamePrimary)

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
    End Sub
    Private Sub BackDrag()
        Dim FileToCopy_B As String
        Dim NewCopy_B As String
        Dim FileName_B As String
        Dim FileExtention_B As String
        Dim Memory_B As Integer = 0



        If FileNamePrimary IsNot Nothing Then

            FileToCopy_B = FileNamePrimary
            FileName_B = Path.GetFileName(FileNamePrimary)
            FileExtention_B = Path.GetExtension(FileNamePrimary)

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

    Private Sub Currency_NameComboBox_TextChanged(sender As Object, e As EventArgs) Handles Currency_NameComboBox.TextChanged
        CheckEnteries()

        Try
            If Currency_NameComboBox.Text = "AUD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "GBP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
            ElseIf Currency_NameComboBox.Text = "EUR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "JPY" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "CHF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "USD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("100,000")
            ElseIf Currency_NameComboBox.Text = "AFN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "ALL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "DZD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "AOA" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")

            ElseIf Currency_NameComboBox.Text = "ARS" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "AMD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
            ElseIf Currency_NameComboBox.Text = "AWG" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "AUD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "ATS" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "BEF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
            ElseIf Currency_NameComboBox.Text = "AZN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "BSD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1/2")
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("3")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "BHD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "BDT" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "BBD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "BYR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "BZD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "BMD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "BTN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "BOB" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "BAM" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "BWP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "BRL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "GBP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "BND" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "BGN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
            ElseIf Currency_NameComboBox.Text = "BIF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "XOF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "XAF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "XPF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "KHR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
                DenominationComboBox.Items.Add("100,000")
            ElseIf Currency_NameComboBox.Text = "CAD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "CVE" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("2,500")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "KYD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("40")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "CLP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
            ElseIf Currency_NameComboBox.Text = "CNY" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "COP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
                DenominationComboBox.Items.Add("100,000")
            ElseIf Currency_NameComboBox.Text = "KMF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("2,500")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "CDF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
            ElseIf Currency_NameComboBox.Text = "CRC" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
            ElseIf Currency_NameComboBox.Text = "HRK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "CUC" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("3")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "CUP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("3")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "CYP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
            ElseIf Currency_NameComboBox.Text = "CZK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "DKK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "DJF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "DOP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
            ElseIf Currency_NameComboBox.Text = "XCD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "EGP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "SVC" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "EEK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "ETB" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "EUR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "FKP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
            ElseIf Currency_NameComboBox.Text = "FIM" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "FJD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "GMD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "GEL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "DMK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1/2")
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "GHS" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "GIP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "GRD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "GTQ" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "GNF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "GYD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "HTG" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("250")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "HNL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "HKD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "HUF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
            ElseIf Currency_NameComboBox.Text = "ISK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "INR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
            ElseIf Currency_NameComboBox.Text = "IDR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
                DenominationComboBox.Items.Add("100,000")
            ElseIf Currency_NameComboBox.Text = "IRR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("100,000")
            ElseIf Currency_NameComboBox.Text = "IQD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("250")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("25,000")
            ElseIf Currency_NameComboBox.Text = "IED" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "ILS" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "ITL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "JMD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "JPY" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "JOD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
            ElseIf Currency_NameComboBox.Text = "KZT" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
            ElseIf Currency_NameComboBox.Text = "KES" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "KWD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1/4")
                DenominationComboBox.Items.Add("1/2")
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
            ElseIf Currency_NameComboBox.Text = "KGS" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "LAK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
            ElseIf Currency_NameComboBox.Text = "LVL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "LBP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
                DenominationComboBox.Items.Add("100,000")
            ElseIf Currency_NameComboBox.Text = "LSL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "LRD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "LYD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1/4")
                DenominationComboBox.Items.Add("1/2")
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
            ElseIf Currency_NameComboBox.Text = "LTL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "LUF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("125")
            ElseIf Currency_NameComboBox.Text = "MOP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "MKD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "MGA" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
            ElseIf Currency_NameComboBox.Text = "MWK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
            ElseIf Currency_NameComboBox.Text = "MYR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "MVR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "MTL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
            ElseIf Currency_NameComboBox.Text = "MRO" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "MUR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("0.05")
                DenominationComboBox.Items.Add("0.2")
                DenominationComboBox.Items.Add("0.5")
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
            ElseIf Currency_NameComboBox.Text = "MXN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "MDL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "MNT" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("3")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "MAD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "MZN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "MMK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "ANG" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2 1/2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("250")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "NAD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "NPR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "NLG" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "NZD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "NIO" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "NGN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "KPW" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "NOK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "OMR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1/2")
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "PKR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "PAB" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
            ElseIf Currency_NameComboBox.Text = "PGK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
            ElseIf Currency_NameComboBox.Text = "PYG" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
                DenominationComboBox.Items.Add("100,000")
            ElseIf Currency_NameComboBox.Text = "PEN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "PHP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "PLN" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")

            ElseIf Currency_NameComboBox.Text = "PTE" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "QAR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "RON" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "RUB" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "RWF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "WST" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "STD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("250")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
                DenominationComboBox.Items.Add("100,000")
            ElseIf Currency_NameComboBox.Text = "SAR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "RSD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "SCR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "SLL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "SGD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "SKK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "SIT" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "SBD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("40")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "SOS" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "ZAR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "KRW" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "ESP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "LKR" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
            ElseIf Currency_NameComboBox.Text = "SHP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("40")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "SDG" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
            ElseIf Currency_NameComboBox.Text = "SRD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("25")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "SZL" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "SEK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "CHF" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "SYP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
            ElseIf Currency_NameComboBox.Text = "TWD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
            ElseIf Currency_NameComboBox.Text = "TZS" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "THB" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "TOP" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
            ElseIf Currency_NameComboBox.Text = "TTD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "TND" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("30")
                DenominationComboBox.Items.Add("50")
            ElseIf Currency_NameComboBox.Text = "TRY" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
            ElseIf Currency_NameComboBox.Text = "TMM" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
            ElseIf Currency_NameComboBox.Text = "USD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
            ElseIf Currency_NameComboBox.Text = "UGX" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
            ElseIf Currency_NameComboBox.Text = "UAH" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("2")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "UYU" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
            ElseIf Currency_NameComboBox.Text = "AED" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("20")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "VUV" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
            ElseIf Currency_NameComboBox.Text = "VEB" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
            ElseIf Currency_NameComboBox.Text = "VND" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("2,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
                DenominationComboBox.Items.Add("200,000")
                DenominationComboBox.Items.Add("500,000")
            ElseIf Currency_NameComboBox.Text = "YER" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("200")
                DenominationComboBox.Items.Add("250")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
            ElseIf Currency_NameComboBox.Text = "ZMK" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("5,000")
                DenominationComboBox.Items.Add("10,000")
                DenominationComboBox.Items.Add("20,000")
                DenominationComboBox.Items.Add("50,000")
            ElseIf Currency_NameComboBox.Text = "ZWD" Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
                DenominationComboBox.Items.Add("1")
                DenominationComboBox.Items.Add("5")
                DenominationComboBox.Items.Add("10")
                DenominationComboBox.Items.Add("50")
                DenominationComboBox.Items.Add("100")
                DenominationComboBox.Items.Add("500")
                DenominationComboBox.Items.Add("1,000")
                DenominationComboBox.Items.Add("1,000,000,000,000")
            ElseIf Currency_NameComboBox.Text = Nothing Then
                DenominationComboBox.Text = Nothing
                DenominationComboBox.Items.Clear()
            End If
        Catch ex As Exception
            DenominationComboBox.Text = Nothing
            DenominationComboBox.Items.Clear()
        End Try
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

    Private Sub BindingNavigatorDeleteItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorDeleteItem.Click
        If Val(IDTextBox.Text) < 0 Then
            MsgBox("The record which you are trying to delete is not saved, first save it to continue the action", vbOKOnly + vbCritical)

        Else
            Dim cn As New OleDbConnection("Provider=microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Notes.accdb")
            Dim cmd As OleDbCommand
            Dim da As OleDbDataAdapter
            Dim dt As DataTable

            cmd = New OleDbCommand("DELETE FROM [Table] WHERE ID=" & Val(IDTextBox.Text) & ";", cn)
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()

            Me.Validate()
            Me.TableBindingSource.EndEdit()

            NotesDataSet.AcceptChanges()

            Me.TableAdapterManager.UpdateAll(Me.NotesDataSet)
            Me.TableTableAdapter.Update(NotesDataSet.Table)
            MsgBox("The record has been deleted ", vbOKOnly + vbExclamation)

            RefreshDatabase()
        End If
        ActiveControl = ContinentComboBox

    End Sub

    Private Sub frmNotes_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Control + Keys.S Then
            MsgBox("ok")
        End If
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

    Private Sub TableDataGridView_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView.CellEnter
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
End Class