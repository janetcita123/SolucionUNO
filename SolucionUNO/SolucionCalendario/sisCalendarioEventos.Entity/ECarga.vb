Public Class ECarga
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

    Public Property result As datos()

End Class

Public Class datos

    Private intId As Integer

    Public Property id() As Integer
        Get
            Return intId
        End Get
        Set(ByVal value As Integer)
            intId = value
        End Set
    End Property

    Private intIdStr As String

    Public Property idStr() As String
        Get
            Return intIdStr
        End Get
        Set(ByVal value As String)
            intIdStr = value
        End Set
    End Property

    Private strDescripcion As String
    Public Property descripcion() As String
        Get
            Return strDescripcion
        End Get
        Set(ByVal value As String)
            strDescripcion = value
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
