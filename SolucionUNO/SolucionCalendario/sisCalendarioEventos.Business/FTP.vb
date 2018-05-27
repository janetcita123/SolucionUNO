Imports System.Net
Imports System.IO

Public Class FTP

    Private m_servidor As String
    Private m_usuario As String
    Private m_clave As String

    Public Sub New(ByVal servidor As String, ByVal usuario As String, ByVal clave As String)
        Me.m_servidor = servidor
        Me.m_usuario = usuario
        Me.m_clave = clave
    End Sub

    Public Function CargarArchivoServidor(ByVal archivoOrigen As String) As Boolean
        Dim clsRequest As FtpWebRequest
        Dim clsStream As Stream
        Dim bFile() As Byte
        Dim boolRespuesta As Boolean = False
        Dim archivoNombre As String = System.IO.Path.GetFileName(archivoOrigen).ToUpper
        Dim archivoDestino As String = Me.m_servidor & "/" & archivoNombre
        Try
            clsRequest = DirectCast(WebRequest.Create(archivoDestino), FtpWebRequest)
            clsRequest.Credentials = New NetworkCredential(Me.m_usuario, Me.m_clave)
            clsRequest.Method = WebRequestMethods.Ftp.UploadFile
            clsStream = clsRequest.GetRequestStream()
            bFile = System.IO.File.ReadAllBytes(archivoOrigen)
            clsStream.Write(bFile, 0, bFile.Length)
            clsStream.Close()
            clsStream.Dispose()
            boolRespuesta = True
        Catch Ex As Exception
        Finally
            clsStream = Nothing
            clsRequest = Nothing
        End Try
        Return boolRespuesta
    End Function

End Class
