<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pdfNew.aspx.vb" Inherits="SolucionCalendario.pdfNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>SelectPdf Free Html To Pdf Converter for .NET VB.NET / ASP.NET - Setting Document Properties with Html to Pdf Converter</title>
    <meta name="description" content="SelectPdf Free Html To Pdf Converter - Document Properties with Html to Pdf Converter Sample for VB.NET / ASP.NET. Pdf Library for .NET with full sample code in C# and VB.NET." itemprop="description">
    <meta name="keywords" content="document properties, html to pdf converter, pdf library, sample code, html to pdf, pdf converter" itemprop="keywords">
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
                nombre = "ACTA DE REUNIÓN SIS N°"+nombre.substr(-6);

                $area[0].innerText = nombre;
                $nombre[0].innerText = jsEventosResponse.title;
                $descripcion[0].innerText = jsEventosResponse.detalle;
                $organiza[0].innerText = jsEventosResponse.nombrearea;

                var sfecha = ''; var event = [];
                event.date_start = (new Date(parseInt(jsEventosResponse.start)));
                event.date_end = new Date(parseInt(jsEventosResponse.end));
                sfecha = ('0'+event.date_start.getDate()).substr(-2) + ' de ' + ConvertirMes(event.date_start.getMonth())  + ' ' + event.date_start.getFullYear() + ' ' + event.date_start.getHours() + ':' + event.date_start.getMinutes()

                $fecha[0].innerText = sfecha.toUpperCase();

                //TABLAS
                $("#areas tbody").empty();
                var tr = '';
                if (jsEventosResponse.areas.length > jsEventosResponse.entidades.length) {
                    for (var i = 0; i < jsEventosResponse.areas.length; i++) {
                        tr = tr + '<tr><td>' + jsEventosResponse.areas[i].sigla + '</td>';

                        if (jsEventosResponse.entidades[i] != null) {
                            tr = tr + '<td>' + jsEventosResponse.entidades[i].sigla + '</td></tr>';
                        } else {
                            tr = tr + '<td></td></tr>';
                        }
                    }
                } else {
                    for (var i = 0; i < jsEventosResponse.entidades.length; i++) {
                        if (jsEventosResponse.areas[i] != null) {
                            tr = tr + '<tr><td>' + jsEventosResponse.areas[i].sigla + '</td>';
                        } else {
                            tr = tr + '<tr><td></td>';
                        }
                        
                        tr = tr + '<td>' + jsEventosResponse.entidades[i].sigla + '</td></tr>';
                        
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
                    tr = tr + '<td>' +  '</td></tr>';
                }
                $("#personas tbody").append(tr);

                tr = '<tr>';
                $("#agenda tbody").empty();
                for (var i = 0; i < jsEventosResponse.agenda.length; i++) {
                    var num = i + 1;
                    tr = tr + '<td>' + num + '</td>';
                    tr = tr + "<td style='text-align:left'>" + jsEventosResponse.agenda[i].tituloa + '</td>';
                    tr = tr + "<td style='text-align:left'>" + jsEventosResponse.agenda[i].detallea + '</td>';
                    tr = tr + '</tr>';
                }
                $("#agenda tbody").append(tr);

                tr = '<tr>';
                $("#acuerdos tbody").empty();
                for (var i = 0; i < jsEventosResponse.acuerdos.length; i++) {
                    var num = i + 1;
                    tr = tr + '<td>' + num + '</td>';
                    tr = tr + "<td style='text-align:left'>" + jsEventosResponse.acuerdos[i].tituloac + '</td>';
                    tr = tr + "<td style='text-align:left'>" + jsEventosResponse.acuerdos[i].descripcion + '</td>';
                    tr = tr + '</tr>';
                }
                $("#acuerdos tbody").append(tr);
            });

     
            function ConvertirMes(mes) {
                switch(mes) {
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
       <article class="post type-post status-publish format-standard hentry">
            <header class="entry-header">
                   <div = "cabecera" class="img-thumbnail" style="width:100%;text-align:center;padding:0px 30px 0px 30px;">
            <img src="Images/logosis.png" class="alert-info" style="width:200px;height:80px;float:right;"/>
        </div>
            </header><br />
           
           <div style="width:100%; padding:0px 40px 0px 40px;" ><h3><span id ="acta">ACTA DE REUNIÓN SIS N°000100</span></h3></div>
            <!-- .entry-header -->

            <div class="well-sm" style="padding-left:10%;"> 
            <span class="label label-info" style="width:250px;">TITULO:&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;<span id="titulo" >NOMBRE DEL EVENTO</span>
            </div>
            <div class="well-sm" style="padding-left:10%;"> 
            <span class="label label-info" style="width:250px;">DETALLE:</span>&nbsp;&nbsp;<span id="detalle" >DETALLE DEL EVENTO DETALLE DEL EVENTO DETALLE DEL EVENTO DETALLE DEL EVENTO</span>
            </div>
           <div class="well-sm" style="padding-left:10%;"> 
            <span class="label label-info" style="width:250px;">FECHA:&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;<span id="fecha">20/12/2017</span>
            </div>
           <div class="well-sm" style="padding-left:10%;"> 
            <span class="label label-info" style="width:250px;">ORGANIZADOR:</span>&nbsp;&nbsp;<span id="organiza" >AREA RESPONSABLE DEL EVENTO</span>
            </div>
           <br />
           <div class="table">
            <div class="well-sm" style="width:100%;text-align:center"> 
            <span class="label label-success fa-2x" style="width:250px;">PARTICIPANTES</span>
            </div>
            <br />
            <br />
               <table id="areas" class="table table-bordered" style="width:70%;text-align:center" align="center">
                <thead style="background-color:darkgray">
                    <tr> 
                        <th scope="col" style="text-align:center">Area</th>   
                        <th scope="col" style="text-align:center">Entidad</th>                       
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
            <table id="personas" class="table table-bordered" style="width:70%;text-align:center" align="center">
                <thead style="background-color:darkgray">
                    <tr>
                        <th scope="col" style="width:30px;">#</th>
                        <th scope="col" style="text-align:center;">Personal</th>
                         <th scope="col" style="text-align:center;width:100px;">Dni</th>
                        <th scope="col" style="text-align:center;width:350px;">Firma</th>
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
               <div class="well-sm" style="width:100%;text-align:center">
                   <span class="label label-success fa-2x" style="width:250px;">AGENDA</span>
                </div>
               <br />
               <table id="agenda" class="table table-bordered" style="width:70%;text-align:center" align="center">
                <thead style="background-color:darkgray">
                    <tr>
                        <th scope="col" style="width:30px;">#</th>
                        <th scope="col" style="text-align:center;width:30%;">Titulo</th>
                        <th scope="col" style="text-align:center">Descripcion</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DE LA AGENDA</td>
                        <td>DESCRIPCION DETALLADA DE LA AGENDA</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                        <td>TITULO DE LA AGENDA</td>
                        <td>DESCRIPCION DETALLADA DE LA AGENDA</td>
                    </tr>
                </tbody>
            </table>
            </div>

           <div>
               <div class="well-sm" style="width:100%;text-align:center">
                   <span class="label label-success fa-2x" style="width:250px;">ACUERDOS</span>
                </div>
               <br />
               <table id="acuerdos" class="table table-bordered" style="width:70%;text-align:center" align="center">
                <thead style="background-color:darkgray">
                    <tr>
                        <th scope="col" style="width:30px;">#</th>
                        <th scope="col" style="text-align:center">Titulo</th>
                        <th scope="col" style="text-align:center">Descripcion</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                       <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                       <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                       <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                       <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                       <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                       <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                       <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                    <tr>
                        <th scope="row">1</th>
                        <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                     <tr>
                        <th scope="row">2</th>
                       <td>TITULO DEL ACUERDO</td>
                        <td>DESCRIPCION DEL ACUERDO</td>
                    </tr>
                </tbody>
            </table>
            </div>

           <br />

           <br />
            <!-- .entry-content -->
   
 
           <div id="boton1" style="text-align:right;padding:0px 50px 0px 0px">

            <asp:ImageButton ID="BtnCreatePdf" runat="server" ImageUrl="~/images/downloadpdf.png" ToolTip="Descargar"    /></div>
     
        </article>
        <!-- #post -->
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="otro" runat="server">

</asp:Content>
