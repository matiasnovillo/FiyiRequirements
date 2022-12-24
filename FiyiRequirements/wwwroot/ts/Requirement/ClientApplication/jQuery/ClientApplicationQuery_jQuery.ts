//Import libraries to use
import { ClientApplicationModel, clientapplicationmodelQuery } from "../../ClientApplication/TsModels/ClientApplication_TsModel";
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

//Last modification on: 24/12/2022 6:47:42

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

class ClientApplicationQuery {
    static SelectAllPagedToHTML(request_clientapplicationmodelQuery: clientapplicationmodelQuery) {
        //Used for list view
        $(window).off("scroll");

        //Load some part of table
        var TableContent: string = `<thead class="thead-light">
    <tr>
        <th scope="col">
            <div>
                <input id="clientapplication-table-check-all" type="checkbox">
            </div>
        </th>
        <th scope="col">
            <button value="ClientApplicationId" class="btn btn-outline-secondary btn-sm" type="button">
                ClientApplicationId
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
            <button value="ClientId" class="btn btn-outline-secondary btn-sm" type="button">
                ClientId
            </button>
        </th>
        <th scope="col">
            <button value="ApplicationId" class="btn btn-outline-secondary btn-sm" type="button">
                ApplicationId
            </button>
        </th>
        
        <th scope="col"></th>
    </tr>
</thead>
<tbody>`;

        var ListContent: string = ``;

        ClientApplicationModel.SelectAllPaged(request_clientapplicationmodelQuery).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {

                        const response_clientapplicationQuery = newrow.response as clientapplicationmodelQuery;

                        //Set to default values if they are null
                        QueryString = response_clientapplicationQuery.QueryString ?? "";
                        ActualPageNumber = response_clientapplicationQuery.ActualPageNumber ?? 0;
                        RowsPerPage = response_clientapplicationQuery.RowsPerPage ?? 0;
                        SorterColumn = response_clientapplicationQuery.SorterColumn ?? "";
                        SortToggler = response_clientapplicationQuery.SortToggler ?? false;
                        TotalRows = response_clientapplicationQuery.TotalRows ?? 0;
                        TotalPages = response_clientapplicationQuery.TotalPages ?? 0;

                        //Query string
                        $("#requirement-clientapplication-query-string").attr("placeholder", `Search... (${TotalRows} records)`);
                        //Total pages of pagination
                        $("#requirement-clientapplication-total-pages-lg, #requirement-clientapplication-total-pages").html(TotalPages.toString());
                        //Actual page number of pagination
                        $("#requirement-clientapplication-actual-page-number-lg, #requirement-clientapplication-actual-page-number").html(ActualPageNumber.toString());
                        //If we are at the final of book disable next and last buttons in pagination
                        if (ActualPageNumber === TotalPages) {
                            $("#requirement-clientapplication-lnk-next-page-lg, #requirement-clientapplication-lnk-next-page").attr("disabled", "disabled");
                            $("#requirement-clientapplication-lnk-last-page-lg, #requirement-clientapplication-lnk-last-page").attr("disabled", "disabled");
                            $("#requirement-clientapplication-search-more-button-in-list").html("");
                        }
                        else {
                            $("#requirement-clientapplication-lnk-next-page-lg, #requirement-clientapplication-lnk-next-page").removeAttr("disabled");
                            $("#requirement-clientapplication-lnk-last-page-lg, #requirement-clientapplication-lnk-last-page").removeAttr("disabled");
                            //Scroll arrow for list view
                            $("#requirement-clientapplication-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                        }
                        //If we are at the begining of the book disable previous and first buttons in pagination
                        if (ActualPageNumber === 1) {
                            $("#requirement-clientapplication-lnk-previous-page-lg, #requirement-clientapplication-lnk-previous-page").attr("disabled", "disabled");
                            $("#requirement-clientapplication-lnk-first-page-lg, #requirement-clientapplication-lnk-first-page").attr("disabled", "disabled");
                        }
                        else {
                            $("#requirement-clientapplication-lnk-previous-page-lg, #requirement-clientapplication-lnk-previous-page").removeAttr("disabled");
                            $("#requirement-clientapplication-lnk-first-page-lg, #requirement-clientapplication-lnk-first-page").removeAttr("disabled");
                        }
                        //If book is empty set to default pagination values
                        if (response_clientapplicationQuery?.lstClientApplicationModel?.length === 0) {
                            $("#requirement-clientapplication-lnk-previous-page-lg, #requirement-clientapplication-lnk-previous-page").attr("disabled", "disabled");
                            $("#requirement-clientapplication-lnk-first-page-lg, #requirement-clientapplication-lnk-first-page").attr("disabled", "disabled");
                            $("#requirement-clientapplication-lnk-next-page-lg, #requirement-clientapplication-lnk-next-page").attr("disabled", "disabled");
                            $("#requirement-clientapplication-lnk-last-page-lg, #requirement-clientapplication-lnk-last-page").attr("disabled", "disabled");
                            $("#requirement-clientapplication-total-pages-lg, #requirement-clientapplication-total-pages").html("1");
                            $("#requirement-clientapplication-actual-page-number-lg, #requirement-clientapplication-actual-page-number").html("1");
                        }
                        //Read data book
                        response_clientapplicationQuery?.lstClientApplicationModel?.forEach(row => {

                            TableContent += `<tr>
    <!-- Checkbox -->
    <td>
        <div>
            <input class="clientapplication-table-checkbox-for-row" value="${row.ClientApplicationId}" type="checkbox">
        </div>
    </td>
    <!-- Data -->
    <td class="text-left text-light">
        <i class="fas fa-key"></i> ${row.ClientApplicationId}
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
        <strong>
            <i class="fas fa-key"></i> ${row.ClientId}
        </strong>
    </td>
    <td class="text-left">
        <strong>
            <i class="fas fa-key"></i> ${row.ApplicationId}
        </strong>
    </td>
    
    <!-- Actions -->
    <td class="text-right">
        <a class="btn btn-icon-only text-primary" href="/Requirement/PageClientApplicationNonQuery?ClientApplicationId=${row.ClientApplicationId}" role="button" data-toggle="tooltip" data-original-title="Edit">
            <i class="fas fa-edit"></i>
        </a>
        <div class="dropdown">
            <button class="btn btn-icon-only text-danger" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-trash"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button class="dropdown-item text-danger requirement-clientapplication-table-delete-button" value="${row.ClientApplicationId}" type="button">
                    <i class="fas fa-exclamation-triangle"></i> Yes, delete
                </button>
            </div>
        </div>
        <div class="dropdown">
            <button class="btn btn-sm btn-icon-only text-primary" href="#" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-ellipsis-v"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button type="button" class="dropdown-item requirement-clientapplication-table-copy-button" value="${row.ClientApplicationId}">
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
                           ClientApplicationId <i class="fas fa-key"></i> ${row.ClientApplicationId}
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
                           ClientId <i class="fas fa-key"></i> ${row.ClientId}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                           ApplicationId <i class="fas fa-key"></i> ${row.ApplicationId}
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
                            <div class="requirement-clientapplication-checkbox-list list-row-unchecked mb-2">
                                <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="check">
                                    <i class="fas fa-circle text-white"></i>
                                </a>
                            </div>
                            <input type="hidden" value="${row.ClientApplicationId}"/>
                            <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="/Requirement/PageClientApplicationNonQuery?ClientApplicationId=${row.ClientApplicationId}" role="button" data-toggle="tooltip" data-original-title="edit">
                                <i class="fas fa-edit text-primary"></i>
                            </a>
                            <div class="dropup">
                                <a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-ellipsis-v"></i>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                                    <button value="${row.ClientApplicationId}" class="dropdown-item text-primary requirement-clientapplication-list-copy-button" type="button">
                                        <i class="fas fa-copy"></i>&nbsp;Copy
                                    </button>
                                    <button value="${row.ClientApplicationId}" class="dropdown-item text-danger requirement-clientapplication-list-delete-button" type="button">
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
                            $("#requirement-clientapplication-body-and-head-table").html("");
                            $("#requirement-clientapplication-body-and-head-table").html(TableContent);
                        }
                        else {
                            //Used for list view
                            if (ScrollDownNSearchFlag) {
                                $("#requirement-clientapplication-body-list").append(ListContent);
                                ScrollDownNSearchFlag = false;
                            }
                            else {
                                //Clear list view
                                $("#requirement-clientapplication-body-list").html("");
                                $("#requirement-clientapplication-body-list").html(ListContent);
                            }
                            }
                    }
                    else {
                        //Show error message
                        $("#requirement-clientapplication-error-message-title").html("No registers found");
                        $("#requirement-clientapplication-error-message-text").html("The server did not found any register. HTTP code 204");
                        $("#requirement-clientapplication-button-error-message-in-card").show();
                    }
                },
                complete: () => {
                    //Execute ScrollDownNSearch function when the user scroll the page
                    $(window).on("scroll", ScrollDownNSearch);

                    //Add final content to TableContent
                    TableContent += `</tbody>
                                </table>`;

                    //Check button inside list view
                    $(".requirement-clientapplication-checkbox-list").on("click", function (e) {
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
                    $("#clientapplication-table-check-all").on("click", function (e) { 
                        //Toggler
                        if ($("tr td div input.clientapplication-table-checkbox-for-row").is(":checked")) {
                            $("tr td div input.clientapplication-table-checkbox-for-row").removeAttr("checked");
                        }
                        else {
                            $("tr td div input.clientapplication-table-checkbox-for-row").attr("checked", "checked");
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
                    $("#requirement-clientapplication-error-message-title").html("");
                    $("#requirement-clientapplication-error-message-text").html("");
                    $("#requirement-clientapplication-button-error-message-in-card").hide();

                    //Delete button in table and list
                    $("div.dropdown-menu button.requirement-clientapplication-table-delete-button, div.dropdown-menu button.requirement-clientapplication-list-delete-button").on("click", function (e) {
                        let ClientApplicationId = $(this).val();
                        ClientApplicationModel.DeleteByClientApplicationId(ClientApplicationId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                ValidateAndSearch();

                                //Show OK message
                                $("#requirement-clientapplication-button-error-message-in-card").hide();
                                $("#requirement-clientapplication-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Row deleted successfully`);
                                $("#requirement-clientapplication-button-ok-message-in-card").show();
                            },
                            error: err => {
                                //Related to error message
                                $("#requirement-clientapplication-error-message-title").html("ClientApplicationModel.DeleteByClientApplicationId(ClientApplicationId).subscribe(...)");
                                $("#requirement-clientapplication-error-message-text").html(err);
                                $("#requirement-clientapplication-button-error-message-in-card").show();
                            }
                        });
                    });

