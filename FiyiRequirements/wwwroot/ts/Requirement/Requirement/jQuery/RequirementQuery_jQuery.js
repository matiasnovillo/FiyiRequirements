"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//Import libraries to use
var Requirement_TsModel_1 = require("../../Requirement/TsModels/Requirement_TsModel");
var $ = require("jquery");
var Rx = require("rxjs");
var ajax_1 = require("rxjs/ajax");
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
var LastTopDistance = 0;
var QueryString = "";
var ActualPageNumber = 1;
var RowsPerPage = 50;
var SorterColumn = "";
var SortToggler = false;
var TotalPages = 0;
var TotalRows = 0;
var ViewToggler = "List";
var ScrollDownNSearchFlag = false;
var RequirementQuery = /** @class */ (function () {
    function RequirementQuery() {
    }
    RequirementQuery.SelectAllPagedToHTML = function (request_requirementmodelQuery) {
        //Used for list view
        $(window).off("scroll");
        //Load some part of table
        var TableContent = "<thead class=\"thead-light\">\n    <tr>\n        <th scope=\"col\">\n            <div>\n                <input id=\"requirement-table-check-all\" type=\"checkbox\">\n            </div>\n        </th>\n        <th scope=\"col\">\n            <button value=\"RequirementId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                RequirementId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"Active\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                Active\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"DateTimeCreation\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                DateTimeCreation\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"DateTimeLastModification\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                DateTimeLastModification\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"UserCreationId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                UserCreationId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"UserLastModificationId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                UserLastModificationId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"Title\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                Title\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"Body\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                Body\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"RequirementStateId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                RequirementStateId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"RequirementPriorityId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                RequirementPriorityId\n            </button>\n        </th>\n        <th scope=\"col\">\n            <button value=\"UserEmployeeId\" class=\"btn btn-outline-secondary btn-sm\" type=\"button\">\n                UserEmployeeId\n            </button>\n        </th>\n        \n        <th scope=\"col\"></th>\n    </tr>\n</thead>\n<tbody>";
        var ListContent = "";
        Requirement_TsModel_1.RequirementModel.SelectAllPaged(request_requirementmodelQuery).subscribe({
            next: function (newrow) {
                var _a, _b, _c, _d, _e, _f, _g, _h, _j;
                //Only works when there is data available
                if (newrow.status != 204) {
                    var response_requirementQuery = newrow.response;
                    //Set to default values if they are null
                    QueryString = (_a = response_requirementQuery.QueryString) !== null && _a !== void 0 ? _a : "";
                    ActualPageNumber = (_b = response_requirementQuery.ActualPageNumber) !== null && _b !== void 0 ? _b : 0;
                    RowsPerPage = (_c = response_requirementQuery.RowsPerPage) !== null && _c !== void 0 ? _c : 0;
                    SorterColumn = (_d = response_requirementQuery.SorterColumn) !== null && _d !== void 0 ? _d : "";
                    SortToggler = (_e = response_requirementQuery.SortToggler) !== null && _e !== void 0 ? _e : false;
                    TotalRows = (_f = response_requirementQuery.TotalRows) !== null && _f !== void 0 ? _f : 0;
                    TotalPages = (_g = response_requirementQuery.TotalPages) !== null && _g !== void 0 ? _g : 0;
                    //Query string
                    $("#requirement-requirement-query-string").attr("placeholder", "Search... (" + TotalRows + " records)");
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
                    if (((_h = response_requirementQuery === null || response_requirementQuery === void 0 ? void 0 : response_requirementQuery.lstRequirementModel) === null || _h === void 0 ? void 0 : _h.length) === 0) {
                        $("#requirement-requirement-lnk-previous-page-lg, #requirement-requirement-lnk-previous-page").attr("disabled", "disabled");
                        $("#requirement-requirement-lnk-first-page-lg, #requirement-requirement-lnk-first-page").attr("disabled", "disabled");
                        $("#requirement-requirement-lnk-next-page-lg, #requirement-requirement-lnk-next-page").attr("disabled", "disabled");
                        $("#requirement-requirement-lnk-last-page-lg, #requirement-requirement-lnk-last-page").attr("disabled", "disabled");
                        $("#requirement-requirement-total-pages-lg, #requirement-requirement-total-pages").html("1");
                        $("#requirement-requirement-actual-page-number-lg, #requirement-requirement-actual-page-number").html("1");
                    }
                    //Read data book
                    (_j = response_requirementQuery === null || response_requirementQuery === void 0 ? void 0 : response_requirementQuery.lstRequirementModel) === null || _j === void 0 ? void 0 : _j.forEach(function (row) {
                        TableContent += "<tr>\n    <!-- Checkbox -->\n    <td>\n        <div>\n            <input class=\"requirement-table-checkbox-for-row\" value=\"" + row.RequirementId + "\" type=\"checkbox\">\n        </div>\n    </td>\n    <!-- Data -->\n    <td class=\"text-left text-light\">\n        <i class=\"fas fa-key\"></i> " + row.RequirementId + "\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-toggle-on\"></i> " + (row.Active == true ? "Active <i class='text-success fas fa-circle'></i>" : "Not active <i class='text-danger fas fa-circle'></i>") + "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-calendar\"></i> " + row.DateTimeCreation + "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-calendar\"></i> " + row.DateTimeLastModification + "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-key\"></i> " + row.UserCreationId + "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-key\"></i> " + row.UserLastModificationId + "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong><i class=\"fas fa-font\">\n            </i> " + row.Title + "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <i class=\"fas fa-font\"></i> " + row.Body + "\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-key\"></i> " + row.RequirementStateId + "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-key\"></i> " + row.RequirementPriorityId + "\n        </strong>\n    </td>\n    <td class=\"text-left\">\n        <strong>\n            <i class=\"fas fa-key\"></i> " + row.UserEmployeeId + "\n        </strong>\n    </td>\n    \n    <!-- Actions -->\n    <td class=\"text-right\">\n        <a class=\"btn btn-icon-only text-primary\" href=\"/Requirement/PageRequirementNonQuery?RequirementId=" + row.RequirementId + "\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"Edit\">\n            <i class=\"fas fa-edit\"></i>\n        </a>\n        <div class=\"dropdown\">\n            <button class=\"btn btn-icon-only text-danger\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                <i class=\"fas fa-trash\"></i>\n            </button>\n            <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                <button class=\"dropdown-item text-danger requirement-requirement-table-delete-button\" value=\"" + row.RequirementId + "\" type=\"button\">\n                    <i class=\"fas fa-exclamation-triangle\"></i> Yes, delete\n                </button>\n            </div>\n        </div>\n        <div class=\"dropdown\">\n            <button class=\"btn btn-sm btn-icon-only text-primary\" href=\"#\" type=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                <i class=\"fas fa-ellipsis-v\"></i>\n            </button>\n            <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                <button type=\"button\" class=\"dropdown-item requirement-requirement-table-copy-button\" value=\"" + row.RequirementId + "\">\n                    <i class=\"fas fa-copy text-primary\"></i>&nbsp;Copy\n                </button>\n            </div>\n        </div>\n    </td>\n</tr>";
                        ListContent += "<div class=\"row mx-2\">\n    <div class=\"col-sm\">\n        <div class=\"card bg-gradient-primary mb-2\">\n            <div class=\"card-body\">\n                <div class=\"row\">\n                    <div class=\"col text-truncate\">\n                        <span class=\"text-white text-light mb-4\">\n                           RequirementId <i class=\"fas fa-key\"></i> " + row.RequirementId + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           Active <i class=\"fas fa-toggle-on\"></i> " + (row.Active == true ? "Active <i class='text-success fas fa-circle'></i>" : "Not active <i class='text-danger fas fa-circle'></i>") + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           DateTimeCreation <i class=\"fas fa-calendar\"></i> " + row.DateTimeCreation + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           DateTimeLastModification <i class=\"fas fa-calendar\"></i> " + row.DateTimeLastModification + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           UserCreationId <i class=\"fas fa-key\"></i> " + row.UserCreationId + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           UserLastModificationId <i class=\"fas fa-key\"></i> " + row.UserLastModificationId + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           Title <i class=\"fas fa-font\"></i> " + row.Title + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           Body <i class=\"fas fa-font\"></i> " + row.Body + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           RequirementStateId <i class=\"fas fa-key\"></i> " + row.RequirementStateId + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           RequirementPriorityId <i class=\"fas fa-key\"></i> " + row.RequirementPriorityId + "\n                        </span>\n                        <br/>\n                        <span class=\"text-white mb-4\">\n                           UserEmployeeId <i class=\"fas fa-key\"></i> " + row.UserEmployeeId + "\n                        </span>\n                        <br/>\n                        \n                    </div>\n                    <div class=\"col-auto\">\n                    </div>\n                </div>\n                <!-- Actions -->\n                <div class=\"row\">\n                    <div class=\"col\">\n                        <div class=\"justify-content-end text-right mt-2\">\n                            <div class=\"requirement-requirement-checkbox-list list-row-unchecked mb-2\">\n                                <a class=\"icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"check\">\n                                    <i class=\"fas fa-circle text-white\"></i>\n                                </a>\n                            </div>\n                            <input type=\"hidden\" value=\"" + row.RequirementId + "\"/>\n                            <a class=\"icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"/Requirement/PageRequirementNonQuery?RequirementId=" + row.RequirementId + "\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"edit\">\n                                <i class=\"fas fa-edit text-primary\"></i>\n                            </a>\n                            <div class=\"dropup\">\n                                <a class=\"icon icon-shape bg-white icon-sm text-primary rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\n                                    <i class=\"fas fa-ellipsis-v\"></i>\n                                </a>\n                                <div class=\"dropdown-menu dropdown-menu-right dropdown-menu-arrow\">\n                                    <button value=\"" + row.RequirementId + "\" class=\"dropdown-item text-primary requirement-requirement-list-copy-button\" type=\"button\">\n                                        <i class=\"fas fa-copy\"></i>&nbsp;Copy\n                                    </button>\n                                    <button value=\"" + row.RequirementId + "\" class=\"dropdown-item text-danger requirement-requirement-list-delete-button\" type=\"button\">\n                                        <i class=\"fas fa-trash\"></i>&nbsp;Delete\n                                    </button>\n                                </div>\n                            </div>\n                        </div>\n                    </div>\n                </div>\n            </div>\n        </div>\n    </div>\n</div>";
                    });
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
            complete: function () {
                //Execute ScrollDownNSearch function when the user scroll the page
                $(window).on("scroll", ScrollDownNSearch);
                //Add final content to TableContent
                TableContent += "</tbody>\n                                </table>";
                //Check button inside list view
                $(".requirement-requirement-checkbox-list").on("click", function (e) {
                    //Toggler
                    if ($(this).hasClass("list-row-checked")) {
                        $(this).html("<a class=\"icon icon-shape bg-white icon-sm rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"check\">\n                                                            <i class=\"fas fa-circle text-white\"></i>\n                                                        </a>");
                        $(this).removeClass("list-row-checked").addClass("list-row-unchecked");
                    }
                    else {
                        $(this).html("<a class=\"icon icon-shape bg-white icon-sm text-primary rounded-circle shadow\" href=\"javascript:void(0)\" role=\"button\" data-toggle=\"tooltip\" data-original-title=\"check\">\n                                                            <i class=\"fas fa-check\"></i>\n                                                        </a>");
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
                    var RequirementId = $(this).val();
                    Requirement_TsModel_1.RequirementModel.DeleteByRequirementId(RequirementId).subscribe({
                        next: function (newrow) {
                        },
                        complete: function () {
                            ValidateAndSearch();
                            //Show OK message
                            $("#requirement-requirement-button-error-message-in-card").hide();
                            $("#requirement-requirement-button-ok-message-in-card").html("<strong>\n                                                                    <i class=\"fas fa-check\"></i>\n                                                                </strong> Row deleted successfully");
                            $("#requirement-requirement-button-ok-message-in-card").show();
                        },
                        error: function (err) {
                            //Related to error message
                            $("#requirement-requirement-error-message-title").html("RequirementModel.DeleteByRequirementId(RequirementId).subscribe(...)");
                            $("#requirement-requirement-error-message-text").html(err);
                            $("#requirement-requirement-button-error-message-in-card").show();
                        }
                    });
                });
                //Copy button in table and list
                $("div.dropdown-menu button.requirement-requirement-table-copy-button, div.dropdown-menu button.requirement-requirement-list-copy-button").on("click", function (e) {
                    var RequirementId = $(this).val();
                    Requirement_TsModel_1.RequirementModel.CopyByRequirementId(RequirementId).subscribe({
                        next: function (newrow) {
                        },
                        complete: function () {
                            ValidateAndSearch();
                            //Show OK message
                            $("#requirement-requirement-button-error-message-in-card").hide();
                            $("#requirement-requirement-button-ok-message-in-card").html("<strong>\n                                                                    <i class=\"fas fa-check\"></i>\n                                                                </strong> Row copied successfully");
                            $("#requirement-requirement-button-ok-message-in-card").show();
                        },
                        error: function (err) {
                            //Show error message
                            $("#requirement-requirement-error-message-title").html("RequirementModel.CopyByRequirementId(RequirementId).subscribe(...)");
                            $("#requirement-requirement-error-message-text").html(err);
                            $("#requirement-requirement-button-error-message-in-card").show();
                        }
                    });
                });
            },
            error: function (err) {
                //Show error message
                $("#requirement-requirement-error-message-title").html("RequirementModel.SelectAllPaged(request_requirementmodelQ).subscribe(...)");
                $("#requirement-requirement-error-message-text").html(err);
                $("#requirement-requirement-button-error-message-in-card").show();
            }
        });
    };
    return RequirementQuery;
}());
function ValidateAndSearch() {
    //Hide error and OK message button
    $("#requirement-requirement-button-error-message-in-card").hide();
    $("#requirement-requirement-button-ok-message-in-card").hide();
    var _requirementmodelQuery = {
        QueryString: QueryString,
        ActualPageNumber: ActualPageNumber,
        RowsPerPage: RowsPerPage,
        SorterColumn: SorterColumn,
        SortToggler: SortToggler,
        TotalRows: TotalRows,
        TotalPages: TotalPages
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
    var _a, _b;
    //If undefined, set QueryString to "" value
    QueryString = (_b = ((_a = $(this).val()) === null || _a === void 0 ? void 0 : _a.toString())) !== null && _b !== void 0 ? _b : "";
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
    var _a, _b, _c, _d, _e, _f, _g, _h;
    var WindowsTopDistance = (_a = $(window).scrollTop()) !== null && _a !== void 0 ? _a : 0;
    var WindowsBottomDistance = ((_b = $(window).scrollTop()) !== null && _b !== void 0 ? _b : 0) + ((_c = $(window).innerHeight()) !== null && _c !== void 0 ? _c : 0);
    var CardsFooterTopPosition = (_e = (_d = $("#requirement-requirement-search-more-button-in-list").offset()) === null || _d === void 0 ? void 0 : _d.top) !== null && _e !== void 0 ? _e : 0;
    var CardsFooterBottomPosition = ((_g = (_f = $("#requirement-requirement-search-more-button-in-list").offset()) === null || _f === void 0 ? void 0 : _f.top) !== null && _g !== void 0 ? _g : 0) + ((_h = $("#requirement-requirement-search-more-button-in-list").outerHeight()) !== null && _h !== void 0 ? _h : 0);
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
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#requirement-requirement-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_1 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
                CheckedRows_1.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_1.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_1.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_1.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/Requirement/Requirement/1/ExportAsPDF/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#requirement-requirement-export-message").html("<strong>Exporting as PDF</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //Show download button for PDF file
            $("#requirement-requirement-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/PDFFiles/Requirement/Requirement/Requirement_" + DateTimeNow.AjaxForString + ".pdf\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-pdf\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>");
            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html("<strong>\n                                                                    <i class=\"fas fa-check\"></i>\n                                                                </strong> Conversion completed");
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: function (err) {
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
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#requirement-requirement-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_2 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
                CheckedRows_2.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_2.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_2.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_2.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/Requirement/Requirement/1/ExportAsExcel/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#requirement-requirement-export-message").html("<strong>Exporting as Excel</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //Show download button for Excel file
            $("#requirement-requirement-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/ExcelFiles/Requirement/Requirement/Requirement_" + DateTimeNow.AjaxForString + ".xlsx\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-excel\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>");
            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html("<strong>\n                                                                    <i class=\"fas fa-check\"></i>\n                                                                </strong> Conversion completed");
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: function (err) {
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
    var ExportationType = "";
    var DateTimeNow;
    var Body = {};
    //Define a header for HTTP protocol with Accept (receiver data type) and Content-Type (sender data type)
    var Header = {
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=utf-8'
    };
    if ($("#requirement-requirement-export-rows-all-checkbox").is(":checked")) {
        ExportationType = "All";
    }
    else {
        ExportationType = "JustChecked";
        var CheckedRows_3 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
                CheckedRows_3.push($(this).val());
            });
            Body = {
                AjaxForString: CheckedRows_3.toString()
            };
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_3.push($(this).next().val());
            });
            Body = {
                AjaxForString: CheckedRows_3.toString()
            };
        }
    }
    Rx.from(ajax_1.ajax.post("/api/Requirement/Requirement/1/ExportAsCSV/" + ExportationType, Body, Header)).subscribe({
        next: function (newrow) {
            $("#requirement-requirement-export-message").html("<strong>Exporting as CSV</strong>");
            DateTimeNow = newrow.response;
        },
        complete: function () {
            //Show download button for CSV file
            $("#requirement-requirement-export-message").html("<a class=\"btn btn-icon btn-success\" href=\"/CSVFiles/Requirement/Requirement/Requirement_" + DateTimeNow.AjaxForString + ".csv\" type=\"button\" download>\n                                            <span class=\"btn-inner--icon\"><i class=\"fas fa-file-csv\"></i></span>\n                                            <span class=\"btn-inner--text\">Download</span>\n                                        </a>");
            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html("<strong>\n                                                                    <i class=\"fas fa-check\"></i>\n                                                                </strong> Conversion completed");
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: function (err) {
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
    var CopyType = "";
    var Body = {};
    if ($("#requirement-requirement-copy-rows-all-checkbox").is(":checked")) {
        CopyType = "All";
    }
    else {
        CopyType = "JustChecked";
        var CheckedRows_4 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
                CheckedRows_4.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_4.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows_4.toString()
        };
    }
    Requirement_TsModel_1.RequirementModel.CopyManyOrAll(CopyType, Body).subscribe({
        next: function (newrow) {
        },
        complete: function () {
            ValidateAndSearch();
            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html("<strong>\n                                                                    <i class=\"fas fa-check\"></i>\n                                                                </strong> Rows copied successfully");
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: function (err) {
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
    var DeleteType = "";
    var Body = {};
    if ($("#requirement-requirement-copy-rows-all-checkbox").is(":checked")) {
        DeleteType = "All";
    }
    else {
        DeleteType = "JustChecked";
        var CheckedRows_5 = new Array();
        if (ViewToggler == "Table") {
            $("tr td div input.requirement-table-checkbox-for-row:checked").each(function () {
                CheckedRows_5.push($(this).val());
            });
        }
        else {
            $("div .list-row-checked").each(function () {
                //With .next() we access to input type hidden
                CheckedRows_5.push($(this).next().val());
            });
        }
        Body = {
            AjaxForString: CheckedRows_5.toString()
        };
    }
    Requirement_TsModel_1.RequirementModel.DeleteManyOrAll(DeleteType, Body).subscribe({
        next: function (newrow) {
        },
        complete: function () {
            ValidateAndSearch();
            //Show OK message
            $("#requirement-requirement-button-ok-message-in-card").html("<strong>\n                                                                    <i class=\"fas fa-check\"></i>\n                                                                </strong> Rows deleted successfully");
            $("#requirement-requirement-button-ok-message-in-card").show();
        },
        error: function (err) {
            //Show error message
            $("#requirement-requirement-error-message-title").html("RequirementModel.Copy(CopyType).subscribe(...)");
            $("#requirement-requirement-error-message-text").html(err);
            $("#requirement-requirement-button-error-message-in-card").show();
        }
    });
});
//# sourceMappingURL=RequirementQuery_jQuery.js.map