

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

//Last modification on: 21/12/2022 10:58:18

$(document).ready(function () {

});

//Used for Quill Editor


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#cmscore-menu-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("cmscore-menu-title-page", $("#cmscore-menu-title-page").html());
    formData.append("cmscore-menu-menuid-input", $("#cmscore-menu-menuid-input").val());

    formData.append("cmscore-menu-name-input", $("#cmscore-menu-name-input").val());
    formData.append("cmscore-menu-menufatherid-input", $("#cmscore-menu-menufatherid-input").val());
    formData.append("cmscore-menu-order-input", $("#cmscore-menu-order-input").val());
    formData.append("cmscore-menu-urlpath-input", $("#cmscore-menu-urlpath-input").val());
    formData.append("cmscore-menu-iconurlpath-input", $("#cmscore-menu-iconurlpath-input").val());
    

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
        $("#cmscore-menu-insert-or-update-message").addClass("btn-secondary");
        $("#cmscore-menu-insert-or-update-message").removeClass("btn-success");
        $("#cmscore-menu-insert-or-update-message").removeClass("btn-error");
        $("#cmscore-menu-insert-or-update-message").removeAttr("data-toggle");
        $("#cmscore-menu-insert-or-update-message").removeAttr("data-target");
        $("#cmscore-menu-insert-or-update-message").html(`Sending data. Please, wait`);
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
            $("#cmscore-menu-insert-or-update-message").addClass("btn-danger");
            $("#cmscore-menu-insert-or-update-message").removeClass("btn-success");
            $("#cmscore-menu-insert-or-update-message").removeClass("btn-secondary");
            $("#cmscore-menu-insert-or-update-message").attr("data-toggle", "modal");
            $("#cmscore-menu-insert-or-update-message").attr("data-target", "#cmscore-menu-error-message-modal");
            $("#cmscore-menu-insert-or-update-message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                                                There was an error while sending the data`);
            $("#cmscore-menu-error-message-title").html("There was an error while sending the data");
            $("#cmscore-menu-error-message-text").html(xmlHttpRequest.response);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            //Show success button
            $("#cmscore-menu-insert-or-update-message").addClass("btn-success");
            $("#cmscore-menu-insert-or-update-message").removeClass("btn-error");
            $("#cmscore-menu-insert-or-update-message").removeClass("btn-secondary");
            $("#cmscore-menu-insert-or-update-message").removeAttr("data-toggle");
            $("#cmscore-menu-insert-or-update-message").removeAttr("data-target");
            $("#cmscore-menu-insert-or-update-message").html(`<i class="fas fa-check"></i>
                                                                Data sent successfully`);
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/CMSCore/Menu/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});