                    //Copy button in table and list
                    $("div.dropdown-menu button.requirement-clientapplication-table-copy-button, div.dropdown-menu button.requirement-clientapplication-list-copy-button").on("click", function (e) {
                        let ClientApplicationId = $(this).val();
                        ClientApplicationModel.CopyByClientApplicationId(ClientApplicationId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                ValidateAndSearch();

                                //Show OK message
                                $("#requirement-clientapplication-button-error-message-in-card").hide();
                                $("#requirement-clientapplication-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Row copied successfully`);
                                $("#requirement-clientapplication-button-ok-message-in-card").show();
                            },
                            error: err => {
                                //Show error message
                                $("#requirement-clientapplication-error-message-title").html("ClientApplicationModel.CopyByClientApplicationId(ClientApplicationId).subscribe(...)");
                                $("#requirement-clientapplication-error-message-text").html(err);
                                $("#requirement-clientapplication-button-error-message-in-card").show();
                            }
                        });
                    });
                },
                error: err => {
                    //Show error message
                    $("#requirement-clientapplication-error-message-title").html("ClientApplicationModel.SelectAllPaged(request_clientapplicationmodelQ).subscribe(...)");
                    $("#requirement-clientapplication-error-message-text").html(err);
                    $("#requirement-clientapplication-button-error-message-in-card").show();
                }
            });
    }
}

