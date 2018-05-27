Imports System.Data
Imports System.Data.SqlClient
Imports Oracle.DataAccess.Client
Imports System.Runtime.InteropServices
Imports System.Configuration
Imports System.Data.Common
Imports sisCalendarioEventos.Entity
'Imports System.Reflection
Imports Microsoft.Practices.EnterpriseLibrary.Data


Public Class DCarga

    Dim Query As String
    Friend Connection As String = ConfigurationManager.ConnectionStrings("DataServer_Oracle_Eventos").ConnectionString
    Friend OracleConnection As New OracleConnection(Connection)
    Friend Eschema As String = ConfigurationManager.ConnectionStrings("Eschema-Eventos").ConnectionString

    Public Function ListarTipoEvento(ByVal tipo As String) As DataSet
        Dim ds As New DataSet()
        Dim db As Database = DatabaseFactory.CreateDatabase("DataServer_Oracle_Eventos")
        Try
            Dim sql As String = Eschema + "SP_PGR_SEL_CARGA"

            Dim cmd As DbCommand = db.GetStoredProcCommand(sql)
            db.AddInParameter(cmd, "P_N_TIPO", DbType.Int32, tipo)
            db.AddInParameter(cmd, "P_V_DATO", DbType.String, "")
            db.AddInParameter(cmd, "P_V_DNI", DbType.String, "")
            Dim CV_1 As New OracleParameter("V_CV1", OracleDbType.RefCursor, ds, ParameterDirection.Output)
            cmd.Parameters.Add(CV_1)
            Return db.ExecuteDataSet(cmd)
        Catch ex As Exception

            Throw New Exception(ex.Message, ex)
        Finally

        End Try

    End Function

    Function CargarArea(tipo As String, idEvento As String) As ECarga
        Dim ECarga As New ECarga
        Dim ECargaArreglo As New List(Of datos)
        Query = Eschema & "SP_PGR_SEL_CARGA"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_TIPO", OracleDbType.Int32, tipo, ParameterDirection.Input)
            .Add("P_V_DATO", OracleDbType.Varchar2, idEvento, ParameterDirection.Input)
            .Add("P_V_DNI", OracleDbType.Varchar2, idEvento, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                Dim ECol As New datos
                ECol.idStr = Reader("ID").ToString
                ECol.descripcion = Reader("DES").ToString
                ECol.sigla = Reader("SIGLA").ToString
                ECargaArreglo.Add(ECol)

            End While
            ECarga.success = "1"
            ECarga.result = ECargaArreglo.ToArray
        Catch Exception As Exception
            ECarga.success = "0"
            ECarga.error_ = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return ECarga
    End Function

    Function ListarUsuarioArea(tipo As String, cadenaArea As String) As EUsuario
        Dim EUsuario As New EUsuario
        Dim ECargaArreglo As New List(Of datosusu)
        Query = Eschema & "SP_PGR_SEL_PERSONAL"
        If OracleConnection.State = ConnectionState.Open Then
            OracleConnection.Close()
        End If
        OracleConnection.Open()

        Dim Command As New OracleCommand(Query, OracleConnection)
        Command.CommandType = CommandType.StoredProcedure
        With Command.Parameters

            .Add("P_N_TIPO", OracleDbType.Int32, tipo, ParameterDirection.Input)
            .Add("P_V_AREAS", OracleDbType.Varchar2, cadenaArea, ParameterDirection.Input)
            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

        End With

        Try
            Dim Reader As OracleDataReader = Command.ExecuteReader
            While Reader.Read
                Dim ECol As New datosusu
                ECol.name = Reader("NOMBRE").ToString + " " + Reader("PAT").ToString +
                    " " + Reader("MAT").ToString + " (" + Reader("DESCAREA").ToString + ")"
                ECol.id = Reader("IDUSU").ToString
                ECol.nombres = Reader("NOMBRE").ToString
                ECol.apepat = Reader("PAT").ToString
                ECol.apemat = Reader("MAT").ToString
                ECol.dni = Reader("DNI").ToString
                ECol.area = Reader("AREA").ToString
                ECol.sigla = Reader("SIGLA").ToString
                ECargaArreglo.Add(ECol)

            End While
            EUsuario.success = "1"
            EUsuario.result = ECargaArreglo.ToArray
        Catch Exception As Exception
            EUsuario.success = "0"
            EUsuario.error_ = Exception.Message
        Finally
            Command.Dispose()
            OracleConnection.Close()
        End Try
        Return EUsuario
    End Function

End Class
