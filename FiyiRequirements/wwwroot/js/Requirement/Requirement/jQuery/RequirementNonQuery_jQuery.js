

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

//Last modification on: 27/12/2022 20:52:58

//Used for Quill Editor
let requirementrequirementbodytoolbaroptions = [
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
let requirementrequirementbodyquill = new Quill("#requirement-requirement-body-input", {
    modules: {
        toolbar: requirementrequirementbodytoolbaroptions
    },
    theme: "snow"
});

//LOAD EVENT
$(document).ready(function () {
    requirementrequirementbodyquill.root.innerHTML = $("#requirement-requirement-body-hidden-value").val();

    //RequirementState select tag
    $("#requirement-requirement-requirementstateid-select").on("change", function (e) {
        $("#requirement-requirement-requirementstateid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#requirement-requirement-requirementstateid-select option:selected").text()}
            </a>
            <input type="hidden" id="requirement-requirement-requirementstateid-input" value="${$("#requirement-requirement-requirementstateid-select option:selected").val()}"/>
        </li>`);
    });

    //RequirementPriority select tag
    $("#requirement-requirement-requirementpriorityid-select").on("change", function (e) {
        $("#requirement-requirement-requirementpriorityid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#requirement-requirement-requirementpriorityid-select option:selected").text()}
            </a>
            <input type="hidden" id="requirement-requirement-requirementpriorityid-input" value="${$("#requirement-requirement-requirementpriorityid-select option:selected").val()}"/>
        </li>`);
    });

    //UserProgrammer select tag
    $("#requirement-requirement-useremployeeid-select").on("change", function (e) {
        $("#requirement-requirement-useremployeeid-list").html(`<li class="nav-item">
            <a class="nav-link mb-sm-3 mb-md-0 active" id="tabs-text-1-tab" data-toggle="tab" href="javascript:void(0)" role="tab" aria-controls="" aria-selected="true">
                ${$("#requirement-requirement-useremployeeid-select option:selected").text()}
            </a>
            <input type="hidden" id="requirement-requirement-useremployeeid-input" value="${$("#requirement-requirement-useremployeeid-select option:selected").val()}"/>
        </li>`);

    });

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName("needs-validation");
    // Loop over them and prevent submission
    Array.prototype.filter.call(forms, function (form) {
        form.addEventListener("submit", function (event) {

            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === true) {

                //Create a formdata object
                var formData = new FormData();

                //RequirementId
                formData.append("requirement-requirement-requirementid-input", $("#requirement-requirement-requirementid-input").val());

                formData.append("requirement-requirement-title-input", $("#requirement-requirement-title-input").val());
                formData.append("requirement-requirement-body-input", requirementrequirementbodyquill.root.innerHTML);
                formData.append("requirement-requirement-requirementstateid-input", $("#requirement-requirement-requirementstateid-input").val());
                formData.append("requirement-requirement-requirementpriorityid-input", $("#requirement-requirement-requirementpriorityid-input").val());
                formData.append("requirement-requirement-useremployeeid-input", $("#requirement-requirement-useremployeeid-input").val());


                //Setup request
                var xmlHttpRequest = new XMLHttpRequest();
                //Set event listeners
                xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
                    //SAVING
                    $.notify({ message: "Saving data. Please, wait" }, { type: "info", placement: { from: "bottom", align: "center" } });
                });
                xmlHttpRequest.onload = function () {
                    if (xmlHttpRequest.status >= 400) {
                        //ERROR
                        console.log(xmlHttpRequest);
                        $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while saving the data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                    }
                    else {
                        //SUCCESS
                        $.notify({ icon: "fas fa-check", message: "Data sent successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });

                        //Create a formdata object
                        var changehistoryformData = new FormData();
                        //Stop stuff happening
                        e.stopPropagation();
                        e.preventDefault();

                        //RequirementChangehistoryId
                        changehistoryformData.append("requirement-requirementchangehistory-requirementchangehistoryid-input", $("#requirement-requirementchangehistory-requirementchangehistoryid-input").val());

                        changehistoryformData.append("requirement-requirementchangehistory-requirementid-input", $("#requirement-requirement-requirementid-input").val());
                        changehistoryformData.append("requirement-requirementchangehistory-requirementstateid-input", $("#requirement-requirement-requirementstateid-input").val());
                        changehistoryformData.append("requirement-requirementchangehistory-requirementpriorityid-input", $("#requirement-requirement-requirementpriorityid-input").val());


                        //Setup request
                        var changehistoryxmlHttpRequest = new XMLHttpRequest();
                        //Set event listeners
                        changehistoryxmlHttpRequest.upload.addEventListener("loadstart", function (e) {
                            //SAVING
                            $.notify({ message: "Saving data. Please, wait" }, { type: "info", placement: { from: "bottom", align: "center" } });
                        });
                        changehistoryxmlHttpRequest.onload = function () {
                            if (xmlHttpRequest.status >= 400) {
                                //ERROR
                                console.log(xmlHttpRequest);
                                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while saving the data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                            }
                            else {
                                //SUCCESS
                                $.notify({ icon: "fas fa-check", message: "Data sent successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });
                            }
                        };
                        //Open connection
                        changehistoryxmlHttpRequest.open("POST", "/api/Requirement/RequirementChangehistory/1/InsertOrUpdateAsync", true);
                        //Send request
                        changehistoryxmlHttpRequest.send(changehistoryformData);
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/Requirement/Requirement/1/InsertOrUpdateAsync", true);
                //Send request
                xmlHttpRequest.send(formData);

            }
            else {
                $.notify({ message: "Please, complete all fields." }, { type: "warning", placement: { from: "bottom", align: "center" } });
            }


            form.classList.add("was-validated");
        }, false);
    });

});