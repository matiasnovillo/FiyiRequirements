//Import libraries to use
import { RequirementModel, requirementmodelQuery } from "../../Requirement/TsModels/Requirement_TsModel";
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

//Last modification on: 27/12/2022 20:52:58

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

class RequirementQuery {
    static SelectAllPagedToHTML(request_requirementmodelQuery: requirementmodelQuery) {
        //Used for list view
        $(window).off("scroll");

        //Load some part of table
        var TableContent: string = `<thead class="thead-light">
    <tr>
        <th scope="col">
            <div>
                <input id="requirement-table-check-all" type="checkbox">
            </div>
        </th>
        <th scope="col">
            <button value="RequirementId" class="btn btn-outline-secondary btn-sm" type="button">
                Requirement ID
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
            <button value="RequirementStateId" class="btn btn-outline-secondary btn-sm" type="button">
                State
            </button>
        </th>
        <th scope="col">
            <button value="RequirementPriorityId" class="btn btn-outline-secondary btn-sm" type="button">
                Priority
            </button>
        </th>
        <th scope="col">
            <button value="UserEmployeeId" class="btn btn-outline-secondary btn-sm" type="button">
                Employee
            </button>
        </th>
        
        <th scope="col"></th>
    </tr>
</thead>
<tbody>`;

        var ListContent: string = ``;

        RequirementModel.SelectAllPaged(request_requirementmodelQuery).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {

                        const response_requirementQuery = newrow.response as requirementmodelQuery;

                        //Set to default values if they are null
                        QueryString = response_requirementQuery.QueryString ?? "";
                        ActualPageNumber = response_requirementQuery.ActualPageNumber ?? 0;
                        RowsPerPage = response_requirementQuery.RowsPerPage ?? 0;
                        SorterColumn = response_requirementQuery.SorterColumn ?? "";
                        SortToggler = response_requirementQuery.SortToggler ?? false;
                        TotalRows = response_requirementQuery.TotalRows ?? 0;
                        TotalPages = response_requirementQuery.TotalPages ?? 0;

                        //Query string
                        $("#requirement-requirement-query-string").attr("placeholder", `Search... (${TotalRows} requirements)`);
                        //Total pages of pagination
                        $("#requirement-requirement-total-pages-lg, #requirement-requirement-total-pages").html(TotalPages.toString());
                        //Actual page number of pagination
                        $("#requirement-requirement-actual-page-number-lg, #requirement-requirement-actual-page-number").html(ActualPageNumber.toString());
                        //If we are at the final of book disable next and last buttons in pagination
                        if (ActualPageNumber === TotalPages) {
                            $("#requirement-requirement-lnk-next-page-lg, #requirement-requirement-lnk-next-page").attr("disabled", "disabled");
                            $("#requirement-requirement-lnk-last-page-lg, #requirement-requirement-lnk-last-page").attr("disabled", "disabled");
                            $("#requirement-requirement-search-more-button-in-list").html("");
                        }
                        else {
                            $("#requirement-requirement-lnk-next-page-lg, #requirement-requirement-lnk-next-page").removeAttr("disabled");
                            $("#requirement-requirement-lnk-last-page-lg, #requirement-requirement-lnk-last-page").removeAttr("disabled");
                            //Scroll arrow for list view
                            $("#requirement-requirement-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                        }
                        //If we are at the begining of the book disable previous and first buttons in pagination
                        if (ActualPageNumber === 1) {
                            $("#requirement-requirement-lnk-previous-page-lg, #requirement-requirement-lnk-previous-page").attr("disabled", "disabled");
                            $("#requirement-requirement-lnk-first-page-lg, #requirement-requirement-lnk-first-page").attr("disabled", "disabled");
                        }
                        else {
                            $("#requirement-requirement-lnk-previous-page-lg, #requirement-requirement-lnk-previous-page").removeAttr("disabled");
                            $("#requirement-requirement-lnk-first-page-lg, #requirement-requirement-lnk-first-page").removeAttr("disabled");
                        }
                        //If book is empty set to default pagination values
                        if (response_requirementQuery?.lstRequirementModel?.length === 0) {
                            $("#requirement-requirement-lnk-previous-page-lg, #requirement-requirement-lnk-previous-page").attr("disabled", "disabled");
                            $("#requirement-requirement-lnk-first-page-lg, #requirement-requirement-lnk-first-page").attr("disabled", "disabled");
                            $("#requirement-requirement-lnk-next-page-lg, #requirement-requirement-lnk-next-page").attr("disabled", "disabled");
                            $("#requirement-requirement-lnk-last-page-lg, #requirement-requirement-lnk-last-page").attr("disabled", "disabled");
                            $("#requirement-requirement-total-pages-lg, #requirement-requirement-total-pages").html("1");
                            $("#requirement-requirement-actual-page-number-lg, #requirement-requirement-actual-page-number").html("1");
                        }
                        //Read data book
                        response_requirementQuery?.lstRequirementModel?.forEach(row => {

                            TableContent += `<tr>
    <!-- Checkbox -->
    <td>
        <div>
            <input class="requirement-table-checkbox-for-row" value="${row.RequirementId}" type="checkbox">
        </div>
    </td>
    <!-- Data -->
    <td class="text-left text-light">
        <i class="fas fa-key"></i> ${row.RequirementId}
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
            <i class="fas fa-key"></i> ${row.RequirementStateIdName}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.RequirementPriorityIdName}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.UserEmployeeIdFantasyName}
        </strong>
    </td>
    
    <!-- Actions -->
    <td class="text-right">
        <a class="btn btn-icon-only text-primary" href="/Requirement/PageRequirementNonQuery?RequirementId=${row.RequirementId}" role="button" data-toggle="tooltip" data-original-title="Edit">
            <i class="fas fa-edit"></i>
        </a>
        <div class="dropdown">
            <button class="btn btn-icon-only text-danger" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-trash"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button class="dropdown-item text-danger requirement-requirement-table-delete-button" value="${row.RequirementId}" type="button">
                    <i class="fas fa-exclamation-triangle"></i> Yes, delete
                </button>
            </div>
        </div>
        <div class="dropdown">
            <button class="btn btn-sm btn-icon-only text-primary" href="#" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-ellipsis-v"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button type="button" class="dropdown-item requirement-requirement-table-copy-button" value="${row.RequirementId}">
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
                           Requirement ID <i class="fas fa-key"></i> ${row.RequirementId}
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
                           State <i class="fas fa-key"></i> ${row.RequirementStateIdName}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Priority <i class="fas fa-key"></i> ${row.RequirementPriorityIdName}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           Employee <i class="fas fa-key"></i> ${row.UserEmployeeIdFantasyName}
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
                            <div class="requirement-requirement-checkbox-list list-row-unchecked mb-2">
                                <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                    <i class="fas fa-circle text-white"></i>
                                </a>
                            </div>
                            <input type="hidden" value="${row.RequirementId}"/>
                            <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="/Requirement/PageRequirementNonQuery?RequirementId=${row.RequirementId}" role="button" data-toggle="tooltip" data-original-title="edit">
                                <i class="fas fa-edit text-primary"></i>
                            </a>
                            <div class="dropup">
                                <a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-ellipsis-v"></i>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                                    <button value="${row.RequirementId}" class="dropdown-item text-primary requirement-requirement-list-copy-button" type="button">
                                        <i class="fas fa-copy"></i>&nbsp;Copy
                                    </button>
                                    <button value="${row.RequirementId}" class="dropdown-item text-danger requirement-requirement-list-delete-button" type="button">
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
                            $("#requirement-requirement-body-and-head-table").html("");
                            $("#requirement-requirement-body-and-head-table").html(TableContent);
                        }
                        else {
                            //Used for list view
                            if (ScrollDownNSearchFlag) {
                                $("#requirement-requirement-body-list").append(ListContent);
                                ScrollDownNSearchFlag = false;
                            }
                            else {
                                //Clear list view
                                $("#requirement-requirement-body-list").html("");
                                $("#requirement-requirement-body-list").html(ListContent);
                            }
                            }
                    }
                    else {
                        //Show error message
                        $("#requirement-requirement-error-message-title").html("No registers found");
                        $("#requirement-requirement-error-message-text").html("The server did not found any register. HTTP code 204");
                        $("#requirement-requirement-button-error-message-in-card").show();
                    }
                },
                complete: () => {
                    //Execute ScrollDownNSearch function when the user scroll the page
                    $(window).on("scroll", ScrollDownNSearch);

                    //Add final content to TableContent
                    TableContent += `</tbody>
                                </table>`;

                    //Check button inside list view
                    $(".requirement-requirement-checkbox-list").on("click", function (e) {
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
                    $("#requirement-table-check-all").on("click", function (e) { 
                        //Toggler
                        if ($("tr td div input.requirement-table-checkbox-for-row").is(":checked")) {
                            $("tr td div input.requirement-table-checkbox-for-row").removeAttr("checked");
                        }
                        else {
                            $("tr td div input.requirement-table-checkbox-for-row").attr("checked", "checked");
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
                    $("#requirement-requirement-error-message-title").html("");
                    $("#requirement-requirement-error-message-text").html("");
                    $("#requirement-requirement-button-error-message-in-card").hide();

                    //Delete button in table and list
                    $("div.dropdown-menu button.requirement-requirement-table-delete-button, div.dropdown-menu button.requirement-requirement-list-delete-button").on("click", function (e) {
                        let RequirementId = $(this).val();
                        RequirementModel.DeleteByRequirementId(RequirementId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                ValidateAndSearch();

                                //Show OK message
                                $("#requirement-requirement-button-error-message-in-card").hide();
                                $("#requirement-requirement-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Row deleted successfully`);
                                $("#requirement-requirement-button-ok-message-in-card").show();
                            },
                            error: err => {
                                //Related to error message
                                $("#requirement-requirement-error-message-title").html("RequirementModel.DeleteByRequirementId(RequirementId).subscribe(...)");
                                $("#requirement-requirement-error-message-text").html(err);
                                $("#requirement-requirement-button-error-message-in-card").show();
                            }
                        });
                    });

