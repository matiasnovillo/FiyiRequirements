$(document).ready(function () {
    $("#message").hide();
});

//Create a formdata object
var formData = new FormData();

$("#loginbutton").on("click", function (e) {
    formData.append("fantasynameoremail", $("#fantasynameoremail").val());
    formData.append("password", $("#password").val());

    $("#message").show();

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
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
        console.log(xmlHttpRequest);
        if (xmlHttpRequest.status >= 400) {
            //Show error button and error message modal
            $("#message").addClass("btn-danger");
            $("#message").removeClass("btn-white");
            $("#message").removeClass("btn-success");
            $("#message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                    There was an error while trying to login`);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            if (xmlHttpRequest.response == "User not found") {
                //Show danger button
                $("#message").addClass("btn-danger");
                $("#message").removeClass("btn-white");
                $("#message").removeClass("btn-success");
                $("#message").html(`<i class="fas fa-exclamation-triangle"></i>
                                    User not found`);
            }
            else {
                window.location.href = xmlHttpRequest.response;
            }
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/CMSCore/User/1/Login", false);
    //Send request
    xmlHttpRequest.send(formData);
});