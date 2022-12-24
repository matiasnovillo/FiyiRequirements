$(document).ready(function () {
    $("#message").hide();
});

//Create a formdata object
var formData = new FormData();

$("#update-password").on("click", function (e) {
    if ($("#actual-password").val() == "" || $("#new-password").val() == "" || $("#confirm-password").val() == "" ) {
        $("#message").addClass("btn-danger");
        $("#message").removeClass("btn-white");
        $("#message").removeClass("btn-success");
        $("#message").show();
        $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                    Complete all fields`);

        return;
    }

    if ($("#new-password").val() != $("#confirm-password").val()) {
        $("#message").addClass("btn-danger");
        $("#message").removeClass("btn-white");
        $("#message").removeClass("btn-success");
        $("#message").show();
        $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                    New password and confirm password must be equal`);

        return;
    }

    $("#message").show();

    formData.append("actual-password", $("#actual-password").val());
    formData.append("new-password", $("#new-password").val());

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        $("#message").addClass("btn-white");
        $("#message").removeClass("btn-danger");
        $("#message").removeClass("btn-success");
        $("#message").html(`Changing password...`);
    });
    xmlHttpRequest.upload.addEventListener("progress", function (e) {
        // While sending and loading data.
    });
    xmlHttpRequest.upload.addEventListener("load", function (e) {
        // When the request has successfully completed.
    });
    xmlHttpRequest.upload.addEventListener("loadend", function (e) {
        // When the request has completed (either in success or failure).
    });
    xmlHttpRequest.upload.addEventListener("error", function (e) {
        // When the request has failed.
    });
    xmlHttpRequest.upload.addEventListener("abort", function (e) {
        // When the request has been aborted. 
    });
    xmlHttpRequest.upload.addEventListener("timeout", function (e) {
        // When the author specified timeout has passed before the request could complete
    });
    xmlHttpRequest.onload = function () {
        if (xmlHttpRequest.status >= 400) {
            //Show error button
            $("#message").addClass("btn-danger");
            $("#message").removeClass("btn-white");
            $("#message").removeClass("btn-success");
            $("#message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                    There was an error while trying to change password`);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            if (xmlHttpRequest.response == "Password changed") {
                //Show success button
                $("#message").addClass("btn-success");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-danger");
                $("#message").html(`<i class="fas fa-check"></i>
                                Password changed`);
            }
            else {
                //Show danger button
                $("#message").addClass("btn-danger");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-success");
                $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                The actual password is wrong`);
            }
            
        }
    };
    //Open connection
    xmlHttpRequest.open("PUT", "/api/CMSCore/User/1/ChangePassword", false);
    //Send request
    xmlHttpRequest.send(formData);
});