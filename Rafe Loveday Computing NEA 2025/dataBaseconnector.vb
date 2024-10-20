Imports System.Data.OleDb

Public Class dataBaseconnector


    Public Function createConnection()
        'Creating a variable to hold the connection string to the access database
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Owner\OneDrive - Maidstone Grammar School\A Level NEAs\Computing\My nea\Rafe Loveday Computing NEA 2025\Rafe Loveday Computing NEA 2025\DatabaseFile\DigitalWardrobe.accdb"
        'Creating a new instance of the oleDBConnection object with the connection string as a parameter
        Dim connection As New OleDbConnection(connectionString)
        Return connection
    End Function


    Public Sub Insert(ByVal data As String, ByVal fieldName As String)
        'Runs the createConnection function and the returned value is stored into this subroutine
        Dim connection = createConnection()
        'Opens the connection between my program and the database
        connection.open()
        'Making the sql command
        Dim command As New OleDbCommand($"INSERT INTO CLOTHINGITEM ({fieldName}) VALUES (@data)", connection)
        'This uses a parameterized query to prevent any potential sql injections
        command.Parameters.AddWithValue("@data", data)
        'Executing the command to put the information into the database
        command.ExecuteNonQuery()
    End Sub
End Class
