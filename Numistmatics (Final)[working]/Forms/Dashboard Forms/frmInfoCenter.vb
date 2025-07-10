Imports System.Diagnostics ' For Process.Start
Imports System.Windows.Forms ' For MessageBox, etc.

Public Class frmInfoCenter



    ' --- Helper Function for Network Check ---
    Private Function CheckNetworkAndNotify() As Boolean
        If My.Computer.Network.IsAvailable Then
            Return True
        Else
            MessageBox.Show("Network is not available. Please check your internet connection and try again later.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
    End Function

    ' --- Form Initialization ---
    Private Sub frmInfoCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Any initialization logic for frmInfoCenter can go here.
        ' For example, loading data into Currency_NameComboBox if it's not data-bound.
    End Sub

    ' --- Event Handlers ---

    ' REMOVED: frmInfoCenter_FontChanged event handler as it was causing app crashes.
    ' Private Sub frmInfoCenter_FontChanged(sender As Object, e As EventArgs) Handles MyBase.FontChanged
    '    ' This event should NOT cause application exit. Remove this line.
    '    ' System.Windows.Forms.Application.Exit()
    ' End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click ' XE Currency Charts
        If CheckNetworkAndNotify() Then
            If String.IsNullOrWhiteSpace(Currency_NameComboBox.Text) Then
                Process.Start("https://www.xe.com/currencycharts/")
            Else
                ' Use string interpolation for cleaner URL construction
                Process.Start($"https://www.xe.com/currencycharts/?from={Currency_NameComboBox.Text}&to={My.Settings.ToCurrency}&view=12h")
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click ' XE Currency Converter
        If CheckNetworkAndNotify() Then
            If String.IsNullOrWhiteSpace(DenominationTextBox.Text) OrElse String.IsNullOrWhiteSpace(Currency_NameComboBox.Text) OrElse String.IsNullOrWhiteSpace(My.Settings.ToCurrency) Then
                MessageBox.Show("Please ensure Denomination, Currency Name, and Target Currency are selected before converting.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Sanitize DenominationTextBox.Text to ensure it's a valid number for the URL
            Dim sanitizedDenomination As String = DenominationTextBox.Text.Replace(",", "").Trim()
            If Not IsNumeric(sanitizedDenomination) Then
                MessageBox.Show("Please enter a valid numeric denomination.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Process.Start($"https://www.xe.com/currencyconverter/convert/?Amount={sanitizedDenomination}&From={Currency_NameComboBox.Text}&To={My.Settings.ToCurrency}")
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click ' XE Currency Information
        If CheckNetworkAndNotify() Then
            If String.IsNullOrWhiteSpace(Currency_NameComboBox.Text) Then
                Process.Start("https://www.xe.com/currency/")
            Else
                Process.Start($"https://www.xe.com/currency/{Currency_NameComboBox.Text}")
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click ' Banknote.ws
        If CheckNetworkAndNotify() Then
            Process.Start("http://banknote.ws")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click ' Oanda Live Exchange Rates
        If CheckNetworkAndNotify() Then
            Process.Start("https://www1.oanda.com/currency/live-exchange-rates/")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click ' Colnect Categories
        If CheckNetworkAndNotify() Then
            Process.Start("https://colnect.com/en/categories")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click ' Go to Dashboard
        ' Ensure frmDashboard is initialized
        frmDashboard.Show()
        Me.Hide() ' Hide this form
    End Sub

    Private Sub frmInfoCenter_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Only exit the application if no other forms are open,
        ' allowing for graceful shutdown.
        If Application.OpenForms.Count = 0 Then
            System.Windows.Forms.Application.Exit()
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click ' Go to Login/Ending
        ' Ensure frmLogin is initialized if it's not a shared instance

        frmLogin.Ending() ' Assuming frmLogin.Ending() handles application exit or return to login
    End Sub

End Class
