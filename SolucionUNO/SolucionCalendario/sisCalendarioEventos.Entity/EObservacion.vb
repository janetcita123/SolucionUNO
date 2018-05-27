Public Class EObservacion

    Private intIdOb As Integer

    Public Property idob() As Integer
        Get
            Return intIdOb
        End Get
        Set(ByVal value As Integer)
            intIdOb = value
        End Set
    End Property

    Private intIdAg As Integer

    Private strTitulo As String
    Public Property titulo() As String
        Get
            Return strTitulo
        End Get
        Set(ByVal value As String)
            strTitulo = value
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
    Private strEstado As String
    Public Property estado() As String
        Get
            Return strEstado
        End Get
        Set(ByVal value As String)
            strEstado = value
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
