Imports System.Environment
Imports System.IO
Namespace DynamicComponents
    Public Class AppProtector

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
        Private AuthorText2 As New System.Windows.Forms.TextBox()
        Private AuthorPrgress As New System.Windows.Forms.ProgressBar()
        Private AuthorButton1 As New System.Windows.Forms.Button()
        Private AuthorButton2 As New System.Windows.Forms.Button()
        Private AuthorBuyNow As New System.Windows.Forms.LinkLabel()
        Private ProductID As String = ""
        Private companyID As String = ""
        Private WinSys As String
        Private ProductName As String
        Private CompanyInfo As String
        Private DaysLimit As Integer = 30
        Private myRegkey As String = ""
        Private licenseKey As String = ""
        Private ProductVer As String = ""
        Public Valid As Boolean = False
        Private MachinePrint As String = ""
        Public Shared Internal_Registration_Key As String = "0000-000-000-000-0000"
        Dim currentDomain As AppDomain = AppDomain.CurrentDomain
        Private Sub MYExnHandler(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
            Dim EX As Exception
            EX = e.ExceptionObject
            Dim str_error As String = "http://www.mygoldensoft.com/notify.php?error=" + EX.StackTrace
            Dim result_error As String = GetPageHTML(str_error)
        End Sub
        Private Sub MYThreadHandler(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
            Dim str_error As String = "http://www.mygoldensoft.com/notify.php?error=" + e.Exception.StackTrace
            Dim result_error As String = GetPageHTML(str_error)
        End Sub
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
            Internal_Registration_Key = myKey
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


          

            ' Define a handler for unhandled exceptions.
            AddHandler currentDomain.UnhandledException, AddressOf MYExnHandler

            ' Define a handler for unhandled exceptions for threads behind forms.
            AddHandler Application.ThreadException, AddressOf MYThreadHandler

            
            LicenseFile = getMD5Hash(companyID + ProductID + ProductVer)
            licenseKey = getMD5Hash(Right(LicenseFile, 13) + Mid(LicenseFile, 13, 7) + Left(LicenseFile, 12))
            myRegkey = "HKEY_CURRENT_USER\software\" + companyID + "\" + ProductID + " " + ProductVer + "\"
            WinSys = GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\" + companyID + "\"

            Try
                Directory.CreateDirectory(GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\" + companyID + "\")
                My.Computer.Registry.CurrentUser.CreateSubKey(companyID)
                My.Computer.Registry.CurrentUser.CreateSubKey(ProductID)

            Catch
                MsgBox("You should have an administrator privilege to activate the program")
                Exit Sub
            End Try


            My.Computer.Registry.SetValue(myRegkey, "license", LicenseFile)

            FileNum = FreeFile()
            FileOpen(FileNum, WinSys + LicenseFile, OpenMode.Binary, OpenAccess.ReadWrite)
            FileGet(FileNum, MyChar1, 11, True)
            Dim regValue As String = My.Computer.Registry.GetValue(myRegkey, "s", Nothing)
            If MyChar1 = Chr(23) And regValue = licenseKey Then  'file is licensed
                FileClose(FileNum)
                Exit Sub
            End If

            If MyChar1 = Chr(19) Or regValue = Chr(19) Then   'trial version is ended
                NotLicensed = True
                Ended = True

                AuthorPrgress.Location = New System.Drawing.Point(5, 165)
                AuthorPrgress.Size = New System.Drawing.Size(330, 25)
                AuthorPrgress.Minimum = 0
                AuthorPrgress.Maximum = DaysLimit
                AuthorPrgress.Value = DaysLimit
                AuthorPrgress.CreateControl()
                AuthorForm.Controls.Add(AuthorPrgress)

                AuthorLabel2.Text = "Expired"
                AuthorLabel2.Location = New System.Drawing.Point(5, 140)
                AuthorLabel2.Size = New System.Drawing.Size(300, 20)
                AuthorLabel2.Font = New Font("Arial", 10, FontStyle.Regular)
                AuthorLabel2.CreateControl()
                AuthorForm.Controls.Add(AuthorLabel2)

            End If
            FileClose(FileNum)

            AuthorForm.Font = New Font("Arial", 14, FontStyle.Italic)
            AuthorForm.Width = 350
            AuthorForm.Height = 350
            AuthorForm.Text = ProductID + " " + ProductVer
            AuthorForm.MaximizeBox = False
            AuthorForm.MinimizeBox = False
            AuthorForm.ControlBox = False

            AuthorForm.FormBorderStyle = FormBorderStyle.FixedDialog
            AuthorForm.CreateControl()

            AuthorLabel1.Left = 5
            AuthorLabel1.Top = 5
            AuthorLabel1.Width = 330
            AuthorLabel1.Height = 120
            AuthorLabel1.ReadOnly = True
            AuthorLabel1.Text = CompanyInfo
            AuthorLabel1.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel1)

            AuthorLabel3.Text = "Registration Key"
            AuthorLabel3.Location = New System.Drawing.Point(5, 205)
            AuthorLabel3.Size = New System.Drawing.Size(300, 20)
            AuthorLabel3.Font = New Font("Arial", 10, FontStyle.Regular)
            AuthorLabel3.CreateControl()
            AuthorForm.Controls.Add(AuthorLabel3)

            AuthorText2.Location = New System.Drawing.Point(5, 230)
            AuthorText2.Size = New System.Drawing.Size(330, 25)
            AuthorText2.Font = New Font("Arial", 12, FontStyle.Regular)
            AuthorText2.MaxLength = 21
            AuthorText2.CreateControl()
            AuthorForm.Controls.Add(AuthorText2)


            AuthorButton1.Location = New System.Drawing.Point(5, 275)
            AuthorButton1.Size = New System.Drawing.Size(100, 25)
            AuthorButton1.Font = New Font("Arial", 10, FontStyle.Bold)
            AuthorButton1.Text = "Register"
            AuthorButton1.CreateControl()
            AuthorForm.Controls.Add(AuthorButton1)

            AuthorBuyNow.Location = New System.Drawing.Point(135, 275)
            AuthorBuyNow.Size = New System.Drawing.Size(80, 25)
            AuthorBuyNow.Font = New Font("Arial", 12, FontStyle.Bold)
            AuthorBuyNow.Text = "Buy Now"
            AuthorBuyNow.Links.Add(0, 7, BuyNowURL)
            AddHandler AuthorBuyNow.LinkClicked, AddressOf Me.AuthorBuyNow_LinkClicked
            AddHandler AuthorForm.Closing, AddressOf Me.AuthorForm_Close
            AuthorForm.Controls.Add(AuthorBuyNow)

            AuthorButton2.Location = New System.Drawing.Point(240, 275)
            AuthorButton2.Size = New System.Drawing.Size(100, 25)
            AuthorButton2.Font = New Font("Arial", 10, FontStyle.Bold)
            If Not Ended Then
                If MyChar1 <> Chr(7) And IsNothing(regValue) Then
                    'AuthorButton2.Text = "Activate Trial" ' only for MAA
                    AuthorButton2.Text = "Free Trial"
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
                'MsgBox("Your " + DaysLimit.ToString + " days evaluation period has expired", , ProductID)
                AuthorForm.ShowDialog()
                AuthorForm.Dispose()
                'AuthorForm.Owner.Close()
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

            AuthorPrgress.Location = New System.Drawing.Point(5, 165)
            AuthorPrgress.Size = New System.Drawing.Size(330, 25)
            AuthorPrgress.Minimum = 0
            AuthorPrgress.Maximum = DaysLimit
            AuthorPrgress.Value = IIf(Today().ToOADate - dKey <= DaysLimit, Today().ToOADate - dKey, DaysLimit)
            AuthorPrgress.CreateControl()
            AuthorForm.Controls.Add(AuthorPrgress)


            AuthorLabel2.Text = (DaysLimit - AuthorPrgress.Value).ToString + " days remain"
            AuthorLabel2.Location = New System.Drawing.Point(5, 140)
            AuthorLabel2.Size = New System.Drawing.Size(300, 20)
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
                'AuthorForm.Owner.Close()
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
                'MsgBox("Your " + DaysLimit.ToString + " days evaluation period has expired", , ProductID)

                AuthorForm.ShowDialog()
                AuthorForm.Dispose()
                'AuthorForm.Owner.Close()
                Application.Exit()
                Exit Sub
            End If

            FilePut(FileNum, EncryptMe(OldDaysNo.ToString), 6)
            FileClose(FileNum)
            My.Computer.Registry.SetValue(myRegkey, "old", EncryptMe(OldDaysNo.ToString))

            AuthorForm.ShowDialog()
        End Sub
        Private Sub AuthorButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim MyName As String
            Dim OldStr As String
            Dim Num As Integer
            Dim MyKey As String
            Dim FileRead As String
            Dim FileWrite As String
            Dim KeyRnd As Integer
            Dim KeyVer As String
            Dim CheckValidity As Boolean = False

            On Error GoTo ErrorMsg

            If AuthorText2.Text = "" Then
                MsgBox("Please enter your registration key", , ProductID)
                Exit Sub
            End If

            MyKey = AuthorText2.Text
            KeyRnd = Mid(MyKey, 1, 4)
            KeyVer = ZeroPad((KeyAlgorithm1 * KeyAlgorithm1).ToString, 8) '= '00012100'
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
            End If

            NotLicensed = False
            CheckValidity = True

            '--------------------------------------------------------------------
            If CheckValidity = True Then
                Dim FileNum As Integer
                FileNum = FreeFile()
                FileOpen(FileNum, WinSys + LicenseFile, OpenMode.Binary, OpenAccess.ReadWrite)
                FilePut(FileNum, Chr(23), 11)
                FileClose(FileNum)
                My.Computer.Registry.SetValue(myRegkey, "s", licenseKey)
                Valid = True
                MsgBox("Registration Succeeded" + vbCrLf + "Thank you for purchasing " + ProductID, , ProductID)
                AuthorForm.Dispose()
                'AuthorForm.Owner.Close()
                Application.Exit()
            ElseIf CheckValidity = False Then
                MsgBox("Invalid Key", , ProductID)
            End If

            Exit Sub
ErrorMsg:
            MsgBox("Invalid key or bad connection", , ProductID)
        End Sub
        Private Sub AuthorButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If AuthorButton2.Text = "Activate Trial" Then

Re_Connect:
                Dim str_result As String = ""
                Dim str_link As String = "http://www.mygoldensoft.com/verify.php"
                str_link += "?para1=" + companyID
                str_link += "&para2=" + ProductID
                str_link += "&para3=" + ProductVer
                str_link += "&para4=" + licenseKey
                str_link += "&para5=" + MachinePrint

                str_result = GetPageHTML(str_link)

                If (connect_status = False) Then

                    Dim msg_res As Integer
                    msg_res = MsgBox("We cannot connect to our server to activate your trial period." + vbCrLf + "Go online and try again.", MsgBoxStyle.RetryCancel, "Go online to activate")
                    If msg_res = 4 Then
                        GoTo Re_Connect
                    Else
                        AuthorForm.Dispose()
                        'AuthorForm.Owner.Close()
                        Application.Exit()
                    End If
                    Valid = False
                    Exit Sub
                ElseIf str_result = "FOUND" Then
                    Valid = False
                    MsgBox("Your machine fingerprint shows that you are already registered with us." + vbCrLf + "You should get a license key.", , ProductID)
                    AuthorForm.Dispose()
                    'AuthorForm.Owner.Close()
                    Application.Exit()
                Else ' Yes First Time
                    Valid = True
                    Dim FileNum As Integer
                    FileNum = FreeFile()
                    FileOpen(FileNum, WinSys + LicenseFile, OpenMode.Binary, OpenAccess.ReadWrite)
                    FilePut(FileNum, Chr(7), 11) ' not first time
                    FileClose(FileNum)
                    MsgBox("Your trial period has started.", , ProductID)
                End If

            ElseIf AuthorButton2.Text = "Free Trial" Then
                Valid = True
            End If

            If Not AuthorForm.IsDisposed Then
                AuthorForm.Close()
            End If
        End Sub
        Private Sub AuthorButton22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            AuthorForm.Dispose()
            'AuthorForm.Owner.Close()
            Application.Exit()
        End Sub
        Private Sub AuthorBuyNow_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
            AuthorBuyNow.Links(AuthorBuyNow.Links.IndexOf(e.Link)).Visited = True
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
        End Sub
        Private Function GetPageHTML( _
           ByVal URL As String) As String
            ' Retrieves the HTML from the specified URL
            connect_status = True
            On Error GoTo error_msg

            Dim objWC As New System.Net.WebClient()
            Return New System.Text.UTF8Encoding().GetString( _
               objWC.DownloadData(URL))
error_msg:
            connect_status = False
        End Function
        Private Sub AuthorForm_Close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)

            e.Cancel = Not Valid
            'SendKeys.Send("%{F4}")
        End Sub

    End Class
End Namespace
