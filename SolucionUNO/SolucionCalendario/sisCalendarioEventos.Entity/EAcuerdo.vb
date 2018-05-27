Public Class EAcuerdo

    Private intIdAc As Integer

    Public Property idac() As Integer
        Get
            Return intIdAc
        End Get
        Set(ByVal value As Integer)
            intIdAc = value
        End Set
    End Property

    Private intIdAg As Integer

    Public Property idag() As Integer
        Get
            Return intIdAg
        End Get
        Set(ByVal value As Integer)
            intIdAg = value
        End Set
    End Property

    Private strDniParticipante As String
    Public Property participante() As String
        Get
            Return strDniParticipante
        End Get
        Set(ByVal value As String)
            strDniParticipante = value
        End Set
    End Property

    Private strTituloac As String
    Public Property tituloac() As String
        Get
            Return strTituloac
        End Get
        Set(ByVal value As String)
            strTituloac = value
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

    Private strFecha As Date
    Public Property fechacompromiso() As Date
        Get
            Return strFecha
        End Get
        Set(ByVal value As Date)
            strFecha = value
        End Set
    End Property

End Class