function ValidateAndSearch() {

    //Hide error and OK message button
    $("#requirement-clientapplication-button-error-message-in-card").hide();
    $("#requirement-clientapplication-button-ok-message-in-card").hide();

    var _clientapplicationmodelQuery: clientapplicationmodelQuery = {
        QueryString,
        ActualPageNumber,
        RowsPerPage,
        SorterColumn,
        SortToggler,
        TotalRows,
        TotalPages
    };

    ClientApplicationQuery.SelectAllPagedToHTML(_clientapplicationmodelQuery);
}

//LOAD EVENT
if ($("#requirement-clientapplication-title-page").html().includes("Query clientapplication")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "ClientApplicationId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#requirement-clientapplication-lnk-first-page-lg, #requirement-clientapplication-lnk-first-page").attr("disabled", "disabled");
    $("#requirement-clientapplication-lnk-previous-page-lg, #requirement-clientapplication-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#requirement-clientapplication-export-message").html("");
    $("#requirement-clientapplication-button-error-message-in-card").hide();
    $("#requirement-clientapplication-button-ok-message-in-card").hide();

    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#requirement-clientapplication-search-button")).on("click", function () {
    ValidateAndSearch();
});

//Query string
$("#requirement-clientapplication-query-string").on("change keyup input", function (e) {
    //If undefined, set QueryString to "" value
    QueryString = ($(this).val()?.toString()) ?? "" ;
    ValidateAndSearch();
});

