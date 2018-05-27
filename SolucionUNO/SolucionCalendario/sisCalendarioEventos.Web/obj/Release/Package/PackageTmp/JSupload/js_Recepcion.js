var txtTramaIPRESS;
var txtTramaAppOrigen;
var txtTramaSubida;
var txtTramaVersion;
var hdPaqueteId;
var hdIsSubir;
var txtBusIPRESSCodigo;
var txtBusIPRESSDscp;
var RETURN_LOGEO;

function jsf_Control() {
    //txtTramaIPRESS = jsf_Crear("txtTramaIPRESS");
    //txtTramaAppOrigen = jsf_Crear("txtTramaAppOrigen");
    //txtTramaSubida = jsf_Crear("txtTramaSubida");
    //txtTramaVersion = jsf_Crear("txtTramaVersion");
    //hdPeriodo = jsf_Crear("hdPeriodo");
    //hdCronograma = jsf_Crear("hdCronograma");
    //hdPaqueteId = jsf_Crear("hdPaqueteId");
    //hdIsSubir = jsf_Crear("hdIsSubir");
    //txtBusIPRESSCodigo = jsf_Crear("txtBusIPRESSCodigo");
    //txtBusIPRESSDscp = jsf_Crear("txtBusIPRESSDscp");
}


function jsf_Crear(obj) {

    return document.getElementById(obj);

}
var $btnAceptar;
var $imgNuevo;
var $imgGuardar;
var $imgEliminar;
var $imgSalir;
var $imgAyuda;
var $btnListarPaquetes;
//var $hdserializarregistro;
var hdPeriodo;
var hdCronograma;
var isCorrecto;

//function isSubir() {
//    //return ($.trim($("#spanNota").text()).length > 0 ? false : true);
//    return (hdIsSubir.value == "S");
//}

var p_file_title_subir = "Seleccionar el archivo que contiene la trama de prestaciones";
var p_file_title_view = "Para subir el mismo paquete debe eliminarlo y subirlo nuevamente";