                    //Copy button in table and list
                    $("div.dropdown-menu button.requirement-requirement-table-copy-button, div.dropdown-menu button.requirement-requirement-list-copy-button").on("click", function (e) {
                        let RequirementId = $(this).val();
                        RequirementModel.CopyByRequirementId(RequirementId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                ValidateAndSearch();

                                //Show OK message
                                $("#requirement-requirement-button-error-message-in-card").hide();
                                $("#requirement-requirement-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Row copied successfully`);
                                $("#requirement-requirement-button-ok-message-in-card").show();
                            },
                            error: err => {
                                //Show error message
                                $("#requirement-requirement-error-message-title").html("RequirementModel.CopyByRequirementId(RequirementId).subscribe(...)");
                                $("#requirement-requirement-error-message-text").html(err);
                                $("#requirement-requirement-button-error-message-in-card").show();
                            }
                        });
                    });
                },
                error: err => {
                    //Show error message
                    $("#requirement-requirement-error-message-title").html("RequirementModel.SelectAllPaged(request_requirementmodelQ).subscribe(...)");
                    $("#requirement-requirement-error-message-text").html(err);
                    $("#requirement-requirement-button-error-message-in-card").show();
                }
            });
    }
}

function ValidateAndSearch() {

    //Hide error and OK message button
    $("#requirement-requirement-button-error-message-in-card").hide();
    $("#requirement-requirement-button-ok-message-in-card").hide();

    var _requirementmodelQuery: requirementmodelQuery = {
        QueryString,
        ActualPageNumber,
        RowsPerPage,
        SorterColumn,
        SortToggler,
        TotalRows,
        TotalPages
    };

    RequirementQuery.SelectAllPagedToHTML(_requirementmodelQuery);
}

