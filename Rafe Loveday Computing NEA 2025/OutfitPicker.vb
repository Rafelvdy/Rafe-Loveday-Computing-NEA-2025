Public Class OutfitPicker
    Dim Category As String = ""


    Private Sub CMDChooseOutwear_Click(sender As Object, e As EventArgs) Handles CMDChooseOutwear.Click
        Category = "Outwear"
        Dim myDbcon As New dataBaseconnector
        Dim ImagePathList As New ArrayList
        ImagePathList = myDbcon.loadCategory(Category)
        displayImagePaths(ImagePathList)
    End Sub

    Private Sub CMDChooseJumpers_Click(sender As Object, e As EventArgs) Handles CMDChooseJumpers.Click
        Category = "Jumpers"
        Dim myDbcon As New dataBaseconnector
        Dim ImagePathList As New ArrayList
        ImagePathList = myDbcon.loadCategory(Category)
        displayImagePaths(ImagePathList)
    End Sub

    Private Sub CMDChooseTshirt_Click(sender As Object, e As EventArgs) Handles CMDChooseTshirt.Click
        Category = "T-Shirt"
        Dim myDbcon As New dataBaseconnector
        Dim ImagePathList As New ArrayList
        ImagePathList = myDbcon.loadCategory(Category)
        displayImagePaths(ImagePathList)
    End Sub

    Private Sub CMDChooseBottoms_Click(sender As Object, e As EventArgs) Handles CMDChooseBottoms.Click
        Category = "Bottoms"
        Dim myDbcon As New dataBaseconnector
        Dim ImagePathList As New ArrayList
        ImagePathList = myDbcon.loadCategory(Category)
        displayImagePaths(ImagePathList)
    End Sub

    Private Sub displayImagePaths(ByVal ImagePathList As ArrayList)
        Me.clothesByCategory.Controls.Clear()
        If ImagePathList.Count <> 0 Then
            For Each item In ImagePathList
                Dim clothingItem As New PictureBox
                With clothingItem
                    .Image = Image.FromFile(item)
                    .Size = New Size(150, 150)
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Tag = item
                End With
                Me.clothesByCategory.Controls.Add(clothingItem)
                clothingItem.Visible = True

                'This allows the image to be clicked to be selected
                AddHandler clothingItem.Click, AddressOf selectImage
            Next
        End If
    End Sub

    Private Sub selectImage(sender As Object, e As EventArgs)
        'This IF statement is used to reformat the string to match the picturebox name
        If Category = "T-Shirt" Then
            'This removes the dash from the string
            Category = Category.Replace("-", "")
            'This makes the whole string lowercase
            Category = Category.ToLower()
            'This makes the first letter of the string to Uppercase
            Dim newCategory = Category.Substring(0, 1).ToUpper & Category.Substring(1, Category.Length - 1).ToLower
            Category = newCategory
            'This adds an s onto the end of the string
            Category = Category + "s"
        End If

        'This will find the picture box of the associated category
        For Each ctrl As PictureBox In OutfitPanel.Controls
            If ctrl.Name = Category Then
                'This sets the picturebox to the imagepath of the tag of the image clicked
                'I previously made the image path the tag earlier, so this was easier in future
                ctrl.Image = Image.FromFile(sender.tag)
                With ctrl
                    'This makes the picturebox the same size as the image selected
                    .SizeMode = PictureBoxSizeMode.Zoom
                End With
            End If
        Next
    End Sub

End Class