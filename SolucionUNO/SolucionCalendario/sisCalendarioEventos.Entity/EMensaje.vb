''' <summary>
''' Creado por: Fabián Pérez Vásquez  (21/03/2016)
''' </summary>

Public Class EMensaje

    Public Const ErrorMessage = "Error"
    Public Const SuccessMessage = "Success"

    Public Const ReporteErroresColumnas = -1
    Public Const ReporteErroresDatos = 0
    Public Const NroColumnasInvalido = 1
    Public Const ColumnaNoEspecificada = 2
    Public Const ArchivoSinHojas = 3
    Public Const MalFormacionScript = 4
    Public Const NumeroInvalido = 5
    Public Const PeriodoInvalido = 6
    Public Const RegionInvalida = 7
    Public Const UnidadEjecutoraInvalida = 8
    Public Const ArchivoSinRegistro = 9
    Public Const ValorDemasiadoLargo = 10

    Public Sub New()
        strTitle = " "
        strType = " "
        intIdErrorMessage = -1
        strUserMessage = " "
        strServerMessage = " "
        intIdUsuario = -1
    End Sub

    Private strTitle As String
    Public Property Title() As String
        Get
            Return strTitle
        End Get
        Set(ByVal value As String)
            strTitle = value
        End Set
    End Property

    Private strGraphic As String
    Public Property Graphic() As String
        Get
            Return strGraphic
        End Get
        Set(ByVal value As String)
            strGraphic = value
        End Set
    End Property

    Private strType As String
    Public Property Type() As String
        Get
            Return strType
        End Get
        Set(ByVal value As String)
            strType = value
        End Set
    End Property

    Private intIdErrorMessage As Integer
    Public Property IdErrorMessage() As Integer
        Get
            Return intIdErrorMessage
        End Get
        Set(ByVal value As Integer)
            intIdErrorMessage = value
        End Set
    End Property

    Private strUserMessage As String
    Public Property UserMessage() As String
        Get
            Return strUserMessage
        End Get
        Set(ByVal value As String)
            strUserMessage = value
        End Set
    End Property

    Private lMessages As List(Of EMensaje)
    Public Property Messages() As List(Of EMensaje)
        Get
            Return lMessages
        End Get
        Set(ByVal value As List(Of EMensaje))
            lMessages = value
        End Set
    End Property

    Private strServerMessage As String
    Public Property ServerMessage() As String
        Get
            Return strServerMessage
        End Get
        Set(ByVal value As String)
            strServerMessage = value
        End Set
    End Property

    Private intIdUsuario As Integer
    Public Property IdUsuario() As Integer
        Get
            Return intIdUsuario
        End Get
        Set(ByVal value As Integer)
            intIdUsuario = value
        End Set
    End Property

End Class