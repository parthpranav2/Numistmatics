Imports System.IO ' For Path.Combine, File.Exists
Imports System.Windows.Forms ' For MessageBox, ListView, ImageList, etc.
Imports System.Drawing ' For Image, Size
Imports System.Data.OleDb

Public Class frmGallery

    ' Constants for paths
    Private Const RESOURCES_FOLDER As String = "Resources"
    Private Const IMAGES_FOLDER As String = "Images"
    Private ReadOnly FullImagesPath As String = Path.Combine(Application.StartupPath, RESOURCES_FOLDER, IMAGES_FOLDER)

    ' --- Form References (Initialize these in your main application startup or constructor) ---
    Private frmDashboard As frmDashboard ' Assuming this form exists
    Private frmBackupRestore As frmBackupRestore ' Assuming this form exists



    ' --- Form Closure ---
    Private Sub frmGallery_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Ensure frmDashboard is initialized
        If frmDashboard Is Nothing OrElse frmDashboard.IsDisposed Then
            frmDashboard = New frmDashboard()
        End If
        frmDashboard.Show()
        ' Do not call Application.Exit() here unless this is the very last form to close
        ' and you intend to terminate the entire application.
    End Sub

    ' --- Navigation Buttons ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click ' Go to Dashboard
        ' Ensure frmDashboard is initialized
        If frmDashboard Is Nothing OrElse frmDashboard.IsDisposed Then
            frmDashboard = New frmDashboard()
        End If
        frmDashboard.Show()
        Me.Close() ' Close this form
    End Sub


    ' --- Form Initialization ---
    Private Sub frmGallery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize TableAdapter and DataSet if not done by designer
        If Me.NotesDataSet Is Nothing Then Me.NotesDataSet = New NotesDataSet()
        If Me.TableTableAdapter Is Nothing Then Me.TableTableAdapter = New NotesDataSetTableAdapters.TableTableAdapter()
        If Me.TableBindingSource Is Nothing Then Me.TableBindingSource = New BindingSource()
        Me.TableBindingSource.DataSource = Me.NotesDataSet
        Me.TableBindingSource.DataMember = "Table"
        Me.TableDataGridView.DataSource = Me.TableBindingSource ' Ensure DataGridView is bound if used for data access

        Try
            ' Load data into the 'NotesDataSet.Table' table.
            Me.TableTableAdapter.Fill(Me.NotesDataSet.Table)
        Catch ex As OleDbException
            MessageBox.Show($"Database error loading notes data for gallery: {ex.Message}{Environment.NewLine}Please ensure 'Notes.accdb' and its TableAdapter are correctly configured.", "Database Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"OleDbException (Gallery Load): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            ' Redirect to backup/restore on critical failure
            If frmBackupRestore Is Nothing OrElse frmBackupRestore.IsDisposed Then
                frmBackupRestore = New frmBackupRestore()
            End If
            frmBackupRestore.Show()
            ' Assuming tabRestore is a TabPage or similar control on frmBackupRestore
            frmBackupRestore.tabRestore.Show() ' Or frmBackupRestore.TabControl.SelectTab(1)
            Me.Hide()
            Return ' Exit load if critical error
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred loading notes data for gallery: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Exception (Gallery Load): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            Return ' Exit load if critical error
        End Try

        Try
            LoadImageGalleryF()
            LoadImageGalleryB()
        Catch ex As Exception
            MessageBox.Show($"Error loading image galleries: {ex.Message}", "Image Gallery Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Image Gallery Load Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}")
        End Try
    End Sub


    ' --- Image Gallery Loading (Frontal) ---
    Private Sub LoadImageGalleryF()
        lvFront.View = View.LargeIcon
        lvFront.FullRowSelect = True
        lvFront.Columns.Clear() ' Clear existing columns
        lvFront.Columns.Add("Images", 250)

        lvFront.Items.Clear()
        lvFront.LargeImageList = Nothing ' Clear previous ImageList

        Dim imgs As New ImageList()
        imgs.ImageSize = New Size(231, 93) ' Set desired thumbnail size
        imgs.ColorDepth = ColorDepth.Depth32Bit ' For better image quality

        ' Iterate directly through the DataTable for efficiency
        For Each row As DataRow In NotesDataSet.Table.Rows
            Dim frontalImageFileName As String = row("Frontal_ImageFileName")?.ToString() ' Access by column name
            Dim currencyName As String = row("Currency_Name")?.ToString() ' Access by column name
            Dim denomination As String = row("Denomination")?.ToString() ' Access by column name

            Dim imagePath As String = ""
            Dim itemText As String = ""

            If Not String.IsNullOrWhiteSpace(frontalImageFileName) Then
                imagePath = Path.Combine(FullImagesPath, frontalImageFileName)
                If File.Exists(imagePath) Then
                    Try
                        imgs.Images.Add(Image.FromFile(imagePath))
                        lbFront.Items.Add(frontalImageFileName) ' Add filename to ListBox
                        itemText = $"{currencyName} ({denomination})"
                    Catch ex As OutOfMemoryException
                        ' Handle corrupted or invalid image file
                        imgs.Images.Add(My.Resources.None_Image)
                        lbFront.Items.Add("ERROR: " & frontalImageFileName)
                        itemText = $"Corrupted Image ({currencyName} {denomination})"
                        Console.WriteLine($"Error loading image '{imagePath}': {ex.Message}")
                    Catch ex As Exception
                        imgs.Images.Add(My.Resources.None_Image)
                        lbFront.Items.Add("ERROR: " & frontalImageFileName)
                        itemText = $"Error Image ({currencyName} {denomination})"
                        Console.WriteLine($"Unexpected error loading image '{imagePath}': {ex.Message}")
                    End Try
                Else
                    imgs.Images.Add(My.Resources.None_Image)
                    lbFront.Items.Add("MISSING: " & frontalImageFileName)
                    itemText = $"No Image ({currencyName} {denomination})"
                    Console.WriteLine($"Warning: Frontal image file not found: {imagePath}")
                End If
            Else
                imgs.Images.Add(My.Resources.None_Image)
                lbFront.Items.Add("NOTHING")
                itemText = "No Image Available"
            End If

            ' Add ListViewItem
            Dim lvItem As New ListViewItem(itemText, imgs.Images.Count - 1) ' Use last added image index
            lvFront.Items.Add(lvItem)
        Next

        lvFront.LargeImageList = imgs
    End Sub

    ' --- Image Gallery Loading (Backward) ---
    Private Sub LoadImageGalleryB()
        lvBack.View = View.LargeIcon
        lvBack.FullRowSelect = True
        lvBack.Columns.Clear() ' Clear existing columns
        lvBack.Columns.Add("Images", 250)

        lvBack.Items.Clear()
        lvBack.LargeImageList = Nothing ' Clear previous ImageList

        Dim imgs As New ImageList()
        imgs.ImageSize = New Size(231, 93)
        imgs.ColorDepth = ColorDepth.Depth32Bit

        For Each row As DataRow In NotesDataSet.Table.Rows
            Dim backwardImageFileName As String = row("Backward_ImageFileName")?.ToString() ' Access by column name
            Dim currencyName As String = row("Currency_Name")?.ToString()
            Dim denomination As String = row("Denomination")?.ToString()

            Dim imagePath As String = ""
            Dim itemText As String = ""

            If Not String.IsNullOrWhiteSpace(backwardImageFileName) Then
                imagePath = Path.Combine(FullImagesPath, backwardImageFileName)
                If File.Exists(imagePath) Then
                    Try
                        imgs.Images.Add(Image.FromFile(imagePath))
                        lbBack.Items.Add(backwardImageFileName)
                        itemText = $"{currencyName} ({denomination})"
                    Catch ex As OutOfMemoryException
                        imgs.Images.Add(My.Resources.None_Image)
                        lbBack.Items.Add("ERROR: " & backwardImageFileName)
                        itemText = $"Corrupted Image ({currencyName} {denomination})"
                        Console.WriteLine($"Error loading image '{imagePath}': {ex.Message}")
                    Catch ex As Exception
                        imgs.Images.Add(My.Resources.None_Image)
                        lbBack.Items.Add("ERROR: " & backwardImageFileName)
                        itemText = $"Error Image ({currencyName} {denomination})"
                        Console.WriteLine($"Unexpected error loading image '{imagePath}': {ex.Message}")
                    End Try
                Else
                    imgs.Images.Add(My.Resources.None_Image)
                    lbBack.Items.Add("MISSING: " & backwardImageFileName)
                    itemText = $"No Image ({currencyName} {denomination})"
                    Console.WriteLine($"Warning: Backward image file not found: {imagePath}")
                End If
            Else
                imgs.Images.Add(My.Resources.None_Image)
                lbBack.Items.Add("NOTHING")
                itemText = "No Image Available"
            End If

            Dim lvItem As New ListViewItem(itemText, imgs.Images.Count - 1)
            lvBack.Items.Add(lvItem)
        Next

        lvBack.LargeImageList = imgs
    End Sub

    ' --- Image Double-Click to Open ---
    Private Sub lvFront_DoubleClick_1(sender As Object, e As EventArgs) Handles lvFront.DoubleClick
        If lvFront.SelectedItems.Count = 1 Then
            Try
                Dim selectedIndex As Integer = lvFront.FocusedItem.Index
                If selectedIndex >= 0 AndAlso selectedIndex < lbFront.Items.Count Then
                    Dim fileName As String = lbFront.Items(selectedIndex).ToString()
                    If Not fileName.StartsWith("NOTHING") AndAlso Not fileName.StartsWith("MISSING") AndAlso Not fileName.StartsWith("ERROR") Then
                        Dim fullPath As String = Path.Combine(FullImagesPath, fileName)
                        If File.Exists(fullPath) Then
                            System.Diagnostics.Process.Start(fullPath)
                        Else
                            MessageBox.Show($"The image file '{fileName}' was not found at '{FullImagesPath}'. It might have been moved or deleted.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Console.WriteLine($"Error: Image file not found for opening: {fullPath}")
                        End If
                    Else
                        MessageBox.Show("No valid image to open for this entry.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show($"Error opening frontal image: {ex.Message}", "Image Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Console.WriteLine($"Image Open Error (Frontal): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            End Try
        End If
    End Sub

    Private Sub lvBack_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvBack.MouseDoubleClick
        If lvBack.SelectedItems.Count = 1 Then
            Try
                Dim selectedIndex As Integer = lvBack.FocusedItem.Index
                If selectedIndex >= 0 AndAlso selectedIndex < lbBack.Items.Count Then
                    Dim fileName As String = lbBack.Items(selectedIndex).ToString()
                    If Not fileName.StartsWith("NOTHING") AndAlso Not fileName.StartsWith("MISSING") AndAlso Not fileName.StartsWith("ERROR") Then
                        Dim fullPath As String = Path.Combine(FullImagesPath, fileName)
                        If File.Exists(fullPath) Then
                            System.Diagnostics.Process.Start(fullPath)
                        Else
                            MessageBox.Show($"The image file '{fileName}' was not found at '{FullImagesPath}'. It might have been moved or deleted.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Console.WriteLine($"Error: Image file not found for opening: {fullPath}")
                        End If
                    Else
                        MessageBox.Show("No valid image to open for this entry.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show($"Error opening backward image: {ex.Message}", "Image Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Console.WriteLine($"Image Open Error (Backward): {ex.Message}{Environment.NewLine}{ex.StackTrace}")
            End Try
        End If
    End Sub


End Class