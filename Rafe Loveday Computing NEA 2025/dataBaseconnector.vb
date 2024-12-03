Imports System.Data.OleDb

Public Class dataBaseconnector
    'Creating a variable to hold the connection string to the access database which is private so that it can only be accessed inside the object
    Private connectionString As String = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" & Application.StartupPath & "\DigitalWardrobe.accdb"


    'A structure which will hold the information which is being written to the database
    Public Structure dataToDB
        Public ImagePath As String
        Public ImageURL As String
        Public category As String
        Public SubCategory As String
        Public Colour As String
        Public Material As String
        Public Brand As String
        Public Pattern As String
        Public LastWornDate As String
        Public WearFrequency As String
    End Structure


    Public Function createConnection()
        'Creating a new instance of the oleDBConnection object with the connection string as a parameter
        Dim connection As New OleDbConnection(connectionString)
        Return connection
    End Function


    Public Sub Insert(ByVal data As String, ByVal fieldName As String, ByVal tableName As String, ByVal link As Boolean, Optional ByVal UploadDate As String = Nothing)
        'THIS IS ADDING THE IMAGE PATH TO THE IMAGE
        'Runs the createConnection function and the returned value is stored into this subroutine
        Dim connection = createConnection()
        'Opens the connection between my program and the database
        connection.open()
        Dim sql As String
        'IF STATEMENT USED TO DETERMINE WHICH SQL STATEMENT TO USE DEPENDING ON UPLOADDATE
        If UploadDate <> Nothing Then
            'If there is an upload date, the upload date will be inserted when the image path is inserted
            sql = $"INSERT INTO [{tableName}] ([{fieldName}], UploadDate) VALUES (@data, @uploadDate)"
        Else
            sql = $"INSERT INTO [{tableName}] ([{fieldName}]) VALUES (@data)"
        End If
        'Creating the command depending on the IF statement
        Dim command As New OleDbCommand(sql, connection)
        command.Parameters.AddWithValue("@data", data)

        If UploadDate <> Nothing Then
            command.Parameters.AddWithValue("@uploadDate", UploadDate)
        End If
        command.ExecuteNonQuery()
        connection.close()
        'This finds the wardrobeID of the wardrobe currently being used
        Dim wardrobeID As Integer = findWardrobeID()
        'If the link variable inputted as a parameter is true, the link tables subroutine is used
        If link = True Then
            'This finds the imageID of the most recent image added and then returns it to the variable
            Dim imageID As Integer = findImageID()
            linkTables(wardrobeID, imageID)
        End If

        'Then reloads the flowlayout panel
        populateWardrobe(wardrobeID)
    End Sub

    'This is used to link the image to the wardrobe
    Private Sub linkTables(ByVal wardrobeID As Integer, ByVal imageID As Integer)
        Dim connection = createConnection()
        'Opens the connection between my program and the database
        connection.open()
        'This is connecting the datbase together so that the images are associated to the wardrobe
        Dim sql As String = "INSERT INTO CLOTHINGITEM (WardrobeID, ImageID) VALUES (@WardrobeID, @ImageID)"
        Dim Command = New OleDbCommand(sql, connection)
        Command.Parameters.AddWithValue("@WardrobeID", wardrobeID)
        Command.Parameters.AddWithValue("@ImageID", imageID)
        Command.ExecuteNonQuery()
        connection.close
    End Sub

    'This subroutine is ran before thew wardrobe shows so that any images are populated into the flowlayoutpanel
    'Will only attempt to load images if there are any file paths
    Public Sub populateWardrobe(WardrobeID As Integer)
        'Creating the connection to the database
        Dim connection = createConnection()
        connection.open()
        'Using inner join selects all rows from both tables as long as there is a match between the columns in both tables
        Dim sql = "Select [IMAGE].[ImagePath] FROM [IMAGE] INNER JOIN [CLOTHINGITEM] On [IMAGE].[ImageID] = [CLOTHINGITEM].[ImageID] WHERE [CLOTHINGITEM].[WardrobeID] = @WardrobeID"
        Dim command As New OleDbCommand(sql, connection)
        'This allows me to dynamically add wardrobe id to the Sql statement so that it can change with the variable
        command.Parameters.AddWithValue("@WardrobeID", WardrobeID)
        'This reads the data from the database
        Dim dr As OleDbDataReader = command.ExecuteReader()
        If dr.HasRows Then
            Dim imagePathList As New List(Of String)
            imagePathList = findImagePaths()
            'This will run the loop while it steps through each record and then stops when it stops reading
            While dr.Read()
                'Gets the iamgepath at eac  h record so it can display the image
                Dim imagePath As String = dr("ImagePath")
                If imagePathList.Contains(Nothing) Then
                    MessageBox.Show("NOTHING")
                ElseIf imagePathList.Contains(imagePath) = False Then
                    'creates a new picture box for each record
                    Dim picturebox As New PictureBox
                    'Formats the pictureboxes
                    With picturebox
                        .Image = Image.FromFile(imagePath)
                        .Size = New Size(150, 150)
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .Tag = imagePath
                    End With
                    AddHandler picturebox.Click, AddressOf pictureBox_Click
                    AddHandler picturebox.MouseEnter, AddressOf DeleteButtonHandler
                    Wardrobe.WardrobeImagePanel.Controls.Add(picturebox)
                End If
            End While
        End If
    End Sub

    'This function makes a list of all image paths and returns it
    Private Function findImagePaths()
        'creating a list so all image paths can be added to it
        Dim imagePaths As New List(Of String)
        Dim tempPath As String
        For Each pictureBox As Control In Wardrobe.WardrobeImagePanel.Controls
            If pictureBox.Tag <> Nothing Then
                tempPath = pictureBox.Tag
                imagePaths.Add(tempPath)
            End If
        Next
        Return imagePaths
    End Function

    'This subroutine is used so that iamges which are loaded in are able to be also clicked
    Private Sub pictureBox_Click(sender As Object, e As EventArgs)
        ClothingDetails.Show()
    End Sub

    'This function will first check to see if the database has an existing wardrobe
    'If there is, it will return wardrobeID
    'If not it will run createWardrobe and return its ID
    Public Function checkForWardrobe()
        Dim dr As OleDbDataReader
        'Runs a function to retrieve instance of oledbconnection object
        Dim connection = createConnection()
        connection.open()

        Dim sql As String = "Select WardrobeID FROM WARDROBE"
        'This handles retrieving the wardrobeID if Default Wardrobe is stored in WardrobeName to determine whether there is an existing wardrobe
        Dim command As New OleDbCommand(sql, connection)
        dr = command.ExecuteReader

        If dr.HasRows Then
            'This code will run if there is a match in the database with 'default wardrobe'
            'The wardrobe ID will be returned 
            'This will move onto the first row
            dr.Read()
            Dim wardrobeID As Integer = dr("WardrobeID")
            dr.Close()
            connection.close()
            Return wardrobeID
        Else
            'If there is no wardrobe existing, this function will be run and it will create a wardrobe, and then return the wardrobes ID 
            Return createWardrobe(connection)
        End If
    End Function
    'This subroutine is ran if there is no existing wardrobe
    'It will create a wardrobe and return its ID
    Public Function createWardrobe(ByVal connection As OleDbConnection)
        'This inserts a new value into the wardrobe name so that the autonumber for the ID is created too
        Dim sql As String = "INSERT INTO WARDROBE (WardrobeName) VALUES ('Default Wardrobe')"
        Dim command As New OleDbCommand(sql, connection)
        command.ExecuteNonQuery()

        Return findWardrobeID()
    End Function

    'This subroutine gets the current wardrobeID (only works when there is one wardrobe)
    Public Function findWardrobeID()
        'Creates the connection to the wardrobe
        Dim connection = createConnection()
        connection.open()

        Dim dr As OleDbDataReader
        'Tells the program what data i want
        Dim sql As String = "SELECT WardrobeID FROM WARDROBE"

        Dim command As New OleDbCommand(sql, connection)
        dr = command.ExecuteReader
        'moves it onto the first row
        dr.Read()
        'Saves the wardrobeID to a local variable
        Dim wardrobeID As Integer = dr("WardrobeID")
        'closes all the connections
        dr.Close()
        connection.close()
        Return wardrobeID
    End Function

    Public Function findImageID()
        'Creates the connection to the wardrobe
        Dim connection = createConnection()
        connection.open()

        Dim dr As OleDbDataReader
        'Selecting all imageID's in descending order so the last one added is the first one
        Dim sql As String = "SELECT ImageID FROM [IMAGE] ORDER BY ImageID DESC"

        Dim command As New OleDbCommand(sql, connection)
        dr = command.ExecuteReader
        'moves it onto the first row
        dr.Read()
        If dr.HasRows Then
            'Saves the imageID to a local variable
            Dim imageID As Integer = dr("ImageID")
            'closes all the connections
            dr.Close()
            connection.close()
            Return imageID
        Else
            MessageBox.Show("No data")
        End If
    End Function

    Public Sub DeleteButtonHandler(sender As Object, e As EventArgs)
        'This creates a new picture box so that the bin icon can be loaded into it
        Dim deleteButton As New PictureBox
        'Gets the picture box that was hovered over and stores it in a variable
        Dim imageItem As PictureBox = sender

        'Formatting the delete button that will appear on hover
        With deleteButton
            .Image = Image.FromFile("C:\Users\Owner\OneDrive - Maidstone Grammar School\A Level NEAs\Computing\Images\60761.png")
            .Size = New Size(20, 20)
            .SizeMode = PictureBoxSizeMode.Zoom
            'Choosing the location of the button depending on the width of the picture box so it will always be in top corner
            .Location = New Point(imageItem.Width - 25, 5)
            .BackColor = Color.Transparent
        End With

        'Adding a handler if the deletebutton is clicked so that the record can be deleted
        AddHandler deleteButton.Click, AddressOf DeleteImage


        imageItem.Controls.Add(deleteButton)
        deleteButton.Show()



        'Adding a handler here as it is only needed if the user has hovered over the image
        AddHandler imageItem.MouseLeave, AddressOf DeleteImageLeave


    End Sub

    Public Sub DeleteImageLeave(sender As Object, e As EventArgs)
        Dim imageItem As PictureBox = sender
        imageItem.Controls.Clear()
    End Sub

    Public Sub DeleteImage(sender As Object, e As EventArgs)
        MessageBox.Show("HELLO")
        Dim imageItem As PictureBox = sender
        Dim imagePath As String = imageItem.Tag
        Dim wardrobeID As Integer = findWardrobeID()

        Dim connection = createConnection()
        connection.open()
        Dim command As New OleDbCommand($"DELETE FROM IMAGE WHERE ImagePath = {imagePath}", connection)


        command.ExecuteNonQuery()
        connection.close()
        populateWardrobe(wardrobeID)
    End Sub

End Class
