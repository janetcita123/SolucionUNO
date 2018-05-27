<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="prueba.aspx.vb" Inherits="SolucionCalendario.prueba" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="bootstrap/css/font-awesome.min.css" rel="stylesheet" />
    <link href="bootstrap/css/calendar.css" rel="stylesheet" />
    <link href="bootstrap/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="bootstrap/css/font-awesome.min-3.2.1.css" rel="stylesheet" />
    <link href="bootstrap/css/funkyradio.css" rel="stylesheet" />
    <link href="typehead/scroll.css" rel="stylesheet" />
    <link href="bootstrap/css/fileinput.min.css" rel="stylesheet" />
    <link href="bootstrap/css/fileinput.css" rel="stylesheet" />

    <script src="bootstrap/js/jquery.min.js"></script>
    <script src="bootstrap/js/es-ES.js"></script>
    <script src="bootstrap/js/moment.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="bootstrap/js/bootstrap-datetimepicker.js"></script>
    <script src="bootstrap/js/bootstrap-datetimepicker.es.js"></script>
    <script src="bootstrap/js/underscore-min.js"></script>
    <script src="bootstrap/js/calendar.js"></script>
    <script src="typehead/bootstrap3-typeahead.min.js"></script>
    <script src="pdfConverter/fileinput.min.js"></script>
    <script src="pdfConverter/fileinput.js"></script>
    <script>
        var sUsuario = 'PCORONADOF';
        var usuario = [];
        var rol = [];
        var idAcuerdoGen = '';
        var idEventoGeneral = '';
        var x = 'hola';
        var $btnEditar = 0;
        var $btnFinalizar = 0;
        var $btnAcuerdos = 0;
        var $btnPdf = 0;
        var $btnAgendas = 0;
        var $blockCheck = 0;
        var $reprogramar = 0;
        var $creador = 0;

        usuario = $.parseJSON('<%=jsUsuario%>');

        rol = $.parseJSON('<%=jsRol%>');

        var jsEventos = $.parseJSON('<%=jsEventos%>');
       // alert(jsEventos);
        for (var i = 0; i < jsEventos.result.length; i++) {
            jsEventos.result[i].start = jsEventos.result[i].start.replace("/Date(", "").replace(")/", "");
            jsEventos.result[i].end = jsEventos.result[i].end.replace("/Date(", "").replace(")/", "");

        }
        if (jsEventos.success) {
            jsEventos = jsEventos.result
            for (var i = 0; i < jsEventos.length; i++) {
                jsEventos[i].creador = 0;
                jsEventos[i].rol = rol;
            }

            for (var i = 0; i < jsEventos.length; i++) {
                if (jsEventos[i].usuario == usuario) {
                    jsEventos[i].creador = 1;
                }
            }

        } else {
            jsEventos = jsEventos.error;
        }
        var date = new Date();
        $(function () {
            $("[data-toggle='tooltip']").tooltip();
        });


        $(function () {
            selectEvento(0);
            var URLactual = window.location.href
            var pathname = window.location.pathname;
            var ruta = URLactual.replace(pathname, '/');

            $('#from2,#de2,#hasta2').datetimepicker({
                locale: 'es',
                minDate: date,
                format: 'MM-DD-YYYY HH:mm A'
            });
            $('#from,#de,#hasta').datetimepicker({
                locale: 'es',
                minDate: date,
                format: 'MM-DD-YYYY HH:mm'
            });


            $('#to2').datetimepicker({
                locale: 'es',
                minDate: date,
                format: 'MM-DD-YYYY HH:mm A'
            });

            $('#to').datetimepicker({
                locale: 'es',
                minDate: date,
                format: 'MM-DD-YYYY HH:mm'
            });

            var yyyy = date.getFullYear().toString();
            var mm = (date.getMonth() + 1).toString().length == 1 ? "0" + (date.getMonth() + 1).toString() : (date.getMonth() + 1).toString();
            var dd = (date.getDate()).toString().length == 1 ? "0" + (date.getDate()).toString() : (date.getDate()).toString();

            //establecemos los valores del calendario
            var options = {
                modal: '#events-modal',
                modal_type: 'template',
                language: 'es-ES',
                //ponemos el objeto con los eventos
                events_source: jsEventos,
                view: 'month',

                // y dia actual
                day: yyyy + "-" + mm + "-" + dd,
                //day: '2017-12-05',
                //// definimos el idioma por defecto
                language: 'es-ES',

                //Template de nuestro calendario
                tmpl_path: ruta + 'bootstrap/tmpls/',
                //tmpl_path: '<ajax/Accion.aspx',
                tmpl_cache: false,


                // Hora de inicio
                time_start: '08:00',

                // y Hora final de cada dia
                time_end: '22:00',

                // intervalo de tiempo entre las hora, en este caso son 30 minutos
                time_split: '30',

                // Definimos un ancho del 100% a nuestro calendario
                width: '100%',

                onAfterEventsLoad: function (events) {
                    if (!events) {
                        return;
                    }

                    var list = $('#eventlist');
                    list.html('');

                    $.each(events, function (key, val) {

                        $(document.createElement('li'))
                            .html('<a href="' + val.Id + '">' + val.title + '</a>')
                                //.html('<a href="' + val.url + '">' + val.title + '</a>')
                                .appendTo(list);
                    });
                },
                onAfterViewLoad: function (view) {
                    var mes = this.getTitle();
                    $('.page-header h2').text(mes);
                    $('.btn-group button').removeClass('active');
                    $('button[data-calendar-view="' + view + '"]').addClass('active');
                },
                classes: {
                    months: {
                        general: 'label'
                    }
                }

            };
            // id del div donde se mostrara el calendario
            var calendar = $('#calendar').calendar(options);

            $('.btn-group button[data-calendar-nav]').each(function () {
                var $this = $(this);
                $this.click(function () {
                    calendar.navigate($this.data('calendar-nav'));
                });
            });

            $('.btn-group button[data-calendar-view]').each(function () {
                var $this = $(this);
                $this.click(function () {
                    calendar.view($this.data('calendar-view'));
                });
            });

            $('#first_day').change(function () {
                var value = $(this).val();
                value = value.length ? parseInt(value) : null;
                calendar.setOptions({ first_day: value });
                calendar.view();
            });

            function arreglarfecha(fecha) {
                //fecha = '12-07-2017 09:44 AM';
                var cadena = '';
                cadena = fecha.substring(0, 5);
                cadena = cadena.substring(3, 5) + '-' + cadena.substring(0, 2);
                fecha = fecha.substring(5, fecha.length);
                fecha = cadena + fecha
                return fecha;
            }


            $('#subeActa').click(function () {
                var ele = $('#url');
                archivo = $('#url')[0].title;
                var nombre = ele[0].innerText;
                nombre = nombre.trim();
                alert(nombre);
                var xdata = '{ruta: "' + archivo + '",archivo:"' + nombre + '"}';
                $.ajax({
                    type: "POST",
                    url: "CalendarioIndex.aspx/SubirArchivo",
                    data: xdata,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        response = response.d;
                        alert(response);
                        return false
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });



            $('#pdf').click(function () {
                var items = [];
                idEventoGeneral = document.getElementById('idEvento');
                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEventoGeneral.getAttribute('value')) {
                        items = jsEventos[i];
                    }
                }

                $.ajax({
                    type: "POST",
                    url: "CalendarioIndex.aspx/SubmitItems",
                    //data: '{items:"' + items + '"}',
                    data: JSON.stringify({ items: items }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        response = response.d;
                        window.open('pdfNew.aspx', '_blank');
                        //location.reload();
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });

            });

            //Agregar evento nuevo ini
            $('#addEvent').click(function () {
                //cabecera                  
                var fInicio = $('#from2').val();
                fInicio = arreglarfecha(fInicio);
                var fFinal = $('#to2').val();
                fFinal = arreglarfecha(fFinal);
                var sTipo = $('.combo').val();
                var sOrganiza = $('.comboare').val();
                var sTitle = $('#title').val().toUpperCase();
                var sBody = $('#body').val().toUpperCase();
                //detalles
                var sAreas = '';
                $('#lstAreas input:checked').each(function () {
                    sAreas = sAreas + $(this).attr('value') + '|';
                });
                var sEntidades = '';
                $('#lstEntidases input:checked').each(function () {
                    sEntidades = sEntidades + $(this).attr('value') + '|';
                });
                var sPersonas = '';

                $("#personal tbody tr").each(function (index) {
                    $(this).find("td input:checkbox,td select").each(function () {
                        if ($(this).attr("name") == 'part') {
                            sPersonas = (sPersonas + this.value + '|').replace('', '');
                        } //inputName = $(this).attr("name");                         

                    });

                });
                var sAgenda = '';
                $("#tbAgenda tbody tr").each(function (index) {
                    $(this).find("td,td select").each(function () {
                        if ($(this).attr("name") == 'tdagenda') {
                            sAgenda = sAgenda + this.innerHTML + '|';
                            sAgenda = sAgenda + this.title + '*';
                        } //inputName = $(this).attr("name");                         

                    });

                });


                $.ajax({
                    type: "POST",
                    url: "CalendarioIndex.aspx/GuardarEvento",
                    data: '{fInicio: "' + fInicio + '",fFinal:"' + fFinal + '",sTipo:"' + sTipo + '",sOrganiza:"' + sOrganiza + '",sTitle:"' + sTitle + '",sBody:"' + sBody + '",sAreas:"' + sAreas + '",sEntidades:"' + sEntidades + '",sPersonas:"' + sPersonas + '",sAgenda:"' + sAgenda + '",sUsuario:"' + sUsuario + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        $("#tbAgenda tbody").empty();
                        $(".personal tbody").empty();
                        location.reload();
                        //alert(response.d);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });


            });
            //Agregar evento nuevo fin

            //Editar Evento
            $('#setEvent').click(function () {

                var idEvento = idEventoGeneral.getAttribute('value');
                //cabecera                  

                var fInicio = $('#from2').val();
                fInicio = arreglarfecha(fInicio);
                var fFinal = $('#to2').val();
                fFinal = arreglarfecha(fFinal);
                //detalles
                var campo = '';
                var sAreas = '';
                //listar areas nuevas (actualizo mi json)
                var encontrado = 0;
                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEvento) {
                        $('#lstAreas input:checked').each(function () {
                            campo = $(this).attr('value');
                            encontrado = 0;
                            for (var j = 0; j < jsEventos[i].areas.length; j++) {
                                //jsEventos[i].areas.marca = 'no';
                                //setear a json y 
                                if (jsEventos[i].areas[j].id == campo) {
                                    jsEventos[i].areas[j].marca = 'si';
                                    encontrado = encontrado + 1;

                                    if (jsEventos[i].areas[j].estado != 0) {
                                        jsEventos[i].areas[j].accion = '0';
                                    }

                                }
                            }
                            if (encontrado == 0) {
                                var areas_ = { accion: + '0' + '', descripcion: '' + 'DESC AREA GENERAL' + '', estado: '' + '2' + '', id: '' + campo + '', sigla: '' + 'GENERAL' + '', marca: '' + 'si' + '' };
                                jsEventos[i].areas.push(areas_);
                                //return false;
                            }

                        });
                    }
                }

                //recorrer json
                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEvento) {
                        for (var j = 0; j < jsEventos[i].areas.length; j++) {
                            if (jsEventos[i].areas[j].marca != 'si') {//inactivar 
                                jsEventos[i].areas[j].accion = '1';
                            }
                            if (jsEventos[i].areas[j].accion != 2) {
                                sAreas = sAreas + jsEventos[i].areas[j].id + '|' + jsEventos[i].areas[j].accion + '*';
                            }
                        }
                    }
                }
                var sEntidades = '';
                campo = '';
                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEvento) {
                        $('#lstEntidases input:checked').each(function () {
                            campo = sEntidades + $(this).attr('value');
                            encontrado = 0;
                            for (var j = 0; j < jsEventos[i].entidades.length; j++) {
                                //jsEventos[i].entidades[j].marca = 'no';
                                //setear a json y 
                                if (jsEventos[i].entidades[j].id == campo) {
                                    jsEventos[i].entidades[j].marca = 'si';
                                    encontrado = encontrado + 1;

                                    if (jsEventos[i].entidades[j].estado != 0) {
                                        jsEventos[i].entidades[j].accion = '0';
                                    }

                                }
                            }
                            if (encontrado == 0) {
                                var entidades_ = {
                                    accion: + '0' + '', descripcion: '' + 'DESC ENTIDAD GENERAL' + '', estado: '' + '2' + '', id: '' + campo + '', sigla: ''
                                        + 'GENERAL' + '', marca: '' + 'si' + ''
                                };
                                jsEventos[i].entidades.push(entidades_);
                                //return false;
                            }
                        });
                    }

                }

                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEvento) {
                        for (var j = 0; j < jsEventos[i].entidades.length; j++) {
                            if (jsEventos[i].entidades[j].marca != 'si') {//inactivar 
                                jsEventos[i].entidades[j].accion = '1';
                            }
                            if (jsEventos[i].entidades[j].accion != '2') {
                                sEntidades = sEntidades + jsEventos[i].entidades[j].id + '|' + jsEventos[i].entidades[j].accion + '*';
                            }

                        }
                    }
                }

                var sPersonas = '';
                campo = '';
                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEvento) {
                        $("#personal tbody tr").each(function (index) {
                            $(this).find("td input:checkbox,td select").each(function () {
                                if ($(this).attr("name") == 'part') {
                                    campo = this.value;
                                    campo = campo.replace('N', '');
                                    encontrado = 0;
                                    for (var j = 0; j < jsEventos[i].personas.length; j++) {
                                        //jsEventos[i].personas[j].marca = 'no';
                                        //setear a json y 

                                        if (jsEventos[i].personas[j].dni == campo) {
                                            jsEventos[i].personas[j].marca = 'si';
                                            encontrado = encontrado + 1;

                                            if (jsEventos[i].personas[j].estado != 0) {
                                                jsEventos[i].personas[j].accion = '0';
                                            }
                                            //return false;
                                        }
                                    }
                                    if (encontrado == 0) {
                                        var personas_ = {
                                            accion: + '0' + '', apemat: '' + 'apemat' + '', apepat: '' + 'apepat' + '', area: '' + 'area' + '',
                                            asistencia: '' + 'true' + '', dni: '' + campo + '', estado: '' + '2' + '', id: '' + 'nuevo' + '',
                                            marca: '' + 'si' + '', name: '' + 'name' + '', nombres: '' + 'nombres' + '', participa: '' + 'true' + '', sigla: '' + 'SIGLA' + ''
                                        };

                                        jsEventos[i].personas.push(personas_);
                                    }

                                    //sPersonas = sPersonas + this.value + '|';
                                } //inputName = $(this).attr("name");                         

                            });

                        });
                    }
                }
                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEvento) {
                        for (var j = 0; j < jsEventos[i].personas.length; j++) {
                            if (jsEventos[i].personas[j].marca != 'si') {//inactivar 
                                jsEventos[i].personas[j].accion = '1';
                            }
                            if (jsEventos[i].personas[j].accion != '2') {
                                sPersonas = sPersonas + jsEventos[i].personas[j].dni + '|' + jsEventos[i].personas[j].accion + '*';
                            }
                        }
                    }
                }


                var sAgenda = '';
                campo = '';
                var titulo = ''
                var descripcion = '';
                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEvento) {
                        $("#tbAgenda tbody tr").each(function (index) {
                            $(this).find("td,td select").each(function () {
                                if ($(this).attr("name") == 'tdagenda') {
                                    campo = this.id;
                                    titulo = this.title;
                                    descripcion = this.innerHTML
                                    encontrado = 0;
                                    for (var j = 0; j < jsEventos[i].agenda.length; j++) {
                                        //jsEventos[i].areas.marca = 'no';
                                        //setear a json y 
                                        if (jsEventos[i].agenda[j].ida == campo) {
                                            jsEventos[i].agenda[j].marca = 'si';
                                            encontrado = encontrado + 1;

                                            if (jsEventos[i].agenda[j].estado != 0) {
                                                jsEventos[i].agenda[j].accion = '0';
                                            }
                                            //return false;
                                        }
                                    }

                                    if (encontrado == 0) {
                                        var agenda_ = { accion: + '0' + '', detallea: '' + descripcion + '', estado: '' + '2' + '', ida: '' + campo + '', marca: '' + 'si' + '', tituloa: '' + titulo + '' };
                                        jsEventos[i].agenda.push(agenda_);
                                    }

                                } //inputName = $(this).attr("name");                         

                            });

                        });
                    }
                }

                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEvento) {
                        for (var j = 0; j < jsEventos[i].agenda.length; j++) {
                            if (jsEventos[i].agenda[j].marca != 'si') {//inactivar 
                                jsEventos[i].agenda[j].accion = '1';
                            }
                            if (jsEventos[i].agenda[j].accion != '2') {
                                sAgenda = sAgenda + jsEventos[i].agenda[j].ida + '|';
                                sAgenda = sAgenda + jsEventos[i].agenda[j].tituloa + '|';
                                sAgenda = sAgenda + jsEventos[i].agenda[j].detallea + '|';
                                sAgenda = sAgenda + jsEventos[i].agenda[j].accion + '*';

                            }
                        }
                    }
                }

                var data = '{idEvento: "' + idEvento + '",fInicio:"' + fInicio + '",fFinal:"' + fFinal + '",sAreas:"' + sAreas + '",sEntidades:"' + sEntidades + '",sPersonas:"' + sPersonas + '",sAgenda:"' + sAgenda + '",sUsuario:"' + sUsuario + '"}';
                $.ajax({
                    type: "POST",
                    url: "CalendarioIndex.aspx/ActualizarEvento",
                    data: '{idEvento: "' + idEvento + '",fInicio:"' + fInicio + '",fFinal:"' + fFinal + '",sAreas:"' + sAreas + '",sEntidades:"' + sEntidades + '",sPersonas:"' + sPersonas + '",sAgenda:"' + sAgenda + '",sUsuario:"' + sUsuario + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        location.reload();
                        //alert(response.d);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });


            });
            //Usos template html ini

            $('#sendEditar').click(function () {
                idEventoGeneral = document.getElementById('idEvento');
                document.getElementById('myModalLabel').innerHTML = 'Editar Evento';
                var idEventocon = idEventoGeneral.getAttribute('value');
                $('#title').attr('disabled', 'disabled');
                $('#body').attr('disabled', 'disabled');

                listarCheckbox(idEventocon);
                //listamos personas

                //selectEvento();
                var oso = '';
                $(".personal tbody").empty();
                for (var i = 0; i < jsEventos.length; i++) {

                    if (jsEventos[i].id == idEventocon) {
                        $('#title').val(jsEventos[i].title);
                        $('#body').val(jsEventos[i].detalle);
                        $('#title').attr('readonly', 'true');
                        $('#body').attr('readonly', 'true');
                        $("#tbAgenda tbody").empty();
                        for (var j = 0; j < jsEventos[i].personas.length; j++) {
                            var personas_ = jsEventos[i].personas[j];
                            var newRowContent = "<tr><td>" + personas_.name + "</td><td><input type='checkbox' name='part' value='" +
                            personas_.dni + "' checked='true' onclick='deleteRow(this);' /></td></tr>";
                            $(".personal tbody").append(newRowContent);
                            //<td><input type='checkbox' name='asis' value='|"+ personas_.dni + "' disabled/></td>
                        }
                        for (var k = 0; k < jsEventos[i].areas.length; k++) {
                            oso = oso + jsEventos[i].areas[k].id + '|';
                        }

                        for (var m = 0; m < jsEventos[i].agenda.length; m++) {
                            var numerado = jsEventos[i].agenda[m].ida;
                            var titulo = jsEventos[i].agenda[m].tituloa;
                            var descripcion = jsEventos[i].agenda[m].detallea;
                            var newRowContent = "<tr><td><button onclick='editaAgenda(" + numerado + ");' class='btn-view-fund btn btn-default btn-xs  pull-left' type='button'>" +
                        "<span class='glyphicon glyphicon-edit' aria-hidden='true'></span></button></td><td name='tdagenda' id='" + numerado + "' colspan='2' title='" + descripcion +
                        "' style='text-align: left;'>" + titulo + "</td><td style='text-align: right;'><button onclick='verAgenda(" + numerado + ");' class='btn-view-fund btn btn-default btn-xs' type='button'>" +
                        "<span class='glyphicon glyphicon-list' aria-hidden='true'></span></button><button class='btn-view-fund btn btn-default btn-xs' type='button' onclick='eliminarAgenda(this);'  >" +
                        "<span class='glyphicon glyphicon-remove' aria-hidden='true'></span></button></td></tr>";
                            $("#tbAgenda tbody").append(newRowContent);
                        }

                    }

                }
                listarPersonas(oso);
                //seteamos fechas
                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idEventocon) {
                        var inicio = (new Date(parseInt(jsEventos[i].start)));
                        var fin = (new Date(parseInt(jsEventos[i].end)));
                        //$('#from2').val(convertir(inicio.getDate()) + '/' + convertir(inicio.getMonthFormatted()) + '/' + inicio.getFullYear() + ' ' + convertir(inicio.getHours()) + ':' + convertir(inicio.getMinutes()));
                        //$('#to2').val(convertir(fin.getDate()) + '/' + convertir(fin.getMonthFormatted()) + '/' + fin.getFullYear() + ' ' + convertir(fin.getHours()) + ':' + convertir(fin.getMinutes()));;
                        $('#from2').val(formatDate(inicio));
                        $('#to2').val(formatDate(fin));
                        //combos
                        var idcomoboare = '';
                        $(".combo").val(jsEventos[i].tipo).find("option[value=" + jsEventos[i].tipo + "]").attr('selected', true);
                        $(".comboare").each(function () {
                            idcomoboare = '#' + $(this).attr('id');
                        });
                        selectEvento(jsEventos[i].area);
                        //$(".comboare").append("<option value=\"" + jsEventos[i].area + "\">" + jsEventos[i].nombrearea + "</option>");



                    }

                }
            });

            $('#sendCancelar').click(function () {
                OpenModalFinaliza();
            });

            $('#setfinaliza').click(function () {
                var idevento = document.getElementById('idEvento');
                var ioriginal = idevento.getAttribute('value');
                $('#add_finaliza').modal('hide');
                FinalizaEvento(ioriginal);
            });

            function FinalizaEvento(ioriginal) {

                var sObservacion = $("#bodyAviso").val().toUpperCase();
                $.ajax({
                    type: "POST",
                    url: "CalendarioIndex.aspx/FinalizarEvento",
                    data: '{idEvento: "' + ioriginal + '",sObservacion:"' + sObservacion + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        response = response.d;
                        location.reload();
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }

            function formatDate(dateVal) {
                var newDate = new Date(dateVal);

                var sMonth = padValue(newDate.getMonth() + 1);
                var sDay = padValue(newDate.getDate());
                var sYear = newDate.getFullYear();
                var sHour = newDate.getHours();
                var sMinute = padValue(newDate.getMinutes());
                var sAMPM = "AM";

                var iHourCheck = parseInt(sHour);

                if (iHourCheck > 12) {
                    sAMPM = "PM";
                    sHour = iHourCheck - 12;
                }
                else if (iHourCheck === 0) {
                    sHour = "12";
                }

                sHour = padValue(sHour);

                return sMonth + "-" + sDay + "-" + sYear + " " + sHour + ":" + sMinute + " " + sAMPM;
            }

            function padValue(value) {
                return (value < 10) ? "0" + value : value;
            }
            function convertir(dia) {
                if (dia < 10) {
                    dia = '0' + dia;
                }
                return dia;
            }


            $('#cbotipoEvento').click(function () {
                var tipoEvento = $('#cbotipoEvento').val();

            });
            //Usos template html fin

            //Agregar agenda ini
            var numerado = 99999999;

            $('#addAgenda').click(function () {
                var titulo = $("#tituloAgenda").val().toUpperCase();
                var descripcion = $("#bodyAgenda").val().toUpperCase();

                numerado = numerado + 1;
                var newRowContent = "<tr><td><button onclick='editaAgenda(" + numerado + ");' class='btn-view-fund btn btn-default btn-xs  pull-left' type='button'>" +
                    "<span class='glyphicon glyphicon-edit' aria-hidden='true'></span></button></td><td name='tdagenda' id='" + numerado + "' colspan='2' title='" + descripcion +
                    "' style='text-align: left;'>" + titulo + "</td><td style='text-align: right;'><button onclick='verAgenda(" + numerado + ");' class='btn-view-fund btn btn-default btn-xs' type='button'>" +
                    "<span class='glyphicon glyphicon-list' aria-hidden='true'></span></button><button class='btn-view-fund btn btn-default btn-xs' type='button' onclick='eliminarAgenda(this);'  >" +
                    "<span class='glyphicon glyphicon-remove' aria-hidden='true'></span></button></td></tr>";
                $("#tbAgenda tbody").append(newRowContent);


                $("#tituloAgenda").val('');
                $('#bodyAgenda').val('');
                $('#add_agenda').modal('hide');
                //}


            });
            //Agregar agenda fin


            //Editar Agenda
            $('#editAgenda').click(function () {
                idTd = $('#editAgenda').val();
                var titulo = $("#tituloAgenda").val().toUpperCase();
                var descripcion = $("#bodyAgenda").val().toUpperCase();
                td = document.getElementById(idTd);
                td.innerHTML = titulo;
                td.title = descripcion;
                $('#add_agenda').modal('hide');
            });

            //Agregar acuerdo

            $('#addAcuerdo').click(function () {
                $('#editAcuerdo').hide();
                $('#addAgenda').show();
                var numerado = 7777777777;
                var nroacuerdo = 1;
                var titulo = $("#tituloAcuerdo").val().toUpperCase();
                //obtenemos evento actual

                var idevento = document.getElementById('idEvento');
                var agenda = $("#cboAgenda").val();
                var responsable = $("#cboResponsable").val();
                var descripcion = $("#bodyAcuerdo").val().toUpperCase();

                //sacamos la informacion de la agenda
                for (var i = 0; i < jsEventos.length; i++) {
                    var ioriginal = idevento.getAttribute('value');
                    if (jsEventos[i].id == ioriginal) {
                        var promise = insertarAcuerdo(jsEventos[i].id, agenda, responsable, titulo, descripcion);
                        var xi = i;
                        promise.success(function (data) {
                            numerado = data.d.idac;
                            var acuerdo = { idac: +data.d.idac + '', idag: '' + agenda + '', participante: '' + responsable + '', descripcion: '' + descripcion + '', tituloac: '' + titulo + '', accion: '' };

                            jsEventos[xi].acuerdos.push(acuerdo);

                            var newRowContent = "<tr><td><button onclick='editaAcuerdo(" + numerado + "," + ioriginal + "," + agenda + "," + responsable + ");' class='btn-view-fund btn btn-default btn-xs  pull-left' type='button'>" +
                            "<span class='glyphicon glyphicon-edit' aria-hidden='true'></span></button></td><td name='tdagenda' id='" + numerado + "' colspan='2' title='" + descripcion +
                            "' style='text-align: left;'>" + titulo + "</td><td style='text-align: right;'><button onclick='verAcuerdo(" + numerado + "," + ioriginal + ");' class='btn-view-fund btn btn-default btn-xs' type='button'>" +
                            "<span class='glyphicon glyphicon-list' aria-hidden='true'></span></button><button class='btn-view-fund btn btn-default btn-xs' type='button' onclick='eliminarAcuerdo(this," + numerado + "," + ioriginal + "," + agenda + ");'  >" +
                            "<span class='glyphicon glyphicon-remove' aria-hidden='true'></span></button></td></tr>";
                            $("#tbAcuerdo tbody").append(newRowContent);

                        });

                    }
                }


                $('#tbodyAcuerdos').val('');
                $('#add_acuerdo').modal('hide');

            });

            $('#editAcuerdo').click(function () {
                idEventoGeneral = document.getElementById('idEvento');
                $("#tbAcuerdo tbody").empty();
                var numerado = $('#editAcuerdo').val();
                var idevento = idEventoGeneral;
                var agenda = $("#cboAgenda").val();
                var responsable = $("#cboResponsable").val();
                var descripcion = $("#bodyAcuerdo").val().toUpperCase();
                var titulo = $("#tituloAcuerdo").val().toUpperCase();

                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idevento.getAttribute('value')) {
                        for (var j = 0; j < jsEventos[i].acuerdos.length; j++) {
                            if (jsEventos[i].acuerdos[j].idac == numerado) {
                                jsEventos[i].acuerdos[j].tituloac = titulo;
                                jsEventos[i].acuerdos[j].descripcion = descripcion;
                                jsEventos[i].acuerdos[j].idag = agenda;
                                jsEventos[i].acuerdos[j].participante = responsable;

                                var promise = actualizarAcuerdo(numerado, jsEventos[i].id, agenda, responsable, titulo, descripcion);

                                promise.success(function (data) {
                                    numerado = data.d.idac;

                                });
                            }
                        }
                    }
                }

                for (var i = 0; i < jsEventos.length; i++) {
                    if (jsEventos[i].id == idevento.getAttribute('value')) {
                        for (var j = 0; j < jsEventos[i].acuerdos.length; j++) {
                            titulo = jsEventos[i].acuerdos[j].tituloac;
                            numerado = jsEventos[i].acuerdos[j].idac;
                            idEvento = jsEventos[i].id;
                            agenda = jsEventos[i].acuerdos[j].idag;
                            responsable = jsEventos[i].acuerdos[j].participante;
                            descripcion = jsEventos[i].acuerdos[j].descripcion;

                            var newRowContent = "<tr><td><button onclick='editaAcuerdo(" + numerado + "," + idEvento + "," + agenda + "," + "" + responsable.toString() + "" + ");' class='btn-view-fund btn btn-default btn-xs  pull-left' type='button'>" +
                            "<span class='glyphicon glyphicon-edit' aria-hidden='true'></span></button></td><td name='tdagenda' id='" + numerado + "' colspan='2' title='" + descripcion +
                            "' style='text-align: left;'>" + titulo + "</td><td style='text-align: right;'><button onclick='verAcuerdo(" + numerado + "," + idEvento + ");' class='btn-view-fund btn btn-default btn-xs' type='button'>" +
                            "<span class='glyphicon glyphicon-list' aria-hidden='true'></span></button><button class='btn-view-fund btn btn-default btn-xs' type='button' onclick='eliminarAcuerdo(this," + numerado + "," + idEvento + "," + agenda + ");'  >" +
                            "<span class='glyphicon glyphicon-remove' aria-hidden='true'></span></button></td></tr>";
                            $("#tbAcuerdo tbody").append(newRowContent);



                        }
                    }
                } 

                $('#tbodyAcuerdos').val('');
                $('#add_acuerdo').modal('hide');


            });




        });

        function selectEvento(area) {
            if (area == -1) {
                $('#setEvent').hide();
                $('#addEvent').show();
                $('#personal tbody').empty();
                $('#title').val('');
                $('#body').val('');
                $('#title').removeAttr('readonly');
                $('#body').removeAttr('readonly');
                $('#tbAgenda tbody').empty();
            }

            if (area == 0) {
                $('#setEvent').hide();
                $('#addEvent').show();
                $('#personal tbody').empty();
                $('#title').val('');
                $('#body').val('');
                $('#title').removeAttr('readonly');
                $('#body').removeAttr('readonly');

            } else {
                if (area != -1) {

                    $('#addEvent').hide();
                    $('#setEvent').show();
                }
            }
            //
            $('.combo').removeAttr('disabled');
            $('.comboare').removeAttr('disabled');
            $('#title').removeAttr('disabled');
            $('#body').removeAttr('disabled');
            document.getElementById('myModalLabel').innerHTML = 'Agregar nuevo evento';
            //obtenemos id
            var idCombo = "";
            var idcomoboare = ""
            $(".combo").each(function () {
                idCombo = '#' + $(this).attr('id');
            });
            $(".comboare").each(function () {
                idcomoboare = '#' + $(this).attr('id');
            });
            //1:interno 2:externo
            var elemento = $(idcomoboare);
            var idTipoEvento = $(idCombo).val();
            var lblElement = $('#lblarea');

            if (idTipoEvento == '1') {
                lblElement[0].innerText = "Area  que Organiza";
                $("#lblarea").removeAttr('style');
                $('.comboare').removeAttr('style');
            }
            if (idTipoEvento == '2') {
                lblElement[0].innerText = "Entidad que Organiza";
                $("#lblarea").removeAttr('style');
                $('.comboare').removeAttr('style');
            }

            $.ajax({
                type: "POST",
                url: "CalendarioIndex.aspx/CargarArea",
                data: '{idDom: "' + 3 + '",idTipoEvento:"' + idTipoEvento + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $(elemento).empty();
                    response = response.d.result;
                    $.each(response, function (k, v) {
                        $(elemento).append("<option value=\"" + v.id + "\">" + v.descripcion + "</option>");
                    });

                    if (area != '0' && area != -1) {
                        $(elemento).val(area).find("option[value=" + area + "]").attr('selected', true);
                        $('.combo').attr('disabled', 'disabled');
                        $('.comboare').attr('disabled', 'disabled');
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

        }
        function listarCheckbox(editado) {
            if (rol == "USUARIO") {
                $('#add_evento').modal('hide');
                return false;

            }
            var htmlContruccion = "";
            var htmlContruccionEntidad = "";
            var contador = 0;
            $.ajax({
                type: "POST",
                url: "CalendarioIndex.aspx/CargarArea",
                data: '{idDom: "' + 10 + '",idTipoEvento:"' + '1' + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (editado == 0) {
                        $(".comboare").empty();
                    }
                    response = response.d.result;
                    $.each(response, function (k, v) {
                        contador = contador + 1;
                        htmlContruccion = htmlContruccion + "<label for='info" + contador + "' class='btn btn-primary btnNuevo' title='" + v.descripcion + "' data-placement='right' " +
                                                       "data-toggle='tooltip'>" + v.sigla + "&nbsp;<input type='checkbox' id='info" + contador + "' class='badgebox'  value='" + v.id + "'/><span class='badge'>&check;</span></label>"
                    });
                    var lista = document.getElementById('lstAreaCk');
                    lista.innerHTML = htmlContruccion;
                    $(function () {
                        $("[data-toggle='tooltip']").tooltip();
                    });

                    if (editado != 0) {
                        for (var i = 0; i < jsEventos.length; i++) {
                            if (jsEventos[i].id == editado) {
                                for (var j = 0; j < jsEventos[i].areas.length; j++) {
                                    var valuea = jsEventos[i].areas[j].id;
                                    $('#lstAreaCk input[type=checkbox][value=' + valuea + ']').prop('checked', true);

                                }
                            }
                        }
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });


            $.ajax({
                type: "POST",
                url: "CalendarioIndex.aspx/CargarArea",
                data: '{idDom: "' + 10 + '",idTipoEvento:"' + '2' + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    contador = 100;
                    if (editado == 0) {
                        $(".comboare").empty();
                    }
                    response = response.d.result;
                    $.each(response, function (k, v) {
                        contador = contador + 1;
                        htmlContruccionEntidad = htmlContruccionEntidad + "<label for='info" + contador + "' class='btn btn-danger btnNuevo' title='" + v.descripcion + "' data-placement='right' " +
                                                      "data-toggle='tooltip'>" + v.sigla + "&nbsp;<input type='checkbox' id='info" + contador + "' class='badgebox'  value='" + v.id + "'/><span class='badge'>&check;</span></label>"
                    });
                    var lista = document.getElementById('lstEntidasesCk');
                    lista.innerHTML = htmlContruccionEntidad;
                    $(function () {
                        $("[data-toggle='tooltip']").tooltip();
                    });
                    //lstEntidasesCk
                    if (editado != 0) {

                        for (var i = 0; i < jsEventos.length; i++) {
                            if (jsEventos[i].id == editado) {
                                for (var j = 0; j < jsEventos[i].entidades.length; j++) {
                                    var valuea = jsEventos[i].entidades[j].id;
                                    $('#lstEntidasesCk input[type=checkbox][value=' + valuea + ']').prop('checked', true);

                                }
                            }
                        }
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

        }

        var products;

        function listarPersonas(oso) {
            products = [];
            var cont = 0;
            var checkboxes = document.getElementById('lstAreas');//document.getElementById('lstAreas').checkbox;
            var selected = [];

            //alert(oso);
            $.ajax({
                type: "POST",
                url: "CalendarioIndex.aspx/ListarUsuarioArea",
                data: '{tipo: "' + 1 + '",cadenaArea:"' + oso + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    response = response.d.result;
                    products = response;

                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        $(function () {

            $(".personal tbody").empty();
            $('.yalert').change(function () {
                products = [];
                var cont = 0;
                var oso = "";
                var checkboxes = document.getElementById('lstAreas');//document.getElementById('lstAreas').checkbox;
                var selected = [];
                $('#lstAreas input:checked').each(function () {
                    oso = oso + $(this).attr('value') + '|';
                });
                //alert(oso);
                $.ajax({
                    type: "POST",
                    url: "CalendarioIndex.aspx/ListarUsuarioArea",
                    data: '{tipo: "' + 1 + '",cadenaArea:"' + oso + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        response = response.d.result;
                        products = response;

                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });
            var idpersona = '';
            $('[id*=txtSearch]').typeahead({

                hint: true,
                highlight: true,
                minLength: 1,

                source: function (query, process) {
                    var results = _.map(products, function (product) {
                        return product;

                    });
                    process(results);
                },
                updater: function (product) {
                    var idexiste = [];
                    $('.personal tbody input:checked').each(function () {
                        idexiste.push($(this).attr('value'));
                    });

                    if (idexiste.indexOf(product.dni) == -1) {
                        var newRowContent = "<tr><td>" + product.name + "</td><td><input type='checkbox' name='part' value='N" +
                            product.dni + "' checked='true' onclick='deleteRow(this);' /></td></tr>";
                        $(".personal tbody").append(newRowContent);

                        //<td><input type='checkbox' name='asis' value='|"+ product.dni + "' disabled/></td>
                    }

                    //return product;
                }
            });
        });
        function deleteRow(r) {
            var miTabla = $(".personal tbody");
            id = '#' + miTabla.attr('id');
            var i = r.parentNode.parentNode.rowIndex;
            miTabla.find("tr:nth-child(" + i + ")").remove();
        }
        function editaAgenda(r) {
            OpenModalAgenda();
            $('#editAgenda').show();
            $('#addAgenda').hide();
            $("#myModalLabel2")[0].innerHTML = 'Editar punto de agenda';
            var xi = "#tbAgenda tbody " + r;
            var miTabla = $(xi);
            var x = document.getElementById(r);
            var titulo = x.innerHTML;
            var descripcion = x.title;
            $('#editAgenda').val(r);
            $("#tituloAgenda").val(titulo);
            $('#bodyAgenda').val(descripcion);

        }
        function verAgenda(r) {
            OpenModalAgenda();
            $("#myModalLabel2")[0].innerHTML = 'Ver punto de agenda';
            $("#tituloAgenda").attr('disabled', 'disabled');
            $('#bodyAgenda').attr('disabled', 'disabled');
            $('#editAgenda').hide();
            $('#addAgenda').hide();
            var ids = "#tbAgenda tbody " + r;
            var miTabla = $(ids);
            var x = document.getElementById(r);
            var titulo = x.innerHTML;
            var descripcion = x.title;
            $("#tituloAgenda").val(titulo);
            $('#bodyAgenda').val(descripcion);


            //var i = r.parentNode.parentNode.rowIndex;
            //var valor=miTabla.find("tr:nth-child(" + i + ")").val();
            //miTabla.find("tr:nth-child(" + i + ")").remove();
        }
        function eliminarAgenda(r) {
            var miTabla = $(".tbAgenda tbody");
            id = '#' + miTabla.attr('id');
            var i = r.parentNode.parentNode.rowIndex + 1;
            miTabla.find("tr:nth-child(" + i + ")").remove();
        }
        function OpenModalAgenda() {
            $("#myModalLabel2")[0].innerHTML = 'Agregar nuevo punto de agenda';
            $('#add_agenda').modal('show');
            $('#editAgenda').hide();
            $('#addAgenda').show();
            $("#tituloAgenda").removeAttr("disabled");
            $('#bodyAgenda').removeAttr("disabled");
            $("#tituloAgenda").val('');
            $('#bodyAgenda').val('');
        }
        function actualizarAcuerdo(acuerdo, idEvento, idAgenda, responsable, titulo, descripcion) {

            return $.ajax({
                type: "POST",
                url: "CalendarioIndex.aspx/ActualizaAcuerdo",
                data: '{idAcuerdo: "' + acuerdo + '", idEvento: "' + idEvento + '",idAgenda:"' + idAgenda + '",responsable:"' + responsable + '",titulo:"' + titulo + '",descripcion:"' + descripcion + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    return response.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function insertarAcuerdo(idEvento, idAgenda, responsable, titulo, descripcion) {
            var acuerdo = '';
            return $.ajax({
                type: "POST",
                url: "CalendarioIndex.aspx/GuardarAcuerdo",
                data: '{idEvento: "' + idEvento + '",idAgenda:"' + idAgenda + '",responsable:"' + responsable + '",titulo:"' + titulo + '",descripcion:"' + descripcion + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    return response.d;
                    //response = response.d.idac;
                    //acuerdo = response;
                    //idAcuerdoGen = acuerdo;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function editaAcuerdo(idacuerdo, idevento, agenda, responsable) {
            responsable = ("0000" + responsable).substr(-8, 8)
            OpenModalAcuerdo()
            $('#editAcuerdo').val(idacuerdo);
            $('#editAgenda').show();
            $('#addAgenda').hide();

            $("#myModalLabel3")[0].innerHTML = 'Editar acuerdo';
            //agenda
            $("#cboAgenda").removeAttr("disabled");
            $("#cboResponsable").removeAttr("disabled");
            $("#tituloAcuerdo").removeAttr("disabled");
            var titulo = '';
            var descripcion = '';
            for (var i = 0; i < jsEventos.length; i++) {
                if (jsEventos[i].id == idEvento) {
                    for (var j = 0; j < jsEventos[i].acuerdos.length; j++) {
                        if (jsEventos[i].acuerdos[j].idac == idacuerdo) {
                            var agenda = jsEventos[i].acuerdos[j].idag;
                            var responsable = jsEventos[i].acuerdos[j].participante;
                            descripcion = jsEventos[i].acuerdos[j].descripcion;
                            titulo = jsEventos[i].acuerdos[j].tituloac;
                            $("#cboAgenda").val(agenda).find("option[value=" + agenda + "]").attr('selected', true);
                            $("#cboResponsable").val(responsable).find("option[value=" + responsable + "]").attr('selected', true);

                        }
                    }
                }
            }
            $('#editAcuerdo').show();
            $('#addAcuerdo').hide();

            $("#tituloAcuerdo").val(titulo)
            $('#bodyAcuerdo').val(descripcion);

        }
        function verAcuerdo(r, idEvento) {
            OpenModalAcuerdo();
            $("#cboAgenda").val(agenda)
            .find("option[value=" + agenda + "]").attr('selected', true);
            $("#cboResponsable").val(responsable)
            .find("option[value=" + responsable + "]").attr('selected', true);
            $("#myModalLabel3")[0].innerHTML = 'Ver acuerdo';
            $("#tituloAcuerdo").attr('disabled', 'disabled');
            $("#cboAgenda").attr('disabled', 'disabled');
            $("#cboResponsable").attr('disabled', 'disabled');
            $('#bodyAcuerdo').attr('disabled', 'disabled');
            $('#addAcuerdo').hide();
            $('#editAcuerdo').hide();
            var descripcion = '';
            for (var i = 0; i < jsEventos.length; i++) {
                if (jsEventos[i].id == idEvento) {
                    for (var j = 0; j < jsEventos[i].acuerdos.length; j++) {
                        if (jsEventos[i].acuerdos[j].idac == r) {
                            var agenda = jsEventos[i].acuerdos[j].idag;
                            var responsable = jsEventos[i].acuerdos[j].participante;
                            descripcion = jsEventos[i].acuerdos[j].descripcion;
                            var titulo = jsEventos[i].acuerdos[j].tituloac;
                            $("#tituloAcuerdo").val(titulo)
                            $("#cboAgenda").val(agenda).find("option[value=" + agenda + "]").attr('selected', true);
                            $("#cboResponsable").val(responsable).find("option[value=" + responsable + "]").attr('selected', true);

                        }
                    }
                }
            }

            $('#bodyAcuerdo').val(descripcion);

        }
        function eliminarAcuerdo(r, idAcuerdo, idEvento, idAgenda) {
            //eliminar de mis eventos
            for (var i = 0; i < jsEventos.length; i++) {
                if (jsEventos[i].id == idEvento) {
                    jsEventos[i].acuerdos.forEach(function (currentValue, index, arr) {
                        if (jsEventos[i].acuerdos[index].idac == idAcuerdo) {
                            jsEventos[i].acuerdos.splice(index, index);
                        }
                    })
                }
            }
            //elimina de la vista
            var miTabla = $("#tbAcuerdo tbody");
            id = '#' + miTabla.attr('id');
            var i = r.parentNode.parentNode.rowIndex;
            miTabla.find("tr:nth-child(" + i + ")").remove();

            //elimina de la base de datos
            $.ajax({
                type: "POST",
                url: "CalendarioIndex.aspx/EliminarAcuerdo",
                data: '{idAcuerdo: "' + idAcuerdo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    return response.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function OpenModalFinaliza() {
            $('#add_finaliza').modal('show');
        }

        function OpenModalAcuerdo() {
            $("#myModalLabel3")[0].innerHTML = 'Agregar nuevo Acuerdo';
            $('#add_acuerdo').modal('show');
            $('#editAcuerdo').hide();
            $('#addAcuerdo').show();
            $("#tituloAcuerdo").removeAttr("disabled");
            $('#bodyAcuerdo').removeAttr("disabled");
            $("#cboResponsable").removeAttr("disabled");
            $("#cboAgenda").removeAttr("disabled");
            $("#tituloAcuerdo").val('');
            $('#bodyAcuerdo').val('');
            var idevento = document.getElementById('idEvento');
            $("#cboAgenda").empty();
            $("#cboResponsable").empty();
            for (var i = 0; i < jsEventos.length; i++) {

                if (jsEventos[i].id == idevento.getAttribute('value')) {
                    for (var j = 0; j < jsEventos[i].agenda.length; j++) {
                        $("#cboAgenda").append('<option title=' + jsEventos[i].agenda[j].detallea + ' value=' + jsEventos[i].agenda[j].ida + '>' + jsEventos[i].agenda[j].tituloa + '</option>');

                    }

                    for (var j = 0; j < jsEventos[i].personas.length; j++) {
                        $("#cboResponsable").append('<option title=' + jsEventos[i].personas[j].sigla + ' value=' + jsEventos[i].personas[j].dni + '>' + jsEventos[i].personas[j].nombres + ' ' + jsEventos[i].personas[j].apepat + ' ' + jsEventos[i].personas[j].apemat + '</option>');

                    }
                }
            }



        }
        function ListarAcuerdos(estado, dates, idevento) {

            //botones que dependen del cierre



            var miEvento = [];
            for (var i = 0; i < jsEventos.length; i++) {
                jsEventos[i].creador = 0;
                if (jsEventos[i].id == idevento) {
                    miEvento = jsEventos[i]
                }
            }
            var hoy = new Date();
            var inicio = (new Date(parseInt(miEvento.start)));

            $btnEditar = 0;
            $btnFinalizar = 0;
            $btnAcuerdos = 0;
            $btnPdf = 0;
            $btnAgendas = 0;
            $blockCheck = 0;
            $reprogramar = 0;
            $btnAcuerdos = 0;
            //analizamos los accesos por rol            
            if (rol == 'SUPERVISOR') { //supervisor
                $reprogramar = 2;
                //los accesos por propietario el que crea tiene acceso a todo sino solo a ver
                if (miEvento.usuario == usuario) {
                    $btnEditar = 1;
                    $btnFinalizar = 1;
                    $btnAcuerdos = 1;
                    $btnAgendas = 1;
                    $reprogramar = 1;
                    $creador = 1;
                }

                //botones que dependen del estado
                if (miEvento.estado == 1) {
                    $btnEditar = 0;
                    $btnFinalizar = 0;
                    $btnAcuerdos = 0;
                    $btnPdf = 1;
                    $btnAgendas = 0;
                    $blockCheck = 0;
                    $reprogramar = 0;
                    $btnAcuerdos = 0;
                    $reprogramar = 0;
                }

                //botones que dependen de la fecha
                if (hoy.getDate() == inicio.getDate() & hoy.getMonth() == inicio.getMonth() & hoy.getFullYear() == inicio.getFullYear()) {
                    $btnEditar = 0;
                    $btnFinalizar = 0;
                    $btnAcuerdos = 0;
                    $btnPdf = 1;
                    $btnAgendas = 0;
                    $blockCheck = 0;
                    $reprogramar = 0;
                    $btnAcuerdos = 0;
                }





            }

            //$btnPdf
            if (rol == 'GERENTE') {
                //bloquear todo
                $reprogramar = 2;
                $btnEditar = 0;

                //los accesos por propietario el que crea tiene acceso a todo sino solo a ver
                if (miEvento.usuario == usuario) {
                    $btnEditar = 1;
                    $btnFinalizar = 1;
                    $btnAcuerdos = 1;
                    $btnAgendas = 1;
                    $reprogramar = 1;
                    $creador = 1;

                }

                //botones que dependen del estado
                if (miEvento.estado == 1) {
                    $btnEditar = 0;
                    $btnFinalizar = 0;
                    $btnAcuerdos = 0;
                    $btnPdf = 1;
                    $btnAgendas = 0;
                    $blockCheck = 0;
                    $reprogramar = 0;
                    $btnAcuerdos = 0;
                    $reprogramar = 0;
                }

                //botones que dependen de la fecha
                if (hoy.getDate() == inicio.getDate() & hoy.getMonth() == inicio.getMonth() & hoy.getFullYear() == inicio.getFullYear()) {
                    $btnEditar = 0;
                    $btnFinalizar = 0;
                    $btnAcuerdos = 0;
                    $btnPdf = 1;
                    $btnAgendas = 0;
                    $blockCheck = 0;
                    $reprogramar = 0;
                    $btnAcuerdos = 0;
                }
            }
            if (rol == 'USUARIO') {

                //bloquear todo solo ve e imprime   
            }






            //por usuario si es el creador tiene todos los accesos para el evento




            if ($reprogramar == 0) {
                $('#reprogramar').hide();
            } else {
                $('#reprogramar').show();
            }

            if ($btnAcuerdos == 0) {
                $('#btnAddAcuerdo').hide();
            } else { $('#btnAddAcuerdo').show(); }

            if ($btnEditar == 0) {
                $('#sendEditar').hide();
            } else { $('#sendEditar').show(); }

            if ($btnFinalizar == 0) {
                $('#sendCancelar').hide();
            } else { $('#sendCancelar').show(); }

            if ($btnPdf == 0) {
                $('#pdf').hide();
            } else { $('#pdf').show(); }


            var hoy = new Date();
            var star = 'falso'
            if (dates.getMonth() + dates.getDate() + dates.getFullYear() == hoy.getMonth() + hoy.getDate() + hoy.getFullYear()) {
                star = 'ok';
            };

            if (estado == '1') {
                //$('#sendEditar').hide();
                //$('#btnAddAcuerdo').hide();
                //$('#sendCancelar').hide();
            }

            if (estado == '0') {
                if (star == 'ok') {
                    // $('#btnAddAcuerdo').show();
                } else {
                    //$('#btnAddAcuerdo').hide();
                }
                // $('#sendEditar').show();
                //  $('#sendCancelar').show();
            }

            //ahora construimos los acuerdos
            $("#tbAcuerdo tbody").empty();
            var titulo = '';
            var descripcion = '';
            var numerado = 666666666666;
            for (var i = 0; i < jsEventos.length; i++) {
                if (jsEventos[i].id == idevento) {
                    for (var j = 0; j < jsEventos[i].acuerdos.length; j++) {
                        titulo = jsEventos[i].acuerdos[j].tituloac;
                        descripcion = jsEventos[i].acuerdos[j].descripcion;
                        numerado = jsEventos[i].acuerdos[j].idac;
                        idEvento = jsEventos[i].id;
                        responsable = jsEventos[i].acuerdos[j].participante;
                        agenda = jsEventos[i].acuerdos[j].idag;
                        var newRowContent = "<tr><td><button onclick='editaAcuerdo(" + numerado + "," + idEvento + "," + agenda + "," + "" + responsable.toString() + "" + ");' class='btn-view-fund btn btn-default btn-xs  pull-left' type='button'>" +
                  "<span class='glyphicon glyphicon-edit' aria-hidden='true'></span></button></td><td name='tdagenda' id='" + numerado + "' colspan='2' title='" + descripcion +
                  "' style='text-align: left;'>" + titulo + "</td><td style='text-align: right;'><button onclick='verAcuerdo(" + numerado + "," + idEvento + ");' class='btn-view-fund btn btn-default btn-xs' type='button'>" +
                  "<span class='glyphicon glyphicon-list' aria-hidden='true'></span></button><button class='btn-view-fund btn btn-default btn-xs' type='button' onclick='eliminarAcuerdo(this," + numerado + "," + idEvento + "," + agenda + ");'  >" +
                  "<span class='glyphicon glyphicon-remove' aria-hidden='true'></span></button></td></tr>";
                        $("#tbAcuerdo tbody").append(newRowContent);
                    }
                }
            }


        }

        function marcarAsistencia(dni, chek) {
            var checkeado = '0';
            if (chek) {
                checkeado = '1';
            }

            var idevento = document.getElementById('idEvento');
            var ioriginal = idevento.getAttribute('value');
            $.ajax({
                type: "POST",
                url: "CalendarioIndex.aspx/MarcaAsistencia",
                data: '{idEvento: "' + ioriginal + '",sPersona:"' + dni + '",sChek:"' + checkeado + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    response = response.d;

                    for (var i = 0; i < jsEventos.length; i++) {
                        if (jsEventos[i].id == ioriginal) {
                            for (var j = 0; j < jsEventos[i].personas.length; j++) {
                                if (jsEventos[i].personas[j].dni == dni) {
                                    jsEventos[i].personas[j].asistencia = chek;

                                }
                            }

                        }
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
            //alert(dni);
        }


        function pasarVariables(pagina) {    
            var querystr = jQuery.param(jsEventos[0]); 
            pagina += "?eventopdf=" + JSON.stringify({ items: jsEventos[0] });             
            window.open(pagina, '_blank');
            //window.location = pagina;
        }
 
    </script>

    <div class="container">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
 
        <div class="row">
            <div class="page-header">
                <h2></h2>
            </div>
            <div class="pull-left form-inline">
                <br />
                <div class="btn-group">
                    <button class="btn btn-primary" data-calendar-nav="prev" onclick="return false;"><< Anterior</button>
                    <button class="btn" data-calendar-nav="today" onclick="return false;">Hoy</button>
                    <button class="btn btn-primary" data-calendar-nav="next" onclick="return false;">Siguiente >></button>
                </div>
                <div class="btn-group">
                    <button id="year" class="btn btn-warning" data-calendar-view="year" runat="server" onclick="return false;">Año</button>
                    <button id="month" class="btn btn-warning active" data-calendar-view="month" runat="server" onclick="return false;">Mes</button>
                    <button id="week" class="btn btn-warning" data-calendar-view="week" runat="server" onclick="return false;">Semana</button>
                    <button id="day" class="btn btn-warning" data-calendar-view="day" runat="server" onclick="return false;">Dia</button>
                </div>
            </div>
            <%--    <asp:UpdatePanel ID="UpdatePanelDI" runat="server">
                <ContentTemplate>--%>
            <div class="pull-right form-inline">
                <br />
                <button id="ocultar" runat="server" class="btn btn-info" data-toggle='modal' data-target='#add_evento' onclick="selectEvento(-1);listarCheckbox(0);return false;">Añadir Evento</button>
            </div>
            <%--            </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>

        <hr />
        <div class="row">
            <div id="calendar"></div>

            <br />
            <br />
        </div>
        <!--ventana modal para el calendario-->
        <div class="modal fade" id="events-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="false">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header">
                        <h4 class="modal-title" id="lbl_Actualiza">Datos del Evento</h4>
                        <button class="btn btn-black" style="position: relative; top: 1px; left: 440px;" data-toggle='modal' id="reprogramar" data-target='#sen_reprograma' onclick="return false;">Reprogramar</button>
                    </div>

                    <div class="modal-body" id="eventTemplate" style="height: auto;">

                        <p>One fine body&hellip;</p>

                    </div>
                    <div class="modal-bodyx" id="" style="height: auto;">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="alert alert-info" style="max-height: 300px;">
                                    <label for="tipo">Acuerdos</label>
                                    <br />
                                    <div class="scrollbar scrollbar-success">
                                        <div class="force-overflow">
                                            <table class="table Acuerdos" id="tbAcuerdo">
                                                <thead class="">
                                                </thead>

                                                <tbody id="tbodyAcuerdos">
                                                    <%--AQUI SE LISTAN LOS acuerdos--%>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-4" style="text-align: left;">

                            <button type="button" id="btnAddAcuerdo" class="btn btn-info" data-target='#add_acuerdo' onclick="OpenModalAcuerdo();"><i class="fa fa-comment"></i>Agregar Acuerdo</button>
                        </div>

                        <button class="btn btn-warning" data-toggle='modal' id="pdf" onclick="return false;">Imprimir</button>
                        <%--<button type="button" class="btn btn-success" id="sendEditar" data-target='#add_evento' data-dismiss="modal" onclick="return false;"><i class="fa fa-check"></i>Editar</button>--%>
                        <button class="btn btn-success" data-toggle='modal' id="sendEditar" data-target='#add_evento' onclick="return false;">Editar</button>
                        <button type="button" class="btn btn-danger" id="sendCancelar" data-dismiss="modal"><i class="fa fa-times"></i>Finalizar</button>
                        <button type="button" class="btn btn-" data-dismiss="modal" id="closeEvent">Cerrar</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->


        <div class="modal fade" id="add_evento" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel">Agregar nuevo evento</h4>
                    </div>
                    <div class="modal-body">
                        <label for="to">Inicial</label>
                        <div class='input-group date' id='from'>
                            <input type='text' id="from2" name="from" class="form-control" readonly="true" disabled />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                        <br />

                        <label for="to">Final</label>
                        <div class='input-group date' id='to'>
                            <input type='text' name="to" id="to2" class="form-control " readonly="true" disabled />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                        <br />

                        <label for="tipo">Tipo de evento</label>
                        <select class="form-control combo" name="class" id="cbotipoEvento" onchange="selectEvento(0);" runat="server">
                        </select>
                        <br />

                        <label id="lblarea" for="tipo">Entidad que Organiza</label>
                        <select class="form-control comboare" name="class" id="cboarea" runat="server">
                        </select>
                        <br />

                        <div class="well">
                            <div class="row">
                                <%--LISTA DE AREAS--%>
                                <div class="col-md-12" style="text-align: center;">
                                    <label for="tipo">Participantes</label>
                                </div>

                                <div class="col-md-6">

                                    <div class="alertnew alert-info yalert" id="lstAreas" style="height: 200px;">
                                        <label for="tipo">Areas SIS</label>
                                        <br />
                                        <div class="NuevoChecked" id="lstAreaCk">
                                            <%--AQUI SE LISTAN LAS AREAS--%>
                                        </div>
                                    </div>
                                </div>
                                <%--LISTA DE  ENTIDADES--%>
                                <div class="col-md-6">

                                    <div class="alertnew alert-danger" id="lstEntidases" style="height: 200px;">
                                        <label for="tipo">Entidades</label>
                                        <br />
                                        <div class="NuevoChecked" id="lstEntidasesCk">
                                            <%--AQUI SE LISTAN LAS ENTIDADES--%>
                                            <%-- <label for="default" class="btn btn-default" data-original-title="Default" data-placement="right" data-toggle="tooltip">
                                                Default
                                                    <input type="checkbox" id="default" class="badgebox" style="display: none;" /><span class="badge">&check;</span></label>
                                            <label for="primary" class="btn btn-primary">
                                                Primary
                                                    <input type="checkbox" id="primary" class="badgebox" /><span class="badge">&check;</span></label>
                                            <label for="info" class="btn btn-info" data-original-title="Default" data-placement="right" data-toggle="tooltip">
                                                Info
                                                    <input type="checkbox" id="info" class="badgebox" /><span class="badge">&check;</span></label>
                                            <label for="success" class="btn btn-success">
                                                Success
                                                    <input type="checkbox" id="success" class="badgebox" /><span class="badge">&check;</span></label>
                                            <label for="warning" class="btn btn-warning">
                                                Warning
                                                    <input type="checkbox" id="warning" class="badgebox" /><span class="badge">&check;</span></label>
                                            <label for="danger" class="btn btn-danger">
                                                Danger
                                                    <input type="checkbox" id="danger" class="badgebox" /><span class="badge">&check;</span></label>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="alert alert-success" id="lstPersonas" style="max-height: 300px;">
                                            <label for="tipo">Participantes</label>
                                            <input type='text' name="to" id="txtSearch" class="form-control" autocomplete="off" />
                                            <br />
                                            <div class="scrollbar scrollbar-success">
                                                <div class="force-overflow">
                                                    <table class="table personal" id="personal">
                                                        <thead class="">
                                                            <tr>
                                                                <th>Nombre</th>
                                                                <th>Participa</th>
                                                            </tr>
                                                        </thead>

                                                        <tbody id="personalt">
                                                            <%--AQUI SE LISTAN LOS PARTICIPANTES--%>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <label for="title">Título</label>
                            <input type="text" autocomplete="off" name="title" class="form-control" id="title" placeholder="Introduce un título" />
                            <br />

                            <label for="body">Evento</label>

                            <div class="well">
                                <div class="row">
                                    <div class="col-md-12" style="text-align: right;">
                                        <textarea id="body" name="event" class="form-control" rows="3" placeholder="Introduce una descripción del Evento, las agendas se ingresan en el siguiente paso"></textarea>
                                        <br />

                                        <div class="col-md-14">
                                            <div class="col-md-6" style="text-align: left;">
                                                <label for="tipo">Listado de Agenda</label>
                                            </div>

                                            <button type="button" class="btn btn-info" data-target='#add_agenda' onclick="OpenModalAgenda();"><i class="fa fa-comment"></i>Agregar Agenda</button>
                                            <div class="alertnew alert-warning" id="agenda" style="height: 120px;">
                                                <div class="scrollbar2 scrollbar-success">
                                                    <div class="force-overflow">
                                                        <div class="table-responsive">

                                                            <table class="table table-sm table-hover tbAgenda" id="tbAgenda">
                                                                <thead class="thead-default">
                                                                </thead>

                                                                <tbody id="tbodyAgenda">
                                                                    <%--AQUI SE LISTAN LAS AGENDAS--%>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>Cerrar</button>
                                <button type="submit" class="btn btn-success" id="addEvent"><i class="fa fa-check"></i>Agregar</button>
                                <button type="submit" class="btn btn-success" id="setEvent"><i class="fa fa-check"></i>Guardar</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

        <div class="modal fade" id="add_agenda" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="false">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: lightcyan;">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel2">Agregar nuevo punto de agenda</h4>
                    </div>
                    <div class="modal-body">
                        <label for="title">Título</label>
                        <input type="text" id="tituloAgenda" autocomplete="off" name="title" class="form-control" id="titleAgenda" placeholder="Introduce un título" />
                        <br />

                        <label for="body">Descipción</label>
                        <textarea id="bodyAgenda" name="event" class="form-control" rows="3" placeholder="Introduce una descripción de la agenda"></textarea>
                        <br />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>Cerrar</button>
                        <button type="button" class="btn btn-success" id="addAgenda"><i class="fa fa-check" onclick="addAgenda();"></i>Agregar</button>
                        <button type="button" class="btn btn-success" id="editAgenda"><i class="fa fa-check" onclick="edtAgenda(this);"></i>Guardar</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="add_acuerdo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="false">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: lightcyan;">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel3">Agregar nuevo Acuerdo</h4>
                    </div>

                    <div class="modal-body">
                        <label for="title">Título</label>
                        <input type="text" id="tituloAcuerdo" autocomplete="off" name="title" class="form-control" id="titleAgenda" placeholder="Introduce un título" />
                        <br />
                        <label for="title">Agenda</label>
                        <select class="form-control comboage" name="class" id="cboAgenda">
                            <option>Agenda1</option>
                            <option>Agenda2</option>
                        </select>
                        <br />
                        <label for="title">Responsable</label>
                        <select class="form-control combores" name="class" id="cboResponsable">
                            <option>Participante1</option>
                            <option>Participante2</option>
                        </select>
                        <%--  <input type="text" id="tituloAcuerdo" autocomplete="off" name="title" class="form-control" id="titleAcuerdo" placeholder="Introduce un título" />
                        --%>
                        <br />

                        <label for="body">Acuerdo</label>
                        <textarea id="bodyAcuerdo" name="event" class="form-control" rows="5" placeholder="Introduce los términos del acuerdo"></textarea>
                        <br />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>Cerrar</button>
                        <button type="button" class="btn btn-success" id="addAcuerdo"><i class="fa fa-check" onclick="addAcuerdo();"></i>Agregar</button>
                        <button type="button" class="btn btn-success" id="editAcuerdo"><i class="fa fa-check" onclick="editAcuerdo(this);"></i>Guardar</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="add_finaliza" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="false">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: lightcyan;">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel4">Finalización del Evento</h4>
                    </div>

                    <div class="modal-body">
                        <label for="title">Aviso</label><br />
                        <span>Luego de finalizar el evento no podrá realizar ninguna edición sobre él.<br />
                            Si el evento ha sido cancelado indicar en el campo <b>Observacion</b> la razón.
                        </span>
                        <br />
                        <br />
                        <label for="body">Observación</label>
                        <br />
                        <textarea id="bodyAviso" name="event" class="form-control" rows="6" placeholder="Introduce la razón por la que se cancela el evento"></textarea>
                        <br />
                        <label>Acta</label>                         
                            <div class="form-group">
                            <%--<input id="selActa" runat="server" type="file" class="file" onclick="alert();" data-preview-file-type=".pdf" />--%>
                                <input id="x" type="file" class="inputfile file" value="image1" name="image1" required />
                            </div>
                            
                        
                        
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-success" id="setfinaliza"><i class="fa fa-check" onclick="FinalizaEvento();"></i>Terminar Evento</button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>Cancelar</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="sen_reprograma" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="false">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: lightcyan;">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel5">Reprogramación del Evento</h4>
                    </div>

                    <div class="modal-body">
                        <label for="title">Aviso</label><br />
                        <span>Usted no está autorizado a realizar una preprogramación para este evento, sin embargo puede enviar la propuesta al creador del evento
                        </span>
                        <br />
                        <br />
                        <div class='input-group date' id='de'>
                            <input type='text' id="de2" name="from" class="form-control" readonly="true" disabled />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                        <br />

                        <label for="to">Final</label>
                        <div class='input-group date' id='hasta'>
                            <input type='text' name="to" id="hasta2" class="form-control " readonly="true" disabled />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                        <br />


                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-success" id="setReprograma"><i class="fa fa-check" onclick="ReprogramarEvento();"></i>Reprogramar</button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="otro" runat="server">
</asp:Content>
