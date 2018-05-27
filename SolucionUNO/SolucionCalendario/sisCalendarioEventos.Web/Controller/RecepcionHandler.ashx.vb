Imports sisCalendarioEventos.Entity
Imports sisCalendarioEventos.Business

Public Class RecepcionHandler : Implements System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim respuesta As String
        If context.Session Is Nothing OrElse context.Session.IsNewSession Then
            respuesta = "000000000"
        Else
            Dim accion As String = Trim(context.Request.Form("accion")).ToUpper
            If accion = "I" Then
                respuesta = RegistrarPaquete(context)
            ElseIf accion = "D" Then
                respuesta = EliminarPaquete(context)
            ElseIf accion = "S" Then
                respuesta = BuscarPaquetes(context)
            ElseIf accion = "V" Then
                respuesta = MostrarPaquete(context)
            Else
                respuesta = "Operación incorrecta"
            End If
        End If
        context.Response.Write(respuesta)
    End Sub

    Private Function RegistrarPaquete(ByVal context As HttpContext)
        context.Response.ContentType = "text/plain"
        Dim directorio As String = context.Server.MapPath("~/paquetes/")
        Dim respuesta As String = String.Empty
        Dim archivoNombre As String = String.Empty
        Dim oBPaquete As New BPaquete(System.Configuration.ConfigurationManager.AppSettings("Conexion.Cadena"))
        Dim strSesionUsuario As String = context.Session("usuariofrm").ToString
        Dim periodoID As String = Trim(context.Request.Form("p")).Trim
        Dim cronogramaID As String = Trim(context.Request.Form("c")).Trim

        If String.IsNullOrEmpty(periodoID) Then
            respuesta = "Excepción consignando el periodo actual."
        ElseIf String.IsNullOrEmpty(cronogramaID) Then
            respuesta = "Excepción consignando el cronograma actual."
        Else
            If context.Request.Files.Count = 0 Then
                respuesta = "Por favor seleccione el archivo que contiene la trama de prestaciones"
            ElseIf context.Request.Files.Count = 1 Then
                Dim archivoProcesado As HttpPostedFile = context.Request.Files(0)
                archivoNombre = System.IO.Path.GetFileName(archivoProcesado.FileName).ToUpper
                Dim identificadorPaquete As String = System.IO.Path.GetFileNameWithoutExtension(archivoNombre)
                'respuesta = oBPaquete.alojarPaquete(archivoProcesado, directorio, archivoNombre)
                If respuesta = "1" Then
                    Dim claveElegida As String = String.Empty
                    Dim oPaquete As Paquete = oBPaquete.ValidarPaquetePermitido(strSesionUsuario, identificadorPaquete, periodoID, cronogramaID)
                    If String.IsNullOrEmpty(oPaquete.mensaje) Then
                        respuesta = oBPaquete.isDesempaquetar(archivoNombre, directorio, oPaquete, True)
                        If respuesta = "1" Then
                            respuesta = oBPaquete.descomprimirPaqueteZip(archivoNombre, directorio, oPaquete)
                            If (respuesta = "1") Then
                                Dim servidorFTP As String = ConfigurationManager.AppSettings("ServidorFTPTemporal").ToString()
                                Dim usuarioFTP As String = ConfigurationManager.AppSettings("UsuarioFTP").ToString()
                                Dim claveFTP As String = ConfigurationManager.AppSettings("ClaveFTP").ToString()
                                oBPaquete.configurarServidorFTP(servidorFTP, usuarioFTP, claveFTP)
                                respuesta = oBPaquete.cargarArchivosFTP(archivoNombre, directorio)
                                If respuesta = "1" Then
                                    oPaquete.usuarioCrea = strSesionUsuario
                                    oPaquete.descripcion = archivoNombre
                                    oPaquete.PeriodoID = periodoID
                                    oPaquete.CronogramaID = cronogramaID
                                    oBPaquete.GuardarPaquete(oPaquete)
                                    If oPaquete.codigoError = "1" Then
                                        Dim detallePaquete As List(Of DetallePaquete) = oBPaquete.ListarDetalle(oPaquete.ID)
                                        respuesta = detallePaquete.Count
                                        For Each o As DetallePaquete In detallePaquete
                                            respuesta &= "$" & o.tablaNombre & "$" & o.registrosEnResumen & "$" & o.registrosEnTrama & "$" & o.registrosEnBD & "$" & o.existeDiferencia
                                        Next
                                        respuesta = "1$" & oPaquete.ID & "$" & oPaquete.mensaje & "$" & oPaquete.IPRESS & "$" & oPaquete.aplicativo & "$" & oPaquete.fechaSubida & "$" & oPaquete.versionTrama & "$" & respuesta
                                    Else
                                        respuesta = oPaquete.mensaje
                                    End If
                                    oBPaquete.eliminarDirectorio(directorio & identificadorPaquete)
                                End If
                            Else
                                'oBPaquete.eliminarArchivo(archivoNombre, directorio)
                                oBPaquete.eliminarDirectorio(directorio & identificadorPaquete)
                            End If
                        Else
                            'oBPaquete.eliminarArchivo(archivoNombre, directorio)
                            oBPaquete.eliminarDirectorio(directorio & identificadorPaquete)
                        End If
                    Else
                        respuesta = oPaquete.mensaje
                    End If
                End If
            Else
                respuesta = "Por favor seleccionar solamente un archivo que contiene la trama de prestaciones."
            End If
        End If

        oBPaquete = Nothing
        Return respuesta
    End Function

    Private Function EliminarPaquete(context As HttpContext) As String
        context.Response.ContentType = "text/plain"
        Dim strSesionUsuario As String = context.Session("usuariofrm").ToString
        Dim paqueteID As String = Trim(context.Request.Form("p")).Trim
        Dim motivoEliminacion As String = Trim(context.Request.Form("m")).Trim.ToUpper
        Dim strRespuesta As String
        Dim oBPaquete As BPaquete
        If Not IsNumeric(paqueteID) Then
            strRespuesta = "El código consignado para eliminar paquete es incorrecto."
        ElseIf String.IsNullOrEmpty(motivoEliminacion) Then
            strRespuesta = "Por favor consignar el motivo de eliminación del paquete."
        Else
            oBPaquete = New BPaquete(System.Configuration.ConfigurationManager.AppSettings("Conexion.Cadena"))
            strRespuesta = oBPaquete.EliminarPaquete(paqueteID, strSesionUsuario, motivoEliminacion)
            oBPaquete = Nothing
        End If
        Return strRespuesta
    End Function

    Private Function BuscarPaquetes(context As HttpContext) As String
        context.Response.ContentType = "text/plain"
        Dim strSesionUsuario As String = context.Session("usuariofrm").ToString

        Dim param_flag As String = Trim(context.Request.Form("f")).Trim
        Dim param_codigo As String = Trim(context.Request.Form("c")).Trim.ToUpper

        'Dim param_gmr As String = Trim(context.Request.Form("g")).Trim
        'Dim param_disa As String = Trim(context.Request.Form("d")).Trim.ToUpper
        'Dim param_udr As String = Trim(context.Request.Form("u")).Trim
        'Dim param_ue As String = Trim(context.Request.Form("e")).Trim.ToUpper
        'Dim param_eess As String = Trim(context.Request.Form("i")).Trim.ToUpper
        Dim param_periodo As String = Trim(context.Request.Form("p")).Trim
        Dim param_mes As String = Trim(context.Request.Form("m")).Trim.ToUpper
        Dim strRespuesta As String
        If String.IsNullOrEmpty(param_periodo) Then
            strRespuesta = "El periodo consignado para la busquedas de paquetes es incorrecto."
        ElseIf String.IsNullOrEmpty(param_mes) Then
            strRespuesta = "El mes consignado para la busquedas de paquetes es incorrecto."
        Else
            'Dim dtb As DataTable = New BRecepcion(System.Configuration.ConfigurationManager.AppSettings("Conexion.Cadena")).BuscarPaquete(strSesionUsuario, param_gmr, param_disa, param_udr, param_ue, param_eess, param_periodo, param_mes)
            Dim dtb As DataTable = New BRecepcion(System.Configuration.ConfigurationManager.AppSettings("Conexion.Cadena")).BuscarPaquete(strSesionUsuario, param_flag, param_codigo, param_periodo, param_mes)
            If dtb IsNot Nothing Then
                strRespuesta = dtb.Rows.Count & "$"
                For Each fila As DataRow In dtb.Rows
                    strRespuesta &= fila("PAQ_IDPAQUETE").ToString & "$" & fila("PAQ_IDESTABLECIMIENTO").ToString & "$" & fila("PER_PERIODO_DSCP").ToString & "$" & fila("PAQ_NUMEROENVIO").ToString & "$" & fila("PAQ_RESPONSABLE").ToString & "$" & fila("PAQ_FECHA_RECEPCION").ToString & "$" & fila("PAQ_IDUSUARIO").ToString & "$" & fila("PAQ_DET_ATENCIONES").ToString & "$"
                Next
            Else
                strRespuesta = "Ocurrió un error procesando la consulta."
            End If
        End If
        Return strRespuesta
    End Function

    Private Function MostrarPaquete(context As HttpContext) As String
        context.Response.ContentType = "text/plain"
        Dim strSesionUsuario As String = context.Session("usuariofrm").ToString
        Dim param_idpaquete As String = Trim(context.Request.Form("p")).Trim
        Dim respuesta As String = String.Empty
        If String.IsNullOrEmpty(param_idpaquete) Then
            respuesta = "Se debe indicar el paquete ha visualizar."
        Else
            Dim dts As DataSet '= New BRecepcion(System.Configuration.ConfigurationManager.AppSettings("Conexion.Cadena")).MostrarPaquete(strSesionUsuario, param_idpaquete)
            If dts IsNot Nothing AndAlso dts.Tables.Count = 2 Then
                Dim dtb As DataTable = dts.Tables(0)
                If dtb.Rows.Count = 1 Then
                    Dim registrosEnResumen As String
                    Dim registrosEnTrama As String
                    Dim registrosEnBD As String
                    respuesta = dts.Tables(1).Rows.Count
                    For Each fila As DataRow In dts.Tables(1).Rows
                        registrosEnResumen = fila("REGISTROS_TRAMA").ToString
                        registrosEnTrama = fila("REGISTROS_BD").ToString
                        registrosEnBD = registrosEnResumen
                        respuesta &= "$" & fila("TABLA").ToString & "$" & registrosEnResumen & "$" & registrosEnTrama & "$" & registrosEnBD & "$" & IIf(registrosEnResumen = registrosEnTrama And registrosEnResumen = registrosEnBD, "N", "S")
                    Next
                    respuesta = "1$" & param_idpaquete & "$$" & dtb.Rows(0)("PAQ_EESS").ToString & "$" & dtb.Rows(0)("PAQ_APP").ToString & "$" & dtb.Rows(0)("PAQ_FECHA_RECEPCION").ToString & "$" & dtb.Rows(0)("PAQ_VER_TRAMA").ToString & "$" & dtb.Rows(0)("PAQ_IS_ELIMINAR").ToString & "$" & dtb.Rows(0)("PAQ_DESCRIPCION").ToString & "$" & respuesta
                Else
                    respuesta = "No se han podido visualizar los datos del paquete."
                End If
            Else
                respuesta = "Ocurrió un error procesando la consulta."
            End If
        End If
        Return respuesta
    End Function


    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class