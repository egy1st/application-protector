Imports System.Environment
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Namespace DynamicComponents

    Friend Class MyProtection

        Private Company As String = "Egyfirst Software,Inc."
        Private AuthorName As String = "Mohamed Ali Abbas Mohamed "
        Private AuthorNation As String = "Egypt-Alexandria-Smouha "
        Private AuthorPhone As String = "20-1007500290"
        Private AuthorMail As String = "mohamed.alyabbas@gmail.com"
        Private connect_status As Boolean

        Public NotLicensed As Boolean = False
        Private KeyAlgorithm1 As Integer
        Private KeyAlgorithm2 As Integer
        Private KeyAlgorithm3 As Integer
        Private LicenseFile As String
        Private Ended As Boolean = False
        Private BuyNowURL As String

        Private AuthorForm As New System.Windows.Forms.Form()
        Private AuthorLabel1 As New System.Windows.Forms.RichTextBox()
        Private AuthorLabel2 As New System.Windows.Forms.Label()
        Private AuthorLabel3 As New System.Windows.Forms.Label()
        Private AuthorLabel4 As New System.Windows.Forms.Label()
        Private AuthorLabel5 As New System.Windows.Forms.Label()
        Private AuthorLabel6 As New System.Windows.Forms.Label()
        Private AuthorLabel7 As New System.Windows.Forms.Label()
        Private AuthorText2 As New System.Windows.Forms.TextBox()
        Private AuthorText3 As New System.Windows.Forms.TextBox()
        Private AuthorText4 As New System.Windows.Forms.TextBox()
        Private AuthorText5 As New System.Windows.Forms.TextBox()
        Private AuthorCombo As New System.Windows.Forms.ComboBox()
        Private AuthorPrgress As New System.Windows.Forms.ProgressBar()
        Private AuthorButton1 As New System.Windows.Forms.Button()
        Private AuthorButton2 As New System.Windows.Forms.Button()
        Private AuthorBuyNow As New System.Windows.Forms.LinkLabel()
        Private Seprator1 As New System.Windows.Forms.GroupBox
        Private Seprator2 As New System.Windows.Forms.GroupBox

        'Private exitFlag As Boolean = False
        Private ProductID As String = ""
        Private companyID As String = ""
        Private WinSys As String
        Private ProductName As String
        Private CompanyInfo As String
        Private DaysLimit As Integer = 30
        Private myRegkey As String = ""
        Private licenseKey As String = ""
        Private ProductVer As String = ""
        Private encKey As String = "13121971@Mohamed-Ali-Abbas"
        Public Valid As Boolean = False
        Private MachinePrint As String = ""
        Private Registration_Key As String = ""
        Private ProductNum As String = "11"
        Private ProductV As String = "35"
        Private Reseller As String = "ComponentSource"
        Public Sub SetProductKeys(ByVal Company_ID As String, ByVal Product_ID As String, ByVal Product_Version As String)
            ProductID = Product_ID
            companyID = Company_ID
            ProductVer = Product_Version
        End Sub
        Public Sub SetInformation(ByVal BuyNow_URL As String, ByVal Message As String)
            BuyNowURL = BuyNow_URL
            CompanyInfo = Message
        End Sub
        Public Sub SetLicense(ByVal int_DaysLimit As Integer)
            DaysLimit = int_DaysLimit
        End Sub
        Public Sub setRegistrationKey(ByVal myKey As String)
            Registration_Key = myKey
        End Sub
        Public Sub SetAlgorithms(ByVal int_Algorithms1 As Integer, ByVal int_Algorithms2 As Integer, ByVal int_Algorithms3 As Integer)

            If int_Algorithms1 >= 1000 And int_Algorithms1 <= 7000 Then
                KeyAlgorithm1 = int_Algorithms1
            Else
                MsgBox("int_Algorithms1 must be between 1000 and 7000", , ProductID)
                Exit Sub
            End If

            If int_Algorithms2 >= 10 And int_Algorithms2 <= 99 Then
                KeyAlgorithm2 = int_Algorithms2
            Else
                MsgBox("int_Algorithms2 must be between 10 and 99", , ProductID)
                Exit Sub
            End If

            If int_Algorithms3 >= 10 And int_Algorithms3 <= 99 Then
                KeyAlgorithm3 = int_Algorithms3
            Else
                MsgBox("int_Algorithms3 must be between 10 and 99", , ProductID)
                Exit Sub
            End If
        End Sub
        Private Function Formula(ByVal Num As Integer, ByVal Mode As Byte) As Integer
            Dim result As String
            result = (13 * Num ^ 3 + 12 * Num ^ 2 + KeyAlgorithm2 * Num ^ 1 + KeyAlgorithm3 * Num ^ 0).ToString

            If Mode = 1 Then
                Return Mid(result, 1, 3)
            ElseIf Mode = 2 Then
                Return Mid(result, 4, 3)
            ElseIf Mode = 3 Then
                Return Mid(result, 7, 3)
            ElseIf Mode = 4 Then
                Return Mid(result, 10, 3)
            End If
        End Function

        Public Sub ShowAuthor()

            Dim dKey As Long
            Dim OldDaysNo As Long
            Dim WshShell As New Object()
            Dim FileNum As Integer
            Dim MyChar3 As String = "   "
            Dim MyChar1 As String = " "
            Dim MyChar5 As String = "     "
            Dim DaysNo As Long


