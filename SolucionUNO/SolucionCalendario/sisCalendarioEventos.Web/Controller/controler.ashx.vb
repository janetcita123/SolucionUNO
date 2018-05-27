Imports sisCalendarioEventos.Business
Imports sisCalendarioEventos.Entity
Imports System.Web
Imports System.Web.Services
Imports System.IO
 

Imports System.Net


Public Class controler
    Implements System.Web.IHttpHandler
    Dim carpeta As String = ""
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "text/plain"
        context.Response.Write(carpeta)
        Dim respuesta As String

        Dim accion As String = Trim(context.Request.Form("accion")).ToUpper
        If accion = "I" Then
            respuesta = RegistrarPaquete(context)
        ElseIf accion = "D" Then


            respuesta = DownLoad2(context)

        ElseIf accion = "S" Then
            respuesta = "Operación incorrecta"
        ElseIf accion = "V" Then
            respuesta = "Operación incorrecta"
        Else
            respuesta = "Operación incorrecta"
        End If

        context.Response.Write(respuesta)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    'Private Function RegistrarPaquete(context As HttpContext) As String
    '    carpeta = ConfigurationManager.AppSettings("CalendarioCarpeta").ToString() 'Path.GetFullPath(path1) 
    '    Dim path As String = carpeta
    '    context.Response.ContentType = "text/plain"
    '    Dim files = context.Request.Files
    '    Dim respuesta As String = String.Empty
    '    Dim sObservacion As String = Trim(context.Request.Form("observacion")).Trim
    '    Dim archivoNombre As String = String.Empty
    '    Dim id As String = Trim(context.Request.Form("id")).Trim
    '    Dim vNombreOriginal As String
    '    Dim fileOK As Boolean
    '    If context.Request.Files.Count = 0 Then
    '        Dim archivoProcesado As HttpPostedFile = context.Request.Files(0)
    '        Dim fileExtension As String
    '        fileExtension = System.IO.Path. _
    '            GetExtension(archivoProcesado.FileName).ToLower()
    '        vNombreOriginal = archivoProcesado.FileName
    '        Dim allowedExtensions As String() = _
    '            {".pdf"}
    '        For i As Integer = 0 To allowedExtensions.Length - 1
    '            If fileExtension = allowedExtensions(i) Then
    '                fileOK = True
    '            End If

    '        Next
    '        If fileOK Then
    '            Try
    '                Dim vResult As String = ""


    '                Dim Cod_Tram As Integer

    '                Dim vNombreArchivo = Path & Cod_Tram & fileExtension
    '                Try
    '                    FileIO.FileSystem.DeleteFile(path & vNombreOriginal & ".pdf", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
    '                Catch

    '                End Try


    '                archivoProcesado.SaveAs(vNombreArchivo)


    '            Catch ex As Exception 
    '                respuesta = "Error al cargar el archivo." & vbCr & ex.Message
    '            End Try
    '        Else
    '            respuesta = "No se aceptan el tipo de archivo seleccionado"
    '        End If

    '    End If



    '    Return respuesta
    'End Function
    Private Function RegistrarPaquete(context As HttpContext) As String
        carpeta = ConfigurationManager.AppSettings("CalendarioCarpeta").ToString() 'Path.GetFullPath(path1) 
        context.Response.ContentType = "text/plain"
        Dim files = context.Request.Files
        Dim respuesta As String = String.Empty
        Dim sObservacion As String = Trim(context.Request.Form("observacion")).Trim
        Dim archivoNombre As String = String.Empty
        Dim id As String = Trim(context.Request.Form("id")).Trim

        If context.Request.Files.Count = 0 Then
            respuesta = "Por favor seleccione el archivo que contiene la trama de prestaciones"
        ElseIf context.Request.Files.Count = 1 Then
            Dim archivoProcesado As HttpPostedFile = context.Request.Files(0)
            Dim nombreArchivo = System.IO.Path.GetFileName(archivoProcesado.FileName).ToUpper()

            If archivoProcesado.FileName = "" Then
                respuesta = "NO seleccionaste ningun archivo"
            Else
                Dim extension As String = Path.GetExtension(archivoProcesado.FileName)
                Select Case extension.ToLower()
                    Case ".pdf"
                        Try
                            archivoNombre = Path.GetFileName(archivoProcesado.FileName)
                            Dim carpeta_final As String = Path.Combine(carpeta, archivoNombre)
                            archivoProcesado.SaveAs(carpeta_final)
                            respuesta = "ok"
                        Catch ex As Exception
                            respuesta = "Error: " & ex.Message
                        End Try
                    Case Else
                        respuesta = "Extensión no válida"
                End Select
            End If
        End If
        If respuesta = "ok" Then
            respuesta = finalizaEvento(id, archivoNombre, sObservacion.ToUpper())
        End If
        Return respuesta
    End Function

    Private Function finalizaEvento(id As String, archivoNombre As String, sObservacion As String) As String
        Dim resultado As String
        Dim objBusinessEvento As New BEvento
        resultado = objBusinessEvento.FinalizarEvento(id, archivoNombre, sObservacion)
        Return resultado
    End Function

    Private Function DownLoad(context As HttpContext) As String
        Dim Filpath As String = ConfigurationManager.AppSettings("CalendarioCarpeta").ToString()
        context.Response.ContentType = "text/plain"
        Dim sObservacion As String = Trim(context.Request.Form("observacion")).Trim
        Dim remoteUri As String = Filpath '+ "/" + sObservacion
        Dim fileName As String = sObservacion
        Dim myStringWebResource As String = Nothing
        ' Create a new WebClient instance.
        Dim myWebClient As New WebClient()
        ' Concatenate the domain with the Web resource filename. Because DownloadFile 
        'requires a fully qualified resource name, concatenate the domain with the Web resource file name.
        myStringWebResource = remoteUri + fileName
        DownloadFile(Filpath + fileName)
        myWebClient.DownloadFile(myStringWebResource, fileName)
 
        Return "ok"
    End Function


    Private Function DownLoad2(context As HttpContext) As String
        Dim Filpath As String = ConfigurationManager.AppSettings("CalendarioCarpeta").ToString()
        context.Response.ContentType = "text/plain"
        Dim sObservacion As String = Trim(context.Request.Form("observacion")).Trim
        Dim remoteUri As String = Filpath '+ "/" + sObservacion
        Dim fileNamec As String = sObservacion
        Dim myStringWebResource As String = Nothing

        Dim fileName As String = System.IO.Path.GetFileName(Filpath + fileNamec)
        Dim descFilePathAndName As String = System.IO.Path.Combine("D:\nueva version Calendario\xxxxxx", fileName)

        Try
            Dim myre As WebRequest = WebRequest.Create(Filpath + fileNamec)
        Catch
            Return False
        End Try

        Try
            Dim fileData As Byte()
            Using client As WebClient = New WebClient()
                fileData = client.DownloadData(Filpath + fileNamec)
            End Using

            Using fs As FileStream = New FileStream(descFilePathAndName, FileMode.OpenOrCreate)
                fs.Write(fileData, 0, fileData.Length)
            End Using

            Return True
        Catch ex As Exception
            Throw New Exception("download field", ex.InnerException)
        End Try

    End Function

    




    Public Function DownloadFile(ByVal ruta As String) As Boolean
        Dim fileName As String = System.IO.Path.GetFileName(ruta)
        Dim proceso As New System.Diagnostics.Process
        With proceso
            .StartInfo.FileName = (ruta)
            .Start()
        End With

        ' Dim descFilePathAndName As String = System.IO.Path.Combine(Me.DescFilePath, fileName)
        Try
            '    Dim myre As WebRequest = WebRequest.Create(ruta)
            'Catch
            '    Return False
            'End Try

            'Try
            '    Dim fileData As Byte()
            '    Using client As WebClient = New WebClient()
            '        fileData = client.DownloadData(Me.UrlString)
            '    End Using

            '    Using fs As FileStream = New FileStream(descFilePathAndName, FileMode.OpenOrCreate)
            '        fs.Write(fileData, 0, fileData.Length)
            '    End Using


            Return True
        Catch ex As Exception
            Throw New Exception("download field", ex.InnerException)
        End Try
    End Function
