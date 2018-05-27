Public Class EUsuario
    Public strSuccess As String

    Public Property success() As String
        Get
            Return strSuccess
        End Get
        Set(ByVal value As String)
            strSuccess = value
        End Set
    End Property

    Public strError As String

    Public Property error_() As String
        Get
            Return strError
        End Get
        Set(ByVal value As String)
            strError = value
        End Set
    End Property

    Public Property result As datosusu()

End Class

Public Class datosusu

    Private intId As String

    Public Property id() As String
        Get
            Return intId
        End Get
        Set(ByVal value As String)
            intId = value
        End Set
    End Property

    Private strNombres As String
    Public Property nombres() As String
        Get
            Return strNombres
        End Get
        Set(ByVal value As String)
            strNombres = value
        End Set
    End Property

    Private strApepat As String
    Public Property apepat() As String
        Get
            Return strApepat
        End Get
        Set(ByVal value As String)
            strApepat = value
        End Set
    End Property

    Private strApemat As String
    Public Property apemat() As String
        Get
            Return strApemat
        End Get
        Set(ByVal value As String)
            strApemat = value
        End Set
    End Property

    Private strNombreCompleto As String
    Public Property name() As String
        Get
            Return strNombreCompleto
        End Get
        Set(ByVal value As String)
            strNombreCompleto = value
        End Set
    End Property

    Private strDni As String
    Public Property dni() As String
        Get
            Return strDni
        End Get
        Set(ByVal value As String)
            strDni = value
        End Set
    End Property


    Private strParticipa As Boolean
    Public Property participa() As Boolean
        Get
            Return strParticipa
        End Get
        Set(ByVal value As Boolean)
            strParticipa = value
        End Set
    End Property

    Private strEvento As String
    Public Property evento() As String
        Get
            Return strEvento
        End Get
        Set(ByVal value As String)
            strEvento = value
        End Set
    End Property

    Private strAsistencia As Boolean
    Public Property asistencia() As Boolean
        Get
            Return strAsistencia
        End Get
        Set(ByVal value As Boolean)
            strAsistencia = value
        End Set
    End Property

    Private strArea As String
    Public Property area() As String
        Get
            Return strArea
        End Get
        Set(ByVal value As String)
            strArea = value
        End Set
    End Property

    Private strSigla As String
    Public Property sigla() As String
        Get
            Return strSigla
        End Get
        Set(ByVal value As String)
            strSigla = value
        End Set
    End Property

    Private strEstado As String
    Public Property estado() As String
        Get
            Return strEstado
        End Get
        Set(ByVal value As String)
            strEstado = value
        End Set
    End Property
    'ACCION activo o inactivo para enviar a bd
    Private strAccion As String
    Public Property accion() As String
        Get
            Return strAccion
        End Get
        Set(ByVal value As String)
            strAccion = value
        End Set
    End Property

    Private strMarca As String
    Public Property marca() As String
        Get
            Return strMarca
        End Get
        Set(ByVal value As String)
            strMarca = value
        End Set
    End Property
End Class
