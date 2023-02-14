//Import libraries to use
import { MenuModel, menumodelQuery } from "../../Menu/TsModels/Menu_TsModel";
import * as $ from "jquery";
import * as Rx from "rxjs";
import { ajax } from "rxjs/ajax";
import { Ajax } from "../../../Library/Ajax";
import "bootstrap-notify";

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

//Last modification on: 14/02/2023 14:04:59

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

class MenuQuery {
    static SelectAllPagedToHTML(request_menumodelQuery: menumodelQuery) {
        //Used for list view
        $(window).off("scroll");

        //Load some part of table
        var TableContent: string = `<thead class="thead-light">
    <tr>
        <th scope="col">
            <div>
                <input id="menu-table-check-all" type="checkbox">
            </div>
        </th>
        <th scope="col">
            <button value="MenuId" class="btn btn-outline-secondary btn-sm" type="button">
                Menu ID
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
            <button value="Name" class="btn btn-outline-secondary btn-sm" type="button">
                Name
            </button>
        </th>
        <th scope="col">
            <button value="MenuFatherId" class="btn btn-outline-secondary btn-sm" type="button">
                Menu Father ID
            </button>
        </th>
        <th scope="col">
            <button value="Order" class="btn btn-outline-secondary btn-sm" type="button">
                Order
            </button>
        </th>
        <th scope="col">
            <button value="URLPath" class="btn btn-outline-secondary btn-sm" type="button">
                URL Path
            </button>
        </th>
        <th scope="col">
            <button value="IconURLPath" class="btn btn-outline-secondary btn-sm" type="button">
                Icon URL Path
            </button>
        </th>
        
        <th scope="col"></th>
    </tr>
</thead>
<tbody>`;

        var ListContent: string = ``;

        MenuModel.SelectAllPaged(request_menumodelQuery).subscribe(
            {
                next: newrow => {
                    //Only works when there is data available
                    if (newrow.status != 204) {

                        const response_menuQuery = newrow.response as menumodelQuery;

                        //Set to default values if they are null
                        QueryString = response_menuQuery.QueryString ?? "";
                        ActualPageNumber = response_menuQuery.ActualPageNumber ?? 0;
                        RowsPerPage = response_menuQuery.RowsPerPage ?? 0;
                        SorterColumn = response_menuQuery.SorterColumn ?? "";
                        SortToggler = response_menuQuery.SortToggler ?? false;
                        TotalRows = response_menuQuery.TotalRows ?? 0;
                        TotalPages = response_menuQuery.TotalPages ?? 0;

                        //Query string
                        $("#cmscore-menu-query-string").attr("placeholder", `Search... (${TotalRows} menues)`);
                        //Total pages of pagination
                        $("#cmscore-menu-total-pages-lg, #cmscore-menu-total-pages").html(TotalPages.toString());
                        //Actual page number of pagination
                        $("#cmscore-menu-actual-page-number-lg, #cmscore-menu-actual-page-number").html(ActualPageNumber.toString());
                        //If we are at the final of book disable next and last buttons in pagination
                        if (ActualPageNumber === TotalPages) {
                            $("#cmscore-menu-lnk-next-page-lg, #cmscore-menu-lnk-next-page").attr("disabled", "disabled");
                            $("#cmscore-menu-lnk-last-page-lg, #cmscore-menu-lnk-last-page").attr("disabled", "disabled");
                            $("#cmscore-menu-search-more-button-in-list").html("");
                        }
                        else {
                            $("#cmscore-menu-lnk-next-page-lg, #cmscore-menu-lnk-next-page").removeAttr("disabled");
                            $("#cmscore-menu-lnk-last-page-lg, #cmscore-menu-lnk-last-page").removeAttr("disabled");
                            //Scroll arrow for list view
                            $("#cmscore-menu-search-more-button-in-list").html("<i class='fas fa-2x fa-chevron-down'></i>");
                        }
                        //If we are at the begining of the book disable previous and first buttons in pagination
                        if (ActualPageNumber === 1) {
                            $("#cmscore-menu-lnk-previous-page-lg, #cmscore-menu-lnk-previous-page").attr("disabled", "disabled");
                            $("#cmscore-menu-lnk-first-page-lg, #cmscore-menu-lnk-first-page").attr("disabled", "disabled");
                        }
                        else {
                            $("#cmscore-menu-lnk-previous-page-lg, #cmscore-menu-lnk-previous-page").removeAttr("disabled");
                            $("#cmscore-menu-lnk-first-page-lg, #cmscore-menu-lnk-first-page").removeAttr("disabled");
                        }
                        //If book is empty set to default pagination values
                        if (response_menuQuery?.lstMenuModel?.length === 0) {
                            $("#cmscore-menu-lnk-previous-page-lg, #cmscore-menu-lnk-previous-page").attr("disabled", "disabled");
                            $("#cmscore-menu-lnk-first-page-lg, #cmscore-menu-lnk-first-page").attr("disabled", "disabled");
                            $("#cmscore-menu-lnk-next-page-lg, #cmscore-menu-lnk-next-page").attr("disabled", "disabled");
                            $("#cmscore-menu-lnk-last-page-lg, #cmscore-menu-lnk-last-page").attr("disabled", "disabled");
                            $("#cmscore-menu-total-pages-lg, #cmscore-menu-total-pages").html("1");
                            $("#cmscore-menu-actual-page-number-lg, #cmscore-menu-actual-page-number").html("1");
                        }
                        //Read data book
                        response_menuQuery?.lstMenuModel?.forEach(row => {

                            TableContent += `<tr>
    <!-- Checkbox -->
    <td>
        <div>
            <input class="menu-table-checkbox-for-row" value="${row.MenuId}" type="checkbox">
        </div>
    </td>
    <!-- Data -->
    <td class="text-left text-light">
        <i class="fas fa-key"></i> ${row.MenuId}
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
            </i> ${row.Name}
        </strong>
    </td>
    <td class="text-left">
        <strong><i class="fas fa-divide">
            </i> ${row.MenuFatherId}
        </strong>
    </td>
    <td class="text-left">
        <strong><i class="fas fa-divide">
            </i> ${row.Order}
        </strong>
    </td>
    <td class="text-left">
        <a href="${row.URLPath}" target="_blank">
            <strong>
                <i class="fas fa-globe"></i> ${row.URLPath}
            </strong>
        </a>
    </td>
    <td class="text-left">
        <a href="${row.IconURLPath}" target="_blank">
            <strong>
                <i class="fas fa-globe"></i> ${row.IconURLPath}
            </strong>
        </a>
    </td>
    
    <!-- Actions -->
    <td class="text-right">
        <a class="btn btn-icon-only text-primary" href="/CMSCore/MenuNonQueryPage?MenuId=${row.MenuId}" role="button" data-toggle="tooltip" data-original-title="Edit">
            <i class="fas fa-edit"></i>
        </a>
        <div class="dropdown">
            <button class="btn btn-icon-only text-danger" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-trash"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button class="dropdown-item text-danger cmscore-menu-table-delete-button" value="${row.MenuId}" type="button">
                    <i class="fas fa-exclamation-triangle"></i> Yes, delete
                </button>
            </div>
        </div>
        <div class="dropdown">
            <button class="btn btn-sm btn-icon-only text-primary" href="#" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="fas fa-ellipsis-v"></i>
            </button>
            <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                <button type="button" class="dropdown-item cmscore-menu-table-copy-button" value="${row.MenuId}">
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
                           Menu ID <i class="fas fa-key"></i> ${row.MenuId}
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
                           Name <i class="fas fa-font"></i> ${row.Name}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                            Menu Father ID <i class="fas fa-divide"></i> ${row.MenuFatherId}
                        </span>
                        <br/>
                        <span class="text-white mb-4">
                            Order <i class="fas fa-divide"></i> ${row.Order}
                        </span>
                        <br/>
                        <span class="mb-4">
                            <a href="${row.URLPath}" style="color:#FFFFFF" target="_blank">
                               URL Path <i class="fas fa-globe"></i> ${row.URLPath}
                            </a>
                        </span>
                        <br/>
                        <span class="mb-4">
                            <a href="${row.IconURLPath}" style="color:#FFFFFF" target="_blank">
                               Icon URL Path <i class="fas fa-globe"></i> ${row.IconURLPath}
                            </a>
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
                            <div class="mb-2">
                                <a class="cmscore-menu-checkbox-list list-row-unchecked icon icon-shape bg-white icon-sm rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="tooltip" data-original-title="Check">
                                    <i class="fas fa-circle text-white"></i>
                                </a>
                                <input type="hidden" value="${row.MenuId}"/>
                            </div>
                            <a class="icon icon-shape bg-white icon-sm rounded-circle shadow" href="/CMSCore/MenuNonQueryPage?MenuId=${row.MenuId}" role="button" data-toggle="tooltip" data-original-title="edit">
                                <i class="fas fa-edit text-primary"></i>
                            </a>
                            <div class="dropup">
                                <a class="icon icon-shape bg-white icon-sm text-primary rounded-circle shadow" href="javascript:void(0)" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-ellipsis-v"></i>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right dropdown-menu-arrow">
                                    <button value="${row.MenuId}" class="dropdown-item text-primary cmscore-menu-list-copy-button" type="button">
                                        <i class="fas fa-copy"></i>&nbsp;Copy
                                    </button>
                                    <button value="${row.MenuId}" class="dropdown-item text-danger cmscore-menu-list-delete-button" type="button">
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
                            $("#cmscore-menu-body-and-head-table").html("");
                            $("#cmscore-menu-body-and-head-table").html(TableContent);
                        }
                        else {
                            //Used for list view
                            if (ScrollDownNSearchFlag) {
                                $("#cmscore-menu-body-list").append(ListContent);
                                ScrollDownNSearchFlag = false;
                            }
                            else {
                                //Clear list view
                                $("#cmscore-menu-body-list").html("");
                                $("#cmscore-menu-body-list").html(ListContent);
                            }
                            }
                    }
                    else {
                        //ERROR
                        // @ts-ignore
                        $.notify({ icon: "fas fa-exclamation-triangle", message: "No registers found" }, { type: "warning", placement: { from: "bottom", align: "center" } });
                    }
                },
                complete: () => {
                    //Execute ScrollDownNSearch function when the user scroll the page
                    $(window).on("scroll", ScrollDownNSearch);

                    //Add final content to TableContent
                    TableContent += `</tbody>
                                </table>`;

                    //Check button inside list view
                    $(".cmscore-menu-checkbox-list").on("click", function (e) {
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
                    $("#menu-table-check-all").on("click", function (e) { 
                        //Toggler
                        if ($("tr td div input.menu-table-checkbox-for-row").is(":checked")) {
                            $("tr td div input.menu-table-checkbox-for-row").removeAttr("checked");
                        }
                        else {
                            $("tr td div input.menu-table-checkbox-for-row").attr("checked", "checked");
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

                    //Delete button in table and list
                    $("div.dropdown-menu button.cmscore-menu-table-delete-button, div.dropdown-menu button.cmscore-menu-list-delete-button").on("click", function (e) {
                        let MenuId = $(this).val();
                        MenuModel.DeleteByMenuId(MenuId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                //SUCCESS
                                // @ts-ignore
                                $.notify({ icon: "fas fa-check", message: "Row deleted successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });

                                ValidateAndSearch();
                            },
                            error: err => {
                                //ERROR
                                // @ts-ignore
                                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                                console.log(err);
                            }
                        });
                    });

                    //Copy button in table and list
                    $("div.dropdown-menu button.cmscore-menu-table-copy-button, div.dropdown-menu button.cmscore-menu-list-copy-button").on("click", function (e) {
                        let MenuId = $(this).val();
                        MenuModel.CopyByMenuId(MenuId).subscribe({
                            next: newrow => {
                            },
                            complete: () => {
                                //SUCCESS
                                // @ts-ignore
                                $.notify({ icon: "fas fa-check", message: "Row copied successfully" }, { type: "success", placement: { from: "bottom", align: "center" } });

                                ValidateAndSearch();
                            },
                            error: err => {
                                //ERROR
                                // @ts-ignore
                                $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to copy data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                                console.log(err);
                            }
                        });
                    });
                },
                error: err => {
                    //ERROR
                    // @ts-ignore
                    $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to get data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
                    console.log(err);
                }
            });
    }
}

function ValidateAndSearch() {

    var _menumodelQuery: menumodelQuery = {
        QueryString,
        ActualPageNumber,
        RowsPerPage,
        SorterColumn,
        SortToggler,
        TotalRows,
        TotalPages
    };

    MenuQuery.SelectAllPagedToHTML(_menumodelQuery);
}

//LOAD EVENT
if ($("#cmscore-menu-title-page").html().includes("Query menu")) {
    //Set to default values
    QueryString = "";
    ActualPageNumber = 1;
    RowsPerPage = 50;
    SorterColumn = "MenuId";
    SortToggler = false;
    TotalRows = 0;
    TotalPages = 0;
    ViewToggler = "List";
    //Disable first and previous links in pagination
    $("#cmscore-menu-lnk-first-page-lg, #cmscore-menu-lnk-first-page").attr("disabled", "disabled");
    $("#cmscore-menu-lnk-previous-page-lg, #cmscore-menu-lnk-previous-page").attr("disabled", "disabled");
    //Hide messages
    $("#cmscore-menu-export-message").html("");

    ValidateAndSearch();
}
//CLICK, SCROLL AND KEYBOARD EVENTS
//Search button
$($("#cmscore-menu-search-button")).on("click", function () {
    ValidateAndSearch();
});

//Query string
$("#cmscore-menu-query-string").on("change keyup input", function (e) {
    //If undefined, set QueryString to "" value
    QueryString = ($(this).val()?.toString()) ?? "" ;
    ValidateAndSearch();
});

//First page link in pagination
$("#cmscore-menu-lnk-first-page-lg, #cmscore-menu-lnk-first-page").on("click", function (e) {
    ActualPageNumber = 1;
    ValidateAndSearch();
});

//Previous page link in pagination
$("#cmscore-menu-lnk-previous-page-lg, #cmscore-menu-lnk-previous-page").on("click", function (e) {
    ActualPageNumber -= 1;
    ValidateAndSearch();
});

//Next page link in pagination
$("#cmscore-menu-lnk-next-page-lg, #cmscore-menu-lnk-next-page").on("click", function (e) {
    ActualPageNumber += 1;
    ValidateAndSearch();
});

//Last page link in pagination
$("#cmscore-menu-lnk-last-page-lg, #cmscore-menu-lnk-last-page").on("click", function (e) {
    ActualPageNumber = TotalPages;
    ValidateAndSearch();
});

//Table view button
$("#cmscore-menu-table-view-button").on("click", function (e) {
    $("#cmscore-menu-view-toggler").val("Table");
    ViewToggler = "Table";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear table view
    $("#cmscore-menu-body-and-head-table").html("");
    ValidateAndSearch();
});

//List view button
$("#cmscore-menu-list-view-button").on("click", function (e) {
    $("#cmscore-menu-view-toggler").val("List");
    ViewToggler = "List";
    //Reset some values to default
    ActualPageNumber = 1;
    //Clear list view
    $("#cmscore-menu-body-list").html("");
    ValidateAndSearch();
});

//Used to list view
function ScrollDownNSearch() {
    let WindowsTopDistance: number = $(window).scrollTop() ?? 0;
    let WindowsBottomDistance: number = ($(window).scrollTop() ?? 0) + ($(window).innerHeight() ?? 0);
    let CardsFooterTopPosition: number = $("#cmscore-menu-search-more-button-in-list").offset()?.top ?? 0;
    let CardsFooterBottomPosition: number = ($("#cmscore-menu-search-more-button-in-list").offset()?.top ?? 0) + ($("#cmscore-menu-search-more-button-in-list").outerHeight() ?? 0);

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
$("#cmscore-menu-export-as-pdf").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#cmscore-menu-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else{
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.menu-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/CMSCore/Menu/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#cmscore-menu-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for PDF file
            $("#cmscore-menu-export-message").html(`<a class="btn btn-icon btn-success" href="/PDFFiles/CMSCore/Menu/Menu_${DateTimeNow.AjaxForString}.pdf" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-pdf"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);

        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});

//Export as Excel button
$("#cmscore-menu-export-as-excel").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#cmscore-menu-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.menu-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/CMSCore/Menu/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#cmscore-menu-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for Excel file
            $("#cmscore-menu-export-message").html(`<a class="btn btn-icon btn-success" href="/ExcelFiles/CMSCore/Menu/Menu_${DateTimeNow.AjaxForString}.xlsx" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-excel"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});

//Export as CSV button
$("#cmscore-menu-export-as-csv").on("click", function (e) {
    //There are two exportation types, All and JustChecked
    let ExportationType: string = "";
    let DateTimeNow: Ajax;
    let Body: Ajax = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    let Header: any = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };

    if ($("#cmscore-menu-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.menu-table-checkbox-for-row:checked").each(function () {
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

    Rx.from(ajax.post("/api/CMSCore/Menu/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: newrow => {
            $("#cmscore-menu-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response as Ajax;
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Conversion completed" }, { type: "success", placement: { from: "bottom", align: "center" } });

            //Show download button for CSV file
            $("#cmscore-menu-export-message").html(`<a class="btn btn-icon btn-success" href="/CSVFiles/CMSCore/Menu/Menu_${DateTimeNow.AjaxForString}.csv" type="button" download>
                                            <span class="btn-inner--icon"><i class="fas fa-file-csv"></i></span>
                                            <span class="btn-inner--text">Download</span>
                                        </a>`);
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to convert" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});

//Export close button in modal
$("#cmscore-menu-export-close-button").on("click", function (e) {
    $("#cmscore-menu-export-message").html("");
});

//Massive action Copy
$("#cmscore-menu-massive-action-copy").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let CopyType: string = "";
    let Body: Ajax = {};

    if ($("#cmscore-menu-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.menu-table-checkbox-for-row:checked").each(function () {
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

    MenuModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Completed copy" }, { type: "success", placement: { from: "bottom", align: "center" } });

            ValidateAndSearch();
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to copy" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});

//Massive action Delete
$("#cmscore-menu-massive-action-delete").on("click", function (e) {
    //There are two deletion types, All and JustChecked
    let DeleteType: string = "";
    let Body: Ajax = {};

    if ($("#cmscore-menu-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        let CheckedRows = new Array();

        if (ViewToggler == "Table") {
            $("tr td div input.menu-table-checkbox-for-row:checked").each(function () {
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

    MenuModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: newrow => {
        },
        complete: () => {
            //SUCCESS
            // @ts-ignore
            $.notify({ icon: "fas fa-check", message: "Completed deletion" }, { type: "success", placement: { from: "bottom", align: "center" } });

            ValidateAndSearch();
        },
        error: err => {
            //ERROR
            // @ts-ignore
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to delete" }, { type: "danger", placement: { from: "bottom", align: "center" } });
            console.log(err);
        }
    });
});