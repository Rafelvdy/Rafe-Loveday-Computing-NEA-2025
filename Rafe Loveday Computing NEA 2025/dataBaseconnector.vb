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

    Private _LeastStatsImagePath As String
    Private _MostStatsImagePath As String

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

    Public Property LeastStatsImagePath As String
        Set(value As String)
            _LeastStatsImagePath = value
        End Set
        Get
            Return _LeastStatsImagePath
        End Get
    End Property

    Public Property MostStatsImagePath As String
        Set(value As String)
            _MostStatsImagePath = value
        End Set
        Get
            Return _MostStatsImagePath
        End Get
    End Property

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
            sql = $"UPDATE CLOTHINGITEM SET Category = @Category, SubCategory = @SubCategory, Colour = @Colour, Material = @Material, Brand = @Brand, Pattern = @Pattern, WearFrequency = @WearFrequency, NoOfWears = 0 WHERE WardrobeID = @WardrobeID AND ImageID = @ImageID"
            command = New OleDbCommand(sql, connection)

            If Category <> Nothing Then
                command.Parameters.AddWithValue("@Category", Category)
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

    Public Function loadCategory(ByVal Category As String)
        Dim ImagePathList As New ArrayList
        Dim WardrobeID As Integer = checkForWardrobe()

        'Creating DB connection
        Dim connection = createConnection()
        connection.open()
        Dim command As New OleDbCommand
        Dim sql As String

        'Checking for bottoms as this will require sql statement to look for both trousers and shorts
        If Category = "Bottoms" Then
            sql = "SELECT [IMAGE].[ImagePath] FROM [IMAGE] INNER JOIN [CLOTHINGITEM] On [IMAGE].[ImageID] = [CLOTHINGITEM].[ImageID] WHERE [CLOTHINGITEM].[WardrobeID] = @WardrobeID AND [CLOTHINGITEM].[Category] = 'Trousers' OR [CLOTHINGITEM].[Category] = 'Shorts'"
            command = New OleDbCommand(sql, connection)
            command.Parameters.AddWithValue("@WardrobeID", WardrobeID)
        Else
            sql = "SELECT [Image].[ImagePath] FROM [IMAGE] INNER JOIN [CLOTHINGITEM] On [IMAGE].[ImageID] = [CLOTHINGITEM].[ImageID] WHERE [CLOTHINGITEM].[WardrobeID] = @WardrobeID And [CLOTHINGITEM].[Category] = @Category"
            command = New OleDbCommand(sql, connection)
            command.Parameters.AddWithValue("@WardrobeID", WardrobeID)
            command.Parameters.AddWithValue("@Category", Category)
        End If

        Dim dr As OleDbDataReader = command.ExecuteReader()
        'This checks if there is any data to display
        If dr.HasRows() Then
            While dr.Read()
                Dim ImagePath As String = dr("ImagePath")
                ImagePathList.Add(ImagePath)
            End While
        End If
        Return ImagePathList
        dr.Close()
    End Function


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

    Public Function findImageID(ByVal ImagePath As String)
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

    Public Sub wearOutfit(ByVal ImagePath As String)
        'creating connection to the database
        Dim connection = createConnection()
        connection.open()

        'Finding the data needed for the sql query
        Dim WardrobeID As Integer = checkForWardrobe()
        Dim ImageID As Integer = findImageID(ImagePath)

        'Building the sql query
        Dim sql As String = "UPDATE CLOTHINGITEM SET LastWornDate=@LastWornDate, NoOfWears = NoOfWears + 1 WHERE WardrobeID = @WardrobeID AND ImageID = @ImageID"
        Dim command As New OleDbCommand(sql, connection)

        Dim todaysDate As String = DateString
        Dim dateParts() As String = todaysDate.Split("-")

        Dim month As String = dateParts(0)
        Dim day As String = dateParts(1)
        Dim year As String = dateParts(2)

        'Adding the data to the SQL statement
        command.Parameters.AddWithValue("@LastWornDate", day & "/" & month & "/" & year)
        command.Parameters.AddWithValue("@WardrobeID", WardrobeID)
        command.Parameters.AddWithValue("ImageID", ImageID)

        command.ExecuteNonQuery()
        connection.close()
    End Sub

    'Finds a non worn clothing item and least worn clothing item to display
    Public Function getClothingSuggestion(ByVal Category As String, ByVal field As String)
        'creating a connection to the database
        Dim connection = createConnection()
        connection.open()
        Dim sql As String
        Dim command As New OleDbCommand
        'This finds the wardrobe ID and stores it in the variable
        Dim wardrobeID As Integer = checkForWardrobe()
        If field = "LastWornDate" Then
            If Category = "Bottoms" Then
                sql = "SELECT [IMAGE].[ImagePath] FROM [IMAGE] INNER JOIN [CLOTHINGITEM] ON [IMAGE].[ImageID] = [CLOTHINGITEM].[ImageID] WHERE [WardrobeID] = @WardrobeID AND [Category] = 'Trousers' OR [Category] = 'Shorts' AND LastWornDate IS NOT NULL ORDER BY LastWornDate ASC"
            Else
                sql = "SELECT [IMAGE].[ImagePath] FROM [IMAGE] INNER JOIN [CLOTHINGITEM] ON [IMAGE].[ImageID] = [CLOTHINGITEM].[ImageID] WHERE [WardrobeID] = @WardrobeID AND [Category] = @Category AND LastWornDate IS NOT NULL ORDER BY LastWornDate ASC"
            End If
            command = New OleDbCommand(sql, connection)
            'Adding the variable parameter to the sql statement
            command.Parameters.AddWithValue("@WardrobeID", wardrobeID)
            command.Parameters.AddWithValue("@Category", Category)
        ElseIf field = "NoOfWears" Then
            If Category = "Bottoms" Then
                sql = "SELECT [IMAGE].[ImagePath] FROM [IMAGE] INNER JOIN [CLOTHINGITEM] ON [IMAGE].[ImageID] = [CLOTHINGITEM].[ImageID] WHERE [WardrobeID] = @WardrobeID And [Category] = 'Trousers' OR [Category] = 'Shorts' AND NoOfWears = 0"
            Else
                sql = "SELECT [IMAGE].[ImagePath] FROM [IMAGE] INNER JOIN [CLOTHINGITEM] ON [IMAGE].[ImageID] = [CLOTHINGITEM].[ImageID] WHERE [WardrobeID] = @WardrobeID And [Category] = @Category AND NoOfWears = 0"
            End If
            command = New OleDbCommand(sql, connection)

            command.Parameters.AddWithValue("@WardrobeID", wardrobeID)
            command.Parameters.AddWithValue("@Category", Category)
        End If
        'Reads the data
        Dim reader As OleDbDataReader
        reader = command.ExecuteReader()
        If reader.HasRows Then
            'Moves the reader onto the first line 
            reader.Read()
            'Will output the date longest ago
            Return (reader(0).ToString)
        Else
            Return ""
        End If
    End Function

    Public Sub retrieveStatistics(ByVal tableName As String, ByVal fieldName As String)
        Dim connection = createConnection()
        connection.open()

        Dim command As New OleDbCommand
        Dim sql As String
        If tableName = "CLOTHINGITEM" Then
            'THIS WILL OCCUPY THE COLOUR STATISTICS BOTH LEAST WORN AND MOST WORN
            If fieldName = "Colour" Then 'This will handle the sql statement for the colour statistic
                'This first part is showing the least worn colour
                sql = "SELECT Colour FROM CLOTHINGITEM ORDER BY NoOfWears ASC"
                command = New OleDbCommand(sql, connection)
                Dim dr As OleDbDataReader
                dr = command.ExecuteReader
                'This will retrieve the relevent data if there is any
                If dr.HasRows Then
                    While dr.Read()
                        If Not IsDBNull(dr("Colour")) Then
                            'This will be the colour of the panel for this stat
                            Dim Colour As String = dr("Colour")
                            Dim newColour As Color = Color.FromName(Colour)
                            'This is setting the appearance and data into the stats form
                            UserStats.LeastColourPanel.BackColor = newColour
                            UserStats.LeastColourPanel2.BackColor = newColour
                            UserStats.WearLeastLabel.Text = Colour
                            If Colour = "White" Then
                                UserStats.WearLeastLabel.ForeColor = Color.Black
                            End If
                            Exit While
                        End If
                    End While
                End If

                'Using same strcuture as above, but to show most worn colour instead
                sql = "SELECT Colour FROM CLOTHINGITEM ORDER BY NoOfWears DESC"
                command = New OleDbCommand(sql, connection)
                dr = command.ExecuteReader
                If dr.HasRows Then
                    While dr.Read()
                        If Not IsDBNull(dr("Colour")) Then
                            Dim Colour As String = dr("Colour")
                            Dim newColour As Color = Color.FromName(Colour)

                            UserStats.MostColourPanel.BackColor = newColour
                            UserStats.MostColourPanel2.BackColor = newColour
                            UserStats.WearMostLabel.Text = Colour
                            If Colour = "White" Then
                                UserStats.WearLeastLabel.ForeColor = Color.Black
                            End If
                            Exit While
                        End If
                    End While
                End If
                connection.close()

            ElseIf fieldName = "Material" Then 'This will handle the sql statement for the material statistic
                sql = "SELECT Material FROM CLOTHINGITEM ORDER BY NoOfWears DESC"
                command = New OleDbCommand(sql, connection)
                Dim dr As OleDbDataReader
                dr = command.ExecuteReader
                'This will retrieve the relevent data if there is any
                If dr.HasRows Then
                    While dr.Read()
                        If Not IsDBNull(dr("Material")) Then
                            'This will be the colour of the panel for this stat
                            Dim Material As String = dr("Material")

                            'Changes the label to show the material selected
                            UserStats.LeastMaterialLabel.Text = Material
                            Exit While
                        End If
                    End While
                End If

                'Using same strcuture as above, but to show most worn material instead
                sql = "SELECT Material FROM CLOTHINGITEM ORDER BY NoOfWears ASC"
                command = New OleDbCommand(sql, connection)
                dr = command.ExecuteReader

                If dr.HasRows Then
                    While dr.Read()
                        If Not IsDBNull(dr("Material")) Then
                            Dim Material As String = dr("Material")
                            UserStats.MostMaterialLabel.Text = Material
                            Exit While
                        End If
                    End While
                End If
                connection.close()
            End If

        ElseIf tableName = "IMAGE" Then

            If fieldName = "ImagePath" Then 'This will handle the sql statement for the least and most worn clothing item
                sql = "SELECT [IMAGE].[ImagePath] FROM [CLOTHINGITEM] INNER JOIN [IMAGE] On [CLOTHINGITEM].[ImageID] = [IMAGE].[ImageID] ORDER BY [CLOTHINGITEM].[NoOfWears] ASC"
                command = New OleDbCommand(sql, connection)
                Dim dr As OleDbDataReader
                dr = command.ExecuteReader

                If dr.HasRows Then
                    dr.Read()
                    'This accesses the first record that is taken from the datab
                    Dim ImagePath As String = dr(0)
                    _LeastStatsImagePath = ImagePath
                End If

                sql = "SELECT [IMAGE].[ImagePath] FROM [CLOTHINGITEM] INNER JOIN [IMAGE] On [CLOTHINGITEM].[ImageID] = [IMAGE].[ImageID] ORDER BY [CLOTHINGITEM].[NoOfWears] DESC"
                command = New OleDbCommand(sql, connection)
                dr = command.ExecuteReader

                If dr.HasRows Then
                    dr.Read()
                    'This accesses the first record that is taken from the datab
                    Dim ImagePath As String = dr(0)
                    _MostStatsImagePath = ImagePath
                End If

            End If
        End If
    End Sub

    Public Sub createLeastWorn()
        Dim Category As New List(Of String)({"Outwear", "Jumpers", "T-Shirt", "Trousers", "Shorts"})
        'THis will hold the values in adjacent indexs in the list
        Dim CategoryImagePaths As New List(Of String)({"", "", "", "", ""})
        Dim index As Integer = 0

        'Variables for the image paths to be saved to
        Dim LeastOutwear As String = ""
        Dim LeastJumpers As String = ""
        Dim LeastTShirt As String = ""
        Dim LeastTrousers As String = ""
        Dim LeastShorts As String = ""

        Dim connection = createConnection()
        connection.open()
        Dim sql As String
        Dim command As New OleDbCommand
        Dim dr As OleDbDataReader

        'This loops through each category and saves the image path to an adjacent index in a different list.
        For Each item In Category
            sql = "SELECT [IMAGE].[ImagePath] FROM [CLOTHINGITEM] INNER JOIN [IMAGE] On [CLOTHINGITEM].[ImageID] = [IMAGE].[ImageID] WHERE [CLOTHINGITEM].[Category] = @Category ORDER BY [CLOTHINGITEM].[NoOfWears] ASC"
            command = New OleDbCommand(sql, connection)
            command.Parameters.AddWithValue("@Category", item)

            dr = command.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                'Storing value in an adjacent index
                CategoryImagePaths(index) = dr(0)
                index += 1
            End If
        Next

        LeastOutwear = CategoryImagePaths(0)
        If LeastOutwear <> "" Then
            UserStats.LeastWornOutwear.Image = Image.FromFile(LeastOutwear)
        End If

        LeastJumpers = CategoryImagePaths(1)
        If LeastJumpers <> "" Then
            UserStats.LeastWornJumpers.Image = Image.FromFile(LeastJumpers)
        End If

        LeastTShirt = CategoryImagePaths(2)
        If LeastTShirt <> "" Then
            UserStats.LeastWornTShirt.Image = Image.FromFile(LeastTShirt)
        End If

        LeastTrousers = CategoryImagePaths(3)
        If LeastTrousers <> "" Then
            UserStats.LeastWornTrousers.Image = Image.FromFile(LeastTrousers)
        End If

        LeastShorts = CategoryImagePaths(4)
        If LeastShorts <> "" Then
            UserStats.LeastWornShorts.Image = Image.FromFile(LeastShorts)
        End If
        connection.close()
    End Sub

    Public Sub insertURL(ByVal url As String, ByVal ImagePath As String)
        Dim ImageID As Integer = findImageID(ImagePath)
        Dim connection = createConnection()
        connection.open()

        Dim sql As String = "UPDATE [IMAGE] SET [ImageURL] = @url WHERE [ImageID] = @ImageID"
        Dim command As New OleDbCommand(sql, connection)

        command.Parameters.AddWithValue("@url", url)
        command.Parameters.AddWithValue("@ImageID", ImageID)

        command.ExecuteNonQuery()
        connection.close()
    End Sub

    'Retrieves the url from the database of the selected image
    Public Function retrieveURL(ByVal ImagePath As String)
        'creating a connection to the database
        Dim connection = createConnection()
        connection.open()

        'This finds the image id of the current selected image
        Dim ImageID As Integer = findImageID(ImagePath)

        'Creating the sql query and adding the parameters
        Dim sql As String = "SELECT [ImageURL] FROM [IMAGE] WHERE [ImageID] = @ImageID"
        Dim command As New OleDbCommand(sql, connection)
        command.Parameters.AddWithValue("@ImageID", ImageID)

        'Creating a variable to store the url
        Dim url As String

        'Reads the data only if there are rows to read
        Dim dr As OleDbDataReader
        dr = command.ExecuteReader
        If dr.HasRows Then
            While dr.Read()
                If Not IsDBNull(dr("ImageURL")) Then
                    url = dr("ImageURL")
                    Return url
                    Exit While

                End If
            End While
        Else
            Return Nothing
        End If
    End Function
End Class
