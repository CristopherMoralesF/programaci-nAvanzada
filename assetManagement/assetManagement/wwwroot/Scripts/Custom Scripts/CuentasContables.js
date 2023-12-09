function validarCuentaDuplicada() {

    idCuenta = document.getElementById("txtCuentaContable").value; 

    $.ajax({
        url: "/CuentasContables/consultarCuentaContable",
        type: "GET",
        data: {
            "idCuentaContable": idCuenta
        },
        dataType: "json",
        success: function (data) {

            if (data != "Ok") {
                document.getElementById("alrMensajeError").innerHTML = data;
                document.getElementById("alrMensajeError").style.display = "";
                document.getElementById("btnCrearCuenta").style.display = "none";
            } else {
                document.getElementById("btnCrearCuenta").style.display = "";
                document.getElementById("alrMensajeError").style.display = "none"; 
                validarCategoriaCuenta(); 
            }

        }
    })

}

function validarCategoriaCuenta() {

    cuenta = document.getElementById("txtCuentaContable").value; 
    categoria = document.getElementById("selCategoriaCuenta").value; 

    if (cuenta.substring(0, 1) == categoria) {
        document.getElementById("btnCrearCuenta").style.display = "";
        document.getElementById("alrMensajeError").style.display = "none";
    } else {
        document.getElementById("alrMensajeError").innerHTML = "La cuenta seleccionada no coincide con la categoria indicada";
        document.getElementById("alrMensajeError").style.display = "";
        document.getElementById("btnCrearCuenta").style.display = "none";
    }

}

function agregarCuentaContable() {

    idCuenta = document.getElementById("txtCuentaContable").value; 
    descripcionCuenta = document.getElementById("txtDescripciónCuenta").value; 
    idCategoria = document.getElementById("selCategoriaCuenta").value; 
    naturalezaCuenta = document.getElementById("selNaturalezaCuenta").value

    $.ajax({
        url: "/CuentasContables/CrearCuentaContable",
        type: "POST",
        data: {
            "idCuenta": idCuenta,
            "descripcionCuenta": descripcionCuenta,
            "idCategoria": idCategoria,
            "naturalezaCuenta": naturalezaCuenta
        },
        dataType: "json",
        success: function (data) {

            Swal.fire({
                title: 'Cuenta Creada',
                text: "La cuenta fue creada con exito",
                icon: 'success',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok!'
            }).then((result) => {
                if (result.isConfirmed) {
                    setTimeout(function () {
                        window.location.reload();
                    }, 2700);
                }
            })
        }
    })


}
