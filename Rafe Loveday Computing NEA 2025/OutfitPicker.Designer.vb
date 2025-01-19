<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OutfitPicker
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
        Me.CMDChooseOutwear = New System.Windows.Forms.Button()
        Me.CMDChooseTshirt = New System.Windows.Forms.Button()
        Me.CMDChooseBottoms = New System.Windows.Forms.Button()
        Me.CMDChooseJumpers = New System.Windows.Forms.Button()
        Me.Outwear = New System.Windows.Forms.PictureBox()
        Me.Jumpers = New System.Windows.Forms.PictureBox()
        Me.Tshirts = New System.Windows.Forms.PictureBox()
        Me.Bottoms = New System.Windows.Forms.PictureBox()
        Me.clothesByCategory = New System.Windows.Forms.FlowLayoutPanel()
        Me.OutfitPanel = New System.Windows.Forms.Panel()
        CType(Me.Outwear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Jumpers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tshirts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bottoms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OutfitPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'CMDChooseOutwear
        '
        Me.CMDChooseOutwear.Location = New System.Drawing.Point(702, 107)
        Me.CMDChooseOutwear.Name = "CMDChooseOutwear"
        Me.CMDChooseOutwear.Size = New System.Drawing.Size(140, 96)
        Me.CMDChooseOutwear.TabIndex = 0
        Me.CMDChooseOutwear.Text = "Outwear"
        Me.CMDChooseOutwear.UseVisualStyleBackColor = True
        '
        'CMDChooseTshirt
        '
        Me.CMDChooseTshirt.Location = New System.Drawing.Point(702, 522)
        Me.CMDChooseTshirt.Name = "CMDChooseTshirt"
        Me.CMDChooseTshirt.Size = New System.Drawing.Size(140, 96)
        Me.CMDChooseTshirt.TabIndex = 1
        Me.CMDChooseTshirt.Text = "T-Shirts"
        Me.CMDChooseTshirt.UseVisualStyleBackColor = True
        '
        'CMDChooseBottoms
        '
        Me.CMDChooseBottoms.Location = New System.Drawing.Point(1370, 731)
        Me.CMDChooseBottoms.Name = "CMDChooseBottoms"
        Me.CMDChooseBottoms.Size = New System.Drawing.Size(140, 96)
        Me.CMDChooseBottoms.TabIndex = 2
        Me.CMDChooseBottoms.Text = "Bottoms"
        Me.CMDChooseBottoms.UseVisualStyleBackColor = True
        '
        'CMDChooseJumpers
        '
        Me.CMDChooseJumpers.Location = New System.Drawing.Point(1370, 308)
        Me.CMDChooseJumpers.Name = "CMDChooseJumpers"
        Me.CMDChooseJumpers.Size = New System.Drawing.Size(140, 96)
        Me.CMDChooseJumpers.TabIndex = 3
        Me.CMDChooseJumpers.Text = "Jumpers"
        Me.CMDChooseJumpers.UseVisualStyleBackColor = True
        '
        'Outwear
        '
        Me.Outwear.BackColor = System.Drawing.SystemColors.Control
        Me.Outwear.Location = New System.Drawing.Point(21, 4)
        Me.Outwear.Name = "Outwear"
        Me.Outwear.Size = New System.Drawing.Size(224, 358)
        Me.Outwear.TabIndex = 5
        Me.Outwear.TabStop = False
        '
        'Jumpers
        '
        Me.Jumpers.BackColor = System.Drawing.SystemColors.Control
        Me.Jumpers.Location = New System.Drawing.Point(265, 165)
        Me.Jumpers.Name = "Jumpers"
        Me.Jumpers.Size = New System.Drawing.Size(224, 358)
        Me.Jumpers.TabIndex = 6
        Me.Jumpers.TabStop = False
        '
        'Tshirts
        '
        Me.Tshirts.BackColor = System.Drawing.SystemColors.Control
        Me.Tshirts.Location = New System.Drawing.Point(21, 417)
        Me.Tshirts.Name = "Tshirts"
        Me.Tshirts.Size = New System.Drawing.Size(224, 358)
        Me.Tshirts.TabIndex = 7
        Me.Tshirts.TabStop = False
        '
        'Bottoms
        '
        Me.Bottoms.BackColor = System.Drawing.SystemColors.Control
        Me.Bottoms.Location = New System.Drawing.Point(265, 574)
        Me.Bottoms.Name = "Bottoms"
        Me.Bottoms.Size = New System.Drawing.Size(224, 358)
        Me.Bottoms.TabIndex = 8
        Me.Bottoms.TabStop = False
        '
        'clothesByCategory
        '
        Me.clothesByCategory.Location = New System.Drawing.Point(13, 48)
        Me.clothesByCategory.Name = "clothesByCategory"
        Me.clothesByCategory.Size = New System.Drawing.Size(679, 818)
        Me.clothesByCategory.TabIndex = 9
        '
        'OutfitPanel
        '
        Me.OutfitPanel.Controls.Add(Me.Outwear)
        Me.OutfitPanel.Controls.Add(Me.Jumpers)
        Me.OutfitPanel.Controls.Add(Me.Bottoms)
        Me.OutfitPanel.Controls.Add(Me.Tshirts)
        Me.OutfitPanel.Location = New System.Drawing.Point(848, -1)
        Me.OutfitPanel.Name = "OutfitPanel"
        Me.OutfitPanel.Size = New System.Drawing.Size(516, 942)
        Me.OutfitPanel.TabIndex = 10
        '
        'OutfitPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1763, 943)
        Me.Controls.Add(Me.OutfitPanel)
        Me.Controls.Add(Me.clothesByCategory)
        Me.Controls.Add(Me.CMDChooseJumpers)
        Me.Controls.Add(Me.CMDChooseBottoms)
        Me.Controls.Add(Me.CMDChooseTshirt)
        Me.Controls.Add(Me.CMDChooseOutwear)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "OutfitPicker"
        Me.Text = "OutfitPicker"
        CType(Me.Outwear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Jumpers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tshirts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bottoms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.OutfitPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CMDChooseOutwear As Button
    Friend WithEvents CMDChooseTshirt As Button
    Friend WithEvents CMDChooseBottoms As Button
    Friend WithEvents CMDChooseJumpers As Button
    Friend WithEvents Outwear As PictureBox
    Friend WithEvents Jumpers As PictureBox
    Friend WithEvents Tshirts As PictureBox
    Friend WithEvents Bottoms As PictureBox
    Friend WithEvents clothesByCategory As FlowLayoutPanel
    Friend WithEvents OutfitPanel As Panel
End Class
