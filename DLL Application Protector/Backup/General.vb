Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Cryptography
Imports Microsoft.Win32
Imports System.IO

Module General
    Public aEncrypt() As Integer = {7, 13, 27, 1, 18, 3, 29, 33, 5, 12, 17, 17, 18, 4, 4, 11, 9, 23, 19, 5, 2, 14, 13, 13, 13}
    Public Function ZeroPad(ByVal str_String As String, ByVal int_Count As Byte) As String
        If str_String <> "" Then
            Return (New String("0", int_Count - Len(Trim(str_String))) & Trim(str_String))
        End If
        Return ""
    End Function
    Public Function SpacePad(ByVal str_String As String, ByVal int_Count As Byte) As String
        If str_String <> "" Then
            Return (New String(" ", int_Count - Len(Trim(str_String))) & Trim(str_String))
        End If
        Return ""
    End Function
    Public Function CheckSum(ByVal strNum As String) As Boolean

        Dim intCheckSum, X, intDigit As Integer
        Dim blnDoubleFlag As Boolean = False

        For X = Len(strNum) To 1 Step -1
            intDigit = Asc(Mid(strNum, X, 1))
            If intDigit >= 48 And intDigit <= 57 Then ' is it a digit starting 0 and ending 9
                intDigit = intDigit - 48

                If blnDoubleFlag = True Then
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
        Next
        If intCheckSum = 0 Then Return True
        Return False
    End Function
    Public Function EncryptMe(ByVal OldStr As String) As String
        Dim NewStr As String = ""
        Dim Num As Integer
        Dim MyChar As Char

        OldStr = ZeroPad(OldStr, 5)
        For Num = 1 To OldStr.Length
            MyChar = Mid(OldStr, Num, 1)
            NewStr += Chr(Asc(MyChar) - 33)
        Next Num
        Return NewStr
    End Function
    Public Function DecryptMe(ByVal NewStr As String) As String
        If Trim(NewStr) = "" Or IsNothing(NewStr) Then Return ""

        Dim OldStr As String = ""
        Dim Num As Integer
        Dim MyChar As Char
        Dim nLoop As Integer = 0
        nLoop += 1
        For Num = 1 To NewStr.Length
            MyChar = Chr(Asc(Mid(NewStr, Num, 1)) + 33)
            OldStr += MyChar
        Next Num
        Return OldStr
    End Function
    Function getMD5Hash(ByVal strToHash As String) As String
        Dim md5Obj As New Security.Cryptography.MD5CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = md5Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult
    End Function
    Public Function Encrypt(ByVal toEncrypt As String, ByVal key As String, ByVal useHashing As Boolean) As String

        Dim keyArray() As Byte
        Dim toEncryptArray As Byte() = UTF8Encoding.UTF8.GetBytes(toEncrypt)

        If useHashing = True Then

            Dim hashmd5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
        Else
            keyArray = UTF8Encoding.UTF8.GetBytes(key)
        End If

        Dim tdes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
        tdes.Key = keyArray
        tdes.Mode = CipherMode.ECB
        tdes.Padding = PaddingMode.PKCS7

        Dim cTransform As ICryptoTransform = tdes.CreateEncryptor()
        Dim resultArray() As Byte = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length)

        Return Convert.ToBase64String(resultArray, 0, resultArray.Length)
    End Function
    Public Function IsValidEmail(ByVal email As String) As Boolean
        'regular expression pattern for valid email
        'addresses, allows for the following domains:
        'com,edu,info,gov,int,mil,net,org,biz,name,museum,coop,aero,pro,tv
        Dim pattern As String = "^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\." & _
        "(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$"
        'Regular expression object
        email = email.ToLower()

        Dim check As New System.Text.RegularExpressions.Regex(pattern, RegexOptions.IgnorePatternWhitespace)
        'boolean variable to return to calling method
        Dim valid As Boolean = False

        'make sure an email address was provided
        If String.IsNullOrEmpty(email) Then
            valid = False
        Else
            'use IsMatch to validate the address
            valid = check.IsMatch(email)
        End If
        'return the value to the calling method
        Return valid
    End Function
    Public Function EncryptTripleDES(ByVal sIn As String, ByVal sKey As String) As String

        Dim DES As New System.Security.Cryptography.TripleDESCryptoServiceProvider
        Dim hashMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider

        ' scramble the key
        sKey = ScrambleKey(sKey)
        ' Compute the MD5 hash.
        DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(sKey))
        ' Set the cipher mode.
        DES.Mode = System.Security.Cryptography.CipherMode.ECB
        ' Create the encryptor.
        Dim DESEncrypt As System.Security.Cryptography.ICryptoTransform = DES.CreateEncryptor()
        ' Get a byte array of the string.
        Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(sIn)
        ' Transform and return the string.
        Return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length))

    End Function
    Public Function DecryptTripleDES(ByVal sOut As String, ByVal sKey As String) As String
        Dim DES As New System.Security.Cryptography.TripleDESCryptoServiceProvider
        Dim hashMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider

        ' scramble the key
        sKey = ScrambleKey(sKey)
        ' Compute the MD5 hash.
        DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(sKey))
        ' Set the cipher mode.
        DES.Mode = System.Security.Cryptography.CipherMode.ECB
        ' Create the decryptor.
        Dim DESDecrypt As System.Security.Cryptography.ICryptoTransform = DES.CreateDecryptor()
        Dim Buffer As Byte() = Convert.FromBase64String(sOut)
        ' Transform and return the string.
        Return System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length))

    End Function

    Private Function ScrambleKey(ByVal v_strKey As String) As String

        Dim sbKey As New System.Text.StringBuilder
        Dim intPtr As Integer
        For intPtr = 1 To v_strKey.Length
            Dim intIn As Integer = v_strKey.Length - intPtr + 1
            sbKey.Append(Mid(v_strKey, intIn, 1))
        Next

        Dim strKey As String = sbKey.ToString

        Return sbKey.ToString

    End Function

    Public Function adjTrim(ByVal str As String) As String

        If IsNothing(str) Or IsDBNull(str) Then Return ""
        Return Trim(str)

    End Function
    Function regGetBuyURL(ByVal publisher As String, ByVal appName As String, ByVal appVer As String) _
         As String
        ' form the registry key path
        Dim keyPath As String
        keyPath = "\SOFTWARE\Digital River\SoftwarePassport\" & publisher & "\" & appName & "\" & appVer

        Dim buyURL As String

        ' open the registry key
        ' try to get from HKEY_LOCAL_MACHINE first
        buyURL = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE" & keyPath, "BuyURL", Nothing)

        ' if fail to get from HKEY_LOCAL_MACHINE branch, try HKEY_CURRENT_USER
        If buyURL Is Nothing Then
            buyURL = My.Computer.Registry.GetValue("HKEY_CURRENT_USER" & keyPath, "BuyURL", Nothing)
        End If

        regGetBuyURL = buyURL
    End Function

    Public Function GetDriveId() As String
        Dim myRegkey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\MountedDevices\")
        Dim myRegkey2 As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\MountedDevices\DosDevices\C:", True)

        Dim drive(4) As String
        drive(0) = "\DosDevices\C:"
        drive(1) = "\DosDevices\Z:"
        drive(2) = "\DosDevices\E:"
        drive(3) = "\DosDevices\F:"
        Dim data As Byte()
        Dim hexdata(4) As String
        Dim x As Byte
        Dim y As Byte


        For x = 0 To 0


            Try

                If myRegkey.GetValueKind(drive(x)) = RegistryValueKind.Binary Then

                    data = DirectCast(myRegkey.GetValue(drive(x)), Byte())
                    For y = 0 To 11
                        hexdata(x) += ZeroPad(Hex(data(y)), 2)
                    Next y
                End If
            Catch
                hexdata(x) = "000000000000000000000000"

            End Try
        Next x

        Dim s0 As String = Environment.MachineName
        If s0 Is Nothing Then
            s0 = ""
        End If

        Dim s1 As String = Environment.UserName
        If s1 Is Nothing Then
            s1 = ""
        End If

        Return hexdata(0) + s0 + s1

    End Function

    Public Function getCountries() As ArrayList
        Dim country As New ArrayList
        country.Add("Afghanistan")
        country.Add("Albania")
        country.Add("Algeria")
        country.Add("American Samoa")
        country.Add("Andorra")
        country.Add("Angola")
        country.Add("Anguilla")
        country.Add("Antigua and Barbuda")
        country.Add("Argentina")
        country.Add("Armenia")
        country.Add("Aruba")
        country.Add("Australia")
        country.Add("Austria")
        country.Add("Azerbaijan")
        country.Add("Bahamas")
        country.Add("Bahrain")
        country.Add("Bangladesh")
        country.Add("Barbados")
        country.Add("Belarus")
        country.Add("Belgium")
        country.Add("Belize")
        country.Add("Benin")
        country.Add("Bermuda")
        country.Add("Bhutan")
        country.Add("Bolivia")
        country.Add("Bosnia and Herzegovina")
        country.Add("Botswana")
        country.Add("Brazil")
        country.Add("British Virgin Islands")
        country.Add("Brunei")
        country.Add("Bulgaria")
        country.Add("Burkina Faso")
        country.Add("Burma")
        country.Add("Burundi")
        country.Add("Cambodia")
        country.Add("Cameroon")
        country.Add("Canada")
        country.Add("Cape Verde")
        country.Add("Cayman Islands")
        country.Add("Central African Republic")
        country.Add("Chad")
        country.Add("Chile")
        country.Add("China")
        country.Add("Colombia")
        country.Add("Comoros")
        country.Add("Cook Islands")
        country.Add("Costa Rica")
        country.Add("Cote d'Ivoire")
        country.Add("Croatia")
        country.Add("Cuba")
        country.Add("Cyprus")
        country.Add("Czech Republic")
        country.Add("Democratic Republic of the Congo")
        country.Add("Denmark")
        country.Add("Djibouti")
        country.Add("Dominica")
        country.Add("Dominican Republic")
        country.Add("Ecuador")
        country.Add("Egypt")
        country.Add("El Salvador")
        country.Add("Equatorial Guinea")
        country.Add("Eritrea")
        country.Add("Estonia")
        country.Add("Ethiopia")
        country.Add("Faroe Islands")
        country.Add("Federated States of Micronesia")
        country.Add("Fiji")
        country.Add("Finland")
        country.Add("France")
        country.Add("French Polynesia")
        country.Add("Gabon")
        country.Add("Gambia")
        country.Add("Gaza Strip")
        country.Add("Georgia")
        country.Add("Germany")
        country.Add("Ghana")
        country.Add("Gibraltar")
        country.Add("Greece")
        country.Add("Greenland")
        country.Add("Grenada")
        country.Add("Guatemala")
        country.Add("Guernsey")
        country.Add("Guinea")
        country.Add("Guinea-Bissau")
        country.Add("Guyana")
        country.Add("Haiti")
        country.Add("Honduras")
        country.Add("Hong Kong")
        country.Add("Hungary")
        country.Add("Iceland")
        country.Add("India")
        country.Add("Indonesia")
        country.Add("Iran")
        country.Add("Iraq")
        country.Add("Ireland")
        country.Add("Isle of Man")
        country.Add("Israel")
        country.Add("Italy")
        country.Add("Jamaica")
        country.Add("Japan")
        country.Add("Jersey")
        country.Add("Jordan")
        country.Add("Kazakhstan")
        country.Add("Kenya")
        country.Add("Kiribati")
        country.Add("Kuwait")
        country.Add("Kyrgyzstan")
        country.Add("Laos")
        country.Add("Latvia")
        country.Add("Lebanon")
        country.Add("Lesotho")
        country.Add("Liberia")
        country.Add("Libya")
        country.Add("Liechtenstein")
        country.Add("Lithuania")
        country.Add("Luxembourg")
        country.Add("Macau")
        country.Add("Macedonia")
        country.Add("Madagascar")
        country.Add("Malawi")
        country.Add("Malaysia")
        country.Add("Maldives")
        country.Add("Mali")
        country.Add("Malta")
        country.Add("Marshall Islands")
        country.Add("Mauritania")
        country.Add("Mauritius")
        country.Add("Mexico")
        country.Add("Moldova")
        country.Add("Monaco")
        country.Add("Mongolia")
        country.Add("Montenegro")
        country.Add("Montserrat")
        country.Add("Morocco")
        country.Add("Mozambique")
        country.Add("Namibia")
        country.Add("Nauru")
        country.Add("Nepal")
        country.Add("Netherlands")
        country.Add("New Caledonia")
        country.Add("New Zealand")
        country.Add("Nicaragua")
        country.Add("Niger")
        country.Add("Nigeria")
        country.Add("North Korea")
        country.Add("Northern Mariana Islands")
        country.Add("Norway")
        country.Add("Oman")
        country.Add("Pakistan")
        country.Add("Palau")
        country.Add("Panama")
        country.Add("Papua New Guinea")
        country.Add("Paraguay")
        country.Add("Peru")
        country.Add("Philippines")
        country.Add("Poland")
        country.Add("Portugal")
        country.Add("Puerto Rico")
        country.Add("Qatar")
        country.Add("Republic of the Congo")
        country.Add("Romania")
        country.Add("Russia")
        country.Add("Rwanda")
        country.Add("Saint Helena, Ascension and Tristan da Cunha")
        country.Add("Saint Kitts and Nevis")
        country.Add("Saint Lucia")
        country.Add("Saint Pierre and Miquelon")
        country.Add("Saint Vincent and the Grenadines")
        country.Add("Samoa")
        country.Add("San Marino")
        country.Add("Sao Tome and Principe")
        country.Add("Saudi Arabia")
        country.Add("Senegal")
        country.Add("Serbia")
        country.Add("Seychelles")
        country.Add("Sierra Leone")
        country.Add("Singapore")
        country.Add("Slovakia")
        country.Add("Slovenia")
        country.Add("Solomon Islands")
        country.Add("Somalia")
        country.Add("South Africa")
        country.Add("South Korea")
        country.Add("Spain")
        country.Add("Sri Lanka")
        country.Add("Sudan")
        country.Add("Suriname")
        country.Add("Swaziland")
        country.Add("Sweden")
        country.Add("Switzerland")
        country.Add("Syria")
        country.Add("Taiwan")
        country.Add("Tajikistan")
        country.Add("Tanzania")
        country.Add("Thailand")
        country.Add("Timor-Leste")
        country.Add("Togo")
        country.Add("Tonga")
        country.Add("Trinidad and Tobago")
        country.Add("Tunisia")
        country.Add("Turkey")
        country.Add("Turkmenistan")
        country.Add("Turks and Caicos Islands")
        country.Add("Tuvalu")
        country.Add("U.S. Virgin Islands")
        country.Add("Uganda")
        country.Add("Ukraine")
        country.Add("United Arab Emirates")
        country.Add("United Kingdom")
        country.Add("United States")
        country.Add("Uruguay")
        country.Add("Uzbekistan")
        country.Add("Vanuatu")
        country.Add("Venezuela")
        country.Add("Vietnam")
        country.Add("Wallis and Futuna")
        country.Add("West Bank")
        country.Add("Western Sahara")
        country.Add("Yemen")
        country.Add("Zambia")
        country.Add("Zimbabwe")
        Return country
    End Function
    Private Declare Function GetVolumeInformation Lib "kernel32" _
       Alias "GetVolumeInformationA" _
      (ByVal lpRootPathName As String, _
       ByVal lpVolumeNameBuffer As String, _
       ByVal nVolumeNameSize As Long, _
    ByVal lpVolumeSerialNumber As Long, _
    ByVal lpMaximumComponentLength As Long, _
    ByVal lpFileSystemFlags As Long, _
       ByVal lpFileSystemNameBuffer As String, _
       ByVal nFileSystemNameSize As Long) As Long

    Private Sub rgbGetVolume(ByVal PathName As String, _
    ByVal DrvVolumeName As String, _
    ByVal DrvSerialNo As String)

        'create working variables
        'to keep it simple, use dummy variables for info
        'we're not interested in right now
        Dim r As Long
        Dim pos As Integer
        Dim hword As Long
        Dim HiHexStr As String
        Dim lword As Long
        Dim LoHexStr As String
        Dim VolumeSN As Long

        Dim UnusedStr As String
        Dim UnusedVal1 As Long
        Dim UnusedVal2 As Long

        'pad the strings
        DrvVolumeName = Space$(14)
        UnusedStr = Space$(32)

        'do what it says
        r = GetVolumeInformation(PathName, _
                                 DrvVolumeName, _
                                 Len(DrvVolumeName), _
                                 VolumeSN&, _
                                 UnusedVal1, UnusedVal2, _
                                 UnusedStr, Len(UnusedStr))


        'error check
        If r = 0 Then Exit Sub

        'determine the volume label
        pos = InStr(DrvVolumeName, Chr(0))
        If pos Then DrvVolumeName = Left$(DrvVolumeName, pos - 1)
        If Len(Trim$(DrvVolumeName)) = 0 Then DrvVolumeName = "(no label)"

        'determine the drive volume id
        hword = HiWord(VolumeSN)
        lword = LoWord(VolumeSN)
        HiHexStr = Format$(Hex(hword), "0000")
        LoHexStr = Format$(Hex(lword), "0000")

        DrvSerialNo = HiHexStr & "-" & LoHexStr

    End Sub
    Private Function HiWord(ByVal dw As Long) As Integer

        HiWord = (dw And &HFFFF0000) \ &H10000

    End Function


    Private Function LoWord(ByVal dw As Long) As Integer

        If dw And &H8000& Then
            LoWord = dw Or &HFFFF0000
        Else
            LoWord = dw And &HFFFF&
        End If

    End Function

End Module