Imports sisCalendarioEventos.Entity


Public Class DataPaquete

    Private m_connectionString As String

    Public Sub New(ByVal connectionString As String)
        Me.m_connectionString = connectionString
    End Sub

    Public Function ListarDetalle(idPaquete As Integer) As List(Of DetallePaquete)
        'Dim objData As PrimitiveCommand
        'Dim dt As DataTable = Nothing
        Dim detallePaquete As New List(Of DetallePaquete)
        'Try
        '    objData = New PrimitiveCommand
        '    objData.CreateInput("P_ID_PAQUETE", idPaquete, 15)
        '    objData.CreateOutPutCursor("P_CURSOR_RESUL")
        '    dt = objData.FP_ExecuteDataTable(Me.m_connectionString, "RECEPCION.PR_FUAE_SEL_PAQ_DETALLE")
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        For Each fila As DataRow In dt.Rows
        '            Dim objeto As New DetallePaquete
        '            objeto.tablaNombre = fila("TABLA").ToString
        '            objeto.registrosEnResumen = fila("REGISTROS_TRAMA").ToString
        '            objeto.registrosEnBD = fila("REGISTROS_BD").ToString
        '            objeto.registrosEnTrama = objeto.registrosEnResumen
        '            detallePaquete.Add(objeto)
        '        Next
        '    End If
        'Catch ex As Exception
        'Finally
        '    objData = Nothing
        '    dt = Nothing
        'End Try
        Return detallePaquete
    End Function

    Public Function EliminarPaquete(idPaquete As Integer, usuario As String, motivoEliminacion As String) As String
        'Dim objData As PrimitiveCommand
        'Dim objTx As PrimitiveTransaction = Nothing
        Dim msjError As String = "Ocurrio un error eliminando el paquete."
        Dim strRespuesta As String = "0$" & msjError
        'Try
        '    objData = New PrimitiveCommand
        '    objTx = New PrimitiveTransaction
        '    objTx.SP_CreateTransaction(Me.m_connectionString)
        '    objData.CreateInput("P_ID_PAQUETE", idPaquete, 15)
        '    objData.CreateInput("P_MOTIVODEL", motivoEliminacion, 500)
        '    objData.CreateInput("P_USUARIO", usuario, 20)
        '    objData.CreateOutPutCursor("P_CURSOR_RESUL")
        '    strRespuesta = objData.FP_ExecuteScalar(objTx, "RECEPCION.PR_FUAE_DEL_PAQ")
        '    objTx.SP_Commit()
        'Catch ex As Exception
        '    objTx.SP_Rollback()
        '    strRespuesta = "0$" & msjError
        'Finally
        '    objTx = Nothing
        '    objData = Nothing
        'End Try
        Return strRespuesta
    End Function

    Public Sub GuardarPaquete(ByRef oPaquete As Paquete)
        'Dim objData As PrimitiveCommand
        'Dim objTx As PrimitiveTransaction = Nothing
        Dim msjError As String = "Ocurrio un error registrando la información de las tramas."
        Dim strRespuesta As String = "0$" & msjError
        Dim arrRespuesta() As String
        'Try
        '    objData = New PrimitiveCommand
        '    objTx = New PrimitiveTransaction
        '    objTx.SP_CreateTransaction(Me.m_connectionString)
        '    objData.CreateInput("v_paq_IdPeriodo", oPaquete.PeriodoID, 15)
        '    objData.CreateInput("v_paq_IdCronograma", oPaquete.CronogramaID, 15)
        '    objData.CreateInput("v_paq_Descripcion", oPaquete.descripcion, 20)
        '    objData.CreateInput("v_paq_IdUsuario", oPaquete.usuarioCrea, 20)
        '    'objData.CreateInput("v_paq_IdEstablecimiento", oPaquete.IPRESS, 0)
        '    objData.CreateOutPutCursor("cv_1")
        '    strRespuesta = objData.FP_ExecuteScalar(objTx, "RECEPCION.PR_TRAMA_RECEPCIONAR_FUAE")
        '    objTx.SP_Commit()
        'Catch ex As Exception
        '    objTx.SP_Rollback()
        '    strRespuesta = "0$" & msjError
        'Finally
        '    objTx = Nothing
        '    objData = Nothing
        'End Try
        arrRespuesta = strRespuesta.Split("$"c)
        If arrRespuesta.Length > 0 Then
            If arrRespuesta(0) = "1" Then
                If IsNumeric(arrRespuesta(1)) Then
                    oPaquete.codigoError = "1" 'IDENTIFICAR QUE ES VALIDO TODO
                    oPaquete.ID = arrRespuesta(1)
                    oPaquete.mensaje = arrRespuesta(2)
                    oPaquete.IPRESS = arrRespuesta(3)
                    oPaquete.fechaSubida = arrRespuesta(4)
                    oPaquete.versionTrama = arrRespuesta(5)
                Else
                    oPaquete.mensaje = arrRespuesta(1)
                End If
            Else
                oPaquete.mensaje = arrRespuesta(1)
            End If
        Else
            oPaquete.mensaje = msjError
        End If
    End Sub

     

End Class
