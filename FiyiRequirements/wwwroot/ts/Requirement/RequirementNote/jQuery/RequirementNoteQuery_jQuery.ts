//Import libraries to use
import { RequirementNoteModel, requirementnotemodelQuery } from "../../RequirementNote/TsModels/RequirementNote_TsModel";
import * as $ from "jquery";
import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";

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

//Set default values
let LastTopDistance: number = 0;
let QueryString: string = "";
let ActualPageNumber: number = 1;
let RowsPerPage: number = 50;
let SorterColumn: string | undefined = "";
let SortToggler: boolean = false;
let TotalPages: number = 0;
let TotalRows: number = 0;
let ViewToggler: string = "List";
let ScrollDownNSearchFlag: boolean = false;

class RequirementNoteQuery {
    static SelectAllPagedToHTML(request_requirementnotemodelQuery: requirementnotemodelQuery) {
        //Used for list view
        $(window).off("scroll");

        //Load some part of table
        var TableContent: string = `<thead class="thead-light">
    <tr>
        <th scope="col">
            <div>
                <input id="requirementnote-table-check-all" type="checkbox">
            </div>
        </th>
        <th scope="col">
            <button value="RequirementNoteId" class="btn btn-outline-secondary btn-sm" type="button">
                Note ID
            </button>
        </th>
        <th scope="col">
            <button value="Active" class="btn btn-outline-secondary btn-sm" type="button">
                Active
            </button>
        </th>
        <th scope="col">
            <button value="DateTimeCreation" class="btn btn-outline-secondary btn-sm" type="button">
                Date Time Creation
            </button>
        </th>
        <th scope="col">
            <button value="DateTimeLastModification" class="btn btn-outline-secondary btn-sm" type="button">
                Date Time Last Modification
            </button>
        </th>
        <th scope="col">
            <button value="UserCreationId" class="btn btn-outline-secondary btn-sm" type="button">
                User Creation
            </button>
        </th>
        <th scope="col">
            <button value="UserLastModificationId" class="btn btn-outline-secondary btn-sm" type="button">
                User Last Modification
            </button>
        </th>
        <th scope="col">
            <button value="Title" class="btn btn-outline-secondary btn-sm" type="button">
                Title
            </button>
        </th>
        <th scope="col">
            <button value="Body" class="btn btn-outline-secondary btn-sm" type="button">
                Body
            </button>
        </th>
        <th scope="col">
            <button value="RequirementId" class="btn btn-outline-secondary btn-sm" type="button">
                Requirement ID
            </button>
        </th>
        
        <th scope="col"></th>
    </tr>
</thead>
<tbody>`;

        var ListContent: string = ``;

        RequirementNoteModel.SelectAllPaged(request_requirementnotemodelQuery, $("#requirement-requirement-requirementid-input").val()).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {

                        const response_requirementnoteQuery = newrow.response as requirementnotemodelQuery;

                        //Set to default values if they are null
                        QueryString = response_requirementnoteQuery.QueryString ?? "";
                        ActualPageNumber = response_requirementnoteQuery.ActualPageNumber ?? 0;
                        RowsPerPage = response_requirementnoteQuery.RowsPerPage ?? 0;
                        SorterColumn = response_requirementnoteQuery.SorterColumn ?? "";
                        SortToggler = response_requirementnoteQuery.SortToggler ?? false;
                        TotalRows = response_requirementnoteQuery.TotalRows ?? 0;
                        TotalPages = response_requirementnoteQuery.TotalPages ?? 0;

                        //Query string
                        $("#requirement-requirementnote-query-string").attr("placeholder", `Search... (${TotalRows} records)`);
                        //Total pages of pagination
                        $("#requirement-requirementnote-total-pages-lg, #requirement-requirementnote-total-pages").html(TotalPages.toString());
                        //Actual page number of pagination
                        $("#requirement-requirementnote-actual-page-number-lg, #requirement-requirementnote-actual-page-number").html(ActualPageNumber.toString());
                        //If we are at the final of book disable next and last buttons in pagination
                        if (ActualPageNumber === TotalPages) {
                            $("#requirement-requirementnote-lnk-next-page-lg, #requirement-requirementnote-lnk-next-page").attr("disabled", "disabled");
                            $("#requirement-requirementnote-lnk-last-page-lg, #requirement-requirementnote-lnk-last-page").attr("disabled", "disabled");
                            $("#requirement-requirementnote-search-more-button-in-list").html("");
                        }
                        else {
                            $("#requirement-requirementnote-lnk-next-page-lg, #requirement-requirementnote-lnk-next-page").removeAttr("disabled");
                            $("#requirement-requirementnote-lnk-last-page-lg, #requirement-requirementnote-lnk-last-page").removeAttr("disabled");
                            //Scroll arrow for list view
                            $("#requirement-requirementnote-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                        }
                        //If we are at the begining of the book disable previous and first buttons in pagination
                        if (ActualPageNumber === 1) {
                            $("#requirement-requirementnote-lnk-previous-page-lg, #requirement-requirementnote-lnk-previous-page").attr("disabled", "disabled");
                            $("#requirement-requirementnote-lnk-first-page-lg, #requirement-requirementnote-lnk-first-page").attr("disabled", "disabled");
                        }
                        else {
                            $("#requirement-requirementnote-lnk-previous-page-lg, #requirement-requirementnote-lnk-previous-page").removeAttr("disabled");
                            $("#requirement-requirementnote-lnk-first-page-lg, #requirement-requirementnote-lnk-first-page").removeAttr("disabled");
                        }
                        //If book is empty set to default pagination values
                        if (response_requirementnoteQuery?.lstRequirementNoteModel?.length === 0) {
                            $("#requirement-requirementnote-lnk-previous-page-lg, #requirement-requirementnote-lnk-previous-page").attr("disabled", "disabled");
                            $("#requirement-requirementnote-lnk-first-page-lg, #requirement-requirementnote-lnk-first-page").attr("disabled", "disabled");
                            $("#requirement-requirementnote-lnk-next-page-lg, #requirement-requirementnote-lnk-next-page").attr("disabled", "disabled");
                            $("#requirement-requirementnote-lnk-last-page-lg, #requirement-requirementnote-lnk-last-page").attr("disabled", "disabled");
                            $("#requirement-requirementnote-total-pages-lg, #requirement-requirementnote-total-pages").html("1");
                            $("#requirement-requirementnote-actual-page-number-lg, #requirement-requirementnote-actual-page-number").html("1");
                        }
                        //Read data book
                        response_requirementnoteQuery?.lstRequirementNoteModel?.forEach(row => {

                            TableContent += `<tr>
    <!-- Checkbox -->
    <td>
        <div>
            <input class="requirementnote-table-checkbox-for-row" value="${row.RequirementNoteId}" type="checkbox">
        </div>
    </td>
    <!-- Data -->
    <td class="text-left text-light">
        <i class="fas fa-key"></i> ${row.RequirementNoteId}
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-toggle-on"></i> ${row.Active == true ? "Active <i class='text-success fas fa-circle'></i>" : "Not active <i class='text-danger fas fa-circle'></i>"}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-calendar"></i> ${row.DateTimeCreation}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-calendar"></i> ${row.DateTimeLastModification}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.UserCreationIdFantasyName}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.UserLastModificationIdFantasyName}
        </strong>
    </td>
    <td class="text-left">
        <strong><i class="fas fa-font">
            </i> ${row.Title}
        </strong>
    </td>
    <td class="text-left">
        <i class="fas fa-font"></i> ${row.Body}
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.RequirementId}
        </strong>
    </td>
    
    <!-- Actions -->
    <td class="text-right">
        <div class="dropdown">
            <button class="btn btn-icon-only text-danger" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-trash"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button class="dropdown-item text-danger requirement-requirementnote-table-delete-button" value="${row.RequirementNoteId}" type="button">
                    <i class="fas fa-exclamation-triangle"></i> Yes, delete
                </button>
            </div>
        </div>
    </td>
</tr>`;

                            ListContent += `<div class="row mx-2">
    <div class="col-sm">
        <div class="card bg-gradient-primary mb-2">
            <div class="card-body">
                <div class="row">
                    <div class="col text-truncate">
                        <span class="text-white text-light mb-4">
                           Note ID <i class="fas fa-key"></i> ${row.RequirementNoteId}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Active <i class="fas fa-toggle-on"></i> ${row.Active == true ? "Active <i class='text-success fas fa-circle'></i>" : "Not active <i class='text-danger fas fa-circle'></i>"}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Date Time Creation <i class="fas fa-calendar"></i> ${row.DateTimeCreation}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Date Time Last Modification <i class="fas fa-calendar"></i> ${row.DateTimeLastModification}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           User Creation <i class="fas fa-key"></i> ${row.UserCreationIdFantasyName}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           User Last Modification <i class="fas fa-key"></i> ${row.UserLastModificationIdFantasyName}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Title <i class="fas fa-font"></i> ${row.Title}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Body <i class="fas fa-font"></i> ${row.Body}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Requirement ID <i class="fas fa-key"></i> ${row.RequirementId}
                        </span>
                        <br/>
                        
                    </div>
                    <div class="col-auto">
                    </div>
                </div>
                <!-- Actions -->
                <div class="row">
                    <div class="col">
                        <div class="justify-content-end text-right mt-2">
                            <div class="dropup">
                                <a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-ellipsis-v"></i>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                                    <button value="${row.RequirementNoteId}" class="dropdown-item text-danger requirement-requirementnote-list-delete-button" type="button">
                                        <i class="fas fa-trash"></i>&nbsp;Delete
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>`;
                        })

                        //If view table is activated, clear table view, if not, clear list view
                        if (ViewToggler === "Table") {
                            $("#requirement-requirementnote-body-and-head-table").html("");
                            $("#requirement-requirementnote-body-and-head-table").html(TableContent);
                        }
                        else {
                            //Used for list view
                            if (ScrollDownNSearchFlag) {
                                $("#requirement-requirementnote-body-list").append(ListContent);
                                ScrollDownNSearchFlag = false;
                            }
                            else {
                                //Clear list view
                                $("#requirement-requirementnote-body-list").html("");
                                $("#requirement-requirementnote-body-list").html(ListContent);
                            }
                            }
                    }
                    else {
                        //Show error message
                        $("#requirement-requirementnote-error-message-title").html("No registers found");
                        $("#requirement-requirementnote-error-message-text").html("The server did not found any register. HTTP code 204");
                        $("#requirement-requirementnote-button-error-message-in-card").show();
                    }
                },
                complete: () => {
                    //Execute ScrollDownNSearch function when the user scroll the page
                    $(window).on("scroll", ScrollDownNSearch);

                    //Add final content to TableContent
                    TableContent += `</tbody>
                                </table>`;

                    //Check button inside list view
                    $(".requirement-requirementnote-checkbox-list").on("click", function (e) {
                        //Toggler
                        if ($(this).hasClass("list-row-checked")) {
                            $(this).html(`<a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                                            <i class="fas fa-circle text-white"></i>
                                                        </a>`);
                            $(this).removeClass("list-row-checked").addClass("list-row-unchecked");
                        }
                        else {
                            $(this).html(`<a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                                            <i class="fas fa-check"></i>
                                                        </a>`);
                            $(this).removeClass("list-row-unchecked").addClass("list-row-checked");
                        }
                    });

                    //Check all button inside table
                    $("#requirementnote-table-check-all").on("click", function (e) { 
                        //Toggler
                        if ($("tr td div input.requirementnote-table-checkbox-for-row").is(":checked")) {
                            $("tr td div input.requirementnote-table-checkbox-for-row").removeAttr("checked");
                        }
                        else {
                            $("tr td div input.requirementnote-table-checkbox-for-row").attr("checked", "checked");
                        }
                    });

                    //Buttons inside head of table
                    $("tr th button").one("click", function (e) {
                        //Toggler
                        if (SorterColumn == $(this).attr("value")) {
                            SorterColumn = "";
                            SortToggler = true;
                        }
                        else {
                            SorterColumn = $(this).attr("value");
                            SortToggler = false;
                        }

                        ValidateAndSearch();
                    });

                    //Hide error message
                    $("#requirement-requirementnote-error-message-title").html("");
                    $("#requirement-requirementnote-error-message-text").html("");
                    $("#requirement-requirementnote-button-error-message-in-card").hide();

                    //Delete button in table and list
                    $("div.dropdown-menu button.requirement-requirementnote-table-delete-button, div.dropdown-menu button.requirement-requirementnote-list-delete-button").on("click", function (e) {
                        let RequirementNoteId = $(this).val();
                        RequirementNoteModel.DeleteByRequirementNoteId(RequirementNoteId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                ValidateAndSearch();

                                //Show OK message
                                $("#requirement-requirementnote-button-error-message-in-card").hide();
                                $("#requirement-requirementnote-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Row deleted successfully`);
                                $("#requirement-requirementnote-button-ok-message-in-card").show();
                            },
                            error: err => {
                                //Related to error message
                                $("#requirement-requirementnote-error-message-title").html("RequirementNoteModel.DeleteByRequirementNoteId(RequirementNoteId).subscribe(...)");
                                $("#requirement-requirementnote-error-message-text").html(err);
                                $("#requirement-requirementnote-button-error-message-in-card").show();
                            }
                        });
                    });

                    //Copy button in table and list
                    $("div.dropdown-menu button.requirement-requirementnote-table-copy-button, div.dropdown-menu button.requirement-requirementnote-list-copy-button").on("click", function (e) {
                        let RequirementNoteId = $(this).val();
                        RequirementNoteModel.CopyByRequirementNoteId(RequirementNoteId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                ValidateAndSearch();

                                //Show OK message
                                $("#requirement-requirementnote-button-error-message-in-card").hide();
                                $("#requirement-requirementnote-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Row copied successfully`);
                                $("#requirement-requirementnote-button-ok-message-in-card").show();
                            },
                            error: err => {
                                //Show error message
                                $("#requirement-requirementnote-error-message-title").html("RequirementNoteModel.CopyByRequirementNoteId(RequirementNoteId).subscribe(...)");
                                $("#requirement-requirementnote-error-message-text").html(err);
                                $("#requirement-requirementnote-button-error-message-in-card").show();
                            }
                        });
                    });
                },
                error: err => {
                    //Show error message
                    $("#requirement-requirementnote-error-message-title").html("RequirementNoteModel.SelectAllPaged(request_requirementnotemodelQ).subscribe(...)");
                    $("#requirement-requirementnote-error-message-text").html(err);
                    $("#requirement-requirementnote-button-error-message-in-card").show();
                }
            });
    }
}

