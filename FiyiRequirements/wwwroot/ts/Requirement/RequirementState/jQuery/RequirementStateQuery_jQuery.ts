//Import libraries to use
import { RequirementStateModel, requirementstatemodelQuery } from "../../RequirementState/TsModels/RequirementState_TsModel";
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

//Last modification on: 24/12/2022 6:47:04

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

class RequirementStateQuery {
    static SelectAllPagedToHTML(request_requirementstatemodelQuery: requirementstatemodelQuery) {
        //Used for list view
        $(window).off("scroll");

        //Load some part of table
        var TableContent: string = `<thead class="thead-light">
    <tr>
        <th scope="col">
            <div>
                <input id="requirementstate-table-check-all" type="checkbox">
            </div>
        </th>
        <th scope="col">
            <button value="RequirementStateId" class="btn btn-outline-secondary btn-sm" type="button">
                RequirementStateId
            </button>
        </th>
        <th scope="col">
            <button value="Active" class="btn btn-outline-secondary btn-sm" type="button">
                Active
            </button>
        </th>
        <th scope="col">
            <button value="DateTimeCreation" class="btn btn-outline-secondary btn-sm" type="button">
                DateTimeCreation
            </button>
        </th>
        <th scope="col">
            <button value="DateTimeLastModification" class="btn btn-outline-secondary btn-sm" type="button">
                DateTimeLastModification
            </button>
        </th>
        <th scope="col">
            <button value="UserCreationId" class="btn btn-outline-secondary btn-sm" type="button">
                UserCreationId
            </button>
        </th>
        <th scope="col">
            <button value="UserLastModificationId" class="btn btn-outline-secondary btn-sm" type="button">
                UserLastModificationId
            </button>
        </th>
        <th scope="col">
            <button value="Name" class="btn btn-outline-secondary btn-sm" type="button">
                Name
            </button>
        </th>
        
        <th scope="col"></th>
    </tr>
</thead>
<tbody>`;

        var ListContent: string = ``;

        RequirementStateModel.SelectAllPaged(request_requirementstatemodelQuery).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {

                        const response_requirementstateQuery = newrow.response as requirementstatemodelQuery;

                        //Set to default values if they are null
                        QueryString = response_requirementstateQuery.QueryString ?? "";
                        ActualPageNumber = response_requirementstateQuery.ActualPageNumber ?? 0;
                        RowsPerPage = response_requirementstateQuery.RowsPerPage ?? 0;
                        SorterColumn = response_requirementstateQuery.SorterColumn ?? "";
                        SortToggler = response_requirementstateQuery.SortToggler ?? false;
                        TotalRows = response_requirementstateQuery.TotalRows ?? 0;
                        TotalPages = response_requirementstateQuery.TotalPages ?? 0;

                        //Query string
                        $("#requirement-requirementstate-query-string").attr("placeholder", `Search... (${TotalRows} records)`);
                        //Total pages of pagination
                        $("#requirement-requirementstate-total-pages-lg, #requirement-requirementstate-total-pages").html(TotalPages.toString());
                        //Actual page number of pagination
                        $("#requirement-requirementstate-actual-page-number-lg, #requirement-requirementstate-actual-page-number").html(ActualPageNumber.toString());
                        //If we are at the final of book disable next and last buttons in pagination
                        if (ActualPageNumber === TotalPages) {
                            $("#requirement-requirementstate-lnk-next-page-lg, #requirement-requirementstate-lnk-next-page").attr("disabled", "disabled");
                            $("#requirement-requirementstate-lnk-last-page-lg, #requirement-requirementstate-lnk-last-page").attr("disabled", "disabled");
                            $("#requirement-requirementstate-search-more-button-in-list").html("");
                        }
                        else {
                            $("#requirement-requirementstate-lnk-next-page-lg, #requirement-requirementstate-lnk-next-page").removeAttr("disabled");
                            $("#requirement-requirementstate-lnk-last-page-lg, #requirement-requirementstate-lnk-last-page").removeAttr("disabled");
                            //Scroll arrow for list view
                            $("#requirement-requirementstate-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                        }
                        //If we are at the begining of the book disable previous and first buttons in pagination
                        if (ActualPageNumber === 1) {
                            $("#requirement-requirementstate-lnk-previous-page-lg, #requirement-requirementstate-lnk-previous-page").attr("disabled", "disabled");
                            $("#requirement-requirementstate-lnk-first-page-lg, #requirement-requirementstate-lnk-first-page").attr("disabled", "disabled");
                        }
                        else {
                            $("#requirement-requirementstate-lnk-previous-page-lg, #requirement-requirementstate-lnk-previous-page").removeAttr("disabled");
                            $("#requirement-requirementstate-lnk-first-page-lg, #requirement-requirementstate-lnk-first-page").removeAttr("disabled");
                        }
                        //If book is empty set to default pagination values
                        if (response_requirementstateQuery?.lstRequirementStateModel?.length === 0) {
                            $("#requirement-requirementstate-lnk-previous-page-lg, #requirement-requirementstate-lnk-previous-page").attr("disabled", "disabled");
                            $("#requirement-requirementstate-lnk-first-page-lg, #requirement-requirementstate-lnk-first-page").attr("disabled", "disabled");
                            $("#requirement-requirementstate-lnk-next-page-lg, #requirement-requirementstate-lnk-next-page").attr("disabled", "disabled");
                            $("#requirement-requirementstate-lnk-last-page-lg, #requirement-requirementstate-lnk-last-page").attr("disabled", "disabled");
                            $("#requirement-requirementstate-total-pages-lg, #requirement-requirementstate-total-pages").html("1");
                            $("#requirement-requirementstate-actual-page-number-lg, #requirement-requirementstate-actual-page-number").html("1");
                        }
                        //Read data book
                        response_requirementstateQuery?.lstRequirementStateModel?.forEach(row => {

                            TableContent += `<tr>
    <!-- Checkbox -->
    <td>
        <div>
            <input class="requirementstate-table-checkbox-for-row" value="${row.RequirementStateId}" type="checkbox">
        </div>
    </td>
    <!-- Data -->
    <td class="text-left text-light">
        <i class="fas fa-key"></i> ${row.RequirementStateId}
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
            <i class="fas fa-key"></i> ${row.UserCreationId}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.UserLastModificationId}
        </strong>
    </td>
    <td class="text-left">
        <strong><i class="fas fa-font">
            </i> ${row.Name}
        </strong>
    </td>
    
    <!-- Actions -->
    <td class="text-right">
        <a class="btn btn-icon-only text-primary" href="/Requirement/PageRequirementStateNonQuery?RequirementStateId=${row.RequirementStateId}" role="button" data-toggle="tooltip" data-original-title="Edit">
            <i class="fas fa-edit"></i>
        </a>
        <div class="dropdown">
            <button class="btn btn-icon-only text-danger" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-trash"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button class="dropdown-item text-danger requirement-requirementstate-table-delete-button" value="${row.RequirementStateId}" type="button">
                    <i class="fas fa-exclamation-triangle"></i> Yes, delete
                </button>
            </div>
        </div>
        <div class="dropdown">
            <button class="btn btn-sm btn-icon-only text-primary" href="#" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-ellipsis-v"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button type="button" class="dropdown-item requirement-requirementstate-table-copy-button" value="${row.RequirementStateId}">
                    <i class="fas fa-copy text-primary"></i>&nbsp;Copy
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
                           RequirementStateId <i class="fas fa-key"></i> ${row.RequirementStateId}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Active <i class="fas fa-toggle-on"></i> ${row.Active == true ? "Active <i class='text-success fas fa-circle'></i>" : "Not active <i class='text-danger fas fa-circle'></i>"}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           DateTimeCreation <i class="fas fa-calendar"></i> ${row.DateTimeCreation}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           DateTimeLastModification <i class="fas fa-calendar"></i> ${row.DateTimeLastModification}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           UserCreationId <i class="fas fa-key"></i> ${row.UserCreationId}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           UserLastModificationId <i class="fas fa-key"></i> ${row.UserLastModificationId}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Name <i class="fas fa-font"></i> ${row.Name}
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
                            <div class="requirement-requirementstate-checkbox-list list-row-unchecked mb-2">
                                <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                    <i class="fas fa-circle text-white"></i>
                                </a>
                            </div>
                            <input type="hidden" value="${row.RequirementStateId}"/>
                            <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="/Requirement/PageRequirementStateNonQuery?RequirementStateId=${row.RequirementStateId}" role="button" data-toggle="tooltip" data-original-title="edit">
                                <i class="fas fa-edit text-primary"></i>
                            </a>
                            <div class="dropup">
                                <a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-ellipsis-v"></i>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                                    <button value="${row.RequirementStateId}" class="dropdown-item text-primary requirement-requirementstate-list-copy-button" type="button">
                                        <i class="fas fa-copy"></i>&nbsp;Copy
                                    </button>
                                    <button value="${row.RequirementStateId}" class="dropdown-item text-danger requirement-requirementstate-list-delete-button" type="button">
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
                            $("#requirement-requirementstate-body-and-head-table").html("");
                            $("#requirement-requirementstate-body-and-head-table").html(TableContent);
                        }
                        else {
                            //Used for list view
                            if (ScrollDownNSearchFlag) {
                                $("#requirement-requirementstate-body-list").append(ListContent);
                                ScrollDownNSearchFlag = false;
                            }
                            else {
                                //Clear list view
                                $("#requirement-requirementstate-body-list").html("");
                                $("#requirement-requirementstate-body-list").html(ListContent);
                            }
                            }
                    }
                    else {
                        //Show error message
                        $("#requirement-requirementstate-error-message-title").html("No registers found");
                        $("#requirement-requirementstate-error-message-text").html("The server did not found any register. HTTP code 204");
                        $("#requirement-requirementstate-button-error-message-in-card").show();
                    }
                },
                complete: () => {
                    //Execute ScrollDownNSearch function when the user scroll the page
                    $(window).on("scroll", ScrollDownNSearch);

                    //Add final content to TableContent
                    TableContent += `</tbody>
                                </table>`;

                    //Check button inside list view
                    $(".requirement-requirementstate-checkbox-list").on("click", function (e) {
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
                    $("#requirementstate-table-check-all").on("click", function (e) { 
                        //Toggler
                        if ($("tr td div input.requirementstate-table-checkbox-for-row").is(":checked")) {
                            $("tr td div input.requirementstate-table-checkbox-for-row").removeAttr("checked");
                        }
                        else {
                            $("tr td div input.requirementstate-table-checkbox-for-row").attr("checked", "checked");
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
                    $("#requirement-requirementstate-error-message-title").html("");
                    $("#requirement-requirementstate-error-message-text").html("");
                    $("#requirement-requirementstate-button-error-message-in-card").hide();

                    //Delete button in table and list
                    $("div.dropdown-menu button.requirement-requirementstate-table-delete-button, div.dropdown-menu button.requirement-requirementstate-list-delete-button").on("click", function (e) {
                        let RequirementStateId = $(this).val();
                        RequirementStateModel.DeleteByRequirementStateId(RequirementStateId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                ValidateAndSearch();

                                //Show OK message
                                $("#requirement-requirementstate-button-error-message-in-card").hide();
                                $("#requirement-requirementstate-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Row deleted successfully`);
                                $("#requirement-requirementstate-button-ok-message-in-card").show();
                            },
                            error: err => {
                                //Related to error message
                                $("#requirement-requirementstate-error-message-title").html("RequirementStateModel.DeleteByRequirementStateId(RequirementStateId).subscribe(...)");
                                $("#requirement-requirementstate-error-message-text").html(err);
                                $("#requirement-requirementstate-button-error-message-in-card").show();
                            }
                        });
                    });

