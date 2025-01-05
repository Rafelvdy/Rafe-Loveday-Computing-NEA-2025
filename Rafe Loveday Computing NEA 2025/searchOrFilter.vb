Public Class searchOrFilter
    Public Function mergeSort(ByVal priceArray As Double(), ByVal low As Integer, ByVal high As Integer)
        'THIS IS THE BASE CASE
        If low >= high Then Return priceArray

        'FINDING THE MIDDLE AND THE LENGTH
        Dim length As Integer = high - low + 1
        Dim middle As Integer = Math.Floor((low + high) / 2)

        'SPLITTING ARRAY INTO TWO HALVES AND RECURSIVELY CALLING MY SUB
        mergeSort(priceArray, low, middle)
        mergeSort(priceArray, middle + 1, high)

        'MAKING A TEMPORARY ARRAY TO HOLD ELEMENTS OF CURRENT SEGMENT
        Dim temp(priceArray.Count() - 1)
        For i = 0 To length - 1
            temp(i) = priceArray(low + i)
        Next

        'MERGING PART OF THE ALGORITHM
        Dim M1 As Integer = 0 'Points to the first element in the left half
        Dim M2 As Integer = middle - low + 1 'Points to the first element in the right half

        For i = 0 To length - 1 'Creating the merge loop
            If M2 <= high - low Then 'Checks for elements in the right
                If M1 <= middle - low Then 'Checks for elements in the left
                    If temp(M1) > temp(M2) Then 'Comparing elements in their different positions
                        'IF M1 > M2 THEN M2 PUT IN LOWER POSITION
                        priceArray(i + low) = temp(M2) 'This will update the current position with smaller value from right half
                        M2 += 1 'Increments to the next position
                    Else
                        'IF M1 < M2 THEN M1 PUT IN LOWER POSITION
                        priceArray(i + low) = temp(M1) 'Updates position with value from left half 
                        M1 += 1
                    End If
                Else
                    'NO ELEMENTS IN LEFT ARRAY SO NEXT ELEMENT OF RIGHT ARRAY ADDED
                    priceArray(i + low) = temp(M2)
                    M2 += 1
                End If
            Else
                'NO ELEMENTS IN RIGHT ARRAY SO NEXT ELEMENT OF LEFT ARRAY ADDED
                priceArray(i + low) = temp(M1)
                M1 += 1
            End If
        Next
        Return priceArray
    End Function
End Class
