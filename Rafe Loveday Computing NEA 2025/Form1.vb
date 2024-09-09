Public Class MainMenu
    Private Sub CMDSelectImage_Click(sender As Object, e As EventArgs) Handles CMDSelectImage.Click
        'Create a new OpenFileDialogue Instance
        Dim openFileDialog As New OpenFileDialog()

        'Set the filter options
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        openFileDialog.Title = "Select an Image File"
        openFileDialog.ShowDialog()

        If openFileDialog.FileName <> "" Then
            Dim imageFile = openFileDialog.FileName
            ImageAnalysis.PictureBox1.Image = Image.FromFile(imageFile)
            ImageAnalysis.Show
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If SC.SplitterDistance > 60 Then
            SC.SplitterDistance -= 90
        Else
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If SC.SplitterDistance < 60 Then
            SC.SplitterDistance += 90
        Else
            Timer2.Enabled = False
        End If
    End Sub

    Private Sub CMDOpenMenu_Click(sender As Object, e As EventArgs) Handles CMDOpenMenu.Click
        If SC.SplitterDistance > 60 Then
            Timer1.Enabled = True
        Else
            Timer2.Enabled = True
        End If
    End Sub
End Class
