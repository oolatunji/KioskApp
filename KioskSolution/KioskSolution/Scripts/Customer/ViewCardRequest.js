$(document).ready(function () {
    try {
        //var currentUrl = window.location.href;
        //var user = JSON.parse(window.sessionStorage.getItem("loggedInUser"));
        //var userFunctions = user.Function;

        var exist = true;
        //$.each(userFunctions, function (key, userfunction) {
        //    var link = settingsManager.websiteURL.trimRight('/') + userfunction.PageLink;
        //    if (currentUrl == link) {
        //        exist = true;
        //    }
        //});

        if (!exist)
            window.location.href = '../System/UnAuthorized';
        else
            getCardRequests();
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
    }
});


String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function getCardRequests() {

    $('#example tfoot th').each(function () {
        var title = $('#example thead th').eq($(this).index()).text();
        if (title != "")
            $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    if ($.fn.DataTable.isDataTable('#example')) {

        var table = $('#example').DataTable();
        table.ajax.url(settingsManager.websiteURL + 'api/CustomerAPI/RetrieveCustomers').load();

    } else {

        var table = $('#example').DataTable({

            "processing": true,

            "ajax": settingsManager.websiteURL + 'api/CustomerAPI/RetrieveCardRequests',

            "columns": [
                {
                    "className": 'edit-control',
                    "orderable": false,
                    "data": null,
                    "defaultContent": ''
                },
                { "data": "CustomerName" },
                { "data": "AccountNumber" },
                { "data": "Pan" },
                { "data": "PickupBranch" },
                { "data": "CardType" },
                { "data": "RequestType" },
                { "data": "RequestDate" },
                { "data": "SerialNumber" },
                {
                    "data": "RequestStatus",
                    "visible": false
                },
                {
                    "data": "ID",
                    "visible": false
                },
            ],

            "order": [[1, "asc"]],

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

        $('#example tbody').on('click', 'td.edit-control', function () {
            var tr = $(this).closest('tr');
            var row = table.row(tr);

            function closeAll() {
                var e = $('#example tbody tr.shown');
                var rows = table.row(e);
                if (tr != e) {
                    e.removeClass('shown');
                    rows.child.hide();
                }
            }

            if (row.child.isShown()) {
                closeAll();
            }
            else {
                closeAll();

                row.child(format(row.data())).show();
                tr.addClass('shown');
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

function format(d) {
    // `d` is the original data object for the row
    var table = '';
    table += '<form><table width="100%" class="cell-border" cellpadding="5" cellspacing="0" border="2" style="padding-left:50px;">';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Status:</td>';
    table += '<td>"' + d.RequestStatus + '"</td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="display:none">ID:</td>';
    table += '<td style="display:none"><input class="form-control" id="id" value="' + d.ID + '"/></td>';
    table += '</tr>';
    if (_.isEqual(d.RequestStatus, 'Requested')) {
        table += '<tr>';
        table += '<td style="color:navy;width:20%;font-family:Calibri;"></td>';
        table += '<td><button type="button" id="updateBtn" class="btn btn-primary" style="float:right;" onclick="update();"><i class="fa fa-cog"></i> Approve</button></td>';
        table += '</tr>';
    }
    table += '</table></form>';

    return table;
}

function update() {

    try {
        var err = genericFormValidation();
        if (_.isEmpty(err)) {
            $('#updateBtn').html('<i class="fa fa-spinner fa-spin"></i> Approving...');
            $("#updateBtn").attr("disabled", "disabled");

            var lastname = $('#lastname').val();
            var othernames = $('#othernames').val();
            var phoneNumber = $('#phoneNumber').val();
            var emailAddress = $('#emailAddress').val();
            var id = $('#id').val();

            var data = { Lastname: lastname, Othernames: othernames, PhoneNumber: phoneNumber, EmailAddress: emailAddress, ID: id };
            $.ajax({
                url: settingsManager.websiteURL + 'api/CustomerAPI/UpdateCustomer',
                type: 'PUT',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response);
                    getCustomers();
                    $("#updateBtn").removeAttr("disabled");
                    $('#updateBtn').html('<i class="fa fa-cog"></i> Approve');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText);
                    $("#updateBtn").removeAttr("disabled");
                    $('#updateBtn').html('<i class="fa fa-cog"></i> Approve');
                }
            });
        } else {
            displayMessage("error", 'Error experienced: ' + err);
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
        $("#updateBtn").removeAttr("disabled");
        $('#updateBtn').html('<i class="fa fa-cog"></i> Update');
    }
}