                    //Copy button in table and list
                    $("div.dropdown-menu button.requirement-requirementstate-table-copy-button, div.dropdown-menu button.requirement-requirementstate-list-copy-button").on("click", function (e) {
                        let RequirementStateId = $(this).val();
                        RequirementStateModel.CopyByRequirementStateId(RequirementStateId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                ValidateAndSearch();

                                //Show OK message
                                $("#requirement-requirementstate-button-error-message-in-card").hide();
                                $("#requirement-requirementstate-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Row copied successfully`);
                                $("#requirement-requirementstate-button-ok-message-in-card").show();
                            },
                            error: err => {
                                //Show error message
                                $("#requirement-requirementstate-error-message-title").html("RequirementStateModel.CopyByRequirementStateId(RequirementStateId).subscribe(...)");
                                $("#requirement-requirementstate-error-message-text").html(err);
                                $("#requirement-requirementstate-button-error-message-in-card").show();
                            }
                        });
                    });
                },
                error: err => {
                    //Show error message
                    $("#requirement-requirementstate-error-message-title").html("RequirementStateModel.SelectAllPaged(request_requirementstatemodelQ).subscribe(...)");
                    $("#requirement-requirementstate-error-message-text").html(err);
                    $("#requirement-requirementstate-button-error-message-in-card").show();
                }
            });
    }
}

