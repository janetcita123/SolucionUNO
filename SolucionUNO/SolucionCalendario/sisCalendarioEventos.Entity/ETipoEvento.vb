Public Class ETipoEvento

    Private strErrorMessage As String
    Public Property ErrorMessage() As String
        Get
            Return strErrorMessage
        End Get
        Set(ByVal value As String)
            strErrorMessage = value
        End Set
    End Property

    Private intId As Integer
    Public Property Id() As Integer
        Get
            Return intId
        End Get
        Set(ByVal value As Integer)
            intId = value
        End Set
    End Property

    Private strDescripcion As String
    Public Property Descripcion() As String
        Get
            Return strDescripcion
        End Get
        Set(ByVal value As String)
            strDescripcion = value
        End Set
    End Property

    'Private strClave As String
    'Public Property Clave() As String
    '    Get
    '        Return strClave
    '    End Get
    '    Set(ByVal value As String)
    '        strClave = value
    '    End Set
    'End Property

    'Private strFecha As String
    'Public Property Fecha() As String
    '    Get
    '        Return strFecha
    '    End Get
    '    Set(ByVal value As String)
    '        strFecha = value
    '    End Set
    'End Property

    'Private strDuracion As String
    'Public Property Duracion() As String
    '    Get
    '        Return strDuracion
    '    End Get
    '    Set(ByVal value As String)
    '        strDuracion = value
    '    End Set
    'End Property

    'Private strStatus As String
    'Public Property Status() As String
    '    Get
    '        Return strStatus
    '    End Get
    '    Set(ByVal value As String)
    '        strStatus = value
    '    End Set
    'End Property

End Class