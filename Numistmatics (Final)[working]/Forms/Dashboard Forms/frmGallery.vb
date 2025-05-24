Imports System.IO
Public Class frmGallery
    Dim folder As String = ("Resources\Images")
    Private Sub frmGallery_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        frmDashboard.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmDashboard.Show()
        Me.Close()
    End Sub
    Dim Frontal As String

    Private Sub TableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.TableBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.NotesDataSet)

    End Sub

    Private Sub frmGallery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'TODO: This line of code loads data into the 'NotesDataSet.Table' table. You can move, or remove it, as needed.
            Me.TableTableAdapter.Fill(Me.NotesDataSet.Table)
        Catch ex As Exception
            MsgBox("Unable to load the database, please try to restore or reset the database from the Backup and Restore window. The program will redirect you to the Backup and Restore form", vbOKOnly + vbCritical)
            frmBackupRestore.Show()
            frmBackupRestore.tabRestore.Show()
            Me.Hide()
        End Try

        Try
            LoadImageGalleryF()
            LoadImageGalleryB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadImageGalleryF()
        'frontal image
        lvFront.View = View.LargeIcon
        lvFront.FullRowSelect = True

        lvFront.Columns.Add("Images", 250)

        Dim Current As Integer = 0

        lvFront.Items.Clear()
        lvFront.LargeImageList = Nothing
        lbFront.Items.Clear()

        Dim imgs As ImageList = New ImageList
        imgs.ImageSize = New Size(231, 93)

        Current = 0

        Do Until Current + 1 > TableBindingSource.Count
            TableDataGridView.CurrentCell = Nothing
            TableDataGridView.Rows(Current).Cells(11).Selected = True

            Dim row As DataGridViewRow = TableDataGridView.CurrentRow

            Frontal = TableDataGridView.Rows(Current).Cells(11).Value.ToString

            If Frontal IsNot Nothing And File.Exists(Application.StartupPath & "\" & folder & "\" & Frontal) Then

                imgs.Images.Add(Image.FromFile(Application.StartupPath & "\" & folder & "\" + Frontal))
                lbFront.Items.Add(Frontal)

                ' Try
                lvFront.Items.Add(TableDataGridView.Rows(Current).Cells(4).Value.ToString & " (" & TableDataGridView.Rows(Current).Cells(5).Value.ToString & ")", Current)
                ' Catch ex As Exception

                'End Try
            Else
                imgs.Images.Add(My.Resources.None_Image)
                lbFront.Items.Add("Nothing")
                lvFront.Items.Add("No Image available")
            End If

            Current = Current + 1
        Loop

        lvFront.LargeImageList = imgs

    End Sub

    Dim backward As String
    Private Sub LoadImageGalleryB()
        'backward image
        lvBack.View = View.LargeIcon
        lvBack.FullRowSelect = True

        lvBack.Columns.Add("Images", 250)

        Dim Current As Integer = 0

        lvBack.Items.Clear()
        lvBack.LargeImageList = Nothing
        lbBack.Items.Clear()

        Dim imgs As ImageList = New ImageList
        imgs.ImageSize = New Size(231, 93)

        Current = 0

        Do Until Current + 1 > TableBindingSource.Count
            TableDataGridView.CurrentCell = Nothing
            TableDataGridView.Rows(Current).Cells(12).Selected = True

            Dim row As DataGridViewRow = TableDataGridView.CurrentRow

            backward = TableDataGridView.Rows(Current).Cells(12).Value.ToString

            If backward IsNot Nothing And File.Exists(Application.StartupPath & "\" & folder & "\" & backward) Then

                imgs.Images.Add(Image.FromFile(Application.StartupPath & "\" & folder & "\" + backward))
                lbBack.Items.Add(backward)

                ' Try
                lvBack.Items.Add(TableDataGridView.Rows(Current).Cells(4).Value.ToString & " (" & TableDataGridView.Rows(Current).Cells(5).Value.ToString & ")", Current)
                ' Catch ex As Exception

                'End Try
            Else
                imgs.Images.Add(My.Resources.None_Image)
                lbBack.Items.Add("Nothing")
                lvBack.Items.Add("No Image available")
            End If
            Current = Current + 1
        Loop

        lvBack.LargeImageList = imgs
    End Sub

    Private Sub lvFront_DoubleClick_1(sender As Object, e As EventArgs) Handles lvFront.DoubleClick
        Try
            If lvFront.SelectedIndices.Count = 1 Then
                lbFront.SelectedIndex = lvFront.FocusedItem.Index
                System.Diagnostics.Process.Start(Application.StartupPath & "\" & folder & "\" & lbFront.SelectedItem.ToString)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lvBack_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvBack.MouseDoubleClick
        Try
            If lvBack.SelectedIndices.Count = 1 Then
                lbBack.SelectedIndex = lvBack.FocusedItem.Index
                System.Diagnostics.Process.Start(Application.StartupPath & "\" & folder & "\" & lbBack.SelectedItem.ToString)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tabNFrontal_Click(sender As Object, e As EventArgs) Handles tabNFrontal.Click

    End Sub

    Private Sub SplitContainer2_SplitterMoved(sender As Object, e As SplitterEventArgs)

    End Sub
End Class