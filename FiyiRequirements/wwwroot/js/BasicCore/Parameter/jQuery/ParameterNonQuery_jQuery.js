

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

//Last modification on: 21/12/2022 9:32:45

$(document).ready(function () {
basiccoreparametervaluequill.root.innerHTML = $("#basiccore-parameter-value-hidden-value").val();
    
});

//Used for Quill Editor
let basiccoreparametervaluetoolbaroptions = [
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
let basiccoreparametervaluequill = new Quill("#basiccore-parameter-value-input", {
    modules: {
        toolbar: basiccoreparametervaluetoolbaroptions
    },
    theme: "snow"
});


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#basiccore-parameter-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("basiccore-parameter-title-page", $("#basiccore-parameter-title-page").html());
    formData.append("basiccore-parameter-parameterid-input", $("#basiccore-parameter-parameterid-input").val());

    formData.append("basiccore-parameter-name-input", $("#basiccore-parameter-name-input").val());
    formData.append("basiccore-parameter-value-input", basiccoreparametervaluequill.root.innerHTML);
    formData.append("basiccore-parameter-isprivate-input", $("#basiccore-parameter-isprivate-input").is(":checked"));


    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
        $("#basiccore-parameter-insert-or-update-message").addClass("btn-secondary");
        $("#basiccore-parameter-insert-or-update-message").removeClass("btn-success");
        $("#basiccore-parameter-insert-or-update-message").removeClass("btn-error");
        $("#basiccore-parameter-insert-or-update-message").removeAttr("data-toggle");
        $("#basiccore-parameter-insert-or-update-message").removeAttr("data-target");
        $("#basiccore-parameter-insert-or-update-message").html(`Sending data. Please, wait`);
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
            $("#basiccore-parameter-insert-or-update-message").addClass("btn-danger");
            $("#basiccore-parameter-insert-or-update-message").removeClass("btn-success");
            $("#basiccore-parameter-insert-or-update-message").removeClass("btn-secondary");
            $("#basiccore-parameter-insert-or-update-message").attr("data-toggle", "modal");
            $("#basiccore-parameter-insert-or-update-message").attr("data-target", "#basiccore-parameter-error-message-modal");
            $("#basiccore-parameter-insert-or-update-message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                                                There was an error while sending the data`);
            $("#basiccore-parameter-error-message-title").html("There was an error while sending the data");
            $("#basiccore-parameter-error-message-text").html(xmlHttpRequest.response);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            //Show success button
            $("#basiccore-parameter-insert-or-update-message").addClass("btn-success");
            $("#basiccore-parameter-insert-or-update-message").removeClass("btn-error");
            $("#basiccore-parameter-insert-or-update-message").removeClass("btn-secondary");
            $("#basiccore-parameter-insert-or-update-message").removeAttr("data-toggle");
            $("#basiccore-parameter-insert-or-update-message").removeAttr("data-target");
            $("#basiccore-parameter-insert-or-update-message").html(`<i class="fas fa-check"></i>
                                                                Data sent successfully`);
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/BasicCore/Parameter/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});