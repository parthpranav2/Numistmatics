Imports System.Windows.Forms ' For MessageBox, etc.

Public Class frmDashboard ' Renamed from frmSelection for consistency

    ' --- Form Instances (Declare as Shared properties for single-instance management) ---
    ' These should ideally be managed from your main application startup point (e.g., Program.vb or a central module).
    ' For demonstration, they are declared here.
    Public Shared frmSettingInstance As frmSetting
    Public Shared frmAboutMeInstance As frmAboutMe
    Public Shared frmInfoCenterInstance As frmInfoCenter
    Public Shared frmCoinsInstance As frmCoins
    Public Shared frmDatabaseInstance As frmDatabase
    Public Shared frmNotesInstance As frmNotes
    Public Shared frmBackupRestoreInstance As frmBackupRestore
    Public Shared frmGalleryInstance As frmGallery
    Public Shared frmLoginInstance As frmLogin ' Assuming frmLogin is the initial form

    ' --- Form Initialization ---
    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Any dashboard specific initialization can go here.
        ' Ensure all child form instances are created if they are not already.
        ' This can be done lazily (on first click) or eagerly here.
        ' E.g., if you want to pre-load them:
        ' If frmSettingInstance Is Nothing Then frmSettingInstance = New frmSetting()
        ' ... and so on for all forms.
    End Sub

    ' --- Form Closure ---
    Private Sub frmDashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' This is likely your main application form.
        ' Ensure all other forms are closed before exiting the application.
        ' A simple way is to iterate through Application.OpenForms and close them.
        For Each openForm As Form In Application.OpenForms
            If openForm IsNot Me Then ' Don't close myself here
                Try
                    openForm.Close()
                Catch ex As Exception
                    Console.WriteLine($"Error closing form {openForm.Name}: {ex.Message}")
                    ' Log error, but continue attempting to close others
                End Try
            End If
        Next
        System.Windows.Forms.Application.Exit() ' Exit the application once all forms are closed
    End Sub

    ' --- Navigation Buttons ---

    ' Settings
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        frmSettingInstance.Show()
        Me.Hide()
    End Sub

    ' About Me
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        frmAboutMeInstance.Show()
        Me.Hide()
    End Sub

    ' Info Center
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        frmInfoCenterInstance.Show()
        Me.Hide()
    End Sub

    ' Coins Form
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        frmCoinsInstance.Show()
        frmCoinsInstance.RefreshDatabase() ' Ensure data is fresh
        Me.Hide()
    End Sub

    ' Database Form
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        frmDatabaseInstance.Show()
        ' frmDatabase.RefreshAllDatabases() ' Call refresh if it has such a method
        Me.Hide() ' Hide dashboard if you want to navigate away
    End Sub

    ' Notes Form
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        frmNotesInstance.Show()
        frmNotesInstance.RefreshDatabase() ' Ensure data is fresh
        Me.Hide()
    End Sub

    ' Backup/Restore
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click ' This was Button1_Click_1 in original

        frmBackupRestoreInstance.Show()
        Me.Hide()
    End Sub

    ' Gallery
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        frmGalleryInstance.Show()
        Me.Hide()
    End Sub

    ' Logout
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        frmLoginInstance.Show()
        Me.Hide()
    End Sub

    ' Exit (Assuming Button10 is an explicit Exit button)
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ' You might want to ask for confirmation before exiting
        If MessageBox.Show("Are you sure you want to exit the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ' Ensure all other forms are closed before exiting
            For Each openForm As Form In Application.OpenForms
                If openForm IsNot Me Then
                    Try
                        openForm.Close()
                    Catch ex As Exception
                        Console.WriteLine($"Error closing form {openForm.Name} during exit: {ex.Message}")
                    End Try
                End If
            Next
            System.Windows.Forms.Application.Exit()
        End If
    End Sub

    ' --- UI Hover Effects (Ensure My.Resources exist) ---
    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        Button2.Image = My.Resources.Gear2
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        Button2.Image = My.Resources.Gear1
    End Sub

    Private Sub Button6_MouseEnter(sender As Object, e As EventArgs) Handles Button6.MouseEnter
        Button6.Image = My.Resources.Database2
    End Sub

    Private Sub Button6_MouseLeave(sender As Object, e As EventArgs) Handles Button6.MouseLeave
        Button6.Image = My.Resources.Database1
    End Sub

    Private Sub Button9_MouseEnter(sender As Object, e As EventArgs) Handles Button9.MouseEnter
        Button9.Image = My.Resources.Logout2
    End Sub

    Private Sub Button9_MouseLeave(sender As Object, e As EventArgs) Handles Button9.MouseLeave
        Button9.Image = My.Resources.Logout1
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.Image = My.Resources.Save2
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.Image = My.Resources.Save1
    End Sub

    Private Sub Button8_MouseEnter(sender As Object, e As EventArgs) Handles Button8.MouseEnter
        Button8.Image = My.Resources.Gallery2
    End Sub

    Private Sub Button8_MouseLeave(sender As Object, e As EventArgs) Handles Button8.MouseLeave
        Button8.Image = My.Resources.Gallery1
    End Sub

    Private Sub Button7_MouseEnter(sender As Object, e As EventArgs) Handles Button7.MouseEnter
        Button7.Image = My.Resources.Notes2
    End Sub

    Private Sub Button7_MouseLeave(sender As Object, e As EventArgs) Handles Button7.MouseLeave
        Button7.Image = My.Resources.Notes1
    End Sub

    Private Sub Button5_MouseEnter(sender As Object, e As EventArgs) Handles Button5.MouseEnter
        Button5.Image = My.Resources.Coins2
    End Sub

    Private Sub Button5_MouseLeave(sender As Object, e As EventArgs) Handles Button5.MouseLeave
        Button5.Image = My.Resources.Coins1
    End Sub

    ' --- Unused/Empty Event Handlers (Removed for clarity, add back if needed) ---
    ' Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint
    ' End Sub

    ' Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    ' End Sub

End Class
