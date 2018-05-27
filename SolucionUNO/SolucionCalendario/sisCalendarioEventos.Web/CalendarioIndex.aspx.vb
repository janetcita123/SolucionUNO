Imports sisCalendarioEventos.Business
Imports sisCalendarioEventos.Entity
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.IO
Imports System.Reflection


Public Class CalendarioIndex

    Inherits System.Web.UI.Page
    Public jsPath As String
    Public jsEventos As String
    Public jsUsuario As String
    Public jsRol As String
    Dim jsSerializer As New JavaScriptSerializer
    Dim objBusinessEvento As New BEvento
    Dim objUsuario As New Usuario

    Dim usuario As String
    Dim dni As String
    Dim rolUsu As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objUsuario = Session("UsuarioIPRESS")
        If objUsuario.rol = 1 Then
            ocultar.Disabled = True
        End If
 

        usuario = objUsuario.usuario
        dni = objUsuario.dni
        rolUsu = objUsuario.rol

        cargarParametros()
        ObtenerEventos() 
        SerializaPublic(usuario, 1)
        SerializaPublic(rolUsu, 2)
    End Sub



#Region "Eventos"
#End Region



#Region "Web"
    <System.Web.Services.WebMethod()> _
    Public Shared Function GuardarEvento(ByVal fInicio As String, ByVal fFinal As String, ByVal sTipo As String, sOrganiza As String, ByVal sTitle As String, ByVal sBody As String, ByVal sAreas As String, ByVal sEntidades As String, ByVal sPersonas As String, ByVal sAgenda As String, ByVal sUsuario As String) As String
        'primero insertamos la cabecera
        Dim resultado As String = "ok"
        Dim estado As String = "0"
        Dim tipo As String = "1"
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        Dim idEvento As String
        idEvento = objBusinessEvento.GuardarNuevoEvento(tipo, fInicio, fFinal, sTipo, sOrganiza, sTitle, sBody, estado, sUsuario)

        'luego insertamos los detalles Areas Participantes
        If idEvento.Length < 10 And sAreas.Length Then
            resultado = objBusinessEvento.InsertaArea(idEvento, sAreas)
        End If

        'luego insertamos los detalles Entidades Participantes
        If (resultado = "ok" And sEntidades.Length > 0) Then
            resultado = objBusinessEvento.InsertaEntidad(idEvento, sEntidades)
        Else
            If (resultado = "1" And sEntidades.Length > 0) Then
                resultado = objBusinessEvento.InsertaEntidad(idEvento, sEntidades)
            End If
        End If
       

        'luego insertamos los detalles usuarios Participantes
        If (resultado = "ok" And sPersonas.Length > 0) Then
            resultado = objBusinessEvento.InsertaPersona(idEvento, sPersonas)
        Else
            If (resultado = "1" And sPersonas.Length > 0) Then
                resultado = objBusinessEvento.InsertaPersona(idEvento, sPersonas)
            End If
        End If
        

        'luego insertamos la agenda del evento
        If (resultado = "ok" And sAgenda.Length > 0) Then
            resultado = objBusinessEvento.InsertaAgenda(idEvento, sAgenda)
        Else
            If (resultado = "1" And sAgenda.Length > 0) Then
                resultado = objBusinessEvento.InsertaAgenda(idEvento, sAgenda)
            End If
        End If
        
        Return resultado
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function ActualizarEvento(ByVal idEvento As String, ByVal fInicio As String, ByVal fFinal As String, ByVal sAreas As String, ByVal sEntidades As String, ByVal sPersonas As String, ByVal sAgenda As String, ByVal sUsuario As String) As String
        'primero insertamos la cabecera
        Dim resultado As String = "1"
        Dim estado As String = "0"
        Dim tipo As String = "1"
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento

        'resultado = objBusinessEvento.ReprogramarEvento(idEvento, fInicio, fFinal)
        'luego insertamos los detalles Areas Participantes
        If idEvento.Length < 10 And sAreas.Length > 1 Then
            resultado = objBusinessEvento.InsertaAreaUDP(idEvento, sAreas)
        End If

        'luego insertamos los detalles Entidades Participantes
        If (resultado = "1" And sEntidades.Length > 0) Then
            resultado = objBusinessEvento.InsertaEntidadUDP(idEvento, sEntidades)
        End If

        'luego insertamos los detalles usuarios Participantes
        If (resultado = "1" And sPersonas.Length > 1) Then
            resultado = objBusinessEvento.InsertaPersonaUDP(idEvento, sPersonas)
        End If

        'luego insertamos la agenda del evento
        If (resultado = "ok" And sAgenda.Length > 1) Then
            resultado = objBusinessEvento.InsertaAgendaUDP(idEvento, sAgenda)
        End If
        If (resultado = "1" And sAgenda.Length > 1) Then
            resultado = objBusinessEvento.InsertaAgendaUDP(idEvento, sAgenda)
        End If
        Return resultado
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GuardarAcuerdo(ByVal idEvento As String, ByVal idAgenda As String, ByVal responsable As String, ByVal titulo As String, ByVal descripcion As String, ByVal fFecha As String) As EAcuerdo

        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        Dim lAcuerdo As New EAcuerdo
        lAcuerdo = objBusinessEvento.GuardarAcuerdo(idEvento, idAgenda, responsable, titulo, descripcion, fFecha)
        Return lAcuerdo
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GuardarObservacion(ByVal idEvento As String, ByVal titulo As String, ByVal descripcion As String) As EObservacion

        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        Dim lObservacion As New EObservacion
        lObservacion = objBusinessEvento.GuardarObservacion(idEvento, titulo, descripcion)
        Return lObservacion
    End Function



    <System.Web.Services.WebMethod()> _
    Public Shared Function EliminarAgenda(ByVal idAgenda As String) As String
        Dim resultado As String = "1"
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        Dim lAcuerdo As New EAcuerdo
        resultado = objBusinessEvento.EliminarAgenda(idAgenda)
        Return resultado
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function ActualizaAcuerdo(ByVal idAcuerdo As String, ByVal idEvento As String, ByVal idAgenda As String, ByVal responsable As String, ByVal titulo As String, ByVal descripcion As String) As EAcuerdo

        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        Dim lAcuerdo As New EAcuerdo
        lAcuerdo = objBusinessEvento.ActualizaAcuerdo(idAcuerdo, idEvento, idAgenda, responsable, titulo, descripcion)
        Return lAcuerdo
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function ActualizaObservacion(ByVal idObservacion As String, ByVal titulo As String, ByVal descripcion As String) As EObservacion

        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        Dim lObservacion As New EObservacion
        lObservacion = objBusinessEvento.ActualizaObservacion(idObservacion, titulo, descripcion)
        Return lObservacion
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function EliminarAcuerdo(ByVal idAcuerdo As String) As EAcuerdo

        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        Dim lAcuerdo As New EAcuerdo
        lAcuerdo = objBusinessEvento.EliminarAcuerdo(idAcuerdo)
        Return lAcuerdo
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function EliminarObservacion(ByVal idObservacion As String) As EObservacion

        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        Dim lObservacion As New EObservacion
        lObservacion = objBusinessEvento.EliminarObservacion(idObservacion)
        Return lObservacion
    End Function



    <System.Web.Services.WebMethod()> _
    Public Shared Function FinalizarEvento(ByVal idEvento As String, ByVal sObservacion As String) As String

        Dim resultado As String = ""
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        ' resultado = objBusinessEvento.FinalizarEvento(idEvento, sObservacion)
        Return resultado
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function MarcaAsistencia(ByVal idEvento As String, ByVal sPersona As String, ByVal sChek As String) As String

        Dim resultado As String
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        resultado = objBusinessEvento.MarcaAsistencia(idEvento, sPersona, sChek)
        Return resultado


    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function ReprogramarEvento1(ByVal fInicio As String, ByVal fFinal As String, ByVal sEvento As String, ByVal tipo As String, ByVal sUsuario As String) As String



        Dim resultado As String
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento
        resultado = objBusinessEvento.ReprogramarEvento1(fInicio, fFinal, sEvento, tipo, sUsuario)
        Return resultado


    End Function

    <System.Web.Services.WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=False, XmlSerializeString:=False)> _
    Public Shared Function ObtenerEventos3(ByVal from As String, ByVal to2 As String) As String

        Dim jsEventos2 As String
        Dim Inicio As DateTime = DateTime.Parse(from)
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento

        Dim lEvento As New List(Of EEvento)
        'lEvento = objBusinessEvento.ListarEventos("PCORONADOF")
        jsEventos2 = jsSerializer.Serialize(lEvento)


        Return jsEventos2

    End Function
  

    '<System.Web.Services.WebMethod()> _
    'Public Shared Function ObtenerEventos2(ByVal from As String, ByVal to2 As String) As Json

    '    Dim Inicio As DateTime = DateTime.Parse(from)
    '    Dim jsSerializer As New JavaScriptSerializer
    '    Dim objBusinessEvento As New BEvento

    '    Dim lEvento As New Json
    '    lEvento = objBusinessEvento.Prueba(usuario, dni)


    '    Return lEvento

    'End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function CargarArea(ByVal idDom As String, ByVal idTipoEvento As String) As ECarga
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessCarga As New BCarga
        Dim lEvento As New ECarga
        lEvento = objBusinessCarga.CargarArea(idDom, idTipoEvento)
        Return lEvento

    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function ListarUsuarioArea(ByVal tipo As String, ByVal cadenaArea As String) As EUsuario
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessCarga As New BCarga
        Dim lUsuarios As New EUsuario
        lUsuarios = objBusinessCarga.ListarUsuarioArea(tipo, cadenaArea)
        Return lUsuarios
    End Function

    <System.Web.Services.WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function SubmitItems(ByVal items As Object)
        Dim lstItems As List(Of Object) = New JavaScriptSerializer().ConvertToType(Of List(Of Object))(items)

        RequestEvento = items
        Return "ok"
    End Function

    '  <System.Web.Services.WebMethod()> _
    '<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    '  Public Shared Function File(ByVal items As Object, ByVal accion As String)
    '      Dim lstItems As List(Of Object) = New JavaScriptSerializer().ConvertToType(Of List(Of Object))(items)

    '      RequestEvento = items
    '      Return "ok"
    'End Function

    <System.Web.Services.WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function File(ByVal data As HttpPostedFile) As String
        Dim nombre As HttpPostedFile = data
        Return "ok"
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function UploadFile(ByVal data As HttpContext) As String

        Dim name As HttpContext = data
        'If Not Request.Content.IsMimeMultipartContent() Then
        '    Return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "The request doesn't contain valid content!")
        'End If

        'Try
        '    Dim provider = New MultipartMemoryStreamProvider()
        '    Await Request.Content.ReadAsMultipartAsync(provider)
        '    For Each File() In provider.Contents
        '        Dim dataStream = Await File.ReadAsStreamAsync()
        '        Dim response = Request.CreateResponse(HttpStatusCode.OK)
        '        response.Content = New StringContent("Successful upload", Encoding.UTF8, "text/plain")
        '        response.Content.Headers.ContentType = New MediaTypeWithQualityHeaderValue("text/html")
        '        Return response
        '    Next
        'Catch e As Exception
        '      Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message)
        'End Try
        Return "ok"
    End Function





    <System.Web.Services.WebMethod()> _
    Public Shared Function SubirArchivo(ByVal file As Object, ByVal archivo As String)
        Dim ruta = file
        '   ruta = ruta.Replace(archivo, "")

        Dim fileName As String = archivo
        Dim sourcePath As String '= ruta
        Dim url = Path.GetTempPath() 'Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        Dim z = Path.GetTempFileName()
        'Dim url = HttpContext.Current.Request.Url.AbsoluteUri
        Dim targetPath As String = "D:\subir_archivo"
        Dim sourceFile As String = System.IO.Path.Combine(sourcePath, fileName)
        Dim destFile As String = System.IO.Path.Combine(targetPath, fileName)
        If Not System.IO.Directory.Exists(targetPath) Then
            System.IO.Directory.CreateDirectory(targetPath)
        End If

        System.IO.File.Copy(sourceFile, destFile, True)
        If System.IO.Directory.Exists(sourcePath) Then
            Dim files As String() = System.IO.Directory.GetFiles(sourcePath)
            For Each s As String In files
                Dim a As String = System.IO.Path.GetFileName(s)
                Dim b As String = fileName

                If a = b Then
                    fileName = System.IO.Path.GetFileName(s)
                    destFile = System.IO.Path.Combine(targetPath, fileName)
                    System.IO.File.Copy(s, destFile, True)
                End If

            Next
        Else
            Console.WriteLine("Source path does not exist!")
        End If


        'My.Computer.Network.UploadFile("D:\hola.txt", "D:\subir_archivo")
        ' 
        Return url
    End Function

