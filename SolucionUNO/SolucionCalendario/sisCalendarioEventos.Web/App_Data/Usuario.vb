Public Class Usuario
    Public Const CONSADMI As Integer = 132
    Public Const CONSEPRE As Integer = 13388
    Public Const CONSGREP As Integer = 13488
    Public Const CONSGNFA As Integer = 13588
    Public Const CONSMEDI As Integer = 136
    Public Const CONSGNFS As Integer = 137
    Public Const CONSREGI As Integer = 207
    Public Const CONSGMR As Integer = 300
    Public Const CONSDUDR As Integer = 301
    Public Const CONSUSUA As Integer = 264

    'para PSU
    Public Const USUARIO_ING As String = ""
    Public Const USUARIO_ID As Integer = 302

    'Public Const PSUSUPER As Integer = 135 '17439 '
    'Public Const PSUEXPER As Integer = 134 '12773 '
    'Public Const PSUUSER As Integer = 133 '16074 '

    Public Const CRUSUARIO As Integer = 303 '17439 '135
    Public Const CRUGERENTE As Integer = 304 '12773 '134
    Public Const CRSUPERVISOR As Integer = 305 '16074 '133
    
  
    Private _Cod_Usua As Integer
    Private _Nom_Usua As String
    Private _Cod_Prov As Integer
    Private _Cod_UDR As String
    Private _Nom_UDR As String
    Private _Nom_Prov As String
    Private _Cod_Acce As Integer
    Private _Cod_Perf As Integer
    Private _Nom_Perf As String
    Private _Cor_Elec As String
    Private _Cod_EXP As String
    Private _Cadena As String
    Private _PagCambio As String = "1"
    Private _idArea As String
    Private _dni As String
    Private _rol As String
    Private _usuario As String


    Property usuario As String
        Get
            Return _usuario
        End Get
        Set(ByVal value As String)
            _usuario = value
        End Set
    End Property

    Property rol As String
        Get
            Return _rol
        End Get
        Set(ByVal value As String)
            _rol = value
        End Set
    End Property


    Property dni As String
        Get
            Return _dni
        End Get
        Set(ByVal value As String)
            _dni = value
        End Set
    End Property
    Property idArea As String
        Get
            Return _idArea
        End Get
        Set(ByVal value As String)
            _idArea = value
        End Set
    End Property

    Property PagCambio As String
        Get
            Return _PagCambio
        End Get
        Set(ByVal value As String)
            _PagCambio = value
        End Set
    End Property
    Property Cadena As String
        Get
            Return _Cadena
        End Get
        Set(ByVal value As String)
            _Cadena = value
        End Set
    End Property

    Property Cod_EXP As String
        Get
            Return _Cod_EXP
        End Get
        Set(ByVal value As String)
            _Cod_EXP = value
        End Set
    End Property
    Property Cod_Usua As Integer
        Get
            Return _Cod_Usua
        End Get
        Set(ByVal value As Integer)
            _Cod_Usua = value
        End Set
    End Property

    Property Nom_Usua As String
        Get
            Return _Nom_Usua
        End Get
        Set(value As String)
            _Nom_Usua = value
        End Set
    End Property
    Property Cod_Perf As Integer
        Get
            Return _Cod_Perf
        End Get
        Set(value As Integer)
            _Cod_Perf = value
        End Set
    End Property

    Property Nom_Perf As String
        Get
            Return _Nom_Perf
        End Get
        Set(value As String)
            _Nom_Perf = value
        End Set
    End Property

    Property Cod_Prov As Integer
        Get
            Return _Cod_Prov
        End Get
        Set(value As Integer)
            _Cod_Prov = value
        End Set
    End Property
    Property Cod_UDR As String
        Get
            Return _Cod_UDR
        End Get
        Set(ByVal value As String)
            _Cod_UDR = value
        End Set
    End Property

    Property Nom_UDR As String
        Get
            Return _Nom_UDR
        End Get
        Set(ByVal value As String)
            _Nom_UDR = value
        End Set
    End Property


    Property Nom_Prov As String
        Get
            Return _Nom_Prov
        End Get
        Set(value As String)
            _Nom_Prov = value
        End Set
    End Property

    Property Cod_Acce As Integer
        Get
            Return _Cod_Acce
        End Get
        Set(value As Integer)
            _Cod_Acce = value
        End Set
    End Property


    Property Cor_Elec As String
        Get
            Return _Cor_Elec
        End Get
        Set(value As String)
            _Cor_Elec = value
        End Set
    End Property

End Class