$(function () {
    jsf_Control();
    $("#p_file_title").attr("title", p_file_title_subir);

    //$hdserializarregistro = $("#hdserializarregistro");
 //   isCorrecto = isSubir();
    $imgNuevo = $("#imgNuevo").click(function (e) { e.preventDefault(); window.location = "frmRecepcion.aspx"; });

    $("#imgListadoBusqueda").click(function (e) { 
        e.preventDefault();
        $imgGuardar.attr("disabled", "disabled");
        //$imgEliminar.attr("disabled", "disabled");
        $("#paq_ges").hide(); $("#paq_bus").show();
    });

    if (isCorrecto) {
        $imgGuardar = $("#imgGuardar").click(function (e) { onClickGuardar(e) });
        $imgEliminar = $("#imgEliminar").click(function (e) { onClickEliminar(e) });
        $("#fuParticipantes").change(function () {
            var valor = $(this).val();
            if (valor != "") {
                if (valor.split('.').pop().toUpperCase() == "ZIP") {
                    var fileName = valor.match(/[^\/\\]+$/)[0];
                    if (fileName.length > 31)
                        fileName = fileName.substring(0, 31) + '...';
                    $("#lblFUParticipantes").html(fileName);
                } else {
                    alert("Por favor, seleccione un archivo en formato ZIP.");
                    $("#lblFUParticipantes").html("Seleccionar archivo ...");
                    $(this).val('');
                }
            }
        });
    } else {
        $imgGuardar = $("#imgGuardar").attr("disabled", "disabled");
        $imgEliminar = $("#imgEliminar").attr("disabled", "disabled");
        $("#fuParticipantes").remove();
        $("#lblFUParticipantes").addClass("bloquear");
    }
    
    $("#imgSalirJS").click(function (e) { e.preventDefault(); $("#imgSalir").trigger("click"); });
    $btnListarPaquetes = $("#btnListarPaquetes").click(function (e) { onClickBuscar(e) });

    $btnAceptar = $("#btnAceptar").click(function (e) {
        e.preventDefault();
        if (!$(this).hasClass("disabled"))
            jsf_verDatos();
    });

    $("input[type=checkbox], input[type=radio]")
    .focus(function (event) {
        if ($(this).parent().hasClass("div_checkbox"))
            $(this).parent().css({ "background-color": jsf_celeste(), "border-color": "#6f6f6f" });
    })
    .focusout(function (event) {
        if ($(this).parent().hasClass("div_checkbox"))
            $(this).parent().css({ "background-color": "transparent", "border-color": "transparent" });
    });

    //iniControlesPanel();
    //configurarEstiloNavegador();

    $('body').fadeIn('200', function () {
        /*if (document.Form1.txtCodFormato.disabled) {
        document.Form1.txtLote.focus();
        } else {
        document.Form1.txtCodFormato.focus();
        }*/
    }).keydown(function (e) {
        kGrabar(e);
    });

    //var crono_ini = cadenaFecha($("#txtCronoInicio").val());
    //if (crono_ini.length > 0)
    //    $("#txtCronoInicio").attr('title', crono_ini).addClass('mostrar_ayuda');

    //var crono_fin = cadenaFecha($("#txtCronoFin").val());
    //if (crono_fin.length > 0)
    //    $("#txtCronoFin").attr('title', crono_fin).addClass('mostrar_ayuda');

    //$(".mostrar_ayuda, #toolbar input[type=image], #toolbar #ibElimina").tooltip({ trigger: "hover", container: "body" });
    
    cargarPeriodos();
    
    //$("#ddlGMR").OutEvent(function ($c) { onChangeDisa("DISAS_X_GMR", "", $c, $("#ddlDISA"), "#ddlUDR, #ddlUE", "#txtBusIPRESSCodigo, #txtBusIPRESSDscp"); });
    //$("#ddlDISA").OutEvent(function ($c) { onChangeDisa("UDRS_X_DISA", $("#ddlGMR").val(), $c, $("#ddlUDR"), "#ddlUE", "#txtBusIPRESSCodigo, #txtBusIPRESSDscp"); });
    //$("#ddlUDR").OutEvent(function ($c) {  onChangeDisa("UUEES_X_UDR", $("#ddlDISA").val(), $c, $("#ddlUE"), "", "#txtBusIPRESSCodigo, #txtBusIPRESSDscp"); });
    //$("#ddlUE").OutEvent(function ($c) { txtBusIPRESSCodigo.value = ""; txtBusIPRESSDscp.value = ""; });
});

function onChangeDisa(flag, valorTopPadre, $cbbPadre, $cbbHijo, strCbbBlanquear, strTxtBlanquear) {
    var codActual = $.trim($cbbPadre.val());
    $cbbHijo.html("");
    $(strCbbBlanquear).html("");
    $(strTxtBlanquear).val("");
    if (codActual != "0" && codActual.length > 0) {
        $cbbPadre.attr("disabled", "disabled");
        $.ajax({
            data: $.toJSON({ flag: flag, param1: valorTopPadre, param2: codActual, param3: "" }),
            url: '../Recepcion/frmRecepcion.aspx/ObtenerOrganizaciones',
            type: 'post',
            contentType: "application/json; charset=iso-8859-1",
            dataType: 'json',
            beforeSend: function (data) { },
            success: function (data) {
                if (data != null) {
                    if ($.trim(data.d).length > 0) {
                        var datos = data.d.split("$");
                        var cant = datos.length - 1;
                        if (cant > 0) {
                            $cbbHijo.html('<option value="0" selected="selected">-- TODOS --</option>')
                            for (var i = 0; i < cant; i++) {
                                $cbbHijo.append('<option value="' + datos[i] + '">' + datos[i + 1] + '</option>');
                                i += 1;
                            }
                        }
                    }
                    $cbbPadre.removeAttr("disabled");
                }
            },
            error: function (data) {
                /*$cbbHijo.html("");                
                $(strCbbBlanquear).html("");
                $(strTxtBlanquear).val("");*/
                $cbbPadre.removeAttr("disabled");
                alert("Ocurrio un error con la respuesta del servidor!");
            }
        });
    } /*else {
        $cbbHijo.html("");
        $(strCbbBlanquear).html("");
        $(strTxtBlanquear).val("");
    }*/
}