//First page link in pagination
$("#requirement-clientapplication-lnk-first-page-lg, #requirement-clientapplication-lnk-first-page").on("click", function (e) {
    ActualPageNumber = 1;
    ValidateAndSearch();
});

//Previous page link in pagination
$("#requirement-clientapplication-lnk-previous-page-lg, #requirement-clientapplication-lnk-previous-page").on("click", function (e) {
    ActualPageNumber -= 1;
    ValidateAndSearch();
});

//Next page link in pagination
$("#requirement-clientapplication-lnk-next-page-lg, #requirement-clientapplication-lnk-next-page").on("click", function (e) {
    ActualPageNumber += 1;
    ValidateAndSearch();
});

//Last page link in pagination
$("#requirement-clientapplication-lnk-last-page-lg, #requirement-clientapplication-lnk-last-page").on("click", function (e) {
    ActualPageNumber = TotalPages;
    ValidateAndSearch();
});

//Table view button
$("#requirement-clientapplication-table-view-button").on("click", function (e) {
    $("#requirement-clientapplication-view-toggler").val("Table");
    ViewToggler = "Table";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear table view
    $("#requirement-clientapplication-body-and-head-table").html("");
    ValidateAndSearch();
});

//List view button
$("#requirement-clientapplication-list-view-button").on("click", function (e) {
    $("#requirement-clientapplication-view-toggler").val("List");
    ViewToggler = "List";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear list view
    $("#requirement-clientapplication-body-list").html("");
    ValidateAndSearch();
});