function ValidateAndSearch() {

    //Hide error and OK message button
    $("#requirement-requirementnote-button-error-message-in-card").hide();
    $("#requirement-requirementnote-button-ok-message-in-card").hide();

    var _requirementnotemodelQuery: requirementnotemodelQuery = {
        QueryString,
        ActualPageNumber,
        RowsPerPage,
        SorterColumn,
        SortToggler,
        TotalRows,
        TotalPages
    };

    RequirementNoteQuery.SelectAllPagedToHTML(_requirementnotemodelQuery);
}

//LOAD EVENT
if ($("#requirement-requirement-title-page").html().includes("Edit requirement")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "RequirementNoteId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#requirement-requirementnote-lnk-first-page-lg, #requirement-requirementnote-lnk-first-page").attr("disabled", "disabled");
    $("#requirement-requirementnote-lnk-previous-page-lg, #requirement-requirementnote-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#requirement-requirementnote-export-message").html("");
    $("#requirement-requirementnote-button-error-message-in-card").hide();
    $("#requirement-requirementnote-button-ok-message-in-card").hide();

    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#requirement-requirementnote-search-button")).on("click", function () {
    ValidateAndSearch();
});

//Query string
$("#requirement-requirementnote-query-string").on("change keyup input", function (e) {
    //If undefined, set QueryString to "" value
    QueryString = ($(this).val()?.toString()) ?? "" ;
    ValidateAndSearch();
});