function cargarPeriodos() {
    var anio = $("#txtPeriodo").val();
    var mes = $("#txtMesCodigo").val();
    $("#cbbBusMes").val(mes);
    var strAnios = "";
    for (var i = anio; i >= 2016; i--) {
        strAnios += '<option value="' + i + '">' + i + '</option>';
    }
    $("#cbbBusPeriodo").html(strAnios);
}

function kGrabar(e) {
    //var codigo = getCodigoKey(e);
    //if (codigo == KEY_F9) {
    //    $imgGuardar.focus();
    //} else if (codigo == KEY_F2) {
    //    $imgNuevo.focus();
    //} else if (codigo == KEY_ESC) {
    //    $imgSalir.focus();
    //} else if (codigo == KEY_F1) {
    //    $imgAyuda.focus();
    //}
}

function onClickGuardar(e) {
    e.preventDefault();
    hdPaqueteId.value = '';
    //$hdserializarregistro.val('');
    $imgGuardar.attr('disabled', 'disabled');
    var rpta = jsf_ValidarForm('1');
    if (rpta === false) {
        $imgGuardar.removeAttr('disabled').focus();
    } else {  
        mostrarCargaExterna();
        jsf_GuardarPaquete();
    }
}

function jsf_GuardarPaquete() {
    mostrarCargaExterna();
    var files = $("#fuParticipantes").get(0).files;
    var params = new FormData();
    params.append(files[0].name, files[0]);
    params.append('accion', 'I');
    params.append('p', hdPeriodo.value);
    params.append('c', hdCronograma.value);
    guardarPaquete(params);
}

function guardarPaquete(parametros) {
    $.ajax({
        url: "../Controller/RecepcionHandler.ashx",
        type: "POST",
        data: parametros,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response == "000000000") {
                window.location = RETURN_LOGEO;
            } else {
                var resul = new Array();
                resul = response.split("$");
                var str_mensaje = resul[0].toString();
                if (str_mensaje == "1") {
                    var msj = resul[2];
                    hdPaqueteId.value = resul[1];
                    txtTramaIPRESS.value = resul[3];
                    txtTramaAppOrigen.value = resul[4];
                    txtTramaSubida.value = resul[5];
                    txtTramaVersion.value = resul[6];
                    var detalleIndice = 7;
                    var detalleRegistros = resul[detalleIndice];
                    var $tbody = $("#tblPaqueteDetalle tbody");
                    var strTbody = "";
                    if (detalleRegistros > 0) {
                        detalleRegistros = detalleRegistros * 5;
                        for (var i = 1; i <= detalleRegistros; i++) {
                            strTbody += '<tr class="tbody_mini_registros_tr"><td>&nbsp;</td><td class="nombre">' + resul[i + detalleIndice] + '</td><td>' + resul[i + detalleIndice + 1] + '</td><td>' + resul[i + detalleIndice + 2] + '</td><td>' + resul[i + detalleIndice + 3] + '</td><td class="diferencia_' + (resul[i + detalleIndice + 4] == 'S' ? 'si">SI' : 'no">NO') + '</td><td>&nbsp;</td></tr>';
                            i = i + 4;
                        }
                    }
                    $tbody.html(strTbody);
                    $("#td_foot_mensaje").addClass("info").removeClass("error").text(msj);
                    alert(msj);
                } else if (str_mensaje == "0") {
                    //alert(resul[1]);
                    $("#td_foot_mensaje").removeClass("info").addClass("error").text(resul[1]);
                } else {
                    //alert(resul[0]);
                    $("#td_foot_mensaje").removeClass("info").addClass("error").text(resul[0]);
                }
            }
            removerCargaExterna();
            $imgGuardar.removeAttr('disabled').focus();
        }
    });
}