function ValidateAndSearch() {

    //Hide error and OK message button
    $("#requirement-requirementstate-button-error-message-in-card").hide();
    $("#requirement-requirementstate-button-ok-message-in-card").hide();

    var _requirementstatemodelQuery: requirementstatemodelQuery = {
        QueryString,
        ActualPageNumber,
        RowsPerPage,
        SorterColumn,
        SortToggler,
        TotalRows,
        TotalPages
    };

    RequirementStateQuery.SelectAllPagedToHTML(_requirementstatemodelQuery);
}

//LOAD EVENT
if ($("#requirement-requirementstate-title-page").html().includes("Query requirementstate")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "RequirementStateId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#requirement-requirementstate-lnk-first-page-lg, #requirement-requirementstate-lnk-first-page").attr("disabled", "disabled");
    $("#requirement-requirementstate-lnk-previous-page-lg, #requirement-requirementstate-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#requirement-requirementstate-export-message").html("");
    $("#requirement-requirementstate-button-error-message-in-card").hide();
    $("#requirement-requirementstate-button-ok-message-in-card").hide();

    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#requirement-requirementstate-search-button")).on("click", function () {
    ValidateAndSearch();
});

//Query string
$("#requirement-requirementstate-query-string").on("change keyup input", function (e) {
    //If undefined, set QueryString to "" value
    QueryString = ($(this).val()?.toString()) ?? "" ;
    ValidateAndSearch();
});

