Imports System.Data
Imports System.Data.SqlClient
Imports Oracle.DataAccess.Client
Imports System.Runtime.InteropServices
Imports System.Configuration
Imports System.Data.Common
Imports sisCalendarioEventos.Entity 
Imports Microsoft.Practices.EnterpriseLibrary.Data
Public Class DEvento
    Dim Query As String
    Friend Connection As String = ConfigurationManager.ConnectionStrings("DataServer_Oracle_Eventos").ConnectionString
    Friend OracleConnection As New OracleConnection(Connection)
    Friend Eschema As String = ConfigurationManager.ConnectionStrings("Eschema-Eventos").ConnectionString

    Public Function GuardarEvento(ByVal tipo As String, ByVal fInicio As String, ByVal fFinal As String, ByVal sTipo As String, ByVal sTitle As String, ByVal sBody As String, ByVal estado As String, ByVal user As String) As DataSet
        Dim ds As New DataSet()
        Dim db As Database = DatabaseFactory.CreateDatabase("DataServer_Oracle_Eventos")
        Try
            Dim sql As String = Eschema + "SP_PGR_CRUD_EVENTO"
            Dim datini As Date = Convert.ToDateTime(fInicio)
            Dim datfin As Date = Convert.ToDateTime(fFinal)
            Dim cmd As DbCommand = db.GetStoredProcCommand(sql)
            db.AddInParameter(cmd, "P_N_TIPO", DbType.Int32, tipo)
            db.AddInParameter(cmd, "P_N_TIPOEVENTO", DbType.Int32, sTipo)
            db.AddInParameter(cmd, "P_V_TITULO", DbType.String, sTitle)
            db.AddInParameter(cmd, "P_V_DETALLE", DbType.String, sBody)
            db.AddInParameter(cmd, "P_V_USUARIO", DbType.String, user)
            db.AddInParameter(cmd, "P_D_FECHAINI", DbType.Date, datini)
            db.AddInParameter(cmd, "P_D_FECHAFIN", DbType.Date, datfin)
            db.AddInParameter(cmd, "P_D_ESTADO", DbType.String, estado)
            db.AddInParameter(cmd, "P_V_RUTA", DbType.String, Nothing)
            db.AddInParameter(cmd, "P_V_MOTIVO", DbType.String, Nothing)
            Dim CV_1 As New OracleParameter("V_CV1", OracleDbType.RefCursor, ds, ParameterDirection.Output)
            cmd.Parameters.Add(CV_1) 
            Return db.ExecuteDataSet(cmd)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally

        End Try


    End Function

     
    Function ListarEventos(ByVal usuario As String, ByVal dni As String) As Json
        Dim EEventos As New Json
        Dim EEventosArreglo As New List(Of result)
        Query = Eschema & "SP_PGR_SEL_CARGA"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_TIPO", OracleDbType.Int32, 2, ParameterDirection.Input)
            .Add("P_V_DATO", OracleDbType.Varchar2, usuario, ParameterDirection.Input)
            .Add("P_V_DNI", OracleDbType.Varchar2, dni, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                Dim ECol As New result
                ECol.id = Reader("IDE").ToString
                ECol.start = Reader("INI").ToString
                ECol.[end] = Reader("FIN").ToString
                ECol.title = Reader("TIT").ToString
                ECol.detalle = Reader("DETALLE").ToString
                ECol.estado = Reader("ESTADO").ToString
                ECol.tipo = Reader("N_TEVE_ID").ToString

                ECol.area = Reader("N_AREA_IDAREA").ToString
                ECol.nombrearea = Reader("NOMBAREA").ToString
                ECol.usuario = Reader("USUARIO").ToString
                ECol.ruta = Reader("RUTA").ToString

                '///////////////////agendas
                Dim EAgendaArreglo As New List(Of Agenda)

                Dim Command2 As New OracleCommand(Query, OracleConnection)
                Command2.CommandType = CommandType.StoredProcedure
                With Command2.Parameters

                    .Add("P_N_TIPO", OracleDbType.Int32, 20, ParameterDirection.Input)
                    .Add("P_V_DATO", OracleDbType.Varchar2, ECol.id, ParameterDirection.Input)
                    .Add("P_V_DNI", OracleDbType.Varchar2, dni, ParameterDirection.Input)
                    .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

                End With
                Try
                    Dim Reader2 As OracleDataReader = Command2.ExecuteReader
                    While Reader2.Read
                        Dim EAge As New Agenda
                        EAge.ida = Reader2("IDA").ToString
                        EAge.tituloa = Reader2("TIT").ToString
                        EAge.detallea = Reader2("DETALLE").ToString
                        EAge.estado = "0"
                        EAge.accion = "2"
                        EAge.marca = "si"

                        EAgendaArreglo.Add(EAge)

                    End While
                    ECol.agenda = EAgendaArreglo.ToArray
                Catch ex As Exception
                    EEventos.success = "0"
                    EEventos.error_ = ex.Message

                End Try
                '///////////////////observaciones
                Dim EObservacionArreglo As New List(Of EObservacion)

                Dim Command7 As New OracleCommand(Query, OracleConnection)
                Command7.CommandType = CommandType.StoredProcedure
                With Command7.Parameters

                    .Add("P_N_TIPO", OracleDbType.Int32, 25, ParameterDirection.Input)
                    .Add("P_V_DATO", OracleDbType.Varchar2, ECol.id, ParameterDirection.Input)
                    .Add("P_V_DNI", OracleDbType.Varchar2, dni, ParameterDirection.Input)
                    .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

                End With
                Try
                    Dim Reader7 As OracleDataReader = Command7.ExecuteReader
                    While Reader7.Read
                        Dim EObs As New EObservacion
                        EObs.idob = Reader7("IDO").ToString
                        EObs.titulo = Reader7("TIT").ToString
                        EObs.descripcion = Reader7("DETALLE").ToString
                        EObs.estado = "0"
                        EObs.accion = "2"
                        EObs.marca = "si"

                        EObservacionArreglo.Add(EObs)

                    End While
                    ECol.observaciones = EObservacionArreglo.ToArray
                Catch ex As Exception
                    EEventos.success = "0"
                    EEventos.error_ = ex.Message

                End Try

                '///////////////////personas
                Dim EPersonaArreglo As New List(Of datosusu)

                Dim Command3 As New OracleCommand(Query, OracleConnection)
                Command3.CommandType = CommandType.StoredProcedure
                With Command3.Parameters

                    .Add("P_N_TIPO", OracleDbType.Int32, 21, ParameterDirection.Input)
                    .Add("P_V_DATO", OracleDbType.Varchar2, ECol.id, ParameterDirection.Input)
                    .Add("P_V_DNI", OracleDbType.Varchar2, dni, ParameterDirection.Input)
                    .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

                End With
                Try
                    Dim Reader3 As OracleDataReader = Command3.ExecuteReader
                    While Reader3.Read
                        Dim EPersona As New datosusu

                        EPersona.id = Reader3("IDP").ToString
                        EPersona.dni = Reader3("DNI").ToString
                        EPersona.nombres = Reader3("NOMBRES").ToString
                        EPersona.apepat = Reader3("APEPAT").ToString
                        EPersona.apemat = Reader3("APEMAT").ToString
                        EPersona.area = Reader3("ARDESC").ToString
                        EPersona.sigla = Reader3("AREA").ToString
                        EPersona.participa = Reader3("PART").ToString
                        EPersona.asistencia = Reader3("ASIS").ToString
                        EPersona.name = Reader3("NOMBRES").ToString + " " + Reader3("APEPAT").ToString +
                        " " + Reader3("APEMAT").ToString + " (" + Reader3("AREA").ToString + ")"
                        EPersona.estado = "0"
                        EPersona.accion = "2"
                        EPersona.marca = "si"
                        EPersonaArreglo.Add(EPersona)

                    End While
                    ECol.personas = EPersonaArreglo.ToArray
                Catch ex2 As Exception
                    EEventos.success = "0"
                    EEventos.error_ = ex2.Message

                End Try

                '///////////////////areas
                Dim EAreasArreglo As New List(Of datos)

                Dim Command4 As New OracleCommand(Query, OracleConnection)
                Command4.CommandType = CommandType.StoredProcedure
                With Command4.Parameters

                    .Add("P_N_TIPO", OracleDbType.Int32, 22, ParameterDirection.Input)
                    .Add("P_V_DATO", OracleDbType.Varchar2, ECol.id, ParameterDirection.Input)
                    .Add("P_V_DNI", OracleDbType.Varchar2, dni, ParameterDirection.Input)
                    .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

                End With
                Try
                    Dim Reader4 As OracleDataReader = Command4.ExecuteReader
                    While Reader4.Read
                        Dim EArea As New datos

                        EArea.idStr = Reader4("IDAR").ToString
                        EArea.descripcion = Reader4("DESCR").ToString
                        EArea.sigla = Reader4("SIGLA").ToString
                        EArea.estado = "0"
                        EArea.accion = "2"
                        EArea.marca = "si"
                        EAreasArreglo.Add(EArea)

                    End While
                    ECol.areas = EAreasArreglo.ToArray
                Catch ex3 As Exception
                    EEventos.success = "0"
                    EEventos.error_ = ex3.Message

                End Try

                '///////////////////entidades
                Dim EEntidadesArreglo As New List(Of datos)

                Dim Command5 As New OracleCommand(Query, OracleConnection)
                Command5.CommandType = CommandType.StoredProcedure
                With Command5.Parameters

                    .Add("P_N_TIPO", OracleDbType.Int32, 23, ParameterDirection.Input)
                    .Add("P_V_DATO", OracleDbType.Varchar2, ECol.id, ParameterDirection.Input)
                    .Add("P_V_DNI", OracleDbType.Varchar2, dni, ParameterDirection.Input)
                    .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

                End With
                Try
                    Dim Reader5 As OracleDataReader = Command5.ExecuteReader
                    While Reader5.Read
                        Dim EEntidad As New datos

                        EEntidad.id = Reader5("IDEN").ToString
                        EEntidad.descripcion = Reader5("DESCR").ToString
                        EEntidad.sigla = Reader5("SIGLA").ToString
                        EEntidad.estado = "0"
                        EEntidad.accion = "2"
                        EEntidad.marca = "si"
                        EEntidadesArreglo.Add(EEntidad)

                    End While
                    ECol.entidades = EEntidadesArreglo.ToArray
                Catch ex4 As Exception
                    EEventos.success = "0"
                    EEventos.error_ = ex4.Message

                End Try

                '///////////////////acuerdos
                Dim EAcuerdosArreglo As New List(Of EAcuerdo)

                Dim Command6 As New OracleCommand(Query, OracleConnection)
                Command6.CommandType = CommandType.StoredProcedure
                With Command6.Parameters

                    .Add("P_N_TIPO", OracleDbType.Int32, 24, ParameterDirection.Input)
                    .Add("P_V_DATO", OracleDbType.Varchar2, ECol.id, ParameterDirection.Input)
                    .Add("P_V_DNI", OracleDbType.Varchar2, dni, ParameterDirection.Input)
                    .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

                End With
                Try
                    Dim Reader6 As OracleDataReader = Command6.ExecuteReader
                    While Reader6.Read
                        Dim EAcuerdo As New EAcuerdo

                        EAcuerdo.idac = Reader6("IDAC").ToString
                        EAcuerdo.idag = Reader6("IDAG").ToString
                        EAcuerdo.participante = Reader6("IDUS").ToString
                        EAcuerdo.tituloac = Reader6("TITU").ToString
                        EAcuerdo.descripcion = Reader6("DESCR").ToString
                        EAcuerdo.estado = "0"
                        EAcuerdo.accion = "2"
                        EAcuerdo.marca = "si"
                        EAcuerdo.fechacompromiso = Reader6("FECACU").ToString

                        EAcuerdosArreglo.Add(EAcuerdo)

                    End While
                    ECol.acuerdos = EAcuerdosArreglo.ToArray
                Catch ex5 As Exception
                    EEventos.success = "0"
                    EEventos.error_ = ex5.Message

                End Try

                '///////////////////fin
                EEventosArreglo.Add(ECol)

            End While
            EEventos.success = "1"
            EEventos.result = EEventosArreglo.ToArray
        Catch Exception As Exception
            EEventos.success = "0"
            EEventos.error_ = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try

        'Return CargarAgenda(EEventos)

        Return EEventos
    End Function

   


    Function GuardarNuevoEvento(tipo As String, fInicio As String, fFinal As String, sTipo As String, sOrganiza As String, sTitle As String, sBody As String, estado As String, sUsuario As String) As String
        Dim idEvento As String = ""
        Dim EEventos As New Json
        Dim EEventosArreglo As New List(Of result)
        Query = Eschema & "SP_PGR_INS_EVENTO"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters
            Dim datini As Date = Convert.ToDateTime(fInicio)
            Dim datfin As Date = Convert.ToDateTime(fFinal)
            .Add("P_N_TIPO", OracleDbType.Int32, tipo, ParameterDirection.Input)
            .Add("P_N_TIPOEVENTO", OracleDbType.Int32, sTipo, ParameterDirection.Input)
            .Add("P_N_ORGANIZADOR", OracleDbType.Int32, sOrganiza, ParameterDirection.Input)
            .Add("P_V_TITULO", OracleDbType.Varchar2, sTitle, ParameterDirection.Input)
            .Add("P_V_DETALLE", OracleDbType.Varchar2, sBody, ParameterDirection.Input)
            .Add("P_V_USUARIO", OracleDbType.Varchar2, sUsuario, ParameterDirection.Input)
            .Add("P_D_FECHAINI", OracleDbType.Date, datini, ParameterDirection.Input)
            .Add("P_D_FECHAFIN", OracleDbType.Date, datfin, ParameterDirection.Input)
            .Add("P_D_ESTADO", OracleDbType.Varchar2, estado, ParameterDirection.Input)
            .Add("P_V_RUTA", OracleDbType.Varchar2, Nothing, ParameterDirection.Input)
            .Add("P_V_MOTIVO", OracleDbType.Varchar2, Nothing, ParameterDirection.Input)
            .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                idEvento = Reader("RESUL").ToString
            End While

        Catch Exception As Exception
            idEvento = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return idEvento
    End Function

    Function InsertaArea(idEvento As String, idArea As String) As String
        Dim result As String = ""

        Query = Eschema & "SP_PGR_INS_EVEN_AREA"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If


        Dim area As String = ""

        Dim value As String = idArea.TrimEnd("|")
        Dim delimiter As Char = "|"
        Dim substrings() As String = value.Split(delimiter)
        For Each substring In substrings
            OracleConnection.Open()
            area = substring
            Dim Command As New OracleCommand(Query, OracleConnection)
            Command.CommandType = CommandType.StoredProcedure
            With Command.Parameters

                .Add("P_V_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
                .Add("P_V_AREA", OracleDbType.Varchar2, area, ParameterDirection.Input)
                .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

            End With

            Try
                Dim Reader As OracleDataReader = Command.ExecuteReader
                While Reader.Read
                    result = Reader("RESUL").ToString
                End While
            Catch Exception As Exception
                result = Exception.Message
            Finally
                Command.Dispose()
                OracleConnection.Close()
            End Try

        Next


        Return result
    End Function

    Function InsertaEntidad(idEvento As String, idEntidad As String) As String
        Dim result As String = ""

        Query = Eschema & "SP_PGR_INS_EVEN_ENTIDAD"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If


        Dim entidad As String = ""

        Dim value As String = idEntidad.TrimEnd("|")
        Dim delimiter As Char = "|"
        Dim substrings() As String = value.Split(delimiter)
        For Each substring In substrings
            OracleConnection.Open()
            entidad = substring
            Dim Command As New OracleCommand(Query, OracleConnection)
            Command.CommandType = CommandType.StoredProcedure
            With Command.Parameters

                .Add("P_V_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
                .Add("P_V_ENTI", OracleDbType.Int32, entidad, ParameterDirection.Input)
                .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

            End With

            Try
                Dim Reader As OracleDataReader = Command.ExecuteReader
                While Reader.Read
                    result = Reader("RESUL").ToString
                End While
            Catch Exception As Exception
                result = Exception.Message
            Finally
                Command.Dispose()
                OracleConnection.Close()
            End Try

        Next


        Return result
    End Function

    Function InsertaPersona(idEvento As String, idPersona As String) As String
        Dim result As String = ""

        Query = Eschema & "SP_PGR_INS_EVEN_PERSONA"
        If OracleConnection.State = ConnectionState.Open Then

        End If
        Dim persona As String = ""

        Dim value As String = idPersona.TrimEnd("|")
        Dim delimiter As Char = "|"
        Dim substrings() As String = value.Split(delimiter)
        For Each substring In substrings
            OracleConnection.Open()
            persona = substring.Replace("N", "")

            Dim Command As New OracleCommand(Query, OracleConnection)
            Command.CommandType = CommandType.StoredProcedure
            With Command.Parameters

                .Add("P_V_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
                .Add("P_V_PERS", OracleDbType.Varchar2, persona, ParameterDirection.Input)
                .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

            End With

            Try
                Dim Reader As OracleDataReader = Command.ExecuteReader
                While Reader.Read
                    result = Reader("RESUL").ToString
                End While
            Catch Exception As Exception
                result = Exception.Message
            Finally
                Command.Dispose()
                OracleConnection.Close()
            End Try

        Next

        Return result
    End Function

    Function InsertaAgenda(idEvento As String, idAgenda As String) As String
        Dim result As String = ""

        Query = Eschema & "SP_PGR_INS_AGENDA"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If


        Dim Titulo As String = ""
        Dim Descipcion As String = ""

        Dim value As String = idAgenda.TrimEnd("*")
        Dim delimiter As Char = "*"
        Dim substrings() As String = value.Split(delimiter)
        For Each substring In substrings
            Dim value2 As String = substring
            Dim delimiter2 As Char = "|"
            Dim substrings2() As String = value2.Split(delimiter2)
            Dim contador As Integer = 0

            For Each substring2 In substrings2
                contador = contador + 1
                If contador = 1 Then
                    Titulo = substring2
                Else
                    Descipcion = substring2
                End If

            Next
            OracleConnection.Open()
            Dim Command As New OracleCommand(Query, OracleConnection)
            Command.CommandType = CommandType.StoredProcedure
            With Command.Parameters

                .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
                .Add("P_V_TITU", OracleDbType.Varchar2, Titulo, ParameterDirection.Input)
                .Add("P_V_DESC", OracleDbType.Varchar2, Descipcion, ParameterDirection.Input)
                .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

            End With

            Try
                Dim Reader As OracleDataReader = Command.ExecuteReader
                While Reader.Read
                    result = Reader("RESUL").ToString
                End While
            Catch Exception As Exception
                result = Exception.Message
            Finally
                Command.Dispose()
                OracleConnection.Close()
            End Try
        Next
        Return result
    End Function
     
    Function GuardarObservacion(ByVal idEvento As String, ByVal titulo As String, ByVal descripcion As String) As EObservacion

        Dim EObservacion As New EObservacion
        Query = Eschema & "SP_PGR_INS_OBSERVACION"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input) 
            .Add("P_V_TITU", OracleDbType.Varchar2, titulo, ParameterDirection.Input)
            .Add("P_V_DESC", OracleDbType.Varchar2, descripcion, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                EObservacion.idob = Reader("RESUL").ToString
            End While
            EObservacion.accion = "ok"
        Catch Exception As Exception
            EObservacion.accion = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return EObservacion
    End Function
    Function GuardarAcuerdo(idEvento As String, idAgenda As String, responsable As String, titulo As String, descripcion As String, fFecha As String) As EAcuerdo
        Dim EAcuerdo As New EAcuerdo
        Dim data As Date = Convert.ToDateTime(fFecha)
        Query = Eschema & "SP_PGR_INS_ACUERDO"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
            .Add("P_N_AGEN", OracleDbType.Int32, idAgenda, ParameterDirection.Input)
            .Add("P_V_RESPONSABLE", OracleDbType.Varchar2, responsable, ParameterDirection.Input)
            .Add("P_V_TITU", OracleDbType.Varchar2, titulo, ParameterDirection.Input)
            .Add("P_V_DESC", OracleDbType.Varchar2, descripcion, ParameterDirection.Input)
            .Add("P_D_FECHACOM", OracleDbType.Date, data, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                EAcuerdo.idac = Reader("RESUL").ToString
            End While
            EAcuerdo.accion = "ok"
        Catch Exception As Exception
            EAcuerdo.accion = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return EAcuerdo
    End Function

    Function EliminarAcuerdo(idAcuerdo As String) As EAcuerdo
        Dim EAcuerdo As New EAcuerdo
        Query = Eschema & "SP_PGR_DEL_ACUERDO"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_ACUERDO", OracleDbType.Int32, idAcuerdo, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                EAcuerdo.idac = Reader("RESUL").ToString
            End While
            EAcuerdo.accion = "ok"
        Catch Exception As Exception
            EAcuerdo.accion = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return EAcuerdo
    End Function



    Function ActualizaObservacion(ByVal idObservacion As String, ByVal titulo As String, ByVal descripcion As String) As EObservacion
        Dim EObservacion As New EObservacion
        Query = Eschema & "SP_PGR_UPD_OBSERVACION"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_OBSERVACION", OracleDbType.Int32, idObservacion, ParameterDirection.Input)
            .Add("P_V_TITU", OracleDbType.Varchar2, titulo, ParameterDirection.Input)
            .Add("P_V_DESC", OracleDbType.Varchar2, descripcion, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                EObservacion.idob = Reader("RESUL").ToString
            End While
            EObservacion.accion = "ok"
        Catch Exception As Exception
            EObservacion.accion = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return EObservacion
    End Function

    Function ActualizaAcuerdo(idAcuerdo As String, idEvento As String, idAgenda As String, responsable As String, titulo As String, descripcion As String) As EAcuerdo
        Dim EAcuerdo As New EAcuerdo
        Query = Eschema & "SP_PGR_UPD_ACUERDO"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_ACUERDO", OracleDbType.Int32, idAcuerdo, ParameterDirection.Input)
            .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
            .Add("P_N_AGEN", OracleDbType.Int32, idAgenda, ParameterDirection.Input)
            .Add("P_V_RESPONSABLE", OracleDbType.Varchar2, responsable, ParameterDirection.Input)
            .Add("P_V_TITU", OracleDbType.Varchar2, titulo, ParameterDirection.Input)
            .Add("P_V_DESC", OracleDbType.Varchar2, descripcion, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                EAcuerdo.idac = Reader("RESUL").ToString
            End While
            EAcuerdo.accion = "ok"
        Catch Exception As Exception
            EAcuerdo.accion = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return EAcuerdo

    End Function

    Function EliminarObservacion(idObservacion As String) As EObservacion
        Dim EObservacion As New EObservacion
        Query = Eschema & "SP_PGR_DEL_OBSERVACION"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_ACUERDO", OracleDbType.Int32, idObservacion, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                EObservacion.idob = Reader("RESUL").ToString
            End While
            EObservacion.accion = "ok"
        Catch Exception As Exception
            EObservacion.accion = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return EObservacion
    End Function


    Function InsertaAreaUDP(idEvento As String, idArea As String) As String
        Dim result As String = ""
        Query = Eschema & "SP_PGR_UPD_AREA"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        Dim area As String = ""
        Dim accion As String = ""

        Dim value As String = idArea.TrimEnd("*")
        Dim delimiter As Char = "*"
        Dim substrings() As String = value.Split(delimiter)
        For Each substring In substrings
            Dim value2 As String = substring
            Dim delimiter2 As Char = "|"
            Dim substrings2() As String = value2.Split(delimiter2)
            Dim contador As Integer = 0

            For Each substring2 In substrings2
                contador = contador + 1
                If contador = 1 Then
                    area = substring2
                Else
                    accion = substring2
                End If

            Next
            OracleConnection.Open()
            Dim Command As New OracleCommand(Query, OracleConnection)
            Command.CommandType = CommandType.StoredProcedure
            With Command.Parameters
                .Add("P_N_AREA", OracleDbType.Int32, 0, ParameterDirection.Input)
                .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
                .Add("P_N_ACCION", OracleDbType.Int32, accion, ParameterDirection.Input)
                .Add("P_N_AREASTRING", OracleDbType.Varchar2, area, ParameterDirection.Input)
                .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

            End With

            Try
                Dim Reader As OracleDataReader = Command.ExecuteReader
                While Reader.Read
                    result = Reader("RESUL").ToString
                End While
            Catch Exception As Exception
                result = Exception.Message
            Finally
                Command.Dispose()
                OracleConnection.Close()
            End Try
        Next
        Return result
    End Function

    Function InsertaEntidadUDP(idEvento As String, idEntidad As String) As String
        Dim result As String = ""
        Query = Eschema & "SP_PGR_UPD_ENTIDAD"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        Dim entidad As String = ""
        Dim accion As String = ""

        Dim value As String = idEntidad.TrimEnd("*")
        Dim delimiter As Char = "*"
        Dim substrings() As String = value.Split(delimiter)
        For Each substring In substrings
            Dim value2 As String = substring
            Dim delimiter2 As Char = "|"
            Dim substrings2() As String = value2.Split(delimiter2)
            Dim contador As Integer = 0

            For Each substring2 In substrings2
                contador = contador + 1
                If contador = 1 Then
                    entidad = substring2
                Else
                    accion = substring2
                End If

            Next
            OracleConnection.Open()
            Dim Command As New OracleCommand(Query, OracleConnection)
            Command.CommandType = CommandType.StoredProcedure
            With Command.Parameters
                .Add("P_N_ENTIDAD", OracleDbType.Int32, entidad, ParameterDirection.Input)
                .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
                .Add("P_N_ACCION", OracleDbType.Int32, accion, ParameterDirection.Input)
                .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

            End With

            Try
                Dim Reader As OracleDataReader = Command.ExecuteReader
                While Reader.Read
                    result = Reader("RESUL").ToString
                End While
            Catch Exception As Exception
                result = Exception.Message
            Finally
                Command.Dispose()
                OracleConnection.Close()
            End Try
        Next
        Return result
    End Function

    Function InsertaPersonaUDP(idEvento As String, idPersona As String) As String
        Dim result As String = ""
        Query = Eschema & "SP_PGR_UPD_EVEN_PERSONA"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        Dim persona As String = ""
        Dim accion As String = ""

        Dim value As String = idPersona.TrimEnd("*")
        Dim delimiter As Char = "*"
        Dim substrings() As String = value.Split(delimiter)
        For Each substring In substrings
            Dim value2 As String = substring
            Dim delimiter2 As Char = "|"
            Dim substrings2() As String = value2.Split(delimiter2)
            Dim contador As Integer = 0

            For Each substring2 In substrings2
                contador = contador + 1
                If contador = 1 Then
                    persona = substring2
                Else
                    accion = substring2
                End If

            Next
            OracleConnection.Open()
            Dim Command As New OracleCommand(Query, OracleConnection)
            Command.CommandType = CommandType.StoredProcedure
            With Command.Parameters
                .Add("P_N_PERSONA", OracleDbType.Varchar2, persona, ParameterDirection.Input)
                .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
                .Add("P_N_ACCION", OracleDbType.Int32, accion, ParameterDirection.Input)
                .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

            End With

            Try
                Dim Reader As OracleDataReader = Command.ExecuteReader
                While Reader.Read
                    result = Reader("RESUL").ToString

                End While
            Catch Exception As Exception
                result = Exception.Message
            Finally
                Command.Dispose()
                OracleConnection.Close()
            End Try
        Next
        Return result
    End Function



    Function EliminarAgenda(ByVal idAgenda As String) As String
        Dim result As String = ""
        Query = Eschema & "SP_PGR_UPD_AGENDA"
        If OracleConnection.State = ConnectionState.Open Then
        End If
        Dim persona As String = ""
        OracleConnection.Open()
        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure



        With Command.Parameters
            .Add("P_N_AGENDA", OracleDbType.Int32, idAgenda, ParameterDirection.Input)
            .Add("P_N_EVEN", OracleDbType.Int32, 0, ParameterDirection.Input)
            .Add("P_V_TITU", OracleDbType.Varchar2, "", ParameterDirection.Input)
            .Add("P_V_DESC", OracleDbType.Varchar2, "", ParameterDirection.Input)
            .Add("P_N_ACCION", OracleDbType.Int32, 100, ParameterDirection.Input)
            .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

        End With
        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                result = Reader("RESUL").ToString
            End While
        Catch Exception As Exception
            result = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return result
    End Function
    Function InsertaAgendaUDP(idEvento As String, idAgenda As String) As String
        Dim result As String = ""
        Query = Eschema & "SP_PGR_UPD_AGENDA"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        Dim agenda As String = ""
        Dim titulo As String = ""
        Dim descripcion As String = ""
        Dim accion As String = ""

        Dim value As String = idAgenda.TrimEnd("*")
        Dim delimiter As Char = "*"
        Dim substrings() As String = value.Split(delimiter)
        For Each substring In substrings
            Dim value2 As String = substring
            Dim delimiter2 As Char = "|"
            Dim substrings2() As String = value2.Split(delimiter2)
            Dim contador As Integer = 0

            For Each substring2 In substrings2
                contador = contador + 1
                If contador = 1 Then
                    agenda = substring2
                End If
                If contador = 2 Then
                    descripcion = substring2
                End If
                If contador = 3 Then
                    titulo = substring2
                End If
                If contador = 4 Then
                    accion = substring2
                End If

            Next
            OracleConnection.Open()
            Dim Command As New OracleCommand(Query, OracleConnection)
            Command.CommandType = CommandType.StoredProcedure
            With Command.Parameters
                .Add("P_N_AGENDA", OracleDbType.Int32, agenda, ParameterDirection.Input)
                .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
                .Add("P_V_TITU", OracleDbType.Varchar2, titulo, ParameterDirection.Input)
                .Add("P_V_DESC", OracleDbType.Varchar2, descripcion, ParameterDirection.Input)
                .Add("P_N_ACCION", OracleDbType.Int32, accion, ParameterDirection.Input)
                .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

            End With

            Try
                Dim Reader As OracleDataReader = Command.ExecuteReader
                While Reader.Read
                    result = Reader("RESUL").ToString
                End While
            Catch Exception As Exception
                result = Exception.Message
            Finally
                Command.Dispose()
                OracleConnection.Close()
            End Try
        Next
        Return result
    End Function

    Function ReprogramarEvento(idEvento As String, fInicio As String, fFinal As String, tipo As String, ByVal usuario As String) As String
        Dim result As String = ""
        Dim datini As Date = Convert.ToDateTime(fInicio)
        Dim datfin As Date = Convert.ToDateTime(fFinal)
        Query = Eschema & "SP_PGR_UDP_EVEN_REPROGRAMAR"
        If OracleConnection.State = ConnectionState.Open Then
        End If
        Dim persona As String = ""
        OracleConnection.Open()
        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure



        With Command.Parameters
            .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
            .Add("P_D_FECI", OracleDbType.Date, datini, ParameterDirection.Input)
            .Add("P_D_FECF", OracleDbType.Date, datfin, ParameterDirection.Input)
            .Add("P_V_TIPO", OracleDbType.Int32, tipo, ParameterDirection.Input)
            .Add("P_V_USUARIO", OracleDbType.Varchar2, usuario, ParameterDirection.Input)
            .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

        End With
        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                result = Reader("RESUL").ToString
            End While
        Catch Exception As Exception
            result = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return result
    End Function

    Function FinalizarEvento(idEvento As String, ByVal archivoNombre As String, sObservacion As String) As String
        Dim result As String = ""
        Query = Eschema & "SP_PGR_UDP_EVEN_FINALIZAR"
        If OracleConnection.State = ConnectionState.Open Then
        End If
        Dim persona As String = ""
        OracleConnection.Open()
        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters
            .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
            .Add("P_V_OBSERVACION", OracleDbType.Varchar2, sObservacion, ParameterDirection.Input)
            .Add("P_V_NOMBREARCHIVO", OracleDbType.Varchar2, archivoNombre, ParameterDirection.Input)
            .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

        End With
        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                result = Reader("RESUL").ToString
            End While
        Catch Exception As Exception
            result = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try

        Return result
    End Function

    Function MarcaAsistencia(idEvento As String, sPersona As String, ByVal sChek As String) As String
        Dim result As String = ""
        Query = Eschema & "SP_PGR_UDP_EVEN_ASISTENCIA"
        If OracleConnection.State = ConnectionState.Open Then
        End If
        Dim persona As String = ""
        OracleConnection.Open()
        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters
            .Add("P_N_EVEN", OracleDbType.Int32, idEvento, ParameterDirection.Input)
            .Add("P_V_DNI", OracleDbType.Varchar2, sPersona, ParameterDirection.Input)
            .Add("P_V_CHECK", OracleDbType.Varchar2, sChek, ParameterDirection.Input)
            .Add("V_CV1", OracleDbType.RefCursor, ParameterDirection.Output)

        End With
        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                result = Reader("RESUL").ToString
            End While
        Catch Exception As Exception
            result = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try

        Return result
    End Function

    

End Class
