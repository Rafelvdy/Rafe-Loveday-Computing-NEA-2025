Public Class UserStats
    Private Sub UserStats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myDBConn As New dataBaseconnector
        myDBConn.retrieveStatistics("CLOTHINGITEM", "Colour")
        myDBConn.retrieveStatistics("CLOTHINGITEM", "Material")
        myDBConn.retrieveStatistics("IMAGE", "ImagePath")
        Dim LeastImagePath As String = myDBConn.LeastStatsImagePath
        Dim MostImagePath As String = myDBConn.MostStatsImagePath

        LeastWornClothingItem.Image = Image.FromFile(LeastImagePath)
        With LeastWornClothingItem
            .SizeMode = PictureBoxSizeMode.Zoom
        End With

        MostWornClothingItem.Image = Image.FromFile(MostImagePath)
        With MostWornClothingItem
            .SizeMode = PictureBoxSizeMode.Zoom
        End With

        myDBConn.createLeastWorn()
    End Sub

    Private Sub TrousersOrShorts_Click(sender As Object, e As EventArgs) Handles TrousersOrShorts.Click
        If TrousersOrShorts.Text = "Showing Trousers" Then
            TrousersOrShorts.Text = "Showing Shorts"
            LeastWornShorts.Visible = True
            LeastWornTrousers.Visible = False

        ElseIf TrousersOrShorts.Text = "Showing Shorts" Then
            TrousersOrShorts.Text = "Showing Trousers"
            LeastWornTrousers.Visible = True
            LeastWornShorts.Visible = False
        End If
    End Sub

End Class