Public Class frmCustomSplashScreen

    'One timer instance for the whole form
    Private ReadOnly splashTimer As New Timer() With {.Interval = 3500}   '3 500 ms = 3. 5s

    'Start the countdown when the splash screen appears
    Private Sub frmCustomSplashScreen_Load(sender As Object, e As EventArgs) _
        Handles MyBase.Load

        AddHandler splashTimer.Tick, AddressOf SplashTimer_Tick
        splashTimer.Start()
    End Sub

    'This runs after 5 s (or immediately if the user clicks the PictureBox)
    Private Sub SplashTimer_Tick(sender As Object, e As EventArgs)
        splashTimer.Stop()           'stop the timer—one shot is enough

        frmLogin.Show()             'open the login window

        Me.Hide()                    'or use Me.Close() if you don’t need the splash any more
    End Sub

    'Let the user bypass the delay by clicking the splash image
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) _
        Handles PictureBox1.Click

        SplashTimer_Tick(Nothing, EventArgs.Empty)
    End Sub

End Class
