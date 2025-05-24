Public Class frmAboutMe
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub frmAboutMe_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub frmAboutMe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label4.Text = My.Application.Info.Version.ToString

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmLogin.Ending()

    End Sub
End Class