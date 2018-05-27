Public Class Paquete

    Public Property descripcion As String
    Public Property periodoAnio As Integer
    Public Property periodoMes As Integer
    Public Property usuarioCrea As String
    Public Property GMR As String
    Public Property UDR As String
    Public Property IPRESS As String
    Public Property PPDD As String
    Public Property mensaje As String

    Public Property ID As String
    Public Property PeriodoID As String
    Public Property CronogramaID As String
    Public Property aplicativo As String
    Public Property fechaSubida As String
    Public Property versionTrama As String
    Public Property codigoError As String

    Private s_clave As String
    Private m_archivosPermitidos As New List(Of String)

    Public Sub New()
    End Sub

    Public Sub New(descripcion)
        Me.descripcion = descripcion
    End Sub

    Public Property clave As String
        Get
            Return Me.s_clave
        End Get
        Set(value As String)
            Me.s_clave = value
        End Set
    End Property

    Public ReadOnly Property archivosPermitidos As List(Of String)
        Get
            Return m_archivosPermitidos
        End Get
    End Property

    Public WriteOnly Property addArchivosPermitidos As String
        Set(value As String)
            m_archivosPermitidos.Add(value)
        End Set
    End Property

End Class

'Public Class PaquetePermitidoLista

'    Private m_archivosPermitidos As List(Of PaquetePermitido)

'    Public Sub New()
'        m_archivosPermitidos = New List(Of PaquetePermitido)
'    End Sub

'    Public ReadOnly Property archivosPermitidos As List(Of PaquetePermitido)
'        Get
'            Return m_archivosPermitidos
'        End Get
'    End Property

'    Public WriteOnly Property addArchivosPermitidos As PaquetePermitido
'        Set(value As PaquetePermitido)
'            m_archivosPermitidos.Add(value)
'        End Set
'    End Property

'End Class

Public Class PaquetePermitido

    Public Sub New()
    End Sub

    Public Sub New(version As String, clave As String, archivo As String)
        Me.version = version
        Me.clave = clave
        Me.archivo = archivo
    End Sub

    Public Property version As String
    Public Property clave As String
    Public Property archivo As String

End Class

Public Class DetallePaquete

    Public Property tablaNombre As String
    Public Property registrosEnResumen As String
    Public Property registrosEnTrama As String
    Public Property registrosEnBD As String

    Public Sub New()
    End Sub

    Public Sub New(tablaNombre As String, registrosEnResumen As String, RegistrosEnTrama As String, RegistrosEnBD As String)
        Me.tablaNombre = tablaNombre
        Me.registrosEnResumen = registrosEnResumen
        Me.registrosEnTrama = RegistrosEnTrama
        Me.registrosEnBD = RegistrosEnBD
    End Sub

    Public ReadOnly Property existeDiferencia As String
        Get
            If registrosEnResumen = registrosEnTrama And registrosEnResumen = registrosEnBD Then
                Return "N"
            Else
                Return "S"
            End If
        End Get
    End Property

End Class