

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

//Last modification on: 21/12/2022 9:25:46

$(document).ready(function () {
});

//Used for Quill Editor


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#basiccore-failure-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("basiccore-failure-title-page", $("#basiccore-failure-title-page").html());
    formData.append("basiccore-failure-failureid-input", $("#basiccore-failure-failureid-input").val());

    formData.append("basiccore-failure-httpcode-input", $("#basiccore-failure-httpcode-input").val());
    formData.append("basiccore-failure-emergencylevel-input", $("#basiccore-failure-emergencylevel-input").val());
    formData.append("basiccore-failure-message-input", $("#basiccore-failure-message-input").val());
    formData.append("basiccore-failure-stacktrace-input", $("#basiccore-failure-stacktrace-input").val());
    formData.append("basiccore-failure-source-input", $("#basiccore-failure-source-input").val());
    formData.append("basiccore-failure-comment-input", $("#basiccore-failure-comment-input").val());
    

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //SAVING
        $.notify({message: "Saving data. Please, wait"}, {type: "info", placement: { from: "bottom", align: "center" }});
    });
    xmlHttpRequest.onload = function () {
        if (xmlHttpRequest.status >= 400) {
            //ERROR
            console.log(xmlHttpRequest);
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while saving the data" }, { type: "danger", placement: { from: "bottom", align: "center"}});
        }
        else {
            //SUCCESS
            $.notify({ icon: "fas fa-check", message: "Data sent successfully"}, { type: "success", placement: { from: "bottom", align: "center"}});
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/BasicCore/Failure/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});