Imports sisCalendarioEventos.Entity
Imports sisCalendarioEventos.Data
Public Class BCarga
    Dim objData As New DMensaje

  

    Public Sub GuardarEvento(ByVal fInicio As String, ByVal fFinal As String, ByVal sTipo As String, ByVal sTitle As String, ByVal sBody As String)

        Dim fechaInicio As String

        fechaInicio = fInicio

        'AQUI LLAMAS A LA BASE DE DATOS CON LOS PARAMETROS ENVIADOS, SI LLEGUE HASTA AQUI CON DEBUG
    End Sub


    Public Function ListarTipoEvento() As DataSet
        Dim Obj As New DCarga()
        Return Obj.ListarTipoEvento(1)
    End Function
    Public Function CargarArea(ByVal tipo As String, ByVal idEvento As String) As ECarga
        Dim ObjDataCarga As New DCarga()
        Return ObjDataCarga.CargarArea(tipo, idEvento)
    End Function

    Public Function ListarUsuarioArea(ByVal tipo As String, ByVal cadenaArea As String) As EUsuario
        Dim ObjDataCarga As New DCarga()
        Return ObjDataCarga.ListarUsuarioArea(tipo, cadenaArea)
    End Function


End Class
