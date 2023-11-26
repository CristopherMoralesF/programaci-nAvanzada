//Creaste function to download in an excel file the content of the table. 
function exportJournalsExcel(tableID, filename = '') {

    var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var tableSelect = document.getElementById(tableID);
    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

    //Specify the file name
    filename = filename ? filename + '.xls' : 'excel_data.xls';

    //Create a download link element
    downloadLink = document.createElement("a");
    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
        var blob = new Blob(['\ufeff', tableHTML], {
            type: dataType
        });

        navigator.msSaveOrOpenBlob(blob, filename);

    } else {

        //Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        //Setting the file name
        downloadLink.download = filename;

        //Trigger the function
        downloadLink.click();
    }
}