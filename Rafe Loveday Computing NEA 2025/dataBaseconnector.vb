Imports System.Data.OleDb

Public Class dataBaseconnector
    'This creates a new picture box so that the bin icon can be loaded into it
    'Also becomes global so that i can add handlers to it
    Dim WithEvents deleteButton As New PictureBox

    'This timer is used to make the bin icon disappear
    Dim deleteButtonTimer As New Timer

    'This list is used for storing which clothing items have been deleted
    'Is reset everytime the wardrobe is populated
    Dim deletedItems As New List(Of String)

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
        Public _Brand As String
        Public Pattern As String
        Public LastWornDate As String
        Public WearFrequency As Integer
    End Structure

    Public Function createConnection()
        'Creating a new instance of the oleDBConnection object with the connection string as a parameter
        Dim connection As New OleDbConnection(connectionString)
        Return connection
    End Function

    Public Sub Insert(ByVal data As String, ByVal fieldName As String, ByVal tableName As String, ByVal link As Boolean, Optional ByVal UploadDate As String = Nothing, Optional ByVal Category As String = Nothing, Optional ByVal subCategory As String = Nothing, Optional ByVal colour As String = Nothing, Optional ByVal Material As String = Nothing, Optional ByVal Brand As String = Nothing, Optional ByVal Pattern As String = Nothing, Optional ByVal LastWornDate As String = Nothing, Optional ByVal WearFrequency As Integer = 0)
        'Runs the createConnection function and the returned value is stored into this subroutine
        Dim connection = createConnection()
        'Opens the connection between my program and the database
        connection.open()

        'Initialising sql and oledbcommand here so that they only have to be done once
        Dim sql As String
        Dim command As New OleDbCommand

        'This finds the wardrobeID of the wardrobe currently being used
        'imageID cannot be found yet as if there are no images in the wardrobe, the program will not be able to find one.
        Dim imageID As Integer
        Dim wardrobeID As Integer = checkForWardrobe()

        'CHOOSING WHAT SQL STATEMENT TO USE
        If tableName = "WARDROBE" Then
            'THIS CODE WILL RUN IF INSERTING TO THE WARDROBE TABLE
            'This inserts the string default wardrobe to the database to create the record, the PK is an autonumber
            sql = $"INSERT INTO [{tableName}] ([{fieldName}]) VALUES (@data)"
            'Adding the data from my program to the query
            command = New OleDbCommand(sql, connection)
            command.Parameters.AddWithValue("@data", data)

            'Executing the query
            command.ExecuteNonQuery()

        ElseIf tableName = "IMAGE" Then
            'THIS CODE WILL RUN IF INSERTING TO THE IMAGE TABLE
            sql = $"INSERT INTO [{tableName}] ([{fieldName}], UploadDate) VALUES (@data, @uploadDate)"
            command = New OleDbCommand(sql, connection)

            command.Parameters.AddWithValue("@data", data)
            command.Parameters.AddWithValue("@uploadDate", UploadDate)

            command.ExecuteNonQuery()
            connection.close()
            imageID = findImageID(data)
            linkTables(wardrobeID, imageID)

        ElseIf tableName = "CLOTHINGITEM" Then
            imageID = findImageID(data)

            'THIS CODE WILL RUN IF UPDATING TO THE CLOTHINGITEM TABLE
            sql = $"UPDATE CLOTHINGITEM SET Category = @Category, SubCategory = @SubCategory, Colour = @Colour, Material = @Material, Brand = @Brand, Pattern = @Pattern, LastWornDate = @LastWornDate, WearFrequency = @WearFrequency WHERE WardrobeID = @WardrobeID AND ImageID = @ImageID"
            command = New OleDbCommand(sql, connection)

            If Category <> Nothing Then
                command.Parameters.AddWithValue("@Category", Category)
                MessageBox.Show(command.CommandText)
            Else
                command.Parameters.AddWithValue("@Category", "N")
            End If

            If subCategory <> Nothing Then
                command.Parameters.AddWithValue("@SubCategory", subCategory)
            Else
                command.Parameters.AddWithValue("@SubCategory", "N")
            End If

            If colour <> Nothing Then
                command.Parameters.AddWithValue("@Colour", colour)
            Else
                command.Parameters.AddWithValue("@Colour", "N")
            End If

            If Material <> Nothing Then
                command.Parameters.AddWithValue("@Material", Material)
            Else
                command.Parameters.AddWithValue("@Material", "N")
            End If

            If Brand <> Nothing Then
                command.Parameters.AddWithValue("@Brand", Brand)
            Else
                command.Parameters.AddWithValue("@Brand", "N")
            End If

            If Pattern <> Nothing Then
                command.Parameters.AddWithValue("@Pattern", Pattern)
            Else
                command.Parameters.AddWithValue("@Pattern", "N")
            End If

            If LastWornDate <> Nothing Then
                command.Parameters.AddWithValue("@LastWornDate", LastWornDate)
            Else
                command.Parameters.AddWithValue("@LastWornDate", "N")
            End If

            If WearFrequency <> Nothing Then
                command.Parameters.AddWithValue("@WearFrequency", WearFrequency)
            Else
                command.Parameters.AddWithValue("@WearFrequency", 0)
            End If

            'must have a value so no if statement needed
            command.Parameters.AddWithValue("@wardrobeID", wardrobeID)
            command.Parameters.AddWithValue("@ImageID", imageID)

            command.ExecuteNonQuery()
            connection.close()
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

            'This total algorith compares each item in the list to each tag of the pictureboxes to see if they need to be deleted
            'The first for statement is to access each image path
            For Each item In deletedItems
                'Creating the picturebox so that i can remove it later if the IF statement is met
                Dim clothingItem As PictureBox
                'Going through each picturebox
                For Each ctrl As Control In Wardrobe.WardrobeImagePanel.Controls
                    If ctrl.Tag = item Then
                        clothingItem = ctrl
                    End If
                Next
                'Removing any items that need to be deleted
                Wardrobe.WardrobeImagePanel.Controls.Remove(clothingItem)
            Next
            'Clears the list after they are all deleted
            deletedItems.Clear()

            'This will run the loop while it steps through each record and then stops when it stops reading
            While dr.Read()
                'Gets the iamgepath at each record so it can display the image
                Dim imagePath As String = dr("ImagePath")
                If imagePathList.Contains(imagePath) = False Then
                    'creates a new picture box for each record
                    Dim picturebox As New PictureBox

                    'Formats the pictureboxes
                    With picturebox
                        .Image = Image.FromFile(imagePath)
                        .Size = New Size(150, 150)
                        .SizeMode = PictureBoxSizeMode.Zoom
                        .Tag = imagePath
                    End With

                    'This handler makes the clothing details page when double clicked
                    AddHandler picturebox.DoubleClick, AddressOf pictureBox_Click
                    'This handler makes the bin icon appear when hovering over a clothing item
                    AddHandler picturebox.MouseEnter, AddressOf DeleteButtonHandler

                    Wardrobe.WardrobeImagePanel.Controls.Add(picturebox)
                End If
            End While
        ElseIf dr.HasRows = False Then
            Wardrobe.WardrobeImagePanel.Controls.Clear()
        End If
    End Sub

    'This function makes a list of all image paths and returns it
    Private Function findImagePaths()
        'creating a list so all image paths can be added to it
        Dim imagePaths As New List(Of String)
        Dim tempPath As String
        'Finding the picture box using imagePath
        For Each pictureBox As Control In Wardrobe.WardrobeImagePanel.Controls
            If pictureBox.Tag <> Nothing Then
                tempPath = pictureBox.Tag
                imagePaths.Add(tempPath)
            End If
        Next
        Return imagePaths
    End Function

    Private Function findSpecificPath()

    End Function
    'This subroutine is used so that iamges which are loaded in are able to be also clicked
    Private Sub pictureBox_Click(sender As Object, e As EventArgs)
        ''This gets the clothing item that was just clicked and retrieves the image path so that the filters can be loaded
        Dim clothingItem As PictureBox = sender
        Dim ImagePath As String = clothingItem.Tag
        'Dim myClothingDetails As New ClothingDetails
        ClothingDetails.currentImagePath = ImagePath
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
        connection.Close()

        Return checkForWardrobe()
    End Function

    ''This subroutine gets the current wardrobeID (only works when there is one wardrobe)
    'Public Function findWardrobeID()
    '    'Creates the connection to the wardrobe
    '    Dim connection = createConnection()
    '    connection.open()

    '    Dim dr As OleDbDataReader
    '    'Tells the program what data i want
    '    Dim sql As String = "SELECT WardrobeID FROM WARDROBE"

    '    Dim command As New OleDbCommand(sql, connection)
    '    dr = command.ExecuteReader
    '    'moves it onto the first row
    '    dr.Read()
    '    'Saves the wardrobeID to a local variable
    '    Dim wardrobeID As Integer = dr("WardrobeID")
    '    'closes all the connections
    '    dr.Close()
    '    connection.close()
    '    Return wardrobeID
    'End Function

    Public Function findImageID(ByVal ImagePath As String)
        'Creates the connection to the wardrobe
        'Dim connection = createConnection()
        'connection.open()

        'Dim dr As OleDbDataReader
        ''Selecting all imageID's in descending order so the last one added is the first one
        'Dim sql As String = "SELECT ImageID FROM [IMAGE] ORDER BY ImageID DESC"

        'Dim command As New OleDbCommand(sql, connection)
        'dr = command.ExecuteReader
        ''moves it onto the first row
        'dr.Read()
        'If dr.HasRows Then
        '    'Saves the imageID to a local variable
        '    Dim imageID As Integer = dr("ImageID")
        '    'closes all the connections
        '    dr.Close()

        '    Return imageID
        'Else
        '    MessageBox.Show("No data")
        'End If
        'connection.close()

        'THIS SECTION RETRIEVES THE IMAGEID USING THE IMAGEPATH
        Dim ImageID As Integer
        'Creating a connection to the database
        Dim connection = createConnection()
        connection.open()

        Dim sql As String = "SELECT ImageID FROM [IMAGE] WHERE ImagePath = @ImagePath"
        Dim command As New OleDbCommand(sql, connection)
        command.Parameters.AddWithValue("@ImagePath", ImagePath)

        Dim dr As OleDbDataReader
        dr = command.ExecuteReader
        'moves it onto the first row
        dr.Read()
        If dr.HasRows Then
            'Saves the imageID to a local variable
            ImageID = dr("ImageID")
            'closes all the connections
            Return ImageID
        Else
            MessageBox.Show("No Image ID")
        End If
        dr.Close()
    End Function

    Public Sub DeleteButtonHandler(sender As Object, e As EventArgs)
        'Gets the picture box that was hovered over and stores it in a variable
        Dim clothingItem As PictureBox = sender

        'When the user hovers over the clothing item any current controls are removed to ensure one bin icon
        clothingItem.Controls.Clear()

        'Formatting the delete button that will appear on hover
        With deleteButton
            .Image = Image.FromFile(Application.StartupPath & "\Icons\60761.png")
            .Size = New Size(20, 20)
            .SizeMode = PictureBoxSizeMode.Zoom
            'Choosing the location of the button depending on the width of the picture box so it will always be in top corner
            .Location = New Point(clothingItem.Width - 25, 5)
            .BackColor = Color.Transparent
            'This adds the imagepath of the clothing item to the tag of the bin icon so that when clicked the program knows which image is being deleted
            .Tag = clothingItem.Tag
        End With
        'This sets the interval for how long the button will stay for
        deleteButtonTimer.Interval = 1500
        AddHandler deleteButtonTimer.Tick, AddressOf TimerEnd

        'Handler is now only added to the bin icon when the clothing item is hovered over
        'So there is only ever one handler for this event running at one time on one bin icon
        AddHandler deleteButton.Click, AddressOf DeleteImage
        AddHandler clothingItem.MouseMove, AddressOf showIcon
        clothingItem.Controls.Add(deleteButton)
        deleteButton.Show()
    End Sub

    Private Sub TimerEnd()
        deleteButton.Visible = False
        deleteButtonTimer.Stop()
    End Sub

    Private Sub showIcon()
        deleteButton.Visible = True
        deleteButtonTimer.Start()
    End Sub

    Public Sub DeleteImage(sender As Object, e As EventArgs)
        deleteButtonTimer.Stop()
        'Sender is the delete button and the program needs to be able to access the tag of the clothingItem
        'This finds the parent of the deleteButton so the image can be accessed
        deleteButton = sender
        Dim imagePath As String = deleteButton.Tag
        Dim wardrobeID As Integer = checkForWardrobe()


        Dim connection = createConnection()
        connection.open()
        Dim command As New OleDbCommand("DELETE FROM [IMAGE] WHERE [ImagePath] = @imagePath", connection)
        command.Parameters.AddWithValue("@imagePath", imagePath)

        command.ExecuteNonQuery()
        connection.close()

        'Adding the imagepath to the list so that it does not get added to the wardrobe as a picturebox
        deletedItems.Add(imagePath)

        'Refreshing the wardrobe
        populateWardrobe(wardrobeID)

    End Sub

    'This subroutine retrieves any previous applied filters from the database so that they can be displayed.
    Public Sub RetrieveFilters(ByVal ImagePath As String)
        findImageID(ImagePath)
        'THIS SECTION RETRIEVES THE IMAGEID USING THE IMAGEPATH
        Dim ImageID As Integer
        'Creating a connection to the database
        Dim connection = createConnection()
        connection.open()

        Dim sql As String = "SELECT ImageID FROM [IMAGE] WHERE ImagePath = @ImagePath"
        Dim command As New OleDbCommand(sql, connection)
        command.Parameters.AddWithValue("@ImagePath", ImagePath)

        Dim dr As OleDbDataReader
        dr = command.ExecuteReader
        'moves it onto the first row
        dr.Read()
        If dr.HasRows Then
            'Saves the imageID to a local variable
            ImageID = dr("ImageID")
            'closes all the connections

        End If
        dr.Close()

        'RETRIEVING FILTERS

        sql = "SELECT Category, SubCategory, Colour, Material, Brand, Pattern FROM CLOTHINGITEM WHERE WardrobeID = @WardrobeID AND ImageID = @ImageID"

        command = New OleDbCommand(sql, connection)

        Dim WardrobeID As Integer = checkForWardrobe()
        command.Parameters.AddWithValue("@WardrobeID", WardrobeID)
        command.Parameters.AddWithValue("@ImageID", ImageID)
        'Creating a new datareader
        Dim reader As OleDbDataReader
        reader = command.ExecuteReader

        'These variables are just being used to test if this works.
        Dim Category As String
        Dim SubCategory As String
        Dim Colour As String
        Dim Material As String
        Dim Brand As String
        Dim Pattern As String

        If reader.Read() Then ' Read the first matching row
            'This finds the data from the position in the reader
            Category = reader(0).ToString()
            SubCategory = reader(1).ToString
            Colour = reader(2).ToString
            Material = reader(3).ToString
            Brand = reader(4).ToString
            Pattern = reader(5).ToString
        End If

        ' Close the DataReader and connection
        reader.Close()
        connection.Close()


        'If there is any data retrieved it is sent to the clothingdetails form to be displayed
        If Category <> "" And Category <> "N" Then
            ClothingDetails.Category = Category
        End If

        If SubCategory <> "" And SubCategory <> "N" Then
            ClothingDetails.SubCategory = SubCategory
        End If

        If Colour <> "" And Colour <> "N" Then
            ClothingDetails.Colour = Colour
        End If

        If Material <> "" And Material <> "N" Then
            ClothingDetails.Material = Material
        End If

        If Brand <> "" And Brand <> "N" Then
            ClothingDetails.Brand = Brand
        End If

        If Pattern <> "" And Pattern <> "N" Then
            ClothingDetails.Pattern = Pattern
        End If
    End Sub
End Class
