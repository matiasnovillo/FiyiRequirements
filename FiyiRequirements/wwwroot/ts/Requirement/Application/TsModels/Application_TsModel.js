"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.applicationmodelQuery = exports.ApplicationModel = void 0;
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
//9 fields | Last modification on: 24/12/2022 6:47:27 | Stack: 9
var ApplicationModel = /** @class */ (function () {
    function ApplicationModel() {
    }
    //Queries
    ApplicationModel.Select1ByApplicationId = function (ApplicationId) {
        var URL = "/api/Requirement/Application/1/Select1ByApplicationIdToJSON/" + ApplicationId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ApplicationModel.SelectAll = function () {
        var URL = "/api/Requirement/Application/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ApplicationModel.SelectAllPaged = function (applicationmodelQuery) {
        var URL = "/api/Requirement/Application/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: applicationmodelQuery.QueryString,
            ActualPageNumber: applicationmodelQuery.ActualPageNumber,
            RowsPerPage: applicationmodelQuery.RowsPerPage,
            SorterColumn: applicationmodelQuery.SorterColumn,
            SortToggler: applicationmodelQuery.SortToggler,
            RowCount: applicationmodelQuery.TotalRows,
            TotalPages: applicationmodelQuery.TotalPages,
            lstApplicationModel: applicationmodelQuery.lstApplicationModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    ApplicationModel.DeleteByApplicationId = function (ApplicationId) {
        var URL = "/api/Requirement/Application/1/DeleteByApplicationId/" + ApplicationId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    ApplicationModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/Application/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ApplicationModel.CopyByApplicationId = function (ApplicationId) {
        var URL = "/api/Requirement/Application/1/CopyByApplicationId/" + ApplicationId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ApplicationModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Applicationing/Application/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return ApplicationModel;
}());
exports.ApplicationModel = ApplicationModel;
var applicationmodelQuery = /** @class */ (function () {
    function applicationmodelQuery() {
    }
    return applicationmodelQuery;
}());
exports.applicationmodelQuery = applicationmodelQuery;
//# sourceMappingURL=Application_TsModel.js.map