Imports System.IO.Compression
Imports System.IO
Imports System.Security.Cryptography

Public Class frmBackupRestore

    ' Constants for paths and file names
    Private Const ZIP_FILE_OFFICIAL_NAME As String = "Numistmatics(backup).ndb"
    Private Const TEMP_COPY_FOLDER As String = "temp_backup_data"
    Private Const TEMP_EXISTING_BACKUP_FOLDER As String = "temp_existing_backup"
    Private Const TEMP_EXTRACTED_FOLDER As String = "temp_extracted_data"
    Private Const PRESENT_DATA_SAVE_FOLDER As String = "present_app_data_temp_save" ' Renamed for clarity

    Private Const RESOURCES_FOLDER As String = "Resources"
    Private Const IMAGES_FOLDER As String = "Images"
    Private ReadOnly FullResourcesPath As String = Path.Combine(Application.StartupPath, RESOURCES_FOLDER)
    Private ReadOnly FullImagesPath As String = Path.Combine(Application.StartupPath, RESOURCES_FOLDER, IMAGES_FOLDER)

    ' Database file names
    Private Const COINS_DB As String = "Coins.accdb"
    Private Const NOTES_DB As String = "Notes.accdb"
    Private Const CURRENCY_NAMES_DB As String = "Currency_Short_Names.accdb"

    ' Password retry variables (consider a more robust lockout mechanism for production)
    Private wrongPasswordAttempts As Integer = 0
    Private Const MAX_PASSWORD_ATTEMPTS As Integer = 3

    ' State variable for restoration process
    Private NewRestored As Boolean = False

    ' --- Form References (Initialize these in your main application startup or constructor) ---
    Private frmDashboard As frmDashboard ' Assuming this form exists
    Private frmLogin As frmLogin ' Assuming this form exists
    Private frmDatabase As frmDatabase ' Assuming this form exists

    ' --- Password Hashing Helper Functions (Copied from frmChangeKey, or reference a shared module) ---
    ' In a real application, extract these to a dedicated utility class/module.


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
    Private Sub frmBackupRestore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize TableAdapters and DataSets if not done by designer
        Me.TableTableAdapter1 = New NotesDataSetTableAdapters.TableTableAdapter()
        Me.NotesDataSet = New NotesDataSet()
        Me.TableTableAdapter = New CoinsDataSetTableAdapters.TableTableAdapter()
        Me.CoinsDataSet = New CoinsDataSet()

        Try
            ' Fill DataSets (ensure TableAdapters are initialized)
            Me.TableTableAdapter1.Fill(Me.NotesDataSet.Table)
            Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
        Catch ex As Exception
            MessageBox.Show($"Error loading data for display: {ex.Message}", "Database Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Database Load Error (frmBackupRestore): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try

        ' Load backup frequency setting
        cmbBF.SelectedIndex = My.Settings.BackupFrequency

        AutoBackupRegistryLister() ' Corrected method name
    End Sub

    ' --- Form Closure ---
    Private Sub frmBackupRestore_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Only show Dashboard if it's not already visible and not explicitly exiting application
        If Not NewRestored Then ' Only navigate back if no new restore was performed

            frmDashboard.Show()
        End If

        ' Only exit the application if no other forms are open,
        ' allowing for graceful shutdown.
        If Application.OpenForms.Count = 0 Then
            System.Windows.Forms.Application.Exit()
        End If
    End Sub



    ' --- Navigation Buttons ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click ' OK button
        ' This button seems to confirm an action and then closes the form.
        If Not NewRestored Then
            PutPresentFilesBack() ' Revert temporary changes if no new restore happened
        End If

        ' Ensure frmDashboard is initialized

        frmDashboard.Show()
        Me.Close() ' Close this form, let the application continue
    End Sub

    ' --- Backup Directory Selection ---
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using FolderBrowserDialog1 As New FolderBrowserDialog()
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                TextBox1.Text = FolderBrowserDialog1.SelectedPath
            End If
        End Using
    End Sub

    ' --- Select Backup File for Restore ---
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Using OpenFileDialog1 As New OpenFileDialog()
            OpenFileDialog1.Filter = $"Numistmatics Database Backup Folder (.NDB)|*.ndb|All Files (*.*)|*.*"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Title = "Choose Numistmatics Backup File"
            OpenFileDialog1.AddExtension = True
            OpenFileDialog1.FilterIndex = 1
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.ValidateNames = True
            OpenFileDialog1.InitialDirectory = Application.StartupPath ' Suggest current app path
            OpenFileDialog1.RestoreDirectory = True

            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                TextBox2.Text = OpenFileDialog1.FileName
            Else
                ' User cancelled, do nothing
            End If
        End Using
    End Sub


    ' --- Custom Backup Logic ---
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show($"Please select your backup directory by pressing on '{Button2.Text}' button.", "Missing Directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim passwordInput As String = InputBox("Please enter your password", "User Verification", "") ' Default value empty

        If String.IsNullOrWhiteSpace(passwordInput) Then
            MessageBox.Show("The password field cannot be empty.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Password Validation ---
        If Not VerifyPassword(passwordInput, My.Settings.key) Then
            wrongPasswordAttempts += 1
            If wrongPasswordAttempts >= MAX_PASSWORD_ATTEMPTS Then
                MessageBox.Show("Your access to this program is denied due to too many incorrect password attempts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ' Consider disabling further attempts or exiting more gracefully
                If frmDashboard IsNot Nothing AndAlso Not frmDashboard.IsDisposed Then
                    frmDashboard.Button1.Enabled = False ' Assuming Button1 is a critical function
                End If
                System.Windows.Forms.Application.Exit()
            Else
                MessageBox.Show($"Password is incorrect. You have {MAX_PASSWORD_ATTEMPTS - wrongPasswordAttempts} attempts remaining.", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return ' Exit if password is incorrect
        End If
        wrongPasswordAttempts = 0 ' Reset attempts on successful login

        ' --- Backup Process ---
        Try
            pbCustomBackup.Value = 0 ' Reset progress bar

            Dim tempBackupPath As String = Path.Combine(Application.StartupPath, TEMP_COPY_FOLDER)

            ' 1. Prepare temporary directory
            If Directory.Exists(tempBackupPath) Then
                Directory.Delete(tempBackupPath, True) ' Delete existing temp folder
            End If
            Directory.CreateDirectory(tempBackupPath)

            pbCustomBackup.Value = 10 ' Progress update

            ' 2. Copy database files to temp directory
            File.Copy(Path.Combine(Application.StartupPath, COINS_DB), Path.Combine(tempBackupPath, COINS_DB), True)
            File.Copy(Path.Combine(Application.StartupPath, NOTES_DB), Path.Combine(tempBackupPath, NOTES_DB), True)
            File.Copy(Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB), Path.Combine(tempBackupPath, CURRENCY_NAMES_DB), True)

            pbCustomBackup.Value = 40 ' Progress update

            ' 3. Copy Resources folder to temp directory (if it exists)
            If Directory.Exists(FullResourcesPath) Then
                My.Computer.FileSystem.CopyDirectory(FullResourcesPath, Path.Combine(tempBackupPath, RESOURCES_FOLDER), True)
            End If

            pbCustomBackup.Value = 70 ' Progress update

            ' 4. Create ZIP file
            Dim backupDestinationPath As String = TextBox1.Text
            Dim finalZipPath As String = Path.Combine(backupDestinationPath, ZIP_FILE_OFFICIAL_NAME)

            ' Handle existing backup files by appending a number
            Dim memory As Integer = 0
            Dim baseZipFileName As String = Path.GetFileNameWithoutExtension(ZIP_FILE_OFFICIAL_NAME)
            Dim zipExtension As String = Path.GetExtension(ZIP_FILE_OFFICIAL_NAME)

            While File.Exists(finalZipPath)
                memory += 1
                finalZipPath = Path.Combine(backupDestinationPath, $"{baseZipFileName}_{memory}{zipExtension}")
            End While

            ZipFile.CreateFromDirectory(tempBackupPath, finalZipPath)

            pbCustomBackup.Value = 90 ' Progress update

            ' 5. Clean up temporary directory
            If Directory.Exists(tempBackupPath) Then
                Directory.Delete(tempBackupPath, True)
            End If

            pbCustomBackup.Value = 100 ' Progress complete
            MessageBox.Show("Backup has been taken of all databases successfully!", "Backup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Failed to take backup database due to: {ex.Message}{Environment.NewLine}Please check directory permissions and ensure no database files are in use.", "Backup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Backup Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        Finally
            pbCustomBackup.Value = 0 ' Reset progress bar
            ' Ensure temporary directories are cleaned up even on error
            If Directory.Exists(Path.Combine(Application.StartupPath, TEMP_COPY_FOLDER)) Then
                Try : Directory.Delete(Path.Combine(Application.StartupPath, TEMP_COPY_FOLDER), True) : Catch : End Try
            End If
        End Try
    End Sub

    ' --- Restore Logic ---
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show($"Please select your restore database location by pressing on '{Button5.Text}' button.", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim passwordInput As String = InputBox("Please enter your password", "User Verification", "")

        If String.IsNullOrWhiteSpace(passwordInput) Then
            MessageBox.Show("The password field cannot be empty.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Password Validation ---
        If Not VerifyPassword(passwordInput, My.Settings.key) Then
            wrongPasswordAttempts += 1
            If wrongPasswordAttempts >= MAX_PASSWORD_ATTEMPTS Then
                MessageBox.Show("Your access to this program is denied due to too many incorrect password attempts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If frmDashboard IsNot Nothing AndAlso Not frmDashboard.IsDisposed Then
                    frmDashboard.Button1.Enabled = False
                End If
                System.Windows.Forms.Application.Exit()
            Else
                MessageBox.Show($"Password is incorrect. You have {MAX_PASSWORD_ATTEMPTS - wrongPasswordAttempts} attempts remaining.", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return ' Exit if password is incorrect
        End If
        wrongPasswordAttempts = 0 ' Reset attempts on successful login

        ' --- Restore Process ---
        Dim tempExistingBackupPath As String = Path.Combine(Application.StartupPath, TEMP_EXISTING_BACKUP_FOLDER)
        Dim tempExtractedPath As String = Path.Combine(Application.StartupPath, TEMP_EXTRACTED_FOLDER)
        Dim selectedZipFile As String = TextBox2.Text
        Dim selectedZipFileName As String = Path.GetFileName(selectedZipFile)
        Dim localCopyOfZip As String = Path.Combine(Application.StartupPath, selectedZipFileName)

        Try
            pbRestore.Value = 0 ' Reset progress bar

            ' 1. Clean up previous temporary folders and local zip copy
            CleanTempDirectories(tempExistingBackupPath, tempExtractedPath, localCopyOfZip)

            MessageBox.Show("This function will overwrite existing data. Proceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            ' 2. Create temp folder for current app data (as a fallback)
            Directory.CreateDirectory(PRESENT_DATA_SAVE_FOLDER)
            File.SetAttributes(PRESENT_DATA_SAVE_FOLDER, FileAttributes.Hidden)

            pbRestore.Value = 10 ' Progress update

            ' 3. Move current application data to temp fallback folder
            SavePresentFiles() ' Use the existing helper to move current data

            pbRestore.Value = 30 ' Progress update

            ' 4. Copy selected backup ZIP to application startup path for extraction
            File.Copy(selectedZipFile, localCopyOfZip, True)

            pbRestore.Value = 40 ' Progress update

            ' 5. Extract backup to a temporary folder
            Directory.CreateDirectory(tempExtractedPath)
            File.SetAttributes(tempExtractedPath, FileAttributes.Hidden)
            ZipFile.ExtractToDirectory(localCopyOfZip, tempExtractedPath)

            pbRestore.Value = 60 ' Progress update

            ' 6. Verify extracted contents and move to application path
            Dim extractedCoinsDbPath As String = Path.Combine(tempExtractedPath, COINS_DB)
            Dim extractedNotesDbPath As String = Path.Combine(tempExtractedPath, NOTES_DB)
            Dim extractedCurrencyDbPath As String = Path.Combine(tempExtractedPath, CURRENCY_NAMES_DB)
            Dim extractedResourcesPath As String = Path.Combine(tempExtractedPath, RESOURCES_FOLDER)

            If File.Exists(extractedCoinsDbPath) AndAlso File.Exists(extractedNotesDbPath) AndAlso File.Exists(extractedCurrencyDbPath) Then
                ' Delete existing app databases before moving new ones
                DeleteApplicationDatabases()

                File.Move(extractedCoinsDbPath, Path.Combine(Application.StartupPath, COINS_DB))
                File.Move(extractedNotesDbPath, Path.Combine(Application.StartupPath, NOTES_DB))
                File.Move(extractedCurrencyDbPath, Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB))

                If Directory.Exists(extractedResourcesPath) Then
                    If Directory.Exists(FullResourcesPath) Then
                        Directory.Delete(FullResourcesPath, True) ' Delete existing resources
                    End If
                    Directory.Move(extractedResourcesPath, FullResourcesPath) ' Move extracted resources
                End If

                ' Re-hide resource folders
                If Directory.Exists(FullResourcesPath) Then
                    File.SetAttributes(FullResourcesPath, FileAttributes.Hidden)
                End If
                If Directory.Exists(FullImagesPath) Then
                    File.SetAttributes(FullImagesPath, FileAttributes.Hidden)
                End If

                ' Assuming frmLogin.FileHider() handles hiding files, call it if needed
                ' frmLogin.FileHider()

                NewRestored = True ' Indicate successful restore
                pbRestore.Value = 100 ' Progress complete
                MessageBox.Show("Restoration of database has been done. Please press refresh button in Notes or Coins form.", "Restoration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LinkLabel1.Visible = True ' Show link label on success

            Else
                ' If essential files are missing from backup, revert to previous state
                MessageBox.Show($"The selected file is not a valid Numistmatics backup ({ZIP_FILE_OFFICIAL_NAME}) or is corrupted. Your previous data has been restored.", "Invalid Backup File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NewRestored = False
                pbRestore.Value = 0 ' Reset progress
                LinkLabel1.Visible = False
                PutPresentFilesBack() ' Revert from temporary fallback
            End If

        Catch ex As Exception
            MessageBox.Show($"Failed to restore database due to: {ex.Message}{Environment.NewLine}Attempting to revert to previous data. Please check file permissions.", "Restoration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Restore Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            NewRestored = False
            pbRestore.Value = 0 ' Reset progress
            LinkLabel1.Visible = False
            PutPresentFilesBack() ' Always attempt to revert to previous state on failure
        Finally
            ' Clean up all temporary directories and the copied zip file
            CleanTempDirectories(tempExistingBackupPath, tempExtractedPath, localCopyOfZip)
        End Try
    End Sub


    ' Helper to delete application databases
    Private Sub DeleteApplicationDatabases()
        Try
            If File.Exists(Path.Combine(Application.StartupPath, COINS_DB)) Then File.Delete(Path.Combine(Application.StartupPath, COINS_DB))
            If File.Exists(Path.Combine(Application.StartupPath, NOTES_DB)) Then File.Delete(Path.Combine(Application.StartupPath, NOTES_DB))
            If File.Exists(Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB)) Then File.Delete(Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB))
            If Directory.Exists(FullResourcesPath) Then Directory.Delete(FullResourcesPath, True)
        Catch ex As Exception
            ' Log error but try to continue
            Console.WriteLine($"Warning: Could not delete existing application databases/resources: {ex.Message}")
            MessageBox.Show("Warning: Could not delete existing database files. Please ensure they are not in use and try again. Application will restart.", "File Access Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Application.Restart() ' Restart if unable to delete existing files
        End Try
    End Sub

    ' Helper to clean up temporary directories
    Private Sub CleanTempDirectories(ParamArray paths As String())
        For Each p As String In paths
            If Directory.Exists(p) Then
                Try : Directory.Delete(p, True) : Catch ex As Exception : Console.WriteLine($"Warning: Could not delete temp directory {p}: {ex.Message}") : End Try
            ElseIf File.Exists(p) Then
                Try : File.Delete(p) : Catch ex As Exception : Console.WriteLine($"Warning: Could not delete temp file {p}: {ex.Message}") : End Try
            End If
        Next
    End Sub

    ' --- Save Present Files (Fallback for Restore) ---
    Private Sub SavePresentFiles()
        ' This method moves current application data to a temporary folder
        Dim destFolder As String = Path.Combine(Application.StartupPath, PRESENT_DATA_SAVE_FOLDER)

        If Directory.Exists(destFolder) Then
            Directory.Delete(destFolder, True)
        End If
        Directory.CreateDirectory(destFolder)
        File.SetAttributes(destFolder, FileAttributes.Hidden)

        ' Move database files
        Try
            File.Move(Path.Combine(Application.StartupPath, COINS_DB), Path.Combine(destFolder, COINS_DB))
            File.Move(Path.Combine(Application.StartupPath, NOTES_DB), Path.Combine(destFolder, NOTES_DB))
            File.Move(Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB), Path.Combine(destFolder, CURRENCY_NAMES_DB))
        Catch ex As Exception
            Console.WriteLine($"Error moving current databases to temp save: {ex.Message}")
            MessageBox.Show("Could not move current database files to temporary save. Please ensure they are not in use. Application will restart.", "File Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Restart() ' Crucial if files cannot be moved
        End Try

        ' Move Resources folder
        If Directory.Exists(FullResourcesPath) Then
            My.Computer.FileSystem.MoveDirectory(FullResourcesPath, Path.Combine(destFolder, RESOURCES_FOLDER), True)
        End If
    End Sub

    ' --- Put Present Files Back (Revert from Fallback) ---
    Private Sub PutPresentFilesBack()
        ' This method moves data back from the temporary fallback folder to the application path
        Dim sourceFolder As String = Path.Combine(Application.StartupPath, PRESENT_DATA_SAVE_FOLDER)

        If Not Directory.Exists(sourceFolder) Then Return ' Nothing to restore if temp folder doesn't exist

        ' Delete current application databases/resources before restoring fallback
        DeleteApplicationDatabases()

        ' Move database files back
        Try
            File.Move(Path.Combine(sourceFolder, COINS_DB), Path.Combine(Application.StartupPath, COINS_DB))
            File.Move(Path.Combine(sourceFolder, NOTES_DB), Path.Combine(Application.StartupPath, NOTES_DB))
            File.Move(Path.Combine(sourceFolder, CURRENCY_NAMES_DB), Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB))
        Catch ex As Exception
            Console.WriteLine($"Error moving fallback databases back: {ex.Message}")
            MessageBox.Show("Could not restore previous database files. Data might be corrupted. Application will restart.", "Critical Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Restart()
        End Try

        ' Move Resources folder back
        If Directory.Exists(Path.Combine(sourceFolder, RESOURCES_FOLDER)) Then
            My.Computer.FileSystem.MoveDirectory(Path.Combine(sourceFolder, RESOURCES_FOLDER), FullResourcesPath, True)
        End If

        ' Clean up the temporary save folder
        If Directory.Exists(sourceFolder) Then
            Try : Directory.Delete(sourceFolder, True) : Catch ex As Exception : Console.WriteLine($"Warning: Could not delete temp save folder {sourceFolder}: {ex.Message}") : End Try
        End If

        ' Re-hide resource folders after restoration
        If Directory.Exists(FullResourcesPath) Then
            File.SetAttributes(FullResourcesPath, FileAttributes.Hidden)
        End If
        If Directory.Exists(FullImagesPath) Then
            File.SetAttributes(FullImagesPath, FileAttributes.Hidden)
        End If

        ' Assuming frmLogin.FileHider() handles hiding files, call it if needed
        ' frmLogin.FileHider()
    End Sub

    ' --- Auto Backup Registry Listing ---
    Private Sub AutoBackupRegistryLister() ' Corrected method name
        tbcAutoBRestore.Visible = False ' Assuming this is a TabControl or similar
        butABRestore.Enabled = False
        lbFileName.Items.Clear() ' Clear existing items

        Dim autoBackupPath As String = Path.Combine(Application.StartupPath, "Auto Backup") ' Assuming "Auto Backup" is the folder name

        If Directory.Exists(autoBackupPath) Then
            For Each DirectoryName As String In Directory.GetDirectories(autoBackupPath)
                lbFileName.Items.Add(Path.GetFileName(DirectoryName))
            Next
        Else
            Console.WriteLine($"Auto Backup directory not found: {autoBackupPath}")
            ' Optionally, inform the user or disable auto-backup features
        End If
    End Sub


    Private Sub SelectedBackupLoader()
        If lbFileName.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a backup from the list.", "No Backup Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim selectedBackupFolderName As String = lbFileName.SelectedItem.ToString()
        Dim autoBackupSourcePath As String = Path.Combine(Application.StartupPath, "Auto Backup", selectedBackupFolderName)
        Dim zipFilePath As String = Path.Combine(autoBackupSourcePath, ZIP_FILE_OFFICIAL_NAME)

        If Not File.Exists(zipFilePath) Then
            MessageBox.Show($"The selected auto-backup file '{ZIP_FILE_OFFICIAL_NAME}' was not found in '{selectedBackupFolderName}'. It might be corrupted or incomplete.", "Backup File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbcAutoBRestore.Visible = False
            butABRestore.Enabled = False
            PutPresentFilesBack() ' Revert if something went wrong
            Return
        End If

        Dim tempExtractedPath As String = Path.Combine(Application.StartupPath, TEMP_EXTRACTED_FOLDER)

        Try
            ' Clean up previous temporary extraction folder
            If Directory.Exists(tempExtractedPath) Then
                Directory.Delete(tempExtractedPath, True)
            End If
            Directory.CreateDirectory(tempExtractedPath)
            File.SetAttributes(tempExtractedPath, FileAttributes.Hidden)

            ZipFile.ExtractToDirectory(zipFilePath, tempExtractedPath)

            ' Verify extracted contents
            Dim extractedCoinsDbPath As String = Path.Combine(tempExtractedPath, COINS_DB)
            Dim extractedNotesDbPath As String = Path.Combine(tempExtractedPath, NOTES_DB)
            Dim extractedCurrencyDbPath As String = Path.Combine(tempExtractedPath, CURRENCY_NAMES_DB)
            Dim extractedResourcesPath As String = Path.Combine(tempExtractedPath, RESOURCES_FOLDER)

            If File.Exists(extractedCoinsDbPath) AndAlso File.Exists(extractedNotesDbPath) AndAlso File.Exists(extractedCurrencyDbPath) Then
                ' Delete existing application databases before moving new ones
                DeleteApplicationDatabases()

                File.Move(extractedCoinsDbPath, Path.Combine(Application.StartupPath, COINS_DB))
                File.Move(extractedNotesDbPath, Path.Combine(Application.StartupPath, NOTES_DB))
                File.Move(extractedCurrencyDbPath, Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB))

                If Directory.Exists(extractedResourcesPath) Then
                    If Directory.Exists(FullResourcesPath) Then
                        Directory.Delete(FullResourcesPath, True) ' Delete existing resources
                    End If
                    Directory.Move(extractedResourcesPath, FullResourcesPath) ' Move extracted resources
                End If

                ' Re-hide resource folders
                If Directory.Exists(FullResourcesPath) Then
                    File.SetAttributes(FullResourcesPath, FileAttributes.Hidden)
                End If
                If Directory.Exists(FullImagesPath) Then
                    File.SetAttributes(FullImagesPath, FileAttributes.Hidden)
                End If

                ' Refresh DataGridViews in the presentor view
                RefreshDatabasePresentor()

                tbcAutoBRestore.Visible = True ' Show presentor view
                butABRestore.Enabled = True ' Enable restore button for this selected backup
            Else
                tbcAutoBRestore.Visible = False
                butABRestore.Enabled = False
                MessageBox.Show("The selected auto-backup does not contain all required data files.", "Incomplete Backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PutPresentFilesBack() ' Revert to previous state
            End If

        Catch ex As Exception
            tbcAutoBRestore.Visible = False
            butABRestore.Enabled = False
            MessageBox.Show($"Failed to load selected auto-backup due to: {ex.Message}{Environment.NewLine}Attempting to revert to previous data.", "Auto-Backup Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Auto-Backup Load Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            PutPresentFilesBack() ' Revert to previous state on failure
        Finally
            ' Clean up temporary extraction directory
            If Directory.Exists(tempExtractedPath) Then
                Try : Directory.Delete(tempExtractedPath, True) : Catch ex As Exception : Console.WriteLine($"Warning: Could not delete temp extracted folder {tempExtractedPath}: {ex.Message}") : End Try
            End If
        End Try
    End Sub

    ' --- Auto Backup Restore Button ---
    Private Sub butABRestore_Click(sender As Object, e As EventArgs) Handles butABRestore.Click
        Dim passwordInput As String = InputBox("Please enter your password", "User Verification", "")

        If String.IsNullOrWhiteSpace(passwordInput) Then
            MessageBox.Show("The password field cannot be empty.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            NewRestored = False
            Return
        End If

        ' --- Password Validation ---
        If Not VerifyPassword(passwordInput, My.Settings.key) Then
            wrongPasswordAttempts += 1
            If wrongPasswordAttempts >= MAX_PASSWORD_ATTEMPTS Then
                MessageBox.Show("Your access to this program is denied due to too many incorrect password attempts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If frmDashboard IsNot Nothing AndAlso Not frmDashboard.IsDisposed Then
                    frmDashboard.Button1.Enabled = False
                End If
                System.Windows.Forms.Application.Exit()
            Else
                MessageBox.Show($"Password is incorrect. You have {MAX_PASSWORD_ATTEMPTS - wrongPasswordAttempts} attempts remaining.", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            NewRestored = False
            Return ' Exit if password is incorrect
        End If
        wrongPasswordAttempts = 0 ' Reset attempts on successful login

        ' --- Restore Selected Auto Backup ---
        Try
            ' The SelectedBackupLoader already moved the files to Application.StartupPath
            ' So, we just need to clean up the temporary save folder if it exists
            If Directory.Exists(Path.Combine(Application.StartupPath, PRESENT_DATA_SAVE_FOLDER)) Then
                Directory.Delete(Path.Combine(Application.StartupPath, PRESENT_DATA_SAVE_FOLDER), True)
            End If
            NewRestored = True
            MessageBox.Show("Database Restored successfully from auto-backup!", "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"Error restoring from auto-backup: {ex.Message}", "Restore Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Auto-Backup Restore Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            NewRestored = False
        End Try
    End Sub


    ' --- Reset Database Logic ---
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click ' Reset Database
        Dim resetBackupPath As String = Path.Combine(Application.StartupPath, "Reset Backup") ' Assuming "Reset Backup" is the folder name

        If Not Directory.Exists(resetBackupPath) Then
            MessageBox.Show("The 'Reset Backup' folder is missing. Cannot perform database reset. Please contact the developer.", "Reset Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        MessageBox.Show("This function will delete existing data and replace it with default databases. Proceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        Dim passwordInput As String = InputBox("Please enter your password", "User Verification", "")

        If String.IsNullOrWhiteSpace(passwordInput) Then
            MessageBox.Show("The password field cannot be empty.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Password Validation ---
        If Not VerifyPassword(passwordInput, My.Settings.key) Then
            MessageBox.Show("Password is incorrect.", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Reset Process ---
        Try
            pbReset.Value = 0 ' Reset progress bar

            Dim defaultCoinsDbPath As String = Path.Combine(resetBackupPath, COINS_DB)
            Dim defaultNotesDbPath As String = Path.Combine(resetBackupPath, NOTES_DB)
            Dim defaultCurrencyDbPath As String = Path.Combine(resetBackupPath, CURRENCY_NAMES_DB)
            Dim defaultResourcesPath As String = Path.Combine(resetBackupPath, RESOURCES_FOLDER)

            If File.Exists(defaultCoinsDbPath) AndAlso File.Exists(defaultNotesDbPath) AndAlso File.Exists(defaultCurrencyDbPath) Then
                pbReset.Value = 10 ' Progress update

                ' 1. Ensure existing files are not read-only and delete them
                DeleteApplicationDatabases() ' Use helper to delete existing files

                pbReset.Value = 40 ' Progress update

                ' 2. Copy default databases from "Reset Backup"
                File.Copy(defaultCoinsDbPath, Path.Combine(Application.StartupPath, COINS_DB), True)
                File.Copy(defaultNotesDbPath, Path.Combine(Application.StartupPath, NOTES_DB), True)
                File.Copy(defaultCurrencyDbPath, Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB), True)

                pbReset.Value = 70 ' Progress update

                ' 3. Handle Resources folder
                If Directory.Exists(FullResourcesPath) Then
                    Directory.Delete(FullResourcesPath, True) ' Delete existing resources
                End If
                If Directory.Exists(defaultResourcesPath) Then
                    My.Computer.FileSystem.CopyDirectory(defaultResourcesPath, FullResourcesPath, True)
                End If

                ' Re-hide resource folders
                If Directory.Exists(FullResourcesPath) Then
                    File.SetAttributes(FullResourcesPath, FileAttributes.Hidden)
                End If
                If Directory.Exists(FullImagesPath) Then
                    File.SetAttributes(FullImagesPath, FileAttributes.Hidden)
                End If

                ' Assuming frmLogin.FileHider() handles hiding files, call it if needed
                ' frmLogin.FileHider()

                pbReset.Value = 100 ' Progress complete
                MessageBox.Show("The database has been reset to default. Please refresh Notes or Coins form.", "Database Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Missing default database files in 'Reset Backup' folder. Cannot perform reset.", "Reset Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show($"Failed to reset database due to: {ex.Message}{Environment.NewLine}Application will restart to attempt recovery.", "Reset Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"Reset Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            Application.Restart() ' Restart as a last resort for critical reset failures
        Finally
            pbReset.Value = 0 ' Reset progress bar
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        frmDatabase.Show()

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        LinkLabel1.Visible = False
    End Sub


    ' --- Connection Test ---
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim allConnected As Boolean = True

        ' Test Coins.accdb
        If File.Exists(Path.Combine(Application.StartupPath, COINS_DB)) Then
            lblCoins.ForeColor = Color.Green
            lblCoins.Text = "Connected"
        Else
            lblCoins.ForeColor = Color.Red
            lblCoins.Text = "Disconnected"
            allConnected = False
        End If

        ' Test Notes.accdb
        If File.Exists(Path.Combine(Application.StartupPath, NOTES_DB)) Then
            lblNotes.ForeColor = Color.Green
            lblNotes.Text = "Connected"
        Else
            lblNotes.ForeColor = Color.Red
            lblNotes.Text = "Disconnected"
            allConnected = False
        End If

        ' Test Currency_Short_Names.accdb
        If File.Exists(Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB)) Then
            lblCurrency.ForeColor = Color.Green
            lblCurrency.Text = "Connected"
        Else
            lblCurrency.ForeColor = Color.Red
            lblCurrency.Text = "Disconnected"
            allConnected = False
        End If

        If allConnected Then
            MessageBox.Show("All databases are connected.", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Button7.Enabled = False ' Disable repair if all connected
        Else
            MessageBox.Show("Some databases are disconnected. You may need to reset or restore them.", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Button7.Enabled = True ' Enable repair if any are disconnected
        End If
    End Sub

    ' --- Repair Database Logic ---
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim passwordInput As String = InputBox("Please enter your password", "User Verification", "")

        If String.IsNullOrWhiteSpace(passwordInput) Then
            MessageBox.Show("The password field cannot be empty.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Password Validation ---
        If Not VerifyPassword(passwordInput, My.Settings.key) Then
            MessageBox.Show("Password is incorrect.", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Repair Process ---
        Dim resetBackupPath As String = Path.Combine(Application.StartupPath, "Reset Backup")

        Try
            Dim repairedCount As Integer = 0

            ' Repair Currency_Short_Names.accdb
            If Not File.Exists(Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB)) Then
                If File.Exists(Path.Combine(resetBackupPath, CURRENCY_NAMES_DB)) Then
                    File.Copy(Path.Combine(resetBackupPath, CURRENCY_NAMES_DB), Path.Combine(Application.StartupPath, CURRENCY_NAMES_DB), True)
                    repairedCount += 1
                Else
                    Console.WriteLine($"Warning: Reset backup for {CURRENCY_NAMES_DB} not found.")
                End If
            End If

            ' Repair Coins.accdb
            If Not File.Exists(Path.Combine(Application.StartupPath, COINS_DB)) Then
                If File.Exists(Path.Combine(resetBackupPath, COINS_DB)) Then
                    File.Copy(Path.Combine(resetBackupPath, COINS_DB), Path.Combine(Application.StartupPath, COINS_DB), True)
                    repairedCount += 1
                Else
                    Console.WriteLine($"Warning: Reset backup for {COINS_DB} not found.")
                End If
            End If

            ' Repair Notes.accdb
            If Not File.Exists(Path.Combine(Application.StartupPath, NOTES_DB)) Then
                If File.Exists(Path.Combine(resetBackupPath, NOTES_DB)) Then
                    File.Copy(Path.Combine(resetBackupPath, NOTES_DB), Path.Combine(Application.StartupPath, NOTES_DB), True)
                    repairedCount += 1
                Else
                    Console.WriteLine($"Warning: Reset backup for {NOTES_DB} not found.")
                End If
            End If

            ' Re-run connection test to update UI
            Button8_Click(sender, e)

            If repairedCount > 0 Then
                MessageBox.Show($"Successfully repaired {repairedCount} database(s). Please refresh Notes or Coins form.", "Repair Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No databases needed repair or default files were missing.", "Repair Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show($"Failed to repair database due to: {ex.Message}", "Repair Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Repair Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub

    ' --- Backup Frequency Settings ---
    Private Sub cmbBF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBF.SelectedIndexChanged
        ' Assuming Label5 and Label6 are related to displaying info about backup frequency
        Label5.Visible = (cmbBF.SelectedIndex = 2) ' Example: show Label5 if index is 2
        Label6.Visible = (cmbBF.SelectedIndex = 1) ' Example: show Label6 if index is 1

        ' Enable/disable save button based on change
        Button9.Enabled = (cmbBF.SelectedIndex <> My.Settings.BackupFrequency)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBF.SelectedIndexChanged
        If cmbBF.SelectedIndex = 2 Then
            Label5.Visible = True
            Label6.Visible = False
        ElseIf cmbBF.SelectedIndex = 1 Then
            Label5.Visible = False
            Label6.Visible = True
        ElseIf cmbBF.SelectedIndex = 0 Then
            Label5.Visible = False
            Label6.Visible = False
        End If

        If cmbBF.SelectedIndex = My.Settings.BackupFrequency Then
            Button9.Enabled = False
        Else
            Button9.Enabled = True
        End If
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click ' Save Backup Frequency
        Dim passwordInput As String = InputBox("Please enter your password", "User Verification", "")

        If String.IsNullOrWhiteSpace(passwordInput) Then
            MessageBox.Show("The password field cannot be empty.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Password Validation ---
        If Not VerifyPassword(passwordInput, My.Settings.key) Then
            MessageBox.Show("Password is incorrect.", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' --- Save Setting ---
        My.Settings.BackupFrequency = cmbBF.SelectedIndex
        My.Settings.Save()

        MessageBox.Show("Backup frequency setting saved successfully!", "Setting Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Button9.Enabled = False ' Disable button after saving
    End Sub


    Private Sub TableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.TableBindingSource.EndEdit()
        Me.TableAdapterManager1.UpdateAll(Me.CoinsDataSet)
    End Sub



    Private Sub lbFileName_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lbFileName.MouseDoubleClick
        If lbFileName.SelectedItem Is Nothing Then Return
        SelectedBackupLoader()
    End Sub

    Private Sub RefreshDatabasePresentor()
        tbcAutoBRestore.Visible = True ' Show the tab control for auto-backup preview

        Try
            ' Assuming these TableAdapters and DataSets are correctly initialized
            Me.TableTableAdapter.Fill(Me.CoinsDataSet.Table)
            Me.TableTableAdapter1.Fill(Me.NotesDataSet.Table)
        Catch ex As Exception
            MessageBox.Show($"Unable to load database for presentor view due to: {ex.Message}", "Database Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Console.WriteLine($"Presentor DB Load Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            SavePresentFiles()
            lbFileName.Enabled = True
            Button10.Enabled = False
            butABRestore.Enabled = False

        Catch ex As Exception
            MsgBox("There is a problem in achiving this task pls restart the Numistmatics app")
            lbFileName.Enabled = False
            Button10.Enabled = True
            butABRestore.Enabled = False
        End Try
    End Sub

    'Private Sub TableDataGridView_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles TableDataGridView.CellEnter
    ' This seems to be for displaying coins data in a presentor form (frmECoins)
    'If cmbPresentorDecision.SelectedIndex = 0 Then ' Assuming 0 means presenter mode is active
    'If e.ColumnIndex >= 0 And e.ColumnIndex <= 12 Then ' Check column bounds
    '  Dim row As DataGridViewRow = TableDataGridView.Rows(e.RowIndex)
    '           ' Ensure frmECoins is initialized
    ' If frmECoins Is Nothing OrElse frmECoins.IsDisposed Then frmECoins = New frmECoins()
    '         frmECoins.Label2.Text = row.Cells(1).Value?.ToString()
    '         frmECoins.Label3.Text = row.Cells(2).Value?.ToString()
    ' ... populate other labels
    'If Not frmECoins.Visible Then frmECoins.Show()
    'End If
    'End If
    'End Sub

End Class