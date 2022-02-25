Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim MyProtection As New DynamicComponents.MyProtection
        Dim ProductId As String
        Dim CompanyId As String
        Dim CompanyInfo As String
        Dim ProductVersion As String
        Dim buynow_URL As String

        CompanyId = "EgyFirst Software"
        ProductId = "Appprotector"
        ProductVersion = "V. 3.5"

        CompanyInfo = "Application protector V. 3.5" + vbCrLf
        CompanyInfo += "Powered by EgyFirst Software, Inc." + vbCrLf
        CompanyInfo += "Free 30 Days Trial Version"

        buynow_URL = "http://www.mygoldensoft.com/ordernow.htm"

        MyProtection.SetProductKeys(CompanyId, ProductId, ProductVersion)
        MyProtection.SetAlgorithms(1971, 11, 35)
        MyProtection.SetInformation(buynow_URL, CompanyInfo)
        MyProtection.SetLicense(30) ' could be any number of days
        MyProtection.ShowAuthor() ' should be last line of your protection code

    End Sub
End Class
