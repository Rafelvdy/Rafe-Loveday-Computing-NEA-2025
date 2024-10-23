Imports System.Net
Imports System.IO
Imports Newtonsoft.Json.Linq


Public Class URLFinder
    Private _keyWord As String
    Private urlList As List(Of String)

    Public Property TitleResult As String
    Public Property URLResult As String
    Public Property PriceResult As String

    'Setter and getter so that i can securely have the keyword inputted from the user put into the object
    Public Property keyWord As String
        Set(value As String)
            _keyWord = value
        End Set
        Get
            Return _keyWord
        End Get
    End Property

    'Subroutine which will get the URLs
    Public Sub getURLs()
        'Variables will be instantiated when the subroutine is called to save memory
        'Variables are used to set up the API request
        Dim apiKey As String = "RafeLove-TLtest-PRD-6cd756e94-4998fc56"
        Dim keyWord As String = _keyWord
        'This is the end point URL and handles the connection to the ebay API 
        Dim endPoint As String = $"https://svcs.ebay.com/services/search/FindingService/v1?SECURITY-APPNAME={apiKey}&OPERATION-NAME=findItemsByKeywords&SERVICE-VERSION=1.0.0&RESPONSE-DATA-FORMAT=JSON&keywords={keyWord}"
        'Making the HTTP request
        Dim request As HttpWebRequest = CType(WebRequest.Create(endPoint), HttpWebRequest)

        'This specifies the request method as GET to retrieve data from the server
        request.Method = "GET"
        'Indicates that the program is expecting a json response
        request.ContentType = "application/json"

        Try
            'sends the request and gets the response from the API
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim responseStream As Stream = response.GetResponseStream()

            'Creating a streamreader to read the response stream
            Dim reader As New StreamReader(responseStream)
            'Reads the entire response data as a string
            Dim responseData As String = reader.ReadToEnd()

            'Parsing the JSON response (uses newtonsoft.json)
            'Makes easier data manipulation
            Dim json As JObject = JObject.Parse(responseData)

            'Searching through the JSON structure to access the array of items returned by the API
            Dim items = json("findItemsByKeywordsResponse")(0)("searchResult")(0)("item")
            'Initialises an empty string so that the results can be added to it
            Dim titleResult As String = ""
            Dim urlResult As String = ""
            Dim priceResult As String = ""

            'This will loop through the items so that it can gather URLs and prices
            For Each item In items
                'Getting the title from the JSON object
                Dim title As String = item("title").ToString()
                'Getting the URL so the user can view the items on ebay 
                Dim viewUrl As String = item("viewItemURL").ToString()
                'Getting the price from the JSON structure
                Dim price As String = item("sellingStatus")(0)("currentPrice")(0)("__value__").ToString()

                'Appends the results so that it is formatted for display
                titleResult &= $"Item: {title}" & Environment.NewLine
                'me allows me to reference the current class so that the variables can be stored in the public properties
                Me.TitleResult = titleResult
                urlResult &= $"URL: {viewUrl}" & Environment.NewLine
                Me.URLResult = urlResult
                priceResult &= $"Price: ${price}" & Environment.NewLine & Environment.NewLine
                Me.PriceResult = priceResult
            Next

            'calls subroutine so that it can be returned to to the clothingDetails form
            Dim results() As String = {titleResult, urlResult, priceResult}
            For Each item In results
                getResults(item)
            Next

            reader.Close()
            response.Close()

        Catch ex As WebException
            'This will handle any potential errors
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetresponseStream())
                    Dim errorResponse As String = reader.ReadToEnd()
                    MessageBox.Show("API Error: " & errorResponse, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Using
            Else
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    'A method which can pass the data back into clothingDetails form
    Public Function getResults(ByVal result As String)
        Return result
    End Function
End Class
