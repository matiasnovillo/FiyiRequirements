$(document).ready(function () {
    $("#message").hide();
});

//Create a formdata object
var formData = new FormData();

$("#send-email").on("click", function (e) {
    $("#message").show();

    if ($("#email").val() == "") {
        $("#message").addClass("btn-danger");
        $("#message").removeClass("btn-white");
        $("#message").removeClass("btn-success");
        $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                    Please, put your email address`);

        return;
    }

    formData.append("email", $("#email").val());

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        $("#message").addClass("btn-white");
        $("#message").removeClass("btn-danger");
        $("#message").removeClass("btn-success");
        $("#message").html(`Sending recovery email...`);
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
                                    There was an error while trying to sending recovery email`);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            if (xmlHttpRequest.response == "Recovery email sent") {
                //Show success button
                $("#message").addClass("btn-success");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-danger");
                $("#message").html(`<i class="fas fa-check"></i>
                                Recovery email sent. Please, check your inbox to recover the password`);
            }
            else {
                //Show error button
                $("#message").addClass("btn-danger");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-success");
                $("#message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                    The email doesn't exist`);
            }
        }
    };
    //Open connection
    xmlHttpRequest.open("PUT", "/api/CMSCore/User/1/RecoverPassword", false);
    //Send request
    xmlHttpRequest.send(formData);
});