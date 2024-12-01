Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing
Imports System.Windows.Automation

Public Class Wardrobe
    'Creating variables to communicate with the database (& Application.StartupPath &)(
    ' Dim con As OleDbConnection = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\DigitalWardrobe.accdb;")


    Private Sub CMDAddImage_Click(sender As Object, e As EventArgs) Handles CMDAddImage.Click
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
                'Dim pictureBox As New PictureBox
                ''This will resize the picture box proportionally so that all images are then displayed as same sizes
                'pictureBox.SizeMode = PictureBoxSizeMode.Zoom
                'pictureBox.Height = 150
                'pictureBox.Width = 150
                ''Loading the image from file so that it can be added to the library
                'pictureBox.Image = Image.FromFile(filename)
                ''This then adds the picture box into the flow layout pannel so that they images can be automatically arranged
                'WardrobeImagePanel.Controls.Add(pictureBox)

                'This calls the class that connects the program to the database and runs the subroutine to insert data
                Dim myDBCon As New dataBaseconnector
                myDBCon.Insert(filename, "ImagePath", "IMAGE")
                Dim wardrobeID As Integer = myDBCon.findWardrobeID()
                myDBCon.populateWardrobe(wardrobeID)
                'Event handler to link each picture box to the picturebox click subroutine, so that as new pictureboxes are created they can all open the clothingdetails form
                'AddHandler pictureBox.Click, AddressOf PictureBox_click
            Next
        End If
    End Sub

    Private Sub PictureBox_click(sender As Object, e As EventArgs)
        ClothingDetails.Show()
    End Sub

    Private Sub Wardrobe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
End Class