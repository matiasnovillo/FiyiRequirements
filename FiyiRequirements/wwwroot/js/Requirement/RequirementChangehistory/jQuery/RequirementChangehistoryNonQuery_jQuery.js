

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

//Last modification on: 25/12/2022 18:01:44

$(document).ready(function () {

});

//Used for Quill Editor


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#requirement-requirementchangehistory-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("requirement-requirementchangehistory-title-page", $("#requirement-requirementchangehistory-title-page").html());
    formData.append("requirement-requirementchangehistory-requirementchangehistoryid-input", $("#requirement-requirementchangehistory-requirementchangehistoryid-input").val());

    formData.append("requirement-requirementchangehistory-requirementid-input", $("#requirement-requirementchangehistory-requirementid-input").val());
    formData.append("requirement-requirementchangehistory-requirementstateid-input", $("#requirement-requirementchangehistory-requirementstateid-input").val());
    formData.append("requirement-requirementchangehistory-requirementpriorityid-input", $("#requirement-requirementchangehistory-requirementpriorityid-input").val());
    

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
        $("#requirement-requirementchangehistory-insert-or-update-message").addClass("btn-secondary");
        $("#requirement-requirementchangehistory-insert-or-update-message").removeClass("btn-success");
        $("#requirement-requirementchangehistory-insert-or-update-message").removeClass("btn-error");
        $("#requirement-requirementchangehistory-insert-or-update-message").removeAttr("data-toggle");
        $("#requirement-requirementchangehistory-insert-or-update-message").removeAttr("data-target");
        $("#requirement-requirementchangehistory-insert-or-update-message").html(`Sending data. Please, wait`);
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
            $("#requirement-requirementchangehistory-insert-or-update-message").addClass("btn-danger");
            $("#requirement-requirementchangehistory-insert-or-update-message").removeClass("btn-success");
            $("#requirement-requirementchangehistory-insert-or-update-message").removeClass("btn-secondary");
            $("#requirement-requirementchangehistory-insert-or-update-message").attr("data-toggle", "modal");
            $("#requirement-requirementchangehistory-insert-or-update-message").attr("data-target", "#requirement-requirementchangehistory-error-message-modal");
            $("#requirement-requirementchangehistory-insert-or-update-message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                                                There was an error while sending the data`);
            $("#requirement-requirementchangehistory-error-message-title").html("There was an error while sending the data");
            $("#requirement-requirementchangehistory-error-message-text").html(xmlHttpRequest.response);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            //Show success button
            $("#requirement-requirementchangehistory-insert-or-update-message").addClass("btn-success");
            $("#requirement-requirementchangehistory-insert-or-update-message").removeClass("btn-error");
            $("#requirement-requirementchangehistory-insert-or-update-message").removeClass("btn-secondary");
            $("#requirement-requirementchangehistory-insert-or-update-message").removeAttr("data-toggle");
            $("#requirement-requirementchangehistory-insert-or-update-message").removeAttr("data-target");
            $("#requirement-requirementchangehistory-insert-or-update-message").html(`<i class="fas fa-check"></i>
                                                                Data sent successfully`);
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/Requirement/RequirementChangehistory/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});