

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

//Last modification on: 31/01/2023 7:54:01

$(document).ready(function () {
examplesexampletexttexteditorquill.root.innerHTML = $("#examples-example-texttexteditor-hidden-value").val();
    
});

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



//Create a formdata object
var formData = new FormData();
$("#examples-example-insert-or-update-button").on("click", function (e) {
    //Stop stuff happening
    e.stopPropagation();
    e.preventDefault();

    //Add or edit value
    formData.append("examples-example-title-page", $("#examples-example-title-page").html());
    formData.append("examples-example-exampleid-input", $("#examples-example-exampleid-input").val());

    formData.append("examples-example-boolean-input", $("#examples-example-boolean-input").is(":checked"));
    formData.append("examples-example-datetime-input", $("#examples-example-datetime-input").val());
    formData.append("examples-example-decimal-input", $("#examples-example-decimal-input").val());
    formData.append("examples-example-foreignkeydropdown-input", $("#examples-example-foreignkeydropdown-input").val());
    formData.append("examples-example-foreignkeyoptions-input", $(".examples-example-foreignkeyoptions-a.active").next().val());
    formData.append("examples-example-integer-input", $("#examples-example-integer-input").val());
    formData.append("examples-example-textbasic-input", $("#examples-example-textbasic-input").val());
    formData.append("examples-example-textemail-input", $("#examples-example-textemail-input").val());
    if (!examplesexampletextfileboolfileadded) {
    formData.append("examples-example-textfile-input", $("#examples-example-textfile-readonly").val());
}
formData.append("examples-example-texthexcolour-input", $("#examples-example-texthexcolour-input").val());
    formData.append("examples-example-textpassword-input", $("#examples-example-textpassword-input").val());
    formData.append("examples-example-textphonenumber-input", $("#examples-example-textphonenumber-input").val());
    formData.append("examples-example-texttag-input", $("#examples-example-texttag-input").val());
    formData.append("examples-example-texttextarea-input", $("#examples-example-texttextarea-input").val());
    formData.append("examples-example-texttexteditor-input", examplesexampletexttexteditorquill.root.innerHTML);
    formData.append("examples-example-texturl-input", $("#examples-example-texturl-input").val());
    formData.append("examples-example-time-input", $("#examples-example-time-input").val());
    

    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();
    //Set event listeners
    xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
        //Show success button and success message modal
        $("#examples-example-insert-or-update-message").addClass("btn-secondary");
        $("#examples-example-insert-or-update-message").removeClass("btn-success");
        $("#examples-example-insert-or-update-message").removeClass("btn-error");
        $("#examples-example-insert-or-update-message").removeAttr("data-toggle");
        $("#examples-example-insert-or-update-message").removeAttr("data-target");
        $("#examples-example-insert-or-update-message").html(`Sending data. Please, wait`);
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
            $("#examples-example-insert-or-update-message").addClass("btn-danger");
            $("#examples-example-insert-or-update-message").removeClass("btn-success");
            $("#examples-example-insert-or-update-message").removeClass("btn-secondary");
            $("#examples-example-insert-or-update-message").attr("data-toggle", "modal");
            $("#examples-example-insert-or-update-message").attr("data-target", "#examples-example-error-message-modal");
            $("#examples-example-insert-or-update-message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                                                There was an error while sending the data`);
            $("#examples-example-error-message-title").html("There was an error while sending the data");
            $("#examples-example-error-message-text").html(xmlHttpRequest.response);
            console.log("Error:" + xmlHttpRequest.response);
        }
        else {
            //Show success button
            $("#examples-example-insert-or-update-message").addClass("btn-success");
            $("#examples-example-insert-or-update-message").removeClass("btn-error");
            $("#examples-example-insert-or-update-message").removeClass("btn-secondary");
            $("#examples-example-insert-or-update-message").removeAttr("data-toggle");
            $("#examples-example-insert-or-update-message").removeAttr("data-target");
            $("#examples-example-insert-or-update-message").html(`<i class="fas fa-check"></i>
                                                                Data sent successfully`);
        }
    };
    //Open connection
    xmlHttpRequest.open("POST", "/api/Examples/Example/1/InsertOrUpdateAsync", true);
    //Send request
    xmlHttpRequest.send(formData);
});