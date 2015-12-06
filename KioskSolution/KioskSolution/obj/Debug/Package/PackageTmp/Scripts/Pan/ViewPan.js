$(document).ready(function () {
    try {
        var currentUrl = window.location.href;
        var user = JSON.parse(window.sessionStorage.getItem("loggedInUser"));
        var userFunctions = user.Function;

        var exist = false;
        $.each(userFunctions, function (key, userfunction) {
            var link = settingsManager.websiteURL.trimRight('/') + userfunction.PageLink;
            if (currentUrl == link) {
                exist = true;
            }
        });

        if (!exist)
            window.location.href = '../System/UnAuthorized';
        else
            getBranches();
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
    }
});


String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function getBranches() {

    $('#example tfoot th').each(function () {
        var title = $('#example thead th').eq($(this).index()).text();
        if (title != "")
            $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    if ($.fn.DataTable.isDataTable('#example')) {

        var table = $('#example').DataTable();
        table.ajax.url(settingsManager.websiteURL + 'api/ThirdPartyAPI/RetrievePans').load();

    } else {

        var table = $('#example').DataTable({

            "processing": true,

            "ajax": settingsManager.websiteURL + 'api/ThirdPartyAPI/RetrievePans',

            "columns": [
                { "data": "Pan" },
                { "data": "SerialNumber" },
                { "data": "Status" }
            ],

            "order": [[0, "asc"]],

            "sDom": 'T<"clear">lrtip',

            "oTableTools": {
                "sSwfPath": settingsManager.websiteURL + "img/copy_csv_xls_pdf.swf",
                "aButtons": [
                    {
                        "sExtends": "copy",
                        "sButtonText": "Copy to Clipboard",
                        "oSelectorOpts": { filter: 'applied', order: 'current' },
                        "mColumns": "visible"
                    },
                    {
                        "sExtends": "csv",
                        "sButtonText": "Save to CSV",
                        "oSelectorOpts": { filter: 'applied', order: 'current' },
                        "mColumns": "visible"
                    },
                    {
                        "sExtends": "xls",
                        "sButtonText": "Save for Excel",
                        "oSelectorOpts": { filter: 'applied', order: 'current' },
                        "mColumns": "visible"
                    }
                ]
            }
        });

        $("#example tfoot input").on('keyup change', function () {
            table
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });
    }
}

$(document).ready(function () {
    $('#dataTables-example').DataTable({
        responsive: true
    });
});