function onClickEliminar(e) {
    e.preventDefault();
    $imgEliminar.attr('disabled', 'disabled');
    var rpta = jsf_ValidarForm('2');
    if (rpta === false) {
        $imgEliminar.removeAttr('disabled').focus();
    } else {
        mostrarCargaExterna();
        jsf_EliminarPaquete();
    }
}

function jsf_EliminarPaquete() {
    mostrarCargaExterna();
    var params = new FormData();
    params.append('accion', 'D');
    params.append('p', hdPaqueteId.value);
    params.append('m', 'abc ');
    eliminarPaquete(params);
}

function eliminarPaquete(parametros) {
    $.ajax({
        url: "../Controller/RecepcionHandler.ashx",
        type: "POST",
        data: parametros,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response == "000000000") {
                window.location = RETURN_LOGEO;
            } else {
                var resul = new Array();
                resul = response.split("$");
                var str_mensaje = resul[0].toString();
                if (str_mensaje == "1") {
                    var msj = resul[1];
                    hdPaqueteId.value = '';
                    $("#tblPaqueteDetalle tbody").html('<tr class="tbody_mini_registros_tr"><td>&nbsp;</td><td class="nombre" colspan="6">No se ha cargado ningun paquete</td></tr>');
                    $("#td_foot_mensaje").addClass("info").removeClass("error").text(msj);
                    alert(msj);
                    txtTramaIPRESS.value = "";
                    txtTramaAppOrigen.value = "";
                    txtTramaSubida.value = "";
                    txtTramaVersion.value = "";
                    var $file = $("#fuParticipantes");
                    clearInput($file);
                    $("#lblFUParticipantes").text("Seleccionar archivo ...")
                    /*if (/MSIE/.test(navigator.userAgent)) {
                        $file.replaceWith($file.clone(true));
                    } else {
                        $file.val('');
                    }*/
                } else if (str_mensaje == "0") {
                    alert(resul[1]);
                    $("#td_foot_mensaje").removeClass("info").addClass("error").text(resul[1]);
                } else {
                    alert(resul[0]);
                    $("#td_foot_mensaje").removeClass("info").addClass("error").text(resul[0]);
                }
            }
            removerCargaExterna();
            $imgEliminar.removeAttr('disabled').focus();
        }
    });
}

function onClickBuscar(e) {
    e.preventDefault();
    $btnListarPaquetes.attr('disabled', 'disabled');
    var rpta = jsf_ValidarForm('3');
    if (rpta === false) {
        $btnListarPaquetes.removeAttr('disabled').focus();
    } else {
        var Filtro_flag = "";
        var Filtro = $.trim(txtBusIPRESSCodigo.value);
        if (Filtro.length == 0) {
            Filtro = $.trim($("#ddlUE").val());
            if (Filtro.length > 0 && Filtro != "0") {
                Filtro_flag = "UUEE";
            } else {
                Filtro = $.trim($("#ddlUDR").val());
                if (Filtro.length > 0 && Filtro != "0") {
                    Filtro_flag = "UDR"
                } else {
                    Filtro = $.trim($("#ddlDISA").val());
                    if (Filtro.length > 0 && Filtro != "0") {
                        Filtro_flag = "DISA";
                    } else {
                        Filtro = $.trim($("#ddlGMR").val());
                        if (Filtro.length > 0 && Filtro != "0") {
                            Filtro_flag = "GMR";
                        } else {
                            alert("Por favor seleccionar al menos alguna GMR");
                            return false;
                        }
                    }
                }
            }
        } else {
            if (Filtro.length == 10) {
                Filtro_flag = "EESS";
            } else {
                alert("Por favor seleccionar completar el código de la IPRESS");
                txtBusIPRESSCodigo.focus();
                return false;
            }
        }

        if (Filtro_flag.length == 0 || Filtro.length == 0) {
            alert("Por favor seleccionar alguna jerarquía de búsqueda");
            return false;
        }
        mostrarCargaExterna();
        jsf_BuscarPaquete(Filtro_flag, Filtro);
    }
}

