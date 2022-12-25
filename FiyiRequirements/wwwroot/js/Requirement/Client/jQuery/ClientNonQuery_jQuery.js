

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2022
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
*/

//Stack: 10

//Last modification on: 25/12/2022 12:12:08

$(document).ready(function () {

});

//Used for Quill Editor


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#requirement-client-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("requirement-client-title-page", $("#requirement-client-title-page").html());
    formData.append("requirement-client-clientid-input", $("#requirement-client-clientid-input").val());

    formData.append("requirement-client-firstname-input", $("#requirement-client-firstname-input").val());
    formData.append("requirement-client-lastname-input", $("#requirement-client-lastname-input").val());
    formData.append("requirement-client-businessname-input", $("#requirement-client-businessname-input").val());
    formData.append("requirement-client-phonenumber-input", $("#requirement-client-phonenumber-input").val());
    formData.append("requirement-client-email-input", $("#requirement-client-email-input").val());
    

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
        $("#requirement-client-insert-or-update-message").addClass("btn-secondary");
        $("#requirement-client-insert-or-update-message").removeClass("btn-success");
        $("#requirement-client-insert-or-update-message").removeClass("btn-error");
        $("#requirement-client-insert-or-update-message").removeAttr("data-toggle");
        $("#requirement-client-insert-or-update-message").removeAttr("data-target");
        $("#requirement-client-insert-or-update-message").html(`Sending data. Please, wait`);
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
            $("#requirement-client-insert-or-update-message").addClass("btn-danger");
            $("#requirement-client-insert-or-update-message").removeClass("btn-success");
            $("#requirement-client-insert-or-update-message").removeClass("btn-secondary");
            $("#requirement-client-insert-or-update-message").attr("data-toggle", "modal");
            $("#requirement-client-insert-or-update-message").attr("data-target", "#requirement-client-error-message-modal");
            $("#requirement-client-insert-or-update-message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                                                There was an error while sending the data`);
            $("#requirement-client-error-message-title").html("There was an error while sending the data");
            $("#requirement-client-error-message-text").html(xmlHttpRequest.response);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            //Show success button
            $("#requirement-client-insert-or-update-message").addClass("btn-success");
            $("#requirement-client-insert-or-update-message").removeClass("btn-error");
            $("#requirement-client-insert-or-update-message").removeClass("btn-secondary");
            $("#requirement-client-insert-or-update-message").removeAttr("data-toggle");
            $("#requirement-client-insert-or-update-message").removeAttr("data-target");
            $("#requirement-client-insert-or-update-message").html(`<i class="fas fa-check"></i>
                                                                Data sent successfully`);
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/Requirement/Client/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});