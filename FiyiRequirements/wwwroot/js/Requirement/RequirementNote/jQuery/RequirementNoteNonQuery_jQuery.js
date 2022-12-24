

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

//Last modification on: 24/12/2022 6:47:58

$(document).ready(function () {
requirementrequirementnotebodyquill.root.innerHTML = $("#requirement-requirementnote-body-hidden-value").val();
    
});

//Used for Quill Editor
let requirementrequirementnotebodytoolbaroptions = [
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
let requirementrequirementnotebodyquill = new Quill("#requirement-requirementnote-body-input", {
    modules: {
        toolbar: requirementrequirementnotebodytoolbaroptions
    },
    theme: "snow"
});


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#requirement-requirementnote-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("requirement-requirementnote-title-page", $("#requirement-requirementnote-title-page").html());
    formData.append("requirement-requirementnote-requirementnoteid-input", $("#requirement-requirementnote-requirementnoteid-input").val());

    formData.append("requirement-requirementnote-title-input", $("#requirement-requirementnote-title-input").val());
    formData.append("requirement-requirementnote-body-input", requirementrequirementnotebodyquill.root.innerHTML);
    

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
        $("#requirement-requirementnote-insert-or-update-message").addClass("btn-secondary");
        $("#requirement-requirementnote-insert-or-update-message").removeClass("btn-success");
        $("#requirement-requirementnote-insert-or-update-message").removeClass("btn-error");
        $("#requirement-requirementnote-insert-or-update-message").removeAttr("data-toggle");
        $("#requirement-requirementnote-insert-or-update-message").removeAttr("data-target");
        $("#requirement-requirementnote-insert-or-update-message").html(`Sending data. Please, wait`);
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
            $("#requirement-requirementnote-insert-or-update-message").addClass("btn-danger");
            $("#requirement-requirementnote-insert-or-update-message").removeClass("btn-success");
            $("#requirement-requirementnote-insert-or-update-message").removeClass("btn-secondary");
            $("#requirement-requirementnote-insert-or-update-message").attr("data-toggle", "modal");
            $("#requirement-requirementnote-insert-or-update-message").attr("data-target", "#requirement-requirementnote-error-message-modal");
            $("#requirement-requirementnote-insert-or-update-message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                                                There was an error while sending the data`);
            $("#requirement-requirementnote-error-message-title").html("There was an error while sending the data");
            $("#requirement-requirementnote-error-message-text").html(xmlHttpRequest.response);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            //Show success button
            $("#requirement-requirementnote-insert-or-update-message").addClass("btn-success");
            $("#requirement-requirementnote-insert-or-update-message").removeClass("btn-error");
            $("#requirement-requirementnote-insert-or-update-message").removeClass("btn-secondary");
            $("#requirement-requirementnote-insert-or-update-message").removeAttr("data-toggle");
            $("#requirement-requirementnote-insert-or-update-message").removeAttr("data-target");
            $("#requirement-requirementnote-insert-or-update-message").html(`<i class="fas fa-check"></i>
                                                                Data sent successfully`);
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/Requirement/RequirementNote/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});