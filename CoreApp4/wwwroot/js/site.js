// Write your JavaScript code.
function dr_mostrarDialogo(idContenedor, texto, color) {
    if (color == 'danger')
        var div = '<div class="alert alert-danger alert-dismissible"><i class="icon fa fa-ban"></i></div>';
    else if (color == 'warning')
        var div = '<div class="alert alert-warning alert-dismissible"><i class="icon fa fa-warning"></i></div>';
    else if (color == 'success')
        var div = '<div class="alert alert-success alert-dismissible"><i class="icon fa fa-check"></i></div>';
    else
        var div = '<div class="alert alert-info alert-dismissible"><i class="icon fa fa-check"></i></div>';
    var comp = $(div).append(texto);
    $('#' + idContenedor).html(comp);
}

function dr_mostrarMensajesRespuestaTransaccion(idContenedor, respuesta) {
    $('#' + idContenedor).children().remove();
    if (respuesta.Contenido) {
        var idgen = idContenedor + "_cont";
        $('#' + idContenedor).append("<div id=" + idgen + "></div>");
            dr_mostrarDialogo(idgen, respuesta.Contenido, 'success');
    }
    if (respuesta.msjError) {
        var idgen = idContenedor + "_error";
        $('#' + idContenedor).append("<div id=" + idgen + "></div>");
        dr_mostrarDialogo(idgen, respuesta.msjError, 'danger');
    }
}