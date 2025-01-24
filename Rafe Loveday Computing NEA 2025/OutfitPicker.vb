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
            Dim myDBConn As New dataBaseconnector
            'This returns the imagepath of the clothing item that has not been worn in the longest amount of time
            Dim LongestImagePath As String = myDBConn.getClothingSuggestion(Category, "LastWornDate")

            If LongestImagePath <> "" Then
                'Displaying its image in the flowlayoutpanel
                Dim LongestNotWorn As New PictureBox
                With LongestNotWorn
                    .Image = Image.FromFile(LongestImagePath)
                    .Size = New Size(150, 150)
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Tag = LongestImagePath
                End With
                'This adds the function of being able to select the image
                AddHandler LongestNotWorn.Click, AddressOf selectImage
                clothesByCategory.Controls.Add(LongestNotWorn)
                LongestNotWorn.Visible = True
                'Prevents the clotihngitem from being displayed again 
                ImagePathList.Remove(LongestImagePath)

                MessageBox.Show("You haven't worn this in a while!")
            End If

            'This gets the image path of the clothingitem that has never been worn before
            Dim NeverImagePath = myDBConn.getClothingSuggestion(Category, "NoOfWears")
            If NeverImagePath <> "" Then
                Dim NeverWorn As New PictureBox
                With NeverWorn
                    .Image = Image.FromFile(NeverImagePath)
                    .Size = New Size(150, 150)
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .Tag = NeverImagePath
                End With
                AddHandler NeverWorn.Click, AddressOf selectImage
                clothesByCategory.Controls.Add(NeverWorn)
                NeverWorn.Visible = True
                'Prevents the clotihngitem from being displayed again 
                ImagePathList.Remove(NeverImagePath)

                MessageBox.Show("You have never worn this!")
            End If

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
        'This makes the wear outfit button appear when the first item of clothing is added to the outfit
        If CMDWearOutfit.Visible = False Then
            CMDWearOutfit.Visible = True
        End If

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
                If ctrl.Tag = sender.tag Then
                    ctrl.Image = Nothing
                    ctrl.Tag = ""
                Else
                    'This sets the picturebox to the imagepath of the tag of the image clicked
                    'I previously made the image path the tag earlier, so this was easier in future
                    ctrl.Image = Image.FromFile(sender.tag)
                    ctrl.Tag = sender.tag
                    With ctrl
                        'This makes the picturebox the same size as the image selected
                        .SizeMode = PictureBoxSizeMode.Zoom
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub CMDWearOutfit_Click(sender As Object, e As EventArgs) Handles CMDWearOutfit.Click
        Dim myDBConn As New dataBaseconnector
        For Each clothingItem As PictureBox In OutfitPanel.Controls
            If clothingItem.Tag <> Nothing Then
                Dim ImagePath As String = clothingItem.Tag
                myDBConn.wearOutfit(ImagePath)
            End If
        Next
    End Sub

End Class
