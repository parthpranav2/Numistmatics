Public Class frmChangeKey
    Dim wrongpassword As Integer = 0
    Dim wrongpassword1 As Integer = 3
    Dim na As String = ""
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MaskedTextBoxO.UseSystemPasswordChar = True Then
            MaskedTextBoxO.UseSystemPasswordChar = False
            Button2.Text = "Hide Password"

        Else
            MaskedTextBoxO.UseSystemPasswordChar = True
            Button2.Text = "Show Password"

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MaskedTextBoxN.UseSystemPasswordChar = True Then
            MaskedTextBoxN.UseSystemPasswordChar = False
            Button1.Text = "Hide Password"

        Else
            MaskedTextBoxN.UseSystemPasswordChar = True
            Button1.Text = "Show Password"

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MaskedTextBoxRN.UseSystemPasswordChar = True Then
            MaskedTextBoxRN.UseSystemPasswordChar = False
            Button3.Text = "Hide Password"

        Else
            MaskedTextBoxRN.UseSystemPasswordChar = True
            Button3.Text = "Show Password"

        End If
    End Sub

    Private Sub frmChangeKey_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaskedTextBoxO.UseSystemPasswordChar = True
        MaskedTextBoxN.UseSystemPasswordChar = True
        MaskedTextBoxRN.UseSystemPasswordChar = True

        AccessDeny()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MaskedTextBoxO.UseSystemPasswordChar = True
        MaskedTextBoxN.UseSystemPasswordChar = True
        MaskedTextBoxRN.UseSystemPasswordChar = True

        MaskedTextBoxO.Text = Nothing
        MaskedTextBoxN.Text = Nothing
        MaskedTextBoxRN.Text = Nothing

        Button1.Text = "Show Password"
        Button2.Text = "Show Password"
        Button3.Text = "Show Password"
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        frmSetting.Show()
        Me.Hide()
    End Sub

    Private Sub frmChangeKey_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub MaskedTextBoxO_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles MaskedTextBoxO.MaskInputRejected

    End Sub
    Private Sub AccessGrant()
        MaskedTextBoxN.Enabled = True
        MaskedTextBoxRN.Enabled = True
        Button1.Enabled = True
        Button3.Enabled = True

        MaskedTextBoxN.Text = Nothing
        MaskedTextBoxRN.Text = Nothing

        butApply.Enabled = True

        Button1.Text = "Show Password"
        Button2.Text = "Show Password"
        Button3.Text = "Show Password"
    End Sub
    Private Sub AccessDeny()
        MaskedTextBoxN.Enabled = False
        MaskedTextBoxRN.Enabled = False

        MaskedTextBoxN.Text = Nothing
        MaskedTextBoxRN.Text = Nothing

        Button1.Enabled = False
        Button3.Enabled = False

        butApply.Enabled = False

        Button1.Text = "Show Password"
        Button2.Text = "Show Password"
        Button3.Text = "Show Password"
    End Sub

    Private Sub MaskedTextBoxO_TextChanged(sender As Object, e As EventArgs) Handles MaskedTextBoxO.TextChanged
        If (MaskedTextBoxO.Text = My.Settings.key) Then
            AccessGrant()

        Else
            AccessDeny()

        End If
    End Sub

    Private Sub butApply_Click(sender As Object, e As EventArgs) Handles butApply.Click
        If MaskedTextBoxN.Text = Nothing Or MaskedTextBoxRN.Text = Nothing Then
            MsgBox("The password field cannot be null", vbOKOnly + vbCritical)
        ElseIf MaskedTextBoxN.TextLength < 8 Or MaskedTextBoxRN.TextLength < 8 Then
            MsgBox("Password should be atleast of 8 characters", vbOKOnly + vbExclamation)
        Else
            If MaskedTextBoxN.Text = MaskedTextBoxRN.Text Then
                My.Settings.key = MaskedTextBoxN.Text
                My.Settings.Save()
                MsgBox("New password applied", vbOKOnly + vbInformation)

                frmDashboard.Show()
                MaskedTextBoxO.Text = Nothing
                Me.Hide()

            Else
                MsgBox("The password in " + Label2.Text + " " + Label3.Text + " are not same. Please recheck the password", vbOKOnly + vbExclamation)
            End If
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmLogin.Ending()

    End Sub
End Class