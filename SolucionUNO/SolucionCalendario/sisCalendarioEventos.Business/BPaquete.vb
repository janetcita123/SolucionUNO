Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports sisCalendarioEventos.Data
Imports sisCalendarioEventos.Entity
Imports System.Linq
Imports System.Web

Public Class BPaquete

    Private m_connectionString As String
    Private m_FTP As FTP
    Private extensionesPermitidas As String() = {".ZIP"}
    Private oDataPaqueteDAO As DataPaquete

    Public Sub New()
    End Sub

    Public Sub New(ByVal strStringConnection As String)
        Me.m_connectionString = strStringConnection
        oDataPaqueteDAO = New DataPaquete(Me.m_connectionString)
    End Sub

    Public Sub New(ByVal strStringConnection As String, ByVal servidorFTP As String, ByVal usuarioFTP As String, ByVal claveFTP As String)
        Me.m_connectionString = strStringConnection
        Me.m_FTP = New FTP(servidorFTP, usuarioFTP, claveFTP)
        oDataPaqueteDAO = New DataPaquete(Me.m_connectionString)
    End Sub

    Public Sub configurarServidorFTP(ByVal servidorFTP As String, ByVal usuarioFTP As String, ByVal claveFTP As String)
        Me.m_FTP = New FTP(servidorFTP, usuarioFTP, claveFTP)
    End Sub

    Public Function EliminarPaquete(idPaquete As Integer, usuario As String, motivoEliminacion As String) As String
        Return oDataPaqueteDAO.EliminarPaquete(idPaquete, usuario, motivoEliminacion)
    End Function

    Public Sub GuardarPaquete(ByRef oPaquete As Paquete)
        oDataPaqueteDAO.GuardarPaquete(oPaquete)
    End Sub

    Public Function ListarDetalle(idPaquete As Integer) As List(Of DetallePaquete)
        Return oDataPaqueteDAO.ListarDetalle(idPaquete)
    End Function

    Public Function ValidarPaquetePermitido(sessionUsuario As String, paqueteDescripcion As String, periodoId As String, crongoramaId As String) As Paquete
        ' Return oDataPaqueteDAO.ValidarPaquetePermitido(sessionUsuario, paqueteDescripcion, periodoId, crongoramaId)
    End Function

    Public Function eliminarArchivo(ByVal sNombreArchivo As String, ByVal sRutaDestino As String) As Boolean
        Dim boolRespuesta As Boolean = False
        Dim archivo As New FileInfo(sRutaDestino & "\" & sNombreArchivo)
        Try
            archivo.Delete()
            boolRespuesta = True
        Catch ex As Exception
        End Try
        Return boolRespuesta
    End Function

    Public Function eliminarDirectorio(ByVal sRutaDestino As String) As Boolean
        Dim boolRespuesta As Boolean = False
        Dim directorio As New DirectoryInfo(sRutaDestino)
        Try
            If directorio.Exists Then
                For Each archivo As FileInfo In directorio.GetFiles
                    archivo.Delete()
                Next
                directorio.Delete()
            End If
            boolRespuesta = True
        Catch ex As Exception
        End Try
        Return boolRespuesta
    End Function

    'Public Function alojarPaquete(archivoProcesado As HttpPostedFile, directorio As String, archivoNombre As String) As String
    '    Dim archivoExtension As String = System.IO.Path.GetExtension(archivoNombre).ToUpper
    '    Dim archivoNombreCompleto As String = String.Empty
    '    Dim intIndex As Int16 = 0
    '    Dim archivoTamanioCorrecto As Boolean = False
    '    Dim archivoCumpleExtension As Boolean = False
    '    Dim archivoPermitido As Boolean = False
    '    Dim megasPermitido As UInt32 = 10 'INDICAR LA CANTIDAD DE MEGAS PERMITIDOS
    '    Dim strRespuesta As String = String.Empty

    '    If Not archivoNombre.EndsWith(".ZIP") Then
    '        strRespuesta = "Por favor el archivo debe ser un archivo comprimido ZIP"
    '    Else
    '        For i As Integer = 0 To extensionesPermitidas.Length - 1
    '            If archivoExtension = extensionesPermitidas(i) Then
    '                archivoCumpleExtension = True
    '                If archivoProcesado.ContentLength > (megasPermitido * 1048576) Then
    '                    archivoTamanioCorrecto = False
    '                    archivoPermitido = False
    '                Else
    '                    archivoTamanioCorrecto = True
    '                    archivoPermitido = True
    '                End If
    '            Else
    '                archivoCumpleExtension = False
    '                archivoPermitido = False
    '            End If
    '        Next
    '        If archivoPermitido Then
    '            Try
    '                archivoNombreCompleto = directorio & archivoNombre
    '                archivoProcesado.SaveAs(archivoNombreCompleto)

    '                'Dim carpetaCompartida As String = "T://"
    '                'Dim carpetaCompartida As String = "\\10.10.10.61\t\" & archivoNombre
    '                'Dim usuario As String = "10.10.10.61\oracle"

    '                'Dim MyFileStream As FileStream = Nothing
    '                'MyFileStream = New FileStream("T:\\TXT.TXT", FileMode.OpenOrCreate, FileAccess.Write)
    '                'MyFileStream.WriteByte(1)
    '                'MyFileStream.Close()
    '                'My.Computer.Network.UploadFile(archivoNombreCompleto, carpetaCompartida, usuario, "Q7nWBuCGJ2Se")
    '                'My.Computer.Network.UploadFile(archivoNombreCompleto, carpetaCompartida)

    '                'Dim ServidorFTP As String = ConfigurationManager.AppSettings("ServidorFTPTemporal").ToString()
    '                'Dim UsuarioFTP As String = ConfigurationManager.AppSettings("UsuarioFTP").ToString()
    '                'Dim ClaveFTP As String = ConfigurationManager.AppSettings("ClaveFTP").ToString()
    '                'Dim archivoDestino As String = ServidorFTP & "/" & archivoNombre
    '                'My.Computer.Network.UploadFile(archivoNombreCompleto, archivoDestino, UsuarioFTP, ClaveFTP)
    '                strRespuesta = "1"
    '            Catch ex As Exception
    '                'respuesta = ex.Message
    '                strRespuesta = "Ha ocurrido un error al subir el archivo. Por favor, comuníquese con el administrador."
    '            End Try
    '        Else
    '            If archivoCumpleExtension = False Then
    '                strRespuesta = "Por favor el archivo debe ser un archivo comprimido en formato ZIP."
    '            Else
    '                If archivoTamanioCorrecto = False Then
    '                    strRespuesta = "El archivo no debe pesar más de " & megasPermitido & " MB."
    '                End If
    '            End If
    '        End If
    '    End If
    '    Return strRespuesta
    'End Function

    Public Function isDesempaquetar(ByVal sNombreArchivo As String, ByVal sRutaDestino As String, oPaquete As Paquete, almenosUnaClave As Boolean) As String
        Dim archivoZIP As ZipInputStream = Nothing
        Dim archivoTramaEnZIP As ZipEntry = Nothing
        Dim arrClaves As String() = Nothing
        Dim listResultadoTmp As List(Of String)
        Dim strRespuesta As String = String.Empty
        Dim listArchivos As New List(Of String)
        Try
            archivoZIP = New ZipInputStream(New FileStream(sRutaDestino & "\" & sNombreArchivo, FileMode.Open, FileAccess.Read))
            If oPaquete IsNot Nothing Then
                arrClaves = New String() {oPaquete.clave}
            End If
            If arrClaves IsNot Nothing AndAlso arrClaves.Length > 0 Then
                For i As Integer = 0 To arrClaves.Length - 1
                    If archivoTramaEnZIP Is Nothing Then archivoTramaEnZIP = archivoZIP.GetNextEntry
                    If archivoTramaEnZIP.IsCrypted Then archivoZIP.Password = arrClaves(i)
                    If archivoZIP.CanDecompressEntry Then
                        Try
                            'TRATAR DE LEER ALGUNA TRAMA
                            Dim buffer(1) As Byte
                            Dim count As Integer = archivoZIP.Read(buffer, 0, 1)
                            While archivoTramaEnZIP IsNot Nothing
                                If almenosUnaClave Then
                                    If Not archivoTramaEnZIP.IsCrypted Then
                                        Throw New BPaqueteException("El paquete consigna tramas que no se encuentran encriptadas.")
                                    End If
                                End If
                                'VERIFICAR QUE SOLO SEAN ARCHIVOS
                                If Not archivoTramaEnZIP.IsFile Then
                                    If archivoTramaEnZIP.IsDirectory Then
                                        Throw New BPaqueteException("No está permitido que los paquetes contengan carpetas.")
                                    Else
                                        Throw New BPaqueteException("Sólo está permitido que los paquetes contengan archivos.")
                                    End If
                                    Exit While
                                End If
                                listArchivos.Add(archivoTramaEnZIP.Name.ToUpper)
                                archivoTramaEnZIP = archivoZIP.GetNextEntry
                            End While

                            'VERIFICAR NOMBRES DE TRAMAS DUPLICADAS
                            listResultadoTmp = (From item As String In listArchivos Group By item Into Group Where Group.Count() > 1 Select item).ToList
                            If listResultadoTmp.Count > 0 Then
                                Throw New BPaqueteException("El paquete consigna " & listResultadoTmp.Count & " trama(s) con nombres similares.")
                            End If
                            strRespuesta = "1"
                            Exit For
                        Catch ex As ZipException
                        End Try
                    End If
                Next
            End If
            If strRespuesta = "1" Then
                If almenosUnaClave AndAlso String.IsNullOrEmpty(archivoZIP.Password) Then
                    strRespuesta = "El paquete no cuenta con la clave correspondiente."
                End If
            Else
                If almenosUnaClave Then
                    If String.IsNullOrEmpty(archivoZIP.Password) Then
                        strRespuesta = "El paquete no cuenta con la clave correspondiente."
                    Else
                        strRespuesta = "El paquete no cuenta con una clave válida."
                    End If
                Else
                    strRespuesta = "El paquete no puede descomprimirse."
                End If
            End If
        Catch ex As ZipException
            strRespuesta = "Ocurrió un inconveniente descifrando el paquete."
        Catch ex As BPaqueteException
            strRespuesta = ex.Message
        Catch ex As Exception
            strRespuesta = "Ocurrió un error descifrando el paquete."
        Finally
            archivoTramaEnZIP = Nothing
            If archivoZIP IsNot Nothing Then
                archivoZIP.Close()
                archivoZIP = Nothing
            End If
        End Try
        Return strRespuesta
    End Function

    Public Function descomprimirPaqueteZip(ByVal sNombreArchivo As String, ByVal sRutaDestino As String, oPaquete As Paquete) As String
        Dim archivoZIP As ZipInputStream = Nothing
        Dim archivoTramaEnDisco As FileStream = Nothing
        Dim archivoTramaEnZIP As ZipEntry = Nothing
        Dim archivosAlmacen As String = String.Empty
        Dim listResultadoTmp As List(Of String)
        Dim strRespuesta As String = String.Empty

        Try
            Dim identificadorPaquete As String = Path.GetFileNameWithoutExtension(sNombreArchivo)
            archivosAlmacen = sRutaDestino & identificadorPaquete
            Directory.CreateDirectory(archivosAlmacen)

            archivoZIP = New ZipInputStream(New FileStream(sRutaDestino & "\" & sNombreArchivo, FileMode.Open, FileAccess.Read))
            archivoZIP.Password = oPaquete.clave
            archivoTramaEnZIP = archivoZIP.GetNextEntry
            While archivoTramaEnZIP IsNot Nothing
                If archivoTramaEnZIP.IsFile Then
                    Try
                        'LEER EL ARCHIVO EMPAQUETADO
                        Dim buffer(4096) As Byte
                        Dim count As Long = archivoZIP.Read(buffer, 0, 4096)
                        Dim archivoAlmacenado As String = archivoTramaEnZIP.Name.ToUpper

                        'VERIFICAR SI EL ARCHIVO A DESEMPAQUETAR ES UN ARCHIVO PERMITIDO
                        listResultadoTmp = (From item As String In oPaquete.archivosPermitidos Where item = archivoAlmacenado Select "1").ToList
                        If listResultadoTmp.Count = 0 Then Throw New BPaqueteException("El archivo " & archivoAlmacenado & " no está permitido en el paquete")

                        'DESEMPAQUETAR EL ARCHIVO
                        archivoTramaEnDisco = New FileStream(archivosAlmacen & "\" & identificadorPaquete & "_" & archivoAlmacenado, FileMode.OpenOrCreate, FileAccess.Write)
                        While count > 0
                            archivoTramaEnDisco.Write(buffer, 0, count)
                            count = archivoZIP.Read(buffer, 0, 4096)
                        End While
                        archivoTramaEnDisco.Close()

                        'OBTENER EL SIGUIENTE ARCHIVO EMPAQUETADO
                        archivoTramaEnZIP = archivoZIP.GetNextEntry
                        strRespuesta = "1"
                    Catch ex As ZipException
                        Throw New BPaqueteException("Ocurrió un error desempaquetando las tramas.")
                    Catch ex As BPaqueteException
                        Throw New BPaqueteException(ex.Message)
                    Catch ex As Exception
                        Throw New BPaqueteException("Ocurrió un error almacenando las tramas del paquete.")
                    End Try
                Else
                    Throw New BPaqueteException("Sólo está permitido recepcionar archivos en los paquetes.")
                End If
            End While
        Catch ex As ZipException
            strRespuesta = "Ocurrió un error leyendo el paquete."
        Catch ex As BPaqueteException
            strRespuesta = ex.Message
        Catch ex As Exception
            strRespuesta = "Ocurrió un error almaecnando las tramas del paquete."
        Finally
            archivoTramaEnZIP = Nothing
            If archivoTramaEnDisco IsNot Nothing Then
                archivoTramaEnDisco.Close()
                archivoTramaEnDisco = Nothing
            End If
            If archivoZIP IsNot Nothing Then
                archivoZIP.Close()
                archivoZIP = Nothing
            End If
        End Try

        If strRespuesta.Length = 0 Then
            strRespuesta = "No se puede procesar el paquete."
        End If

        Return strRespuesta
    End Function

    Public Function cargarArchivosFTP(nombreArchivoZIP As String, directorioPaquete As String) As String
        Dim strRespuesta As String = String.Empty
        Dim identificadorPaquete As String = Path.GetFileNameWithoutExtension(nombreArchivoZIP)
        Dim directorioAlmacen As String = directorioPaquete & "\" & identificadorPaquete
        If Directory.Exists(directorioAlmacen) Then
            If Me.m_FTP IsNot Nothing Then
                Dim directorioOrigen As New DirectoryInfo(directorioAlmacen)
                Dim archivosEnDirectorio() As FileInfo = directorioOrigen.GetFiles()
                For i As Integer = 0 To archivosEnDirectorio.Length - 1
                    If archivosEnDirectorio(i).Name.EndsWith(".TXT") Then
                        If Me.m_FTP.CargarArchivoServidor(archivosEnDirectorio(i).FullName) Then
                            strRespuesta = "1"
                        Else
                            strRespuesta = "No se ha podido subir la trama " & archivosEnDirectorio(i).Name & " al servidor de procesamiento."
                            Exit For
                        End If
                    End If
                Next
                Me.m_FTP = Nothing
            Else
                strRespuesta = "No se ha configurado el servidor de procesamiento."
            End If
        Else
            strRespuesta = "No se ha consignado un directorio correcto para el paquete."
        End If
        Return strRespuesta
    End Function

   

    'Public Function ListarClavesAndArchivosPermitidos() As PaquetePermitidoLista
    '    Return New DataPaquete(m_connectionString).ListarClavesAndArchivosPermitidos()
    'End Function

    'Public Function isDesempaquetar2(ByVal sNombreArchivo As String, ByVal sRutaDestino As String, listaPermisibles As PaquetePermitidoLista, almenosUnaClave As Boolean, ByRef claveElegida As String) As String
    '    Dim archivoZIP As ZipInputStream = Nothing
    '    Dim archivoTramaEnZIP As ZipEntry = Nothing
    '    Dim arrClaves As String() = Nothing
    '    Dim listResultadoTmp As List(Of String)
    '    Dim strRespuesta As String = String.Empty
    '    Dim listArchivos As New List(Of String)
    '    Try
    '        archivoZIP = New ZipInputStream(New FileStream(sRutaDestino & "\" & sNombreArchivo, FileMode.Open, FileAccess.Read))
    '        If listaPermisibles IsNot Nothing Then
    '            listResultadoTmp = (From item As PaquetePermitido In listaPermisibles.archivosPermitidos Select item.clave Distinct).ToList
    '            arrClaves = listResultadoTmp.ToArray
    '        End If
    '        If arrClaves IsNot Nothing AndAlso arrClaves.Length > 0 Then
    '            For i As Integer = 0 To arrClaves.Length - 1
    '                If archivoTramaEnZIP Is Nothing Then archivoTramaEnZIP = archivoZIP.GetNextEntry
    '                If archivoTramaEnZIP.IsCrypted Then archivoZIP.Password = arrClaves(i)
    '                If archivoZIP.CanDecompressEntry Then
    '                    Try
    '                        'TRATAR DE LEER ALGUNA TRAMA
    '                        Dim buffer(1) As Byte
    '                        Dim count As Integer = archivoZIP.Read(buffer, 0, 1)
    '                        While archivoTramaEnZIP IsNot Nothing
    '                            If almenosUnaClave Then
    '                                If Not archivoTramaEnZIP.IsCrypted Then
    '                                    Throw New BPaqueteException("El paquete consigna tramas que no se encuentran encriptadas.")
    '                                End If
    '                            End If
    '                            'VERIFICAR QUE SOLO SEAN ARCHIVOS
    '                            If Not archivoTramaEnZIP.IsFile Then
    '                                If archivoTramaEnZIP.IsDirectory Then
    '                                    Throw New BPaqueteException("No está permitido que los paquetes contengan carpetas.")
    '                                Else
    '                                    Throw New BPaqueteException("Sólo está permitido que los paquetes contengan archivos.")
    '                                End If
    '                                Exit While
    '                            End If
    '                            listArchivos.Add(archivoTramaEnZIP.Name.ToUpper)
    '                            archivoTramaEnZIP = archivoZIP.GetNextEntry
    '                        End While

    '                        'VERIFICAR NOMBRES DE TRAMAS DUPLICADAS
    '                        listResultadoTmp = (From item As String In listArchivos Group By item Into Group Where Group.Count() > 1 Select item).ToList
    '                        If listResultadoTmp.Count > 0 Then
    '                            Throw New BPaqueteException("El paquete consigna " & listResultadoTmp.Count & " trama(s) con nombres similares.")
    '                        End If
    '                        strRespuesta = "1"
    '                        Exit For
    '                    Catch ex As ZipException
    '                    End Try
    '                End If
    '            Next
    '        End If
    '        If strRespuesta = "1" Then
    '            If almenosUnaClave AndAlso String.IsNullOrEmpty(archivoZIP.Password) Then
    '                strRespuesta = "El paquete no cuenta con la clave correspondiente."
    '            End If
    '            claveElegida = archivoZIP.Password
    '        Else
    '            If almenosUnaClave Then
    '                If String.IsNullOrEmpty(archivoZIP.Password) Then
    '                    strRespuesta = "El paquete cuenta con la clave correspondiente."
    '                Else
    '                    strRespuesta = "El paquete no cuenta con una clave válida."
    '                End If
    '            Else
    '                strRespuesta = "El paquete no puede descomprimirse."
    '            End If
    '        End If
    '    Catch ex As ZipException
    '        strRespuesta = "Ocurrió un inconveniente descifrando el paquete."
    '    Catch ex As BPaqueteException
    '        strRespuesta = ex.Message
    '    Catch ex As Exception
    '        strRespuesta = "Ocurrió un error descifrando el paquete."
    '    Finally
    '        archivoTramaEnZIP = Nothing
    '        If archivoZIP IsNot Nothing Then archivoZIP.Close()
    '    End Try
    '    Return strRespuesta
    'End Function

    '    Public Function descomprimirPaqueteZip2(ByVal sNombreArchivo As String, ByVal sRutaDestino As String, listaPermisibles As PaquetePermitidoLista, almenosUnaClave As Boolean, ByRef claveElegida As String) As String
    '        Dim archivoZIP As ZipInputStream = Nothing
    '        Dim archivoTramaEnDisco As FileStream = Nothing
    '        Dim isPudoDescomprimir As Boolean = False
    '        Dim isProbandoClave As Boolean = False
    '        Dim indiceClaveCorrecta As Integer = -1
    '        Dim strClaveCorrecta As String = String.Empty
    '        Dim archivoTramaEnZIP As ZipEntry = Nothing
    '        Dim archivosAlmacen As String = String.Empty
    '        Dim arrClaves As String() = Nothing
    '        Dim listResultadoTmp As List(Of String)
    '        Dim strRespuesta As String = String.Empty

    '        Try
    '            Dim identificadorPaquete As String = Path.GetFileNameWithoutExtension(sNombreArchivo)
    '            archivosAlmacen = sRutaDestino & identificadorPaquete
    '            Directory.CreateDirectory(archivosAlmacen)

    '            archivoZIP = New ZipInputStream(New FileStream(sRutaDestino & "\" & sNombreArchivo, FileMode.Open, FileAccess.Read))

    '            'CONSULTAR TODAS LAS CLAVES DISPONIBLES
    '            If listaPermisibles IsNot Nothing Then
    '                listResultadoTmp = (From item As PaquetePermitido In listaPermisibles.archivosPermitidos Select item.clave Distinct).ToList
    '                arrClaves = listResultadoTmp.ToArray
    '            End If

    '            If almenosUnaClave Then
    '                If arrClaves IsNot Nothing AndAlso arrClaves.Length > 0 Then
    '                    indiceClaveCorrecta = 0
    '                    archivoZIP.Password = arrClaves(indiceClaveCorrecta)
    '                Else
    '                    Throw New BPaqueteException("Excepción con la certificación del paquete.")
    '                End If
    '            End If

    'NUEVA_CLAVE:
    '            If isProbandoClave Then
    '                If arrClaves IsNot Nothing AndAlso arrClaves.Length > 0 Then
    '                    If indiceClaveCorrecta + 1 < arrClaves.Length Then
    '                        indiceClaveCorrecta += 1
    '                        archivoZIP.Password = arrClaves(indiceClaveCorrecta)
    '                    Else
    '                        archivoTramaEnZIP = Nothing
    '                    End If
    '                Else
    '                    archivoTramaEnZIP = Nothing
    '                End If
    '            Else
    '                archivoTramaEnZIP = archivoZIP.GetNextEntry
    '                If almenosUnaClave AndAlso Not archivoTramaEnZIP.IsCrypted Then
    '                    Throw New BPaqueteException("Excepción con la certificación de las tramas del paquete")
    '                End If
    '            End If

    '            While archivoTramaEnZIP IsNot Nothing
    '                If archivoTramaEnZIP.IsDirectory Then
    '                    strRespuesta = "No está permitido recepcionar carpetas en los paquetes."
    '                    Exit While
    '                ElseIf archivoTramaEnZIP.IsFile Then
    '                    Try
    '                        'LEER EL ARCHIVO EMPAQUETADO
    '                        Dim count As Long
    '                        Dim buffer(4096) As Byte
    '                        count = archivoZIP.Read(buffer, 0, 4096)
    '                        isPudoDescomprimir = True
    '                        Dim archivoAlmacenado As String = archivoTramaEnZIP.Name.ToUpper

    '                        'VERIFICAR SI EL ARCHIVO A DESEMPAQUETAR ES UN ARCHIVO PERMITIDO
    '                        If almenosUnaClave Then
    '                            listResultadoTmp = (From item As PaquetePermitido In listaPermisibles.archivosPermitidos Where item.clave = archivoZIP.Password And item.archivo = archivoAlmacenado Select item.clave Distinct).ToList
    '                            If listResultadoTmp.Count = 0 Then
    '                                Throw New BPaqueteException("El archivo " & archivoAlmacenado & " no está permitido en el paquete")
    '                            End If
    '                        Else
    '                            If listaPermisibles IsNot Nothing Then
    '                                If String.IsNullOrEmpty(archivoZIP.Password) Then
    '                                    listResultadoTmp = (From item As PaquetePermitido In listaPermisibles.archivosPermitidos Where item.archivo = archivoAlmacenado Select item.clave Distinct).ToList
    '                                Else
    '                                    listResultadoTmp = (From item As PaquetePermitido In listaPermisibles.archivosPermitidos Where item.clave = archivoZIP.Password And item.archivo = archivoAlmacenado Select item.clave Distinct).ToList
    '                                End If
    '                                If listResultadoTmp.Count = 0 Then
    '                                    Throw New BPaqueteException("El archivo " & archivoAlmacenado & " no está permitido en el paquete")
    '                                End If
    '                            End If
    '                        End If

    '                        'DESEMPAQUETAR EL ARCHIVO
    '                        archivoTramaEnDisco = New FileStream(archivosAlmacen & "\" & identificadorPaquete & "_" & archivoAlmacenado, FileMode.OpenOrCreate, FileAccess.Write)
    '                        While count > 0
    '                            archivoTramaEnDisco.Write(buffer, 0, count)
    '                            count = archivoZIP.Read(buffer, 0, 4096)
    '                        End While
    '                        archivoTramaEnDisco.Close()

    '                        'OBTENER EL SIGUIENTE ARCHIVO EMPAQUETADO
    '                        archivoTramaEnZIP = archivoZIP.GetNextEntry
    '                        If archivoTramaEnZIP IsNot Nothing AndAlso almenosUnaClave AndAlso Not archivoTramaEnZIP.IsCrypted Then
    '                            Throw New BPaqueteException("Excepción con la certificación de las tramas del paquete")
    '                        End If
    '                        strRespuesta = "1"
    '                    Catch ex As ZipException
    '                        If Not isPudoDescomprimir Then
    '                            isProbandoClave = True
    '                            GoTo NUEVA_CLAVE
    '                        End If
    '                    Catch ex As BPaqueteException
    '                        Throw New BPaqueteException(ex.Message)
    '                    Catch ex As Exception
    '                        Throw New BPaqueteException("Ocurrió un error almacenando las tramas del paquete.")
    '                    End Try
    '                Else
    '                    Throw New BPaqueteException("Sólo está permitido recepcionar archivos en los paquetes.")
    '                End If
    '            End While
    '        Catch ex As ZipException
    '            strRespuesta = "Ocurrió un error leyendo el paquete."
    '        Catch ex As BPaqueteException
    '            strRespuesta = ex.Message
    '        Catch ex As Exception
    '            strRespuesta = "Ocurrió un error almaecnando las tramas del paquete."
    '        Finally
    '            If archivoTramaEnDisco IsNot Nothing Then archivoTramaEnDisco.Close()
    '            If archivoZIP IsNot Nothing Then
    '                claveElegida = archivoZIP.Password
    '                archivoZIP.Close()
    '            End If
    '        End Try

    '        If Not isPudoDescomprimir And strRespuesta.Length = 0 Then
    '            strRespuesta = "No se puede procesar el paquete."
    '        End If

    '        Return strRespuesta
    '    End Function

End Class

Public Class BPaqueteException : Inherits Exception

    Public Sub New(mensaje As String)
        MyBase.New(mensaje)
    End Sub

End Class