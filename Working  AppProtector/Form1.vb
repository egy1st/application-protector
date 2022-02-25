Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim MyProtection As New DynamicComponents.AppProtector()
        Dim ProductId As String
        Dim CompanyId As String
        Dim CompanyInfo As String
        Dim ProductVersion As String
        Dim buynow_URL As String

        CompanyId = "My Company"
        ProductId = "My Product"
        ProductVersion = "V. 1.0"

        CompanyInfo = "My Product V. 1.0" + vbCrLf 'vbcrlf forces new line
        CompanyInfo += "Powered by My Company" + vbCrLf
        CompanyInfo += "Free 30 Days Trial Version"

        buynow_URL = "http://www.mygoldensoft.com/ordernow.htm"

        MyProtection.SetProductKeys(CompanyId, ProductId, ProductVersion)
        MyProtection.SetAlgorithms(5555, 22, 99)
        MyProtection.SetInformation(buynow_URL, CompanyInfo)
        MyProtection.SetLicense(30) ' could be any number of days
        MyProtection.setRegistrationKey("0000-000-000-000-0000") ' replace with your purchased serial number once you get one

        MyProtection.ShowAuthor() ' should be last line of your protection code


    End Sub


End Class
