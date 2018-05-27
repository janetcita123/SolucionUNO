Public Class EEvento

    Private intId As Integer
    Public Property Id() As Integer
        Get
            Return intId
        End Get
        Set(ByVal value As Integer)
            intId = value
        End Set
    End Property

    Private strTitle As String
    Public Property Title() As String
        Get
            Return strTitle
        End Get
        Set(ByVal value As String)
            strTitle = value
        End Set
    End Property

    Private strDetalle As String

    Public Property Detalle() As String
        Get
            Return strDetalle
        End Get
        Set(ByVal value As String)
            strDetalle = value
        End Set
    End Property

    Private strRuta As String

    Public Property Ruta() As String
        Get
            Return strRuta
        End Get
        Set(ByVal value As String)
            strRuta = value
        End Set
    End Property


    Private strIni As String
    Public Property Ini() As String
        Get
            Return strIni
        End Get
        Set(ByVal value As String)
            strIni = value
        End Set
    End Property

    Private strFin As String
    Public Property Fin() As String
        Get
            Return strFin
        End Get
        Set(ByVal value As String)
            strFin = value
        End Set
    End Property


    Private strColor As String
    Public Property Color() As String
        Get
            Return strColor
        End Get
        Set(ByVal value As String)
            strColor = value
        End Set
    End Property

    Private strErrorMessage As String
    Public Property ErrorMessage() As String
        Get
            Return strErrorMessage
        End Get
        Set(ByVal value As String)
            strErrorMessage = value
        End Set
    End Property

End Class
