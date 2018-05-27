Public Class prueba_upload
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function SubmitItems(ByVal items As Object)

        Return "ok"
    End Function
End Class