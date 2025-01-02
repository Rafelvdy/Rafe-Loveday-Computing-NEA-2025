Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing
Imports System.Windows.Automation

Public Class Wardrobe
    Private link As Boolean = False
    Private _imagePath As String

    Public Property ImagePath As String
        Set(value As String)
            _imagePath = value
        End Set
        Get
            Return _imagePath
        End Get
    End Property

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
                ''Link is set to true before hand as it this requires linking the tables
                link = True
                Dim todaysDate As String = DateString
                myDBCon.Insert(filename, "ImagePath", "IMAGE", link, todaysDate)
                'DISPLAYING THE WARDROBE
                Dim wardrobeID As Integer = myDBCon.checkForWardrobe()
                myDBCon.populateWardrobe(wardrobeID)
            Next
        End If
    End Sub
End Class