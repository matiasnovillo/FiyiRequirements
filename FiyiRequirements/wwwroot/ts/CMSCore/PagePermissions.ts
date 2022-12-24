import * as $ from "jquery";
import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";

let RoleId = "0";

//LOAD EVENT
if ($("#title-page").html().includes("Permissions")) {

    //Activate when the button is pushed
    $(".role-a").on("click", function (e) {

        RoleId = $(this).next().val()?.toString() ?? "";

        $("#checkboxes-permissions").html(``);


        Rx.from(ajax.get("/api/CMSCore/RoleMenu/1/SelectAllByRoleIdToRoleMenuForChechboxes/" + RoleId)).subscribe({
                next: newrow => {

                newrow.response.forEach(function (item) {

                    $("#checkboxes-permissions").append(`<div class="form-group mb-3">
    <label class="form-control-label d-inline d-sm-inline d-md-inline d-lg-none d-xl-none">
        <i class="fas fa-toggle-on"></i> ${item.Text}
    </label>
    <div class="input-group input-group-merge input-group-alternative">
        <div class="input-group-prepend d-none d-sm-none d-md-none d-lg-inline d-xl-inline">
            <span class="input-group-text">
                <strong>
                    <i class="fas fa-toggle-on"></i> ${item.Text}
                </strong>
            </span>
        </div>
        <label class="custom-toggle ml-2 mt-2 mr-4">
            <input type="checkbox" value="${item.Value}" ${(item.Selected == true ? "checked" : "")}>
            <span class="custom-toggle-slider rounded-circle" data-label-off="OFF" data-label-on="ON">
            </span>
        </label>
    </div>
</div>`);
                });

                    
                },
                complete: () => {
                },
                error: err => {
                    console.log("Error:" + err)
                }
            });
    });

    $("#update-button").on("click", function (e) {
        let formData = new FormData();

        //RoleId value
        formData.append("RoleId", RoleId);

        //MenuId and Selected values
        $("input:checkbox").each(function () {
            formData.append("MenuId", $(this).val()?.toString() ?? "");
            formData.append("Selected", $(this).is(":checked")?.toString())
        });

        //Setup request
        var xmlHttpRequest = new XMLHttpRequest();
        //Set event listeners
        xmlHttpRequest.upload.addEventListener("loadstart", function (e) {
            //Show success button and success message modal
            $("#message").addClass("btn-secondary");
            $("#message").removeClass("btn-success");
            $("#message").removeClass("btn-danger");
            $("#message").html(`Sending data. Please, wait`);
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
                $("#message").addClass("btn-danger");
                $("#message").removeClass("btn-success");
                $("#message").removeClass("btn-secondary");
                $("#message").html(`<i class="fas fa-exclamation-triangle"></i> 
                                        There was an error while sending the data`);
                console.log("Error:" + xmlHttpRequest.response);
            }
            else {
                //Show success button
                $("#message").addClass("btn-success");
                $("#message").removeClass("btn-danger");
                $("#message").removeClass("btn-secondary");
                $("#message").html(`<i class="fas fa-check"></i>
                                        Data sent successfully`);
            }
        };
        //Open connection
        xmlHttpRequest.open("POST", "/api/CMSCore/RoleMenu/1/InsertPermissions/", true);
        //Send request
        xmlHttpRequest.send(formData);
    });
}

