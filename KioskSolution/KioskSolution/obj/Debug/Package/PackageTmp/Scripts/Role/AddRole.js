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
        else {
            $('#dynamicfunctions').html('<p style="font-family:Calibri;color:red;"><i class="fa fa-cog fa-spin"></i> Loading functions...</p>');
            $.ajax({
                url: settingsManager.websiteURL + 'api/FunctionAPI/RetrieveFunctions',
                type: 'GET',
                async: true,
                cache: false,
                success: function (response) {
                    var functions = response.data;
                    var html = '';
                    $.each(functions, function (key, value) {
                        html += '<input type="checkbox" name="functions" value="' + value.ID + '"/>' + value.Name + '<br/>';
                    });
                    $('#dynamicfunctions').html('');
                    $('#dynamicfunctions').append(html);
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

function addRole() {
    try {
        var err = genericFormValidation();
        if (_.isEmpty(err)) {
            var roleName = $('#roleName').val();
            var roleFunctions = [];
            $("input:checkbox[name=functions]:checked").each(function () {
                var roleFunction = {};
                var _function = $(this).val();
                roleFunction = { FunctionID: _function };
                roleFunctions.push(roleFunction);
            });
            if (_.size(roleFunctions) > 0) {
                $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i> Adding...');
                $("#addBtn").attr("disabled", "disabled");

                var data = { Name: roleName, RoleFunctions: roleFunctions };
                $.ajax({
                    url: settingsManager.websiteURL + 'api/RoleAPI/SaveRole',
                    type: 'POST',
                    data: data,
                    processData: true,
                    async: true,
                    cache: false,
                    success: function (response) {
                        displayMessage("success", response, "Roles Management");
                        $('#roleName').val('');
                        $('#dynamicfunctions input[type=checkbox]').removeAttr('checked');
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
                displayMessage("error", 'Error experienced: Role Functions are required.');
            }
        } else {
            displayMessage("error", 'Error experienced: ' + err);
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err);
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('<i class="fa fa-cog"></i> Add');
    }
}