

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

//Last modification on: 14/02/2023 14:04:59

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
    xmlHttpRequest.open("POST", "/api/CMSCore/Menu/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});