//LOAD EVENT
if ($("#requirement-requirement-title-page").html().includes("Query requirement")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "RequirementId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#requirement-requirement-lnk-first-page-lg, #requirement-requirement-lnk-first-page").attr("disabled", "disabled");
    $("#requirement-requirement-lnk-previous-page-lg, #requirement-requirement-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#requirement-requirement-export-message").html("");
    $("#requirement-requirement-button-error-message-in-card").hide();
    $("#requirement-requirement-button-ok-message-in-card").hide();

    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#requirement-requirement-search-button")).on("click", function () {
    ValidateAndSearch();
});

//Query string
$("#requirement-requirement-query-string").on("change keyup input", function (e) {
    //If undefined, set QueryString to "" value
    QueryString = ($(this).val()?.toString()) ?? "" ;
    ValidateAndSearch();
});

//First page link in pagination
$("#requirement-requirement-lnk-first-page-lg, #requirement-requirement-lnk-first-page").on("click", function (e) {
    ActualPageNumber = 1;
    ValidateAndSearch();
});

//Previous page link in pagination
$("#requirement-requirement-lnk-previous-page-lg, #requirement-requirement-lnk-previous-page").on("click", function (e) {
    ActualPageNumber -= 1;
    ValidateAndSearch();
});

//Next page link in pagination
$("#requirement-requirement-lnk-next-page-lg, #requirement-requirement-lnk-next-page").on("click", function (e) {
    ActualPageNumber += 1;
    ValidateAndSearch();
});

