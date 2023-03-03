

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

//Last modification on: 15/02/2023 16:56:40

//Create a formdata object
var formData = new FormData();

//Used for Quill Editor
let examplesexampletexttexteditortoolbaroptions = [
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
let examplesexampletexttexteditorquill = new Quill("#examples-example-texttexteditor-input", {
    modules: {
        toolbar: examplesexampletexttexteditortoolbaroptions
    },
    theme: "snow"
});


//Used for file input
let examplesexampletextfileinput;
let examplesexampletextfileboolfileadded;
$("#examples-example-textfile-input").on("change", function (e) {
    examplesexampletextfileinput = $(this).get(0).files;
    examplesexampletextfileboolfileadded = true;
    formData.append("examples-example-textfile-input", examplesexampletextfileinput[0], examplesexampletextfileinput[0].name);
});



//LOAD EVENT
$(document).ready(function () {
    examplesexampletexttexteditorquill.root.innerHTML = $("#examples-example-texttexteditor-hidden-value").val();
    
    
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName("needs-validation");
    // Loop over them and prevent submission
    Array.prototype.filter.call(forms, function (form) {
        form.addEventListener("submit", function (event) {

            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === true) {
                
                //ExampleId
                formData.append("examples-example-exampleid-input", $("#examples-example-exampleid-input").val());

                formData.append("examples-example-boolean-input", $("#examples-example-boolean-input").is(":checked"));
                formData.append("examples-example-datetime-input", $("#examples-example-datetime-input").val());
                formData.append("examples-example-decimal-input", $("#examples-example-decimal-input").val());
                formData.append("examples-example-integer-input", $("#examples-example-integer-input").val());
                formData.append("examples-example-textbasic-input", $("#examples-example-textbasic-input").val());
                formData.append("examples-example-textemail-input", $("#examples-example-textemail-input").val());
                if (!examplesexampletextfileboolfileadded) {
                    formData.append("examples-example-textfile-input", $("#examples-example-textfile-readonly").val());
                }
                formData.append("examples-example-textpassword-input", $("#examples-example-textpassword-input").val());
                formData.append("examples-example-textphonenumber-input", $("#examples-example-textphonenumber-input").val());
                formData.append("examples-example-texttag-input", $("#examples-example-texttag-input").val());
                formData.append("examples-example-texttextarea-input", $("#examples-example-texttextarea-input").val());
                formData.append("examples-example-texttexteditor-input", examplesexampletexttexteditorquill.root.innerHTML);
                formData.append("examples-example-texturl-input", $("#examples-example-texturl-input").val());
                formData.append("examples-example-foreignkeydropdown-input", $("#examples-example-foreignkeydropdown-input").val());
                formData.append("examples-example-foreignkeyoption-input", $(".examples-example-foreignkeyoption-a.active").next().val());
                formData.append("examples-example-texthexcolour-input", $("#examples-example-texthexcolour-input").val());
                formData.append("examples-example-time-input", $("#examples-example-time-input").val());
                

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
                        window.location.replace("/Examples/ExampleQueryPage");
                    }
                };
                //Open connection
                xmlHttpRequest.open("POST", "/api/Examples/Example/1/InsertOrUpdateAsync", true);
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