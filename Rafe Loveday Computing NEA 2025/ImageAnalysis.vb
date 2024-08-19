Imports Google.Cloud.Vision.V1
Imports Google.Apis.Auth.OAuth2
Imports Grpc.Auth
Imports System.IO


Public Class ImageAnalysis

    Private Sub ImageAnalysis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MainMenu.Hide()
    End Sub

    Private Async Sub CMDImageAnalyse_Click(sender As Object, e As EventArgs) Handles CMDImageAnalyse.Click
        Try
            'file path to the json file containing my account key
            Dim jsonPath As String = "C:\Users\Owner\Desktop\A Level NEA\Computing\Rafe Loveday NEA 2025\digital-wardrobe-426413-500b759eedb8.json"

            'file path for the image which i want to analyse
            Dim imagePath As String = Label1.Text

            'Authenticates account using the service account key file
            Dim credential = GoogleCredential.FromFile(jsonPath).CreateScoped(ImageAnnotatorClient.DefaultScopes)

            'Creates a gRPC channel using the json credentials
            'gRPC channel is a communication channel for making remote procedure calls (RPCs) between a client and a server 
            Dim channel = New Grpc.Core.Channel(ImageAnnotatorClient.DefaultEndpoint, credential.ToChannelCredentials())

            'Creates the ImageAnnotatorClient using the credentials from the JSON file
            Dim client = New ImageAnnotatorClientBuilder() With {
                .JsonCredentials = File.ReadAllText(jsonPath)
            }.Build()

            'Loads the image file into a byte array
            Dim imageBytes = File.ReadAllBytes(imagePath)

            'Makes an image object from the byte array
            Dim image = Google.Cloud.Vision.V1.Image.FromBytes(imageBytes)

            'label detection occurs on the image and results are stored in labelResponse
            Dim labelResponse = client.DetectLabels(image)

            'logo detection occurs on the image and results are stored in logoResponse
            Dim logoResponse = client.DetectLogos(image)

            'Dim targetClothingType As New List(Of String) From {"Jeans", "Pants", "Suit", "Outwear", "Tshirts", "Suit", "Blazer", "Jumper", "Hoodie", "Sweater", "Trousers", "Shorts", "Socks", "Underwear", "Top", "Dress", "Jacket"}
            'Dim targetColour As New List(Of String) From {"Red", "Blue", "Yellow", "Green", "Orange", "Purple", "Black", "White", "Grey", "Beige", "Brown", "Navy", "Burgundy", "Maroon", "Multicolour", "Gradient", "Ombre", "Two-tone", "Striped", "Chequered"}
            'Dim targetOccasion As New List(Of String) From {"Casual Wear", "Business Wear", "Work Wear", "Formal Wear", "Smart Casual", "Athletic", "Sportswear", "Loungewear", "Sleepwear", "SwimWear", "Activewear", "Vintage", "Gothic", "Y2K"}
            'Dim targetFit As New List(Of String) From {"Slim Fit", "Regular Fit", "Oversized", "Loose Fit", "Tailoured Fit", "Boxy Fit", "Relaxed Fit", "High-Waisted", "Low-Rise", "Flare", "Straight Fit", "Skinny Fit", "Cropped", "Wide Leg"}

            'A list of clothing related words that i want to be explicitly shown from the labels created to filter them
            Dim targetLabels As New List(Of String) From {
               "Clothing", "Apparel", "Fashion", "Wear", "Outfit", "Garment", "Dress",
               "Shirt", "T-shirt", "Blouse", "Pants", "Trousers", "Jeans", "Shorts",
               "Skirt", "Jacket", "Coat", "Sweater", "Hoodie", "Suit", "Tie",
               "Formal wear", "Casual wear", "Sportswear", "Activewear",
               "Footwear", "Shoes", "Sneakers", "Boots", "Sandals",
               "Accessories", "Hat", "Cap", "Scarf", "Gloves", "Belt",
               "Material", "Cotton", "Wool", "Silk", "Denim", "Leather",
               "Pattern", "Striped", "Checked", "Polka dot", "Plaid", "Floral",
               "Color", "Red", "Blue", "Green", "Black", "White", "Yellow", "Pink", "Purple",
               "Brand", "Logo", "Label", "Tag", "Design", "Texture", "Stussy"
             }

            'String to store the results of label detection
            Dim labelResults As String = "Labels detected:" & Environment.NewLine

            'String to store the results of logo detection
            Dim logoResults As String = "Brands detected:" & Environment.NewLine

            'Processes and appends the results of logo detection from logoResponse to logoResult
            For Each annotation In logoResponse
                logoResults &= annotation.Description & " (score: " & (annotation.Score) * 100 & "%)" & Environment.NewLine
            Next

            'Processes and appends the results of label detection from labelResponse to labelResult and will filter using the target labels
            For Each annotation In labelResponse
                If targetLabels.Contains(annotation.Description, StringComparer.OrdinalIgnoreCase) Then
                    labelResults &= annotation.Description & " (score: " & (annotation.Score) * 100 & "%)" & Environment.NewLine
                End If

            Next

            'Displays the results in a message box

            MessageBox.Show(labelResults & logoResults)

        Catch ex As Exception
            ' Handle exceptions
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub



End Class