//Last page link in pagination
$("#requirement-requirement-lnk-last-page-lg, #requirement-requirement-lnk-last-page").on("click", function (e) {
    ActualPageNumber = TotalPages;
    ValidateAndSearch();
});

//Table view button
$("#requirement-requirement-table-view-button").on("click", function (e) {
    $("#requirement-requirement-view-toggler").val("Table");
    ViewToggler = "Table";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear table view
    $("#requirement-requirement-body-and-head-table").html("");
    ValidateAndSearch();
});

//List view button
$("#requirement-requirement-list-view-button").on("click", function (e) {
    $("#requirement-requirement-view-toggler").val("List");
    ViewToggler = "List";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear list view
    $("#requirement-requirement-body-list").html("");
    ValidateAndSearch();
});

//Used to list view
function ScrollDownNSearch() {
    let WindowsTopDistance: number = $(window).scrollTop() ?? 0;
    let WindowsBottomDistance: number = ($(window).scrollTop() ?? 0) + ($(window).innerHeight() ?? 0);
    let CardsFooterTopPosition: number = $("#requirement-requirement-search-more-button-in-list").offset()?.top ?? 0;
    let CardsFooterBottomPosition: number = ($("#requirement-requirement-search-more-button-in-list").offset()?.top ?? 0) + ($("#requirement-requirement-search-more-button-in-list").outerHeight() ?? 0);

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
$("#requirement-requirement-export-as-pdf").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirement-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else{
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/Requirement/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirement-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for PDF file
            $("#requirement-requirement-export-message").html(`<a class="btn btn-icon btn-success" href="/PDFFiles/Requirement/Requirement/Requirement_${DateTimeNow.AjaxForString}.pdf" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-pdf"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirement-error-message-title").html("Rx.from(ajax.post('/api/Requirement/Requirement/1/ExportAsPDF/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirement-error-message-text").html(err);
            $("#requirement-requirement-button-error-message-in-card").show();
        }
    });
});

//Export as Excel button
$("#requirement-requirement-export-as-excel").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirement-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/Requirement/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirement-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for Excel file
            $("#requirement-requirement-export-message").html(`<a class="btn btn-icon btn-success" href="/ExcelFiles/Requirement/Requirement/Requirement_${DateTimeNow.AjaxForString}.xlsx" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-excel"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirement-error-message-title").html("Rx.from(ajax.post('/api/Requirement/Requirement/1/ExportAsExcel/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirement-error-message-text").html(err);
            $("#requirement-requirement-button-error-message-in-card").show();
        }
    });
});

//Export as CSV button
$("#requirement-requirement-export-as-csv").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-requirement-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/Requirement/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-requirement-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for CSV file
            $("#requirement-requirement-export-message").html(`<a class="btn btn-icon btn-success" href="/CSVFiles/Requirement/Requirement/Requirement_${DateTimeNow.AjaxForString}.csv" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-csv"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirement-error-message-title").html("Rx.from(ajax.post('/api/Requirement/Requirement/1/ExportAsCSV/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-requirement-error-message-text").html(err);
            $("#requirement-requirement-button-error-message-in-card").show();
        }
    });
});

//Export close button in modal
$("#requirement-requirement-export-close-button").on("click", function (e) {
    $("#requirement-requirement-export-message").html("");
});

//Massive action Copy
$("#requirement-requirement-massive-action-copy").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let CopyType: string = "";
    let Body: Ajax = {};

    if ($("#requirement-requirement-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
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

    RequirementModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            ValidateAndSearch();

            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Rows copied successfully`);
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirement-error-message-title").html("RequirementModel.Copy(CopyType).subscribe(...)");
            $("#requirement-requirement-error-message-text").html(err);
            $("#requirement-requirement-button-error-message-in-card").show();
        }
    });
});

//Massive action Delete
$("#requirement-requirement-massive-action-delete").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let DeleteType: string = "";
    let Body: Ajax = {};

    if ($("#requirement-requirement-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
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

    RequirementModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            ValidateAndSearch();

            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Rows deleted successfully`);
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-requirement-error-message-title").html("RequirementModel.Copy(CopyType).subscribe(...)");
            $("#requirement-requirement-error-message-text").html(err);
            $("#requirement-requirement-button-error-message-in-card").show();
        }
    });
});