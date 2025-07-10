Imports System.IO ' For Path.Combine and File.Exists
Imports System.Windows.Forms ' For MessageBox, etc.
Imports System.Diagnostics ' For Process.Start

Public Class frmSetting
    Dim filename As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click ' Select Scanner Driver
        Using OpenFileDialog1 As New OpenFileDialog()
            OpenFileDialog1.Title = "Select Scanner Driver Executable"
            OpenFileDialog1.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
            OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) ' Suggest a common program files path
            OpenFileDialog1.RestoreDirectory = True

            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                My.Settings.ScannerDrive = OpenFileDialog1.FileName ' Update setting
                TextBox1.Text = OpenFileDialog1.FileName ' Update UI
                My.Settings.Save() ' Save settings immediately after change
                MessageBox.Show("Scanner driver path saved successfully.", "Setting Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click ' Test Scanner Connection
        If String.IsNullOrWhiteSpace(My.Settings.ScannerDrive) Then
            MessageBox.Show("No scanner driver path has been selected. Please select one first.", "Missing Driver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Try
            MessageBox.Show("Attempting to test connection to scanner driver...", "Testing Connection", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If File.Exists(My.Settings.ScannerDrive) Then
                System.Diagnostics.Process.Start(My.Settings.ScannerDrive)
                MessageBox.Show("Connection test initiated. Please check the scanner application.", "Connection Established", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show($"Scanner driver file not found at: {My.Settings.ScannerDrive}{Environment.NewLine}Please re-select the correct file.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Console.WriteLine($"Error: Scanner driver file not found at {My.Settings.ScannerDrive}")
            End If
        Catch ex As Exception
            ' Log the specific error for debugging
            Console.WriteLine($"Error testing scanner connection: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            MessageBox.Show($"Connection Failed: An error occurred while trying to start the scanner driver. Details: {ex.Message}", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = Nothing
        My.Settings.ScannerDrive = Nothing
        filename = Nothing
    End Sub

    Private Sub frmSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtTocurrency.Text = My.Settings.ToCurrency
        TextBox1.Text = My.Settings.ScannerDrive
    End Sub

    Private Sub frmSetting_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Ensure all settings are saved one last time on form close
        My.Settings.ToCurrency = txtTocurrency.Text ' Capture any last-minute changes
        My.Settings.Save()

        ' Only exit the application if no other forms are open,
        ' allowing for graceful shutdown.
        If Application.OpenForms.Count = 0 Then
            System.Windows.Forms.Application.Exit()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click ' Go to Dashboard
        ' Save current settings before navigating away
        My.Settings.ToCurrency = txtTocurrency.Text ' Assuming txtTocurrency is where 'ToCurrency' is set
        My.Settings.Save()


        frmDashboard.Show()
        Me.Hide()
    End Sub




    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click ' Go to Change Key
        ' Save current settings before navigating away
        My.Settings.ToCurrency = txtTocurrency.Text ' Assuming txtTocurrency is where 'ToCurrency' is set
        My.Settings.Save()


        frmChangeKey.Show()
        Me.Hide()
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click ' Go to Login/Ending
        ' Save current settings before navigating away
        My.Settings.ToCurrency = txtTocurrency.Text ' Assuming txtTocurrency is where 'ToCurrency' is set
        My.Settings.Save()

        ' Assuming frmLogin.Ending() handles application exit or return to login
        ' Ensure frmLogin is initialized if it's not a shared instance

        frmLogin.Ending()
    End Sub


End Class