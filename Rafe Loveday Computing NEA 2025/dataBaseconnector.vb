Imports System.Data.OleDb

Public Class dataBaseconnector
    'Creating a variable to hold the connection string to the access database which is private so that it can only be accessed inside the object
    Private connectionString As String = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\Owner\OneDrive - Maidstone Grammar School\A Level NEAs\Computing\My nea\Rafe Loveday Computing NEA 2025\Rafe Loveday Computing NEA 2025\DatabaseFile\DigitalWardrobe.accdb"


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


    Public Sub Insert(ByVal data As String, ByVal fieldName As String, ByVal tableName As String)
        'Runs the createConnection function and the returned value is stored into this subroutine
        Dim connection = createConnection()
        'Opens the connection between my program and the database
        connection.open()
        'Making the sql command
        Dim command As New OleDbCommand($"INSERT INTO [{tableName}] ([{fieldName}]) VALUES (@data)", connection)
        'This uses a parameterized query to prevent any potential sql injections
        command.Parameters.AddWithValue("@data", data)
        'Executing the command to put the information into the database
        command.ExecuteNonQuery()
    End Sub

    Public Sub checkForWardrobe()
        'Runs a function to retrieve instance of oledbconnection object
        Dim connection = createConnection()
        connection.open()
        Dim da As New OleDbDataAdapter
        Dim sql As String = "SELECT WardrobeID FORM WARDROBE WHERE WardrobeName = 'Default Wardrobe"
        'This handles retrieving the wardrobeID if Default Wardrobe is stored in WardrobeName to determine whether there is an existing wardrobe
        Dim command As New OleDbCommand(sql, connection)




    End Sub
End Class