//Used to list view
function ScrollDownNSearch() {
    let WindowsTopDistance: number = $(window).scrollTop() ?? 0;
    let WindowsBottomDistance: number = ($(window).scrollTop() ?? 0) + ($(window).innerHeight() ?? 0);
    let CardsFooterTopPosition: number = $("#requirement-clientapplication-search-more-button-in-list").offset()?.top ?? 0;
    let CardsFooterBottomPosition: number = ($("#requirement-clientapplication-search-more-button-in-list").offset()?.top ?? 0) + ($("#requirement-clientapplication-search-more-button-in-list").outerHeight() ?? 0);

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
$("#requirement-clientapplication-export-as-pdf").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-clientapplication-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else{
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.clientapplication-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/ClientApplication/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-clientapplication-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for PDF file
            $("#requirement-clientapplication-export-message").html(`<a class="btn btn-icon btn-success" href="/PDFFiles/Requirement/ClientApplication/ClientApplication_${DateTimeNow.AjaxForString}.pdf" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-pdf"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-clientapplication-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-clientapplication-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-clientapplication-error-message-title").html("Rx.from(ajax.post('/api/Requirement/ClientApplication/1/ExportAsPDF/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-clientapplication-error-message-text").html(err);
            $("#requirement-clientapplication-button-error-message-in-card").show();
        }
    });
});

//Export as Excel button
$("#requirement-clientapplication-export-as-excel").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-clientapplication-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.clientapplication-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/ClientApplication/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-clientapplication-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for Excel file
            $("#requirement-clientapplication-export-message").html(`<a class="btn btn-icon btn-success" href="/ExcelFiles/Requirement/ClientApplication/ClientApplication_${DateTimeNow.AjaxForString}.xlsx" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-excel"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-clientapplication-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-clientapplication-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-clientapplication-error-message-title").html("Rx.from(ajax.post('/api/Requirement/ClientApplication/1/ExportAsExcel/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-clientapplication-error-message-text").html(err);
            $("#requirement-clientapplication-button-error-message-in-card").show();
        }
    });
});

//Export as CSV button
$("#requirement-clientapplication-export-as-csv").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#requirement-clientapplication-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.clientapplication-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/Requirement/ClientApplication/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#requirement-clientapplication-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //Show download button for CSV file
            $("#requirement-clientapplication-export-message").html(`<a class="btn btn-icon btn-success" href="/CSVFiles/Requirement/ClientApplication/ClientApplication_${DateTimeNow.AjaxForString}.csv" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-csv"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

            //Show OK message
            $("#requirement-clientapplication-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Conversion completed`);
            $("#requirement-clientapplication-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-clientapplication-error-message-title").html("Rx.from(ajax.post('/api/Requirement/ClientApplication/1/ExportAsCSV/' + ExportationType, Body, Header)).subscribe(...)");
            $("#requirement-clientapplication-error-message-text").html(err);
            $("#requirement-clientapplication-button-error-message-in-card").show();
        }
    });
});

//Export close button in modal
$("#requirement-clientapplication-export-close-button").on("click", function (e) {
    $("#requirement-clientapplication-export-message").html("");
});

//Massive action Copy
$("#requirement-clientapplication-massive-action-copy").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let CopyType: string = "";
    let Body: Ajax = {};

    if ($("#requirement-clientapplication-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.clientapplication-table-checkbox-for-row:checked").each(function () {
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

    ClientApplicationModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            ValidateAndSearch();

            //Show OK message
            $("#requirement-clientapplication-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Rows copied successfully`);
            $("#requirement-clientapplication-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-clientapplication-error-message-title").html("ClientApplicationModel.Copy(CopyType).subscribe(...)");
            $("#requirement-clientapplication-error-message-text").html(err);
            $("#requirement-clientapplication-button-error-message-in-card").show();
        }
    });
});

//Massive action Delete
$("#requirement-clientapplication-massive-action-delete").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let DeleteType: string = "";
    let Body: Ajax = {};

    if ($("#requirement-clientapplication-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.clientapplication-table-checkbox-for-row:checked").each(function () {
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

    ClientApplicationModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            ValidateAndSearch();

            //Show OK message
            $("#requirement-clientapplication-button-ok-message-in-card").html(`<strong>
                                                                    <i class="fas fa-check"></i>
                                                                </strong> Rows deleted successfully`);
            $("#requirement-clientapplication-button-ok-message-in-card").show();
        },
        error: err => {
            //Show error message
            $("#requirement-clientapplication-error-message-title").html("ClientApplicationModel.Copy(CopyType).subscribe(...)");
            $("#requirement-clientapplication-error-message-text").html(err);
            $("#requirement-clientapplication-button-error-message-in-card").show();
        }
    });
});