Imports sisCalendarioEventos.Business
Imports sisCalendarioEventos.Entity
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.IO
Imports System.Reflection
Public Class prueba
    Inherits System.Web.UI.Page
    Public jsEventos As String
    Public jsUsuario As String
    Public jsRol As String
    Dim jsSerializer As New JavaScriptSerializer
    Dim objBusinessEvento As New BEvento
    Dim objUsuario As New Usuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarParametros()
        ObtenerEventos()
        SerializaPublic("USUARIO", 1)
        SerializaPublic("PCORONADOF", 2)
    End Sub

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
        lEvento = objBusinessEvento.Prueba("PCORONADOF")
        jsEventos = jsSerializer.Serialize(lEvento)

    End Sub


    Private Sub SerializaPublic(dato As String, id As Integer)
        Dim jsSerializer As New JavaScriptSerializer
        If id = 1 Then
            jsUsuario = jsSerializer.Serialize(dato)

        End If

        If id = 2 Then
            jsRol = jsSerializer.Serialize(dato)
        End If
    End Sub
#End Region
End Class