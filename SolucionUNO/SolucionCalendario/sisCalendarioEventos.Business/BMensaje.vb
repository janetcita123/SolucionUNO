
Imports sisCalendarioEventos.Entity
Imports sisCalendarioEventos.Data

 
Public Class BMensaje

    Dim objData As New DMensaje

    Public Function GetMap() As List(Of EObjeto)

        ' Return objData.GetPrueba
    End Function

    Public Sub GuardarEvento(ByVal fInicio As String, ByVal fFinal As String, ByVal sTipo As String, ByVal sTitle As String, ByVal sBody As String)

        Dim fechaInicio As String

        fechaInicio = fInicio

        'AQUI LLAMAS A LA BASE DE DATOS CON LOS PARAMETROS ENVIADOS, SI LLEGUE HASTA AQUI CON DEBUG
    End Sub

    'Public Function IndicadoresCapita() As List(Of EIndicadorCapita)
    '    Return objData.IndicadoresCapita
    'End Function

    'Public Function IndicadoresPreLiquidados() As List(Of EPreLiquidado)
    '    Return objData.IndicadoresPreLiquidados
    'End Function

    'Public Function AtencionesPorEmergencia() As List(Of EAtencionPorEmergencia)
    '    Return objData.AtencionesPorEmergencia
    'End Function

    'Public Function IntercambioPrestacional() As List(Of EIntercambioPrestacional)
    '    Return objData.IntercambioPrestacional
    'End Function

    'Public Function TamizadosConAnemia() As List(Of ETamizadoConAnemia)
    '    Return objData.TamizadosConAnemia
    'End Function

    'Public Function TamizadosConCancer() As List(Of ETamizadoConCancer)
    '    Return objData.TamizadosConCancer
    'End Function

    'Public Function AtencionesSISOL() As List(Of EAtencionSISOL)
    '    Return objData.AtencionesSISOL
    'End Function

    'Public Function AtendidosAtencionesEsperanzaMap() As List(Of EAtendidoAtencionesEsperanza)
    '    Return objData.AtendidosAtencionesEsperanzaMap
    'End Function

    'Public Function AvanceMetaCapita() As List(Of EAvanceMetaCapita)
    '    Return objData.AvanceMetaCapita
    'End Function

    'Public Function PrestacionesEnReconsideracion() As List(Of EReconsideracion)
    '    Return objData.PrestacionesEnReconsideracion
    'End Function

    'Public Function ProblNutr() As List(Of EProblNutricional)
    '    Return objData.ProblNutr
    'End Function

    'Public Function DiagnosticadosOdontologicos() As List(Of EOdontologico)
    '    Return objData.DiagnosticadosOdontologicos
    'End Function

    'Public Function DiagnosticadosRefrectarios() As List(Of ERefractario)
    '    Return objData.DiagnosticadosRefractarios
    'End Function

    'Public Function PseBeneficiados() As List(Of EPseBeneficiado)
    '    Return objData.PseBeneficiados
    'End Function

    'Public Function PrestacionesNoConformesPCPP() As List(Of ENoConformePCPP)
    '    Return objData.PrestacionesNoConformesPCPP
    'End Function

    'Public Function SerieTemporalProduccion() As List(Of ESerieTemporalProduccion)
    '    Return objData.SerieTemporalProduccion
    'End Function

    'Public Function SerieTemporalPorNivel() As List(Of ESerieTemporalPorNivel)
    '    Return objData.SerieTemporalPorNivel
    'End Function

    'Public Function SerieTemporalPorPrestacion() As List(Of ESerieTemporalPorPrestacion)
    '    Return objData.SerieTemporalPorPrestacion
    'End Function

    'Public Function Capitados() As List(Of ECapitado)
    '    Return objData.Capitados
    'End Function

    'Public Function DistribucionPorCategorias() As List(Of EDistribucionPorCategoria)
    '    Return objData.DistribucionPorCategorias
    'End Function

    'Public Function ValidarIndicadoresCapita(pFile As String) As String
    '    Return objData.ValidarIndicadoresCapita(pFile)
    'End Function

    'Public Sub SubirIndicadoresCapita(pFile As String, pIdUsuario As Integer)
    '    objData.SubirIndicadoresCapita(pFile, pIdUsuario)
    'End Sub

    'Public Function AtendidosGrupo18() As List(Of EAtendidos)
    '    Return objData.AtendidosGrupo18
    'End Function

    'Public Function DistribucionPorServicio() As List(Of EDistribucionPorServicio)
    '    Return objData.DistribucionPorServicio
    'End Function
    'Public Function AtencionesConsExt() As List(Of EAtencionSISOL)
    '    Return objData.AtencionesConsExt
    'End Function

    'Public Function AtendidosAtencionesEsperanza() As List(Of EFormato5)
    '    Dim objFissal As New DFissal
    '    Return objFissal.Formato5
    'End Function

    'Public Function AtencionesServiciosComplementarios() As List(Of EServicioComplementario)
    '    Return objData.AtencionesServiciosComplementarios
    'End Function

    'Public Function ResultadoPea() As List(Of ENoConformePCPP)
    '    Return objData.ResultadoPea
    'End Function

    'Public Function ResultadoRecon() As List(Of ENoConformePCPP)
    '    Return objData.ResultadoRecon
    'End Function

End Class