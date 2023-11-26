function mensajeError(title, body) {
    Swal.fire({
        icon: 'error',
        title: title,
        text: body,
    })
}

function mensajeSuccess(title, body) {
    Swal.fire({
        icon: 'success',
        title: title,
        text: body,
        showConfirmButton: false,
        timer: 2500
    })
}