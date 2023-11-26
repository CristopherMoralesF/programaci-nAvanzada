window.onload = AgregarOpcionesClases(); 

function editarValidacion(idActivo, idValidacion) {

    Swal.fire({
        title: 'Indique el valor de la validación',
        input: 'text',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Modificar',
        confirmButtonColor: '#4C9A2A',
        showLoaderOnConfirm: true,
        preConfirm: (valor) => {

            //Editar el activo
            $.ajax({
                url: "/Activo/editarValidacion",
                type: "POST",
                data: {
                    "idActivo": idActivo,
                    "idValidacion": idValidacion,
                    "valor": valor
                },
                dataType: "json",
                success: function (data) {
                    window.location.reload(); 
                }
            })


        },
        allowOutsideClick: () => !Swal.isLoading()
    })

}

function agregarValidacion(idActivo, idValidacion) {

    Swal.fire({
        title: 'Indique el valor de la validación',
        input: 'text',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Agregar',
        confirmButtonColor: '#4C9A2A',
        showLoaderOnConfirm: true,
        preConfirm: (valor) => {

            //Editar el activo
            $.ajax({
                url: "/Activo/agregarValidacion",
                type: "POST",
                data: {
                    "idActivo": idActivo,
                    "idValidacion": idValidacion,
                    "valor": valor
                },
                dataType: "json",
                success: function (data) {
                    window.location.reload(); 
                }
            })


        },
        allowOutsideClick: () => !Swal.isLoading()
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
            var listaClase = document.getElementById("selNewClase");
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

function modificarClase(idActivo) {


    var idNuevaClase = document.getElementById("selNewClase").value; 

    $.ajax({
        url: "/Activo/modificarClase",
        type: "POST",
        data: {
            idActivo: idActivo, 
            idNuevaClase: idNuevaClase
        },
        dataType: "json",
        success: function (data) {
            mensajeSuccess('Activo Actualizado', 'El activo se actualizo con exito');

            setTimeout(function () {
                window.location.reload();
            }, 2700);
        }
    })
}