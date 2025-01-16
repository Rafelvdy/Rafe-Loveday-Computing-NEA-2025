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
        Me.clothesByCategory = New System.Windows.Forms.FlowLayoutPanel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CMDChooseOutwear
        '
        Me.CMDChooseOutwear.Location = New System.Drawing.Point(985, 89)
        Me.CMDChooseOutwear.Name = "CMDChooseOutwear"
        Me.CMDChooseOutwear.Size = New System.Drawing.Size(140, 96)
        Me.CMDChooseOutwear.TabIndex = 0
        Me.CMDChooseOutwear.Text = "Outwear"
        Me.CMDChooseOutwear.UseVisualStyleBackColor = True
        '
        'CMDChooseTshirt
        '
        Me.CMDChooseTshirt.Location = New System.Drawing.Point(985, 515)
        Me.CMDChooseTshirt.Name = "CMDChooseTshirt"
        Me.CMDChooseTshirt.Size = New System.Drawing.Size(140, 96)
        Me.CMDChooseTshirt.TabIndex = 1
        Me.CMDChooseTshirt.Text = "T-Shirts"
        Me.CMDChooseTshirt.UseVisualStyleBackColor = True
        '
        'CMDChooseBottoms
        '
        Me.CMDChooseBottoms.Location = New System.Drawing.Point(985, 736)
        Me.CMDChooseBottoms.Name = "CMDChooseBottoms"
        Me.CMDChooseBottoms.Size = New System.Drawing.Size(140, 96)
        Me.CMDChooseBottoms.TabIndex = 2
        Me.CMDChooseBottoms.Text = "Bottoms"
        Me.CMDChooseBottoms.UseVisualStyleBackColor = True
        '
        'CMDChooseJumpers
        '
        Me.CMDChooseJumpers.Location = New System.Drawing.Point(985, 304)
        Me.CMDChooseJumpers.Name = "CMDChooseJumpers"
        Me.CMDChooseJumpers.Size = New System.Drawing.Size(140, 96)
        Me.CMDChooseJumpers.TabIndex = 3
        Me.CMDChooseJumpers.Text = "Jumpers"
        Me.CMDChooseJumpers.UseVisualStyleBackColor = True
        '
        'clothesByCategory
        '
        Me.clothesByCategory.BackColor = System.Drawing.SystemColors.Control
        Me.clothesByCategory.Location = New System.Drawing.Point(58, 48)
        Me.clothesByCategory.Name = "clothesByCategory"
        Me.clothesByCategory.Size = New System.Drawing.Size(747, 818)
        Me.clothesByCategory.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PictureBox1.Location = New System.Drawing.Point(1310, 48)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(197, 163)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PictureBox2.Location = New System.Drawing.Point(1310, 262)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(197, 163)
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PictureBox3.Location = New System.Drawing.Point(1310, 482)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(197, 163)
        Me.PictureBox3.TabIndex = 7
        Me.PictureBox3.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PictureBox4.Location = New System.Drawing.Point(1310, 703)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(197, 163)
        Me.PictureBox4.TabIndex = 8
        Me.PictureBox4.TabStop = False
        '
        'OutfitPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1763, 943)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.clothesByCategory)
        Me.Controls.Add(Me.CMDChooseJumpers)
        Me.Controls.Add(Me.CMDChooseBottoms)
        Me.Controls.Add(Me.CMDChooseTshirt)
        Me.Controls.Add(Me.CMDChooseOutwear)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "OutfitPicker"
        Me.Text = "OutfitPicker"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CMDChooseOutwear As Button
    Friend WithEvents CMDChooseTshirt As Button
    Friend WithEvents CMDChooseBottoms As Button
    Friend WithEvents CMDChooseJumpers As Button
    Friend WithEvents clothesByCategory As FlowLayoutPanel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
End Class
