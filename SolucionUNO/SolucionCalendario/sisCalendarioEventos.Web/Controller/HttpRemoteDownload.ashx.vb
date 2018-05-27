Imports System.Web
Imports System.Web.Services
Imports System.Net
Imports System.IO

Public Class HttpRemoteDownload


    Public Sub New(ByVal urlString As String, ByVal descFilePath As String)
    End Sub

    Public Function DownloadFile() As Boolean
        Dim fileName As String = System.IO.Path.GetFileName("url")
        Dim descFilePathAndName As String = System.IO.Path.Combine("rutafin", fileName)
        Try
            Dim myre As WebRequest = WebRequest.Create("url")
        Catch
            Return False
        End Try

        Try
            Dim fileData As Byte()
            Using client As WebClient = New WebClient()
                fileData = client.DownloadData("url")
            End Using

            Using fs As FileStream = New FileStream(descFilePathAndName, FileMode.OpenOrCreate)
                fs.Write(fileData, 0, fileData.Length)
            End Using

            Return True
        Catch ex As Exception
            Throw New Exception("download field", ex.InnerException)
        End Try
    End Function

End Class