function jsf_BuscarPaquete(Filtro_flag, Filtro) {
    mostrarCargaExterna();
    var params = new FormData();
    params.append('accion', 'S');
    params.append('f', Filtro_flag);
    params.append('c', Filtro); /*
    params.append('g', $("#ddlGMR").val());
    params.append('d', $("#ddlDISA").val());
    params.append('u', $("#ddlUDR").val());
    params.append('e', $("#ddlUE").val());
    params.append('i', $("#txtBusIPRESSCodigo").val());*/
    params.append('p', $("#cbbBusPeriodo").val());
    params.append('m', $("#cbbBusMes").val());
    buscarPaquete(params);
}

function buscarPaquete(parametros) {
    $.ajax({
        url: "../Controller/RecepcionHandler.ashx",
        type: "POST",
        data: parametros,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response == "000000000") {
                window.location = RETURN_LOGEO;
            } else {
                var resul = new Array();
                resul = response.split("$");
                var str_mensaje = resul[0].toString();
                if ($.isNumeric(str_mensaje)) {
                    var detalleRegistros = resul[0];
                    var $tbody = $("#tblPaqueteBusqueda tbody");
                    var strTbody = "";
                    if (detalleRegistros > 0) {
                        detalleRegistros = detalleRegistros * 8;
                        for (var i = 1; i <= detalleRegistros; i++) {
                            strTbody += '<tr class="tbody_mini_registros_tr"><td>&nbsp;</td><td><a href="#" title="Ver detalle" class="a_view mostrar_ayuda" data-value="' + resul[i] + '"></a></td><td class="nombre">' + resul[i + 1] + '</td><td>' + resul[i + 2] + '</td><td>' + resul[i + 3] + '</td><td>' + resul[i + 4] + '</td><td>' + resul[i + 5] + '</td><td>' + resul[i + 6] + '</td><td>' + resul[i + 7] + '</td></tr>';
                            i = i + 7;
                        }
                    }
                    $tbody.html(strTbody);
                    $("#td_busqueda_foot_mensaje").addClass("info").removeClass("error").text("Se han encontrado (" + resul[0] + ") registros.");
                    //$tbody.find("a.a_view").click(function (e) { onClickVerPaquete(e, $(this));  });
                    onClickVerPaquete($tbody.find("a.a_view"));
                    $tbody.find(".mostrar_ayuda").tooltip({ trigger: "hover", container: "body" });                    
                } else {
                    $("#td_busqueda_foot_mensaje").removeClass("info").addClass("error").text(resul[0]);
                }
            }
            removerCargaExterna();
            $btnListarPaquetes.removeAttr('disabled').focus();
        }
    });
}

var isNotClick_btnEliminar = true;

function onClickVerPaquete($boton) {
    $boton
    .click(function (e) {
        isNotClick_btnEliminar = false;
        $(this).parent().parent().addClass("mpi_tr_mini_registro_foco");
        verPaquete(e, $(this));       
        isNotClick_btnEliminar = true;
    })
    .keydown(function (e) {
        var $tr = $(this).parent().parent();
        var codigo = getCodigoKey(e);
        if (codigo == KEY_UP) {
            e.preventDefault();
            $tr.prev().find(".a_view").focus();
        } else if (codigo == KEY_DOWN) {
            e.preventDefault();
            $tr.next().find(".a_view").focus();
        }
    })
    .focusin(function (e) {
        $(this).parent().parent().addClass("mpi_tr_mini_registro_foco");
    })
    .focusout(function (e) {
        if (isNotClick_btnEliminar) $(this).parent().parent().removeClass("mpi_tr_mini_registro_foco");
    });
}

