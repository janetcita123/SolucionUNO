Imports sisCalendarioEventos.Entity
Imports sisCalendarioEventos.Data
Public Class BEvento
    Dim ObjDataEvento As New DEvento()
    Public Function GuardarEvento(ByVal tipo As String, ByVal fInicio As String, ByVal fFinal As String, ByVal sTipo As String, ByVal sTitle As String, ByVal sBody As String, ByVal estado As String, ByVal user As String) As DataSet

        Return ObjDataEvento.GuardarEvento(tipo, fInicio, fFinal, sTipo, sTitle, sBody, estado, user)
    End Function

    Public Function Prueba(ByVal usuario As String, ByVal dni As String) As Json
        Return ObjDataEvento.ListarEventos(usuario, dni)
    End Function

    Public Function GuardarNuevoEvento(ByVal tipo As String, ByVal fInicio As String, ByVal fFinal As String, ByVal sTipo As String, sOrganiza As String, ByVal sTitle As String, ByVal sBody As String, ByVal estado As String, ByVal sUsuario As String) As String

        Return ObjDataEvento.GuardarNuevoEvento(tipo, fInicio, fFinal, sTipo, sOrganiza, sTitle, sBody, estado, sUsuario)
    End Function



    Public Function GuardarAcuerdo(ByVal idEvento As String, ByVal idAgenda As String, ByVal responsable As String, ByVal titulo As String, ByVal descripcion As String,
                                   ByVal fFecha As String) As EAcuerdo

        Return ObjDataEvento.GuardarAcuerdo(idEvento, idAgenda, responsable, titulo, descripcion, fFecha)
    End Function
    Public Function GuardarObservacion(ByVal idEvento As String, ByVal titulo As String, ByVal descripcion As String) As EObservacion

        Return ObjDataEvento.GuardarObservacion(idEvento, titulo, descripcion)
    End Function



    Public Function InsertaArea(ByVal idEvento As String, ByVal idArea As String) As String
        Return ObjDataEvento.InsertaArea(idEvento, idArea)
    End Function

    Public Function InsertaEntidad(ByVal idEvento As String, ByVal idEntidad As String) As String
        Return ObjDataEvento.InsertaEntidad(idEvento, idEntidad)
    End Function
    Public Function InsertaPersona(ByVal idEvento As String, ByVal idPersona As String) As String
        Return ObjDataEvento.InsertaPersona(idEvento, idPersona)
    End Function
    Public Function InsertaAgenda(ByVal idEvento As String, ByVal idAgenda As String) As String
        Return ObjDataEvento.InsertaAgenda(idEvento, idAgenda)
    End Function


    Public Function InsertaAreaUDP(ByVal idEvento As String, ByVal idArea As String) As String
        Return ObjDataEvento.InsertaAreaUDP(idEvento, idArea)
    End Function

    Public Function InsertaEntidadUDP(ByVal idEvento As String, ByVal idEntidad As String) As String
        Return ObjDataEvento.InsertaEntidadUDP(idEvento, idEntidad)
    End Function
    Public Function InsertaPersonaUDP(ByVal idEvento As String, ByVal idPersona As String) As String
        Return ObjDataEvento.InsertaPersonaUDP(idEvento, idPersona)
    End Function
    Public Function InsertaAgendaUDP(ByVal idEvento As String, ByVal idAgenda As String) As String
        Return ObjDataEvento.InsertaAgendaUDP(idEvento, idAgenda)
    End Function

    Public Function ActualizaAcuerdo(ByVal idAcuerdo As String, ByVal idEvento As String, ByVal idAgenda As String, ByVal responsable As String, ByVal titulo As String, ByVal descripcion As String) As EAcuerdo
        Return ObjDataEvento.ActualizaAcuerdo(idAcuerdo, idEvento, idAgenda, responsable, titulo, descripcion)
    End Function


    Public Function EliminarAcuerdo(ByVal idAcuerdo As String) As EAcuerdo
        Return ObjDataEvento.EliminarAcuerdo(idAcuerdo)
    End Function
    Public Function EliminarObservacion(ByVal idObservacion As String) As EObservacion
        Return ObjDataEvento.EliminarObservacion(idObservacion)
    End Function


    Public Function ActualizaObservacion(ByVal idObservacion As String, ByVal titulo As String, ByVal descripcion As String) As EObservacion
        Return ObjDataEvento.ActualizaObservacion(idObservacion, titulo, descripcion)
    End Function
    Public Function ReprogramarEvento(ByVal idEvento As String, ByVal fInicio As String, ByVal fFinal As String) As String
        '  Return ObjDataEvento.ReprogramarEvento(idEvento, fInicio, fFinal, 1)
        Return idEvento

    End Function
    Public Function ReprogramarEvento1(ByVal fInicio As String, ByVal fFinal As String, ByVal sEvento As String, ByVal tipo As String, ByVal usuario As String) As String
        Return ObjDataEvento.ReprogramarEvento(sEvento, fInicio, fFinal, tipo, usuario)
    End Function


    Public Function FinalizarEvento(ByVal idEvento As String, ByVal archivoNombre As String, ByVal sObservacion As String) As String
        Return ObjDataEvento.FinalizarEvento(idEvento, archivoNombre, sObservacion)
    End Function

    Public Function MarcaAsistencia(ByVal idEvento As String, ByVal sPersona As String, ByVal sChek As String) As String
        Return ObjDataEvento.MarcaAsistencia(idEvento, sPersona, sChek)
    End Function

    Public Function EliminarAgenda(ByVal idAgenda As String) As String
        Return ObjDataEvento.EliminarAgenda(idAgenda)
    End Function

End Class
