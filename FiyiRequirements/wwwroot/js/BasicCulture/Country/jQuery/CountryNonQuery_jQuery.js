

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

//Last modification on: 14/02/2023 11:58:40

$(document).ready(function () {
    //PlanetId select tag
    $("#basicculture-country-planetid-select").on("change", function (e) {
        $("#basicculture-country-planetid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#basicculture-country-planetid-select option:selected").text()}
            </a>
            <input type="hidden" id="basicculture-country-planetid-input" value="${$("#basicculture-country-planetid-select option:selected").val()}"/>
        </li>`);
    });
});

//Used for Quill Editor


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#basicculture-country-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("basicculture-country-title-page", $("#basicculture-country-title-page").html());
    formData.append("basicculture-country-countryid-input", $("#basicculture-country-countryid-input").val());

    formData.append("basicculture-country-name-input", $("#basicculture-country-name-input").val());
    formData.append("basicculture-country-geographicalcoordinates-input", $("#basicculture-country-geographicalcoordinates-input").val());
    formData.append("basicculture-country-code-input", $("#basicculture-country-code-input").val());
    formData.append("basicculture-country-planetid-input", $("#basicculture-country-planetid-input").val());
    

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
    xmlHttpRequest.open("POST", "/api/BasicCulture/Country/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});