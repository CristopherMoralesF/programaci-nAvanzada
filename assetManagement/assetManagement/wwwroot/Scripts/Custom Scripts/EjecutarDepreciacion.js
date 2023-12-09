function validarFormulario() {

    var clase = document.getElementById("idClase").value; 
    var descripcionAsiento = document.getElementById("asientoDepreciacion_descripcion").value; 

    if (clase == 0 || descripcionAsiento == "") {

        document.getElementById("mensajeError").value = "Complete la información requerida"
        document.getElementById("mensajeError").style.display = "";
        document.getElementById("btnEjecutarDepreciación").style.display = "none";

    } else {

        document.getElementById("mensajeError").style.display = "none";
        document.getElementById("btnEjecutarDepreciación").style.display = "";

    }


}


function ejecutarDepreciacion() {

    var idClase = document.getElementById("idClase").value;
    var descripcionAsiento = document.getElementById("asientoDepreciacion_descripcion").value; 

    //Print table
    $.ajax({
        url: "/Clase/EjecutarDepreciacion",
        type: "POST",
        data: {
            "idClase": idClase,
            "descripcionAsiento": descripcionAsiento
        },
        dataType: "json",
        success: function (data) {

            auxiliar = data; 
            tableRows = ""; 

            for (line in data) {
                
                tableRows += "<tr>";
                tableRows += "<td>" + data[line]["descripcionActivo"] + "</td>";
                tableRows += "<td>" + new Intl.NumberFormat('es-ES').format(data[line]["valorAdquisicion"]) + "</td>";
                tableRows += "<td>" + data[line]["periodosDepreciados"] + "</td>";
                tableRows += "<td>" + data[line]["descripcionClase"] + "</td>";
                tableRows += "<td>" + data[line]["vidaUtil"] + "</td>";
                tableRows += "<td>" + new Intl.NumberFormat('es-ES').format(data[line]["depreciacionMensual"]) + "</td>";
                tableRows += "<td>" + new Intl.NumberFormat('es-ES').format(data[line]["depreciacionAcumulada"]) + "</td>";
                tableRows += "</tr>";

            }

            document.getElementById("tblAuxiliarBody").innerHTML = tableRows; 
            document.getElementById("tblResultadoDepreciación").style.display = ""; 

            mensajeSuccess("Depreciación Ejecutada","La ejecición para la clase seleccionada fue exitosa, valide el auxiliar de depreciación")
        }
    })
}