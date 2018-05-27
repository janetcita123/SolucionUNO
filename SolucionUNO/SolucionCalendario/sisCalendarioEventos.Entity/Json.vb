Public Class Json

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

    Public Property result As result()

End Class

Public Class result
    Public Property agenda As Agenda()
    Public Property personas As datosusu()
    Public Property areas As datos() 'Areas  
    Public Property entidades As datos() 'Entidades
    Public Property acuerdos As EAcuerdo()
    Public Property observaciones As EObservacion()
    Private DataStart As Date
    Public Property start() As Date
        Get
            Return DataStart
        End Get
        Set(ByVal value As Date)
            DataStart = value
        End Set
    End Property


    Private DataEnd As Date

    Public Property [end]() As Date
        Get
            Return DataEnd
        End Get
        Set(ByVal value As Date)
            DataEnd = value
        End Set
    End Property

    Private intId As Integer

    Public Property id() As Integer
        Get
            Return intId
        End Get
        Set(ByVal value As Integer)
            intId = value
        End Set
    End Property

    Private intTipo As Integer

    Public Property tipo() As Integer
        Get
            Return intTipo
        End Get
        Set(ByVal value As Integer)
            intTipo = value
        End Set
    End Property

    Private intArea As Integer

    Public Property area() As Integer
        Get
            Return intArea
        End Get
        Set(ByVal value As Integer)
            intArea = value
        End Set
    End Property
    Private strNombreArea As String
    Public Property nombrearea() As String
        Get
            Return strNombreArea
        End Get
        Set(ByVal value As String)
            strNombreArea = value
        End Set
    End Property
    Private strTitle As String
    Public Property title() As String
        Get
            Return strTitle
        End Get
        Set(ByVal value As String)
            strTitle = value
        End Set
    End Property

    Private strDetalle As String
    Public Property detalle() As String
        Get
            Return strDetalle
        End Get
        Set(ByVal value As String)
            strDetalle = value
        End Set
    End Property

    Private strUsuario As String
    Public Property usuario() As String
        Get
            Return strUsuario
        End Get
        Set(ByVal value As String)
            strUsuario = value
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
    Private strRuta As String
    Public Property ruta() As String
        Get
            Return strRuta
        End Get
        Set(ByVal value As String)
            strRuta = value
        End Set
    End Property
End Class

Public Class Agenda
    Private intIda As Integer
    Public Property ida() As Integer
        Get
            Return intIda
        End Get
        Set(ByVal value As Integer)
            intIda = value
        End Set
    End Property
    Private strTituloa As String
    Public Property tituloa() As String
        Get
            Return strTituloa
        End Get
        Set(ByVal value As String)
            strTituloa = value
        End Set
    End Property

    Private strDetallea As String
    Public Property detallea() As String
        Get
            Return strDetallea
        End Get
        Set(ByVal value As String)
            strDetallea = value
        End Set
    End Property
    Private strEstado As String
    'ACTIVO O INACTIVO
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

 