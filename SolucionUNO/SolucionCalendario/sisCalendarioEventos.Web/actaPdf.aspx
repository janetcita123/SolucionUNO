<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="actaPdf.aspx.vb" Inherits="sisCalendarioEventos.Web.actaPdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acta de Reunion SIS</title>
    <link href="bootstrap/css/bootstrap2.min.css" rel="stylesheet" />
    <link href="bootstrap/css/font-awesome.min-3.2.1.css" rel="stylesheet" />
    <link href="bootstrap/css/font-awesome.min.css" rel="stylesheet" />
    <script src="bootstrap/js/jquery-3.2.1.min.js"></script>
    <script src="pdf/jspdf.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jsEventosResponse = $.parseJSON('<%=jsEventosResponse%>');
            var $area = $('#acta');
            var $nombre = $('#titulo');
            var $descripcion = $('#detalle');
            var $fecha = $('#fecha');
            var $organiza = $('#organiza');
            var nombre = "00000" + jsEventosResponse.id;
            nombre = "ACTA DE REUNIÓN SIS N°" + nombre.substr(-6);

            $area[0].innerText = nombre;
            $nombre[0].innerText = jsEventosResponse.title;
            $descripcion[0].innerText = jsEventosResponse.detalle;
            $organiza[0].innerText = jsEventosResponse.nombrearea;

            var sfecha = ''; var event = [];
            event.date_start = (new Date(parseInt(jsEventosResponse.start)));
            event.date_end = new Date(parseInt(jsEventosResponse.end));
            sfecha = ('0' + event.date_start.getDate()).substr(-2) + ' de ' + ConvertirMes(event.date_start.getMonth()) + ' ' + event.date_start.getFullYear() + ' ' + event.date_start.getHours() + ':' + event.date_start.getMinutes()

            $fecha[0].innerText = sfecha.toUpperCase();

            //TABLAS
            $("#areas tbody").empty();
            var tr = '';
            if (jsEventosResponse.areas.length > jsEventosResponse.entidades.length) {
                for (var i = 0; i < jsEventosResponse.areas.length; i++) {
                    tr = tr + '<tr><td style="width:50%">' + jsEventosResponse.areas[i].sigla + '</td>';

                    if (jsEventosResponse.entidades[i] != null) {
                        tr = tr + '<td style="width:50%">' + jsEventosResponse.entidades[i].sigla + '</td></tr>';
                    } else {
                        tr = tr + '<td style="width:50%"></td></tr>';
                    }
                }
            } else {
                for (var i = 0; i < jsEventosResponse.entidades.length; i++) {
                    if (jsEventosResponse.areas[i] != null) {
                        tr = tr + '<tr><td style="width:50%">' + jsEventosResponse.areas[i].sigla + '</td>';
                    } else {
                        tr = tr + '<tr><td style="width:50%"></td>';
                    }

                    tr = tr + '<td style="width:50%">' + jsEventosResponse.entidades[i].sigla + '</td></tr>';

                }

            }

            $("#areas tbody").append(tr);

            tr = '<tr>';
            $("#personas tbody").empty();
            for (var i = 0; i < jsEventosResponse.personas.length; i++) {
                var num = i + 1;
                tr = tr + '<td>' + num + '</td>';
                tr = tr + "<td style='text-align:left'>" + jsEventosResponse.personas[i].name + '</td>';
                tr = tr + '<td>' + jsEventosResponse.personas[i].dni + '</td>';
                tr = tr + '<td>' + '</td></tr>';
            }
            $("#personas tbody").append(tr);

            $("#agenda tbody").empty();
            var cadena = "<tr><td>";
            for (var i = 0; i < jsEventosResponse.agenda.length; i++) {

                cadena = cadena + ' <div class="list-group alert-info" style="text-align:left;"><a href="#" class="list-group-item">' +
                    '<h4 class="list-group-item-heading ">' + jsEventosResponse.agenda[i].tituloa + '</h4><p class="list-group-item-text ">' +
                   jsEventosResponse.agenda[i].detallea + '</p>  ';

            }

            cadena = cadena + '</div></td></tr>';
            $("#agenda tbody").append(cadena);

            var cadena = "<tr><td>";
            $("#observacion tbody").empty();
            for (var i = 0; i < jsEventosResponse.observaciones.length; i++) {
                cadena = cadena + ' <div class="list-group alert-info" style="text-align:left;"><a href="#" class="list-group-item">' +
                    '<h4 class="list-group-item-heading ">' + jsEventosResponse.observaciones[i].titulo + '</h4><p class="list-group-item-text ">' +
                   jsEventosResponse.observaciones[i].descripcion + '</p>   ';

            }
            cadena = cadena + '</div></td></tr>';
            $("#observacion tbody").append(cadena);

            var cadena = "<tr><td>";
            $("#acuerdos tbody").empty();
            for (var i = 0; i < jsEventosResponse.acuerdos.length; i++) {

                cadena = cadena + ' <div class="list-group alert-info" style="text-align:left;"><a href="#" class="list-group-item">' +
                   '<h4 class="list-group-item-heading ">' + jsEventosResponse.acuerdos[i].tituloac + '</h4>' +
                   '<a href="#" class="list-group-item">' +
                '<h4 class="list-group-item-heading ">Encargado:' + jsEventosResponse.acuerdos[i].participante + '</h4><p class="list-group-item-text ">' +
                  jsEventosResponse.acuerdos[i].descripcion + '</p>   ';
                //cadena = cadena + ' <div class="list-group alert-info" style="text-align:left;"><a href="#" class="list-group-item">' +
                //   '<h4 class="list-group-item-heading ">Encargado:' + jsEventosResponse.acuerdos[i].tituloac + '</h4><a href="#" class="list-group-item">' +
                //'<h4 class="list-group-item-heading ">' + jsEventosResponse.acuerdos[i].participante + '</h4><p class="list-group-item-text ">' +
                //  jsEventosResponse.acuerdos[i].descripcion + '</p> ';

            }
            cadena = cadena + "</div></td></tr>";
            $("#acuerdos tbody").append(cadena);
        });


        function ConvertirMes(mes) {
            switch (mes) {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
            }

        }

    </script>
    <style type="text/css">
        #acta {
            font-weight: 700;
        }

        .auto-style1 {
            color: #fff;
            font-size: small;
        }

        .auto-style2 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <article class="post type-post status-publish format-standard hentry">
            <header class="entry-header">
                   <div = "cabecera" class="img-thumbnail" style="width:100%;text-align:center;padding:0px 30px 0px 30px;">
            <img src="Images/logosis.png" class="alert-info" style="width:200px;height:80px;float:right;"/>
        </div>
            </header><br />
           
           <div style="width:100%; padding:0px 40px 0px 40px;" ><h3><span id ="acta">ACTA DE REUNIÓN SIS N°000100</span></h3></div>
            <!-- .entry-header -->

            <div class="well-sm" style="padding-left:10%;"> 
            <span class="label label-info" style="width:250px; font-size: small;background-color:lightgray; color: #000000;">TITULO:&nbsp;</span>&nbsp;&nbsp;<span id="titulo" >NOMBRE DEL EVENTO</span>
            </div>
            <div class="well-sm" style="padding-left:10%;"> 
            <span class="label label-info" style="width:250px; font-size: small;background-color:lightgray; color: #000000;">DETALLE:</span>&nbsp;&nbsp;<span id="detalle" >DETALLE DEL EVENTO DETALLE DEL EVENTO DETALLE DEL EVENTO DETALLE DEL EVENTO</span>
            </div>
           <div class="well-sm" style="padding-left:10%;"> 
            <span class="label label-info" style="width:250px; font-size: small;background-color:lightgray; color: #000000;">FECHA:&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;<span id="fecha">20/12/2017</span>
            </div>
            
           <div class="well-sm" style="padding-left:10%;"> 
            <span class="label label-info" style="width:250px; font-size: small;background-color:lightgray; color: #000000;">ORGANIZADOR:</span>&nbsp;&nbsp;<span id="organiza" >AREA RESPONSABLE DEL EVENTO</span>
            </div>
           <br />
           <div class="table">
             
            <br />
            <br />
               <table id="areas" class="table table-bordered" style="width:80%;text-align:center" align="center">
                <thead >
                     <tr >
                        <th colspan="3" scope="col"><div class="list-group-item active" style="text-align:center;" font-size: xx-small;">
                   <span class="label label fa-2x" style="width:100%;">PARTICIPANTES</span>
                </div></th>
                       
                    </tr>
                    
                </thead>
                <tbody>
                    <tr> 
                        <td>GREP</td>
                        <td>MINSA</td>
                    </tr>
                </tbody>
            </table>
               <br />
            <table id="personas" class="table table-bordered" style="width:80%;text-align:center" align="center">
                <thead style="background-color:#428bca">
                    <tr class="auto-style1">
                        <th scope="col" style="width:30px;" class="auto-style2">#</th>
                        <th scope="col" style="text-align:center;" class="auto-style2">Personal</th>
                         <th scope="col" style="text-align:center;width:100px;" class="auto-style2">Dni</th>
                        <th scope="col" style="text-align:center;width:350px;" class="auto-style2">Firma</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>Mark</td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
           </div>

           <div>
                
               <br />
               <table id="agenda" class="table table-bordered" style="width:80%;text-align:center" align="center">
                  <thead style="">
                    <tr>
                        <th scope="col"><div class="list-group-item active" style="text-align:center;" font-size: xx-small;">
                   <span class="label label fa-2x" style="width:100%;">AGENDA</span>
                </div></th>
                       
                    </tr>
                </thead>
                <tbody>
                   
                </tbody>
            </table>
            </div>

        <div>
               
               <table id="observacion" class="table table-bordered" style="width:80%;text-align:center" align="center">
                <thead style="">
                    <tr>
                        <th scope="col"><div class="list-group-item active" style="text-align:center;" font-size: xx-small;">
                   <span class="label label fa-2x" style="width:100%;">OBSERVACIONES</span>
                </div></th>
                       
                    </tr>
                </thead>
                <tbody>
                    <tr>
                         
                        <td> 
                            <div class="list-group alert-info" style="text-align:left;">
                                <a href="#" class="list-group-item">
                                    <h4 class="list-group-item-heading ">TITULO DE LA OBSERVACION</h4>
                                        <p class="list-group-item-text ">DESCRIPCION DETALLADA DE LA AGENDA DESCRIPCION DETALLADA DE LA AGENDA DESCRIPCION DETALLADA DE LA AGENDA</p>
                                </a>
                                <a href="#" class="list-group-item">
                                    <h4 class="list-group-item-heading">TITULO DE LA AGENDA</h4>
                                        <p class="list-group-item-text">DESCRIPCION DETALLADA DE LA AGENDA</p>
                                </a>
                            </div>
                        </td>
                      
                    </tr>
                    
                    
                </tbody>
            </table>
            </div>


           <div>
               
               <br />
               <table id="acuerdos" class="table table-bordered" style="width:80%;text-align:center" align="center">
                  <thead style="">
                    <tr>
                        <th scope="col"><div class="list-group-item active" style="text-align:center;" font-size: xx-small;">
                   <span class="label label fa-2x" style="width:100%;">ACUERDOS</span>
                </div></th>
                    </tr>
                </thead>
                <tbody>
                     
                </tbody>
            </table>
            </div>

           <br />

           <br />
            <!-- .entry-content -->
   
 
           <div id="boton1" style="text-align:right;padding:0px 50px 0px 0px"  >
             
            <asp:ImageButton ID="BtnCreatePdf" runat="server" ImageUrl="~/images/downloadpdf.png" ToolTip="Descargar"    /></div>
  
        </article>
        </form>
</body>
</html>
