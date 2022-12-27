"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.userapplicationmodelQuery = exports.UserApplicationModel = void 0;
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
//8 fields | Sub-models: 0 models  | Last modification on: 27/12/2022 16:32:18 | Stack: 9
var UserApplicationModel = /** @class */ (function () {
    function UserApplicationModel() {
    }
    //Queries
    UserApplicationModel.Select1ByUserApplicationId = function (UserApplicationId) {
        var URL = "/api/Requirement/UserApplication/1/Select1ByUserApplicationIdToJSON/" + UserApplicationId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    UserApplicationModel.SelectAll = function () {
        var URL = "/api/Requirement/UserApplication/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    UserApplicationModel.SelectAllPaged = function (userapplicationmodelQuery) {
        var URL = "/api/Requirement/UserApplication/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: userapplicationmodelQuery.QueryString,
            ActualPageNumber: userapplicationmodelQuery.ActualPageNumber,
            RowsPerPage: userapplicationmodelQuery.RowsPerPage,
            SorterColumn: userapplicationmodelQuery.SorterColumn,
            SortToggler: userapplicationmodelQuery.SortToggler,
            RowCount: userapplicationmodelQuery.TotalRows,
            TotalPages: userapplicationmodelQuery.TotalPages,
            lstUserApplicationModel: userapplicationmodelQuery.lstUserApplicationModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    UserApplicationModel.DeleteByUserApplicationId = function (UserApplicationId) {
        var URL = "/api/Requirement/UserApplication/1/DeleteByUserApplicationId/" + UserApplicationId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    UserApplicationModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/UserApplication/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    UserApplicationModel.CopyByUserApplicationId = function (UserApplicationId) {
        var URL = "/api/Requirement/UserApplication/1/CopyByUserApplicationId/" + UserApplicationId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    UserApplicationModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/UserApplicationing/UserApplication/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return UserApplicationModel;
}());
exports.UserApplicationModel = UserApplicationModel;
var userapplicationmodelQuery = /** @class */ (function () {
    function userapplicationmodelQuery() {
    }
    return userapplicationmodelQuery;
}());
exports.userapplicationmodelQuery = userapplicationmodelQuery;
//# sourceMappingURL=UserApplication_TsModel.js.map