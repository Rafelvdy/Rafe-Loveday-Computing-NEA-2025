<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainMenu
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
        Me.DisplayPanel = New System.Windows.Forms.Panel()
        Me.MenuPanel = New System.Windows.Forms.Panel()
        Me.CMDOpenWardrobe = New System.Windows.Forms.Button()
        Me.CMDSelectImage = New System.Windows.Forms.Button()
        Me.MenuBar = New System.Windows.Forms.Panel()
        Me.CMDOpenMenu = New System.Windows.Forms.Button()
        Me.MenuPanel.SuspendLayout()
        Me.MenuBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'DisplayPanel
        '
        Me.DisplayPanel.Location = New System.Drawing.Point(117, 45)
        Me.DisplayPanel.Name = "DisplayPanel"
        Me.DisplayPanel.Size = New System.Drawing.Size(1782, 990)
        Me.DisplayPanel.TabIndex = 5
        '
        'MenuPanel
        '
        Me.MenuPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.MenuPanel.Controls.Add(Me.CMDOpenWardrobe)
        Me.MenuPanel.Controls.Add(Me.CMDSelectImage)
        Me.MenuPanel.Location = New System.Drawing.Point(0, 45)
        Me.MenuPanel.Name = "MenuPanel"
        Me.MenuPanel.Size = New System.Drawing.Size(121, 990)
        Me.MenuPanel.TabIndex = 4
        Me.MenuPanel.Visible = False
        '
        'CMDOpenWardrobe
        '
        Me.CMDOpenWardrobe.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.CMDOpenWardrobe.FlatAppearance.BorderSize = 0
        Me.CMDOpenWardrobe.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDOpenWardrobe.Font = New System.Drawing.Font("Segoe UI Black", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOpenWardrobe.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CMDOpenWardrobe.Location = New System.Drawing.Point(0, 3)
        Me.CMDOpenWardrobe.Name = "CMDOpenWardrobe"
        Me.CMDOpenWardrobe.Size = New System.Drawing.Size(123, 57)
        Me.CMDOpenWardrobe.TabIndex = 1
        Me.CMDOpenWardrobe.Text = "Open Wardrobe"
        Me.CMDOpenWardrobe.UseVisualStyleBackColor = False
        '
        'CMDSelectImage
        '
        Me.CMDSelectImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.CMDSelectImage.FlatAppearance.BorderSize = 0
        Me.CMDSelectImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDSelectImage.Font = New System.Drawing.Font("Segoe UI Black", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSelectImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CMDSelectImage.Location = New System.Drawing.Point(-2, 66)
        Me.CMDSelectImage.Name = "CMDSelectImage"
        Me.CMDSelectImage.Size = New System.Drawing.Size(123, 57)
        Me.CMDSelectImage.TabIndex = 0
        Me.CMDSelectImage.Text = "Select Image"
        Me.CMDSelectImage.UseVisualStyleBackColor = False
        '
        'MenuBar
        '
        Me.MenuBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MenuBar.Controls.Add(Me.CMDOpenMenu)
        Me.MenuBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Size = New System.Drawing.Size(1902, 46)
        Me.MenuBar.TabIndex = 3
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
        Me.CMDOpenMenu.Size = New System.Drawing.Size(123, 46)
        Me.CMDOpenMenu.TabIndex = 0
        Me.CMDOpenMenu.Text = "Menu"
        Me.CMDOpenMenu.UseVisualStyleBackColor = False
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1902, 1033)
        Me.Controls.Add(Me.MenuPanel)
        Me.Controls.Add(Me.DisplayPanel)
        Me.Controls.Add(Me.MenuBar)
        Me.Name = "MainMenu"
        Me.Text = "Digital Wardrobe"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuPanel.ResumeLayout(False)
        Me.MenuBar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DisplayPanel As Panel
    Friend WithEvents MenuPanel As Panel
    Friend WithEvents CMDOpenWardrobe As Button
    Friend WithEvents CMDSelectImage As Button
    Friend WithEvents MenuBar As Panel
    Friend WithEvents CMDOpenMenu As Button
End Class
