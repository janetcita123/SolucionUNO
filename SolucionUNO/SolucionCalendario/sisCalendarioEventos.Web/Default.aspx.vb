Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsNothing(Request.QueryString("sid")) And Not IsNothing(Request.QueryString("sm")) Then

        Dim vUsuario As New Usuario
        Dim galleta As String
        Dim dni As String = ""
        Dim sw As String = Request.QueryString("tp")

        Session("sw_acceso") = sw


        ''DESARROLLO()
        'If Request.Cookies("pCookie") Is Nothing Then
        ''Usuario PSU
        'galleta = "cok_NombreUsuario = QUISPE CARDENAS, ALONSO EZEQUIEL&cok_Dni=40764943&cok_DISA=000&cok_GMR=&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=consultor_ogti_55@sis.gob.pe&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=26&cok_CEN_DIG_NOMBRE=OFICINA GENERAL DE TECNOLOGIA DE LA INFORMACION&cok_USU_ID=77777778&cok_idRed=0&cok_CP_NOMBRE=SIS CENTRAL - OGTI&cok_CP=&cok_CP_NOMBRE=&cok_UndEje=0000&cok_EESS=0000000000&cok_usuario=AQUISPECA&cok_ppdd=0&cok_SID=vwq2doavoym40brl1j5k5jyp&FAR=N&cok_ROL_GR=297&cok_ROL_DESCRIPCION_GR=Mis Preguntas"
        ''Asigna a SUBGERENTE
        'galleta = "cok_NombreUsuario = MC - CUBBIN SOSA, EDUARDO&cok_Dni=25772915&cok_DISA=000&cok_GMR=&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=consultor_ogti_56@sis.gob.pe&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=26&cok_CEN_DIG_NOMBRE=OFICINA GENERAL DE TECNOLOGIA DE LA INFORMACION&cok_USU_ID=77777782&cok_idRed=0&cok_CP_NOMBRE=SIS CENTRAL - OGTI&cok_CP=&cok_CP_NOMBRE=&cok_UndEje=0000&cok_EESS=0000000000&cok_usuario=EMCCUBBINS&cok_ppdd=0&cok_SID=vwq2doavoym40brl1j5k5jyp&FAR=N&cok_ROL_GR=303&cok_ROL_DESCRIPCION_GR=Mis Supervisiones"

        'galleta = "cok_NombreUsuario = CORONADO FARIS, PATRICIA&cok_Dni=40764943&cok_DISA=000&cok_GMR=&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=consultor_ogti_26@sis.gob.pe&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=26&cok_CEN_DIG_NOMBRE=OFICINA GENERAL DE TECNOLOGIA DE LA INFORMACION&cok_USU_ID=40764943&cok_idRed=0&cok_CP=&cok_CP_NOMBRE=SIS CENTRAL - OGTI&cok_UndEje=0000&cok_EESS=0000000000&cok_usuario=AQUISPECA&cok_ppdd=0&cok_SID=vwq2doavoym40brl1j5k5jyp&FAR=N&cok_ROL_GR=303&cok_ROL_DESCRIPCION_GR=Usuario Reuniones"
        'galleta = "cok_NombreUsuario = BENITES GOMEZ, PABLO&cok_Dni=70321563&cok_DISA=000&cok_GMR=&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=PBENITES@sis.gob.pe&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=2666&cok_CEN_DIG_NOMBRE=OFICINA GENERAL DE TECNOLOGIA DE LA INFORMACION&cok_USU_ID=70321563&cok_idRed=0&cok_CP=&cok_CP_NOMBRE=SIS CENTRAL - OGTI&cok_UndEje=0000&cok_EESS=0000000000&cok_usuario=PBENITESG&cok_ppdd=0&cok_SID=vwq2doavoym40brl1j5k5jyp&FAR=N&cok_ROL_GR=304&cok_ROL_DESCRIPCION_GR=Usuario Reuniones"
        'galleta = "cok_NombreUsuario = CORONADO FARIS, PATRICIA&cok_Dni=42949905&cok_DISA=000&cok_GMR=&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=consultor_ogti_26@sis.gob.pe&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=26&cok_CEN_DIG_NOMBRE=OFICINA GENERAL DE TECNOLOGIA DE LA INFORMACION&cok_USU_ID=42949905&cok_idRed=0&cok_CP=&cok_CP_NOMBRE=SIS CENTRAL - OGTI&cok_UndEje=0000&cok_EESS=0000000000&cok_usuario=PCORONADOF&cok_ppdd=0&cok_SID=vwq2doavoym40brl1j5k5jyp&FAR=N&cok_ROL_GR=304&cok_ROL_DESCRIPCION_GR=Usuario Reuniones"
        ''    'Experto PSU
        'galleta = "cok_NombreUsuario=ALVARADO, LUIS&cok_Dni=44457902&cok_DISA=000&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=pbenites@SIS.GOB.PE&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=&cok_CEN_DIG_NOMBRE=&cok_USU_ID=4092&cok_idRed=&cok_UndEje=0000&cok_EESS=150101A102&cok_usuario=CQUIROZ&cok_ppdd=0&cok_SID=cqjy11fw4ah55k55zjrx1055&FAR=N&cok_ROL_GR=301&cok_ROL_DESCRIPCION_GR=PERFIL PSU_USER"
        ''ADJUNTO
        'galleta = "cok_NombreUsuario=ZAPATA BERNAOLA, ANDRES JESUS&cok_Dni=44457902&cok_DISA=000&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=pbenites@SIS.GOB.PE&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=&cok_CEN_DIG_NOMBRE=&cok_USU_ID=4065&cok_idRed=&cok_UndEje=0000&cok_EESS=150101A102&cok_usuario=CQUIROZ&cok_ppdd=0&cok_SID=cqjy11fw4ah55k55zjrx1055&FAR=N&cok_ROL_GR=301&cok_ROL_DESCRIPCION_GR=PERFIL PSU_USER"

        '    'Subgerente PSU
        'galleta = "cok_NombreUsuario = QUIROZ ANGULO, CHRISTIAN JANDERSON&cok_Dni=42385497&cok_DISA=000&cok_GMR=2&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=CQUIROZ@SIS.GOB.PE&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=26&cok_CEN_DIG_NOMBRE=OFICINA GENERAL DE TECNOLOGIA DE LA INFORMACION&cok_USU_ID=4065&cok_idRed=0&cok_CP_NOMBRE=SIS CENTRAL - OGTI&cok_CP=&cok_CP_NOMBRE=&cok_UndEje=0000&cok_EESS=&cok_usuario=CQUIROZ&cok_ppdd=0&cok_SID=vwq2doavoym40brl1j5k5jyp&FAR=N&cok_ROL_GR=301&cok_ROL_DESCRIPCION_GR=Asigna Preguntas a Subgerentes"

        '    'Supervisor PSU
        'galleta = "cok_NombreUsuario=FELIPE, FELIPE&cok_Dni=46536259&cok_DISA=000&cok_ODSIS=000&cok_ODSIS_NOMBRE=&cok_EMAIL=LVEGA@SIS.GOB.PE&cok_CEN_LUG_CODIGO=1&cok_CEN_DIG_CODIGO=&cok_CEN_DIG_NOMBRE=&cok_USU_ID=11139&cok_idRed=&cok_UndEje=0000&cok_EESS=150101A102&cok_usuario=FELIPE&cok_ppdd=0&cok_SID=cqjy11fw4ah55k55zjrx1055&FAR=N&cok_ROL_GR=299&cok_ROL_DESCRIPCION_GR=PERFIL PSU_SUPERVISOR"


        'End If

        ''PRODUCCION
        galleta = Request.Cookies("pCookie").Value

        Session("UsuarioIPRESS") = vUsuario
        If Not galleta Is Nothing Then

            galleta = galleta.Replace("Ã'", "Ñ")

            Dim arr() As String
            arr = Split(galleta, "&")

            For i = 0 To arr.Count - 1
                Dim val() As String = Split(arr(i), "=")
                Try
                    If val(0).Trim = "cok_NombreUsuario" Then
                        vUsuario.Nom_Usua = val(1)
                    ElseIf val(0).Trim = "cok_usuario" Then
                        vUsuario.usuario = val(1)
                        Session("Cod_USU") = val(1)
                    ElseIf val(0).Trim = "cok_USU_ID" Then
                        vUsuario.Cod_Usua = val(1)
                        Session("ID_USER") = val(1)

                    ElseIf val(0).Trim = "cok_CEN_DIG_NOMBRE" Then
                        vUsuario.idArea = val(1)
                        Session("digitacion") = val(1)

                    ElseIf val(0).Trim = "cok_Dni" Then
                        vUsuario.dni = val(1)
                        Session("dni") = val(1)

                    ElseIf val(0).Trim = "cok_CP" Then
                        Try
                            vUsuario.Cod_Prov = val(1) 'Validando que solo el proveedor sea 6, 7 o 12
                            Session("Proveedor") = val(1)
                            'If val(1) <> 6 And val(1) <> 7 And val(1) <> 12 Then
                            '    vUsuario.Cod_Prov = 0
                            'End If
                        Catch ex As Exception
                            vUsuario.Cod_Prov = 0
                        End Try
                    ElseIf val(0).Trim = "cok_CP_NOMBRE" Then
                        Try

                            vUsuario.Nom_Prov = val(1)
                        Catch ex As Exception
                            vUsuario.Nom_Prov = ""
                        End Try
                    ElseIf val(0).Trim = "cok_ROL_GR" Then

                        Select Case val(1)
                            Case 303
                                vUsuario.rol = 1
                            Case 304
                                vUsuario.rol = 2
                            Case 305
                                vUsuario.rol = 3
                        End Select

                        vUsuario.Cod_Perf = val(1)
                    ElseIf val(0).Trim = "cok_ROL_DESCRIPCION_GR" Then
                        vUsuario.Nom_Perf = val(1)
                    ElseIf val(0).Trim = "cok_EMAIL" Then
                        vUsuario.Cor_Elec = val(1)
                    ElseIf val(0).Trim = "cok_ODSIS" Then
                        vUsuario.Cod_UDR = val(1)
                        Session("UDR_USER") = val(1)
                    ElseIf val(0).Trim = "cok_ODSIS_NOMBRE" Then
                        vUsuario.Nom_UDR = val(1)
                    ElseIf val(0).Trim = "cok_Dni" Then
                        dni = val(1)
                    End If
                Catch ex As Exception
                    Dim msnErr As String = ""
                    If val(0).Trim = "cok_NombreUsuario" Or val(0) = "cok_USU_ID" Then
                        msnErr = "Usuario NO es válido.\n\nPor favor contacte con el administrador del Sistema."
                    ElseIf val(0).Trim = "cok_CP_NOMBRE" Or val(0) = "cok_CP" Then
                        msnErr = "Proveedor NO es válido.\n\nPor favor contacte con el administrador del Sistema."
                    ElseIf val(0).Trim = "cok_ROL_GR" Or val(0) = "cok_ROL_DESCRIPCION_GR" Then
                        msnErr = "Perfil de Usuario NO es válido.\n\nPor favor contacte con el administrador del Sistema."
                    ElseIf val(0).Trim = "cok_EMAIL" Then
                        msnErr = "Cuenta de correo NO es válido.\n\nPor favor contacte con el administrador del Sistema."
                    ElseIf val(0).Trim  = "cok_ODSIS" Or val(0) = "cok_ODSIS_NOMBRE" Then
                        msnErr = "UDR NO es válido.\n\nPor favor contacte con el administrador del Sistema."
                    End If

                    mensajeErrorJS(msnErr)
                    Exit For
                    'Session("UsuarioIPRESS") = Nothing
                    'Response.Redirect("../sisERP/SisMenu/frmGestionVinculos.aspx?sid=" + Session.SessionID, False)
                End Try

            Next

            'Try
            '    Dim Obj As New PSU_business
            '    Dim ds As DataSet = Obj.Listar_AreaDelUsuario(vUsuario.Cod_Usua)
            '    If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            '        vUsuario.idArea = ds.Tables(0).Rows(0)(0).ToString
            '    Else
            '        vUsuario.idArea = ""
            '    End If
            'Catch ex As Exception
            '    vUsuario.idArea = ""
            'End Try

            Session("UsuarioIPRESS") = vUsuario
            ''FIN PRODUCCION
            'Registrando acceso
            Dim user() As String
            user = Split(vUsuario.Nom_Usua, ",")

            'Dim Obj As New Seguridad_business
            'Dim ds As DataSet
            'ds = Obj.PR_PRCAT_ACT_REGISTRAUSUARIO(vUsuario.Cod_Usua, dni, Microsoft.VisualBasic.Trim(user(0)), Microsoft.VisualBasic.Trim(user(1)), vUsuario.Cor_Elec)

            'Response.Redirect("WebMantenimiento.aspx")
            Select Case vUsuario.Cod_Perf
                'DESCOMENTAR PARA QUITAR EL INACTIVO 5/08/2017

                Case Usuario.CRUSUARIO
                    'Response.Redirect("prueba.aspx")
                    Response.Redirect("CalendarioIndex.aspx")

                Case Usuario.CRUGERENTE
                    Response.Redirect("CalendarioIndex.aspx")
                    'Response.Redirect("prueba.aspx")
                Case Usuario.CRSUPERVISOR
                    Response.Redirect("CalendarioIndex.aspx")
                    'Response.Redirect("prueba.aspx")

            End Select
        Else
            Response.Redirect("http: //www.sis.gob.pe/modulos/sisMenu/frmGestionVinculos.aspx", False)
        End If
    End Sub

    Public Sub mensajeErrorJS(ByVal msn As String)
        Dim script As String = "<script type='text/javascript'>MensajeError('" + msn + "');</script>"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "MensajeErrorX", script, False)
    End Sub



End Class