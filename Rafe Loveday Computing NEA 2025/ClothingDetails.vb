Imports System.Text.RegularExpressions
Imports System.Windows.Automation

Public Class ClothingDetails
    Private _currentImagePath As String

    Public Property currentImagePath As String
        Set(value As String)
            _currentImagePath = value
        End Set
        Get
            Return _currentImagePath
        End Get
    End Property

    'This variable is used to store the state of the price filtering
    'By default it shows low to high
    '1 - low to high
    '2 - high to low
    '3 - low to high all results
    Dim priceState As Integer = 1

    'Stores the data pulled from API
    Dim titles As String
    Dim prices As String
    Dim urls As String

    'Variables which store the colour of the selected and unselected buttons
    Private _unselectedColour As Color = Color.FromArgb(53, 59, 72)
    Private _selectedColour As Color = Color.FromArgb(72, 126, 176)

    'Variable used to show if there is already a filter open
    Dim filterOpen As Boolean = False
    'Variable to record the last drop down menu which is open
    Dim lastOpen As Panel = Nothing

    'Making variables so that data from database can be copied to the variables upon load.
    Private _Category As String = Nothing
    Private _SubCategory As String = Nothing
    Private _colour As String = Nothing
    Private _material As String = Nothing
    Private _pattern As String = Nothing
    Private _brand As String = Nothing

    Public Property Category As String
        Set(value As String)
            _Category = value
        End Set
        Get
            Return _Category
        End Get
    End Property

    Public Property SubCategory As String
        Set(value As String)
            _SubCategory = value
        End Set
        Get
            Return _SubCategory
        End Get
    End Property

    Public Property Colour As String
        Set(value As String)
            _colour = value
        End Set
        Get
            Return _colour
        End Get
    End Property

    Public Property Material As String
        Set(value As String)
            _material = value
        End Set
        Get
            Return _material
        End Get
    End Property

    Public Property Pattern As String
        Set(value As String)
            _pattern = value
        End Set
        Get
            Return _pattern
        End Get
    End Property

    Public Property Brand As String
        Set(value As String)
            _brand = value
        End Set
        Get
            Return _brand
        End Get
    End Property

    'A dictionary created to store which sub-categories are associated with each category
    Dim associatedCategories As New Dictionary(Of String, List(Of String)) From {
        {"Outwear", New List(Of String) From {"Jackets", "Coats", "Gilets"}},
        {"Jumpers", New List(Of String) From {"Hoodies", "Zipup", "knit"}},
        {"T-Shirt", New List(Of String) From {"Tshirt", "Shirt"}},
        {"Trousers", New List(Of String) From {"Jeans", "Chinos", "Joggers", "Cargos", "Corduroy", "Other"}},
        {"Shorts", New List(Of String) From {"Cargo", "Chino", "Denim", "Sports", "Jersey"}}
    }

    Dim panelTomainButton As New Dictionary(Of String, String) From {
        {"CatagoryPanel", "CMDCatagory"},
        {"SubCatagoryPanel", "CMDSubCatagory"},
        {"ColourPanel", "CMDColour"},
        {"MaterialPanel", "CMDMaterial"},
        {"PatternPanel", "CMDPattern"}}


    Private Sub insertToDB(filter, fieldName)
        'instantiates teh dataBaseconnecter class to an object
        Dim myDBConnector As New dataBaseconnector
        'Calls the required method, passing the data and table name for the data to go to
        'myDBConnector.Insert(filter, fieldName)
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
        Dim listOfsubCategories As List(Of String) = associatedCategories(_Category)
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
            'This IF statement allows it so if a button is selected, when they are created it will stay highlighted
            If newButton.Text = _SubCategory Then
                newButton.BackColor = _selectedColour
            Else
                newButton.BackColor = _unselectedColour
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
        If _Category <> Nothing Then
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

    Private Sub CMDBrand_Click(sender As Object, e As EventArgs) Handles CMDBrand.Click
        openPanel(BrandPanel)
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

        'This subroutine will run when clothingDetails loads, so that it can retrieve any filters
        Dim myDBCon As New dataBaseconnector
        myDBCon.RetrieveFilters(_currentImagePath)

        'This is used to indicate any previous selected filters when the form loads
        If _Category <> "" And _Category <> "N" Then
            'This changes the main options text to the selected text
            CMDCatagory.Text = _Category
            'This compares each button in the CatagoryPanel to the selected category so it can find which button to change colour.
            For Each ctrl As Control In CatagoryPanel.Controls
                If ctrl.Text = _Category Then
                    ctrl.BackColor = _selectedColour
                End If
            Next
        End If

        If _SubCategory <> "" And _SubCategory <> "N" Then
            'This changes the main options text to the selected text
            CMDSubCatagory.Text = _SubCategory
            'This compares each button in the CatagoryPanel to the selected category so it can find which button to change colour.
            For Each ctrl As Control In SubCatagoryPanel.Controls
                If ctrl.Text = _SubCategory Then
                    ctrl.BackColor = _selectedColour
                End If
            Next
        End If

        If _colour <> "" And _colour <> "N" Then
            'This changes the main options text to the selected text
            CMDColour.Text = _colour
            'This compares each button in the CatagoryPanel to the selected category so it can find which button to change colour.
            For Each ctrl As Control In ColourPanel.Controls
                If ctrl.Text = _colour Then
                    ctrl.BackColor = _selectedColour
                End If
            Next
        End If

        If _material <> "" And _material <> "N" Then
            'This changes the main options text to the selected text
            CMDMaterial.Text = _material
            'This compares each button in the CatagoryPanel to the selected category so it can find which button to change colour.
            For Each ctrl As Control In MaterialPanel.Controls
                If ctrl.Text = _material Then
                    ctrl.BackColor = _selectedColour
                End If
            Next
        End If

        If _pattern <> "" And _pattern <> "N" Then
            'This changes the main options text to the selected text
            CMDPattern.Text = _pattern
            'This compares each button in the CatagoryPanel to the selected category so it can find which button to change colour.
            For Each ctrl As Control In PatternPanel.Controls
                If ctrl.Text = _pattern Then
                    ctrl.BackColor = _selectedColour
                End If
            Next
        End If


    End Sub

    Private Sub filterPress(sender As Object, e As EventArgs)
        'This saves the button that was pressed into a local variable
        Dim buttonClicked As Button = sender
        'Saves the parent of the button to a local variable
        Dim buttonsPanel As Panel = buttonClicked.Parent
        'THIS CODE HANDLES THE COLOUR OF THE BUTTONS WHEN SELECTING AND DESELECTING
        'Checking if the clicked button is already selected so that if it is, it can be reverted and the variable set back to nothing
        If buttonClicked.BackColor = _selectedColour Then
            'If the button is selected already and then pressed again, its colour will be reverted back
            buttonClicked.BackColor = _unselectedColour
            'THIS CODE HANDLES WHAT IS SAVED IN THE VARIABLES WHEN BUTTONS ARE SELECTED AND DESELECTED
            'If the button clicked is already the selected colour, then the variable associated with it is changed back to nothing
            Select Case buttonsPanel.Name
                Case "CatagoryPanel"
                    _Category = Nothing
                    'When the button is deselected it will change the text of the button back to its original text
                    CMDCatagory.Text = "Category"
                Case "SubCatagoryPanel"
                    _SubCategory = Nothing
                    CMDSubCatagory.Text = "Sub Category"
                Case "ColourPanel"
                    _colour = Nothing
                    CMDColour.Text = "Colour"
                Case "MaterialPanel"
                    _material = Nothing
                    CMDMaterial.Text = "Material"
                Case "PatternPanel"
                    _pattern = Nothing
                    CMDPattern.Text = "Pattern"
            End Select
        Else
            'If the colour is not selected it will make all the buttons in the panel become unselected so the new one can be selected
            'This for loop goes through each button inside of the panel which is being used
            For Each control As Control In buttonsPanel.Controls
                'If any of the buttons are selected they will be set back to the original colour
                If control.BackColor = _selectedColour Then
                    control.BackColor = _unselectedColour
                End If
            Next
            'This will set the button that was pressed to the selected colour
            buttonClicked.BackColor = _selectedColour
            'This case statement will compare the name of the panel to the panels i have to determine which panel the buttons are apart of
            Select Case buttonsPanel.Name
                Case "CatagoryPanel"
                    'Will save the filter chosen to the appropriate variable
                    _Category = buttonClicked.Text
                    'When the button is selected, it will change the text of the category button
                    CMDCatagory.Text = buttonClicked.Text
                Case "SubCatagoryPanel"
                    _SubCategory = buttonClicked.Text
                    CMDSubCatagory.Text = buttonClicked.Text
                Case "ColourPanel"
                    _colour = buttonClicked.Text
                    CMDColour.Text = buttonClicked.Text
                Case "MaterialPanel"
                    _material = buttonClicked.Text
                    CMDMaterial.Text = buttonClicked.Text
                Case "PatternPanel"
                    _pattern = buttonClicked.Text
                    CMDPattern.Text = buttonClicked.Text
            End Select
        End If
    End Sub

    'This subroutine handles finding URLs depending on the chosen inputs
    Private Sub CMDFindURLs_Click(sender As Object, e As EventArgs) Handles CMDFindURLs.Click
        'This IF statement makes a minimum requirement for the amount of data that must be entered
        If _Category = Nothing Or _SubCategory = Nothing Or _colour = Nothing Or _material = Nothing Then
            MessageBox.Show("Please make sure you have selected a filter for category, subcategory, colour and material!")
        Else
            'This constructs the keyword string that is entered for the API
            Dim keyword As String = _brand & " " & _Category & " " & _SubCategory & " " & _colour & " " & _material & " " & _pattern
            Dim myURLFinder As New URLFinder
            myURLFinder.keyWord = keyword
            myURLFinder.getURLs()
            titles = myURLFinder.TitleResult
            urls = myURLFinder.URLResult
            prices = myURLFinder.PriceResult
            DisplayURLResults(titles, prices, urls)
            Dim myDB As New dataBaseconnector
            myDB.Insert(_currentImagePath, Nothing, "CLOTHINGITEM", False, Nothing, _Category, _SubCategory, _colour, _material, _brand, _pattern, Nothing, 0)
        End If
    End Sub

    Private Sub DisplayURLResults(ByVal titles As String, ByVal prices As String, ByVal urls As String)
        Dim defaultView() As Double

        'This splits the results by using the separator that i put in.
        Dim titleArray() As String = titles.Split("|||").Where(Function(t) Not String.IsNullOrWhiteSpace(t)).ToArray()
        Dim urlArray() As String = urls.Split("|||").Where(Function(u) Not String.IsNullOrWhiteSpace(u)).ToArray()
        Dim priceArray() As String = prices.Split("|||").Where(Function(p) Not String.IsNullOrWhiteSpace(p)).ToArray()

        'Showing the panel so that the buttons can be organised inside and resets anything that could be in there
        If ResultsScrollPanel.Visible = False Then
            ResultsScrollPanel.Show()
            priceLabel.Show()
            CMDUpdate.Show()
            SearchBar.Show()
        End If
        ResultsScrollPanel.Controls.Clear()


        'This creates an instance of the searchOrFilter class so that i can use the merge sort on the price array
        Dim mySearchOrFilter As New searchOrFilter

        'This is used for the searching part of the algorithm
        'This IF statement determines whether the user has entered any data into the search bar
        If SearchBar.Text <> "" Then
            Dim results = mySearchOrFilter.containsSearch(titleArray, priceArray, urlArray, SearchBar.Text)
            Dim arraySize = results.item1

            Array.Resize(titleArray, arraySize)
            Array.Resize(priceArray, arraySize)
            Array.Resize(urlArray, arraySize)

            titleArray = results.item2
            priceArray = results.item3
            urlArray = results.item4
        End If


        'Making a variable to store values of integers to be sorted
        'Allows me to convert array from string to integer
        Dim arraytosort(priceArray.Length - 1) As Double
        For i = 0 To priceArray.Count() - 1
            If priceArray(i) <> 0 Then
                arraytosort(i) = CDbl(priceArray(i))
            End If
        Next

        'This is used for the Sorting part of the Algorithm
        Dim low As Integer = 0
        Dim high As Integer = arraytosort.Length - 1
        'This list created is the list in order of price
        defaultView = mySearchOrFilter.mergeSort(arraytosort, low, high)

        Dim toDisplay(9) As Double

        If priceLabel.Text = "Price ↑" Then
            'This is the position of the last element to be copied to the new array
            Dim endIndex As Integer = defaultView.Length - 10
            Dim addPosition As Integer = 0
            'This for loop will go through the sorted array in reverse and copy each element to a new array
            For i = defaultView.Length - 1 To endIndex Step -1
                toDisplay(addPosition) = defaultView(i)
                addPosition += 1
            Next
        ElseIf priceLabel.Text = "Price ↓" Then
            For i = 0 To 9
                toDisplay(i) = defaultView(i)
            Next
        ElseIf priceLabel.Text = "Price -" Then
            'This will allow me to adjust the size of the array which is displayed so that all the results can be displayed
            ReDim toDisplay(defaultView.Length - 1)
            For i = 0 To defaultView.Length - 1
                toDisplay(i) = defaultView(i)
            Next
        End If

        'These variables will handle the spacing between each of the results
        Dim padding As Integer = 10
        Dim currentX As Integer = padding
        'This creates a loop which will iterate the number of times that there are results. 
        'The array is 0-based so i have to take 1 away from the total length
        For i = 0 To toDisplay.Length - 1
            If priceArray(i) <> "" Then
                'This creates a new label on each iteration
                Dim button As New Button
                With button
                    'Tagging the button with the URL so that it can be accessed
                    Dim cleanURL = urlArray(i)
                    With cleanURL
                        .Replace("[", "")
                        .Replace("]", "")
                        .Replace("""", "")
                        .Replace("'", "")
                        .Trim()
                    End With
                    .Tag = cleanURL
                    'Formatting the label so it is the same format for each iteration 
                    .AutoSize = False
                    .Width = 200
                    .Height = 60
                    .BackColor = Color.White
                    .Text = $"{titleArray(i).Replace("[", "").Replace("]", "").Replace("""", "").Trim()}" & Environment.NewLine & $"${toDisplay(i)}"

                    'This places the label according to other labels and the padding
                    .Location = New Point(currentX, padding)
                    .TextAlign = ContentAlignment.MiddleCenter

                    'Makes the label visible in the panel
                    ResultsScrollPanel.Controls.Add(button)

                    'Creates the new x position for the next label
                    currentX += .Width + padding

                    'Adding a handler to the buttons so that the user can display the pages on the browser
                    AddHandler button.Click, AddressOf resultButton
                End With
            End If
        Next
    End Sub

    'This subroutine is ran when one of the buttons which shows the URL results are pressed to display the url in google chrome
    Private Sub resultButton(sender As Object, e As EventArgs)
        Dim buttonClicked As Button = sender
        Dim url As String = buttonClicked.Tag.ToString()
        If buttonClicked.BackColor = Color.White Then
            buttonClicked.BackColor = Color.Gray
            ' Clean the URL
            url = url.Replace("[", "").Replace("]", "").Replace("""", "").Trim()
            'Open web page with URL
            Process.Start(url)
        ElseIf buttonClicked.BackColor = Color.Gray Then
            buttonClicked.BackColor = Color.White
        End If
    End Sub

    'Subroutine that will check that the brand the user enters is a real brand
    Private Sub CMDValidateBrand_Click(sender As Object, e As EventArgs) Handles CMDValidateBrand.Click
        Dim myValidation As New ValidateBrand
        'Saves the brand name that is entered to the object using setters and getters.
        myValidation.brand = brandText.Text
        'Runs the subroutine inside of the class to valid if the brand is valid or not
        myValidation.GetURLs()
        'If the brand is valid, it will save the brand name to a variable in this forms, if it is not the variable is set to nothing to clear any previous entries from the field
        If myValidation.valid = True Then
            Brand = myValidation.brand
            'This will make a green box appear under the brand text box if the user enters a valid brand
            isValidPanel.BackColor = Color.Green
            isValidPanel.Visible = True
        Else
            'Variable is reset back to nothing if the brand is invalid
            Brand = Nothing
            'This will make a red box appear under the brand text box if the user enters an invalid brand
            isValidPanel.BackColor = Color.Red
            isValidPanel.Visible = True
        End If
    End Sub

    Private Sub priceLabel_Click() Handles priceLabel.Click
        'This increments the priceState variable, while allowing it to wrap back round 
        priceState = (priceState Mod 3) + 1
        If priceState = 1 Then
            priceLabel.Text = "Price ↓"
        ElseIf priceState = 2 Then
            priceLabel.Text = "Price ↑"
        ElseIf priceState = 3 Then
            priceLabel.Text = "Price -"
        End If
    End Sub

    'This updates the scroll bar that shows the results from the API call.
    Private Sub CMDUpdate_Click(sender As Object, e As EventArgs) Handles CMDUpdate.Click




        DisplayURLResults(titles, prices, urls)
    End Sub
End Class