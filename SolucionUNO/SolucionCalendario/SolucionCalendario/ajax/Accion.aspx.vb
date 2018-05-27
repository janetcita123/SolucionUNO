'Imports sisSalaSituacional.EntityLayer
'Imports sisSalaSituacional.BusinessLayer

Imports System.Web.Script.Serialization
Imports System.IO
Imports sisCalendarioEventos.Entity



Public Class Accion
    Inherits System.Web.UI.Page

    'Dim objMensaje As New BMensaje
    'Dim objPresupuesto As New BPresupuesto
    'Dim objFinanciamiento As New BFinanciamiento
    Dim jsSerializer As New JavaScriptSerializer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strAccion = Request.Params("accion").ToString
            Select Case strAccion
                Case "ValidateFile" : ValidateFile()
                Case "UploadFile" : UploadFile()
                Case "GraphicInfo" : GraphicInfo()
            End Select
        Catch Exception As Exception
            Dim objMensaje As New EMensaje
            'objMensaje.IdErrorMessage = 1
            'objMensaje.Title = "Error Inesperado"
            'objMensaje.Type = EMensaje.ErrorMessage
            'objMensaje.UserMessage = Exception.Message
            'Response.Write(jsSerializer.Serialize(objMensaje))
        End Try
    End Sub

    Sub ValidateFile()
        Dim strRutaProyecto = HttpContext.Current.Server.MapPath("~/temporal/")
        Dim strTipo = Request.Params("tipo").ToString
        Dim strRutaArchivo = ""
        Dim strFileName = ""

        Dim Archivo = Request.Files("archivo")
        strFileName = Archivo.FileName

        'Local
        strFileName = strFileName.Split("\")(strFileName.Split("\").Length - 1)

        strRutaArchivo = strRutaProyecto & strFileName
        If New FileInfo(strRutaArchivo).Exists Then
            File.Delete(strRutaArchivo)
        End If
        Archivo.SaveAs(strRutaArchivo)

        'Select Case strTipo
        '    Case "IndicadoresCapita"
        '        Response.Write(objPrestaciones.ValidarIndicadoresCapita(strRutaArchivo))
        '    Case "EjecucionPresupuestal"
        '        Response.Write(objPresupuesto.ValidarEjecucionPresupuestal(strRutaArchivo))
        '    Case "TransferenciasSis"
        '        Response.Write(objFinanciamiento.ValidarTransferenciasSis(strRutaArchivo))
        '    Case "TransferenciasVsEjecucion"
        '        Response.Write(objFinanciamiento.ValidarTransferenciasVsEjecucion(strRutaArchivo))
        '    Case "MontoPorAsegurado"
        '        Response.Write(objFinanciamiento.ValidarMontoPorAsegurado(strRutaArchivo))
        'End Select

        Session.Add("ArchivoCarga", strRutaArchivo)
    End Sub

    Sub UploadFile()
        Response.ContentType = "application/json;charset=utf-8"

        Dim strRutaArchivo = Session("ArchivoCarga")
        Dim strTipo = Request.Params("tipo").ToString
        Dim intPeriodo = CInt(Request.Params("periodo").ToString)
        Dim strMes = Request.Params("mes").ToString
        Dim intIdUsuario = 0

        'Select Case strTipo
        '    Case "IndicadoresCapita"
        '        objPrestaciones.SubirIndicadoresCapita(strRutaArchivo, intIdUsuario)
        '    Case "EjecucionPresupuestal"
        '        objPresupuesto.SubirEjecucionPresupuestal(strRutaArchivo, intPeriodo, strMes, intIdUsuario)
        '    Case "TransferenciasSis"
        '        objFinanciamiento.SubirTransferenciasSis(strRutaArchivo, intPeriodo, intIdUsuario)
        '    Case "TransferenciasVsEjecucion"
        '        objFinanciamiento.SubirTransferenciasVsEjecucion(strRutaArchivo, intPeriodo, intIdUsuario)
        '    Case "MontoPorAsegurado"
        '        objFinanciamiento.SubirMontoPorAsegurado(strRutaArchivo, intPeriodo, intIdUsuario)
        'End Select

        File.Delete(strRutaArchivo)
    End Sub

    Sub GraphicInfo()
        Response.ContentType = "application/json;charset=utf-8"

        Dim strGrafico = Request.Params("grafico").ToString

        Select Case strGrafico
            'Case "IndicadoresPreLiquidados" : Response.Write(jsSerializer.Serialize(objPrestaciones.IndicadoresPreLiquidados))
            'Case "TamizadosConCancer" : Response.Write(jsSerializer.Serialize(objPrestaciones.TamizadosConCancer))
            'Case "AtencionesSISOL" : Response.Write(jsSerializer.Serialize(objPrestaciones.AtencionesSISOL))
            'Case "AtendidosAtencionesEsperanzaMap" : Response.Write(jsSerializer.Serialize(objPrestaciones.AtendidosAtencionesEsperanzaMap))
            'Case "AvanceMetaCapita" : Response.Write(jsSerializer.Serialize(objPrestaciones.AvanceMetaCapita))
            'Case "PrestacionesEnReconsideracion" : Response.Write(jsSerializer.Serialize(objPrestaciones.PrestacionesEnReconsideracion))
            'Case "ProblNutr" : Response.Write(jsSerializer.Serialize(objPrestaciones.ProblNutr))
            'Case "DiagnosticadosOdontologicos" : Response.Write(jsSerializer.Serialize(objPrestaciones.DiagnosticadosOdontologicos))
            'Case "DiagnosticadosRefrectarios" : Response.Write(jsSerializer.Serialize(objPrestaciones.DiagnosticadosRefrectarios))
            'Case "PrestacionesNoConformePCPP" : Response.Write(jsSerializer.Serialize(objPrestaciones.PrestacionesNoConformesPCPP))
            'Case "AtencionesConsExt" : Response.Write(jsSerializer.Serialize(objPrestaciones.AtencionesConsExt))
            'Case "AtencionesServComp" : Response.Write(jsSerializer.Serialize(objPrestaciones.AtencionesServiciosComplementarios))
            'Case "controlPEA" : Response.Write(jsSerializer.Serialize(objPrestaciones.ResultadoPea))
            'Case "controlRecon" : Response.Write(jsSerializer.Serialize(objPrestaciones.ResultadoRecon))
        End Select
    End Sub

End Class