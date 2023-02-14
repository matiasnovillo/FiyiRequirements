

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
*/

//Stack: 10

//Last modification on: 14/02/2023 14:44:39

$(document).ready(function () {
    //User select tag
    $("#cmscore-user-roleid-select").on("change", function (e) {
        $("#cmscore-user-roleid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#cmscore-user-roleid-select option:selected").text()}
            </a>
            <input type="hidden" id="cmscore-user-roleid-input" value="${$("#cmscore-user-roleid-select option:selected").val()}"/>
        </li>`);
    });
});

//Used for Quill Editor


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#cmscore-user-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("cmscore-user-title-page", $("#cmscore-user-title-page").html());
    formData.append("cmscore-user-userid-input", $("#cmscore-user-userid-input").val());

    formData.append("cmscore-user-fantasyname-input", $("#cmscore-user-fantasyname-input").val());
    formData.append("cmscore-user-email-input", $("#cmscore-user-email-input").val());
    formData.append("cmscore-user-password-input", $("#cmscore-user-password-input").val());
    formData.append("cmscore-user-roleid-input", $("#cmscore-user-roleid-input").val());
    formData.append("cmscore-user-registrationtoken-input", $("#cmscore-user-registrationtoken-input").val());
    

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
    xmlHttpRequest.open("POST", "/api/CMSCore/User/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});