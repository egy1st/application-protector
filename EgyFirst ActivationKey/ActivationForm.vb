Public Class ActivationForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ActivationKey As System.Windows.Forms.TextBox
    Friend WithEvents Generator As System.Windows.Forms.Button
    Friend WithEvents Algorithms1_Text As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Algorithms2_Text As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Algorithms3_Text As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents KeyList As System.Windows.Forms.TextBox
    Friend WithEvents ProductName As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ActivationForm))
        Me.Algorithms1_Text = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ActivationKey = New System.Windows.Forms.TextBox()
        Me.Generator = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Algorithms2_Text = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Algorithms3_Text = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.KeyList = New System.Windows.Forms.TextBox()
        Me.ProductName = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Algorithms1_Text
        '
        Me.Algorithms1_Text.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Algorithms1_Text.Location = New System.Drawing.Point(96, 24)
        Me.Algorithms1_Text.MaxLength = 4
        Me.Algorithms1_Text.Name = "Algorithms1_Text"
        Me.Algorithms1_Text.ReadOnly = True
        Me.Algorithms1_Text.Size = New System.Drawing.Size(72, 22)
        Me.Algorithms1_Text.TabIndex = 0
        Me.Algorithms1_Text.Text = "1971"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Algorithms1"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Activation Key"
        '
        'ActivationKey
        '
        Me.ActivationKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.ActivationKey.Location = New System.Drawing.Point(96, 148)
        Me.ActivationKey.MaxLength = 21
        Me.ActivationKey.Name = "ActivationKey"
        Me.ActivationKey.Size = New System.Drawing.Size(208, 22)
        Me.ActivationKey.TabIndex = 4
        Me.ActivationKey.TabStop = False
        '
        'Generator
        '
        Me.Generator.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Generator.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Generator.Location = New System.Drawing.Point(88, 192)
        Me.Generator.Name = "Generator"
        Me.Generator.Size = New System.Drawing.Size(136, 32)
        Me.Generator.TabIndex = 4
        Me.Generator.Text = "Generate Key"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 24)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Algorithms2"
        '
        'Algorithms2_Text
        '
        Me.Algorithms2_Text.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Algorithms2_Text.Location = New System.Drawing.Point(96, 56)
        Me.Algorithms2_Text.MaxLength = 2
        Me.Algorithms2_Text.Name = "Algorithms2_Text"
        Me.Algorithms2_Text.Size = New System.Drawing.Size(40, 22)
        Me.Algorithms2_Text.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 24)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Algorithms3"
        '
        'Algorithms3_Text
        '
        Me.Algorithms3_Text.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Algorithms3_Text.Location = New System.Drawing.Point(96, 94)
        Me.Algorithms3_Text.MaxLength = 2
        Me.Algorithms3_Text.Name = "Algorithms3_Text"
        Me.Algorithms3_Text.Size = New System.Drawing.Size(40, 22)
        Me.Algorithms3_Text.TabIndex = 2
        Me.Algorithms3_Text.Text = "30"
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(8, 128)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 8)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'KeyList
        '
        Me.KeyList.Location = New System.Drawing.Point(336, 24)
        Me.KeyList.Multiline = True
        Me.KeyList.Name = "KeyList"
        Me.KeyList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.KeyList.Size = New System.Drawing.Size(288, 152)
        Me.KeyList.TabIndex = 10
        '
        'ProductName
        '
        Me.ProductName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProductName.Items.AddRange(New Object() {"Num 2 Text", "Data Manger", "Dynamic Report", "Application Protector", "Binding Recordset", "Data Entry Validator", "Image Button", "Help Authority", "Form Flipper", "Form Translator", "Return Key Enable", "Google Instant Listing", "Top Paying Keywords"})
        Me.ProductName.Location = New System.Drawing.Point(144, 58)
        Me.ProductName.Name = "ProductName"
        Me.ProductName.Size = New System.Drawing.Size(152, 21)
        Me.ProductName.TabIndex = 11
        '
        'ActivationForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(634, 238)
        Me.Controls.Add(Me.ProductName)
        Me.Controls.Add(Me.KeyList)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Algorithms3_Text)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Algorithms2_Text)
        Me.Controls.Add(Me.Generator)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ActivationKey)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Algorithms1_Text)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ActivationForm"
        Me.Text = "Activation Key Generator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Function ZeroPad(ByVal str_String As String, ByVal int_Count As Byte) As String
        If str_String <> "" Then
            Return (New String("0", int_Count - Len(Trim(str_String))) & Trim(str_String))
        End If
    End Function

    Private Function CheckSum(ByVal strNum As String) As Byte
        Dim intCheckSum, blnDoubleFlag, X, intDigit As Integer

        For X = Len(strNum) To 1 Step -1
            intDigit = Asc(Mid$(strNum, X, 1))
            If intDigit > 47 Then
                If intDigit < 58 Then
                    intDigit = intDigit - 48

                    If blnDoubleFlag Then
                        intDigit = intDigit + intDigit
                        If intDigit > 9 Then
                            intDigit = intDigit - 9
                        End If
                    End If
                    blnDoubleFlag = Not blnDoubleFlag
                    intCheckSum = intCheckSum + intDigit
                    If intCheckSum > 9 Then
                        intCheckSum = intCheckSum - 10
                    End If
                End If
            End If
        Next
        Return intCheckSum
    End Function

    Private Sub Generator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Generator.Click
        Dim KeyRnd As Integer
        Dim MyKey As String
        Dim myChecksumKey As String
        Dim Rest As String
        Dim Part1, Part2, Part3, Part4, Part5 As String
        Dim ProductId As String
        Dim X As Integer



        Me.Generator.Text = "Generate New Key"
        Me.ActivationKey.Text = ""
        Me.KeyList.Text = ""

        If Not (CInt(Me.Algorithms1_Text.Text) >= 250 And CInt(Me.Algorithms1_Text.Text) <= 7000) Then
            MsgBox("Algorithms1 must be between 1000 and 7000")
            Exit Sub
        End If
        If Not (CInt(Me.Algorithms2_Text.Text) >= 10 And CInt(Me.Algorithms2_Text.Text) <= 99) Then
            MsgBox("Algorithms2 must be between 10 and 99")
            Exit Sub
        End If
        If Not (CInt(Me.Algorithms3_Text.Text) >= 10 And CInt(Me.Algorithms3_Text.Text) <= 99) Then
            MsgBox("Algorithms3 must be between 10 and 99")
            Exit Sub
        End If

        For X = 1 To 1000

