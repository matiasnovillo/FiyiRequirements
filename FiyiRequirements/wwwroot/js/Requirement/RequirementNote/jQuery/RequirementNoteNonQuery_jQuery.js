

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

//Last modification on: 28/12/2022 17:28:12

//Used for Quill Editor
let requirementrequirementnotebodytoolbaroptions = [
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
let requirementrequirementnotebodyquill = new Quill("#requirement-requirementnote-body-input", {
    modules: {
        toolbar: requirementrequirementnotebodytoolbaroptions
    },
    theme: "snow"
});

//LOAD EVENT
$(document).ready(function () {

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName("needs-validation-requirementnote");
    // Loop over them and prevent submission
    Array.prototype.filter.call(forms, function (form) {
        form.addEventListener("submit", function (event) {

            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === true) {

                //Create a formdata object
                var formData = new FormData();

                //RequirementNoteId
                formData.append("requirement-requirementnote-requirementnoteid-input", $("#requirement-requirementnote-requirementnoteid-input").val());

                formData.append("requirement-requirementnote-title-input", $("#requirement-requirementnote-title-input").val());
                formData.append("requirement-requirementnote-body-input", requirementrequirementnotebodyquill.root.innerHTML);
                formData.append("requirement-requirement-requirementid-input", $("#requirement-requirement-requirementid-input").val());


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
                        window.location.replace("/Requirement/RequirementNoteQueryPage");
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/Requirement/RequirementNote/1/InsertOrUpdateAsync", true);
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