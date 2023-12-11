window.onload = drawClassAssetResume(); 

function drawClassAssetResume() {

    rows = document.getElementById("datatablesSimple").rows.length - 1;
    disponibles = 30 - rows

    const data = {
        labels: ['Utilizadas', 'Disponibles'],
        datasets: [
            {
                label: 'Licencias',
                backgroundColor: ['#146CA4', '#ADD8E6'],
                data: [rows, disponibles]
            }
        ]
    };

    const config = {
        type: 'doughnut',
        data: data,
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'bottom',
                },
                title: {
                    display: false,
                }   
            }
        },
    }

    const myChart = new Chart(
        document.getElementById('licenseChart'),
        config
    )

}

function validarCorreo() {

    let correo = document.getElementById("txtCorreoElectronico").value;

    $.ajax({
        url: "/Usuarios/buscarCorreo",
        type: "POST",
        data: {
            "correo": correo
        },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                document.getElementById("alrMensajeError").innerHTML = "El correo que desea ingresar ya existe"
                document.getElementById("alrMensajeError").style.display = "";
                document.getElementById("btnCrearUsuario").style.display = "none"; 

            } else {
                document.getElementById("btnCrearUsuario").style.display = ""; 
                document.getElementById("alrMensajeError").style.display = "none";
                validarNombre(); 
            }

        }
    })

}

function validarNombre() {
    txtNombre = document.getElementById("txtNombre").value; 


    if (txtNombre.length <= 0) {
        document.getElementById("alrMensajeError").innerHTML = "Complete toda la información requerida"
        document.getElementById("alrMensajeError").style.display = "";
        document.getElementById("btnCrearUsuario").style.display = "none"; 

    } else {
        document.getElementById("btnCrearUsuario").style.display = "";
        document.getElementById("alrMensajeError").style.display = "none";
    }
}

function crearUsuario() {

    var correoElectronico = document.getElementById("txtCorreoElectronico").value;
    var nombre = document.getElementById("txtNombre").value;
    var role = document.getElementById("selRole").value

    $.ajax({
        url: "/Usuarios/crearUsuario",
        type: "POST",
        data: {
            "correoElectronico": correoElectronico,
            "nombre": nombre,
            "role":role
        },
        dataType: "json",
        success: function (data) {
            console.log("User Created");
            window.location.reload(); 
        }
    })
}

function modificarEstadoUsuario(idUsuario, estadoUsuario) {
    if (estadoUsuario == 0) {
        requestType = "/Usuarios/activarUsuario"
    } else {
        requestType = "/Usuarios/desactivarUsuario"
    }

    $.ajax({
        url: requestType,
        type: "POST",
        data: {
            "idUsuario": idUsuario
        },
        dataType: "json",
        success: function (data) {

            auxiliar = data; 
            window.location.reload();

        }
    })
}

function editarUsuario(idUsuario) {

    $.ajax({
        url: "/Usuarios/consultarUsuario",
        type: "GET",
        data: {
            "idUsuario": idUsuario
        },
        dataType: "json",
        success: function (data) {

            //Complete default information
            document.getElementById("txtIdUsuario").value = data["idUsuario"]
            document.getElementById("txtEditNombre").value = data["nombre"]
            document.getElementById("txtEditCorreoElectronico").value = data["correo"]
            document.getElementById("selEditRole").value = data["idRole"]
            document.getElementById("editModalHeader").innerHTML = "Editar Usuario " + data["nombre"]

            //Show modal
            $("#editUserModal").modal('show'); 

        }
    })

}

function actualizarUsuario() {

    idUsuario = document.getElementById("txtIdUsuario").value; 
    nombreUsuario = document.getElementById("txtEditNombre").value; 
    idRoleUsuario = document.getElementById("selEditRole").value

    $.ajax({
        url: "/Usuarios/actualizarUsuario",
        type: "POST",
        data: {
            "idUsuario": idUsuario,
            "nombreUsuario": nombreUsuario,
            "idRoleUsuario": idRoleUsuario
        },
        dataType: "json",
        success: function (data) {

            window.location.reload(); 

        }
    })

}