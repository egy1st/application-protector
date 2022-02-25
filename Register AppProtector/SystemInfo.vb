Imports System.Collections.Generic
Imports System.Text
Imports System.Management

Namespace DynamicComponents
    NotInheritable Class SystemInfo
        Private Sub New()
        End Sub
#Region "-> Private Variables"

        Public Shared UseProcessorID As Boolean = True
        Public Shared UseBaseBoardProduct As Boolean = True
        Public Shared UseBaseBoardManufacturer As Boolean
        Public Shared UseDiskDriveSignature As Boolean
        Public Shared UseVideoControllerCaption As Boolean
        Public Shared UsePhysicalMediaSerialNumber As Boolean
        Public Shared UseBiosVersion As Boolean
        Public Shared UseBiosManufacturer As Boolean
        Public Shared UseWindowsSerialNumber As Boolean

#End Region

        Public Shared Function GetSystemInfo() As String
            Dim SoftwareName As String = ""
            If UseProcessorID = True Then
                SoftwareName += RunQuery("Processor", "ProcessorId")
            End If

            If UseBaseBoardProduct = True Then
                SoftwareName += RunQuery("BaseBoard", "Product")
            End If

            'If UseBaseBoardManufacturer = True Then
            'SoftwareName += RunQuery("BaseBoard", "Manufacturer")
            'End If

            'If UseDiskDriveSignature = True Then
            'SoftwareName += RunQuery("DiskDrive", "Signature")
            'End If

            'If UseVideoControllerCaption = True Then
            'SoftwareName += RunQuery("VideoController", "Caption")
            'End If

            'If UsePhysicalMediaSerialNumber = True Then
            'SoftwareName += RunQuery("PhysicalMedia", "SerialNumber")
            'End If

            'If UseBiosVersion = True Then
            'SoftwareName += RunQuery("BIOS", "Version")
            'End If

            'If UseWindowsSerialNumber = True Then
            'SoftwareName += RunQuery("OperatingSystem", "SerialNumber")
            'End If

            'SoftwareName = RemoveUseLess(SoftwareName)

            'If SoftwareName.Length < 25 Then
            'SoftwareName += SoftwareName
            'Return GetSystemInfo(SoftwareName)
            'End If

            'Return SoftwareName.Substring(0, 25).ToUpper()
            Return SoftwareName
        End Function

        Private Shared Function RemoveUseLess(ByVal st As String) As String
            Dim ch As Char
            For i As Integer = st.Length - 1 To 0 Step -1
                ch = Char.ToUpper(st(i))

                If (ch < "A"c OrElse ch > "Z"c) AndAlso (ch < "0"c OrElse ch > "9"c) Then
                    st = st.Remove(i, 1)
                End If
            Next
            Return st
        End Function

        Private Shared Function RunQuery(ByVal TableName As String, ByVal MethodName As String) As String
            Dim MOS As New ManagementObjectSearcher("Select * from Win32_" & TableName)
            For Each MO As ManagementObject In MOS.[Get]()
                Try
                    Return MO(MethodName).ToString()
                Catch e As Exception
                    System.Windows.Forms.MessageBox.Show(e.Message)
                End Try
            Next
            Return ""
        End Function


        Public Function GetHDSerial() As String
            On Error GoTo EndMe
            Dim disk As New ManagementObject("Win32_LogicalDisk.DeviceID=""C:""")
            Dim diskPropertyA As PropertyData = _
                disk.Properties("VolumeSerialNumber")
            Return diskPropertyA.Value.ToString()
EndMe:
            Return "Fail"
        End Function

        Public Function GetCPUId() As String
            On Error GoTo EndMe

            Dim cpuInfo As String = String.Empty
            Dim temp As String = String.Empty
            Dim mc As ManagementClass = New ManagementClass("Win32_Processor")
            Dim moc As ManagementObjectCollection = mc.GetInstances()

            For Each mo As ManagementObject In moc
                If cpuInfo = String.Empty Then
                    cpuInfo = _
                     mo.Properties("ProcessorId").Value.ToString()
                End If
            Next
            Return cpuInfo
EndMe:
            Return "Fail"
        End Function

    End Class


End Namespace
