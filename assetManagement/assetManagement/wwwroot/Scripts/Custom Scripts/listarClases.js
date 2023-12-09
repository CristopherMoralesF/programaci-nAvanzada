
window.onload = consultarCuentaActivos();

function consultarCuentaActivos() {
    $.ajax({
        url: "/CuentasContables/consultarCuenta",
        type: "GET",
        data: {
        },
        dataType: "json",
        success: function (data) {

            //Select the list where the accounts are going to be added. 
            var newOp = "";
            var list = document.getElementById("cuentaActivo");
            var list2 = document.getElementById("cuentaGasto");
            var list3 = document.getElementById("cuentaDepAc");

            //Add a default option for each list.
            //Create the option
            newOp = document.createElement("option")
            newOp.text = "Seleccione una cuenta"
            newOp.value = "0"
            list.options.add(newOp)

            newOp = document.createElement("option")
            newOp.text = "Seleccione una cuenta"
            newOp.value = "0"
            list2.options.add(newOp)

            newOp = document.createElement("option")
            newOp.text = "Seleccione una cuenta"
            newOp.value = "0"
            list3.options.add(newOp)

            //Add the accounts options based on the data base. 
            for (item in data) {
                if (data[item]["categoriaCuenta"]["idCategoria"] == 1) {

                    newOp = document.createElement("option")
                    newOp.text = data[item]["idCuenta"] + " - " + data[item]["descripcionCuenta"]
                    newOp.value = data[item]["idCuenta"] 
                    list.options.add(newOp)
                }

                else if (data[item]["categoriaCuenta"]["idCategoria"] == 5) {

                    newOp = document.createElement("option")
                    newOp.text = data[item]["idCuenta"] + " - " + data[item]["descripcionCuenta"]
                    newOp.value = data[item]["idCuenta"]
                    list2.options.add(newOp)
                }
                else if (data[item]["categoriaCuenta"]["idCategoria"] == 6) {

                    newOp = document.createElement("option")
                    newOp.text = data[item]["idCuenta"] + " - " + data[item]["descripcionCuenta"]
                    newOp.value = data[item]["idCuenta"]
                    list3.options.add(newOp)
                }
            }


        }
    })
}

function valirdarClase(idSelect, tipoCuenta) {

    var cuentaContable = document.getElementById(idSelect).value;

    $.ajax({
        url: "/CuentasContables/validarCuentaContableClase",
        type: "GET",
        data: {
            "cuentaContable": cuentaContable,

        },
        dataType: "json",
        success: function (data) {

            if (data == false) {

                //Show the error
                document.getElementById("alrMensajeError").innerHTML = "La cuenta de " + tipoCuenta + " ya esta siendo utilizada en otra clase"
                document.getElementById("alrMensajeError").style.display = "";

                //Hide the button
                document.getElementById("btnCrearClase").style.display = "none";
            } else {

                //Hide error message
                document.getElementById("alrMensajeError").style.display = "none"; 

                //Show button
                document.getElementById("btnCrearClase").style.display = ""; 
            }

        }
    })
}

function traerInformacionClase(idClase) {

    $.ajax({
        url: "/Clase/buscarInformacionClase",
        type: "GET",
        data: {
            "idClase": idClase,

        },
        dataType: "json",
        success: function (data) {

           
            //Complete information about the class
            var newTitle = "<h4>Detalle Validaciones " + data["descripcionClase"] + "</h4>"
            var bodyTable = ""; 

            document.getElementById("txtIDClase").value = data["idClase"];
            document.getElementById("detalleClaseModalHeader").innerHTML = newTitle

            for (validacion in data["listaValidaciones"]){
                bodyTable += "<tr>"
                bodyTable += "<td>" + data["descripcionClase"] + "</td>"
                bodyTable += "<td>" + data["listaValidaciones"][validacion]["descripcionValidacion"] + "</td>"
                bodyTable += "</tr>"
            }

            document.getElementById("validacionesTableBody").innerHTML = bodyTable

            //Show modal
            $("#detalleClaseModal").modal('show'); 

        }
    })

}

function crearClase() {

    var clase = document.getElementById("Clase").value;
    var vidaUtil = document.getElementById("vidaUtil").value;
    var cuentaActivo = document.getElementById("cuentaActivo").value;
    var cuentaGasto = document.getElementById("cuentaGasto").value;
    var cuentaDepAc = document.getElementById("cuentaDepAc").value;

    $.ajax({
        url: "/Clase/crearClase",
        type: "POST",
        data: {
            "clase": clase,
            "vidaUtil": vidaUtil,
            "activo": cuentaActivo,
            "gasto": cuentaGasto,
            "depAc": cuentaDepAc

        },
        dataType: "json",
        success: function (data) {
            mensajeSuccess('Creación Clase', 'La clase se creo con exito');

            setTimeout(function () {
                window.location.reload();
            }, 4000);
        }
    })
}

function crearValidacionClase() {

    var idClase = document.getElementById("txtIDClase").value
    var descripcion = document.getElementById("txtValidacion").value

    $.ajax({
        url: "/Clase/crearValidacionClase",
        type: "POST",
        data: {
            "idClase": idClase,
            "descripcionValidacion": descripcion
        },
        dataType: "json",
        success: function (data) {

            if(data > 0){
                mensajeSuccess('Validación Creada', 'La validación se creo de forma exitosa');

                setTimeout(function () {
                    window.location.reload(); 
                }, 2700);

            }

        }
    })

}