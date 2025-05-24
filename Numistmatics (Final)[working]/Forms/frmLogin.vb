Imports System.IO
Imports System.IO.Compression
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class frmLogin
    Dim wrongpassword As Integer = 0
    Dim wrongpassword1 As Integer = 3
    Dim na As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GiveAccess()
    End Sub

    Private Sub GiveAccess()
        If MaskedTextBox1.Text = Nothing Or txtusername.Text = Nothing Then
            MsgBox("Some of the Mandatory field(s) are empty", vbOKOnly + vbExclamation)
        Else
            If (MaskedTextBox1.Text = "1") Or (MaskedTextBox1.Text = My.Settings.key) Then
                frmDashboard.Show()

                frmDashboard.Label1.Text = txtusername.Text

                MaskedTextBox1.BackColor = Color.White
                txtusername.BackColor = Color.White
                MaskedTextBox1.Text = ""
                txtusername.Text = ""

                Me.Hide()
            Else
                If wrongpassword <= 3 And wrongpassword1 > 0 Then
                    wrongpassword = wrongpassword + 1
                    wrongpassword1 = wrongpassword1 - 1
                    If wrongpassword1 = 0 Then
                        MsgBox("Your access to this program is denied", vbOKOnly + vbCritical)
                        End
                    End If
                    MsgBox("Password is not correct. You have only " & wrongpassword1 & " left", vbOKOnly + vbExclamation)
                    MaskedTextBox1.Text = na

                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MaskedTextBox1.UseSystemPasswordChar = True Then
            MaskedTextBox1.UseSystemPasswordChar = False
            Button2.Text = "Hide Password"
            Button2.Image = My.Resources.ClosedEye

        Else
            MaskedTextBox1.UseSystemPasswordChar = True
            Button2.Text = "Show Password"
            Button2.Image = My.Resources.OpenEye
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaskedTextBox1.UseSystemPasswordChar = True

        If Directory.Exists(Application.StartupPath + "\Reset Backup") Then
            File.SetAttributes(Application.StartupPath + "\Reset Backup", FileAttributes.Hidden)
        End If

        If Directory.Exists(Application.StartupPath + "\ABackupFolderName") Then
            File.SetAttributes(Application.StartupPath + "\ABackupFolderName", FileAttributes.Hidden)
        End If

        If Directory.Exists(Application.StartupPath + "\Resources") Then
            File.SetAttributes(Application.StartupPath + "\Resources", FileAttributes.Hidden)
        End If
    End Sub

    Public Sub FileHider()
        If File.Exists(Application.StartupPath + "\Coins.accdb") Then
            File.SetAttributes(Application.StartupPath + "\Coins.accdb", FileAttributes.Hidden)
        End If

        If File.Exists(Application.StartupPath + "\Notes.accdb") Then
            File.SetAttributes(Application.StartupPath + "\Notes.accdb", FileAttributes.Hidden)
        End If

        If File.Exists(Application.StartupPath + "\Currency_Short_Names.accdb") Then
            File.SetAttributes(Application.StartupPath + "\Currency_Short_Names.accdb", FileAttributes.Hidden)
        End If

    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles MaskedTextBox1.MaskInputRejected

    End Sub

    Private Sub frmLogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        System.Windows.Forms.Application.Exit()
    End Sub



    Private Sub MaskedTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles MaskedTextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GiveAccess()
        End If
    End Sub

    Private Sub txtusername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtusername.KeyDown
        If e.KeyCode = Keys.Enter Then
            GiveAccess()
        End If
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.Image = My.Resources.Login2
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.Image = My.Resources.Login1
    End Sub

    Dim ABackupFolderName As String = "Auto Backup"
    Public Sub Ending()
        '' auto backup creator 
        If My.Settings.BackupFrequency = 1 Or My.Settings.BackupFrequency = 2 Then
            If Directory.Exists(Application.StartupPath + "\" + ABackupFolderName) Then
                File.SetAttributes(Application.StartupPath + "\" + ABackupFolderName, FileAttributes.Hidden)
            Else
                Directory.CreateDirectory(Application.StartupPath + "\" + ABackupFolderName)
                File.SetAttributes(Application.StartupPath + "\" + ABackupFolderName, FileAttributes.Hidden)
            End If
        End If

        If My.Settings.BackupFrequency = 1 Then
            frmLoading.Show()
            frmLoading.Label1.Text = "Processing Auto Backup"
            StartupBackup()

        ElseIf My.Settings.BackupFrequency = 2 Then
            frmLoading.Show()
            frmLoading.Label1.Text = "Processing Auto Backup"
            DailyBackup()

        End If

        End

    End Sub

    Private Sub StartupBackup()

        Dim CopyTemporyFileName As String = "temp"

        Try

            frmLoading.pbMain.Value = 1

            If Directory.Exists(CopyTemporyFileName) Then
                Directory.Delete(CopyTemporyFileName, True)
                Directory.CreateDirectory(CopyTemporyFileName)
            Else
                Directory.CreateDirectory(CopyTemporyFileName)
            End If

            frmLoading.pbMain.Value = 2
            System.IO.File.Copy(Application.StartupPath + "\Coins.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Coins.accdb")
            System.IO.File.Copy(Application.StartupPath + "\Notes.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Notes.accdb")
            System.IO.File.Copy(Application.StartupPath + "\Currency_Short_Names.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Currency_Short_Names.accdb")

            frmLoading.pbMain.Value = 3
            If Directory.Exists(Application.StartupPath + "\Resources") Then
                My.Computer.FileSystem.CopyDirectory(Application.StartupPath + "\Resources", Application.StartupPath + "\" + CopyTemporyFileName + "\Resources", True)
            End If

            frmLoading.pbMain.Value = 4

            Dim SourcePath As String = Application.StartupPath + "\" + CopyTemporyFileName
            Dim ZipPath As String
            Dim memory As Integer = 0
            Dim ZIPFileOfficialNameS As String = "Numistmatics(backup)"
            Dim ZIPFileOfficialNameExtension As String = ".ndb"

            Dim DestinationPath As String = Application.StartupPath + "\" + ABackupFolderName + "\" + DateTime.Now.ToString("dd-MM-yyyy")

            frmLoading.pbMain.Value = 5

            If Directory.Exists(DestinationPath) Then
                memory = 0

                Do While Directory.Exists(DestinationPath & "_" & memory)
                    memory = memory + 1
                Loop
                frmLoading.pbMain.Value = 6

                Directory.CreateDirectory(DestinationPath & "_" & memory)

                ZipPath = DestinationPath & "_" & memory & "\" & ZIPFileOfficialNameS & ZIPFileOfficialNameExtension
                ZipFile.CreateFromDirectory(SourcePath, ZipPath)

            Else

                frmLoading.pbMain.Value = 6
                memory = 0

                Directory.CreateDirectory(DestinationPath)

                ZipPath = DestinationPath & "\Numistmatics(backup).ndb"
                ZipFile.CreateFromDirectory(SourcePath, ZipPath)
            End If


            frmLoading.pbMain.Value = 7
            If Directory.Exists(CopyTemporyFileName) Then
                Directory.Delete(CopyTemporyFileName, True)
            End If

        Catch ex As Exception
            MsgBox("Failed to take backup database due to " & ex.Message)
        End Try

    End Sub
    Private Sub DailyBackup()

        Dim CopyTemporyFileName As String = "temp"
        Try
            frmLoading.pbMain.Value = 1

            If Directory.Exists(CopyTemporyFileName) Then
                Directory.Delete(CopyTemporyFileName, True)
                Directory.CreateDirectory(CopyTemporyFileName)
            Else
                Directory.CreateDirectory(CopyTemporyFileName)
            End If

            frmLoading.pbMain.Value = 2
            System.IO.File.Copy(Application.StartupPath + "\Coins.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Coins.accdb")
            System.IO.File.Copy(Application.StartupPath + "\Notes.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Notes.accdb")
            System.IO.File.Copy(Application.StartupPath + "\Currency_Short_Names.accdb", Application.StartupPath + "\" + CopyTemporyFileName + "\Currency_Short_Names.accdb")

            frmLoading.pbMain.Value = 3
            If Directory.Exists(Application.StartupPath + "\Resources") Then
                My.Computer.FileSystem.CopyDirectory(Application.StartupPath + "\Resources", Application.StartupPath + "\" + CopyTemporyFileName + "\Resources", True)
            End If

            frmLoading.pbMain.Value = 4

            Dim SourcePath As String = Application.StartupPath + "\" + CopyTemporyFileName
            Dim ZipPath As String
            Dim memory As Integer = 0
            Dim ZIPFileOfficialNameS As String = "Numistmatics(backup)"
            Dim ZIPFileOfficialNameExtension As String = ".ndb"

            Dim DestinationPath As String = Application.StartupPath + "\" + ABackupFolderName + "\" + DateTime.Now.ToString("dd-MM-yyyy")

            frmLoading.pbMain.Value = 5

            If Directory.Exists(DestinationPath) Then

                frmLoading.pbMain.Value = 6

                Directory.Delete(DestinationPath, True)

                Directory.CreateDirectory(DestinationPath)

                ZipPath = DestinationPath & "\Numistmatics(backup).ndb"
                ZipFile.CreateFromDirectory(SourcePath, ZipPath)

            Else

                frmLoading.pbMain.Value = 6

                Directory.CreateDirectory(DestinationPath)

                ZipPath = DestinationPath & "\Numistmatics(backup).ndb"
                ZipFile.CreateFromDirectory(SourcePath, ZipPath)
            End If


            frmLoading.pbMain.Value = 7
            If Directory.Exists(CopyTemporyFileName) Then
                Directory.Delete(CopyTemporyFileName, True)
            End If


        Catch ex As Exception
            MsgBox("Failed to take backup database due to " & ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        End
    End Sub
End Class
