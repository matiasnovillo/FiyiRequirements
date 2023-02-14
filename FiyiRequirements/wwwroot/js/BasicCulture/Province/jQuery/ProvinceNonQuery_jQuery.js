

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

//Last modification on: 14/02/2023 12:40:12

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
    xmlHttpRequest.open("POST", "/api/BasicCulture/Province/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});