<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Wardrobe
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CMDAddImage = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'CMDAddImage
        '
        Me.CMDAddImage.Location = New System.Drawing.Point(566, 85)
        Me.CMDAddImage.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CMDAddImage.Name = "CMDAddImage"
        Me.CMDAddImage.Size = New System.Drawing.Size(121, 40)
        Me.CMDAddImage.TabIndex = 0
        Me.CMDAddImage.Text = "Add Image to Wardrobe"
        Me.CMDAddImage.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Multiselect = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AllowDrop = True
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(9, 130)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1419, 612)
        Me.FlowLayoutPanel1.TabIndex = 2
        '
        'Wardrobe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1426, 804)
        Me.Controls.Add(Me.CMDAddImage)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Wardrobe"
        Me.Text = "Wardrobe"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CMDAddImage As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
End Class
