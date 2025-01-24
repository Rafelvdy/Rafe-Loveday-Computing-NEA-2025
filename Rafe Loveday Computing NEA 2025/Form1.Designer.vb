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
        Me.CMDOutfitPicker = New System.Windows.Forms.Button()
        Me.MenuBar = New System.Windows.Forms.Panel()
        Me.CMDOpenMenu = New System.Windows.Forms.Button()
        Me.CMDStatistics = New System.Windows.Forms.Button()
        Me.MenuPanel.SuspendLayout()
        Me.MenuBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'DisplayPanel
        '
        Me.DisplayPanel.Location = New System.Drawing.Point(117, 46)
        Me.DisplayPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DisplayPanel.Name = "DisplayPanel"
        Me.DisplayPanel.Size = New System.Drawing.Size(1816, 1300)
        Me.DisplayPanel.TabIndex = 5
        '
        'MenuPanel
        '
        Me.MenuPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.MenuPanel.Controls.Add(Me.CMDStatistics)
        Me.MenuPanel.Controls.Add(Me.CMDOpenWardrobe)
        Me.MenuPanel.Controls.Add(Me.CMDOutfitPicker)
        Me.MenuPanel.Location = New System.Drawing.Point(0, 46)
        Me.MenuPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MenuPanel.Name = "MenuPanel"
        Me.MenuPanel.Size = New System.Drawing.Size(121, 186)
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
        Me.CMDOpenWardrobe.Location = New System.Drawing.Point(0, 2)
        Me.CMDOpenWardrobe.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CMDOpenWardrobe.Name = "CMDOpenWardrobe"
        Me.CMDOpenWardrobe.Size = New System.Drawing.Size(123, 57)
        Me.CMDOpenWardrobe.TabIndex = 1
        Me.CMDOpenWardrobe.Text = "Open Wardrobe"
        Me.CMDOpenWardrobe.UseVisualStyleBackColor = False
        '
        'CMDOutfitPicker
        '
        Me.CMDOutfitPicker.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.CMDOutfitPicker.FlatAppearance.BorderSize = 0
        Me.CMDOutfitPicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDOutfitPicker.Font = New System.Drawing.Font("Segoe UI Black", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOutfitPicker.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CMDOutfitPicker.Location = New System.Drawing.Point(-2, 63)
        Me.CMDOutfitPicker.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CMDOutfitPicker.Name = "CMDOutfitPicker"
        Me.CMDOutfitPicker.Size = New System.Drawing.Size(123, 57)
        Me.CMDOutfitPicker.TabIndex = 0
        Me.CMDOutfitPicker.Text = "Outfit Picker"
        Me.CMDOutfitPicker.UseVisualStyleBackColor = False
        '
        'MenuBar
        '
        Me.MenuBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MenuBar.Controls.Add(Me.CMDOpenMenu)
        Me.MenuBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Size = New System.Drawing.Size(1924, 46)
        Me.MenuBar.TabIndex = 3
        '
        'CMDOpenMenu
        '
        Me.CMDOpenMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDOpenMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.CMDOpenMenu.FlatAppearance.BorderSize = 0
        Me.CMDOpenMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDOpenMenu.Font = New System.Drawing.Font("Segoe UI Black", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOpenMenu.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CMDOpenMenu.Location = New System.Drawing.Point(-3, 0)
        Me.CMDOpenMenu.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CMDOpenMenu.Name = "CMDOpenMenu"
        Me.CMDOpenMenu.Size = New System.Drawing.Size(123, 46)
        Me.CMDOpenMenu.TabIndex = 0
        Me.CMDOpenMenu.Text = "Menu"
        Me.CMDOpenMenu.UseVisualStyleBackColor = False
        '
        'CMDStatistics
        '
        Me.CMDStatistics.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.CMDStatistics.FlatAppearance.BorderSize = 0
        Me.CMDStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDStatistics.Font = New System.Drawing.Font("Segoe UI Black", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDStatistics.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CMDStatistics.Location = New System.Drawing.Point(-2, 124)
        Me.CMDStatistics.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CMDStatistics.Name = "CMDStatistics"
        Me.CMDStatistics.Size = New System.Drawing.Size(123, 57)
        Me.CMDStatistics.TabIndex = 2
        Me.CMDStatistics.Text = "Statistics"
        Me.CMDStatistics.UseVisualStyleBackColor = False
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1924, 1033)
        Me.Controls.Add(Me.MenuPanel)
        Me.Controls.Add(Me.DisplayPanel)
        Me.Controls.Add(Me.MenuBar)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
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
    Friend WithEvents CMDOutfitPicker As Button
    Friend WithEvents MenuBar As Panel
    Friend WithEvents CMDOpenMenu As Button
    Friend WithEvents CMDStatistics As Button
End Class
