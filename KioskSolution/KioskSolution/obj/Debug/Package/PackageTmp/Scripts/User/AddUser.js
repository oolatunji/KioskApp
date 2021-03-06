﻿$(document).ready(function () {
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
        else {
            $('#userRole').html('<option>Loading Roles...</option>');
            $('#userBranch').html('<option>Loading Branches...</option>');
            $('#userRole').prop('disabled', 'disabled');
            $('#userBranch').prop('disabled', 'disabled');

            //Get Roles
            $.ajax({
                url: settingsManager.websiteURL + 'api/RoleAPI/RetrieveRoles',
                type: 'GET',
                async: true,
                cache: false,
                success: function (response) {
                    $('#userRole').html('');
                    $('#userRole').prop('disabled', false);
                    $('#userRole').append('<option value="">Select Role</option>');
                    var roles = response.data;
                    var html = '';
                    $.each(roles, function (key, value) {
                        $('#userRole').append('<option value="' + value.ID + '">' + value.Name + '</option>');
                    });
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText);
                }
            });

            //Get Branches
            $.ajax({
                url: settingsManager.websiteURL + 'api/BranchAPI/RetrieveBranches',
                type: 'GET',
                async: true,
                cache: false,
                success: function (response) {
                    $('#userBranch').html('');
                    $('#userBranch').prop('disabled', false);
                    $('#userBranch').append('<option value="">Select Branch</option>');
                    var functions = response.data;
                    var html = '';
                    $.each(functions, function (key, value) {
                        $('#userBranch').append('<option value="' + value.ID + '">' + value.Name + '</option>');
                    });
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText);
                }
            });
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
    }
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function addUser() {
    try {

        var err = genericFormValidation();
        if (_.isEmpty(err)) {

            $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i> Adding...');
            $("#addBtn").attr("disabled", "disabled");

            var lastname = $('#lastname').val();
            var othernames = $('#othernames').val();
            var gender = $('#gender').val();
            var phoneNumber = $('#phoneNumber').val();
            var email = $('#emailAddress').val();
            var username = $('#username').val();
            var userRole = $('#userRole').val();
            var userBranch = $('#userBranch').val();


            var data = { Lastname: lastname, Othernames: othernames, Gender: gender, PhoneNumber: phoneNumber, Email: email, Username: username, UserRole: userRole, UserBranch: userBranch };
            $.ajax({
                url: settingsManager.websiteURL + 'api/UserAPI/SaveUser',
                type: 'POST',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "User Management");
                    $('#lastname').val('');
                    $('#othernames').val('');
                    $('#gender').val('');
                    $('#phoneNumber').val('');
                    $('#emailAddress').val('');
                    $('#username').val('');
                    $('#userRole').val('');
                    $('#userBranch').val('');

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