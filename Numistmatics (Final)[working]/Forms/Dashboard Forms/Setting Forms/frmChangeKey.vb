Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms
Imports System.Linq


Public Class frmChangeKey
    Dim wrongpassword As Integer = 0
    Dim wrongpassword1 As Integer = 3
    Dim na As String = ""


    ' Hashes a password using PBKDF2 with a random salt
    Public Function HashPassword(password As String, Optional saltSize As Integer = 16, Optional iterations As Integer = 10000) As String
        Try
            ' saltBytes does NOT need a Using block as it's just a Byte array and doesn't implement IDisposable.
            Dim saltBytes = New Byte(saltSize - 1) {}
            Using rng = New RNGCryptoServiceProvider() ' RNGCryptoServiceProvider implements IDisposable
                rng.GetBytes(saltBytes) ' Generate a random salt
            End Using

            ' Derive key using PBKDF2
            Using pbkdf2 = New Rfc2898DeriveBytes(password, saltBytes, iterations) ' Rfc2898DeriveBytes implements IDisposable
                Dim hashBytes = pbkdf2.GetBytes(20) ' 20 bytes for SHA1 hash, common size

                ' Combine salt and hash for storage
                Dim combinedBytes(saltBytes.Length + hashBytes.Length - 1) As Byte
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length)
                Buffer.BlockCopy(hashBytes, 0, combinedBytes, saltBytes.Length, hashBytes.Length)

                Return Convert.ToBase64String(combinedBytes) ' Store as Base64 string
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error hashing password: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            Return Nothing ' Or throw the exception, depending on error strategy
        End Try
    End Function

    ' Verifies a password against a stored hashed password
    Public Function VerifyPassword(password As String, hashedPassword As String, Optional iterations As Integer = 10000) As Boolean
        Try
            Dim combinedBytes = Convert.FromBase64String(hashedPassword)
            Dim saltSize As Integer = 16 ' Must match the saltSize used during hashing
            Dim hashSize As Integer = 20 ' Must match the hash size used during hashing

            If combinedBytes.Length < saltSize + hashSize Then Return False ' Invalid hashed password format

            Dim saltBytes = New Byte(saltSize - 1) {}
            Buffer.BlockCopy(combinedBytes, 0, saltBytes, 0, saltSize)

            Dim storedHashBytes = New Byte(hashSize - 1) {}
            Buffer.BlockCopy(combinedBytes, saltSize, storedHashBytes, 0, hashSize)

            Using pbkdf2 = New Rfc2898DeriveBytes(password, saltBytes, iterations)
                Dim newHashBytes = pbkdf2.GetBytes(hashSize)
                Return newHashBytes.SequenceEqual(storedHashBytes) ' Compare the generated hash with the stored hash
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error verifying password: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            Return False
        End Try
    End Function

    ' --- Password Visibility Toggle Helper ---
    Private Sub TogglePasswordVisibility(ByVal maskedTextBox As MaskedTextBox, ByVal button As Button)
        If maskedTextBox.UseSystemPasswordChar = True Then
            maskedTextBox.UseSystemPasswordChar = False
            button.Text = "Hide Password"
        Else
            maskedTextBox.UseSystemPasswordChar = True
            button.Text = "Show Password"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TogglePasswordVisibility(MaskedTextBoxO, Button2)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TogglePasswordVisibility(MaskedTextBoxN, Button1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TogglePasswordVisibility(MaskedTextBoxRN, Button3)
    End Sub

    Private Sub frmChangeKey_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaskedTextBoxO.UseSystemPasswordChar = True
        MaskedTextBoxN.UseSystemPasswordChar = True
        MaskedTextBoxRN.UseSystemPasswordChar = True

        AccessDeny()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click ' Reset button
        MaskedTextBoxO.Text = Nothing
        MaskedTextBoxN.Text = Nothing
        MaskedTextBoxRN.Text = Nothing
        AccessDeny() ' Reset to initial disabled state
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click ' Go to Settings
        ' Ensure frmSetting is initialized

        frmSetting.Show()
        Me.Hide()
    End Sub

    Private Sub frmChangeKey_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Only exit the application if this is the last form or a specific exit scenario
        ' Otherwise, just Me.Close() is sufficient.
        ' If you have a main application form, let it manage the exit.
        If Application.OpenForms.Count = 0 Then
            System.Windows.Forms.Application.Exit()
        End If
    End Sub


    ' --- UI State Management ---
    Private Sub AccessGrant()
        MaskedTextBoxN.Enabled = True
        MaskedTextBoxRN.Enabled = True
        Button1.Enabled = True
        Button3.Enabled = True
        butApply.Enabled = True ' Enable Apply button

        MaskedTextBoxN.Text = Nothing
        MaskedTextBoxRN.Text = Nothing

        ' Reset password visibility buttons
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
        ' Compare entered old password with stored HASHED password
        If VerifyPassword(MaskedTextBoxO.Text, My.Settings.key) Then ' IMPORTANT: Compare with hashed key
            AccessGrant()
        Else
            AccessDeny()
        End If
    End Sub

    Private Sub butApply_Click(sender As Object, e As EventArgs) Handles butApply.Click
        If String.IsNullOrWhiteSpace(MaskedTextBoxN.Text) OrElse String.IsNullOrWhiteSpace(MaskedTextBoxRN.Text) Then
            MessageBox.Show("Password fields cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf MaskedTextBoxN.TextLength < 8 Then ' Only check new password length here
            MessageBox.Show("New password should be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf MaskedTextBoxN.Text <> MaskedTextBoxRN.Text Then
            MessageBox.Show("The new passwords do not match. Please recheck.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            ' All validations pass, hash and save the new password
            My.Settings.key = HashPassword(MaskedTextBoxN.Text) ' Store the HASHED password
            My.Settings.Save()

            MessageBox.Show("New password applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Ensure frmDashboard is initialized
            If frmDashboard Is Nothing OrElse frmDashboard.IsDisposed Then
                frmDashboard = New frmDashboard()
            End If
            frmDashboard.Show()
            MaskedTextBoxO.Text = Nothing ' Clear old password field
            Me.Hide()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click ' Go to Login/Ending
        ' Assuming frmLogin.Ending() handles application exit or return to login
        ' Ensure frmLogin is initialized if it's not a shared instance

        frmLogin.Ending()
    End Sub

End Class