$(document).ready(function () {
    $("#message").hide();
});

$("#reset-captcha").on("click", function (e) {
    $("#captcha-image").attr("src", "/api/CMSCore/User/1/GetCaptchaImage");
});

//Create a formdata object
var formData = new FormData();

$("#register-button").on("click", function (e) {
    $("#message").show();

    if ($("#fantasy-name").val() == "" || $("#email").val() == "" || $("#password").val() == "" || $("#confirm-password").val() == "" || $("#captcha-text").val() == "") {
        $("#message").addClass("btn-danger");
        $("#message").removeClass("btn-white");
        $("#message").removeClass("btn-success");
        $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                    Complete all fields`);

        return;
    }

    if ($("#password").val().length < 6 || $("#confirm-password").val().length < 6) {
        $("#message").addClass("btn-danger");
        $("#message").removeClass("btn-white");
        $("#message").removeClass("btn-success");
        $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                    Minimum required for password = 6 characters`);

        return;
    }

    if ($("#password").val() != $("#confirm-password").val()) {
        $("#message").addClass("btn-danger");
        $("#message").removeClass("btn-white");
        $("#message").removeClass("btn-success");
        $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                    New password and confirm password must be equal`);

        return;
    }

    formData.append("fantasy-name", $("#fantasy-name").val());
    formData.append("email", $("#email").val());
    formData.append("password", $("#password").val());
    formData.append("captcha-text", $("#captcha-text").val());

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        $("#message").addClass("btn-white");
        $("#message").removeClass("btn-danger");
        $("#message").removeClass("btn-success");
        $("#message").html(`Registering. Please, wait...`);
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
                                    There was an error while trying to register`);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            if (xmlHttpRequest.response == "Successfully registered user") {
                //Show success button
                $("#message").addClass("btn-success");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-danger");
                $("#message").html(`<i class="fas fa-check"></i>
                                Check your mailbox,
                                I have sent an email to finish the registration`);
            }
            else if (xmlHttpRequest.response == "The email is already registered"){
                //Show danger button
                $("#message").addClass("btn-danger");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-success");
                $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                The email is already registered`);
            }
            else if (xmlHttpRequest.response == "The captcha is invalid"){
                //Show danger button
                $("#message").addClass("btn-danger");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-success");
                $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                The captcha is invalid`);
            }
            else {
                //Show danger button
                $("#message").addClass("btn-danger");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-success");
                $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                The registration was wrong, try again`);
            }

        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/CMSCore/User/1/Register", false);
    //Send request
    xmlHttpRequest.send(formData);
});