#If Not Debug Then ' Release Mode
            If Validate(Internal_Registration_Key) = True Then
                Exit Sub
            End If
#End If


            LicenseFile = getMD5Hash(companyID + ProductID + ProductVer)
            licenseKey = getMD5Hash(Right(LicenseFile, 13) + Mid(LicenseFile, 13, 7) + Left(LicenseFile, 12))
            myRegkey = "HKEY_CURRENT_USER\software\" + companyID + "\" + ProductID + " " + ProductVer + "\"
            WinSys = GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\Dynamic Components\"
            Try
                Directory.CreateDirectory(GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\Dynamic Components\")
                My.Computer.Registry.CurrentUser.CreateSubKey(companyID)
                My.Computer.Registry.CurrentUser.CreateSubKey(ProductID)

            Catch e As Exception
                MsgBox("You should have an administrator privilege to activate the program")
                Exit Sub
            End Try
            My.Computer.Registry.SetValue(myRegkey, "license", LicenseFile)

            
            AuthorForm.Font = New Font("Arial", 14, FontStyle.Italic)
            AuthorForm.Width = 420
            AuthorForm.Height = 400
            AuthorForm.Text = "DC " + ProductID + " " + ProductVer
            AuthorForm.MaximizeBox = False
            AuthorForm.MinimizeBox = False
            AuthorForm.ControlBox = False

            AuthorForm.FormBorderStyle = FormBorderStyle.FixedDialog
            AuthorForm.CreateControl()

            AuthorLabel1.Left = 5
            AuthorLabel1.Top = 5
            AuthorLabel1.Width = 400
            AuthorLabel1.Height = 85
            AuthorLabel1.ReadOnly = True
            AuthorLabel1.Font = New Font("Arial", 14, FontStyle.Italic)
            AuthorLabel1.Text = CompanyInfo
            AuthorLabel1.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel1)


            Seprator1.Size = New System.Drawing.Size(400, 3)
            Seprator1.Location = New System.Drawing.Point(5, 133)
            Seprator1.Text = ""
            AuthorForm.Controls.Add(Seprator1)

            AuthorLabel3.Text = "Serial Key"
            AuthorLabel3.Location = New System.Drawing.Point(5, 145)
            AuthorLabel3.Size = New System.Drawing.Size(110, 20)
            AuthorLabel3.Font = New Font("Arial", 10, FontStyle.Regular)
            AuthorLabel3.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel3)

            AuthorText2.Location = New System.Drawing.Point(115, 145)
            AuthorText2.Size = New System.Drawing.Size(290, 25)
            AuthorText2.Font = New Font("Arial", 12, FontStyle.Regular)
            AuthorText2.MaxLength = 21
            AuthorText2.Text = "0000-000-000-000-0000"
            AuthorText2.CreateControl()
            AuthorForm.Controls.Add(AuthorText2)

            AuthorLabel4.Text = "Name"
            AuthorLabel4.Location = New System.Drawing.Point(5, 175)
            AuthorLabel4.Size = New System.Drawing.Size(110, 20)
            AuthorLabel4.Font = New Font("Arial", 10, FontStyle.Regular)
            AuthorLabel4.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel4)

            AuthorText3.Location = New System.Drawing.Point(115, 175)
            AuthorText3.Size = New System.Drawing.Size(290, 25)
            AuthorText3.Font = New Font("Arial", 12, FontStyle.Regular)
            AuthorText3.MaxLength = 50
            AuthorText3.CreateControl()
            AuthorForm.Controls.Add(AuthorText3)


            AuthorLabel6.Text = "Company"
            AuthorLabel6.Location = New System.Drawing.Point(5, 205)
            AuthorLabel6.Size = New System.Drawing.Size(110, 20)
            AuthorLabel6.Font = New Font("Arial", 10, FontStyle.Regular)
            AuthorLabel6.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel6)

            AuthorText5.Location = New System.Drawing.Point(115, 205)
            AuthorText5.Size = New System.Drawing.Size(290, 25)
            AuthorText5.Font = New Font("Arial", 12, FontStyle.Regular)
            AuthorText5.MaxLength = 50
            AuthorText5.CreateControl()
            AuthorForm.Controls.Add(AuthorText5)


            AuthorLabel7.Text = "Country"
            AuthorLabel7.Location = New System.Drawing.Point(5, 235)
            AuthorLabel7.Size = New System.Drawing.Size(110, 20)
            AuthorLabel7.Font = New Font("Arial", 10, FontStyle.Regular)
            AuthorLabel7.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel7)

            AuthorCombo.Location = New System.Drawing.Point(115, 235)
            AuthorCombo.Size = New System.Drawing.Size(290, 25)
            AuthorCombo.Font = New Font("Arial", 10, FontStyle.Regular)
            AuthorCombo.MaxLength = 50
            AuthorCombo.CreateControl()
            AuthorCombo.Items.AddRange(getCountries().ToArray())
            AuthorCombo.DropDownStyle = ComboBoxStyle.DropDownList
            AuthorForm.Controls.Add(AuthorCombo)


            AuthorLabel5.Text = "Email"
            AuthorLabel5.Location = New System.Drawing.Point(5, 265)
            AuthorLabel5.Size = New System.Drawing.Size(110, 20)
            AuthorLabel5.Font = New Font("Arial", 10, FontStyle.Regular)
            AuthorLabel5.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel5)

            AuthorText4.Location = New System.Drawing.Point(115, 265)
            AuthorText4.Size = New System.Drawing.Size(290, 25)
            AuthorText4.Font = New Font("Arial", 12, FontStyle.Regular)
            AuthorText4.MaxLength = 50
            AuthorText4.CreateControl()
            AuthorForm.Controls.Add(AuthorText4)


            Seprator2.Size = New System.Drawing.Size(400, 3)
            Seprator2.Location = New System.Drawing.Point(5, 305)
            Seprator2.Text = ""
            AuthorForm.Controls.Add(Seprator2)


            AuthorButton1.Location = New System.Drawing.Point(5, 325)
            AuthorButton1.Size = New System.Drawing.Size(110, 25)
            AuthorButton1.Font = New Font("Arial", 10, FontStyle.Bold)
            AuthorButton1.Text = "Register"
            AuthorButton1.CreateControl()
            AuthorForm.Controls.Add(AuthorButton1)

            AuthorBuyNow.Location = New System.Drawing.Point(170, 325)
            AuthorBuyNow.Size = New System.Drawing.Size(100, 25)
            AuthorBuyNow.Font = New Font("Arial", 12, FontStyle.Bold)
            AuthorBuyNow.Text = "Buy Now"
            AuthorBuyNow.Links.Add(0, 7, BuyNowURL)
            AuthorBuyNow.CreateControl()
            AddHandler AuthorBuyNow.LinkClicked, AddressOf Me.AuthorBuyNow_LinkClicked
            AddHandler AuthorForm.FormClosing, AddressOf Me.AuthorForm_Close
            AuthorForm.Controls.Add(AuthorBuyNow)

            AuthorButton2.Location = New System.Drawing.Point(295, 325)
            AuthorButton2.Size = New System.Drawing.Size(110, 25)
            AuthorButton2.Font = New Font("Arial", 10, FontStyle.Bold)


            FileNum = FreeFile()
            FileOpen(FileNum, WinSys + LicenseFile, OpenMode.Binary, OpenAccess.ReadWrite)
            FileGet(FileNum, MyChar1, 11, True)
            Dim regValue As String = My.Computer.Registry.GetValue(myRegkey, "s", Nothing)
            If MyChar1 = Chr(23) And regValue = licenseKey Then  'file is licensed
                FileClose(FileNum)
                showCredential()
                Exit Sub
            End If

            If MyChar1 = Chr(19) Or regValue = Chr(19) Then   'trial version has ended
                NotLicensed = True
                Ended = True

                AuthorPrgress.Location = New System.Drawing.Point(115, 100)
                AuthorPrgress.Size = New System.Drawing.Size(290, 25)
                AuthorPrgress.Minimum = 0
                AuthorPrgress.Maximum = DaysLimit
                AuthorPrgress.Value = DaysLimit
                AuthorPrgress.CreateControl()
                AuthorForm.Controls.Add(AuthorPrgress)

                AuthorLabel2.Text = "Expired"
                AuthorLabel2.Location = New System.Drawing.Point(5, 100)
                AuthorLabel2.Size = New System.Drawing.Size(110, 20)
                AuthorLabel2.Font = New Font("Arial", 10, FontStyle.Regular)
                AuthorLabel2.CreateControl()
                AuthorForm.Controls.Add(AuthorLabel2)

            End If
            FileClose(FileNum)

            If Not Ended Then
                If MyChar1 <> Chr(7) Or IsNothing(regValue) Then
                    AuthorButton2.Text = "Activate Trial" ' only for MAA
                Else
                    AuthorButton2.Text = "Free Trial"
                End If

                AuthorButton2.CreateControl()
                AuthorForm.Controls.Add(AuthorButton2)
                AddHandler AuthorButton2.Click, AddressOf AuthorButton2_Click
            Else
                AuthorButton2.Text = "Exit"
                AuthorButton2.CreateControl()
                AuthorForm.Controls.Add(AuthorButton2)
                AddHandler AuthorButton2.Click, AddressOf AuthorButton22_Click
            End If
            AddHandler AuthorButton1.Click, AddressOf AuthorButton1_Click


            If Ended Then
                NotLicensed = True
                AuthorForm.ShowDialog()
                AuthorForm.Dispose()
                Application.Exit()
                Exit Sub
            End If

            FileOpen(FileNum, WinSys + LicenseFile, OpenMode.Binary, OpenAccess.ReadWrite)
            FileGet(FileNum, MyChar5, 1, True)
            If adjTrim(MyChar5) <> "" Then
                dKey = CInt(DecryptMe(MyChar5))
            End If

            Dim regDaysValue As String = My.Computer.Registry.GetValue(myRegkey, "long", Nothing)
            Dim regdkey As Integer = 0

            If adjTrim(regDaysValue) <> "" Then
                regdkey = CInt(DecryptMe(regDaysValue))
            End If


            If dKey = 0 Or regdkey = 0 Then
                dKey = Today().ToOADate
                FilePut(FileNum, EncryptMe(dKey.ToString), 1)
                My.Computer.Registry.SetValue(myRegkey, "long", EncryptMe(dKey.ToString))
            End If

            AuthorPrgress.Location = New System.Drawing.Point(115, 100)
            AuthorPrgress.Size = New System.Drawing.Size(290, 25)
            AuthorPrgress.Minimum = 0
            AuthorPrgress.Maximum = DaysLimit
            AuthorPrgress.Value = IIf(Today().ToOADate - dKey <= DaysLimit, Today().ToOADate - dKey, DaysLimit)
            AuthorPrgress.CreateControl()
            AuthorForm.Controls.Add(AuthorPrgress)


            AuthorLabel2.Text = (DaysLimit - AuthorPrgress.Value).ToString + " days remain"
            AuthorLabel2.Location = New System.Drawing.Point(5, 100)
            AuthorLabel2.Size = New System.Drawing.Size(110, 20)
            AuthorLabel2.Font = New Font("Arial", 10, FontStyle.Regular)
            AuthorLabel2.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel2)


            FileGet(FileNum, MyChar5, 6, True)
            If adjTrim(MyChar5) <> "" Then
                OldDaysNo = CInt(DecryptMe(MyChar5))
            End If


            Dim regOldValue As String = My.Computer.Registry.GetValue(myRegkey, "old", Nothing)
            Dim regokey As Integer = 0
            If adjTrim(regOldValue) <> "" Then
                regokey = CInt(DecryptMe(regOldValue))
            End If


            If regokey < OldDaysNo Then
                regokey = OldDaysNo = regokey
            Else : OldDaysNo = regokey
            End If
            DaysNo = Today().ToOADate - dKey


            If DaysNo >= OldDaysNo And DaysNo < DaysLimit + 1 Then
                OldDaysNo = DaysNo
            ElseIf DaysNo < OldDaysNo Then
                NotLicensed = True
                AuthorButton2.Text = "Exit"
                AuthorButton2.CreateControl()
                AuthorForm.Controls.Add(AuthorButton2)
                AddHandler AuthorButton2.Click, AddressOf AuthorButton22_Click
                AuthorLabel2.Hide()
                AuthorPrgress.Hide()

                FileClose(FileNum)
                MsgBox("Please correct your time settings and try again", , ProductID)

                AuthorForm.ShowDialog()
                AuthorForm.Dispose()
                Application.Exit()
                Exit Sub
            Else
                NotLicensed = True
                AuthorButton2.Text = "Exit"
                AuthorButton2.CreateControl()
                AuthorForm.Controls.Add(AuthorButton2)
                AddHandler AuthorButton2.Click, AddressOf AuthorButton22_Click

                FilePut(FileNum, Chr(19), 11)
                My.Computer.Registry.SetValue(myRegkey, "s", Chr(19))
                FileClose(FileNum)

                AuthorForm.ShowDialog()
                AuthorForm.Dispose()
                Application.Exit()
                Exit Sub
            End If

            FilePut(FileNum, EncryptMe(OldDaysNo.ToString), 6)
            FileClose(FileNum)
            My.Computer.Registry.SetValue(myRegkey, "old", EncryptMe(OldDaysNo.ToString))

            'If AuthorButton2.Text = "Activate Trial" Then
            AuthorForm.ShowDialog()
            AuthorForm.Dispose()
            Application.Exit()
            'End If


        End Sub
        Private Sub AuthorButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

            Dim MyKey As String
            Dim KeyRnd As Integer
            Dim KeyVer As String
            Dim CheckValidity As Boolean = False
            Dim FileNum As Integer
            Dim MyChar1 As String = " "

            Dim str_result As String = ""
            Dim str_link As String = "http://www.mygoldensoft.com/purchase.php"


            MachinePrint = "Demo"
            If MachinePrint = "" Then
                MsgBox("We cannot retrieve your machine fingerprint." + vbCrLf + "Please contact support@mygoldensoft.com", , ProductID)
                Exit Sub
            End If

            str_link += "?para1=" + companyID
            str_link += "&para2=" + ProductID
            str_link += "&para3=" + ProductVer
            str_link += "&para4=" + licenseKey
            str_link += "&para5=" + AuthorText3.Text.Trim ' user name
            str_link += "&para6=" + AuthorText4.Text.Trim ' user email
            str_link += "&para7=" + AuthorText5.Text.Trim ' user company
            str_link += "&para8=" + AuthorCombo.Text.Trim ' user country
            str_link += "&para9=" + MachinePrint
            str_link += "&para10=" + ProductNum ' for validating onsite only
            str_link += "&para11=" + ProductV ' for validating onsite only
            str_link += "&para12=" + Reseller
            str_link += "&para13=" + AuthorText2.Text.Trim


            Dim syshash As Hashtable = Environment.GetEnvironmentVariables
            str_link += "&para14=" + syshash("PROCESSOR_IDENTIFIER")
            str_link += "&para15=" + syshash("NUMBER_OF_PROCESSORS")
            str_link += "&para16=" + syshash("PROCESSOR_ARCHITECTURE")
            str_link += "&para17=" + syshash("OS")
            If (syshash("VisualStudioDir")) <> "" Then
                str_link += "&para18=" + syshash("VisualStudioDir").ToString.Replace("\", "\\")
            End If
            If syshash("LOGONSERVER") <> "" Then
                str_link += "&para19=" + syshash("LOGONSERVER").ToString.Replace("\", "\\")
            End If
            str_link += "&para20=" + syshash("USERNAME")
            str_link += "&para21=" + syshash("COMPUTERNAME")

            If AuthorText2.Text = "" Then
                MsgBox("Please enter your registration key", , ProductID)
                Exit Sub
            End If

            If AuthorText3.Text = "" Then
                MsgBox("Please enter your name", , ProductID)
                Exit Sub
            End If

            If AuthorText4.Text = "" Then
                MsgBox("Please enter your email", , ProductID)
                Exit Sub
            End If

            If Not IsValidEmail(AuthorText4.Text) Then
                MsgBox("Please enter a valid email", , ProductID)
                Exit Sub
            End If

            'On Error GoTo ErrorMsg

            If AuthorText2.Text = "" Then
                MsgBox("Please enter your registration key", , ProductID)
                Exit Sub
            End If

            MyKey = AuthorText2.Text
            KeyRnd = Mid(MyKey, 1, 4)
            KeyVer = ZeroPad((KeyAlgorithm1 * KeyAlgorithm1).ToString, 8)
            KeyRnd -= CInt(Mid(KeyVer, 1, 4))
            '---------------------------------------------------------------------
            If Not CheckSum(Mid(AuthorText2.Text, 1, 4) + Mid(AuthorText2.Text, 6, 3) + Mid(AuthorText2.Text, 10, 3) + Mid(AuthorText2.Text, 14, 3) + Mid(AuthorText2.Text, 18, 4)) = True Then
                MsgBox("Invalid Key", , ProductID)
                Exit Sub
            End If
            '---------------------------------------------------------------------
            If Mid(MyKey, 6, 3) <> Formula(KeyRnd, 1) + CInt(Mid(KeyVer, 5, 1)) Then
                MsgBox("Invalid Key", , ProductID)
                Exit Sub
            End If

            If Mid(MyKey, 10, 3) <> Formula(KeyRnd, 2) + CInt(Mid(KeyVer, 6, 1)) Then
                MsgBox("Invalid Key", , ProductID)
                Exit Sub
            End If

            If Mid(MyKey, 14, 3) <> Formula(KeyRnd, 3) + CInt(Mid(KeyVer, 7, 1)) Then
                MsgBox("Invalid Key", , ProductID)
                Exit Sub
            End If

            If Mid(MyKey, 18, 3) <> Formula(KeyRnd, 4) + CInt(Mid(KeyVer, 8, 1)) Then
                MsgBox("Invalid Key", , ProductID)
                Exit Sub
            Else
                NotLicensed = False
                CheckValidity = True
            End If


            '--------------------------------------------------------------------
            If CheckValidity = True Then
Re_Connect:

                str_result = GetPageHTML(str_link).Trim
                If (connect_status = False) Then
                    Dim msg_res As Integer
                    msg_res = MsgBox("We cannot connect to our server to activate your trial period." + vbCrLf + "Go online and try again.", MsgBoxStyle.RetryCancel, "Go online to activate")
                    If msg_res = 4 Then
                        GoTo Re_Connect
                    Else
                        Application.Exit()
                    End If
                    Valid = False
                    Exit Sub
                ElseIf str_result = "VALID" Then 'Valid
                    FileNum = FreeFile()
                    FileOpen(FileNum, WinSys + LicenseFile, OpenMode.Binary, OpenAccess.ReadWrite)
                    FilePut(FileNum, Chr(23), 11)
                    My.Computer.Registry.SetValue(myRegkey, "s", licenseKey)
                    My.Computer.Registry.SetValue(myRegkey, "name", EncryptTripleDES(AuthorText3.Text, encKey))
                    My.Computer.Registry.SetValue(myRegkey, "email", EncryptTripleDES(AuthorText4.Text, encKey))
                    My.Computer.Registry.SetValue(myRegkey, "company", EncryptTripleDES(AuthorText5.Text, encKey))
                    My.Computer.Registry.SetValue(myRegkey, "country", EncryptTripleDES(Me.AuthorCombo.Text, encKey))
                    My.Computer.Registry.SetValue(myRegkey, "serial", EncryptTripleDES(Me.AuthorText2.Text, encKey))

                    FileClose(FileNum)
                    MsgBox("Registration Succeeded" + vbCrLf + "Thank you for purchasing DC " + ProductID + " " + ProductVer, , ProductID)
                    Valid = True
                    AuthorForm.Dispose()
                    Application.Exit()

                ElseIf str_result = "NOMATCH" Then ' negelct it now 'different machineprint
                    MsgBox("Our database shows that your machine fingerprint is different." + vbCrLf + "Please contact support@mygoldensoft.com with your registered name, email, serial number and this issue code " + vbCrLf + MachinePrint, , ProductID)

                ElseIf CheckValidity = False Or str_result = "INVALID" Then
                    MsgBox("Invalid Key", , ProductID)
                End If
            End If

            Exit Sub
            'ErrorMsg:
            'MsgBox("Invalid key or bad connection", , ProductID)

        End Sub
        Private Sub AuthorButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If AuthorButton2.Text = "Activate Trial" Then
                If AuthorText3.Text.Trim = "" Then
                    MsgBox("Please enter your name.", , ProductID)
                    Exit Sub
                End If
                If AuthorText5.Text.Trim = "" Then
                    MsgBox("Please enter your company.", , ProductID)
                    Exit Sub
                End If
                If AuthorCombo.Text.Trim = "" Then
                    MsgBox("Please enter your country.", , ProductID)
                    Exit Sub
                End If

                If AuthorText4.Text.Trim = "" Then
                    MsgBox("Please enter your email.", , ProductID)
                    Exit Sub
                End If
                If Not IsValidEmail(AuthorText4.Text) Then
                    MsgBox("Please enter a valid email", , ProductID)
                    Exit Sub
                End If



Re_Connect:

                MachinePrint = "Demo"
                If MachinePrint = "Demo" Then

                    Dim str_result As String = ""
                    Dim str_link As String = "http://www.mygoldensoft.com/register.php"
                    str_link += "?para1=" + companyID
                    str_link += "&para2=" + ProductID
                    str_link += "&para3=" + ProductVer
                    str_link += "&para4=" + licenseKey
                    str_link += "&para5=" + AuthorText3.Text.Trim ' user name
                    str_link += "&para6=" + AuthorText4.Text.Trim ' user email
                    str_link += "&para7=" + AuthorText5.Text.Trim ' user company
                    str_link += "&para8=" + AuthorCombo.Text.Trim ' user country
                    str_link += "&para9=" + MachinePrint
                    str_link += "&para10=" + ProductNum ' for validating onsite only
                    str_link += "&para11=" + ProductV ' for validating onsite only
                    str_link += "&para12=" + Reseller
                    str_link += "&para13=" + "0000-000-000-000-0000"


                    Dim syshash As Hashtable = Environment.GetEnvironmentVariables
                    str_link += "&para14=" + syshash("PROCESSOR_IDENTIFIER")
                    str_link += "&para15=" + syshash("NUMBER_OF_PROCESSORS")
                    str_link += "&para16=" + syshash("PROCESSOR_ARCHITECTURE")
                    str_link += "&para17=" + syshash("OS")
                    If (syshash("VisualStudioDir")) <> "" Then
                        str_link += "&para18=" + syshash("VisualStudioDir").ToString.Replace("\", "\\")
                    End If
                    If syshash("LOGONSERVER") <> "" Then
                        str_link += "&para19=" + syshash("LOGONSERVER").ToString.Replace("\", "\\")
                    End If
                    str_link += "&para20=" + syshash("USERNAME")
                    str_link += "&para21=" + syshash("COMPUTERNAME")


                    str_result = GetPageHTML(str_link)
                    If (connect_status = False) Then
                        Valid = False
                        Dim msg_res As Integer
                        msg_res = MsgBox("We cannot connect to our server to activate your trial period." + vbCrLf + "Go online and try again.", MsgBoxStyle.RetryCancel, "Go online to activate")
                        If msg_res = 4 Then
                            GoTo Re_Connect
                        Else
                            AuthorForm.Dispose()
                            Application.Exit()
                            Exit Sub
                        End If
                        Exit Sub
                    ElseIf str_result = "FOUND7777777" Then ' to neglect it
                        Valid = False
                        MsgBox("Our database shows that your machine fingerprint is already registered with us." + vbCrLf + "You should get a license key.", , ProductID)
                        AuthorButton2.Text = "Exit"
                        Exit Sub
                    Else ' Yes First Time
                        Valid = True
                        Dim FileNum As Integer
                        FileNum = FreeFile()
                        FileOpen(FileNum, WinSys + LicenseFile, OpenMode.Binary, OpenAccess.ReadWrite)
                        FilePut(FileNum, Chr(7), 11) ' not first time
                        FileClose(FileNum)
                        My.Computer.Registry.SetValue(myRegkey, "s", Chr(7))
                        MsgBox("Your trial period has started.", , ProductID)
                    End If
                Else
                    MsgBox("Invalid verification code. Please check your registered email or register another one.", , ProductID)
                End If


            ElseIf AuthorButton2.Text = "Free Trial" Then
                Valid = True
            ElseIf AuthorButton2.Text = "Exit" Then
                AuthorForm.Dispose()
                Application.Exit()
                Exit Sub
            End If

            If Not AuthorForm.IsDisposed Then
                AuthorForm.Close()
            End If
        End Sub
        Private Sub AuthorButton22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            AuthorForm.Dispose()
            Application.Exit()
        End Sub
        Private Sub AuthorBuyNow_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
            Dim buyURL
            buyURL = regGetBuyURL("EgyFirst Software", "DC Returnkey Enabled", "3.5")

            If buyURL Is Nothing Then
                ' BuyURL doesn't exsits in registry, default it
                buyURL = "http://www.mygoldensoft.com/ordernow.html"
            End If
            AuthorBuyNow.Links(AuthorBuyNow.Links.IndexOf(e.Link)).Visited = True
            System.Diagnostics.Process.Start(buyURL)
        End Sub
        Private Function GetPageHTML( _
           ByVal URL As String) As String
            ' Retrieves the HTML from the specified URL
            connect_status = True
            'On Error GoTo error_msg

            Dim objWC As New System.Net.WebClient()
            Return New System.Text.UTF8Encoding().GetString( _
               objWC.DownloadData(URL))
            'error_msg:
            'connect_status = False
        End Function
        Private Sub AuthorForm_Close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
            e.Cancel = Not Valid
        End Sub
        Private Function Validate(ByVal myKey As String) As Boolean

            Dim KeyRnd As Integer
            Dim KeyVer As String

            'On Error GoTo erroMsg

            If myKey = "" Or Len(myKey) <> 21 Then
                Return False
            End If

            KeyRnd = Mid(myKey, 1, 4)
            KeyVer = ZeroPad((KeyAlgorithm1 * KeyAlgorithm1).ToString, 8)
            KeyRnd -= CInt(Mid(KeyVer, 1, 4))
            '---------------------------------------------------------------------
            If Not CheckSum(Mid(myKey, 1, 4) + Mid(myKey, 6, 3) + Mid(myKey, 10, 3) + Mid(myKey, 14, 3) + Mid(myKey, 18, 4)) = True Then
                Return False
            End If
            '---------------------------------------------------------------------
            If Mid(myKey, 6, 3) <> Formula(KeyRnd, 1) + CInt(Mid(KeyVer, 5, 1)) Then
                Return False
            End If

            If Mid(myKey, 10, 3) <> Formula(KeyRnd, 2) + CInt(Mid(KeyVer, 6, 1)) Then
                Return False
            End If

            If Mid(myKey, 14, 3) <> Formula(KeyRnd, 3) + CInt(Mid(KeyVer, 7, 1)) Then
                Return False
            End If

            If Mid(myKey, 18, 3) <> Formula(KeyRnd, 4) + CInt(Mid(KeyVer, 8, 1)) Then
                Return False
            End If

            NotLicensed = False

            Return True
erroMsg:
            Return False


        End Function

        Sub showCredential()
            AuthorPrgress.Visible = False
            AuthorLabel2.Visible = False

            Dim _serial As String = My.Computer.Registry.GetValue(myRegkey, "serial", Nothing)
            _serial = DecryptTripleDES(_serial, encKey)
            AuthorText2.Text = _serial
            AuthorText2.Enabled = False

            Dim _name As String = My.Computer.Registry.GetValue(myRegkey, "name", Nothing)
            _name = DecryptTripleDES(_name, encKey)
            AuthorText3.Text = _name
            AuthorText3.Enabled = False

            Dim _email As String = My.Computer.Registry.GetValue(myRegkey, "email", Nothing)
            _email = DecryptTripleDES(_email, encKey)
            AuthorText4.Text = _email
            AuthorText4.Enabled = False

            Dim _company As String = My.Computer.Registry.GetValue(myRegkey, "company", Nothing)
            _company = DecryptTripleDES(_company, encKey)
            AuthorText5.Text = _company
            AuthorText5.Enabled = False

            Dim _country As String = My.Computer.Registry.GetValue(myRegkey, "country", Nothing)
            _country = DecryptTripleDES(_country, encKey)
            AuthorCombo.Text = _country
            AuthorCombo.Enabled = False

            AuthorLabel1.Text = "DC " + ProductID + " " + ProductVer + vbCrLf + "Powered by " + companyID + ", Inc." + vbCrLf + "Licensed Version"

            AuthorButton2.Text = "OK"
            AuthorButton2.CreateControl()
            AuthorForm.Controls.Add(AuthorButton2)
            AddHandler AuthorButton2.Click, AddressOf AuthorButton22_Click

            AuthorButton1.Enabled = False

            AuthorForm.ShowDialog()
            AuthorForm.Dispose()
            Application.Exit()

        End Sub


    End Class
End Namespace