var progress_view = 0;

function verPaquete(e, $a) {
    e.preventDefault();
    var idPaq = $a.attr("data-value");
    if (progress_view == 0) {
        progress_view = 1;
        mostrarCargaExterna();
        var params = new FormData();
        params.append('accion', 'V');
        params.append('p', idPaq);
        mostrarPaquete(params);        
    }
}

function mostrarPaquete(parametros) {
    $.ajax({
        url: "../Controller/RecepcionHandler.ashx",
        type: "POST",
        data: parametros,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response == "000000000") {
                window.location = RETURN_LOGEO;
            } else {
                var resul = new Array();
                resul = response.split("$");
                var str_mensaje = resul[0].toString();
                if (str_mensaje == "1") {
                    var msj = resul[2];
                    hdPaqueteId.value = resul[1];
                    txtTramaIPRESS.value = resul[3];
                    txtTramaAppOrigen.value = resul[4];
                    txtTramaSubida.value = resul[5];
                    txtTramaVersion.value = resul[6];
                    
                    var $file_arc = $("#fuParticipantes").attr("disabled", "disabled").hide();
                    $("#lblFUParticipantes").addClass("bloquear");

                    var isUpd = resul[7];
                    if (isUpd == "S") {
                        $file_arc.removeClass("paq_procesado");
                        /*if (isSubir()) {
                            $imgEliminar.removeAttr("disabled");
                        }*/
                    } else {
                        $file_arc.addClass("paq_procesado");
                        //$imgEliminar.attr("disabled", "disabled");
                    }
                    
                    $("#p_file_title").attr("data-original-title", p_file_title_view);

                    $("#lblFUParticipantes").text(resul[8]);
                    var detalleIndice = 9;
                    var detalleRegistros = resul[detalleIndice];
                    var $tbody = $("#tblPaqueteDetalle tbody");
                    var strTbody = "";
                    if (detalleRegistros > 0) {
                        detalleRegistros = detalleRegistros * 5;
                        for (var i = 1; i <= detalleRegistros; i++) {
                            strTbody += '<tr class="tbody_mini_registros_tr"><td>&nbsp;</td><td class="nombre">' + resul[i + detalleIndice] + '</td><td>' + resul[i + detalleIndice + 1] + '</td><td>' + resul[i + detalleIndice + 2] + '</td><td>' + resul[i + detalleIndice + 3] + '</td><td class="diferencia_' + (resul[i + detalleIndice + 4] == 'S' ? 'si">SI' : 'no">NO') + '</td><td>&nbsp;</td></tr>';
                            i = i + 4;
                        }
                    }
                    $tbody.html(strTbody);
                    $("#td_foot_mensaje").addClass("info").removeClass("error").text(msj);

                    $("#paq_ges").show(); $("#paq_bus").hide();                    

                    //alert(msj);
                } else if (str_mensaje == "0") {
                    alert(resul[1]);
                    //$("#td_foot_mensaje").removeClass("info").addClass("error").text(resul[1]);
                } else {
                    alert(resul[0]);
                    //$("#td_foot_mensaje").removeClass("info").addClass("error").text(resul[0]);
                }
                removerCargaExterna();
                //$imgGuardar.removeAttr('disabled').focus();
                progress_view = 0;
            }
        },        
        error: function (data) {
            removerCargaExterna();
            //$("#paq_ges").show(); $("#paq_bus").hide();
            progress_view = 0;
            alert(MSG_ERROR_AJAX);
        }
    });
}

function clearInput($source) {
    var $form = $('<form>');
    var $targ = $source.clone().appendTo($form);
    $form[0].reset();
    $source.replaceWith($targ);
}

