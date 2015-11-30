function requestTypeOnChanged() {
    try {
        var requestType = $('#requestType').val();
        if (!_.isEmpty(requestType)) {
            if (requestType == '1') {
                $('#cardTypeBlock').removeClass('hideControl');
                $('#accountBlock').removeClass('hideControl');
                if ($('#serialNumberBlock').hasClass('hideControl')) {
                    $('#serialNumberBlock').removeClass('hideControl');
                }
            } else if (requestType == '2') {
                $('#cardTypeBlock').removeClass('hideControl');
                $('#accountBlock').removeClass('hideControl');
                if (!$('#serialNumberBlock').hasClass('hideControl')) {
                    $('#serialNumberBlock').addClass('hideControl');
                }
            }
        } else {
            if (!$('#cardTypeBlock').hasClass('hideControl'))
                $('#cardTypeBlock').addClass('hideControl');
            if (!$('#accountBlock').hasClass('hideControl'))
                $('#accountBlock').addClass('hideControl');
            if (!$('#serialNumberBlock').hasClass('hideControl'))
                $('#serialNumberBlock').addClass('hideControl');
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('<i class="fa fa-cog"></i> Request');
    }
}

function requestCard() {
    try{
        var requestType = $('#requestType').val();
        if (_.isEmpty(requestType)) {
            displayMessage("error", "Error encountered: Kindly select your card request type.");
        } else {
            if (requestType == '1') {
                var cardType = $('#cardType').val();
                var accountNumber = $('#accountNumber').val();
                var serialNumber = $('#serialNumber').val();

                if (_.isEmpty(cardType) || _.isEmpty(accountNumber) || _.isEmpty(serialNumber)) {
                    displayMessage("error", "Error encountered: All fields are required.");
                } else {

                    $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i> Requesting...');
                    $("#addBtn").attr("disabled", "disabled");

                    var data = { AccountNumber: accountNumber, SerialNumber: serialNumber };
                    console.log(serialNumber);
                    $.ajax({
                        url: settingsManager.websiteURL + 'api/CustomerAPI/SentTokenToCustomer',
                        type: 'POST',
                        data: data,
                        processData: true,
                        async: true,
                        cache: false,
                        success: function (response) {
                            window.sessionStorage.setItem('requestType', requestType);
                            window.sessionStorage.setItem('cardType', cardType);
                            window.sessionStorage.setItem('accountNumber', accountNumber);
                            window.sessionStorage.setItem('serialNumber', serialNumber);

                            $("#addBtn").removeAttr("disabled");
                            $('#addBtn').html('<i class="fa fa-cog"></i> Request');

                            window.sessionStorage.setItem('customerToken', JSON.stringify(response.data));
                            window.location = '../Customer/ConfirmToken';
                        },
                        error: function (xhr) {
                            displayMessage("error", 'Error experienced: ' + xhr.responseText);
                            $("#addBtn").removeAttr("disabled");
                            $('#addBtn').html('<i class="fa fa-cog"></i> Request');
                        }
                    });
                }
            } else if (requestType == '2') {
                var cardType = $('#cardType').val();
                var accountNumber = $('#accountNumber').val();

                if (_.isEmpty(cardType) || _.isEmpty(accountNumber)) {
                    displayMessage("error", "Error encountered: All fields are required.");
                } else {
                    $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i> Requesting...');
                    $("#addBtn").attr("disabled", "disabled");

                    var data = { AccountNumber: accountNumber };
                    $.ajax({
                        url: settingsManager.websiteURL + 'api/CustomerAPI/SentTokenToCustomer',
                        type: 'POST',
                        data: data,
                        processData: true,
                        async: true,
                        cache: false,
                        success: function (response) {
                            window.sessionStorage.setItem('requestType', requestType);
                            window.sessionStorage.setItem('cardType', cardType);
                            window.sessionStorage.setItem('accountNumber', accountNumber);
                            if (window.sessionStorage.getItem('serialNumber') != null) {
                                window.sessionStorage.removeItem('serialNumber');
                            }

                            $("#addBtn").removeAttr("disabled");
                            $('#addBtn').html('<i class="fa fa-cog"></i> Request');

                            window.sessionStorage.setItem('customerToken', JSON.stringify(response.data));
                            window.location = '../Customer/ConfirmToken';
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
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('<i class="fa fa-cog"></i> Request');
    }
}