//First page link in pagination
$("#requirement-requirementstate-lnk-first-page-lg, #requirement-requirementstate-lnk-first-page").on("click", function (e) {
    ActualPageNumber = 1;
    ValidateAndSearch();
});

//Previous page link in pagination
$("#requirement-requirementstate-lnk-previous-page-lg, #requirement-requirementstate-lnk-previous-page").on("click", function (e) {
    ActualPageNumber -= 1;
    ValidateAndSearch();
});

//Next page link in pagination
$("#requirement-requirementstate-lnk-next-page-lg, #requirement-requirementstate-lnk-next-page").on("click", function (e) {
    ActualPageNumber += 1;
    ValidateAndSearch();
});

//Last page link in pagination
$("#requirement-requirementstate-lnk-last-page-lg, #requirement-requirementstate-lnk-last-page").on("click", function (e) {
    ActualPageNumber = TotalPages;
    ValidateAndSearch();
});

//Table view button
$("#requirement-requirementstate-table-view-button").on("click", function (e) {
    $("#requirement-requirementstate-view-toggler").val("Table");
    ViewToggler = "Table";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear table view
    $("#requirement-requirementstate-body-and-head-table").html("");
    ValidateAndSearch();
});

//List view button
$("#requirement-requirementstate-list-view-button").on("click", function (e) {
    $("#requirement-requirementstate-view-toggler").val("List");
    ViewToggler = "List";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear list view
    $("#requirement-requirementstate-body-list").html("");
    ValidateAndSearch();
});

