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
                'This calls the class that connects the program to the database and runs the subroutine to insert data
                Dim myDBCon As New dataBaseconnector
                myDBCon.Insert(filename, "ImagePath", "IMAGE")
                Dim todaysDate As String = DateString
                myDBCon.Insert(todaysDate, "UploadDate", "IMAGE")
                Dim wardrobeID As Integer = myDBCon.findWardrobeID()
                myDBCon.populateWardrobe(wardrobeID)
            Next
        End If
    End Sub

    Private Sub PictureBox_click(sender As Object, e As EventArgs)
        ClothingDetails.Show()
    End Sub


End Class