Start:
            Randomize(CInt(Mid((Now.ToOADate * 1000000).ToString, 5, 6)))
            KeyRnd = Rnd() * 2000 + 2000
            MyKey = 13 * KeyRnd ^ 3 + 12 * KeyRnd ^ 2 + Algorithms2_Text.Text * KeyRnd ^ 1 + Algorithms3_Text.Text * KeyRnd ^ 0

            ProductId = ZeroPad(Me.Algorithms1_Text.Text * Me.Algorithms1_Text.Text, 8)
            Part1 = KeyRnd + CInt((Mid(ProductId, 1, 4)))
            If Part1 > 9999 Then GoTo Start
            Part1 = ZeroPad(Part1, 4) ' no must  , it must be 4 digit


            Part2 = CInt(Mid(MyKey, 1, 3)) + CInt(Mid(ProductId, 5, 1))
            If Part2 > 999 Then GoTo Start
            Part2 = ZeroPad(Part2, 3)

            Part3 = CInt(Mid(MyKey, 4, 3)) + CInt(Mid(ProductId, 6, 1))
            If Part3 > 999 Then GoTo Start
            Part3 = ZeroPad(Part3, 3)

            Part4 = CInt(Mid(MyKey, 7, 3)) + CInt(Mid(ProductId, 7, 1))
            If Part4 > 999 Then GoTo Start
            Part4 = ZeroPad(Part4, 3)

            Part5 = CInt(Mid(MyKey, 10, 3)) + CInt(Mid(ProductId, 8, 1))
            If Part5 > 999 Then GoTo Start
            Part5 = ZeroPad(Part5, 3)


            myChecksumKey = Part1 + Part2 + Part3 + Part4 + Part5 + "0"
            Rest = CheckSum(myChecksumKey)
            If Rest <> 0 Then
                myChecksumKey = Mid(myChecksumKey, 1, 16) + (CInt(Mid(myChecksumKey, 17, 1)) + 10 - Rest).ToString
            End If
            Me.ActivationKey.Text = Mid(myChecksumKey, 1, 4) + "-" + Mid(myChecksumKey, 5, 3) + "-" + Mid(myChecksumKey, 8, 3) + "-" + Mid(myChecksumKey, 11, 3) + "-" + Mid(myChecksumKey, 14, 4)
            Me.KeyList.Text += Me.ActivationKey.Text + vbCrLf

        Next X

    End Sub

    Private Sub ActivationForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Algorithms1_Text_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Algorithms1_Text.Leave, Algorithms2_Text.Leave, Algorithms3_Text.Leave
        If sender.Text.Length <> sender.maxlength Or Not IsNumeric(sender.Text) Then
            MsgBox("Algorithms" + Mid(sender.name, 11, 1) + " must be " + sender.maxlength.ToString + " Digits")
            Exit Sub
        End If
    End Sub

    Private Sub ProductName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductName.SelectedIndexChanged
        Me.Algorithms2_Text.Text = Me.ProductName.Items.IndexOf(Me.ProductName.Text) + 11
    End Sub
End Class
