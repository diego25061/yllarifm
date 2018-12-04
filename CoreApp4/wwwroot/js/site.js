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


//OBJETOSSSSSSSSSSSSSS en Js

function Biblia(mes, anho) {
    var self = this;
    self.mes = mes;
    self.anho = anho;
}

function Transporte(fecha, ciudad, horaRecojo, horaSalida, vuelo, servicio, pasajeros, nombrePasajero, vr, tc, transp, obs) {
    var self = this;
    self.fecha = ko.observable(fecha);
    self.ciudad = ko.observable(ciudad);
    self.horaRecojo = ko.observable(horaRecojo);
    self.horaSalida = ko.observable(horaSalida);
    self.vuelo = ko.observable(vuelo);
    self.servicio = ko.observable(servicio);
    self.pasajeros = ko.observable(pasajeros);
    self.nombrePasajero = ko.observable(nombrePasajero);
    self.vr = ko.observable(vr);
    //self.agencia = ko.observable(agencia);
    self.tc = ko.observable(tc);
    self.transp = ko.observable(transp);
    self.obs = ko.observable(obs);
}

function Servicio(fecha, ciudad, servicio, hotel, pasajeros, nombrePasajero, tren, alm, obs) {
    var self = this;
    self.fecha = ko.observable(fecha);
    self.ciudad = ko.observable(ciudad);
    self.servicio = ko.observable(servicio);
    self.hotel = ko.observable(hotel);
    self.pasajeros = ko.observable(pasajeros);
    self.nombrePasajero = ko.observable(nombrePasajero);
    //self.agencia = ko.observable(agencia);
    self.tren = ko.observable(tren);
    self.alm = ko.observable(alm);
    self.obs = ko.observable(obs);
}


//apiiiiiii


function cargarBiblias( fail ) {
    var biblias;
    $.getJSON("/api/biblias", function asd(data) {
        data.reverse();
        ko.mapping.fromJS(data, {}, biblias);
    }).fail(fail);

    return biblias;
}

function crearBibliaOnFly(mes, anho, successFunc, failFunc) {
    $.post("/api/biblias/crear", Biblia(mes, anho)).success(successFunc).fail(failFunc);
}