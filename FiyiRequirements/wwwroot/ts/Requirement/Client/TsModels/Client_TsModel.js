"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.clientmodelQuery = exports.ClientModel = void 0;
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
//11 fields | Sub-models: 2 models  | Last modification on: 25/12/2022 12:12:08 | Stack: 9
var ClientModel = /** @class */ (function () {
    function ClientModel() {
    }
    //Queries
    ClientModel.Select1ByClientId = function (ClientId) {
        var URL = "/api/Requirement/Client/1/Select1ByClientIdToJSON/" + ClientId;
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ClientModel.SelectAll = function () {
        var URL = "/api/Requirement/Client/1/SelectAllToJSON";
        return Rx.from((0, ajax_1.ajax)(URL));
    };
    ClientModel.SelectAllPaged = function (clientmodelQuery) {
        var URL = "/api/Requirement/Client/1/SelectAllPagedToJSON";
        var Body = {
            QueryString: clientmodelQuery.QueryString,
            ActualPageNumber: clientmodelQuery.ActualPageNumber,
            RowsPerPage: clientmodelQuery.RowsPerPage,
            SorterColumn: clientmodelQuery.SorterColumn,
            SortToggler: clientmodelQuery.SortToggler,
            RowCount: clientmodelQuery.TotalRows,
            TotalPages: clientmodelQuery.TotalPages,
            lstClientModel: clientmodelQuery.lstClientModel
        };
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.put(URL, Body, Header));
    };
    //Non-Queries
    ClientModel.DeleteByClientId = function (ClientId) {
        var URL = "/api/Requirement/Client/1/DeleteByClientId/" + ClientId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.delete(URL, Header));
    };
    ClientModel.DeleteManyOrAll = function (DeleteType, Body) {
        var URL = "/api/Requirement/Client/1/DeleteManyOrAll/" + DeleteType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ClientModel.CopyByClientId = function (ClientId) {
        var URL = "/api/Requirement/Client/1/CopyByClientId/" + ClientId;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        var Body = {};
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    ClientModel.CopyManyOrAll = function (CopyType, Body) {
        var URL = "/api/Clienting/Client/1/CopyManyOrAll/" + CopyType;
        var Header = {
            "Accept": "application/json",
            "Content-Type": "application/json; charset=utf-8"
        };
        return Rx.from(ajax_1.ajax.post(URL, Body, Header));
    };
    return ClientModel;
}());
exports.ClientModel = ClientModel;
var clientmodelQuery = /** @class */ (function () {
    function clientmodelQuery() {
    }
    return clientmodelQuery;
}());
exports.clientmodelQuery = clientmodelQuery;
//# sourceMappingURL=Client_TsModel.js.map