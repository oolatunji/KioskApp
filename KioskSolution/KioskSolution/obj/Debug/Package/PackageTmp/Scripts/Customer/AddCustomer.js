$(document).ready(function () {

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
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function addCustomer() {
    try {
        var err = genericFormValidation();
        if (_.isEmpty(err)) {
            $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i> Adding...');
            $("#addBtn").attr("disabled", "disabled");

            var lastname = $('#lastname').val();
            var othernames = $('#othernames').val();
            var phoneNumber = $('#phoneNumber').val();
            var emailAddress = $('#emailAddress').val();

            var data = { Lastname: lastname, Othernames: othernames, PhoneNumber: phoneNumber, EmailAddress: emailAddress };
            $.ajax({
                url: settingsManager.websiteURL + 'api/CustomerAPI/SaveCustomer',
                type: 'POST',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "Branch Management");
                    $('#lastname').val('');
                    $('#othernames').val('');
                    $('#phoneNumber').val('');
                    $('#emailAddress').val('');
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('<i class="fa fa-cog"></i> Add');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText);
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('<i class="fa fa-cog"></i> Add');
                }
            });
        } else {
            displayMessage("error", 'Error experienced: ' + err);
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('<i class="fa fa-cog"></i> Add');
    }
}