#End Region

#Region "Recursivo"

    Private Sub cargarParametros()
        Dim Dt_TipoEvento As New DataTable
        Dim ds As DataSet
        Dim bCarga As New BCarga

        ds = bCarga.ListarTipoEvento()


        cbotipoEvento.DataSource = ds.Tables(0)
        cbotipoEvento.DataTextField = "DES"
        cbotipoEvento.DataValueField = "ID"

        cbotipoEvento.DataBind()
    End Sub
    Sub ObtenerEventos()
        Dim jsSerializer As New JavaScriptSerializer
        Dim objBusinessEvento As New BEvento

        Dim lEvento As New Json
        lEvento = objBusinessEvento.Prueba(usuario, dni)
        jsEventos = jsSerializer.Serialize(lEvento)

    End Sub
    


    Private Sub SerializaPublic(dato As String, id As Integer)
        Dim jsSerializer As New JavaScriptSerializer
        If id = 1 Then
            Dim ss As String = Request.Url.AbsoluteUri  ' Path.GetFullPath("CalendarioIndex")
            ss = "{path:'" + ss + "'}"
            jsUsuario = jsSerializer.Serialize(dato)
            jsPath = jsSerializer.Serialize(ss)


        End If

        If id = 2 Then
            jsRol = jsSerializer.Serialize(dato)
        End If
    End Sub
#End Region

    Private Function rutaWeb() As String
        Return ConfigurationManager.AppSettings("CalendarioCarpeta").ToString()
    End Function


End Class