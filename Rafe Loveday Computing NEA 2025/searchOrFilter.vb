Imports System.Text.RegularExpressions

Public Class searchOrFilter
    Public Function mergeSort(ByVal priceArray As Double(), ByVal low As Integer, ByVal high As Integer, ByVal titleArray() As String, ByVal urlArray() As String)
        'THIS IS THE BASE CASE
        If low >= high Then Return priceArray

        'FINDING THE MIDDLE AND THE LENGTH
        Dim length As Integer = high - low + 1
        Dim middle As Integer = Math.Floor((low + high) / 2)

        'SPLITTING ARRAY INTO TWO HALVES AND RECURSIVELY CALLING MY SUB
        mergeSort(priceArray, low, middle, titleArray, urlArray)
        mergeSort(priceArray, middle + 1, high, titleArray, urlArray)

        'Temporary arrays to hold data
        Dim tempPrice(length - 1) As Double
        Dim tempTitle(length - 1) As String
        Dim tempURL(length - 1) As String

        'MAKING A TEMPORARY ARRAY TO HOLD ELEMENTS OF CURRENT SEGMENT
        For i = 0 To length - 1
            tempPrice(i) = priceArray(low + i)
            tempTitle(i) = titleArray(low + i)
            tempURL(i) = urlArray(low + i)
        Next

        'MERGING PART OF THE ALGORITHM
        Dim M1 As Integer = 0 'Points to the first element in the left half
        Dim M2 As Integer = middle - low + 1 'Points to the first element in the right half

        For i = 0 To length - 1 'Creating the merge loop
            If M2 <= high - low Then 'Checks for elements in the right
                If M1 <= middle - low Then 'Checks for elements in the left
                    If tempPrice(M1) > tempPrice(M2) Then 'Comparing elements in their different positions
                        'IF M1 > M2 THEN M2 PUT IN LOWER POSITION
                        priceArray(i + low) = tempPrice(M2) 'This will update the current position with smaller value from right half
                        titleArray(i + low) = tempTitle(M2)
                        urlArray(i + low) = tempURL(M2)
                        M2 += 1 'Increments to the next position
                    Else
                        'IF M1 < M2 THEN M1 PUT IN LOWER POSITION
                        priceArray(i + low) = tempPrice(M1) 'Updates position with value from left half 
                        titleArray(i + low) = tempTitle(M1)
                        urlArray(i + low) = tempURL(M1)
                        M1 += 1

                    End If
                Else
                    'NO ELEMENTS IN LEFT ARRAY SO NEXT ELEMENT OF RIGHT ARRAY ADDED
                    priceArray(i + low) = tempPrice(M2)
                    titleArray(i + low) = tempTitle(M2)
                    urlArray(i + low) = tempURL(M2)
                    M2 += 1 'Increments to the next position
                End If
            Else
                'NO ELEMENTS IN RIGHT ARRAY SO NEXT ELEMENT OF LEFT ARRAY ADDED
                priceArray(i + low) = tempPrice(M1)
                titleArray(i + low) = tempTitle(M1)
                urlArray(i + low) = tempURL(M1)
                M1 += 1
            End If
        Next
        Return (priceArray, titleArray, urlArray)
    End Function

    Public Function containsSearch(ByVal titlesArray() As String, ByVal priceArray() As String, ByVal urlArray() As String, ByVal searchData As String)
        'These variables are used to hold the API data so that they can be kept organised together when searching
        Dim titlesToDisplay(titlesArray.Length - 1) As String
        Dim pricesToDisplay(titlesArray.Length - 1) As String
        Dim urlsToDisplay(titlesArray.Length - 1) As String

        'This variable is used to hold the index so that whenever a match is found so that the program adds the results to the next index 
        Dim matchIndex As Integer = 0
        Dim searchIndex As Integer = 0


        For Each item In titlesArray
            If Regex.IsMatch(item, searchData, RegexOptions.IgnoreCase) Then
                'This holds all the results so they can be returned and displayed 
                titlesToDisplay(matchIndex) = titlesArray(searchIndex)
                pricesToDisplay(matchIndex) = priceArray(searchIndex)
                urlsToDisplay(matchIndex) = urlArray(searchIndex)

                matchIndex += 1
            End If
            searchIndex += 1
        Next

        Array.Resize(titlesToDisplay, matchIndex)
        Array.Resize(pricesToDisplay, matchIndex)
        Array.Resize(urlsToDisplay, matchIndex)

        Return (matchIndex, titlesToDisplay, pricesToDisplay, urlsToDisplay)
    End Function
End Class
