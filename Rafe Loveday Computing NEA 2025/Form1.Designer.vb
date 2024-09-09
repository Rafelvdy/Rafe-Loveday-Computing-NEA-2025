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
        Me.components = New System.ComponentModel.Container()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CMDOpenMenu = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.CMDSelectImage = New System.Windows.Forms.Button()
        Me.CMDOpenWardrobe = New System.Windows.Forms.Button()
        Me.SC = New System.Windows.Forms.SplitContainer()
        Me.Panel2.SuspendLayout()
        CType(Me.SC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SC.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel2.Controls.Add(Me.CMDOpenMenu)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(372, 43)
        Me.Panel2.TabIndex = 3
        '
        'CMDOpenMenu
        '
        Me.CMDOpenMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDOpenMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.CMDOpenMenu.FlatAppearance.BorderSize = 0
        Me.CMDOpenMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDOpenMenu.Font = New System.Drawing.Font("Segoe UI Black", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOpenMenu.Location = New System.Drawing.Point(-2, 0)
        Me.CMDOpenMenu.Name = "CMDOpenMenu"
        Me.CMDOpenMenu.Size = New System.Drawing.Size(123, 43)
        Me.CMDOpenMenu.TabIndex = 0
        Me.CMDOpenMenu.Text = "Menu"
        Me.CMDOpenMenu.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'Timer2
        '
        Me.Timer2.Interval = 1
        '
        'CMDSelectImage
        '
        Me.CMDSelectImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.CMDSelectImage.FlatAppearance.BorderSize = 0
        Me.CMDSelectImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDSelectImage.Font = New System.Drawing.Font("Segoe UI Black", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSelectImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CMDSelectImage.Location = New System.Drawing.Point(175, 66)
        Me.CMDSelectImage.Name = "CMDSelectImage"
        Me.CMDSelectImage.Size = New System.Drawing.Size(117, 57)
        Me.CMDSelectImage.TabIndex = 0
        Me.CMDSelectImage.Text = "Select Image"
        Me.CMDSelectImage.UseVisualStyleBackColor = False
        '
        'CMDOpenWardrobe
        '
        Me.CMDOpenWardrobe.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.CMDOpenWardrobe.FlatAppearance.BorderSize = 0
        Me.CMDOpenWardrobe.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDOpenWardrobe.Font = New System.Drawing.Font("Segoe UI Black", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOpenWardrobe.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CMDOpenWardrobe.Location = New System.Drawing.Point(175, 143)
        Me.CMDOpenWardrobe.Name = "CMDOpenWardrobe"
        Me.CMDOpenWardrobe.Size = New System.Drawing.Size(120, 57)
        Me.CMDOpenWardrobe.TabIndex = 1
        Me.CMDOpenWardrobe.Text = "Open Wardrobe"
        Me.CMDOpenWardrobe.UseVisualStyleBackColor = False
        '
        'SC
        '
        Me.SC.Location = New System.Drawing.Point(-2, 39)
        Me.SC.Name = "SC"
        '
        'SC.Panel1
        '
        Me.SC.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.SC.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.SC.Panel1MinSize = 0
        '
        'SC.Panel2
        '
        Me.SC.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.SC.Size = New System.Drawing.Size(374, 761)
        Me.SC.SplitterDistance = 124
        Me.SC.TabIndex = 4
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 797)
        Me.Controls.Add(Me.SC)
        Me.Controls.Add(Me.CMDSelectImage)
        Me.Controls.Add(Me.CMDOpenWardrobe)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "MainMenu"
        Me.Text = "Digital Wardrobe"
        Me.Panel2.ResumeLayout(False)
        CType(Me.SC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SC.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents CMDSelectImage As Button
    Friend WithEvents CMDOpenWardrobe As Button
    Friend WithEvents SC As SplitContainer
    Friend WithEvents CMDOpenMenu As Button
End Class
