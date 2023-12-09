window.onload = AgregarOpcionesClases();
window.onload = AgregarOpcionesDuennos(); 

function CrearActivo() {


    var descripcion = document.getElementById("txtDescripciónActivo").value; 
    var valorAdquisición = document.getElementById("txtValorAdquisición").value; 
    var idClase = document.getElementById("selidClase").value; 
    var idUbicacion = document.getElementById("selidUbicacion").value; 
    var idDuenno = document.getElementById("selDuenno").value; 
    var idEstado = document.getElementById("selEstado").value; 

    $.ajax({
        url: "/Activo/CrearActivo",
        type: "POST",
        data: {

            "descripcion": descripcion,
            "valorAdquisición": valorAdquisición,
            "idClase": idClase,
            "idUbicacion": idUbicacion,
            "idDuenno": idDuenno,
            "idEstado": idEstado,
        },
        dataType: "json",
        success: function (data) {

            mensajeSuccess('Activo Creado', 'El activo se creo con exito');

            setTimeout(function () {
                window.location.reload();
            }, 2700);
        }
    })
}


function AgregarOpcionesClases() {
    $.ajax({
        url: "/Clase/optenerClases",
        type: "GET",
        data: {
        },
        dataType: "json",
        success: function (data) {

            var listaClase = document.getElementById("selidClase");
            var newoption = "";

            for (item in data) {

                newoption = document.createElement("option");
                newoption.text = data[item]["descripcionClase"];
                newoption.value = data[item]["idClase"];
                listaClase.options.add(newoption);
            }
        }
    })
}

function AgregarOpcionesDuennos() {
    $.ajax({
        url: "/Usuarios/consultarUsuarios",
        type: "GET",
        data: {
        },
        dataType: "json",
        success: function (data) {

            var listaUsuarios = document.getElementById("selDuenno");
            var newoption = "";


            for (item in data) {

                newoption = document.createElement("option");
                newoption.text = data[item]["nombre"];
                newoption.value = data[item]["idUsuario"];
                listaUsuarios.options.add(newoption);
            }
        }
    })
}
