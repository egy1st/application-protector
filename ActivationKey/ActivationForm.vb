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
    Friend WithEvents Generator As System.Windows.Forms.Button
    Friend WithEvents Algorithms1_Text As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Algorithms2_Text As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Algorithms3_Text As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents msg As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents KeyList As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ActivationForm))
        Me.Algorithms1_Text = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Generator = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Algorithms2_Text = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Algorithms3_Text = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.KeyList = New System.Windows.Forms.TextBox
        Me.msg = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Algorithms1_Text
        '
        Me.Algorithms1_Text.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Algorithms1_Text.Location = New System.Drawing.Point(103, 27)
        Me.Algorithms1_Text.MaxLength = 4
        Me.Algorithms1_Text.Name = "Algorithms1_Text"
        Me.Algorithms1_Text.Size = New System.Drawing.Size(98, 29)
        Me.Algorithms1_Text.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(15, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Algorithms1"
        '
        'Generator
        '
        Me.Generator.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Generator.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Generator.Location = New System.Drawing.Point(12, 187)
        Me.Generator.Name = "Generator"
        Me.Generator.Size = New System.Drawing.Size(299, 39)
        Me.Generator.TabIndex = 4
        Me.Generator.Text = "Generate Keys"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(15, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 24)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Algorithms2"
        '
        'Algorithms2_Text
        '
        Me.Algorithms2_Text.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Algorithms2_Text.Location = New System.Drawing.Point(103, 69)
        Me.Algorithms2_Text.MaxLength = 2
        Me.Algorithms2_Text.Name = "Algorithms2_Text"
        Me.Algorithms2_Text.Size = New System.Drawing.Size(51, 29)
        Me.Algorithms2_Text.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(15, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 24)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Algorithms3"
        '
        'Algorithms3_Text
        '
        Me.Algorithms3_Text.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Algorithms3_Text.Location = New System.Drawing.Point(103, 111)
        Me.Algorithms3_Text.MaxLength = 2
        Me.Algorithms3_Text.Name = "Algorithms3_Text"
        Me.Algorithms3_Text.Size = New System.Drawing.Size(51, 29)
        Me.Algorithms3_Text.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(8, 166)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 8)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'KeyList
        '
        Me.KeyList.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyList.Location = New System.Drawing.Point(337, 12)
        Me.KeyList.Multiline = True
        Me.KeyList.Name = "KeyList"
        Me.KeyList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.KeyList.Size = New System.Drawing.Size(247, 162)
        Me.KeyList.TabIndex = 10
        '
        'msg
        '
        Me.msg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.msg.Location = New System.Drawing.Point(337, 189)
        Me.msg.MaxLength = 2
        Me.msg.Name = "msg"
        Me.msg.ReadOnly = True
        Me.msg.Size = New System.Drawing.Size(247, 26)
        Me.msg.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(223, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 24)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "(1000-8000)"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(223, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 24)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "(10-99)"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(223, 116)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 24)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "(10-99)"
        '
        'ActivationForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(601, 238)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.msg)
        Me.Controls.Add(Me.KeyList)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Algorithms3_Text)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Algorithms2_Text)
        Me.Controls.Add(Me.Generator)
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
        Dim ActivationKey As String
        Dim KeyRnd As Integer
        Dim MyKey As String
        Dim myChecksumKey As String
        Dim Rest As String
        Dim Part1, Part2, Part3, Part4, Part5 As String
        Dim ProductId As String
        Dim X As Integer
        Static num As Integer = 0


        Me.Generator.Text = "Generate more Keys"
        ActivationKey = ""
        'Me.KeyList.Text = ""

        If Me.Algorithms1_Text.Text = "" Then
            MsgBox("Please enter a number")
            Exit Sub
        ElseIf Not (CInt(Me.Algorithms1_Text.Text) >= 1000 And CInt(Me.Algorithms1_Text.Text) <= 7000) Then
            MsgBox("Algorithms1 must be between 1000 and 7000")
            Exit Sub
        End If

        If Me.Algorithms2_Text.Text = "" Then
            MsgBox("Please enter a number")
            Exit Sub
        ElseIf Not (CInt(Me.Algorithms2_Text.Text) >= 10 And CInt(Me.Algorithms2_Text.Text) <= 99) Then
            MsgBox("Algorithms2 must be between 10 and 99")
            Exit Sub
        End If

        If Me.Algorithms3_Text.Text = "" Then
            MsgBox("Please enter a number")
            Exit Sub
        ElseIf Not (CInt(Me.Algorithms3_Text.Text) >= 10 And CInt(Me.Algorithms3_Text.Text) <= 99) Then
            MsgBox("Algorithms3 must be between 10 and 99")
            Exit Sub
        End If

        For X = 1 To 100

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
            ActivationKey = Mid(myChecksumKey, 1, 4) + "-" + Mid(myChecksumKey, 5, 3) + "-" + Mid(myChecksumKey, 8, 3) + "-" + Mid(myChecksumKey, 11, 3) + "-" + Mid(myChecksumKey, 14, 4)
            Me.KeyList.Text += ActivationKey + vbCrLf

        Next X
        num += 100
        msg.Text = num.ToString + " keys have been generated"

    End Sub

    Private Sub Algorithms1_Text_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Algorithms1_Text.Leave, Algorithms2_Text.Leave, Algorithms3_Text.Leave
        If sender.Text.Length <> sender.maxlength Or Not IsNumeric(sender.Text) Then
            MsgBox("Algorithms" + Mid(sender.name, 11, 1) + " should be " + sender.maxlength.ToString + " Digits")
            Exit Sub
        End If
    End Sub

    
End Class
