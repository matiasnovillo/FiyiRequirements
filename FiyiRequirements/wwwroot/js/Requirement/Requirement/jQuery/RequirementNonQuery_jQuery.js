

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

//Last modification on: 24/12/2022 6:48:02

$(document).ready(function () {
requirementrequirementbodyquill.root.innerHTML = $("#requirement-requirement-body-hidden-value").val();
    
});

//Used for Quill Editor
let requirementrequirementbodytoolbaroptions = [
    ["bold", "italic", "underline", "strike"],        // toggled buttons
    ["link", "blockquote", "code-block"],

    [{ "header": 1 }, { "header": 2 }],               // custom button values
    [{ "list": "ordered" }, { "list": "bullet" }],
    [{ "script": "sub" }, { "script": "super" }],      // superscript/subscript
    [{ "indent": "-1" }, { "indent": "+1" }],          // outdent/indent
    [{ "direction": "rtl" }],                         // text direction
    ["image", "video"],
    ["clean"]                                         // remove formatting button
];
let requirementrequirementbodyquill = new Quill("#requirement-requirement-body-input", {
    modules: {
        toolbar: requirementrequirementbodytoolbaroptions
    },
    theme: "snow"
});


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#requirement-requirement-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("requirement-requirement-title-page", $("#requirement-requirement-title-page").html());
    formData.append("requirement-requirement-requirementid-input", $("#requirement-requirement-requirementid-input").val());

    formData.append("requirement-requirement-clientid-input", $("#requirement-requirement-clientid-input").val());
    formData.append("requirement-requirement-title-input", $("#requirement-requirement-title-input").val());
    formData.append("requirement-requirement-body-input", requirementrequirementbodyquill.root.innerHTML);
    formData.append("requirement-requirement-requirementstateid-input", $("#requirement-requirement-requirementstateid-input").val());
    formData.append("requirement-requirement-requirementtypeid-input", $("#requirement-requirement-requirementtypeid-input").val());
    formData.append("requirement-requirement-requirementpriorityid-input", $("#requirement-requirement-requirementpriorityid-input").val());
    formData.append("requirement-requirement-userprogrammerid-input", $("#requirement-requirement-userprogrammerid-input").val());
    

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
        $("#requirement-requirement-insert-or-update-message").addClass("btn-secondary");
        $("#requirement-requirement-insert-or-update-message").removeClass("btn-success");
        $("#requirement-requirement-insert-or-update-message").removeClass("btn-error");
        $("#requirement-requirement-insert-or-update-message").removeAttr("data-toggle");
        $("#requirement-requirement-insert-or-update-message").removeAttr("data-target");
        $("#requirement-requirement-insert-or-update-message").html(`Sending data. Please, wait`);
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
            $("#requirement-requirement-insert-or-update-message").addClass("btn-danger");
            $("#requirement-requirement-insert-or-update-message").removeClass("btn-success");
            $("#requirement-requirement-insert-or-update-message").removeClass("btn-secondary");
            $("#requirement-requirement-insert-or-update-message").attr("data-toggle", "modal");
            $("#requirement-requirement-insert-or-update-message").attr("data-target", "#requirement-requirement-error-message-modal");
            $("#requirement-requirement-insert-or-update-message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                                                There was an error while sending the data`);
            $("#requirement-requirement-error-message-title").html("There was an error while sending the data");
            $("#requirement-requirement-error-message-text").html(xmlHttpRequest.response);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            //Show success button
            $("#requirement-requirement-insert-or-update-message").addClass("btn-success");
            $("#requirement-requirement-insert-or-update-message").removeClass("btn-error");
            $("#requirement-requirement-insert-or-update-message").removeClass("btn-secondary");
            $("#requirement-requirement-insert-or-update-message").removeAttr("data-toggle");
            $("#requirement-requirement-insert-or-update-message").removeAttr("data-target");
            $("#requirement-requirement-insert-or-update-message").html(`<i class="fas fa-check"></i>
                                                                Data sent successfully`);
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/Requirement/Requirement/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});