//First page link in pagination
$("#requirement-requirementnote-lnk-first-page-lg, #requirement-requirementnote-lnk-first-page").on("click", function (e) {
    ActualPageNumber = 1;
    ValidateAndSearch();
});

//Previous page link in pagination
$("#requirement-requirementnote-lnk-previous-page-lg, #requirement-requirementnote-lnk-previous-page").on("click", function (e) {
    ActualPageNumber -= 1;
    ValidateAndSearch();
});

//Next page link in pagination
$("#requirement-requirementnote-lnk-next-page-lg, #requirement-requirementnote-lnk-next-page").on("click", function (e) {
    ActualPageNumber += 1;
    ValidateAndSearch();
});

//Last page link in pagination
$("#requirement-requirementnote-lnk-last-page-lg, #requirement-requirementnote-lnk-last-page").on("click", function (e) {
    ActualPageNumber = TotalPages;
    ValidateAndSearch();
});

//Table view button
$("#requirement-requirementnote-table-view-button").on("click", function (e) {
    $("#requirement-requirementnote-view-toggler").val("Table");
    ViewToggler = "Table";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear table view
    $("#requirement-requirementnote-body-and-head-table").html("");
    ValidateAndSearch();
});

//List view button
$("#requirement-requirementnote-list-view-button").on("click", function (e) {
    $("#requirement-requirementnote-view-toggler").val("List");
    ViewToggler = "List";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear list view
    $("#requirement-requirementnote-body-list").html("");
    ValidateAndSearch();
});

