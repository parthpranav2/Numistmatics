Imports System.IO ' For file and directory operations
Imports System.IO.Compression ' For ZipFile operations
Imports System.Windows.Forms ' For MessageBox, ProgressBar, etc.
Imports System.Security.Cryptography ' For password hashing
Imports System.Text ' For encoding strings to bytes
Imports System.Linq ' For SequenceEqual
Imports System.Threading ' For Threading

Public Class frmLogin

    ' --- Constants for paths and file names (Declared here for frmLogin's access) ---
    Private Const RESET_BACKUP_FOLDER As String = "Reset Backup"
    Private Const AUTO_BACKUP_FOLDER_NAME As String = "Auto Backup"
    Private Const TEMP_COPY_FOLDER As String = "temp_backup_data" ' For auto-backup temp files
    Private Const ZIP_FILE_OFFICIAL_NAME As String = "Numistmatics(backup).ndb"

    Private Const COINS_DB As String = "Coins.accdb"
    Private Const NOTES_DB As String = "Notes.accdb"
    Private Const CURRENCY_NAMES_DB As String = "Currency_Short_Names.accdb"
    Private Const RESOURCES_FOLDER As String = "Resources"
    Private Const IMAGES_FOLDER As String = "Images"

    ' Password retry variables
    Private wrongPasswordAttempts As Integer = 0
    Private Const MAX_PASSWORD_ATTEMPTS As Integer = 3


    ' --- Password Hashing Helper Functions (Copied from frmChangeKey, or reference a shared module) ---
    ' In a real application, extract these to a dedicated utility class/module.
    Public Function HashPassword(password As String, Optional saltSize As Integer = 16, Optional iterations As Integer = 10000) As String
        Try
            Dim saltBytes = New Byte(saltSize - 1) {}
            Using rng = New RNGCryptoServiceProvider()
                rng.GetBytes(saltBytes)
            End Using
            Using pbkdf2 = New Rfc2898DeriveBytes(password, saltBytes, iterations)
                Dim hashBytes = pbkdf2.GetBytes(20)
                Dim combinedBytes(saltBytes.Length + hashBytes.Length - 1) As Byte
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length)
                Buffer.BlockCopy(hashBytes, 0, combinedBytes, saltBytes.Length, hashBytes.Length)
                Return Convert.ToBase64String(combinedBytes)
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error hashing password: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            Return Nothing
        End Try
    End Function

    Public Function VerifyPassword(password As String, hashedPassword As String, Optional iterations As Integer = 10000) As Boolean
        Try
            Dim combinedBytes = Convert.FromBase64String(hashedPassword)
            Dim saltSize As Integer = 16
            Dim hashSize As Integer = 20
            If combinedBytes.Length < saltSize + hashSize Then Return False
            Dim saltBytes = New Byte(saltSize - 1) {}
            Buffer.BlockCopy(combinedBytes, 0, saltBytes, 0, saltSize)
            Dim storedHashBytes = New Byte(hashSize - 1) {}
            Buffer.BlockCopy(combinedBytes, saltSize, storedHashBytes, 0, hashSize)
            Using pbkdf2 = New Rfc2898DeriveBytes(password, saltBytes, iterations)
                Dim newHashBytes = pbkdf2.GetBytes(hashSize)
                Return newHashBytes.SequenceEqual(storedHashBytes)
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error verifying password: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            Return False
        End Try
    End Function

    ' --- Form Initialization ---
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaskedTextBox1.UseSystemPasswordChar = True
        Button2.Image = My.Resources.OpenEye ' Set initial eye icon

        ' DEBUG: Check current settings
        Console.WriteLine($"Current stored key: '{My.Settings.key}'")
        Console.WriteLine($"Key is null/empty: {String.IsNullOrWhiteSpace(My.Settings.key)}")

        ' Only set default if completely empty
        If String.IsNullOrWhiteSpace(My.Settings.key) Then
            Console.WriteLine("No password found, setting default...")
            My.Settings.key = "defaultpassword" ' Set as plain text initially
            My.Settings.Save()
            MessageBox.Show("No password was found. Default password 'defaultpassword' has been set.", "Default Password", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' Check what type of password we have
            If IsValidBase64Hash(My.Settings.key) Then
                Console.WriteLine("Existing hashed password detected")
            Else
                Console.WriteLine("Existing plain text password detected")
            End If
        End If

        ' Set attributes for folders
        SetFolderAttributes(Path.Combine(Application.StartupPath, RESET_BACKUP_FOLDER), FileAttributes.Hidden)
        SetFolderAttributes(Path.Combine(Application.StartupPath, AUTO_BACKUP_FOLDER_NAME), FileAttributes.Hidden)
        SetFolderAttributes(Path.Combine(Application.StartupPath, RESOURCES_FOLDER), FileAttributes.Hidden)

        FileHider() ' Hide database files
    End Sub

    ' --- Login Button Click ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GiveAccess()
    End Sub

    ' --- Give Access Logic (Authentication) ---
    ' --- Enhanced Give Access Logic (Supports both plain text and hashed passwords) ---
    Private Sub GiveAccess()
        If String.IsNullOrWhiteSpace(MaskedTextBox1.Text) OrElse String.IsNullOrWhiteSpace(txtusername.Text) Then
            MessageBox.Show("Some of the mandatory field(s) are empty. Please fill in both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Password Validation (supports both plain text and hashed) ---
        If IsPasswordValid(MaskedTextBox1.Text, My.Settings.key) Then
            Console.WriteLine("Password verification successful!")
            wrongPasswordAttempts = 0 ' Reset attempts on successful login

            frmDashboard.Show()

            ' Pass username to Dashboard (via property, not direct Label access)
            ' IMPORTANT: Ensure frmDashboard has a public method like 'SetLoggedInUsername(username As String)'
            frmDashboard.Label1.Text = txtusername.Text


            MaskedTextBox1.BackColor = Color.White
            txtusername.BackColor = Color.White
            MaskedTextBox1.Text = ""
            txtusername.Text = ""

            Me.Hide() ' Hide login form
        Else
            Console.WriteLine("Password verification failed!")
            wrongPasswordAttempts += 1
            If wrongPasswordAttempts >= MAX_PASSWORD_ATTEMPTS Then
                MessageBox.Show("Your access to this program is denied due to too many incorrect password attempts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                System.Windows.Forms.Application.Exit() ' Exit after lockout
            Else
                MessageBox.Show($"Password is not correct. You have only {MAX_PASSWORD_ATTEMPTS - wrongPasswordAttempts} attempt(s) left.", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                MaskedTextBox1.Text = "" ' Clear password field
                MaskedTextBox1.Focus() ' Set focus back to password
            End If
        End If
    End Sub

    ' --- New Hybrid Password Validation Function ---
    Private Function IsPasswordValid(enteredPassword As String, storedPassword As String) As Boolean
        Try
            ' Check if stored password is empty
            If String.IsNullOrWhiteSpace(storedPassword) Then
                Console.WriteLine("Stored password is empty!")
                Return False
            End If

            ' First, try direct plain text comparison
            If enteredPassword = storedPassword Then
                Console.WriteLine("Plain text password match found!")
                ' Optional: Auto-upgrade to hashed password for security
                UpgradeToHashedPassword(enteredPassword)
                Return True
            End If

            ' If plain text doesn't match, try hashed password verification
            If IsValidBase64Hash(storedPassword) Then
                Console.WriteLine("Attempting hashed password verification...")
                Return VerifyPassword(enteredPassword, storedPassword)
            Else
                Console.WriteLine("Password format not recognized (not plain text match, not valid hash)")
                Return False
            End If

        Catch ex As Exception
            Console.WriteLine($"Error in password validation: {ex.Message}")
            Return False
        End Try
    End Function

    ' --- Auto-upgrade plain text password to hashed version ---
    Private Sub UpgradeToHashedPassword(plainTextPassword As String)
        Try
            Console.WriteLine("Upgrading plain text password to hashed version...")
            My.Settings.key = HashPassword(plainTextPassword)
            My.Settings.Save()
            Console.WriteLine("Password successfully upgraded to hashed version!")
        Catch ex As Exception
            Console.WriteLine($"Error upgrading password: {ex.Message}")
            ' Don't crash if upgrade fails, just continue with plain text
        End Try
    End Sub

    ' --- Check if a string is a valid Base64 hash (basic validation) ---
    Private Function IsValidBase64Hash(input As String) As Boolean
        Try
            ' A valid hash should be Base64 encoded and have reasonable length
            If String.IsNullOrWhiteSpace(input) Then Return False

            ' Try to decode as Base64
            Dim decoded As Byte() = Convert.FromBase64String(input)

            ' Our hash format should be at least 36 bytes (16 salt + 20 hash)
            If decoded.Length < 36 Then Return False

            ' If it looks like a hash, return true
            Return True

        Catch ex As Exception
            ' If Base64 decoding fails, it's probably plain text
            Return False
        End Try
    End Function


    ' --- Password Visibility Toggle ---
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MaskedTextBox1.UseSystemPasswordChar = True Then
            MaskedTextBox1.UseSystemPasswordChar = False
            Button2.Text = "Hide Password"
            Button2.Image = My.Resources.ClosedEye ' Assuming My.Resources.ClosedEye exists
        Else
            MaskedTextBox1.UseSystemPasswordChar = True
            Button2.Text = "Show Password"
            Button2.Image = My.Resources.OpenEye ' Assuming My.Resources.OpenEye exists
        End If
    End Sub

    ' --- File Hiding Logic ---
    Public Sub FileHider()
        SetFileAttribute(Path.Combine(Application.StartupPath, COINS_DB), FileAttributes.Hidden)
        SetFileAttribute(Path.Combine(Application.StartupPath, NOTES_DB), FileAttributes.Hidden)
        SetFileAttribute(Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB), FileAttributes.Hidden)
    End Sub

    ' Helper to set file attributes with error handling
    Private Sub SetFileAttribute(filePath As String, attribute As FileAttributes)
        If File.Exists(filePath) Then
            Try
                File.SetAttributes(filePath, attribute)
            Catch ex As Exception
                Console.WriteLine($"Error setting attribute {attribute} for file {filePath}: {ex.Message}")
                ' Log error, but don't crash the app
            End Try
        End If
    End Sub

    ' Helper to set folder attributes with error handling
    Private Sub SetFolderAttributes(folderPath As String, attribute As FileAttributes)
        If Directory.Exists(folderPath) Then
            Try
                File.SetAttributes(folderPath, attribute)
            Catch ex As Exception
                Console.WriteLine($"Error setting attribute {attribute} for folder {folderPath}: {ex.Message}")
                ' Log error, but don't crash the app
            End Try
        End If
    End Sub

    ' --- Exit Button ---
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Ask for confirmation before exiting
        If MessageBox.Show("Are you sure you want to exit the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Ending() ' Call Ending to handle auto-backup and then exit
        End If
    End Sub

    ' --- Form Closed Event ---
    Private Sub frmLogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' If the login form is the last form and it closes, exit the application.
        ' This is often the main form, so Application.Exit() here is acceptable
        ' after any necessary cleanup (like auto-backup).
        If Application.OpenForms.Count = 0 Then
            System.Windows.Forms.Application.Exit()
        End If
    End Sub

    ' --- KeyDown Event for Enter Key ---
    Private Sub MaskedTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles MaskedTextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GiveAccess()
            e.SuppressKeyPress = True ' Prevent the "ding" sound
        End If
    End Sub

    Private Sub txtusername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtusername.KeyDown
        If e.KeyCode = Keys.Enter Then
            GiveAccess()
            e.SuppressKeyPress = True ' Prevent the "ding" sound
        End If
    End Sub

    ' --- UI Hover Effects (Ensure My.Resources exist) ---
    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.Image = My.Resources.Login2 ' Assuming My.Resources.Login2 exists
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.Image = My.Resources.Login1 ' Assuming My.Resources.Login1 exists
    End Sub

    '--- Application Ending And Auto Backup ---
    Public Sub Ending()
        ' This method is called when the application is about to close (e.g., from Logout or Exit)
        ' It handles auto-backup based on settings.
        ' Ensure auto-backup folder exists and is hidden
        Dim autoBackupFolderPath As String = Path.Combine(Application.StartupPath, AUTO_BACKUP_FOLDER_NAME)
        If Not Directory.Exists(autoBackupFolderPath) Then
            Directory.CreateDirectory(autoBackupFolderPath)
            SetFolderAttributes(autoBackupFolderPath, FileAttributes.Hidden)
        End If
        frmLoading.Show()
        frmLoading.Label1.Text = "Processing Auto Backup..."
        frmLoading.pbMain.Value = 0 ' Reset progress bar

        ' Perform backup on a separate thread to keep UI responsive
        Dim backupTask As ThreadStart = Sub()
                                            Try
                                                If My.Settings.BackupFrequency = 1 Then ' Startup Backup (new dated folder each time)
                                                    PerformAutoBackup(False)
                                                ElseIf My.Settings.BackupFrequency = 2 Then ' Daily Backup (overwrite existing daily backup)
                                                    PerformAutoBackup(True)
                                                End If
                                            Catch ex As Exception
                                                Console.WriteLine($"Auto Backup Failed: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
                                                MessageBox.Show($"Auto backup failed: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Finally
                                                ' Hide loading form and exit application on UI thread
                                                If frmLoading IsNot Nothing AndAlso Not frmLoading.IsDisposed Then
                                                    frmLoading.Invoke(Sub() frmLoading.Close())
                                                End If
                                                System.Windows.Forms.Application.Exit() ' Exit after backup attempt (success or failure)
                                            End Try
                                        End Sub

        ' Start the backup task on a new thread
        Dim backupThread As New Thread(backupTask)
        backupThread.Start()
    End Sub

    ' Consolidated Auto Backup Logic
    Private Sub PerformAutoBackup(isDailyBackup As Boolean)
        Dim tempCopyPath As String = Path.Combine(Application.StartupPath, TEMP_COPY_FOLDER)
        Dim destinationPath As String = Path.Combine(Application.StartupPath, AUTO_BACKUP_FOLDER_NAME, DateTime.Now.ToString("dd-MM-yyyy"))
        Dim zipFilePath As String

        Try
            ' 1. Prepare temporary directory
            If Directory.Exists(tempCopyPath) Then
                Directory.Delete(tempCopyPath, True)
            End If
            Directory.CreateDirectory(tempCopyPath)
            UpdateProgressBar(10)

            ' 2. Copy database files to temp directory
            File.Copy(Path.Combine(Application.StartupPath, COINS_DB), Path.Combine(tempCopyPath, COINS_DB), True)
            File.Copy(Path.Combine(Application.StartupPath, NOTES_DB), Path.Combine(tempCopyPath, NOTES_DB), True)
            File.Copy(Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB), Path.Combine(tempCopyPath, CURRENCY_NAMES_DB), True)
            UpdateProgressBar(30)

            ' 3. Copy Resources folder to temp directory (if it exists)
            If Directory.Exists(Path.Combine(Application.StartupPath, RESOURCES_FOLDER)) Then
                My.Computer.FileSystem.CopyDirectory(Path.Combine(Application.StartupPath, RESOURCES_FOLDER), Path.Combine(tempCopyPath, RESOURCES_FOLDER), True)
            End If
            UpdateProgressBar(50)

            ' 4. Prepare destination folder for backup
            If isDailyBackup Then
                ' For daily backup, delete and recreate today's folder
                If Directory.Exists(destinationPath) Then
                    Directory.Delete(destinationPath, True)
                End If
                Directory.CreateDirectory(destinationPath)
            Else
                ' For startup backup, create a new dated folder, handling existing ones
                Dim memory As Integer = 0
                Dim uniqueDestinationPath As String = destinationPath
                Do While Directory.Exists(uniqueDestinationPath)
                    memory += 1
                    uniqueDestinationPath = $"{destinationPath}_{memory}"
                Loop
                destinationPath = uniqueDestinationPath
                Directory.CreateDirectory(destinationPath)
            End If
            UpdateProgressBar(70)

            ' 5. Create ZIP file
            zipFilePath = Path.Combine(destinationPath, ZIP_FILE_OFFICIAL_NAME)
            ZipFile.CreateFromDirectory(tempCopyPath, zipFilePath)
            UpdateProgressBar(90)

            ' 6. Clean up temporary directory
            If Directory.Exists(tempCopyPath) Then
                Directory.Delete(tempCopyPath, True)
            End If
            UpdateProgressBar(100)

        Catch ex As Exception
            ' Log error, but let the Finally block in Ending() handle the exit
            Console.WriteLine($"Auto Backup Process Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            Throw ' Re-throw to be caught by the Ending() method's catch block
        Finally
            ' Ensure temporary directory is cleaned up even on error
            If Directory.Exists(tempCopyPath) Then
                Try : Directory.Delete(tempCopyPath, True) : Catch : End Try
            End If
        End Try
    End Sub

    ' Helper to update progress bar on the UI thread
    Private Sub UpdateProgressBar(value As Integer)
        If frmLoading IsNot Nothing AndAlso Not frmLoading.IsDisposed Then
            frmLoading.Invoke(Sub() frmLoading.pbMain.Value = value)
        End If
    End Sub

    ' --- Unused Event Handlers (Removed for clarity, add back if needed) ---
    ' Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    ' End Sub

    ' Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles MaskedTextBox1.MaskInputRejected
    ' End Sub

End Class