//Used to list view
function ScrollDownNSearch() {
    let WindowsTopDistance: number = $(window).scrollTop() ?? 0;
    let WindowsBottomDistance: number = ($(window).scrollTop() ?? 0) + ($(window).innerHeight() ?? 0);
    let CardsFooterTopPosition: number = $("#requirement-requirementstate-search-more-button-in-list").offset()?.top ?? 0;
    let CardsFooterBottomPosition: number = ($("#requirement-requirementstate-search-more-button-in-list").offset()?.top ?? 0) + ($("#requirement-requirementstate-search-more-button-in-list").outerHeight() ?? 0);

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
$("#requirement-requirementstate-export-as-pdf").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirementstate-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else{
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementstate-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/RequirementState/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirementstate-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for PDF file
            $("#requirement-requirementstate-export-message").html(`<a class="btn btn-icon btn-success" href="/PDFFiles/Requirement/RequirementState/RequirementState_${DateTimeNow.AjaxForString}.pdf" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-pdf"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirementstate-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirementstate-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementstate-error-message-title").html("Rx.from(ajax.post('/api/Requirement/RequirementState/1/ExportAsPDF/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirementstate-error-message-text").html(err);
            $("#requirement-requirementstate-button-error-message-in-card").show();
        }
    });
});

//Export as Excel button
$("#requirement-requirementstate-export-as-excel").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirementstate-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementstate-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/RequirementState/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirementstate-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for Excel file
            $("#requirement-requirementstate-export-message").html(`<a class="btn btn-icon btn-success" href="/ExcelFiles/Requirement/RequirementState/RequirementState_${DateTimeNow.AjaxForString}.xlsx" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-excel"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirementstate-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirementstate-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementstate-error-message-title").html("Rx.from(ajax.post('/api/Requirement/RequirementState/1/ExportAsExcel/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirementstate-error-message-text").html(err);
            $("#requirement-requirementstate-button-error-message-in-card").show();
        }
    });
});

//Export as CSV button
$("#requirement-requirementstate-export-as-csv").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirementstate-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementstate-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/RequirementState/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirementstate-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for CSV file
            $("#requirement-requirementstate-export-message").html(`<a class="btn btn-icon btn-success" href="/CSVFiles/Requirement/RequirementState/RequirementState_${DateTimeNow.AjaxForString}.csv" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-csv"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirementstate-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirementstate-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementstate-error-message-title").html("Rx.from(ajax.post('/api/Requirement/RequirementState/1/ExportAsCSV/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirementstate-error-message-text").html(err);
            $("#requirement-requirementstate-button-error-message-in-card").show();
        }
    });
});

//Export close button in modal
$("#requirement-requirementstate-export-close-button").on("click", function (e) {
    $("#requirement-requirementstate-export-message").html("");
});

//Massive action Copy
$("#requirement-requirementstate-massive-action-copy").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let CopyType: string = "";
    let Body: Ajax = {};

    if ($("#requirement-requirementstate-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementstate-table-checkbox-for-row:checked").each(function () {
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

    RequirementStateModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            ValidateAndSearch();

            //Show OK message
            $("#requirement-requirementstate-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Rows copied successfully`);
            $("#requirement-requirementstate-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementstate-error-message-title").html("RequirementStateModel.Copy(CopyType).subscribe(...)");
            $("#requirement-requirementstate-error-message-text").html(err);
            $("#requirement-requirementstate-button-error-message-in-card").show();
        }
    });
});

//Massive action Delete
$("#requirement-requirementstate-massive-action-delete").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let DeleteType: string = "";
    let Body: Ajax = {};

    if ($("#requirement-requirementstate-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirementstate-table-checkbox-for-row:checked").each(function () {
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

    RequirementStateModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            ValidateAndSearch();

            //Show OK message
            $("#requirement-requirementstate-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Rows deleted successfully`);
            $("#requirement-requirementstate-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirementstate-error-message-title").html("RequirementStateModel.Copy(CopyType).subscribe(...)");
            $("#requirement-requirementstate-error-message-text").html(err);
            $("#requirement-requirementstate-button-error-message-in-card").show();
        }
    });
});