//Used to list view
function ScrollDownNSearch() {
    let WindowsTopDistance: number = $(window).scrollTop() ?? 0;
    let WindowsBottomDistance: number = ($(window).scrollTop() ?? 0) + ($(window).innerHeight() ?? 0);
    let CardsFooterTopPosition: number = $("#requirement-requirementnote-search-more-button-in-list").offset()?.top ?? 0;
    let CardsFooterBottomPosition: number = ($("#requirement-requirementnote-search-more-button-in-list").offset()?.top ?? 0) + ($("#requirement-requirementnote-search-more-button-in-list").outerHeight() ?? 0);

    if (WindowsTopDistance > LastTopDistance) {
        //Scroll down
        if ((WindowsBottomDistance > CardsFooterTopPosition) && (WindowsTopDistance < CardsFooterBottomPosition)) {
            //Search More button visible
            if (ActualPageNumber !== TotalPages) {
                ScrollDownNSearchFlag = true;
                ActualPageNumber += 1;
                ValidateAndSearch();
            }
        }
        else { /*Card footer not visible*/ }
    }
    else { /*Scroll up*/ }
    LastTopDistance = WindowsTopDistance;
}

//Used to list view
$(window).on("scroll", ScrollDownNSearch);

//Export as PDF button
$("#requirement-requirementnote-export-as-pdf").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirementnote-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else{
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementnote-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
    }

    Rx.from(ajax.post("/api/Requirement/RequirementNote/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirementnote-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for PDF file
            $("#requirement-requirementnote-export-message").html(`<a class="btn btn-icon btn-success" href="/PDFFiles/Requirement/RequirementNote/RequirementNote_${DateTimeNow.AjaxForString}.pdf" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-pdf"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirementnote-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirementnote-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementnote-error-message-title").html("Rx.from(ajax.post('/api/Requirement/RequirementNote/1/ExportAsPDF/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirementnote-error-message-text").html(err);
            $("#requirement-requirementnote-button-error-message-in-card").show();
        }
    });
});

//Export as Excel button
$("#requirement-requirementnote-export-as-excel").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirementnote-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementnote-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
    }

    Rx.from(ajax.post("/api/Requirement/RequirementNote/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirementnote-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for Excel file
            $("#requirement-requirementnote-export-message").html(`<a class="btn btn-icon btn-success" href="/ExcelFiles/Requirement/RequirementNote/RequirementNote_${DateTimeNow.AjaxForString}.xlsx" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-excel"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirementnote-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirementnote-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementnote-error-message-title").html("Rx.from(ajax.post('/api/Requirement/RequirementNote/1/ExportAsExcel/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirementnote-error-message-text").html(err);
            $("#requirement-requirementnote-button-error-message-in-card").show();
        }
    });
});

