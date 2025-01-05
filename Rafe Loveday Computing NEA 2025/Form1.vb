Imports System.Windows.Controls
Imports System.Drawing

Public Class MainMenu
    'A variable to keep track of the current form which is being displayed in the panel
    Private currentForm As Form = Nothing
    'This variable is used to store whether there is an existing wardrobe for the user or not
    Public WardrobeID As Integer

    Private Sub CMDOpenMenu_Click(sender As Object, e As EventArgs) Handles CMDOpenMenu.Click
        If MenuPanel.Visible = True Then
            MenuPanel.Hide()
        Else
            MenuPanel.Show()
        End If
    End Sub

    Private Sub CMDOpenWardrobe_Click(sender As Object, e As EventArgs) Handles CMDOpenWardrobe.Click
        menuSwap(Wardrobe)
        Dim myDBconnector As New dataBaseconnector
        'This identifies if there is a wardrobe
        'If there is not it will create one
        'The wardrobe ID is then returned
        WardrobeID = myDBconnector.checkForWardrobe()
        'This then populates the wardrobe using the wardrobeID just returned
        myDBconnector.populateWardrobe(WardrobeID)
        'Clothes only show when this is here, not sure why
        Wardrobe.Show()
    End Sub

    Sub menuSwap(ByVal panel As Form)
        'This IF statement prevents the program from crashing if you press the same button of the page that you are already on
        If panel.Visible = False Then
            'This IF statement was added as the menu would bug out when switching back and forth.
            ' Checks if there is a form being displayed or not
            If currentForm IsNot Nothing Then
                'closes the current form
                currentForm.Close()
                'Removes the current form that is being displayed from the display panel
                DisplayPanel.Controls.Remove(currentForm)
                'Disposes of the current form to free up resources
                currentForm.Dispose()
            End If

            currentForm = panel
            panel.TopLevel = False
            panel.WindowState = FormWindowState.Maximized
            'Removes the close buttons so that the form looks like it is apart of the page.
            panel.FormBorderStyle = Windows.Forms.BorderStyle.None
            panel.Visible = True
            DisplayPanel.Controls.Add(panel)
            'Will close the menu panel when a button is pressed so that the menu disssapears and it appears to be full screen
            MenuPanel.Hide()
        End If
    End Sub

    Private Sub CMDSelectImage_Click(sender As Object, e As EventArgs) Handles CMDSelectImage.Click
        'Create a new OpenFileDialogue Instance
        Dim openFileDialog As New OpenFileDialog()
        'Set the filter options
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        openFileDialog.Title = "Select an Image File"
        openFileDialog.ShowDialog()
        If openFileDialog.FileName <> "" Then
            Dim imageFile = openFileDialog.FileName
            ImageAnalysis.PictureBox1.Image = System.Drawing.Image.FromFile(imageFile)
            menuSwap(ImageAnalysis)
            ImageAnalysis.Show()
        End If
    End Sub

End Class
