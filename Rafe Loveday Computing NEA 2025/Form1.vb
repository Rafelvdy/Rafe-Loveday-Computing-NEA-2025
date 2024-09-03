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

    Private Sub CMDOpenWardrobe_Click(sender As Object, e As EventArgs) Handles CMDOpenWardrobe.Click
        Wardrobe.Show()
    End Sub
End Class
