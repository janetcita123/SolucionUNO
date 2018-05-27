Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports SelectPdf
Imports SelectPdf.PdfMargins
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Drawing.Printing

Public Class pdfNew
    Inherits System.Web.UI.Page
    Public jsEventosResponse As String
    Public namepdf As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConvertirAJonson(RequestEvento)
    End Sub

    Protected Sub BtnCreatePdf_Click(sender As Object, e As EventArgs) Handles BtnCreatePdf.Click

        BtnCreatePdf.Attributes.Add("style", "display:none")
        Dim fecha As DateTime = DateTime.Now.ToString("dd/MM/yyyy")


        namepdf = fecha + "ActaN" + namepdf + ".pdf"
        descargaPDF(namepdf)
        

 
    End Sub

    Sub descargaPDF(nombre)
        Dim userPassword As String = ""
        Dim ownerPassword As String = ""

        Dim canAssembleDocument As Boolean = 0
        Dim canCopyContent As Boolean = 0
        Dim canEditAnnotations As Boolean = 0
        Dim canEditContent As Boolean = 0
        Dim canFillFormFields As Boolean = 0
        Dim canPrint As Boolean = 1

        ' instantiate a html to pdf converter object
        Dim converter As New HtmlToPdf()

        ' set document passwords
        If Not String.IsNullOrEmpty(userPassword) Then
            converter.Options.SecurityOptions.UserPassword = userPassword
        End If
        If Not String.IsNullOrEmpty(ownerPassword) Then
            converter.Options.SecurityOptions.OwnerPassword = ownerPassword
        End If

        'set document permissions
        converter.Options.SecurityOptions.CanAssembleDocument = canAssembleDocument
        converter.Options.SecurityOptions.CanCopyContent = canCopyContent
        converter.Options.SecurityOptions.CanEditAnnotations = canEditAnnotations
        converter.Options.SecurityOptions.CanEditContent = canEditContent
        converter.Options.SecurityOptions.CanFillFormFields = canFillFormFields
        converter.Options.SecurityOptions.CanPrint = canPrint
        'margenes
        converter.Options.MarginLeft = 20
        converter.Options.MarginRight = 20
        converter.Options.MarginTop = 30
        converter.Options.MarginBottom = 30

        Dim url = HttpContext.Current.Request.Url.AbsoluteUri

        ' create a new pdf document converting an url
        Dim doc As PdfDocument = converter.ConvertUrl(url)

        ' save pdf document
        doc.Save(Response, False, nombre)

        ' close pdf document
        doc.Close()

        BtnCreatePdf.Visible = True
        Response.Redirect("pdfNew.aspx")
    End Sub

    Private Sub ConvertirAJonson(RequestEvento As Object)

        For Each item As Object In RequestEvento
            If item.key = "id" Then
                namepdf = item.value
            End If
        Next
        Dim jsSerializer As New JavaScriptSerializer
        jsEventosResponse = jsSerializer.Serialize(RequestEvento)
    End Sub

End Class