End Class

'Public Class FtpRemoteDownload
'    Inherits RemoteDownload

'    Public Sub New(ByVal urlString As String, ByVal descFilePath As String)
'    End Sub

'    Public Overrides Function DownloadFile() As Boolean
'        Dim reqFTP As FtpWebRequest
'        Dim fileName As String = System.IO.Path.GetFileName(Me.UrlString)
'        Dim descFilePathAndName As String = System.IO.Path.Combine(Me.DescFilePath, fileName)
'        Try
'            reqFTP = CType(FtpWebRequest.Create(Me.UrlString), FtpWebRequest)
'            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile
'            reqFTP.UseBinary = True
'            Using outputStream As FileStream = New FileStream(descFilePathAndName, FileMode.OpenOrCreate)
'                Using response As FtpWebResponse = CType(reqFTP.GetResponse(), FtpWebResponse)
'                    Using ftpStream As Stream = response.GetResponseStream()
'                        Dim bufferSize As Integer = 2048
'                        Dim readCount As Integer
'                        Dim buffer As Byte() = New Byte(bufferSize - 1) {}
'                        readCount = ftpStream.Read(buffer, 0, bufferSize)
'                        While readCount > 0
'                            outputStream.Write(buffer, 0, readCount)
'                            readCount = ftpStream.Read(buffer, 0, bufferSize)
'                        End While
'                    End Using
'                End Using
'            End Using

'            Return True
'        Catch ex As Exception
'            Throw New Exception("upload failed", ex.InnerException)
'        End Try
'    End Function
'End Class






