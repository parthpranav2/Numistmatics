Public Class frmInfoCenter
    Private Sub frmInfoCenter_FontChanged(sender As Object, e As EventArgs) Handles MyBase.FontChanged
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If My.Computer.Network.IsAvailable Then
            If Currency_NameComboBox.Text = Nothing Then
                Process.Start("https://www.xe.com/currencycharts/")
            Else
                Process.Start("https://www.xe.com/currencycharts/?from=" + Currency_NameComboBox.Text & "&to=" + My.Settings.ToCurrency & "&view=12h")
            End If
        Else
            MsgBox("Network is not available please try again later", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If My.Computer.Network.IsAvailable Then
            Process.Start("https://www.xe.com/currencyconverter/convert/?Amount=" + DenominationTextBox.Text & "&From=" + Currency_NameComboBox.Text & "&To=" + My.Settings.ToCurrency)
        Else
            MsgBox("Network is not available please try again later", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If My.Computer.Network.IsAvailable Then
            Process.Start("http://banknote.ws")
        Else
            MsgBox("Network is not available please try again later", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If My.Computer.Network.IsAvailable Then
            Process.Start("https://www1.oanda.com/currency/live-exchange-rates/")
        Else
            MsgBox("Network is not available please try again later", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If My.Computer.Network.IsAvailable Then
            Process.Start("https://colnect.com/en/categories")
        Else
            MsgBox("Network is not available please try again later", vbOKOnly + vbExclamation)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmDashboard.Show()
        Me.Hide()
    End Sub

    Private Sub frmInfoCenter_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        frmLogin.Ending()

    End Sub
End Class