"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.clientapplicationmodelQuery = exports.ClientApplicationModel = void 0;
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
//8 fields | Last modification on: 24/12/2022 6:47:42 | Stack: 9
var ClientApplicationModel = /** @class */ (function () {
    function ClientApplicationModel() {
    }
    //Queries
    ClientApplicationModel.Select1ByClientApplicationId = function (ClientApplicationId) {
        var URL = "/api/Requirement/ClientApplication/1/Select1ByClientApplicationIdToJSON/" + ClientApplicationId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ClientApplicationModel.SelectAll = function () {
        var URL = "/api/Requirement/ClientApplication/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ClientApplicationModel.SelectAllPaged = function (clientapplicationmodelQuery) {
        var URL = "/api/Requirement/ClientApplication/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: clientapplicationmodelQuery.QueryString,
            ActualPageNumber: clientapplicationmodelQuery.ActualPageNumber,
            RowsPerPage: clientapplicationmodelQuery.RowsPerPage,
            SorterColumn: clientapplicationmodelQuery.SorterColumn,
            SortToggler: clientapplicationmodelQuery.SortToggler,
            RowCount: clientapplicationmodelQuery.TotalRows,
            TotalPages: clientapplicationmodelQuery.TotalPages,
            lstClientApplicationModel: clientapplicationmodelQuery.lstClientApplicationModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    ClientApplicationModel.DeleteByClientApplicationId = function (ClientApplicationId) {
        var URL = "/api/Requirement/ClientApplication/1/DeleteByClientApplicationId/" + ClientApplicationId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    ClientApplicationModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/ClientApplication/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ClientApplicationModel.CopyByClientApplicationId = function (ClientApplicationId) {
        var URL = "/api/Requirement/ClientApplication/1/CopyByClientApplicationId/" + ClientApplicationId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ClientApplicationModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/ClientApplicationing/ClientApplication/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return ClientApplicationModel;
}());
exports.ClientApplicationModel = ClientApplicationModel;
var clientapplicationmodelQuery = /** @class */ (function () {
    function clientapplicationmodelQuery() {
    }
    return clientapplicationmodelQuery;
}());
exports.clientapplicationmodelQuery = clientapplicationmodelQuery;
//# sourceMappingURL=ClientApplication_TsModel.js.map