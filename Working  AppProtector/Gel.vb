Imports System.Management
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Cryptography
Imports Microsoft.Win32


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
    Public Function getSerial() As String
        Dim myRegkey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\MountedDevices\")
        Dim drive_C As String = "\DosDevices\V:"
        'Dim drive_D As String = myRegkey + "D:"
        'Dim drive_E As String = myRegkey + "E:"

        'Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", True)

        If myRegkey.GetValueKind(drive_C) = RegistryValueKind.Binary Then

            Dim data As Byte() = DirectCast(myRegkey.GetValue(drive_C), Byte())
        End If


        Dim regValue As RegistryKey = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\MountedDevices\", "maa", Nothing)
        regValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\MountedDevices\", "DosDevices\C:\", Nothing)
        regValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\MountedDevices\DosDevices\", "C:", Nothing)
        regValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\MountedDevices\DosDevices\", "C:\", Nothing)


        Return ""

    End Function
    Public Function GetMotherboardSerialNumber() As String
        Dim searcher As New System.Management.ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard")
        For Each obj As System.Management.ManagementObject In searcher.Get
            Return obj.Properties("SerialNumber").Value.ToString
        Next
        Return String.Empty
    End Function
    Public Function IsValidEmail(ByVal email As String) As Boolean
        'regular expression pattern for valid email
        'addresses, allows for the following domains:
        'com,edu,info,gov,int,mil,net,org,biz,name,museum,coop,aero,pro,tv
        Dim pattern As String = "^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\." & _
        "(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$"
        'Regular expression object
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
    Public Function GetProcessorId() As String
        Dim objMOS As ManagementObjectSearcher
        Dim objMOC As Management.ManagementObjectCollection
        Dim objMO As Management.ManagementObject
        objMOS = New ManagementObjectSearcher("Select * From Win32_Processor")
        objMOC = objMOS.Get
        For Each objMO In objMOC
            MessageBox.Show("CPU ID = " & objMO("ProcessorID"))
        Next
		
		objMOS.Dispose()
        objMOS = Nothing
        objMO.Dispose()
        objMO = Nothing
		Return strProcessorId

		Private Sub GetProcessorID()
        Dim objs, obj As Object
        objs =GetObject("winmgmts:").InstancesOf("Win32_Processor")
        For Each obj In objs
          MsgBox(obj.ProcessorId)
         Next
        End Sub
		
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
    Function getOS() As String

        Dim computer As New Microsoft.VisualBasic.Devices.Computer
        Dim operatingSystem As String = computer.Info.OSFullName
        Return operatingSystem

    End Function
    Function getVS() As String

        Return System.Environment.Version.ToString
        
    End Function

    Function regGetBuyURL(ByVal publisher As String, ByVal appName As String, ByVal appVer As String) As String
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
End Module