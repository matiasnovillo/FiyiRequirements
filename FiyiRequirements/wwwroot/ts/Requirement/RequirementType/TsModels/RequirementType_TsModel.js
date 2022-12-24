"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.requirementtypemodelQuery = exports.RequirementTypeModel = void 0;
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
//8 fields | Last modification on: 24/12/2022 6:47:16 | Stack: 9
var RequirementTypeModel = /** @class */ (function () {
    function RequirementTypeModel() {
    }
    //Queries
    RequirementTypeModel.Select1ByRequirementTypeId = function (RequirementTypeId) {
        var URL = "/api/Requirement/RequirementType/1/Select1ByRequirementTypeIdToJSON/" + RequirementTypeId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RequirementTypeModel.SelectAll = function () {
        var URL = "/api/Requirement/RequirementType/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    RequirementTypeModel.SelectAllPaged = function (requirementtypemodelQuery) {
        var URL = "/api/Requirement/RequirementType/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: requirementtypemodelQuery.QueryString,
            ActualPageNumber: requirementtypemodelQuery.ActualPageNumber,
            RowsPerPage: requirementtypemodelQuery.RowsPerPage,
            SorterColumn: requirementtypemodelQuery.SorterColumn,
            SortToggler: requirementtypemodelQuery.SortToggler,
            RowCount: requirementtypemodelQuery.TotalRows,
            TotalPages: requirementtypemodelQuery.TotalPages,
            lstRequirementTypeModel: requirementtypemodelQuery.lstRequirementTypeModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    RequirementTypeModel.DeleteByRequirementTypeId = function (RequirementTypeId) {
        var URL = "/api/Requirement/RequirementType/1/DeleteByRequirementTypeId/" + RequirementTypeId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    RequirementTypeModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/RequirementType/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RequirementTypeModel.CopyByRequirementTypeId = function (RequirementTypeId) {
        var URL = "/api/Requirement/RequirementType/1/CopyByRequirementTypeId/" + RequirementTypeId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    RequirementTypeModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/RequirementTypeing/RequirementType/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return RequirementTypeModel;
}());
exports.RequirementTypeModel = RequirementTypeModel;
var requirementtypemodelQuery = /** @class */ (function () {
    function requirementtypemodelQuery() {
    }
    return requirementtypemodelQuery;
}());
exports.requirementtypemodelQuery = requirementtypemodelQuery;
//# sourceMappingURL=RequirementType_TsModel.js.map