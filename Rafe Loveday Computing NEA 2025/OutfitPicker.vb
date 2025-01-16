Public Class OutfitPicker
    Dim Category As String = ""


    Private Sub CMDChooseOutwear_Click(sender As Object, e As EventArgs) Handles CMDChooseOutwear.Click
        Category = "Outwear"
        Dim myDbcon As New dataBaseconnector
        myDbcon.loadCategory(Category)
    End Sub

    Private Sub CMDChooseJumpers_Click(sender As Object, e As EventArgs) Handles CMDChooseJumpers.Click
        Category = "Jumpers"
        Dim myDbcon As New dataBaseconnector
        myDbcon.loadCategory(Category)
    End Sub

    Private Sub CMDChooseTshirt_Click(sender As Object, e As EventArgs) Handles CMDChooseTshirt.Click
        Category = "T-Shirt"
        Dim myDbcon As New dataBaseconnector
        myDbcon.loadCategory(Category)
    End Sub

    Private Sub CMDChooseBottoms_Click(sender As Object, e As EventArgs) Handles CMDChooseBottoms.Click
        Category = "Bottoms"
        Dim myDbcon As New dataBaseconnector
        myDbcon.loadCategory(Category)
    End Sub


End Class