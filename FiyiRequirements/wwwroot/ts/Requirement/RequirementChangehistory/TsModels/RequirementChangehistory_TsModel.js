"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.requirementchangehistorymodelQuery = exports.RequirementChangehistoryModel = void 0;
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
//9 fields | Last modification on: 24/12/2022 6:48:12 | Stack: 9
var RequirementChangehistoryModel = /** @class */ (function () {
    function RequirementChangehistoryModel() {
    }
    //Queries
    RequirementChangehistoryModel.Select1ByRequirementChangehistoryId = function (RequirementChangehistoryId) {
        var URL = "/api/Requirement/RequirementChangehistory/1/Select1ByRequirementChangehistoryIdToJSON/" + RequirementChangehistoryId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RequirementChangehistoryModel.SelectAll = function () {
        var URL = "/api/Requirement/RequirementChangehistory/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RequirementChangehistoryModel.SelectAllPaged = function (requirementchangehistorymodelQuery) {
        var URL = "/api/Requirement/RequirementChangehistory/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: requirementchangehistorymodelQuery.QueryString,
            ActualPageNumber: requirementchangehistorymodelQuery.ActualPageNumber,
            RowsPerPage: requirementchangehistorymodelQuery.RowsPerPage,
            SorterColumn: requirementchangehistorymodelQuery.SorterColumn,
            SortToggler: requirementchangehistorymodelQuery.SortToggler,
            RowCount: requirementchangehistorymodelQuery.TotalRows,
            TotalPages: requirementchangehistorymodelQuery.TotalPages,
            lstRequirementChangehistoryModel: requirementchangehistorymodelQuery.lstRequirementChangehistoryModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    RequirementChangehistoryModel.DeleteByRequirementChangehistoryId = function (RequirementChangehistoryId) {
        var URL = "/api/Requirement/RequirementChangehistory/1/DeleteByRequirementChangehistoryId/" + RequirementChangehistoryId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    RequirementChangehistoryModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/RequirementChangehistory/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RequirementChangehistoryModel.CopyByRequirementChangehistoryId = function (RequirementChangehistoryId) {
        var URL = "/api/Requirement/RequirementChangehistory/1/CopyByRequirementChangehistoryId/" + RequirementChangehistoryId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RequirementChangehistoryModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/RequirementChangehistorying/RequirementChangehistory/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return RequirementChangehistoryModel;
}());
exports.RequirementChangehistoryModel = RequirementChangehistoryModel;
var requirementchangehistorymodelQuery = /** @class */ (function () {
    function requirementchangehistorymodelQuery() {
    }
    return requirementchangehistorymodelQuery;
}());
exports.requirementchangehistorymodelQuery = requirementchangehistorymodelQuery;
//# sourceMappingURL=RequirementChangehistory_TsModel.js.map