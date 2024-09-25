Imports System.Data.OleDb

Public Class ClothingDetails
    'Variable used to show if there is already a filter open
    Dim filterOpen As Boolean = False
    'Variable to record the last drop down menu which is open
    Dim lastOpen As Panel = Nothing


    Dim con As OleDbConnection = New OleDbConnection

    'Making variables so that data from database can be copied to the variables upon load.
    Dim Catagory As String
    Dim SubCatagory As String
    Dim colour As String
    Dim material As String
    Dim pattern As String

    Private Sub openPanel(ByVal panel As Panel)
        'This finds out whether the panel is already visible or not 
        If panel.Visible = True Then
            'If it is visible it will make the panel disappear when it is clicked
            panel.Hide()
            'Sets the variable to false to indicate that there are no panels open
            filterOpen = False
        Else
            'This finds out if there is a different panel open and if there is it will hide it so that the new one can be shown
            If filterOpen = True Then
                lastOpen.Hide()
            End If
            'SHows the panel if it is not already open
            panel.Show()
            'indicates in a variable that this is the panel which is open and in another variable that there is a panel open
            lastOpen = panel
            filterOpen = True
        End If
    End Sub


    Private Sub CMDCatagory_Click(sender As Object, e As EventArgs) Handles CMDCatagory.Click
        openPanel(CatagoryPanel)
    End Sub

    Private Sub CMDSubCatagory_Click(sender As Object, e As EventArgs) Handles CMDSubCatagory.Click
        openPanel(SubCatagoryPanel)
    End Sub

    Private Sub CMDColour_Click(sender As Object, e As EventArgs) Handles CMDColour.Click
        openPanel(ColourPanel)
    End Sub

    Private Sub CMDMaterial_Click(sender As Object, e As EventArgs) Handles CMDMaterial.Click
        openPanel(MaterialPanel)
    End Sub

    Private Sub CMDPattern_Click(sender As Object, e As EventArgs) Handles CMDPattern.Click
        openPanel(PatternPanel)
    End Sub
End Class