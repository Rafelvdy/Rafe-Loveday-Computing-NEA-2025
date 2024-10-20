Imports System.Data

Public Class ClothingDetails
    'Variable used to show if there is already a filter open
    Dim filterOpen As Boolean = False
    'Variable to record the last drop down menu which is open
    Dim lastOpen As Panel = Nothing

    'Making variables so that data from database can be copied to the variables upon load.
    Dim Catagory As String = Nothing
    Dim SubCatagory As String = Nothing
    Dim colour As String = Nothing
    Dim material As String = Nothing
    Dim pattern As String = Nothing

    'Database connection variables

    'A dictionary created to store which sub-categories are associated with each category
    Dim associatedCategories As New Dictionary(Of String, List(Of String)) From {
        {"Outwear", New List(Of String) From {"Jackets", "Coats", "Gilets"}},
        {"Jumpers", New List(Of String) From {"Hoodies", "Zipup", "knit"}},
        {"T-Shirts", New List(Of String) From {"Tshirt", "Shirt"}},
        {"Trousers", New List(Of String) From {"Jeans", "Chinos", "Joggers", "Cargos", "Corduroy", "Other"}},
        {"Shorts", New List(Of String) From {"Cargo", "Chino", "Denim", "Sports", "Jersey"}}
    }

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

    'This subroutine is designed to load different buttons into the subcategorypanel depending on which category is chosen.
    Private Sub subCategorybuttons()
        'This code will remove all controls from the panel so the buttons do not layer over each other
        SubCatagoryPanel.Controls.Clear()
        'Creating a variable which is a list to hold the sub category options
        Dim listOfsubCategories As List(Of String) = associatedCategories(Catagory)
        'Finds out how many buttons need to be made depending on the size of the list
        Dim noButtons As Integer = listOfsubCategories.Count
        'This for loop will run as large as the list is so that only the required number of buttons are created
        For i = 0 To (noButtons - 1)
            Dim newButton As New Button
            'Changes the name of the button so that they can be uniquely indentified
            newButton.Name = ("CMD" & listOfsubCategories(i))
            'Adds text to the button of what sub category it is
            newButton.Text = (listOfsubCategories(i))
            newButton.ForeColor = Color.White
            newButton.Font = New Font("Segoe UI Black", 7.8, FontStyle.Bold)
            'Changes the appearance of the buttons so that they match the others
            newButton.FlatStyle = FlatStyle.Flat
            newButton.FlatAppearance.BorderSize = 0
            'Adding the button to the panel
            SubCatagoryPanel.Controls.Add(newButton)
            If i <> 0 Then
                newButton.Location = New Point(0, (i * 25))
            End If
        Next
        'Finds the total height of all the buttons, and then will set the height of the panel to change to it so it dynamically changes
        SubCatagoryPanel.Height = (noButtons * 25)
    End Sub

    Private Sub CMDCatagory_Click(sender As Object, e As EventArgs) Handles CMDCatagory.Click
        openPanel(CatagoryPanel)
    End Sub

    Private Sub CMDSubCatagory_Click(sender As Object, e As EventArgs) Handles CMDSubCatagory.Click
        openPanel(SubCatagoryPanel)
        If Catagory <> Nothing Then
            subCategorybuttons()
        End If

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

    'When these buttons are pressed, they will save their associated clothing items into the catagory panel
    'Can be saved to the database and also be used to choose what to display in the subcategory
    Private Sub CMDOutWear_Click(sender As Object, e As EventArgs) Handles CMDOutWear.Click
        Catagory = "Outwear"

    End Sub
    Private Sub CMDJumpers_Click(sender As Object, e As EventArgs) Handles CMDJumpers.Click
        Catagory = "Jumpers"
    End Sub
    Private Sub CMDTshirt_Click(sender As Object, e As EventArgs) Handles CMDTshirt.Click
        Catagory = "T-Shirts"
    End Sub
    Private Sub CMDTrousers_Click(sender As Object, e As EventArgs) Handles CMDTrousers.Click
        Catagory = "Trousers"
    End Sub
    Private Sub CMDShorts_Click(sender As Object, e As EventArgs) Handles CMDShorts.Click
        Catagory = "Shorts"
    End Sub
End Class