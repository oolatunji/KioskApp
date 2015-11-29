$(document).ready(function () {
    try{
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
            $('#pickUpBranch').html('<option>Loading Pick Up Branches...</option>');
            $('#pickUpBranch').prop('disabled', 'disabled');
            $.ajax({
                url: settingsManager.websiteURL + 'api/BranchAPI/RetrieveBranches',
                type: 'GET',
                async: true,
                cache: false,
                success: function (response) {
                    $('#pickUpBranch').html('');
                    $('#pickUpBranch').prop('disabled', false);
                    $('#pickUpBranch').append('<option value="">Select Branch</option>');
                    var functions = response.data;
                    var html = '';
                    $.each(functions, function (key, value) {
                        $('#pickUpBranch').append('<option value="' + value.ID + '">' + value.Name + '</option>');
                    });
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText);
                }
            });
            displayMessage("success", 'A token has been sent to your email. Kindly enter the token to continue with your card request.');
        } else {
            window.location = '../Customer/CustomerCardRequest';
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
    }
});

function continueRequestCard() {
    try{
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
                    var branch = $('#pickUpBranch').val();
                    if (_.isEmpty(branch)) {
                        displayMessage("error", "Error encountered: Kindy select the branch where you will like to pick up your card.");
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

                        var data = { CustomerID: customerID, CardTypeID: cardType, PickupBranchID: branch, RequestTypeID: requestType, SerialNumber: serialNumber };
                        $.ajax({
                            url: settingsManager.websiteURL + 'api/CustomerAPI/SaveCustomerCardRequest',
                            type: 'POST',
                            data: data,
                            processData: true,
                            async: true,
                            cache: false,
                            success: function (response) {
                                displayMessage("success", 'Your card request has been received. You will be informed as soon as your card is ready for pick up.');

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
            }
        } else {
            window.location = '../Customer/CustomerCardRequest';
        }
    }catch(err){
        displayMessage("error", "Error encountered: " + err);
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('<i class="fa fa-cog"></i> Request');
    }
}