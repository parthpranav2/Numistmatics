Public Class frmSetting
    Dim filename As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            filename = OpenFileDialog1.FileName
            My.Settings.ScannerDrive = filename
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If filename = Nothing Or TextBox1.Text = Nothing Then
            MsgBox("There is no scanner driver silected so please re-select it", +vbOKOnly + vbExclamation)
        Else
            Try
                MsgBox("Testing Connection")
                System.Diagnostics.Process.Start("" + filename)
                MsgBox("Connection Established")
            Catch ex As Exception
                MsgBox("Connection Failed")
            End Try
        End If
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
        TextBox1.Text = My.Settings.ScannerDrive
        My.Settings.Save()
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Text = My.Settings.ScannerDrive
        My.Settings.Save()
        frmDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub TableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmChangeKey.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        frmLogin.Ending()
    End Sub
End Class