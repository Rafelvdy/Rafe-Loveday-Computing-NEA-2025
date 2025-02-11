Imports System.Text.RegularExpressions

Public Class ValidateBrand
    Inherits URLFinder

    Private _brand As String
    Public valid As Boolean

    Public Property brand As String
        Set(value As String)
            _brand = value
            'This sets the keyword in the base class to the brand name 
            MyBase.keyWord = _brand
        End Set
        Get
            Return _brand
        End Get
    End Property

    Public Overrides Sub GetURLs()
        MyBase.getURLs()

        Dim titleResults As String = Me.TitleResult
        CheckTitles(titleResults)
    End Sub

    'This subroutine takes the titles retrieved using the overriden GetURLs sub, and then will count through them until 5 of the titles include the brand name
    'This will validate if the brand entered is real or not
    Private Sub CheckTitles(ByVal titleResults As String)
        If titleResults <> Nothing Then
            'Setting the results to lowercase so that they can be compared with the brand without capital letters effecting it
            titleResults = titleResults.ToLower
            'Making a copy of brand so that i can make it lowercase without _brand being effected elsewhere
            Dim brand = _brand.ToLower()

            'This is the the structure of the string i am looking for using the regular expression
            Dim pattern As String = $""".*{brand}.*"""
            'This searches through title results for all of the matches
            Dim matches As MatchCollection = Regex.Matches(titleResults, pattern)
            If matches.Count >= 5 Then
                valid = True
            Else
                valid = False
            End If
        ElseIf titleResults = Nothing Then
            valid = False
        End If
    End Sub
End Class
