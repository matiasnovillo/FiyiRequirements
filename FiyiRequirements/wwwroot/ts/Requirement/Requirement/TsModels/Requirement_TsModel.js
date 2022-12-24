"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.requirementmodelQuery = exports.RequirementModel = void 0;
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
//13 fields | Last modification on: 24/12/2022 6:48:02 | Stack: 9
var RequirementModel = /** @class */ (function () {
    function RequirementModel() {
    }
    //Queries
    RequirementModel.Select1ByRequirementId = function (RequirementId) {
        var URL = "/api/Requirement/Requirement/1/Select1ByRequirementIdToJSON/" + RequirementId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RequirementModel.SelectAll = function () {
        var URL = "/api/Requirement/Requirement/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RequirementModel.SelectAllPaged = function (requirementmodelQuery) {
        var URL = "/api/Requirement/Requirement/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: requirementmodelQuery.QueryString,
            ActualPageNumber: requirementmodelQuery.ActualPageNumber,
            RowsPerPage: requirementmodelQuery.RowsPerPage,
            SorterColumn: requirementmodelQuery.SorterColumn,
            SortToggler: requirementmodelQuery.SortToggler,
            RowCount: requirementmodelQuery.TotalRows,
            TotalPages: requirementmodelQuery.TotalPages,
            lstRequirementModel: requirementmodelQuery.lstRequirementModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    RequirementModel.DeleteByRequirementId = function (RequirementId) {
        var URL = "/api/Requirement/Requirement/1/DeleteByRequirementId/" + RequirementId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    RequirementModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/Requirement/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RequirementModel.CopyByRequirementId = function (RequirementId) {
        var URL = "/api/Requirement/Requirement/1/CopyByRequirementId/" + RequirementId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RequirementModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Requirementing/Requirement/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return RequirementModel;
}());
exports.RequirementModel = RequirementModel;
var requirementmodelQuery = /** @class */ (function () {
    function requirementmodelQuery() {
    }
    return requirementmodelQuery;
}());
exports.requirementmodelQuery = requirementmodelQuery;
//# sourceMappingURL=Requirement_TsModel.js.map