//Export as CSV button
$("#requirement-requirementnote-export-as-csv").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirementnote-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementnote-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });

            Body = {
                AjaxForString: CheckedRows.toString()
            };
        }
    }

    Rx.from(ajax.post("/api/Requirement/RequirementNote/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirementnote-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for CSV file
            $("#requirement-requirementnote-export-message").html(`<a class="btn btn-icon btn-success" href="/CSVFiles/Requirement/RequirementNote/RequirementNote_${DateTimeNow.AjaxForString}.csv" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-csv"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirementnote-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirementnote-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementnote-error-message-title").html("Rx.from(ajax.post('/api/Requirement/RequirementNote/1/ExportAsCSV/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirementnote-error-message-text").html(err);
            $("#requirement-requirementnote-button-error-message-in-card").show();
        }
    });
});

//Export close button in modal
$("#requirement-requirementnote-export-close-button").on("click", function (e) {
    $("#requirement-requirementnote-export-message").html("");
});

//Massive action Copy
$("#requirement-requirementnote-massive-action-copy").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let CopyType: string = "";
    let Body: Ajax = {};

    if ($("#requirement-requirementnote-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementnote-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows.toString()
        };
    }

    RequirementNoteModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            ValidateAndSearch();

            //Show OK message
            $("#requirement-requirementnote-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Rows copied successfully`);
            $("#requirement-requirementnote-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementnote-error-message-title").html("RequirementNoteModel.Copy(CopyType).subscribe(...)");
            $("#requirement-requirementnote-error-message-text").html(err);
            $("#requirement-requirementnote-button-error-message-in-card").show();
        }
    });
});

//Massive action Delete
$("#requirement-requirementnote-massive-action-delete").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let DeleteType: string = "";
    let Body: Ajax = {};

    if ($("#requirement-requirementnote-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementnote-table-checkbox-for-row:checked").each(function () {
                CheckedRows.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows.toString()
        };
    }

    RequirementNoteModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            ValidateAndSearch();

            //Show OK message
            $("#requirement-requirementnote-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Rows deleted successfully`);
            $("#requirement-requirementnote-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementnote-error-message-title").html("RequirementNoteModel.Copy(CopyType).subscribe(...)");
            $("#requirement-requirementnote-error-message-text").html(err);
            $("#requirement-requirementnote-button-error-message-in-card").show();
        }
    });
});