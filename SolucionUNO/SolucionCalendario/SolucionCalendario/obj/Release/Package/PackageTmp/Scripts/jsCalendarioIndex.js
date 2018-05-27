var sUsuario = 'PCORONADOF';
//var usuario = [];
//var rol = [];
var idAcuerdoGen = '';
var idEventoGeneral = '';

$(function () {
    $("[data-toggle='tooltip']").tooltip();
});

//Acceso a botones
var $btnEditar = 0;
var $btnFinalizar = 0;
var $btnAcuerdos = 0;
var $btnPdf = 0;
var $btnAgendas = 0;
var $blockCheck = 0;
var $reprogramar = 0;
var $creador = 0;
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
    jsEventos = jsEventos.error
}
var date = new Date();