Imports System.IO.Compression
Imports System.IO

Public Class frmBackupRestore
    Dim na As String = ""
    Private Sub frmBackupRestore_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        frmDashboard.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If NewRestored = False Then
            PutPresentFilesBack()

        End If
        frmDashboard.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Try

            With OpenFileDialog1

                .Filter = "Numistmatics Database Backup Folder (.NDB)|*.ndb"
                .FileName = ""
                .Title = "Choose Frontal Image of the note ..."
                .AddExtension = True
                .FilterIndex = 1
                .Multiselect = False
                .ValidateNames = True
                .InitialDirectory = Application.StartupPath
                .RestoreDirectory = True

                If (.ShowDialog = DialogResult.OK) Then
                    TextBox2.Text = (OpenFileDialog1.FileName)
                Else
                    Return
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Dim ZIPFileOfficialName As String = "Numistmatics(backup).ndb" 'here .ndb stands for Numistmatic database backup file
    Dim wrongpassword As Integer = 0
    Dim wrongpassword1 As Integer = 3

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim PasswordVerification As String

        Try

            If TextBox1.Text = Nothing Then
                MsgBox("Please select your backup directory by pressing on " & Button2.Text & " button", vbOKOnly + vbExclamation)
            Else
                PasswordVerification = InputBox("Please enter your password", "User Verification")

                If PasswordVerification = Nothing Then
                    MsgBox("The password field can't be empty. ", vbOKOnly + vbCritical)

                Else
                    If (PasswordVerification = My.Settings.key) Then

                        Dim CopyTemporyFileName As String = "temp"

                        pbCustomBackup.Value = 1

                        If Directory.Exists(CopyTemporyFileName) Then
                            Directory.Delete(CopyTemporyFileName, True)
                            Directory.CreateDirectory(CopyTemporyFileName)
                        Else
                            Directory.CreateDirectory(CopyTemporyFileName)
                        End If

                        pbCustomBackup.Value = 2
                        System.IO.File.Copy(Application.StartupPath + "\Coins.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Coins.accdb")
                        System.IO.File.Copy(Application.StartupPath + "\Notes.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Notes.accdb")
                        System.IO.File.Copy(Application.StartupPath + "\Currency_Short_Names.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Currency_Short_Names.accdb")

                        pbCustomBackup.Value = 3
                        If Directory.Exists(Application.StartupPath + "\Resources") Then
                            My.Computer.FileSystem.CopyDirectory(Application.StartupPath + "\Resources", Application.StartupPath + "\" + CopyTemporyFileName + "\Resources", True)
                        End If

                        pbCustomBackup.Value = 4

                        Dim SourcePath As String = Application.StartupPath + "\" + CopyTemporyFileName
                        Dim ZipPath As String
                        Dim memory As Integer = 0
                        Dim ZIPFileOfficialNameS As String = "Numistmatics(backup)"
                        Dim ZIPFileOfficialNameExtension As String = ".ndb"

                        pbCustomBackup.Value = 5

                        If File.Exists(TextBox1.Text & "\" & ZIPFileOfficialName) Then
                            memory = 0

                            Do While File.Exists(TextBox1.Text & "\" & ZIPFileOfficialNameS & "_" & memory & ZIPFileOfficialNameExtension)
                                memory = memory + 1

                            Loop

                            pbCustomBackup.Value = 6
                            ZipPath = TextBox1.Text & "\" & ZIPFileOfficialNameS & "_" & memory & ZIPFileOfficialNameExtension
                            ZipFile.CreateFromDirectory(SourcePath, ZipPath)

                        Else
                            pbCustomBackup.Value = 6
                            memory = 0
                            ZipPath = TextBox1.Text & "\Numistmatics(backup).ndb"
                            ZipFile.CreateFromDirectory(SourcePath, ZipPath)
                        End If


                        pbCustomBackup.Value = 7
                        If Directory.Exists(CopyTemporyFileName) Then
                            Directory.Delete(CopyTemporyFileName, True)
                        End If

                        pbCustomBackup.Value = 0
                        MsgBox("Backup has been taken of all the databases", vbOKOnly + vbInformation)
                    Else
                        If wrongpassword <= 3 And wrongpassword1 > 0 Then
                            wrongpassword = wrongpassword + 1
                            wrongpassword1 = wrongpassword1 - 1

                            If wrongpassword1 = 0 Then
                                MsgBox("Your access to this program is denied", vbOKOnly + vbCritical)
                                frmDashboard.Button1.Enabled = False
                                System.Windows.Forms.Application.Exit()
                            End If

                            MsgBox("Password is incorrect.", vbOKOnly + vbCritical)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox("Failed to take backup database due to " & ex.Message)
        End Try
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim PasswordVerification As String
        Dim CopyTemporyFileName As String = "Existing Backup"
        Dim ExtractionTemporyFileName As String = "Extracted"
        Dim SelectedFile As String = System.IO.Path.GetFileName(TextBox2.Text)

        Try
            If Directory.Exists(CopyTemporyFileName) Then
                Directory.Delete(CopyTemporyFileName, True)
            End If
            If Directory.Exists(ExtractionTemporyFileName) Then
                Directory.Delete(ExtractionTemporyFileName, True)
            End If
            If File.Exists(Application.StartupPath + "\" + SelectedFile) Then
                File.Delete(Application.StartupPath + "\" + SelectedFile)
            End If

            MsgBox("This function will delete existing data", vbOKOnly + vbExclamation)

            If TextBox2.Text = Nothing Then
                MsgBox("Please select your restore database location by pressing on " & Button5.Text & " button", vbOKOnly + vbExclamation)
            Else
                PasswordVerification = InputBox("Please enter your password", "User Verification")

                If PasswordVerification = Nothing Then
                    MsgBox("The password field can't be empty. ", vbOKOnly + vbCritical)

                Else
                    If (PasswordVerification = My.Settings.key) Then

                        pbRestore.Value = 1
                        If Directory.Exists(CopyTemporyFileName) Then
                            Directory.Delete(CopyTemporyFileName, True)
                            Directory.CreateDirectory(CopyTemporyFileName)
                            File.SetAttributes(CopyTemporyFileName, FileAttributes.Hidden)
                        Else
                            Directory.CreateDirectory(CopyTemporyFileName)
                            File.SetAttributes(CopyTemporyFileName, FileAttributes.Hidden)
                        End If

                        pbRestore.Value = 2
                        System.IO.File.Copy(TextBox2.Text, Application.StartupPath + "\" + SelectedFile)

                        pbRestore.Value = 3
                        'moved database files into safe folder

                        Try
                            System.IO.File.Move(Application.StartupPath + "\Coins.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Coins.accdb")
                            System.IO.File.Move(Application.StartupPath + "\Notes.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Notes.accdb")
                            System.IO.File.Move(Application.StartupPath + "\Currency_Short_Names.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Currency_Short_Names.accdb")
                        Catch ex As Exception
                            MsgBox("For this process to run we have to restart your app", vbExclamation + vbOKOnly)
                            Application.Restart()
                        End Try

                        pbRestore.Value = 4
                        If Directory.Exists("Resources") Then
                            My.Computer.FileSystem.MoveDirectory("Resources", Application.StartupPath + "\" + CopyTemporyFileName + "\Resources", True)
                        End If

                        pbRestore.Value = 5
                        If Directory.Exists(ExtractionTemporyFileName) Then
                            Directory.Delete(ExtractionTemporyFileName, True)
                            Directory.CreateDirectory(ExtractionTemporyFileName)
                            File.SetAttributes(ExtractionTemporyFileName, FileAttributes.Hidden)
                        Else
                            Directory.CreateDirectory(ExtractionTemporyFileName)
                            File.SetAttributes(ExtractionTemporyFileName, FileAttributes.Hidden)
                        End If

                        pbRestore.Value = 6
                        Dim ExtractPath As String = Application.StartupPath + "\" + ExtractionTemporyFileName
                        Dim ZipPath As String = Application.StartupPath + "\" + SelectedFile
                        ZipFile.ExtractToDirectory(ZipPath, ExtractPath)

                        pbRestore.Value = 7
                        'listing the names of files
                        ListBox1.Items.Clear()
                        For Each FileName As String In IO.Directory.GetFiles(Application.StartupPath + "\" + ExtractionTemporyFileName)
                            ListBox1.Items.Add(Path.GetFileName(FileName))
                        Next
                        '

                        Dim done As Boolean

                        pbRestore.Value = 8

                        If File.Exists(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Coins.accdb") And File.Exists(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Notes.accdb") And File.Exists(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Currency_Short_Names.accdb") Then
                            pbRestore.Value = 9
                            System.IO.File.Move(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Coins.accdb", Application.StartupPath + "\Coins.accdb")
                            System.IO.File.Move(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Notes.accdb", Application.StartupPath + "\Notes.accdb")
                            System.IO.File.Move(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Currency_Short_Names.accdb", Application.StartupPath + "\Currency_Short_Names.accdb")

                            If Directory.Exists(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Resources") Then
                                My.Computer.FileSystem.CopyDirectory(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Resources", Application.StartupPath + "\Resources", True)
                            End If

                            If Directory.Exists(folderPrimary) Then
                                File.SetAttributes(folderPrimary, FileAttributes.Hidden)
                            Else
                            End If

                            If Directory.Exists(folderPrimary & folderSecondary) Then
                                File.SetAttributes(folderPrimary & folderSecondary, FileAttributes.Hidden)
                            Else
                            End If
                            frmLogin.FileHider()

                            pbRestore.Value = 10
                            done = True

                        Else
                            pbRestore.Value = 9
                            File.Delete(Application.StartupPath + "\" + SelectedFile)
                            System.IO.File.Move(Application.StartupPath + "\" + CopyTemporyFileName + "\Coins.accdb", Application.StartupPath + "\Coins.accdb")
                            System.IO.File.Move(Application.StartupPath + "\" + CopyTemporyFileName + "\Notes.accdb", Application.StartupPath + "\Notes.accdb")
                            System.IO.File.Move(Application.StartupPath + "\" + CopyTemporyFileName + "\Currency_Short_Names.accdb", Application.StartupPath + "\Currency_Short_Names.accdb")

                            If Directory.Exists(Application.StartupPath + "\" + CopyTemporyFileName + "\Resources") Then
                                My.Computer.FileSystem.MoveDirectory(Application.StartupPath + "\" + CopyTemporyFileName + "\Resources", Application.StartupPath + "\Resources", True)
                            End If

                            If Directory.Exists(folderPrimary) Then
                                File.SetAttributes(folderPrimary, FileAttributes.Hidden)
                            Else
                            End If

                            If Directory.Exists(folderPrimary & folderSecondary) Then
                                File.SetAttributes(folderPrimary & folderSecondary, FileAttributes.Hidden)
                            Else
                            End If

                            ' frmLogin.FileHider()

                            If Directory.Exists(CopyTemporyFileName) Then
                                Directory.Delete(CopyTemporyFileName, True)
                            End If

                            'deleting unwanted files
                            Dim index As Integer = 0
                            index = 0
                            ListBox1.SelectedIndex = index

                            Do While File.Exists(Application.StartupPath + "\" + ListBox1.SelectedItem)
                                System.IO.File.Delete(Application.StartupPath + "\" + ListBox1.SelectedItem)
                                If index = ListBox1.Items.Count Then
                                    Exit Do
                                Else
                                    index = index + 1
                                End If
                            Loop

                            done = False
                            pbRestore.Value = 10
                            MsgBox("This file is not the official backup file of Numistmatics so it is not supported. Please select the file named " & ZIPFileOfficialName & " or the file associated with this application", vbOKOnly + vbCritical)

                        End If

                        If done = True Then
                            pbRestore.Value = 0
                            LinkLabel1.Visible = True

                            If Directory.Exists(CopyTemporyFileName) Then
                                Directory.Delete(CopyTemporyFileName, True)
                            End If

                            MsgBox("Restoration of database has been done. Please press refresh button in Notes or Coins form", vbOKOnly + vbInformation)
                        Else
                            pbRestore.Value = 0
                            LinkLabel1.Visible = False
                            MsgBox("Restoration was failed, your previous database has been restored", vbOKOnly + vbCritical)
                        End If

                    Else
                        If wrongpassword <= 3 And wrongpassword1 > 0 Then
                            wrongpassword = wrongpassword + 1
                            wrongpassword1 = wrongpassword1 - 1

                            If wrongpassword1 = 0 Then
                                MsgBox("Your access to this program is denied", vbOKOnly + vbCritical)
                                frmDashboard.Button1.Enabled = False
                                System.Windows.Forms.Application.Exit()
                            End If

                            MsgBox("Password is incorrect.", vbOKOnly + vbCritical)

                        End If
                    End If
                End If
            End If

            If Directory.Exists(CopyTemporyFileName) Then
                Directory.Delete(CopyTemporyFileName, True)
            End If
            If Directory.Exists(ExtractionTemporyFileName) Then
                Directory.Delete(ExtractionTemporyFileName, True)
            End If
            If File.Exists(Application.StartupPath + "\" + SelectedFile) Then
                File.Delete(Application.StartupPath + "\" + SelectedFile)
            End If

        Catch ex As Exception
            MsgBox("Failed to take restore database due to following reason : " & ex.Message & "")

            File.Delete(Application.StartupPath + "\" + SelectedFile)

            If File.Exists(Application.StartupPath + "\" + CopyTemporyFileName + "\Coins.accdb") Then
                System.IO.File.Move(Application.StartupPath + "\" + CopyTemporyFileName + "\Coins.accdb", Application.StartupPath + "\Coins.accdb")
            End If
            If File.Exists(Application.StartupPath + "\" + CopyTemporyFileName + "\Notes.accdb") Then
                System.IO.File.Move(Application.StartupPath + "\" + CopyTemporyFileName + "\Notes.accdb", Application.StartupPath + "\Notes.accdb")
            End If
            If File.Exists(Application.StartupPath + "\" + CopyTemporyFileName + "\Currency_Short_Names.accdb") Then
                System.IO.File.Move(Application.StartupPath + "\" + CopyTemporyFileName + "\Currency_Short_Names.accdb", Application.StartupPath + "\Currency_Short_Names.accdb")
            End If
            If Directory.Exists(Application.StartupPath + "\" + CopyTemporyFileName + "\Resources") Then
                My.Computer.FileSystem.MoveDirectory(Application.StartupPath + "\" + CopyTemporyFileName + "\Resources", Application.StartupPath + "\Resources", True)
            End If

            If Directory.Exists(folderPrimary) Then
                File.SetAttributes(folderPrimary, FileAttributes.Hidden)
            Else
            End If

            If Directory.Exists(folderPrimary & folderSecondary) Then
                File.SetAttributes(folderPrimary & folderSecondary, FileAttributes.Hidden)
            Else
            End If

            ' frmLogin.FileHider()

            If Directory.Exists(CopyTemporyFileName) Then
                Directory.Delete(CopyTemporyFileName, True)
            End If

            'deleting unwanted files
            Dim index As Integer = 0
            index = 0
            ListBox1.SelectedIndex = index

            Do While File.Exists(Application.StartupPath + "\" + ListBox1.SelectedItem)
                System.IO.File.Delete(Application.StartupPath + "\" + ListBox1.SelectedItem)
                If index = ListBox1.Items.Count Then
                    Exit Do
                Else
                    index = index + 1
                End If
            Loop

            Do While File.Exists(Application.StartupPath + "\" + ListBox1.SelectedItem)
                System.IO.File.Delete(Application.StartupPath + "\" + ListBox1.SelectedItem)
                If index = ListBox1.Items.Count Then
                    Exit Do
                Else
                    index = index + 1
                End If
            Loop
        End Try

    End Sub

    Dim folderPrimary As String = ("Resources")
    Dim folderSecondary As String = ("\Images")

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim PasswordVerification As String

        Try
            If Directory.Exists(Application.StartupPath + "\Reset Backup") Then
                MsgBox("This function will delete existing data", vbOKOnly + vbExclamation)
                PasswordVerification = InputBox("Please enter your password", "User Verification")

                If PasswordVerification = Nothing Then
                    MsgBox("The password field can't be empty. ", vbOKOnly + vbCritical)
                Else
                    If (PasswordVerification = My.Settings.key) Then

                        If File.Exists(Application.StartupPath + "\Reset Backup\Coins.accdb") And File.Exists(Application.StartupPath + "\Reset Backup\Notes.accdb") And File.Exists(Application.StartupPath + "\Reset Backup\Currency_Short_Names.accdb") Then
                            pbReset.Value = 1
                            File.SetAttributes(Application.StartupPath + "\Coins.accdb", FileAttributes.Normal)
                            File.SetAttributes(Application.StartupPath + "\Notes.accdb", FileAttributes.Normal)
                            File.SetAttributes(Application.StartupPath + "\Currency_Short_Names.accdb", FileAttributes.Normal)

                            pbReset.Value = 2
                            System.IO.File.Delete(Application.StartupPath + "\Coins.accdb")
                            System.IO.File.Delete(Application.StartupPath + "\Notes.accdb")
                            System.IO.File.Delete(Application.StartupPath + "\Currency_Short_Names.accdb")

                            pbReset.Value = 3
                            System.IO.File.Copy(Application.StartupPath + "\Reset Backup\Coins.accdb", Application.StartupPath + "\Coins.accdb")
                            System.IO.File.Copy(Application.StartupPath + "\Reset Backup\Notes.accdb", Application.StartupPath + "\Notes.accdb")
                            System.IO.File.Copy(Application.StartupPath + "\Reset Backup\Currency_Short_Names.accdb", Application.StartupPath + "\Currency_Short_Names.accdb")

                            pbReset.Value = 4
                            If Directory.Exists(folderPrimary & folderSecondary) Then
                                Directory.Delete(folderPrimary & folderSecondary, True)
                            End If
                            If Directory.Exists(folderPrimary) Then
                                Directory.Delete(folderPrimary, True)
                            End If

                            pbReset.Value = 0
                            MsgBox("The database has been reset. Please press refresh button in Notes or Coins form", vbOKOnly + vbInformation)
                        End If
                    Else
                        MsgBox("Password is incorrect.", vbOKOnly + vbCritical)

                    End If
                End If
            Else
                MsgBox("There is a problem in restoring database function, please contact to the developer", vbOKOnly + vbCritical)
            End If

        Catch ex As Exception
            MsgBox("For this process to run we have to restart your app", vbExclamation + vbOKOnly)
            Application.Restart()
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        frmDatabase.Show()

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        LinkLabel1.Visible = False
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            If File.Exists(Application.StartupPath & "\Coins.accdb") Then
                lblCoins.ForeColor = Color.Green
                lblCoins.Text = "Connection Established"
            Else
                lblCoins.ForeColor = Color.Red
                lblCoins.Text = "Connection Failed"
            End If
            If File.Exists(Application.StartupPath & "\Notes.accdb") Then
                lblNotes.ForeColor = Color.Green
                lblNotes.Text = "Connection Established"
            Else
                lblNotes.ForeColor = Color.Red
                lblNotes.Text = "Connection Failed"
            End If
            If File.Exists(Application.StartupPath & "\Currency_Short_Names.accdb") Then
                lblCurrency.ForeColor = Color.Green
                lblCurrency.Text = "Connection Established"
            Else
                lblCurrency.ForeColor = Color.Red
                lblCurrency.Text = "Connection Failed"
            End If
        Catch ex As Exception
            lblCoins.Text = "--"
            lblNotes.Text = "--"
            lblCurrency.Text = "--"

            lblCoins.ForeColor = Color.Black
            lblNotes.ForeColor = Color.Black
            lblCurrency.ForeColor = Color.Black
        End Try

        If lblCurrency.Text = "Connection Established" And lblNotes.Text = "Connection Established" And lblCoins.Text = "Connection Established" Then
            MsgBox("All database are connected", vbOKOnly + vbInformation)
        ElseIf lblCurrency.Text = "Connection Failed" And lblNotes.Text = "Connection Failed" And lblCoins.Text = "Connection Failed" Then
            MsgBox("All database are disconnected. You need to reset or restore them.", vbOKOnly + vbCritical)
        End If

        If lblCurrency.Text = "Connection Failed" Or lblNotes.Text = "Connection Failed" Or lblCoins.Text = "Connection Failed" Then
            Button7.Enabled = True
        Else
            Button7.Enabled = False
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim PasswordVerification As String

        PasswordVerification = InputBox("Please enter your password", "User Verification")

        If PasswordVerification = Nothing Then
            MsgBox("The password field can't be empty. ", vbOKOnly + vbCritical)
        Else
            If PasswordVerification = My.Settings.key Then
                If File.Exists(Application.StartupPath & "\Currency_Short_Names.accdb") Then
                Else
                    System.IO.File.Copy(Application.StartupPath + "\Reset Backup\Currency_Short_Names.accdb", Application.StartupPath + "\Currency_Short_Names.accdb")

                    If File.Exists(Application.StartupPath & "\Currency_Short_Names.accdb") Then
                        lblCurrency.ForeColor = Color.Green
                        lblCurrency.Text = "Connection Established"
                    Else
                        lblCurrency.ForeColor = Color.Red
                        lblCurrency.Text = "Connection Failed"
                    End If

                    MsgBox("The database has been repaired", vbOKOnly + vbExclamation)

                End If
            Else
                MsgBox("Password is incorrect.", vbOKOnly + vbCritical)
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBF.SelectedIndexChanged
        If cmbBF.SelectedIndex = 2 Then
            Label5.Visible = True
            Label6.Visible = False
        ElseIf cmbBF.SelectedIndex = 1 Then
            Label5.Visible = False
            Label6.Visible = True
        ElseIf cmbBF.SelectedIndex = 0 Then
            Label5.Visible = False
            Label6.Visible = False
        End If

        If cmbBF.SelectedIndex = My.Settings.BackupFrequency Then
            Button9.Enabled = False
        Else
            Button9.Enabled = True
        End If
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim PasswordVerification As String

        PasswordVerification = InputBox("Please enter your password", "User Verification")

        If (PasswordVerification = My.Settings.key) Then
            My.Settings.BackupFrequency = cmbBF.SelectedIndex
            My.Settings.Save()

            MsgBox("Setting saved", vbOKOnly + vbInformation)
            Button9.Enabled = False
        ElseIf (PasswordVerification = Nothing) Then
            MsgBox("The password field can't be empty. ", vbOKOnly + vbCritical)
        Else
            MsgBox("Password is incorrect.", vbOKOnly + vbCritical)
        End If

    End Sub

    Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl2.SelectedIndexChanged

    End Sub

    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl.SelectedIndexChanged

    End Sub

    Private Sub TableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.TableBindingSource.EndEdit()
        Me.TableAdapterManager1.UpdateAll(Me.CoinsDataSet)

    End Sub

    Dim Front As String
    Dim Back As String
    Dim folder As String = ("Resources\Images")



    Private Sub frmBackupRestore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'NotesDataSet.Table' table. You can move, or remove it, as needed.
        Me.TableTableAdapter1.Fill(Me.NotesDataSet.Table)
        'TODO: This line of code loads data into the 'CoinsDataSet.Table' table. You can move, or remove it, as needed.
        Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
        cmbBF.SelectedIndex = My.Settings.BackupFrequency

        AutoBackupRegistoryLister()


    End Sub

    Dim a As String = Application.StartupPath + "\" + "Auto Backup"

    Private Sub AutoBackupRegistoryLister()

        tbcAutoBRestore.Visible = False
        butABRestore.Enabled = False
        lbFileName.SelectedItem = Nothing

        For Each DirectoryName As String In System.IO.Directory.GetDirectories(a)
            lbFileName.Items.Add(Path.GetFileName(DirectoryName))

        Next
    End Sub

    Private Sub lbFileName_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lbFileName.MouseDoubleClick


        SelectedBackupLoader()

    End Sub

    Dim presentDataSaveFolder As String = Application.StartupPath + "/presentdatabase"
    Private Sub SelectedBackupLoader()
        Try

            Dim ExtractionTemporyFileName As String = "Extracted"

            If Directory.Exists(ExtractionTemporyFileName) Then
                Directory.Delete(ExtractionTemporyFileName, True)
                Directory.CreateDirectory(ExtractionTemporyFileName)
                File.SetAttributes(ExtractionTemporyFileName, FileAttributes.Hidden)
            Else
                Directory.CreateDirectory(ExtractionTemporyFileName)
                File.SetAttributes(ExtractionTemporyFileName, FileAttributes.Hidden)
            End If

            Dim ExtractionPath As String = Application.StartupPath + "\" + ExtractionTemporyFileName
            Dim ZipPath As String = a + "\" + lbFileName.SelectedItem.ToString + "\Numistmatics(backup).ndb"
            ZipFile.ExtractToDirectory(ZipPath, ExtractionPath)

            If File.Exists(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Coins.accdb") And File.Exists(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Notes.accdb") And File.Exists(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Currency_Short_Names.accdb") Then

                If File.Exists(Application.StartupPath + "\Coins.accdb") Then
                    File.Delete(Application.StartupPath + "\Coins.accdb")
                End If
                System.IO.File.Move(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Coins.accdb", Application.StartupPath + "\Coins.accdb")

                If File.Exists(Application.StartupPath + "\Notes.accdb") Then
                    File.Delete(Application.StartupPath + "\Notes.accdb")
                End If
                System.IO.File.Move(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Notes.accdb", Application.StartupPath + "\Notes.accdb")

                If File.Exists(Application.StartupPath + "\Currency_Short_Names.accdb") Then
                    File.Delete(Application.StartupPath + "\Currency_Short_Names.accdb")
                End If
                System.IO.File.Move(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Currency_Short_Names.accdb", Application.StartupPath + "\Currency_Short_Names.accdb")


                If Directory.Exists(Application.StartupPath + "\Resources") Then
                    Directory.Delete(Application.StartupPath + "\Resources", True)
                End If
                If Directory.Exists(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Resources") Then
                    My.Computer.FileSystem.MoveDirectory(Application.StartupPath + "\" + ExtractionTemporyFileName + "\Resources", Application.StartupPath + "\Resources", True)
                End If


                If Directory.Exists(folderPrimary) Then
                    File.SetAttributes(folderPrimary, FileAttributes.Hidden)
                Else
                End If

                    If Directory.Exists(folderPrimary & folderSecondary) Then
                        File.SetAttributes(folderPrimary & folderSecondary, FileAttributes.Hidden)
                    Else
                    End If

                    RefreshDatabasePresentor()

                    butABRestore.Enabled = True
                Else
                    tbcAutoBRestore.Visible = False
                butABRestore.Enabled = False
                MsgBox("The selected date backup does ot contain data files")

                PutPresentFilesBack()
            End If

            If Directory.Exists(ExtractionTemporyFileName) Then
                Directory.Delete(ExtractionTemporyFileName, True)
            End If
        Catch ex As Exception
            tbcAutoBRestore.Visible = False
            butABRestore.Enabled = False
            MsgBox("Failed to restore database due to following reason : " & ex.Message & "")

            PutPresentFilesBack()
        End Try

    End Sub

    Private Sub PutPresentFilesBack()
        If File.Exists(Application.StartupPath + "\Coins.accdb") Then
            File.Delete(Application.StartupPath + "\Coins.accdb")
        End If
        If File.Exists(Application.StartupPath + "\Notes.accdb") Then
            File.Delete(Application.StartupPath + "\Notes.accdb")
        End If
        If File.Exists(Application.StartupPath + "\Currency_Short_Names.accdb") Then
            File.Delete(Application.StartupPath + "\Currency_Short_Names.accdb")
        End If
        If Directory.Exists(Application.StartupPath + "\Resources") Then
            Directory.Delete(Application.StartupPath + "\Resources", True)

        End If

        System.IO.File.Move(presentDataSaveFolder + "\Coins.accdb", Application.StartupPath + "\Coins.accdb")
        System.IO.File.Move(presentDataSaveFolder + "\Notes.accdb", Application.StartupPath + "\Notes.accdb")
        System.IO.File.Move(presentDataSaveFolder + "\Currency_Short_Names.accdb", Application.StartupPath + "\Currency_Short_Names.accdb")

        If Directory.Exists(presentDataSaveFolder + "\Resources") Then
            My.Computer.FileSystem.MoveDirectory(presentDataSaveFolder + "\Resources", Application.StartupPath + "\Resources", True)
        End If

        If Directory.Exists(presentDataSaveFolder) Then
            Directory.Delete(presentDataSaveFolder, True)
        End If
    End Sub
    Private Sub RefreshDatabasePresentor()
        tbcAutoBRestore.Visible = True

        Try
            Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
            Me.TableTableAdapter1.Fill(Me.NotesDataSet.Table)
        Catch ex As Exception
            MsgBox("Unable to load the database due to following reason : " & ex.Message & "", vbOKOnly + vbCritical)

        End Try
    End Sub
    Dim NewRestored As Boolean
    Private Sub butABRestore_Click(sender As Object, e As EventArgs) Handles butABRestore.Click
        Dim PasswordVerification As String
        PasswordVerification = InputBox("Please enter your password", "User Verification")

        If PasswordVerification = Nothing Then
            MsgBox("The password field can't be empty. ", vbOKOnly + vbCritical)
            NewRestored = False
        ElseIf PasswordVerification = My.Settings.key Then
            If Directory.Exists(presentDataSaveFolder) Then
                Directory.Delete(presentDataSaveFolder, True)
            End If
            NewRestored = True
            MsgBox("Database Restored")
        Else
            If wrongpassword <= 3 And wrongpassword1 > 0 Then
                wrongpassword = wrongpassword + 1
                wrongpassword1 = wrongpassword1 - 1

                If wrongpassword1 = 0 Then
                    butABRestore.Enabled = False
                    MsgBox("Your access to this program is denied", vbOKOnly + vbCritical)
                    frmDashboard.Button1.Enabled = False
                    System.Windows.Forms.Application.Exit()
                End If

                MsgBox("Password is incorrect.", vbOKOnly + vbCritical)
                NewRestored = False
            End If
        End If
    End Sub

    Private Sub SavePresentFiles()
        'conserving present data files (start)

        If Directory.Exists(presentDataSaveFolder) Then
            Directory.Delete(presentDataSaveFolder, True)
            Directory.CreateDirectory(presentDataSaveFolder)
            File.SetAttributes(presentDataSaveFolder, FileAttributes.Hidden)
        Else
            Directory.CreateDirectory(presentDataSaveFolder)
            File.SetAttributes(presentDataSaveFolder, FileAttributes.Hidden)
        End If

        System.IO.File.Move(Application.StartupPath + "\Coins.accdb", presentDataSaveFolder + "\Coins.accdb")
        System.IO.File.Move(Application.StartupPath + "\Notes.accdb", presentDataSaveFolder + "\Notes.accdb")
        System.IO.File.Move(Application.StartupPath + "\Currency_Short_Names.accdb", presentDataSaveFolder + "\Currency_Short_Names.accdb")

        If Directory.Exists(Application.StartupPath + "\Resources") Then
            My.Computer.FileSystem.MoveDirectory(Application.StartupPath + "\Resources", presentDataSaveFolder + "\Resources", True)
        End If

        'conserving present data files (end)
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            SavePresentFiles()
            lbFileName.Enabled = True
            Button10.Enabled = False
            butABRestore.Enabled = False

        Catch ex As Exception
            MsgBox("There is a problem in achiving this task pls restart the Numistmatics app")
            lbFileName.Enabled = False
            Button10.Enabled = True
            butABRestore.Enabled = False
        End Try
    End Sub

    Private Sub TableDataGridView1_CellEnter_1(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView1.CellEnter

    End Sub

End Class