function jsf_ValidarForm(tipo) {
    if (tipo == "1") {
        var $file_arc = $("#fuParticipantes");
        if ($file_arc.hasClass("paq_procesado")) {
            alert("El paquete ya ha sido procesado.");
            return false;
        }
        var arch_cant = $file_arc.get(0).files.length;
        if (arch_cant == 0) {
            alert("Por favor seleccione el archivo que contiene la trama de prestaciones.");
            //$("#fuParticipantes").focus();
            return false;
        } else if (arch_cant > 1) {
            alert("Por favor seleccionar solamente un archivo que contiene la trama de prestaciones.");
            //$("#fuParticipantes").focus();
            return false;
        }
    } else if (tipo == "2") {
        var codigo = $.trim(hdPaqueteId.value);
        if (codigo.length == 0) {
            alert("No se está visualizando ningún paquete, por favor vuelva a cargar el paquete.");
            return false;
        } else if (!$.isNumeric(codigo)) {
            alert("No se está cargando correctamente el paquete, por favor vuelva a cargar el paquete.");
            return false;
        }
        var $file_arc = $("#fuParticipantes");
        if ($file_arc.hasClass("paq_procesado")) {
            alert("El paquete no se puede eliminar porque ya ha sido procesado.");
            return false;
        }
        return confirm("¿Está seguro de eliminar el paquete subido?");
    } else if (tipo == "3") {
      //BUSCAR PAQUETE
    }
    return true;
}

function wEESS() {    
    var Filtro = $.trim($("#ddlUE").val());
    if (Filtro.length > 0 && Filtro != "0") {
        Filtro = "UUEE$" + Filtro
    } else {
        Filtro = $.trim($("#ddlUDR").val());
        if (Filtro.length > 0 && Filtro != "0") {
            Filtro = "UDR$" + Filtro
        } else {
            Filtro = $.trim($("#ddlDISA").val());
            if (Filtro.length > 0 && Filtro != "0") {
                Filtro = "DISA$" + Filtro
            } else {
                Filtro = $.trim($("#ddlGMR").val());
                if (Filtro.length > 0 && Filtro != "0") {
                    Filtro = "GMR$" + Filtro
                } else {
                    alert("Por favor seleccionar al menos alguna GMR");
                    return false;
                }
            }
        }
    }
    if (txtBusIPRESSCodigo.disabled == false) {
        var scad = "";
        var Filtro;
        Filtro = "EESS_BU$" + Filtro;
        scad += "../consultas/consulta.aspx?nCnx=0&nSQL=0&FiltroInicio=" + Filtro;
        scad += "&Criterio=0";
        scad += "&AnchoCol=30,60,60,60,250,30,40,250";
        scad += "&Alinea=1,1,0,0,0,0,0,0";
        scad += "&Formato=0,0,0,0,0,0,0,0";
        scad += "&Campos=txtBusIPRESSCodigo,txtBusIPRESSDscp";
        scad += "&Tpag=20";
        //scad += "&val=3,4,6,2";
        scad += "&val=4,5,7,2";
        scad += "&ncol=DISA$Und.Ejec.$COD. SIS$RENAES$Nombre$Cat$Abrv.$Cat.Nombre";
        scad += "&foco=jsf_FocoEESS()";
        window.open(scad, "wEESS", "top=0, left=0, width=685,height=500,menubar=no,scrollbars=yes, resizable =yes");
    }
}

function kEESS(e, Filtro) {
    if (e.keyCode == 118)
        wEESS();
}

function jsf_FocoEESS() {
    if (txtBusIPRESSCodigo.disabled == false)
        txtBusIPRESSCodigo.focus();
}

function jsf_CompletarCerosEESS(control, numero) {
    if (control.value.length < 7 && control.value.length > 0) {
        var i = 0;
        var ceros = "";
        for (i = 0; i < numero; i++) {
            ceros += "0";
        }
        control.value = jsf_Right((ceros + control.value), numero);
    }
    return;
}

