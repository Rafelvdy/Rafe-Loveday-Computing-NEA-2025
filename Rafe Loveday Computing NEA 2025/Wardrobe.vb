Imports System.Data.OleDb
Imports System.IO

Public Class Wardrobe
    'Creating variables to communicate with the database
    Dim con As OleDbConnection = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\DigitalWardrobe.accdb")
    Dim cmd As OleDbCommand

    Private Sub CMDAddImage_Click(sender As Object, e As EventArgs) Handles CMDAddImage.Click
        'TO DO!!!!!!
        'Have a validation to make sure images inputted are actually clothes (maybe)


        'Create a new OpenFileDialogue Instance so that the user is able to select their images that they want to enter into the library
        Dim openFileDialog As New OpenFileDialog()

        'USER ENTERING THE IMAGE AND THEN IT IS DISPLAYED IN FLOWLAYOUTPANEL
        'Set the filter options
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        openFileDialog.Title = "Select an Image File"
        openFileDialog.ShowDialog()

        If openFileDialog.FileName <> "" Then
            'This will loop through each selected file, so that if multiple images are selected they can all be processed
            For Each filename In openFileDialog.FileNames
                'creating a new picture box as images are added so that there isnt a limit to the amount of picture boxes
                Dim pictureBox As New PictureBox
                'This will resize the picture box proportionally so that all images are then displayed as same sizes
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom
                pictureBox.Height = 150
                pictureBox.Width = 150


                'Loading the image from file so that it can be added to the library
                pictureBox.Image = Image.FromFile(filename)

                'This then adds the picture box into the flow layout pannel so that they images can be automatically arranged
                FlowLayoutPanel1.Controls.Add(pictureBox)
            Next
        End If
        'SAVING IMAGE TO DATABASE
        Dim Sql = "Insert into tblimage (img) Values (@img)"

    End Sub
    Private Sub saveImage(sql As String)
        Dim arrImage() As Byte
        Dim mstream As New System.IO.MemoryStream()

    End Sub


End Class