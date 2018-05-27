Imports sisCalendarioEventos.Entity
Imports System.Configuration
Imports System.IO
Imports System.Text

 Imports Oracle.DataAccess.Client
Imports OfficeOpenXml
Imports Newtonsoft.Json

''' <summary>
''' Creado por: Fabián Pérez Vásquez  (11/03/2016)
''' </summary>

Public Class DMensaje

    ' ''' <summary>
    ' ''' Variable enviada como consulta a la base de datos a través de un Comando (OracleCommand | SqlCommand)
    ' ''' </summary>
    'Dim Query As String

    ' ''' <summary>
    ' ''' Separador de valores para SQL (Todas las bases de datos) [,]
    ' ''' </summary>
    'Friend SeparadorSql As String = ConfigurationManager.AppSettings("SeparadorSql")
    ' ''' <summary>
    ' ''' Separador de texto [;]
    ' ''' </summary>
    'Friend SeparadorTexto As String = ConfigurationManager.AppSettings("SeparadorTexto")
    ' ''' <summary>
    ' ''' Comilla simple para concatenar con una cadena enviada a la base de datos [']
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Friend StringSql As String = ConfigurationManager.AppSettings("StringSql")
    ' ''' <summary>
    ' ''' Códigos de regiones finales consolidados con la tabla de regiones
    ' ''' [00;01;02;03;04;05;06;07;08;09;10;11;12;13;14;16;17;18;19;20;21;22;23;24;25;27;28]
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Friend Regiones As String() = ConfigurationManager.AppSettings("Regiones").Split(SeparadorTexto)

    ' ''' <summary>
    ' ''' Cadena de conexión a Oracle (BD SALASITUACIONAL)
    ' ''' </summary>
    'Friend Connection As String = ConfigurationManager.ConnectionStrings("OracleConnection-CalendarioEventos").ConnectionString
    ' ''' <summary>
    ' ''' Esquema de conexión a Oracle para ejecutar procedimiento (BD SALASITUACIONAL)
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Friend Eschema As String = ConfigurationManager.ConnectionStrings("Eschema-CalendarioEvento").ConnectionString
    ' ''' <summary>
    ' ''' Conexión a Oracle instanciada a través de la conexión capturada previamente (BD SALASITUACIONAL)
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Friend OracleConnection As New OracleConnection(Connection)

    'Public Function GetPrueba() As List(Of EObjeto)
    '    Dim lMapa As New List(Of EObjeto)

    '    Query = Eschema & ".PR_SS_SEL_SINPARAMETROS"

    '    If OracleConnection.State = ConnectionState.Open Then
    '        OracleConnection.Close()
    '    End If
    '    OracleConnection.Open()

    '    Dim Command As New OracleCommand(Query, OracleConnection)
    '    Command.CommandType = CommandType.StoredProcedure
    '    With Command.Parameters
    '        .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
    '        .Add("V_INDICADOR", OracleDbType.Varchar2, "VW_SS_SIS_MAPAREGIONAL", ParameterDirection.Input)
    '    End With

    '    Try
    '        Dim Reader As OracleDataReader = Command.ExecuteReader
    '        While Reader.Read
    '            Dim objMapa As New EObjeto
    '            'objMapa.Periodo = CInt(Reader("PERIODO").ToString)
    '            'objMapa.Mes = Reader("MES").ToString
    '            'objMapa.Key = Reader("KEYMAPA").ToString
    '            'objMapa.Region = Reader("REGION").ToString
    '            'objMapa.Asegurados = Reader("ASEGURADOS").ToString
    '            'objMapa.CantidadAsegurados = CInt(Reader("ASEG_VALUE").ToString)
    '            'objMapa.Atendidos = Reader("ATENDIDOS").ToString
    '            'objMapa.CantidadAtendidos = CInt(Reader("ATEND_VALUE").ToString)
    '            'objMapa.Atenciones = Reader("ATENCIONES").ToString
    '            'objMapa.CantidadAtenciones = CInt(Reader("ATENC_VALUE").ToString)
    '            'objMapa.Poblacion = Reader("POBLACION").ToString
    '            'objMapa.CantidadPoblacion = CInt(Reader("POBL_VALUE").ToString)
    '            'objMapa.Maseg = Reader("MASEG").ToString
    '            'objMapa.CantidadMaseg = Reader("MASEG_VALUE").ToString
    '            'objMapa.FechaActualizacion = Reader("ACTUALIZACION").ToString
    '            lMapa.Add(objMapa)
    '        End While
    '    Catch Exception As Exception
    '        Dim objMapa As New EObjeto
    '        objMapa.ErrorMessage = Exception.Message
    '        lMapa.Add(objMapa)
    '    Finally
    '        Command.Dispose()
    '        OracleConnection.Close()
    '    End Try

    '    Return lMapa
    'End Function

    ''    Public Function IndicadoresCapita() As List(Of EIndicadorCapita)
    ''        Dim lIndicadores As New List(Of EIndicadorCapita)

    ''        Query = Eschema & ".PR_SS_GREP_SEL_SINPARAMETROS"

    ''        If OracleConnection.State = ConnectionState.Open Then
    ''            OracleConnection.Close()
    ''        End If
    ''        OracleConnection.Open()

    ''        Dim Command As New OracleCommand(Query, OracleConnection)
    ''        Command.CommandType = CommandType.StoredProcedure
    ''        With Command.Parameters
    ''            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
    ''            .Add("V_INDICADOR", OracleDbType.Varchar2, "VW_SS_GREP_INDICAPITA", ParameterDirection.Input)
    ''        End With

    ''        Try
    ''            Dim Reader As OracleDataReader = Command.ExecuteReader
    ''            While Reader.Read
    ''                Dim objIndicador As New EIndicadorCapita
    ''                objIndicador.Key = Reader("KEY").ToString
    ''                objIndicador.Indicador = CDbl(Reader("INDICADOR").ToString)
    ''                objIndicador.Cien = CInt(Reader("100").ToString)
    ''                objIndicador.NoventaOchenta = CInt(Reader("8099").ToString)
    ''                objIndicador.SetentaCincuenta = CInt(Reader("5079").ToString)
    ''                objIndicador.CincuentaMenos = CInt(Reader("50MENOS").ToString)
    ''                objIndicador.FechaCorte = Reader("CORTE").ToString
    ''                objIndicador.FechaCreacion = Reader("CREACION").ToString
    ''                lIndicadores.Add(objIndicador)
    ''            End While
    ''        Catch Exception As Exception
    ''            Dim objIndicador As New EIndicadorCapita
    ''            objIndicador.ErrorMessage = Exception.Message
    ''            lIndicadores.Add(objIndicador)
    ''        Finally
    ''            Command.Dispose()
    ''            OracleConnection.Close()
    ''        End Try

    ''        Return lIndicadores
    ''    End Function

    ''    Public Function IndicadoresPreLiquidados() As List(Of EPreLiquidado)
    ''        Dim lIndicadores As New List(Of EPreLiquidado)
    ''        Dim objPreLiquidadoInd5 As New EPreLiquidado
    ''        objPreLiquidadoInd5.Key = ""
    ''        Dim objPreLiquidadoInd6 As New EPreLiquidado
    ''        objPreLiquidadoInd6.Key = ""
    ''        Dim Den As Decimal = 0.0
    ''        Dim Num As Decimal = 0.0
    ''        Query = Eschema & ".PR_SS_GREP_SEL_SINPARAMETROS"

    ''        If OracleConnection.State = ConnectionState.Open Then
    ''            OracleConnection.Close()
    ''        End If
    ''        OracleConnection.Open()

    ''        Dim Command As New OracleCommand(Query, OracleConnection)
    ''        Command.CommandType = CommandType.StoredProcedure
    ''        With Command.Parameters
    ''            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
    ''            .Add("V_INDICADOR", OracleDbType.Varchar2, "VW_SS_GREP_ATEPRELIQUIDADO", ParameterDirection.Input)
    ''        End With

    ''        Try
    ''            Dim Reader As OracleDataReader = Command.ExecuteReader
    ''            While Reader.Read
    ''                Dim objPreLiquidado As New EPreLiquidado
    ''                objPreLiquidado.Periodo = CInt(Reader("PERIODO").ToString)
    ''                objPreLiquidado.Mes = Reader("MES").ToString
    ''                objPreLiquidado.Key = Reader("KEY").ToString
    ''                objPreLiquidado.IdIndicador = CInt(Reader("ID_INDICADOR").ToString)
    ''                objPreLiquidado.Indicador = Reader("INDICADOR").ToString
    ''                objPreLiquidado.Atenciones = CDec(Reader("ATENCIONES").ToString)
    ''                objPreLiquidado.FechaActualizacion = Reader("ACTUALIZACION").ToString

    ''                If objPreLiquidado.IdIndicador = 561 Or objPreLiquidado.IdIndicador = 562 Then
    ''                    If objPreLiquidadoInd5.Key.Equals("") Then
    ''                        objPreLiquidadoInd5.Key = objPreLiquidado.Key
    ''                        objPreLiquidadoInd5.Periodo = objPreLiquidado.Periodo
    ''                        objPreLiquidadoInd5.IdIndicador = 5
    ''                        objPreLiquidadoInd5.Indicador = "I5: Nro. de Partos"
    ''                        objPreLiquidadoInd5.Atenciones += objPreLiquidado.Atenciones
    ''                        objPreLiquidadoInd5.FechaActualizacion = objPreLiquidado.FechaActualizacion
    ''                    Else
    ''                        If objPreLiquidadoInd5.Key.Equals(objPreLiquidado.Key) Then
    ''                            objPreLiquidadoInd5.Atenciones += objPreLiquidado.Atenciones
    ''                        Else
    ''                            lIndicadores.Add(objPreLiquidadoInd5)
    ''                            objPreLiquidadoInd5 = New EPreLiquidado
    ''                            objPreLiquidadoInd5.Key = objPreLiquidado.Key
    ''                            objPreLiquidadoInd5.Periodo = objPreLiquidado.Periodo
    ''                            objPreLiquidadoInd5.IdIndicador = 5
    ''                            objPreLiquidadoInd5.Indicador = "I5: Nro. de Partos"
    ''                            objPreLiquidadoInd5.Atenciones += objPreLiquidado.Atenciones
    ''                            objPreLiquidadoInd5.FechaActualizacion = objPreLiquidado.FechaActualizacion
    ''                        End If
    ''                    End If
    ''                    If objPreLiquidadoInd6.Key.Equals("") Then
    ''                        objPreLiquidadoInd6.Key = objPreLiquidado.Key
    ''                        objPreLiquidadoInd6.Periodo = objPreLiquidado.Periodo
    ''                        objPreLiquidadoInd6.IdIndicador = 6
    ''                        objPreLiquidadoInd6.Indicador = "I6: Porcentaje de Cesareas"
    ''                        If objPreLiquidado.IdIndicador = 562 Then
    ''                            Num += objPreLiquidado.Atenciones
    ''                            Den += objPreLiquidado.Atenciones
    ''                        ElseIf objPreLiquidado.IdIndicador = 561 Then
    ''                            Den += objPreLiquidado.Atenciones
    ''                        End If
    ''                    Else
    ''                        If objPreLiquidadoInd6.Key.Equals(objPreLiquidado.Key) Then
    ''                            If objPreLiquidado.IdIndicador = 562 Then
    ''                                Num += objPreLiquidado.Atenciones
    ''                                Den += objPreLiquidado.Atenciones
    ''                            ElseIf objPreLiquidado.IdIndicador = 561 Then
    ''                                Den += objPreLiquidado.Atenciones
    ''                            End If
    ''                        Else
    ''                            Dim result As Decimal = 0.0
    ''                            result = Num / Den * 100
    ''                            objPreLiquidadoInd6.Atenciones = Math.Round(result, 2)
    ''                            lIndicadores.Add(objPreLiquidadoInd6)
    ''                            objPreLiquidadoInd6 = New EPreLiquidado
    ''                            Num = 0.0
    ''                            Den = 0.0
    ''                            objPreLiquidadoInd6.Key = objPreLiquidado.Key
    ''                            objPreLiquidadoInd6.Periodo = objPreLiquidado.Periodo
    ''                            objPreLiquidadoInd6.IdIndicador = 6
    ''                            objPreLiquidadoInd6.Indicador = "I6: Porcentaje de Cesareas"
    ''                            If objPreLiquidado.IdIndicador = 562 Then
    ''                                Num += objPreLiquidado.Atenciones
    ''                                Den += objPreLiquidado.Atenciones
    ''                            ElseIf objPreLiquidado.IdIndicador = 561 Then
    ''                                Den += objPreLiquidado.Atenciones
    ''                            End If
    ''                        End If
    ''                    End If
    ''                Else
    ''                    lIndicadores.Add(objPreLiquidado)
    ''                End If
    ''            End While
    ''            If Not objPreLiquidadoInd5.Key.Equals("") Then
    ''                lIndicadores.Add(objPreLiquidadoInd5)
    ''            End If
    ''            If Not objPreLiquidadoInd6.Key.Equals("") Then
    ''                Dim result As Decimal = 0.0
    ''                result = Num / Den * 100
    ''                objPreLiquidadoInd6.Atenciones = Math.Round(result, 2)
    ''                lIndicadores.Add(objPreLiquidadoInd6)
    ''            End If
    ''        Catch Exception As Exception
    ''            Dim objPreLiquidado As New EPreLiquidado
    ''            objPreLiquidado.ErrorMessage = Exception.Message
    ''            lIndicadores.Add(objPreLiquidado)
    ''        Finally
    ''            Command.Dispose()
    ''            OracleConnection.Close()
    ''        End Try
    ''        Return lIndicadores
    ''    End Function

    ''    Public Function AtencionesPorEmergencia() As List(Of EAtencionPorEmergencia)
    ''        Dim lAtenciones As New List(Of EAtencionPorEmergencia)

    ''        Query = Eschema & ".PR_SS_GREP_SEL_SINPARAMETROS"

    ''        If OracleConnection.State = ConnectionState.Open Then
    ''            OracleConnection.Close()
    ''        End If
    ''        OracleConnection.Open()

    ''        Dim Command As New OracleCommand(Query, OracleConnection)
    ''        Command.CommandType = CommandType.StoredProcedure
    ''        With Command.Parameters
    ''            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
    ''            .Add("V_INDICADOR", OracleDbType.Varchar2, "VW_SS_GREP_ATENPOREMER", ParameterDirection.Input)
    ''        End With

    ''        Try
    ''            Dim Reader As OracleDataReader = Command.ExecuteReader
    ''            While Reader.Read
    ''                Dim objAtencion As New EAtencionPorEmergencia
    ''                objAtencion.Key = Reader("KEY").ToString
    ''                objAtencion.Total = CInt(Reader("TOTAL").ToString)
    ''                objAtencion.FechaCorte = Reader("CORTE").ToString
    ''                objAtencion.FechaCreacion = Reader("CREACION").ToString
    ''                lAtenciones.Add(objAtencion)
    ''            End While
    ''        Catch Exception As Exception
    ''            Dim objAtencion As New EAtencionPorEmergencia
    ''            objAtencion.ErrorMessage = Exception.Message
    ''            lAtenciones.Add(objAtencion)
    ''        Finally
    ''            Command.Dispose()
    ''            OracleConnection.Close()
    ''        End Try

    ''        Return lAtenciones
    ''    End Function

    ''    Public Function IntercambioPrestacional() As List(Of EIntercambioPrestacional)
    ''        Dim lIntercambios As New List(Of EIntercambioPrestacional)

    ''        Query = Eschema & ".PR_SS_GREP_SEL_SINPARAMETROS"

    ''        If OracleConnection.State = ConnectionState.Open Then
    ''            OracleConnection.Close()
    ''        End If
    ''        OracleConnection.Open()

    ''        Dim Command As New OracleCommand(Query, OracleConnection)
    ''        Command.CommandType = CommandType.StoredProcedure
    ''        With Command.Parameters
    ''            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
    ''            .Add("V_INDICADOR", OracleDbType.Varchar2, "VW_SS_GREP_INTERPRESTACIONAL", ParameterDirection.Input)
    ''        End With

    ''        Try
    ''            Dim Reader As OracleDataReader = Command.ExecuteReader
    ''            While Reader.Read
    ''                Dim objAtencion As New EIntercambioPrestacional
    ''                objAtencion.Key = Reader("KEY").ToString
    ''                objAtencion.Periodo = CInt(Reader("PERIODO").ToString)
    ''                objAtencion.Atendidos = CInt(Reader("ATENDIDOS").ToString)
    ''                objAtencion.Atenciones = CInt(Reader("ATENCIONES").ToString)
    ''                objAtencion.FechaCorte = Reader("CORTE").ToString
    ''                objAtencion.FechaCreacion = Reader("CREACION").ToString
    ''                lIntercambios.Add(objAtencion)
    ''            End While
    ''        Catch Exception As Exception
    ''            Dim objAtencion As New EIntercambioPrestacional
    ''            objAtencion.ErrorMessage = Exception.Message
    ''            lIntercambios.Add(objAtencion)
    ''        Finally
    ''            Command.Dispose()
    ''            OracleConnection.Close()
    ''        End Try

    ''        Return lIntercambios
    ''    End Function

    ''    Public Function ProblNutr() As List(Of EProblNutricional)
    ''        Dim lProblNutr As New List(Of EProblNutricional)

    ''        Query = Eschema & ".PR_SS_GREP_SEL_SINPARAMETROS"

    ''        If OracleConnection.State = ConnectionState.Open Then
    ''            OracleConnection.Close()
    ''        End If
    ''        OracleConnection.Open()

    ''        Dim Command As New OracleCommand(Query, OracleConnection)
    ''        Command.CommandType = CommandType.StoredProcedure
    ''        With Command.Parameters
    ''            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
    ''            .Add("V_INDICADOR", OracleDbType.Varchar2, "VW_SS_GREP_PSE_PRO_NUT", ParameterDirection.Input)
    ''        End With

    ''        Try
    ''            Dim Reader As OracleDataReader = Command.ExecuteReader
    ''            While Reader.Read
    ''                Dim objProbNutricional As New EProblNutricional
    ''                objProbNutricional.Periodo = CInt(Reader("PERIODO").ToString)
    ''                objProbNutricional.Mes = Reader("MES").ToString
    ''                objProbNutricional.Key = Reader("KEY").ToString
    ''                objProbNutricional.Tamizados = Reader("ALCANZADO").ToString
    ''                objProbNutricional.FechaActualizacion = Reader("CORTE").ToString
    ''                lProblNutr.Add(objProbNutricional)
    ''            End While
    ''        Catch Exception As Exception
    ''            Dim objProbNutricional As New EProblNutricional
    ''            objProbNutricional.ErrorMessage = Exception.Message
    ''            lProblNutr.Add(objProbNutricional)
    ''        Finally
    ''            Command.Dispose()
    ''            OracleConnection.Close()
    ''        End Try

    ''        Return lProblNutr
    ''    End Function

    ''    Public Function TamizadosConAnemia() As List(Of ETamizadoConAnemia)
    ''        Dim lTamizados As New List(Of ETamizadoConAnemia)

    ''        Query = Eschema & ".PR_SS_GREP_SEL_SINPARAMETROS"

    ''        If OracleConnection.State = ConnectionState.Open Then
    ''            OracleConnection.Close()
    ''        End If
    ''        OracleConnection.Open()

    ''        Dim Command As New OracleCommand(Query, OracleConnection)
    ''        Command.CommandType = CommandType.StoredProcedure
    ''        With Command.Parameters
    ''            .Add("SS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
    ''            .Add("V_INDICADOR", OracleDbType.Varchar2, "VW_SS_GREP_TAMIZANEMIA", ParameterDirection.Input)
    ''        End With

    ''        Try
    ''            Dim Reader As OracleDataReader = Command.ExecuteReader
    ''            While Reader.Read
    ''                Dim objTamizado As New ETamizadoConAnemia
    ''                objTamizado.Key = Reader("KEY").ToString
    ''                objTamizado.Grave = CInt(Reader("GRAVE").ToString)
    ''                objTamizado.Leve = CInt(Reader("LEVE").ToString)
    ''                objTamizado.Moderada = CInt(Reader("MODERADA").ToString)
    ''                objTamizado.SinAnemia = CInt(Reader("SINANEMIA").ToString)
    ''                objTamizado.Tamizados = CInt(Reader("TAMIZADOS").ToString)
    ''                objTamizado.FechaCorte = Reader("CORTE").ToString
    ''                objTamizado.FechaCreacion = Reader("CREACION").ToString
    ''                lTamizados.Add(objTamizado)
    ''            End While
    ''        Catch Exception As Exception
    ''            Dim objTamizado As New ETamizadoConAnemia
    ''            objTamizado.ErrorMessage = Exception.Message
    ''            lTamizados.Add(objTamizado)
    ''        Finally
    ''            Command.Dispose()
    ''            OracleConnection.Close()
    ''        End Try

    ''        Return lTamizados
    ''    End Function



    ''#Region "Carga de información"

    ''    Public Function ValidarIndicadoresCapita(pFile As String) As String
    ''        Dim lReporte As New List(Of EMensaje) 'Lista de reportes de errores
    ''        Dim objMensaje As New EMensaje 'Mensaje cambiante según el rumbo de la validación

    ''        Dim isErrorColumn = False
    ''        Dim isErrorData = False

    ''        Dim ExcelFile As New FileInfo(pFile)
    ''        Dim WorkBook = New ExcelPackage(ExcelFile).Workbook

    ''        If WorkBook.Worksheets.Count > 0 Then
    ''            Dim DatosGenerales = WorkBook.Worksheets.First
    ''            Dim iColumns = DatosGenerales.Dimension.End.Column
    ''            Dim iRows = DatosGenerales.Dimension.End.Row

    ''            'Si el número columnas dentro del archivo no es el mismo a la plantilla otorgada al usuario
    ''            If iColumns <> 7 Then
    ''                objMensaje = New EMensaje
    ''                objMensaje.Title = "N&uacute;mero de Columnas Inv&aacute;lido"
    ''                objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                objMensaje.Type = EMensaje.ErrorMessage
    ''                objMensaje.IdErrorMessage = EMensaje.NroColumnasInvalido
    ''                objMensaje.UserMessage = "N&uacute;mero de columnas esperadas [6]; columnas obtenidas del archivo [" & (iColumns - 1) & "]."
    ''                objMensaje.ServerMessage = "Número de columnas inválido, el archivo posee [" & iColumns & "] columnas."
    ''                RegistrarReporte(objMensaje, EIndicadorCapita.Equivalencia)
    ''                Return JsonConvert.SerializeObject(objMensaje)
    ''            End If

    ''            If Not DatosGenerales.Cells(2, 2).Text.Equals("COD. REGIÓN") Then 'Primera columna
    ''                Dim Column = DatosGenerales.Cells(2, 2).Text
    ''                isErrorColumn = True
    ''                objMensaje = New EMensaje
    ''                objMensaje.Title = "Columna No Especificada"
    ''                objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                objMensaje.Type = EMensaje.ErrorMessage
    ''                objMensaje.IdErrorMessage = EMensaje.ColumnaNoEspecificada
    ''                objMensaje.UserMessage = "Se esperaba el nombre de columna [COD. REGIÓN] en la columna [2], fila [2]; el nombre actual es [" & Column & "]."
    ''                objMensaje.ServerMessage = "Se esperaba el nombre de columna [COD. REGIÓN] en la columna [2], fila [2]; el nombre actual es [" & Column & "]."
    ''                lReporte.Add(objMensaje)
    ''            End If

    ''            If Not DatosGenerales.Cells(2, 3).Text.Equals("INDICADOR") Then 'Segunda columna
    ''                Dim Column = DatosGenerales.Cells(2, 4).Text
    ''                isErrorColumn = True
    ''                objMensaje = New EMensaje
    ''                objMensaje.Title = "Columna No Especificada"
    ''                objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                objMensaje.Type = EMensaje.ErrorMessage
    ''                objMensaje.IdErrorMessage = EMensaje.ColumnaNoEspecificada
    ''                objMensaje.UserMessage = "Se esperaba el nombre de columna [INDICADOR] en la columna [3], fila [2]; el nombre actual es [" & Column & "]."
    ''                objMensaje.ServerMessage = "Se esperaba el nombre de columna [INDICADOR] en la columna [3], fila [2]; el nombre actual es [" & Column & "]."
    ''                lReporte.Add(objMensaje)
    ''            End If

    ''            If Not DatosGenerales.Cells(2, 4).Text.Equals("100%") Then 'Tercera columna
    ''                Dim Column = DatosGenerales.Cells(2, 4).Text
    ''                isErrorColumn = True
    ''                objMensaje = New EMensaje
    ''                objMensaje.Title = "Columna No Especificada"
    ''                objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                objMensaje.Type = EMensaje.ErrorMessage
    ''                objMensaje.IdErrorMessage = EMensaje.ColumnaNoEspecificada
    ''                objMensaje.UserMessage = "Se esperaba el nombre de columna [100%] en la columna [4], fila [2]; el nombre actual es [" & Column & "]."
    ''                objMensaje.ServerMessage = "Se esperaba el nombre de columna [100%] en la columna [4], fila [2]; el nombre actual es [" & Column & "]."
    ''                lReporte.Add(objMensaje)
    ''            End If

    ''            If Not DatosGenerales.Cells(2, 5).Text.Equals("80 - 99%") Then 'Cuarta columna
    ''                Dim Column = DatosGenerales.Cells(2, 5).Text
    ''                isErrorColumn = True
    ''                objMensaje = New EMensaje
    ''                objMensaje.Title = "Columna No Especificada"
    ''                objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                objMensaje.Type = EMensaje.ErrorMessage
    ''                objMensaje.IdErrorMessage = EMensaje.ColumnaNoEspecificada
    ''                objMensaje.UserMessage = "Se esperaba el nombre de columna [80 - 99%] en la columna [5], fila [2]; el nombre actual es [" & Column & "]."
    ''                objMensaje.ServerMessage = "Se esperaba el nombre de columna [80 - 99%] en la columna [5], fila [2]; el nombre actual es [" & Column & "]."
    ''                lReporte.Add(objMensaje)
    ''            End If

    ''            If Not DatosGenerales.Cells(2, 6).Text.Equals("50 - 79%") Then 'Quinta columna
    ''                Dim Column = DatosGenerales.Cells(2, 6).Text
    ''                isErrorColumn = True
    ''                objMensaje = New EMensaje
    ''                objMensaje.Title = "Columna No Especificada"
    ''                objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                objMensaje.Type = EMensaje.ErrorMessage
    ''                objMensaje.IdErrorMessage = EMensaje.ColumnaNoEspecificada
    ''                objMensaje.UserMessage = "Se esperaba el nombre de columna [50 - 79%] en la columna [6], fila [2]; el nombre actual es [" & Column & "]."
    ''                objMensaje.ServerMessage = "Se esperaba el nombre de columna [50 - 79%] en la columna [6], fila [2]; el nombre actual es [" & Column & "]."
    ''                lReporte.Add(objMensaje)
    ''            End If

    ''            If Not DatosGenerales.Cells(2, 7).Text.Equals("< 50%") Then 'Sexta columna
    ''                Dim Column = DatosGenerales.Cells(2, 7).Text
    ''                isErrorColumn = True
    ''                objMensaje = New EMensaje
    ''                objMensaje.Title = "Columna No Especificada"
    ''                objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                objMensaje.Type = EMensaje.ErrorMessage
    ''                objMensaje.IdErrorMessage = EMensaje.ColumnaNoEspecificada
    ''                objMensaje.UserMessage = "Se esperaba el nombre de columna [< 50%] en la columna [7], fila [2]; el nombre actual es [" & Column & "]."
    ''                objMensaje.ServerMessage = "Se esperaba el nombre de columna [< 50%] en la columna [7], fila [2]; el nombre actual es [" & Column & "]."
    ''                lReporte.Add(objMensaje)
    ''            End If

    ''            Dim bCorrecto = True

    ''            If lReporte.Count = 0 Then
    ''                For Row = 3 To iRows 'Comienza desde 3, por que las dos primeras filas son el título y cabeceras
    ''                    Dim objIndicadorCapita As New EIndicadorCapita 'Objeto que se irá agregando a la lista de información a insertar
    ''                    Dim iContador = 1 'Identificador de que columna se tiene que insertar según el orden de lectura
    ''                    For Column = 2 To iColumns
    ''                        Dim Val = DatosGenerales.Cells(Row, Column).Text
    ''                        Select Case iContador
    ''                            Case 1 'Código de Región
    ''                                If Val Is Nothing Or Val.Equals("") Then
    ''                                    bCorrecto = False
    ''                                    Exit For
    ''                                End If
    ''                                Dim Exists = False
    ''                                For Each Region In Regiones
    ''                                    If Region.Equals(Val) Then
    ''                                        Exists = True
    ''                                        Exit For
    ''                                    End If
    ''                                Next
    ''                                If Not Exists Then
    ''                                    isErrorData = True
    ''                                    objMensaje = New EMensaje
    ''                                    objMensaje.Title = "C&oacute;digo de Regi&oacute;n Inv&aacute;lido"
    ''                                    objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                                    objMensaje.Type = EMensaje.ErrorMessage
    ''                                    objMensaje.IdErrorMessage = EMensaje.RegionInvalida
    ''                                    objMensaje.UserMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "No es un c&oacute;digo de regi&oacute;n v&aacute;lido [" & Val & "]."
    ''                                    objMensaje.ServerMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "No es código de región válido [" & Val & "]."
    ''                                    lReporte.Add(objMensaje)
    ''                                Else
    ''                                    objIndicadorCapita.Key = Val
    ''                                End If
    ''                            Case 2 'Indicador sintético
    ''                                Try
    ''                                    objIndicadorCapita.Cien = CDbl(Val)
    ''                                Catch Exception As Exception
    ''                                    isErrorData = True
    ''                                    objMensaje = New EMensaje
    ''                                    objMensaje.Title = "Número Inv&aacute;lido"
    ''                                    objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                                    objMensaje.Type = EMensaje.ErrorMessage
    ''                                    objMensaje.IdErrorMessage = EMensaje.NumeroInvalido
    ''                                    objMensaje.UserMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "N&uacute;mero inv&aacute:lido [" & Val & "]."
    ''                                    objMensaje.ServerMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "Número inválido [" & Val & "]."
    ''                                    lReporte.Add(objMensaje)
    ''                                End Try
    ''                            Case 3 '100%
    ''                                Try
    ''                                    objIndicadorCapita.Cien = CInt(Val)
    ''                                Catch Exception As Exception
    ''                                    isErrorData = True
    ''                                    objMensaje = New EMensaje
    ''                                    objMensaje.Title = "Número Inv&aacute;lido"
    ''                                    objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                                    objMensaje.Type = EMensaje.ErrorMessage
    ''                                    objMensaje.IdErrorMessage = EMensaje.NumeroInvalido
    ''                                    objMensaje.UserMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "N&uacute;mero inv&aacute:lido [" & Val & "]."
    ''                                    objMensaje.ServerMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "Número inválido [" & Val & "]."
    ''                                    lReporte.Add(objMensaje)
    ''                                End Try
    ''                            Case 4 'Entre 99 y 80%
    ''                                Try
    ''                                    objIndicadorCapita.NoventaOchenta = CInt(Val)
    ''                                Catch Exception As Exception
    ''                                    isErrorData = True
    ''                                    objMensaje = New EMensaje
    ''                                    objMensaje.Title = "Número Inv&aacute;lido"
    ''                                    objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                                    objMensaje.Type = EMensaje.ErrorMessage
    ''                                    objMensaje.IdErrorMessage = EMensaje.NumeroInvalido
    ''                                    objMensaje.UserMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "N&uacute;mero inv&aacute:lido [" & Val & "]."
    ''                                    objMensaje.ServerMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "Número inválido [" & Val & "]."
    ''                                    lReporte.Add(objMensaje)
    ''                                End Try
    ''                            Case 5 'Entre 79 y 50%
    ''                                Try
    ''                                    objIndicadorCapita.SetentaCincuenta = CInt(Val)
    ''                                Catch Exception As Exception
    ''                                    isErrorData = True
    ''                                    objMensaje = New EMensaje
    ''                                    objMensaje.Title = "Número Inv&aacute;lido"
    ''                                    objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                                    objMensaje.Type = EMensaje.ErrorMessage
    ''                                    objMensaje.IdErrorMessage = EMensaje.NumeroInvalido
    ''                                    objMensaje.UserMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "N&uacute;mero inv&aacute:lido [" & Val & "]."
    ''                                    objMensaje.ServerMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "Número inválido [" & Val & "]."
    ''                                    lReporte.Add(objMensaje)
    ''                                End Try
    ''                            Case 6 'Menor a 50%
    ''                                Try
    ''                                    objIndicadorCapita.CincuentaMenos = CInt(Val)
    ''                                Catch Exception As Exception
    ''                                    isErrorData = True
    ''                                    objMensaje = New EMensaje
    ''                                    objMensaje.Title = "Número Inv&aacute;lido"
    ''                                    objMensaje.Graphic = EIndicadorCapita.Grafico
    ''                                    objMensaje.Type = EMensaje.ErrorMessage
    ''                                    objMensaje.IdErrorMessage = EMensaje.NumeroInvalido
    ''                                    objMensaje.UserMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "N&uacute;mero inv&aacute:lido [" & Val & "]."
    ''                                    objMensaje.ServerMessage = "Columna [" & Column & "] :: Fila [" & Row & "] : " & "Número inválido [" & Val & "]."
    ''                                    lReporte.Add(objMensaje)
    ''                                End Try
    ''                        End Select
    ''                        iContador += 1
    ''                    Next
    ''                    If Not bCorrecto Then
    ''                        Exit For
    ''                    End If
    ''                Next
    ''            End If

    ''        Else 'Si el archivo seleccionado por el usuario no posee hojas de trabajo
    ''            objMensaje = New EMensaje
    ''            objMensaje.Title = "Archivo Vac&iacute;o"
    ''            objMensaje.Type = EMensaje.ErrorMessage
    ''            objMensaje.IdErrorMessage = EMensaje.ArchivoSinHojas
    ''            objMensaje.UserMessage = "El archivo [" & ExcelFile.Name & "] no posee hojas de trabajo."
    ''            objMensaje.ServerMessage = "El archivo [" & ExcelFile.Name & "] no posee hojas de trabajo."
    ''            RegistrarReporte(objMensaje, EIndicadorCapita.Equivalencia)
    ''            Return JsonConvert.SerializeObject(objMensaje)
    ''        End If

    ''        If lReporte.Count > 0 Then

    ''            'Por cada mensaje de error registrado en la validación
    ''            For Each Mensaje In lReporte
    ''                RegistrarReporte(Mensaje, EIndicadorCapita.Equivalencia)
    ''            Next

    ''            objMensaje = New EMensaje
    ''            objMensaje.Title = "Reporte de Errores"
    ''            objMensaje.Type = EMensaje.ErrorMessage
    ''            objMensaje.IdErrorMessage = IIf(isErrorColumn, EMensaje.ReporteErroresColumnas, EMensaje.ReporteErroresDatos)
    ''            objMensaje.UserMessage = JsonConvert.SerializeObject(lReporte)
    ''        Else

    ''            objMensaje = New EMensaje
    ''            objMensaje.Title = "Informaci&oacute; Validada"
    ''            objMensaje.Type = EMensaje.SuccessMessage
    ''            objMensaje.UserMessage = "Informaci&oacute;n validada correctamente"

    ''        End If

    ''        Return JsonConvert.SerializeObject(objMensaje)
    ''    End Function

    ''    Public Sub SubirIndicadoresCapita(pFile As String, pIdUsuario As Integer)
    ''        Dim lInformacion As New List(Of EIndicadorCapita) 'Lista de filas leídas para insertar

    ''        Dim ExcelFile As New FileInfo(pFile)
    ''        Dim WorkBook = New ExcelPackage(ExcelFile).Workbook

    ''        Dim DatosGenerales = WorkBook.Worksheets.First
    ''        Dim iColumns = DatosGenerales.Dimension.End.Column
    ''        Dim iRows = DatosGenerales.Dimension.End.Row

    ''        For Row = 3 To iRows 'Comienza desde 3, por que las dos primeras filas son el título y cabeceras
    ''            Dim objIndicadorCapita As New EIndicadorCapita 'Objeto que se irá agregando a la lista de información a insertar
    ''            Dim iContador = 1 'Identificador de que columna se tiene que insertar según el orden de lectura
    ''            For Column = 2 To iColumns
    ''                Dim Val = DatosGenerales.Cells(Row, Column).Text
    ''                Select Case iContador
    ''                    Case 1 'Código de Región
    ''                        objIndicadorCapita.Key = Val
    ''                    Case 2 'Indicador sintético
    ''                        objIndicadorCapita.Indicador = CDbl(Val)
    ''                    Case 3 '100%
    ''                        objIndicadorCapita.Cien = CInt(Val)
    ''                    Case 4 'Entre 99 y 80%
    ''                        objIndicadorCapita.NoventaOchenta = CInt(Val)
    ''                    Case 5 'Entre 79 y 50%
    ''                        objIndicadorCapita.SetentaCincuenta = CInt(Val)
    ''                    Case 6 'Menor a 50%
    ''                        objIndicadorCapita.CincuentaMenos = CInt(Val)
    ''                End Select
    ''                iContador += 1
    ''            Next
    ''            lInformacion.Add(objIndicadorCapita)
    ''        Next

    ''        ' Eliminar los registros
    ''        EliminarIndicadoresCapita()

    ''        ' Por cada registro que haya dentro del archivo
    ''        Dim Success = True
    ''        For Each objIndicadorCapita In lInformacion

    ''            'Script para insertar en la base de datos
    ''            ' todos los registros por región
    ''            Dim strInsercion As New StringBuilder
    ''            strInsercion.Append("INSERT INTO I_SS_GREP_INDICAPITA (")
    ''            strInsercion.Append("INDI_ID_REGION" & SeparadorSql)
    ''            strInsercion.Append("INDI_N_INDICADOR" & SeparadorSql)
    ''            strInsercion.Append("INDI_N_100" & SeparadorSql)
    ''            strInsercion.Append("INDI_N_8099" & SeparadorSql)
    ''            strInsercion.Append("INDI_N_5079" & SeparadorSql)
    ''            strInsercion.Append("INDI_N_50MENOS" & SeparadorSql)
    ''            strInsercion.Append("INDI_D_FECHACORTE " & SeparadorSql)
    ''            strInsercion.Append("INDI_D_FECHACREACION" & SeparadorSql)
    ''            strInsercion.Append("INDI_ID_USUARIO")
    ''            strInsercion.Append(") VALUES (")
    ''            strInsercion.Append(StringSql & objIndicadorCapita.Key & StringSql & SeparadorSql)
    ''            strInsercion.Append(objIndicadorCapita.Indicador & SeparadorSql)
    ''            strInsercion.Append(objIndicadorCapita.Cien & SeparadorSql)
    ''            strInsercion.Append(objIndicadorCapita.NoventaOchenta & SeparadorSql)
    ''            strInsercion.Append(objIndicadorCapita.SetentaCincuenta & SeparadorSql)
    ''            strInsercion.Append(objIndicadorCapita.CincuentaMenos & SeparadorSql)
    ''            strInsercion.Append("SYSDATE" & SeparadorSql)
    ''            strInsercion.Append("SYSDATE" & SeparadorSql)
    ''            strInsercion.Append(pIdUsuario)
    ''            strInsercion.Append(")")

    ''            Query = Eschema & ".PR_SS_IUD_EJECUTARSCRIPT"

    ''            If OracleConnection.State = ConnectionState.Open Then
    ''                OracleConnection.Close()
    ''            End If
    ''            OracleConnection.Open()

    ''            Dim Command As New OracleCommand(Query, OracleConnection)
    ''            Command.CommandType = CommandType.StoredProcedure
    ''            With Command.Parameters
    ''                .Add("V_SCRIPT", OracleDbType.Varchar2, strInsercion.ToString, ParameterDirection.Input)
    ''            End With

    ''            Try
    ''                Command.ExecuteNonQuery()
    ''            Catch Exception As Exception
    ''                Success = False
    ''                Exit For
    ''            Finally
    ''                Command.Dispose()
    ''                OracleConnection.Close()
    ''            End Try
    ''        Next
    ''    End Sub

    ''    Public Sub EliminarIndicadoresCapita()
    ''        Dim strInsercion As New StringBuilder
    ''        strInsercion.Append("DELETE FROM I_SS_GREP_INDICAPITA")

    ''        Query = Eschema & ".PR_SS_IUD_EJECUTARSCRIPT"

    ''        If OracleConnection.State = ConnectionState.Open Then
    ''            OracleConnection.Close()
    ''        End If
    ''        OracleConnection.Open()

    ''        Dim Command As New OracleCommand(Query, OracleConnection)
    ''        Command.CommandType = CommandType.StoredProcedure
    ''        With Command.Parameters
    ''            .Add("V_SCRIPT", OracleDbType.Varchar2, strInsercion.ToString, ParameterDirection.Input)
    ''        End With

    ''        Try
    ''            Command.ExecuteNonQuery()
    ''        Catch Exception As Exception
    ''            Console.Write(Exception.Message)
    ''        Finally
    ''            Command.Dispose()
    ''            OracleConnection.Close()
    ''        End Try
    ''    End Sub

    ''    Private Sub RegistrarReporte(pMensaje As EMensaje, pEquivalencia As Integer)
    ''        Dim strInsercion As New StringBuilder
    ''        strInsercion.Append("INSERT INTO L_SS_ERRORCARGA VALUES (")
    ''        strInsercion.Append(pEquivalencia & SeparadorSql)
    ''        strInsercion.Append(pMensaje.IdErrorMessage & SeparadorSql)
    ''        strInsercion.Append(IIf(pMensaje.ServerMessage.Equals(" "), "NULL", StringSql & pMensaje.ServerMessage & StringSql) & SeparadorSql)
    ''        strInsercion.Append(pMensaje.IdUsuario & SeparadorSql)
    ''        strInsercion.Append("SYSDATE")
    ''        strInsercion.Append(")")

    ''        Query = Eschema & ".PR_SS_IUD_EJECUTARSCRIPT"

    ''        If OracleConnection.State = ConnectionState.Open Then
    ''            OracleConnection.Close()
    ''        End If
    ''        OracleConnection.Open()

    ''        Dim Command As New OracleCommand(Query, OracleConnection)
    ''        Command.CommandType = CommandType.StoredProcedure
    ''        With Command.Parameters
    ''            .Add("V_SCRIPT", OracleDbType.Varchar2, strInsercion.ToString, ParameterDirection.Input)
    ''        End With

    ''        Try
    ''            Command.ExecuteNonQuery()
    ''        Catch Exception As Exception
    ''            Console.Write(Exception.Message)
    ''        Finally
    ''            Command.Dispose()
    ''            OracleConnection.Close()
    ''        End Try
    ''    End Sub

    ''#End Region

End Class