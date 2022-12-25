"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.requirementfilemodelQuery = exports.RequirementFileModel = void 0;
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
//9 fields | Last modification on: 24/12/2022 6:48:16 | Stack: 9
var RequirementFileModel = /** @class */ (function () {
    function RequirementFileModel() {
    }
    //Queries
    RequirementFileModel.Select1ByRequirementFileId = function (RequirementFileId) {
        var URL = "/api/Requirement/RequirementFile/1/Select1ByRequirementFileIdToJSON/" + RequirementFileId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RequirementFileModel.SelectAll = function () {
        var URL = "/api/Requirement/RequirementFile/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RequirementFileModel.SelectAllPaged = function (requirementfilemodelQuery) {
        var URL = "/api/Requirement/RequirementFile/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: requirementfilemodelQuery.QueryString,
            ActualPageNumber: requirementfilemodelQuery.ActualPageNumber,
            RowsPerPage: requirementfilemodelQuery.RowsPerPage,
            SorterColumn: requirementfilemodelQuery.SorterColumn,
            SortToggler: requirementfilemodelQuery.SortToggler,
            RowCount: requirementfilemodelQuery.TotalRows,
            TotalPages: requirementfilemodelQuery.TotalPages,
            lstRequirementFileModel: requirementfilemodelQuery.lstRequirementFileModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    RequirementFileModel.DeleteByRequirementFileId = function (RequirementFileId) {
        var URL = "/api/Requirement/RequirementFile/1/DeleteByRequirementFileId/" + RequirementFileId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    RequirementFileModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/RequirementFile/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RequirementFileModel.CopyByRequirementFileId = function (RequirementFileId) {
        var URL = "/api/Requirement/RequirementFile/1/CopyByRequirementFileId/" + RequirementFileId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RequirementFileModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/RequirementFileing/RequirementFile/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return RequirementFileModel;
}());
exports.RequirementFileModel = RequirementFileModel;
var requirementfilemodelQuery = /** @class */ (function () {
    function requirementfilemodelQuery() {
    }
    return requirementfilemodelQuery;
}());
exports.requirementfilemodelQuery = requirementfilemodelQuery;
//# sourceMappingURL=RequirementFile_TsModel.js.map