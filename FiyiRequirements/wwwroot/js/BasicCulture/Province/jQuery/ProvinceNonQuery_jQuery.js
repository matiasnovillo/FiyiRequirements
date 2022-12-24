

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

//Last modification on: 21/12/2022 10:37:34

$(document).ready(function () {
    $("#basicculture-province-countryid-select").on("change", function (e) {
        $("#basicculture-province-countryid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#basicculture-province-countryid-select option:selected").text()}
            </a>
            <input type="hidden" id="basicculture-province-countryid-input" value="${$("#basicculture-province-countryid-select option:selected").val()}"/>
        </li>`);
    });
});

//Used for Quill Editor


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#basicculture-province-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("basicculture-province-title-page", $("#basicculture-province-title-page").html());
    formData.append("basicculture-province-provinceid-input", $("#basicculture-province-provinceid-input").val());

    formData.append("basicculture-province-name-input", $("#basicculture-province-name-input").val());
    formData.append("basicculture-province-geographicalcoordinates-input", $("#basicculture-province-geographicalcoordinates-input").val());
    formData.append("basicculture-province-code-input", $("#basicculture-province-code-input").val());
    formData.append("basicculture-province-countryid-input", $("#basicculture-province-countryid-input").val());
    

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
        $("#basicculture-province-insert-or-update-message").addClass("btn-secondary");
        $("#basicculture-province-insert-or-update-message").removeClass("btn-success");
        $("#basicculture-province-insert-or-update-message").removeClass("btn-error");
        $("#basicculture-province-insert-or-update-message").removeAttr("data-toggle");
        $("#basicculture-province-insert-or-update-message").removeAttr("data-target");
        $("#basicculture-province-insert-or-update-message").html(`Sending data. Please, wait`);
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
            $("#basicculture-province-insert-or-update-message").addClass("btn-danger");
            $("#basicculture-province-insert-or-update-message").removeClass("btn-success");
            $("#basicculture-province-insert-or-update-message").removeClass("btn-secondary");
            $("#basicculture-province-insert-or-update-message").attr("data-toggle", "modal");
            $("#basicculture-province-insert-or-update-message").attr("data-target", "#basicculture-province-error-message-modal");
            $("#basicculture-province-insert-or-update-message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                                                There was an error while sending the data`);
            $("#basicculture-province-error-message-title").html("There was an error while sending the data");
            $("#basicculture-province-error-message-text").html(xmlHttpRequest.response);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            //Show success button
            $("#basicculture-province-insert-or-update-message").addClass("btn-success");
            $("#basicculture-province-insert-or-update-message").removeClass("btn-error");
            $("#basicculture-province-insert-or-update-message").removeClass("btn-secondary");
            $("#basicculture-province-insert-or-update-message").removeAttr("data-toggle");
            $("#basicculture-province-insert-or-update-message").removeAttr("data-target");
            $("#basicculture-province-insert-or-update-message").html(`<i class="fas fa-check"></i>
                                                                Data sent successfully`);
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/BasicCulture/Province/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});