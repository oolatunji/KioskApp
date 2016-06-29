$(document).ready(function () {
    try {
        showPage();
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
    }
});


String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function showPage(){
    try {
        if (window.sessionStorage.getItem('requestType') != null) {
            var requestType = window.sessionStorage.getItem('requestType');
            if (requestType == '1') {
                var cardType = window.sessionStorage.getItem('cardType') == '1' ? 'MasterCard' : 'Verve';
                var accountNumber = window.sessionStorage.getItem('accountNumber');
                var serialNumber = window.sessionStorage.getItem('serialNumber');

                $('#requestType').val('With Serial Number');
                $('#cardType').val(cardType);
                $('#accountNumber').val(accountNumber);
                $('#serialNumber').val(serialNumber);
            } else if (requestType == '2') {
                var cardType = window.sessionStorage.getItem('cardType') == '1' ? 'MasterCard' : 'Verve';
                var accountNumber = window.sessionStorage.getItem('accountNumber');
                $('#serialNumberBlock').addClass('hideControl');

                $('#requestType').val('Without Serial Number');
                $('#cardType').val(cardType);
                $('#accountNumber').val(accountNumber);
            }
            displayMessage("success", "A token has been sent to the customer's email. Kindly request for the token to continue with the card request.");
        } else {
            window.location = '../Customer/CustomerCardRequest';
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
    }
}

function continueRequestCard() {
    try {
        if (window.sessionStorage.getItem("loggedInUser") === null) {
            window.location = '../';
            alert("Your session has expired. Kindly login again.");
        } else {

            var user = JSON.parse(window.sessionStorage.getItem("loggedInUser"));
            var branchID = user.BranchID;
            var username = user.Username;

            if (window.sessionStorage.getItem('customerToken') != null) {
                var token = $('#token').val();
                if (_.isEmpty(token)) {
                    displayMessage("error", "Error encountered: Kindly enter the token sent to your mail.");
                } else {
                    var customerToken = JSON.parse(window.sessionStorage.getItem('customerToken'));
                    var generatedtoken = customerToken.customerToken;
                    if (!_.isEqual(token, generatedtoken)) {
                        displayMessage("error", "Error encountered: Invalid token entered.");
                    } else {
                        var requestType = window.sessionStorage.getItem('requestType');
                        var serialNumber = '';
                        if (requestType == '1')
                            serialNumber = window.sessionStorage.getItem('serialNumber');

                        var cardType = window.sessionStorage.getItem('cardType');
                        var accountNumber = window.sessionStorage.getItem('accountNumber');
                        var customerID = customerToken.customerID;

                        $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i> Requesting...');
                        $("#addBtn").attr("disabled", "disabled");

                        var data = { CustomerID: customerID, CardTypeID: cardType, RequestTypeID: requestType, SerialNumber: serialNumber, PickupBranchID: branchID, Username: username };
                        $.ajax({
                            url: settingsManager.websiteURL + 'api/CustomerAPI/SaveCustomerCardRequest',
                            type: 'POST',
                            data: data,
                            processData: true,
                            async: true,
                            cache: false,
                            success: function (response) {
                                displayMessage("success", 'Card Request was successful and ready to be printed.');

                                window.sessionStorage.removeItem('requestType');
                                window.sessionStorage.removeItem('cardType');
                                window.sessionStorage.removeItem('accountNumber');
                                if (window.sessionStorage.getItem('serialNumber') != null) {
                                    window.sessionStorage.removeItem('serialNumber');
                                }
                                window.sessionStorage.removeItem('customerToken');

                                $("#addBtn").removeAttr("disabled");
                                $('#addBtn').html('<i class="fa fa-cog"></i> Request');
                            },
                            error: function (xhr) {
                                displayMessage("error", 'Error experienced: ' + xhr.responseText);
                                $("#addBtn").removeAttr("disabled");
                                $('#addBtn').html('<i class="fa fa-cog"></i> Request');
                            }
                        });
                    }
                }
            } else {
                window.location = '../Customer/CustomerCardRequest';
            }
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('<i class="fa fa-cog"></i> Request');
    }
}