function jsf_DatosEESS1(flag, valorTopPadre, $cbbPadre, $cbbHijo, strCbbBlanquear, strTxtBlanquear) {
    var Filtro_flag = "";
    var Filtro = $.trim($("#ddlUE").val());
    if (Filtro.length > 0 && Filtro != "0") {
        Filtro_flag = "UUEE";
    } else {
        Filtro = $.trim($("#ddlUDR").val());
        if (Filtro.length > 0 && Filtro != "0") {
            Filtro_flag = "UDR"
        } else {
            Filtro = $.trim($("#ddlDISA").val());
            if (Filtro.length > 0 && Filtro != "0") {
                Filtro_flag = "DISA";
            } else {
                Filtro = $.trim($("#ddlGMR").val());
                if (Filtro.length > 0 && Filtro != "0") {
                    Filtro_flag = "GMR";
                } else {
                    alert("Por favor seleccionar al menos alguna GMR");
                    return false;
                }
            }
        }
    }
    if (Filtro_flag.length == 0 || Filtro.length == 0) {
        alert("Por favor seleccionar alguna jerarquía de búsqueda");
        return false;
    }
    if (jsf_Trim(txtBusIPRESSCodigo.value).length >= 7) {
        $.ajax({
            data: $.toJSON({ flag: 'EESS_BU_COD', param1: Filtro_flag, param2: Filtro, param3: txtBusIPRESSCodigo.value }),
            url: '../Recepcion/frmRecepcion.aspx/ObtenerOrganizaciones',
            type: 'post',
            contentType: "application/json; charset=iso-8859-1",
            dataType: 'json',
            beforeSend: function (data) { },
            success: function (data) {
                if (data != null) {
                    if ($.trim(data.d).length > 0) {
                        var arr = new Array();
                        arr = data.d.split("$");
                        if ($.trim(arr[1]).length > 0) {
                            txtBusIPRESSCodigo.style.backgroundColor = jsf_blanco();
                            txtBusIPRESSCodigo.value = arr[0];
                            txtBusIPRESSDscp.value = arr[1];
                            return;
                        }
                    }
                    txtBusIPRESSCodigo.style.backgroundColor = jsf_rojo();
                    txtBusIPRESSDscp.value = "";
                    var Filtro = $.trim($("#ddlUE").val());
                    if (Filtro.length > 0 && Filtro != "0") {
                        Filtro = "Unidad Ejecutora";
                    } else {
                        Filtro = $.trim($("#ddlUDR").val());
                        if (Filtro.length > 0 && Filtro != "0") {
                            Filtro = "UDR";
                        } else {
                            Filtro = $.trim($("#ddlDISA").val());
                            if (Filtro.length > 0 && Filtro != "0") {
                                Filtro = "DISA";
                            } else {
                                Filtro = $.trim($("#ddlGMR").val());
                                if (Filtro.length > 0 && Filtro != "0") {
                                    Filtro = "GMR";
                                } else {
                                    Filtro = "";
                                }
                            }
                        }
                    }
                    if (Filtro.length > 0 )
                        Filtro = "El código de la IPRESS no existe o no pertenece a la " + Filtro + ".";
                    else
                        Filtro = "El código de la IPRESS no existe.";
                    alert(Filtro);                    
                }
            },
            error: function (data) {
                alert("Ocurrio un error con la respuesta del servidor!");
                txtBusIPRESSCodigo.style.backgroundColor = jsf_rojo();
                txtBusIPRESSDscp.value = "";
            }
        });
    } else if (jsf_Trim(txtBusIPRESSCodigo.value).length > 0) {
        txtBusIPRESSCodigo.style.backgroundColor = jsf_rojo();
        txtBusIPRESSDscp.value = "";
    } else {
        txtBusIPRESSCodigo.style.backgroundColor = jsf_blanco();
        txtBusIPRESSDscp.value = "";
    }
}

//**** VERSIONADO

//var jsf_version = 1; //20160216
//var jsf_version = 2; //20160502
var jsf_version = 3; //20170802