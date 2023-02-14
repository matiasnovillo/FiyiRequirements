

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

//Last modification on: 14/02/2023 11:17:47

$(document).ready(function () {
    $("#basicculture-city-provinceid-select").on("change", function (e) {
        $("#basicculture-city-provinceid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#basicculture-city-provinceid-select option:selected").text()}
            </a>
            <input type="hidden" id="basicculture-city-provinceid-input" value="${$("#basicculture-city-provinceid-select option:selected").val()}"/>
        </li>`);
    });
});

//Used for Quill Editor


//Used for file input


//Create a formdata object
var formData = new FormData();
$("#basicculture-city-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("basicculture-city-title-page", $("#basicculture-city-title-page").html());
    formData.append("basicculture-city-cityid-input", $("#basicculture-city-cityid-input").val());

    formData.append("basicculture-city-name-input", $("#basicculture-city-name-input").val());
    formData.append("basicculture-city-geographicalcoordinates-input", $("#basicculture-city-geographicalcoordinates-input").val());
    formData.append("basicculture-city-code-input", $("#basicculture-city-code-input").val());
    formData.append("basicculture-city-provinceid-input", $("#basicculture-city-provinceid-input").val());
    

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
    xmlHttpRequest.open("POST", "/api/BasicCulture/City/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});