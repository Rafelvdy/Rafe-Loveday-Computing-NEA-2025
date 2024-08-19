<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CMDSelectImage = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CMDSelectImage
        '
        Me.CMDSelectImage.Location = New System.Drawing.Point(332, 288)
        Me.CMDSelectImage.Name = "CMDSelectImage"
        Me.CMDSelectImage.Size = New System.Drawing.Size(120, 57)
        Me.CMDSelectImage.TabIndex = 0
        Me.CMDSelectImage.Text = "Select Image"
        Me.CMDSelectImage.UseVisualStyleBackColor = True
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.CMDSelectImage)
        Me.Name = "MainMenu"
        Me.Text = "Digital Wardrobe"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CMDSelectImage As Button
End Class
