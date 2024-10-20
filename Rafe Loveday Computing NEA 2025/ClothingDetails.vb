Imports System.Windows.Automation

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

    'A dictionary created to store which sub-categories are associated with each category
    Dim associatedCategories As New Dictionary(Of String, List(Of String)) From {
        {"Outwear", New List(Of String) From {"Jackets", "Coats", "Gilets"}},
        {"Jumpers", New List(Of String) From {"Hoodies", "Zipup", "knit"}},
        {"TShirts", New List(Of String) From {"Tshirt", "Shirt"}},
        {"Trousers", New List(Of String) From {"Jeans", "Chinos", "Joggers", "Cargos", "Corduroy", "Other"}},
        {"Shorts", New List(Of String) From {"Cargo", "Chino", "Denim", "Sports", "Jersey"}}
    }

    Private Sub insertToDB(filter, fieldName)
        'instantiates teh dataBaseconnecter class to an object
        Dim myDBConnector As New dataBaseconnector
        'Calls the required method, passing the data and table name for the data to go to
        myDBConnector.Insert(filter, fieldName)
    End Sub

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
            'Using addhandler so that when the buttons are created they can also have the addhandler
            AddHandler newButton.Click, AddressOf filterPress
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
    Private Sub ClothingDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Handles saving the press of category buttons to a variable
        AddHandler CMDOutWear.Click, AddressOf filterPress
        AddHandler CMDJumpers.Click, AddressOf filterPress
        AddHandler CMDTshirt.Click, AddressOf filterPress
        AddHandler CMDTrousers.Click, AddressOf filterPress
        AddHandler CMDShorts.Click, AddressOf filterPress

        'Colour button handlers
        AddHandler CMDColourBlack.Click, AddressOf filterPress
        AddHandler CMDColourWhite.Click, AddressOf filterPress
        AddHandler CMDColourBlue.Click, AddressOf filterPress
        AddHandler CMDColourGrey.Click, AddressOf filterPress
        AddHandler CMDColourMulti.Click, AddressOf filterPress
        AddHandler CMDColourRed.Click, AddressOf filterPress
        AddHandler CMDColourNavy.Click, AddressOf filterPress
        AddHandler CMDColourPink.Click, AddressOf filterPress
        AddHandler CMDColourBeige.Click, AddressOf filterPress
        AddHandler CMDColourCream.Click, AddressOf filterPress
        AddHandler CMDColourGreen.Click, AddressOf filterPress
        AddHandler CMDColourLightBlue.Click, AddressOf filterPress
        AddHandler CMDColourBrown.Click, AddressOf filterPress
        AddHandler CMDColourOrange.Click, AddressOf filterPress
        AddHandler CMDColourYellow.Click, AddressOf filterPress
        AddHandler CMDColourKhaki.Click, AddressOf filterPress
        AddHandler CMDColourPurple.Click, AddressOf filterPress

        'Material button handlers
        AddHandler CMDMaterialCotton.Click, AddressOf filterPress
        AddHandler CMDMaterialLeather.Click, AddressOf filterPress
        AddHandler CMDMaterialLinen.Click, AddressOf filterPress
        AddHandler CMDMaterialWool.Click, AddressOf filterPress
        AddHandler CMDMaterialDenim.Click, AddressOf filterPress
        AddHandler CMDMaterialPolyester.Click, AddressOf filterPress

        'Pattern button handlers
        AddHandler CMDPatternStriped.Click, AddressOf filterPress
        AddHandler CMDPatternChequered.Click, AddressOf filterPress
        AddHandler CMDPatternFloral.Click, AddressOf filterPress
        AddHandler CMDPatternPolka.Click, AddressOf filterPress
        AddHandler CMDPatternAbstract.Click, AddressOf filterPress
        AddHandler CMDPatternGeometric.Click, AddressOf filterPress
    End Sub

    Private Sub filterPress(sender As Object, e As EventArgs)
        'This saves the button that was pressed into a local variable
        Dim buttonClicked As Button = sender
        'Saves the parent of the button to a local variable
        Dim buttonsPanel As Panel = buttonClicked.Parent
        'This case statement will compare the name of the panel to the panels i have to determine which panel the buttons are apart of
        Select Case buttonsPanel.Name
            Case "CatagoryPanel"
                'Will save the filter chosen to the appropriate variable
                Catagory = buttonClicked.Text
                'runs the subroutine to start sending the data to the database
                'insertToDB(Catagory, "Category")
            Case "SubCatagoryPanel"
                SubCatagory = buttonClicked.Text
                'insertToDB(SubCatagory, "SubCategory")
            Case "ColourPanel"
                colour = buttonClicked.Text
                'insertToDB(colour, "Colour")
            Case "MaterialPanel"
                material = buttonClicked.Text
               ' insertToDB(material, "Material")
            Case "PatternPanel"
                pattern = buttonClicked.Text
                ' insertToDB(pattern, "Pattern")
        End Select
    End Sub

    Private Sub CMDFindURLs_Click(sender As Object, e As EventArgs) Handles CMDFindURLs.Click
        If Catagory = Nothing Or SubCatagory = Nothing Or colour = Nothing Or material = Nothing Then
            MessageBox.Show("Please make sure you have selected a filter for category, subcategory, colour and material!")
        Else
            Dim keyword As String = Catagory & " " & SubCatagory & " " & colour & " " & material & " " & pattern
            Dim myURLFinder As New URLFinder
            myURLFinder.keyWord = keyword
            myURLFinder.getURLs()
        End If
    End Sub
End Class